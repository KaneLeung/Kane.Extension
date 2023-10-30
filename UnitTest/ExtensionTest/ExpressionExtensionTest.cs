using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kane.Extension;

namespace ExtensionTest
{
    [TestClass]
    public class ExpressionExtensionTest
    {
        [TestMethod]
        public void ExpDeepCloneTest()
        {
            var obj = new TestClass();
            var clone = obj.ExpDeepClone();
            clone.ID = 345;
            Assert.AreNotEqual(obj.ID, clone.ID);
            Assert.AreSame(obj.Name, clone.Name);
            Assert.AreEqual(obj.Temp, clone.Temp);
        }

        public class TestClass
        {
            public int ID { get; set; } = 123;
            public string Name { get; set; } = "Kane";
            public decimal Temp { get; set; } = 36.9m;
        }
    }
}
