using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class XmlHelper
    {
        [TestMethod]
        public void TestToObject()
        {
            using TextReader reader = new StringReader(GlobalData.RsaPrivateXmlKey);
            var rsa = reader.ToObject<RsaModels>();
            Assert.AreEqual("x4gXVwd8NQveOqrLSPypvjQMU+w4DsKIH1NOcEX5hpKntIwc9b5StObbXMZVnDWCuSbxI+1NzJ4/dex8PQ8FxCG/gNV1Uv7R9Y+/+ZTFhPQrMR6XMMQh0Skt99Bz6chphw/RVY9bzD47ANO0dY/z9Gm9c2wrMfDcEGOuwnfRmLJD8wyZ9Q1WASAO6aklyq6/LMc+7Uj0WIJoJPmndHRYgEYoE1xnYhJx2POlppbCKJwhzyVsPKizTxHpxA1cmW/04fD8D9ClC+q/UxVhJqPb4RRY1WMSgFZITbpJ2pd2C4cQL7zyKgF4Ap52fAbUztQXPT1zIkeAjypZllzJ5Wrf4w==", rsa.Modulus);
            Assert.AreEqual("AQAB", rsa.Exponent);
            Assert.AreEqual("7SayMYjD8fPheCBUKndgcIFvfO/AQE8W0iQ8E/of46uSl7FhnU0Yu0JU8GPkOVK15Gd4CEbF8i2xqKK0bZ7QdgYB8OgoUSPg5Nuf7IGVeJupDD7AP1CRJAJA6Q+jrUrqWPfomke3oM3wsqIuUidQ/SHRcy8VOnDcJWqm/MoRyGk=", rsa.P);
            Assert.AreEqual("12PzqT9zDDYhldcWi+4vK51hhAyNOm4s3jsRvWMX/5/W/0DKx7KLgkozlHg8PXUzdu/Uzl2vucI+4IOnaJSxL5Ss9eteZ+xyDzxYtFjgM2fCVJiKPT9bPOg/Wx0ceZK1tG+Nhu4rucGVQQoKKgRAwlU3u7Kus8/l/KpLb3h3vGs=", rsa.Q);
            Assert.AreEqual("BIUFRl5l5ZmRRXnQQNLvmpVM66OiFVeshqq8HmFY9DzT2WI/CwFqJD96YO52bgU+hDDYn8fBhAEM7rrTF3e8X1Nz/ARgqFM3uQTagmZh09+blCkp/srpSTdlx0tOAkJ4PuPSogYV4NGr8auXmGc5aumXFUvjaq6181yZ/B1Zw2k=", rsa.DP);
            Assert.AreEqual("m6XIC5MlUTrA1GYFDgOGJM9rC6CCYioB1Gn9LM2yJvBbzkMFBoA4nmN+mJi2d3b9RMKVFrfO576970JUNJfW3tpijqeSpijQV8A1HcZn5d3iKX29z/q7tOzj87S6wgPJuOUv8cSk5mxEriZSKADAx24Lg2DhLGFdAlQRhTEZ3Zc=", rsa.DQ);
            Assert.AreEqual("PJqUz3p31NC4xJSFSZtOXe3AuwLwMUQeLLnEAtQEqZ6Oixg1N+T9kr/oelkfk7IVo6jbtPpI0qW9Df2FUEkA3Unt8dWhLJkbY6mZBwODKKWatwoAZqpx6mbigtOtJ5fIF2YYxA/0G6S2NQIO7g5u8XnwxrUdTYEY2Z/d+FQJDsQ=", rsa.InverseQ);
            Assert.AreEqual("VtS6k30GaG9Jvm4lGRYbKFKuKrdZjdLKOheewu0wsYeQw9lVoPu6Mk9klvMYvmnrAXgY6HGUsb8MDt1jE48+CyREmmRKMlwCMsLj4FEFw94eDboqQWaY8NTi1D06tDoq8kPH7Vv9rtxGAQ4gMij7GRoK//fKtGkE230FyEssQy5Lw7sy5uhAQbS1fLX+9BqgvqqyOfS17wFUKAmnmWUN/HsgDTk/UGZ1C+xy0Ki6FNK329+8+gb65kM4MGfY/PLAPVpcs2fX8h0UrBzO8lgHLIcjpMg+N+lecKgHYKeaJXeqlk17EUEpv7Si+JvrpuBwIikMqqaXPNfNj9wh84fugQ==", rsa.D);
        }

        [TestMethod]
        public void TestObjectToXml()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,//【True】去除xml声明<?xml version="1.0" encoding="utf-8"?>
                Indent = false,//为True时，换行，缩进
                Encoding = Encoding.UTF8//默认为UTF8编码
            };
            var rsa = new RsaModels
            {
                Modulus = "x4gXVwd8NQveOqrLSPypvjQMU+w4DsKIH1NOcEX5hpKntIwc9b5StObbXMZVnDWCuSbxI+1NzJ4/dex8PQ8FxCG/gNV1Uv7R9Y+/+ZTFhPQrMR6XMMQh0Skt99Bz6chphw/RVY9bzD47ANO0dY/z9Gm9c2wrMfDcEGOuwnfRmLJD8wyZ9Q1WASAO6aklyq6/LMc+7Uj0WIJoJPmndHRYgEYoE1xnYhJx2POlppbCKJwhzyVsPKizTxHpxA1cmW/04fD8D9ClC+q/UxVhJqPb4RRY1WMSgFZITbpJ2pd2C4cQL7zyKgF4Ap52fAbUztQXPT1zIkeAjypZllzJ5Wrf4w==",
                Exponent = "AQAB",
                P = "7SayMYjD8fPheCBUKndgcIFvfO/AQE8W0iQ8E/of46uSl7FhnU0Yu0JU8GPkOVK15Gd4CEbF8i2xqKK0bZ7QdgYB8OgoUSPg5Nuf7IGVeJupDD7AP1CRJAJA6Q+jrUrqWPfomke3oM3wsqIuUidQ/SHRcy8VOnDcJWqm/MoRyGk=",
                Q = "12PzqT9zDDYhldcWi+4vK51hhAyNOm4s3jsRvWMX/5/W/0DKx7KLgkozlHg8PXUzdu/Uzl2vucI+4IOnaJSxL5Ss9eteZ+xyDzxYtFjgM2fCVJiKPT9bPOg/Wx0ceZK1tG+Nhu4rucGVQQoKKgRAwlU3u7Kus8/l/KpLb3h3vGs=",
                DP = "BIUFRl5l5ZmRRXnQQNLvmpVM66OiFVeshqq8HmFY9DzT2WI/CwFqJD96YO52bgU+hDDYn8fBhAEM7rrTF3e8X1Nz/ARgqFM3uQTagmZh09+blCkp/srpSTdlx0tOAkJ4PuPSogYV4NGr8auXmGc5aumXFUvjaq6181yZ/B1Zw2k=",
                DQ = "m6XIC5MlUTrA1GYFDgOGJM9rC6CCYioB1Gn9LM2yJvBbzkMFBoA4nmN+mJi2d3b9RMKVFrfO576970JUNJfW3tpijqeSpijQV8A1HcZn5d3iKX29z/q7tOzj87S6wgPJuOUv8cSk5mxEriZSKADAx24Lg2DhLGFdAlQRhTEZ3Zc=",
                InverseQ = "PJqUz3p31NC4xJSFSZtOXe3AuwLwMUQeLLnEAtQEqZ6Oixg1N+T9kr/oelkfk7IVo6jbtPpI0qW9Df2FUEkA3Unt8dWhLJkbY6mZBwODKKWatwoAZqpx6mbigtOtJ5fIF2YYxA/0G6S2NQIO7g5u8XnwxrUdTYEY2Z/d+FQJDsQ=",
                D = "VtS6k30GaG9Jvm4lGRYbKFKuKrdZjdLKOheewu0wsYeQw9lVoPu6Mk9klvMYvmnrAXgY6HGUsb8MDt1jE48+CyREmmRKMlwCMsLj4FEFw94eDboqQWaY8NTi1D06tDoq8kPH7Vv9rtxGAQ4gMij7GRoK//fKtGkE230FyEssQy5Lw7sy5uhAQbS1fLX+9BqgvqqyOfS17wFUKAmnmWUN/HsgDTk/UGZ1C+xy0Ki6FNK329+8+gb65kM4MGfY/PLAPVpcs2fX8h0UrBzO8lgHLIcjpMg+N+lecKgHYKeaJXeqlk17EUEpv7Si+JvrpuBwIikMqqaXPNfNj9wh84fugQ=="
            };
            Assert.AreEqual(GlobalData.RsaPrivateXmlKey, rsa.ToXml(true, settings));
        }
    }
}