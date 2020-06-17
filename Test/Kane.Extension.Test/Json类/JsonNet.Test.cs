using System.Collections.Generic;
using System.Linq;
using Kane.Extension.JsonNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class JsonNet
    {
        private static readonly ComplexModels Temp = new ComplexModels()
        {
            List = new List<Poco>() { new Poco { PocoValue = "Hello World" }, new Poco { PocoValue = "Hello C#" } }
        };

        [TestMethod]
        public void ToJson()
        {
            var json = "{\"Int\":10086,\"NullableHasValue\":10010,\"String\":\"KaneLeung\",\"EmptyString\":\"\",\"Short\":711,\"NullableShortHasValue\":520,\"Long\":99709394,\"NullableLongHasValue\":997093940,\"Float\":10086.68,\"NullableFloatHasValue\":10010.01,\"Decimal\":10086.68001,\"NullableDecimalHasValue\":10010.01001,\"Time\":\"2020-02-02 00:00:00\",\"NullableTimeHasValue\":\"2020-10-10 00:00:00\",\"List\":[{\"PocoValue\":\"Hello World\"},{\"PocoValue\":\"Hello C#\"}]}";
            Assert.AreEqual(Temp.ToJson(), json);
        }

        [TestMethod]
        public void ToJsonIgnore()
        {
            var json = "{\"Int\":10086,\"Nullable\":null,\"NullableHasValue\":10010,\"String\":\"KaneLeung\",\"EmptyString\":\"\",\"NullableString\":null,\"Short\":711,\"NullableShort\":null,\"NullableShortHasValue\":520,\"Long\":99709394,\"NullableLong\":null,\"NullableLongHasValue\":997093940,\"Float\":10086.68,\"NullableFloat\":null,\"NullableFloatHasValue\":10010.01,\"Decimal\":10086.68001,\"NullableDecimal\":null,\"NullableDecimalHasValue\":10010.01001,\"Time\":\"2020-02-02T00:00:00\",\"NullableTime\":null,\"NullableTimeHasValue\":\"2020-10-10T00:00:00\",\"List\":[{\"PocoValue\":\"Hello World\"},{\"PocoValue\":\"Hello C#\"}],\"NullList\":null}";
            Assert.AreEqual(Temp.ToJson(true), json);
        }

        [TestMethod]
        public void ToObject()
        {
            var json = "{\"Int\":10086,\"NullableHasValue\":10010,\"String\":\"KaneLeung\",\"EmptyString\":\"\",\"Short\":711,\"NullableShortHasValue\":520,\"Long\":99709394,\"NullableLongHasValue\":997093940,\"Float\":10086.68,\"NullableFloatHasValue\":10010.01,\"Decimal\":10086.68001,\"NullableDecimalHasValue\":10010.01001,\"Time\":\"2020-02-02 00:00:00\",\"NullableTimeHasValue\":\"2020-10-10 00:00:00\",\"List\":[{\"PocoValue\":\"Hello World\"},{\"PocoValue\":\"Hello C#\"}]}";
            var obj = json.ToObject<ComplexModels>();
            Assert.AreEqual(Temp.Int, obj.Int);
            Assert.AreEqual(Temp.Nullable, obj.Nullable);
            Assert.AreEqual(Temp.NullableHasValue, obj.NullableHasValue);
            Assert.AreEqual(Temp.String, obj.String);
            Assert.AreEqual(Temp.EmptyString, obj.EmptyString);
            Assert.AreEqual(Temp.NullableString, obj.NullableString);
            Assert.AreEqual(Temp.Short, obj.Short);
            Assert.AreEqual(Temp.NullableShort, obj.NullableShort);
            Assert.AreEqual(Temp.NullableShortHasValue, obj.NullableShortHasValue);
            Assert.AreEqual(Temp.Long, obj.Long);
            Assert.AreEqual(Temp.NullableLong, obj.NullableLong);
            Assert.AreEqual(Temp.NullableLongHasValue, obj.NullableLongHasValue);
            Assert.AreEqual(Temp.Float, obj.Float);
            Assert.AreEqual(Temp.NullableFloat, obj.NullableFloat);
            Assert.AreEqual(Temp.NullableFloatHasValue, obj.NullableFloatHasValue);
            Assert.AreEqual(Temp.Decimal, obj.Decimal);
            Assert.AreEqual(Temp.NullableDecimal, obj.NullableDecimal);
            Assert.AreEqual(Temp.NullableDecimalHasValue, obj.NullableDecimalHasValue);
            Assert.AreEqual(Temp.Time, obj.Time);
            Assert.AreEqual(Temp.NullableTime, obj.NullableTime);
            Assert.AreEqual(Temp.NullableTimeHasValue, obj.NullableTimeHasValue);
            Assert.AreEqual(Temp.List.Count(), obj.List.Count());
            Assert.AreEqual(Temp.List.FirstOrDefault()?.PocoValue, obj.List.FirstOrDefault()?.PocoValue);
            Assert.AreEqual(Temp.List.LastOrDefault()?.PocoValue, obj.List.LastOrDefault()?.PocoValue);
            Assert.AreEqual(Temp.NullList, obj.NullList);
        }

        [TestMethod]
        public void ToObjectIgnore()
        {
            var json = "{\"Int\":10086,\"Nullable\":null,\"NullableHasValue\":10010,\"String\":\"KaneLeung\",\"EmptyString\":\"\",\"NullableString\":null,\"Short\":711,\"NullableShort\":null,\"NullableShortHasValue\":520,\"Long\":99709394,\"NullableLong\":null,\"NullableLongHasValue\":997093940,\"Float\":10086.68,\"NullableFloat\":null,\"NullableFloatHasValue\":10010.01,\"Decimal\":10086.68001,\"NullableDecimal\":null,\"NullableDecimalHasValue\":10010.01001,\"Time\":\"2020-02-02T00:00:00\",\"NullableTime\":null,\"NullableTimeHasValue\":\"2020-10-10T00:00:00\",\"List\":[{\"PocoValue\":\"Hello World\"},{\"PocoValue\":\"Hello C#\"}],\"NullList\":null}";
            var obj = json.ToObject<ComplexModels>(true);
            Assert.AreEqual(Temp.Int, obj.Int);
            Assert.AreEqual(Temp.Nullable, obj.Nullable);
            Assert.AreEqual(Temp.NullableHasValue, obj.NullableHasValue);
            Assert.AreEqual(Temp.String, obj.String);
            Assert.AreEqual(Temp.EmptyString, obj.EmptyString);
            Assert.AreEqual(Temp.NullableString, obj.NullableString);
            Assert.AreEqual(Temp.Short, obj.Short);
            Assert.AreEqual(Temp.NullableShort, obj.NullableShort);
            Assert.AreEqual(Temp.NullableShortHasValue, obj.NullableShortHasValue);
            Assert.AreEqual(Temp.Long, obj.Long);
            Assert.AreEqual(Temp.NullableLong, obj.NullableLong);
            Assert.AreEqual(Temp.NullableLongHasValue, obj.NullableLongHasValue);
            Assert.AreEqual(Temp.Float, obj.Float);
            Assert.AreEqual(Temp.NullableFloat, obj.NullableFloat);
            Assert.AreEqual(Temp.NullableFloatHasValue, obj.NullableFloatHasValue);
            Assert.AreEqual(Temp.Decimal, obj.Decimal);
            Assert.AreEqual(Temp.NullableDecimal, obj.NullableDecimal);
            Assert.AreEqual(Temp.NullableDecimalHasValue, obj.NullableDecimalHasValue);
            Assert.AreEqual(Temp.Time, obj.Time);
            Assert.AreEqual(Temp.NullableTime, obj.NullableTime);
            Assert.AreEqual(Temp.NullableTimeHasValue, obj.NullableTimeHasValue);
            Assert.AreEqual(Temp.List.Count(), obj.List.Count());
            Assert.AreEqual(Temp.List.FirstOrDefault()?.PocoValue, obj.List.FirstOrDefault()?.PocoValue);
            Assert.AreEqual(Temp.List.LastOrDefault()?.PocoValue, obj.List.LastOrDefault()?.PocoValue);
            Assert.AreEqual(Temp.NullList, obj.NullList);
        }
    }
}