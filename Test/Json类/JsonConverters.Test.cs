#if NETCOREAPP3_1_OR_GREATER
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Kane.Extension.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class JsonConverters
    {
        public class ConvertersClass
        {
            public int Int { get; set; }
            public DateTime Time { get; set; }
            public bool Bool { get; set; }
            public long Long { get; set; }
        }

        [TestMethod]
        public void TestIntConverter()
        {
            var jsonString = "{\"Int\":\"10086\",\"Time\":\"2020-06-18T00:00:00\",\"Bool\":true,\"Long\":1152921504606846976}";
            Assert.ThrowsException<JsonException>(() =>
            {
                var normalObj = JsonSerializer.Deserialize<ConvertersClass>(jsonString);
            });//没有使用【IntConverter】时，字符串转Int报错
            var option = new JsonSerializerOptions();
            option.Converters.Add(new IntConverter());
            var obj = JsonSerializer.Deserialize<ConvertersClass>(jsonString, option);
            Assert.AreEqual(obj.Int, 10086);//使用后，可将字符串转Int
        }

        [TestMethod]
        public void TestLongConverter()
        {
            var jsonString = "{\"Int\":10086,\"Time\":\"2020-06-18T00:00:00\",\"Bool\":true,\"Long\":\"1152921504606846976\"}";
            Assert.ThrowsException<JsonException>(() =>
            {
                var normalObj = JsonSerializer.Deserialize<ConvertersClass>(jsonString);
            });//没有使用【LongConverter】时，字符串转Int报错
            var option = new JsonSerializerOptions();
            option.Converters.Add(new LongConverter());
            var obj = JsonSerializer.Deserialize<ConvertersClass>(jsonString, option);
            Assert.AreEqual(obj.Long, 1152921504606846976);//使用后，可将字符串转Long

            var jsonString2 = JsonSerializer.Serialize(obj, option);
            Assert.AreEqual(jsonString, jsonString2);//生成的Long的值是带双引号的
        }

        [TestMethod]
        public void TestDateTimeConverter()
        {
            var jsonString = "{\"Int\":10086,\"Time\":\"2020-06-18 00:00:00\",\"Bool\":true,\"Long\":1152921504606846976}";
            Assert.ThrowsException<JsonException>(() =>
            {
                var normalObj = JsonSerializer.Deserialize<ConvertersClass>(jsonString);
            });//没有使用【DateTimeConverter】时，非标准格式的时间字符串转DateTime报错
            var option = new JsonSerializerOptions();
            option.Converters.Add(new DateTimeConverter());
            var obj = JsonSerializer.Deserialize<ConvertersClass>(jsonString, option);
            Assert.AreEqual(obj.Time, new DateTime(2020, 06, 18));//使用后，可将自定义的时间格式字符串转DateTime

            var jsonString2 = "{\"Int\":10086,\"Time\":\"2020年06月18日 00:00:00\",\"Bool\":true,\"Long\":1152921504606846976}";
            var option2 = new JsonSerializerOptions();
            option2.Converters.Add(new DateTimeConverter("yyyy年MM月dd日 HH:mm:ss"));
            var obj2 = JsonSerializer.Deserialize<ConvertersClass>(jsonString2, option2);
            Assert.AreEqual(obj2.Time, new DateTime(2020, 06, 18));//使用后，可将自定义的时间格式字符串转DateTime
        }

        [TestMethod]
        public void TestBoolConverter()
        {
            var jsonString = "{\"Int\":10086,\"Time\":\"2020-06-18T00:00:00\",\"Bool\":\"true\",\"Long\":1152921504606846976}";
            Assert.ThrowsException<JsonException>(() =>
            {
                var normalObj = JsonSerializer.Deserialize<ConvertersClass>(jsonString);
            });//没有使用【BoolConverter】时，字符串转Bool报错
            var option = new JsonSerializerOptions();
            option.Converters.Add(new BoolConverter());
            var obj = JsonSerializer.Deserialize<ConvertersClass>(jsonString, option);
            Assert.IsTrue(obj.Bool);//使用后，可将字符串的"true"转Bool
        }
    }
}
#endif