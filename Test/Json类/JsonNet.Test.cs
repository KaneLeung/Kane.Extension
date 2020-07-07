using System;
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
            Assert.AreEqual(Temp.ToJson(), GlobalData.CustomOptionJsonString);
        }

        [TestMethod]
        public void ToJsonIgnore()
        {
            Assert.AreEqual(Temp.ToJson(true), GlobalData.IgnoreOptionJsonString);
        }

        [TestMethod]
        public void ToObject()
        {
            var obj = GlobalData.CustomOptionJsonString.ToObject<ComplexModels>();
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
            var obj = GlobalData.IgnoreOptionJsonString.ToObject<ComplexModels>(true);
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