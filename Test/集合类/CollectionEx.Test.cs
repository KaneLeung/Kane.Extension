#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension.Test
* 项目描述 ：
* 类 名 称 ：CollectionEx
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Test
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/7/27 0:34:58
* 更新时间 ：2020/7/27 0:34:58
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class CollectionEx
    {
#if !NET40
        [TestMethod]
        public void ToCsvTest()
        {
            var temp = new List<ComplexModels>();
            temp.Add(new ComplexModels { String = "Hello World" });
            temp.Add(new ComplexModels { String = "ABC,DEF,GHI" });
            temp.Add(new ComplexModels { String = "ABCDEFGHI" });
            temp.Add(new ComplexModels { String = "\"Hello\" \"World\"" });
            temp.Add(new ComplexModels { String = "Hello\" \"World" });
            Assert.IsNotNull(temp.ToCsv());
        }
#endif
    }
}