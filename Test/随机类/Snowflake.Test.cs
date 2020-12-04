#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension.Test
* 项目描述 ：
* 类 名 称 ：Snowflake
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Test
* 机器名称 ：PC-0432 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/12/04 15:31:18
* 更新时间 ：2020/12/04 15:31:18
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if !NET40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class Snowflake
    {
        [TestMethod]
        public void SnowflakeTest()
        {
            var snowflake = new Kane.Extension.Snowflake(1L, 1L);
            var threadCount = 10;
            var IDCount = 10000;
            List<long> historyIDs = new List<long>();
            HashSet<long> duplicateIDs = new HashSet<long>();
            // 10个线程，分别生成 count 个ID
            var tasks = Enumerable.Range(1, threadCount).Select(x =>
            {
                return Task.Run(() =>
                {
                    for (int i = 0; i < IDCount; i++)
                    {
                        var ID = snowflake.NextID();// 生成雪花ID
                        if (historyIDs.Contains(ID)) duplicateIDs.Add(ID);
                        else historyIDs.Add(ID);
                    }
                });
            }).ToArray();
            Task.WaitAll(tasks);
            Assert.IsTrue(duplicateIDs.Count <= 0);
        }
    }
}
#endif