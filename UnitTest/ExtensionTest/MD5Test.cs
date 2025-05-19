using Kane.Extension;

namespace ExtensionTest
{
    [TestClass]
    public class MD5Test
    {
        [TestMethod]
        public void Md5TestString()
        {
            var hash = new HashHelper();
            var md5 = hash.Md5("123456");
            Assert.AreEqual(md5, "e10adc3949ba59abbe56e057f20f883e");
        }
    }
}
