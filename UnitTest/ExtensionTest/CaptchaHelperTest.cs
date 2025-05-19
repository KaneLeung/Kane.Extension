using Kane.Extension;

namespace ExtensionTest
{
    [TestClass]
    public class CaptchaHelperTest
    {
        [TestMethod]
        public void CaptchaTest()
        {
#if NETFRAMEWORK
            var captcha = new CaptchaHelper();
            captcha.Mode = CaptchaMode.All;
            var temp = captcha.GetCaptcha();
            temp.Image.Save("D:\\dfaf.png");
#endif
            Assert.IsTrue(true);
        }
    }
}
