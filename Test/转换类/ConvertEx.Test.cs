#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension.Test
* 项目描述 ：
* 类 名 称 ：ConvertEx
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Test
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/11/04 11:42:43
* 更新时间 ：2020/11/04 11:42:43
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
    public class ConvertEx
    {
        [TestMethod]
        public void ToAmoutInWordsTest()
        {
            decimal value = 123456789.12m;
            Assert.AreEqual(value.ToAmoutInWords(), "壹亿贰仟叁佰肆拾伍万陆仟柒佰捌拾玖元壹角贰分");
            value = 98765432100m;
            Assert.AreEqual(value.ToAmoutInWords(), "玖佰捌拾柒亿陆仟伍佰肆拾叁万贰仟壹佰元整");
        }
    }
}