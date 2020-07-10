
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class EnumEx
    {
        [TestMethod]
        public void ToEnumTest()
        {
            var GZip = "gzip".ToEnum<CompressMode>();
            var returnDefault = "gzip".ToEnum<CompressMode>(ignoreCase: false);
            var returnDeflate = "gzip".ToEnum(CompressMode.Deflate, false);
            var returnGzip = "XXXX".ToEnum(CompressMode.GZip);//【XXXX】不存在
            var returnDefault2 = "XXXX".ToEnum<CompressMode>();//【XXXX】不存在

            var Deflate = "deflate".ToNEnum<CompressMode>();
            var returnNull = "deflate".ToNEnum<CompressMode>(false);
            var returnNull2 = "XXXX".ToNEnum<CompressMode>();//【XXXX】不存在

            Assert.AreEqual(CompressMode.GZip, GZip);
            Assert.AreEqual(default(CompressMode), returnDefault);
            Assert.AreEqual(CompressMode.Deflate, returnDeflate);
            Assert.AreEqual(CompressMode.GZip, returnGzip);
            Assert.AreEqual(default(CompressMode), returnDefault2);

            Assert.AreEqual(CompressMode.Deflate, Deflate);
            Assert.IsNull(returnNull);
            Assert.IsNull(returnNull2);
        }
    }
}