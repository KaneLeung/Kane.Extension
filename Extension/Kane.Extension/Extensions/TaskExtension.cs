// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.6
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

#if !NET40
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kane.Extension
{
    /// <summary>
    /// 异步多线程类扩展方法
    /// </summary>
    public static class TaskExtension
    {
        #region 设置Task过期时间 + TimeoutCancel(this Task task, int milliseconds, string message = "操作已超时。")
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <param name="task">异步操作</param>
        /// <param name="milliseconds">超时时间。单位：毫秒</param>
        /// <param name="message">超时返回的信息，默认为【操作已超时。】</param>
        /// <returns></returns>
        public static async Task TimeoutCancel(this Task task, int milliseconds, string message = "操作已超时。")
        {
            var cancelToken = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(milliseconds, cancelToken.Token));
            if (completedTask == task) cancelToken.Cancel();
            else throw new TimeoutException(message);
        }
        #endregion

        #region 设置Task过期时间 + TimeoutCancel(this Task task, TimeSpan timeoutDelay, string message = "操作已超时。")
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <param name="task">异步操作</param>
        /// <param name="timeoutDelay">超时时间</param>
        /// <param name="message">超时返回的信息，默认为【操作已超时。】</param>
        /// <returns></returns>
        public static async Task TimeoutCancel(this Task task, TimeSpan timeoutDelay, string message = "操作已超时。")
        {
            var cancelToken = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeoutDelay, cancelToken.Token));
            if (completedTask == task) cancelToken.Cancel();
            else throw new TimeoutException(message);
        }
        #endregion

        #region 设置Task过期时间 + TimeoutCancel<T>(this Task<T> task, int milliseconds)
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="task">异步操作</param>
        /// <param name="milliseconds">超时时间。单位：毫秒</param>
        /// <param name="message">超时返回的信息，默认为【操作已超时。】</param>
        /// <returns></returns>
        public static async Task<T> TimeoutCancel<T>(this Task<T> task, int milliseconds, string message = "操作已超时。")
        {
            var cancelToken = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(milliseconds, cancelToken.Token));
            if (completedTask == task)
            {
                cancelToken.Cancel();
                return task.Result;
            }
            else throw new TimeoutException(message);
        }
        #endregion

        #region 设置Task过期时间 + TimeoutCancel<T>(this Task<T> task, TimeSpan timeoutDelay, string message = "操作已超时。")
        /// <summary>
        /// 设置Task过期时间
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="task">异步操作</param>
        /// <param name="timeoutDelay">超时时间</param>
        /// <param name="message">超时返回的信息，默认为【操作已超时。】</param>
        /// <returns></returns>
        public static async Task<T> TimeoutCancel<T>(this Task<T> task, TimeSpan timeoutDelay, string message = "操作已超时。")
        {
            var cancelToken = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeoutDelay, cancelToken.Token));
            if (completedTask == task)
            {
                cancelToken.Cancel();
                return task.Result;
            }
            else throw new TimeoutException(message);
        }
        #endregion
    }
}
#endif