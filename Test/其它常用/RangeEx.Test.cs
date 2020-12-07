using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class RangeEx
    {
        [TestMethod]
        public void InTest()
        {
            int intValue = 10;
            var result1 = intValue.In(10, 11);
            var result2 = intValue.In(10, 11, false);
            var result3 = intValue.In(9, 10, false);
            var result4 = intValue.In(9, 10, false,false);
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
            Assert.IsTrue(result3);
            Assert.IsFalse(result4);
        }
    }
}