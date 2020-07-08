#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ProcessHelper
* 类 描 述 ：进程帮助类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/07/08 10:24:14
* 更新时间 ：2020/07/08 10:24:14
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kane.Extension
{
    /// <summary>
    /// 进程帮助类
    /// </summary>
    public class ProcessHelper
    {
        #region 根据文件名，运行进程，一般用过命令行 + RunCommand(string fileName, string arguments)
        /// <summary>
        /// 根据文件名，运行进程，一般用过命令行
        /// </summary>
        /// <param name="fileName">启动进程时使用的应用程序或文档的文件名</param>
        /// <param name="arguments">进程启动时要传递给应用程序的命令行参数</param>
        /// <returns>每一行结果</returns>
        public List<string> RunCommand(string fileName, string arguments)
        {
            Process process = null;
            try
            {
                var startInfo = new ProcessStartInfo(fileName, arguments)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                process = Process.Start(startInfo);
#if !NET40
                var outputResult = process.StandardOutput.ReadToEndAsync();
                var errorResult = process.StandardError.ReadToEndAsync();
                if (!process.WaitForExit(10000)) try { process?.Kill(); } catch { }//如果超时10秒则退出进程
                if (Task.WaitAll(new Task[] { outputResult, errorResult }, 10000))
                {
                    var temp = $"{outputResult.Result}{Environment.NewLine}{errorResult.Result}".Trim();
                    return temp.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                return new List<string>();
#else
                var outputResult = process.StandardOutput.ReadToEnd();
                var errorResult = process.StandardError.ReadToEnd();
                if (!process.WaitForExit(10000)) try { process?.Kill(); } catch { }//如果超时10秒则退出进程
                var temp = $"{outputResult}{Environment.NewLine}{errorResult}".Trim();
                return temp.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
#endif
            }
            catch (Exception)
            {
                return new List<string>();
            }
            finally
            {
                process?.Close();
            }
        }
        #endregion

        #region 根据端口，获取进程ID + GetPortPid(int port)
        /// <summary>
        /// 根据端口，获取进程ID
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns></returns>
        public int GetPortPid(int port)
        {
            int portColumnIndex = 1;//【Windows】命令行中，返回【端口】信息的列的索引
            int pidColumnIndex = 4;//【Windows】命令行中，返回【Pid】信息的列的索引
            string pidRegex = string.Empty;
            List<string[]> commandResult = new List<string[]>();
#if NETCOREAPP
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                RunCommand("netstat", $"-anv -p tcp").ForEach(k => commandResult.Add(k.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
                RunCommand("netstat", $"-anv -p udp").ForEach(k => commandResult.Add(k.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
                portColumnIndex = 3;
                pidColumnIndex = 8;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                RunCommand("ss", $"-tunlp").ForEach(k => commandResult.Add(k.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
                portColumnIndex = 4;
                pidColumnIndex = 6;
                pidRegex = @"(?:^|"",|"",pid=)(\d+)";
            }
            else RunCommand("netstat", $"-ano").ForEach(k => commandResult.Add(k.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
#else
            RunCommand("netstat", $"-ano").ForEach(k => commandResult.Add(k.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)));
#endif
            foreach (var line in commandResult)
            {
                if (line.Length <= portColumnIndex || line.Length <= pidColumnIndex) continue;
                try
                {
                    var match = Regex.Match(line[portColumnIndex], $"[.:]({port})");
                    if (match.Success)
                    {
                        if (pidRegex.IsNullOrEmpty()) return line[pidColumnIndex].ToInt(-1);//转换失败返回-1;
                        else
                        {
                            match = Regex.Match(line[pidColumnIndex], pidRegex);
                            if (match.Success) return match.Groups[1].Value.ToInt(-1);//转换失败返回-1;
                        }
                    }
                }
                catch { }//出错，则继续
            }
            return -1;
        }
        #endregion

        #region 根据进程名称，终止进程 + KillProcess(string process, bool ignoreCase = true, bool force = false, bool tree = true)
        /// <summary>
        /// 根据进程名称，终止进程
        /// </summary>
        /// <param name="process">进程名称，包含后缀名</param>
        /// <param name="ignoreCase">是否忽略进程名字大小写，只对【Linux】和【OSX】系统生效</param>
        /// <param name="force">是否强制终止进程</param>
        /// <param name="tree">是否终止由它启用的子进程</param>
        /// <returns></returns>
        public bool KillProcess(string process, bool ignoreCase = true, bool force = false, bool tree = true)
        {
            var arguments = new List<string>();
            try
            {
#if NETCOREAPP
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (force) arguments.Add("-9");
                    if (ignoreCase) arguments.Add("-i");
                    arguments.Add(process);
                    RunCommand("pkill", string.Join(" ", arguments));
                    //DOTO:返回的结果还没有上机调试
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (force) arguments.Add("-9");
                    if (ignoreCase) arguments.Add("-I");
                    arguments.Add(process);
                    RunCommand("killall", string.Join(" ", arguments));//注意，有些系统没有安装killall命令，要安装【yum install psmisc -y】
                    //DOTO:返回的结果还没有上机调试
                }
                else
                {
#endif
                    if (force) arguments.Add("/F");
                    if (tree) arguments.Add("/T");
                    arguments.Add("/IM");
                    arguments.Add(process);
                    return RunCommand("taskkill", string.Join(" ", arguments))?.FirstOrDefault()?.StartsWith("SUCCESS", "成功") ?? false;
#if NETCOREAPP
                }
                return true;
#endif
            }
            catch { }
            return false;
        }
        #endregion

        #region 根据进程【PID】，终止进程 + KillProcess(int pid, bool force = false, bool tree = true)
        /// <summary>
        /// 根据进程【PID】，终止进程
        /// </summary>
        /// <param name="pid">进程【PID】</param>
        /// <param name="force">是否强制终止进程</param>
        /// <param name="tree">是否终止由它启用的子进程</param>
        /// <returns></returns>
        public bool KillProcess(int pid, bool force = false, bool tree = true)
        {
            if (pid == -1) return false;
            var arguments = new List<string>();
            try
            {
#if NETCOREAPP
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (force) arguments.Add("-9");
                    arguments.Add(pid.ToString());
                    RunCommand("kill", string.Join(" ", arguments));
                    //DOTO:返回的结果还没有上机调试
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (force) arguments.Add("-9");
                    arguments.Add(pid.ToString());
                    RunCommand("kill", string.Join(" ", arguments));
                    //DOTO:返回的结果还没有上机调试
                }
                else
                {
#endif
                    if (force) arguments.Add("/F");
                    if (tree) arguments.Add("/T");
                    arguments.Add("/PID");
                    arguments.Add(pid.ToString());
                    return RunCommand("taskkill", string.Join(" ", arguments))?.FirstOrDefault()?.StartsWith("SUCCESS", "成功") ?? false;
#if NETCOREAPP
                }
                return true;
#endif
            }
            catch { }
            return false;
        }
        #endregion
    }
}