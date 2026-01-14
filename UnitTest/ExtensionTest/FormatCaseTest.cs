using Kane.Extension;

namespace ExtensionTest
{
    [TestClass]
    public class FormatCaseTest
    {
        [TestMethod]
        public void TestPascalCase()
        {
            var string1 = "hello_world";
            var string2 = "hello-world";
            var string3 = "hello world";
            var string4 = "HELLO WORLD";
            var string5 = "helloWorld";
            Assert.AreEqual(string1.ToPascalCase(), "HelloWorld");
            Assert.AreEqual(string2.ToPascalCase(), "HelloWorld");
            Assert.AreEqual(string3.ToPascalCase(), "HelloWorld");
            Assert.AreEqual(string4.ToPascalCase(), "HelloWorld");
            Assert.AreEqual(string5.ToPascalCase(), "HelloWorld");
        }

        [TestMethod]
        public void TestCamelCase()
        {
            var string1 = "hello_world";
            var string2 = "hello-world";
            var string3 = "hello world";
            var string4 = "HELLO WORLD";
            var string5 = "HelloWorld";
            Assert.AreEqual(string1.ToCamelCase(), "helloWorld");
            Assert.AreEqual(string2.ToCamelCase(), "helloWorld");
            Assert.AreEqual(string3.ToCamelCase(), "helloWorld");
            Assert.AreEqual(string4.ToCamelCase(), "helloWorld");
            Assert.AreEqual(string5.ToCamelCase(), "helloWorld");
        }

        [TestMethod]
        public void TestSnakeCase()
        {
            var string1 = "hello_world";
            var string2 = "hello-world";
            var string3 = "hello world";
            var string4 = "HELLO WORLD";
            var string5 = "HelloWorld";
            Assert.AreEqual(string1.ToSnakeCase(), "hello_world");
            Assert.AreEqual(string2.ToSnakeCase(), "hello_world");
            Assert.AreEqual(string3.ToSnakeCase(), "hello_world");
            Assert.AreEqual(string4.ToSnakeCase(), "hello_world");
            Assert.AreEqual(string5.ToSnakeCase(), "hello_world");
            //大写
            Assert.AreEqual(string1.ToSnakeCase(true), "HELLO_WORLD");
            Assert.AreEqual(string2.ToSnakeCase(true), "HELLO_WORLD");
            Assert.AreEqual(string3.ToSnakeCase(true), "HELLO_WORLD");
            Assert.AreEqual(string4.ToSnakeCase(true), "HELLO_WORLD");
            Assert.AreEqual(string5.ToSnakeCase(true), "HELLO_WORLD");
        }

        [TestMethod]
        public void TestKebabCase()
        {
            var string1 = "hello_world";
            var string2 = "hello-world";
            var string3 = "hello world";
            var string4 = "HELLO WORLD";
            var string5 = "HelloWorld";
            Assert.AreEqual(string1.ToKebabCase(), "hello-world");
            Assert.AreEqual(string2.ToKebabCase(), "hello-world");
            Assert.AreEqual(string3.ToKebabCase(), "hello-world");
            Assert.AreEqual(string4.ToKebabCase(), "hello-world");
            Assert.AreEqual(string5.ToKebabCase(), "hello-world");
            //大写                   
            Assert.AreEqual(string1.ToKebabCase(true), "HELLO-WORLD");
            Assert.AreEqual(string2.ToKebabCase(true), "HELLO-WORLD");
            Assert.AreEqual(string3.ToKebabCase(true), "HELLO-WORLD");
            Assert.AreEqual(string4.ToKebabCase(true), "HELLO-WORLD");
            Assert.AreEqual(string5.ToKebabCase(true), "HELLO-WORLD");
        }

        [TestMethod]
        public void TestFormatString()
        {
            var string1 = "hello_world";
            var string2 = string.Empty;
            string string3 = null;

            Assert.AreEqual(string1.FormatString(FormatCase.UpperCase), "HELLO_WORLD");
            Assert.AreEqual(string2.FormatString(FormatCase.UpperCase), string.Empty);
            Assert.AreEqual(string3.FormatString(FormatCase.UpperCase), null);

            Assert.AreEqual(string1.FormatString(FormatCase.PascalCase), "HelloWorld");
            Assert.AreEqual(string2.FormatString(FormatCase.PascalCase), string.Empty);
            Assert.AreEqual(string3.FormatString(FormatCase.PascalCase), null);
        }
    }
}
