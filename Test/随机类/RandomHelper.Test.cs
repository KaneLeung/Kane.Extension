using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class RandomHelper
    {
        [TestMethod]
        public void RandomIntTest()
        {
            var value0 = Kane.Extension.RandomHelper.RandomInt(0, 0);
            var value01 = Kane.Extension.RandomHelper.RandomInt(0, 1);

            Assert.AreEqual(0, value0);
            Assert.IsTrue(value01 >= 0 && value01 <= 1);
        }

        [TestMethod]
        public void RandomItemTest()
        {
            List<Poco> list = new List<Poco>();
            Assert.IsNull(list.RandomItem());
            list.Add(new Poco { PocoValue = "Hello" });
            list.Add(new Poco { PocoValue = "World" });
            var temp = list.RandomItem().PocoValue;
            Assert.IsTrue(temp == "Hello" || temp == "World");
        }
    }
}