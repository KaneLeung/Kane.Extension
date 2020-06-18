using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class JsonHelper
    {
        [TestMethod]
        public void TestJsonHelper()
        {
            var jsonHelper = new Kane.Extension.JsonHelper(GlobalData.CustomOptionJsonString);
            var listIndex0 = jsonHelper.GetValue<Poco>("List:0");//即读取List元素里Index为0的元素
            Assert.AreEqual("Hello World", listIndex0.PocoValue);
            var longValue = jsonHelper.GetValue<long>("Long");
            Assert.AreEqual(99709394, longValue);
        }
    }
}