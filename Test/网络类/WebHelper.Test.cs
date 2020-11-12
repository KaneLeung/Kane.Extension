using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class WebHelper
    {
        [TestMethod]
        public void ReplaceHtmlTest()
        {
#if NETCOREAPP
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            var contetn = File.ReadAllText("D:\\html.txt", Encoding.GetEncoding("GB18030"));
            var temp = contetn.ReplaceHtml();
            Assert.IsNotNull(temp);
        }
    }
}