using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.Extension.Test
{
    public static class GlobalData
    {
        /// <summary>
        /// 使用预设序列化后的Json字符串
        /// </summary>
        public const string CustomOptionJsonString = "{\"Int\":10086,\"NullableHasValue\":10010,\"String\":\"KaneLeung\",\"EmptyString\":\"\",\"Short\":711,\"NullableShortHasValue\":520,\"Long\":99709394,\"NullableLongHasValue\":997093940,\"Float\":10086.68,\"NullableFloatHasValue\":10010.01,\"Decimal\":10086.68001,\"NullableDecimalHasValue\":10010.01001,\"Time\":\"2020-02-02 00:00:00\",\"NullableTimeHasValue\":\"2020-10-10 00:00:00\",\"List\":[{\"PocoValue\":\"Hello World\"},{\"PocoValue\":\"Hello C#\"}]}";

        /// <summary>
        /// 忽略预设序列化后的Json字符串
        /// </summary>
        public const string IgnoreOptionJsonString = "{\"Int\":10086,\"Nullable\":null,\"NullableHasValue\":10010,\"String\":\"KaneLeung\",\"EmptyString\":\"\",\"NullableString\":null,\"Short\":711,\"NullableShort\":null,\"NullableShortHasValue\":520,\"Long\":99709394,\"NullableLong\":null,\"NullableLongHasValue\":997093940,\"Float\":10086.68,\"NullableFloat\":null,\"NullableFloatHasValue\":10010.01,\"Decimal\":10086.68001,\"NullableDecimal\":null,\"NullableDecimalHasValue\":10010.01001,\"Time\":\"2020-02-02T00:00:00\",\"NullableTime\":null,\"NullableTimeHasValue\":\"2020-10-10T00:00:00\",\"List\":[{\"PocoValue\":\"Hello World\"},{\"PocoValue\":\"Hello C#\"}],\"NullList\":null}";
        /// <summary>
        /// Rsa私钥Xml数据
        /// </summary>
        public const string RsaPrivateXmlKey = "<RSAKeyValue><D>VtS6k30GaG9Jvm4lGRYbKFKuKrdZjdLKOheewu0wsYeQw9lVoPu6Mk9klvMYvmnrAXgY6HGUsb8MDt1jE48+CyREmmRKMlwCMsLj4FEFw94eDboqQWaY8NTi1D06tDoq8kPH7Vv9rtxGAQ4gMij7GRoK//fKtGkE230FyEssQy5Lw7sy5uhAQbS1fLX+9BqgvqqyOfS17wFUKAmnmWUN/HsgDTk/UGZ1C+xy0Ki6FNK329+8+gb65kM4MGfY/PLAPVpcs2fX8h0UrBzO8lgHLIcjpMg+N+lecKgHYKeaJXeqlk17EUEpv7Si+JvrpuBwIikMqqaXPNfNj9wh84fugQ==</D><DP>BIUFRl5l5ZmRRXnQQNLvmpVM66OiFVeshqq8HmFY9DzT2WI/CwFqJD96YO52bgU+hDDYn8fBhAEM7rrTF3e8X1Nz/ARgqFM3uQTagmZh09+blCkp/srpSTdlx0tOAkJ4PuPSogYV4NGr8auXmGc5aumXFUvjaq6181yZ/B1Zw2k=</DP><DQ>m6XIC5MlUTrA1GYFDgOGJM9rC6CCYioB1Gn9LM2yJvBbzkMFBoA4nmN+mJi2d3b9RMKVFrfO576970JUNJfW3tpijqeSpijQV8A1HcZn5d3iKX29z/q7tOzj87S6wgPJuOUv8cSk5mxEriZSKADAx24Lg2DhLGFdAlQRhTEZ3Zc=</DQ><Exponent>AQAB</Exponent><InverseQ>PJqUz3p31NC4xJSFSZtOXe3AuwLwMUQeLLnEAtQEqZ6Oixg1N+T9kr/oelkfk7IVo6jbtPpI0qW9Df2FUEkA3Unt8dWhLJkbY6mZBwODKKWatwoAZqpx6mbigtOtJ5fIF2YYxA/0G6S2NQIO7g5u8XnwxrUdTYEY2Z/d+FQJDsQ=</InverseQ><Modulus>x4gXVwd8NQveOqrLSPypvjQMU+w4DsKIH1NOcEX5hpKntIwc9b5StObbXMZVnDWCuSbxI+1NzJ4/dex8PQ8FxCG/gNV1Uv7R9Y+/+ZTFhPQrMR6XMMQh0Skt99Bz6chphw/RVY9bzD47ANO0dY/z9Gm9c2wrMfDcEGOuwnfRmLJD8wyZ9Q1WASAO6aklyq6/LMc+7Uj0WIJoJPmndHRYgEYoE1xnYhJx2POlppbCKJwhzyVsPKizTxHpxA1cmW/04fD8D9ClC+q/UxVhJqPb4RRY1WMSgFZITbpJ2pd2C4cQL7zyKgF4Ap52fAbUztQXPT1zIkeAjypZllzJ5Wrf4w==</Modulus><P>7SayMYjD8fPheCBUKndgcIFvfO/AQE8W0iQ8E/of46uSl7FhnU0Yu0JU8GPkOVK15Gd4CEbF8i2xqKK0bZ7QdgYB8OgoUSPg5Nuf7IGVeJupDD7AP1CRJAJA6Q+jrUrqWPfomke3oM3wsqIuUidQ/SHRcy8VOnDcJWqm/MoRyGk=</P><Q>12PzqT9zDDYhldcWi+4vK51hhAyNOm4s3jsRvWMX/5/W/0DKx7KLgkozlHg8PXUzdu/Uzl2vucI+4IOnaJSxL5Ss9eteZ+xyDzxYtFjgM2fCVJiKPT9bPOg/Wx0ceZK1tG+Nhu4rucGVQQoKKgRAwlU3u7Kus8/l/KpLb3h3vGs=</Q></RSAKeyValue>";
    }
}