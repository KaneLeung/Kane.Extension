#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension.Test
* 项目描述 ：
* 类 名 称 ：Mapper
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Test
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/6/27 11:46:16
* 更新时间 ：2020/6/27 11:46:16
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if !NET40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class Mapper
    {
        private static readonly ComplexModels Temp = new ComplexModels()
        {
            List = new List<Poco>() { new Poco { PocoValue = "Hello World" }, new Poco { PocoValue = "Hello C#" } }
        };

        [TestMethod]
        public void MapperFuncTest()
        {
            var targetObj = Mapper<ComplexModels, TargetModels>.Map(Temp);
            //Int
            Assert.AreEqual(Temp.Int, targetObj.Int);
            Assert.IsNull(targetObj.Nullable);
            Assert.AreEqual(Temp.NullableHasValue, targetObj.NullableHasValue);
            //String
            Assert.AreEqual(Temp.String, targetObj.String);
            Assert.AreEqual(Temp.EmptyString, targetObj.EmptyString);
            Assert.IsNull(targetObj.NullableString);
            //Short
            Assert.AreEqual(Temp.Short, targetObj.Short);
            Assert.IsNull(targetObj.NullableShort);
            Assert.AreEqual(Temp.NullableShortHasValue, targetObj.NullableShortHasValue);
            //Long
            Assert.AreEqual(Temp.Long, targetObj.Long);
            Assert.IsNull(targetObj.NullableLong);
            Assert.AreEqual(Temp.NullableLongHasValue, targetObj.NullableLongHasValue);
            //Float
            Assert.AreEqual(Temp.Float, targetObj.Float);
            Assert.IsNull(targetObj.NullableFloat);
            Assert.AreEqual(Temp.NullableFloatHasValue, targetObj.NullableFloatHasValue);
            //Decimal
            Assert.AreEqual(Temp.Decimal, targetObj.Decimal);
            Assert.IsNull(targetObj.NullableDecimal);
            Assert.AreEqual(Temp.NullableDecimalHasValue, targetObj.NullableDecimalHasValue);
            //DateTime
            Assert.AreEqual(Temp.Time, targetObj.Time);
            Assert.IsNull(targetObj.NullableTime);
            Assert.AreEqual(Temp.NullableTimeHasValue, targetObj.NullableTimeHasValue);
            //List
            Assert.AreEqual(Temp.List.Count(), targetObj.List.Count());
            Assert.IsNull(targetObj.NullList);
        }

        [TestMethod]
        public void MapperActionTest()
        {
            var targetObj = new TargetModels();
            Mapper<ComplexModels, TargetModels>.Map(Temp, targetObj);
            //Int
            Assert.AreEqual(Temp.Int, targetObj.Int);
            Assert.IsNull(targetObj.Nullable);
            Assert.AreEqual(Temp.NullableHasValue, targetObj.NullableHasValue);
            //String
            Assert.AreEqual(Temp.String, targetObj.String);
            Assert.AreEqual(Temp.EmptyString, targetObj.EmptyString);
            Assert.IsNull(targetObj.NullableString);
            //Short
            Assert.AreEqual(Temp.Short, targetObj.Short);
            Assert.IsNull(targetObj.NullableShort);
            Assert.AreEqual(Temp.NullableShortHasValue, targetObj.NullableShortHasValue);
            //Long
            Assert.AreEqual(Temp.Long, targetObj.Long);
            Assert.IsNull(targetObj.NullableLong);
            Assert.AreEqual(Temp.NullableLongHasValue, targetObj.NullableLongHasValue);
            //Float
            Assert.AreEqual(Temp.Float, targetObj.Float);
            Assert.IsNull(targetObj.NullableFloat);
            Assert.AreEqual(Temp.NullableFloatHasValue, targetObj.NullableFloatHasValue);
            //Decimal
            Assert.AreEqual(Temp.Decimal, targetObj.Decimal);
            Assert.IsNull(targetObj.NullableDecimal);
            Assert.AreEqual(Temp.NullableDecimalHasValue, targetObj.NullableDecimalHasValue);
            //DateTime
            Assert.AreEqual(Temp.Time, targetObj.Time);
            Assert.IsNull(targetObj.NullableTime);
            Assert.AreEqual(Temp.NullableTimeHasValue, targetObj.NullableTimeHasValue);
            //List
            Assert.AreEqual(Temp.List.Count(), targetObj.List.Count());
            Assert.IsNull(targetObj.NullList);
        }

        [TestMethod]
        public void MapperFuncListTest()
        {
            var sourceList = new List<ComplexModels>();
            sourceList.Add(Temp);
            sourceList.Add(new ComplexModels
            {
                Int = 234
            }) ;
            var targetObj = Mapper<ComplexModels, TargetModels>.MapList(sourceList);
            Assert.AreEqual(sourceList.Count, targetObj.Count);
            Assert.AreEqual(sourceList?.LastOrDefault()?.Int, targetObj?.LastOrDefault()?.Int);
        }

        public class TargetModels
        {
            public int Int { get; set; }
            public int? Nullable { get; set; }
            public int? NullableHasValue { get; set; }
            public string String { get; set; }
            public string EmptyString { get; set; }
            public string NullableString { get; set; }
            public short Short { get; set; }
            public short? NullableShort { get; set; }
            public short? NullableShortHasValue { get; set; }
            public long Long { get; set; }
            public long? NullableLong { get; set; }
            public long? NullableLongHasValue { get; set; }
            public float Float { get; set; }
            public float? NullableFloat { get; set; }
            public float? NullableFloatHasValue { get; set; }
            public decimal Decimal { get; set; }
            public decimal? NullableDecimal { get; set; }
            public decimal? NullableDecimalHasValue { get; set; }
            public DateTime Time { get; set; }
            public DateTime? NullableTime { get; set; }
            public DateTime? NullableTimeHasValue { get; set; }
            public IEnumerable<Poco> List { get; set; }
            public IEnumerable<Poco> NullList { get; set; }
        }
    }
}
#endif