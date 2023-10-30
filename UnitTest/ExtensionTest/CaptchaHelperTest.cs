using Kane.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
