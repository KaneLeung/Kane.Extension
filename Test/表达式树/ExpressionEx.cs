using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Kane.Extension;

namespace Kane.Extension.Test
{
    [TestClass]
    public class ExpressionEx
    {
        [TestMethod]
        public void GetPropValueExpTest()
        {
            var temp = new Poco() { PocoValue = "Hello World" };
            var result = temp.GetPropValueExp<Poco, string>("PocoValue");
            Assert.AreEqual(result, "Hello World");
        }

        [TestMethod]
        public void SetPropValueExpTest()
        {
            var temp = new Poco() { PocoValue = "Hello World" };
            var result = temp.SetPropValueExp("PocoValue", "Hello CSharp.");
            Assert.IsTrue(result);
            Assert.AreEqual(temp.PocoValue, "Hello CSharp.");
        }
    }
}