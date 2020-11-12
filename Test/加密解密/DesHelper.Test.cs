#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension.Test
* 项目描述 ：
* 类 名 称 ：DesHelper
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Test
* 机器名称 ：PC-0432 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/11/09 12:51:18
* 更新时间 ：2020/11/09 12:51:18
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class DesHelper
    {
        [TestMethod]
        public void DesTest()
        {
            var des = new AesHelper();
            var date = DateTime.Now.Ticks.ToString();
            var data  = des.Encrypt("123456", date,System.Security.Cryptography.CipherMode.ECB);
            Assert.IsNotNull(data);
        }
    }
}