using System;
using System.Collections.Generic;

namespace Kane.Extension.Test
{
    public class ComplexModels
    {
        public int Int { get; set; } = 10086;
        public int? Nullable { get; set; }
        public int? NullableHasValue { get; set; } = 10010;
        public string String { get; set; } = "KaneLeung";
        public string EmptyString { get; set; } = "";
        public string NullableString { get; set; }
        public short Short { get; set; } = 711;
        public short? NullableShort { get; set; }
        public short? NullableShortHasValue { get; set; } = 520;
        public long Long { get; set; } = 99709394;
        public long? NullableLong { get; set; }
        public long? NullableLongHasValue { get; set; } = 997093940;
        public float Float { get; set; } = 10086.68001F;
        public float? NullableFloat { get; set; }
        public float? NullableFloatHasValue { get; set; } = 10010.01001F;
        public decimal Decimal { get; set; } = 10086.68001M;
        public decimal? NullableDecimal { get; set; }
        public decimal? NullableDecimalHasValue { get; set; } = 10010.01001M;
        public DateTime Time { get; set; } = new DateTime(2020, 02, 02);
        public DateTime? NullableTime { get; set; }
        public DateTime? NullableTimeHasValue { get; set; } = new DateTime(2020, 10, 10);
        /// <summary>
        /// 设置了默认值后，Newtonsoft.Json反序列化时，会自动增加默认值，微软原生的不会
        /// </summary>
        public IEnumerable<Poco> List { get; set; } //= new List<Poco>() { new Poco { PocoValue = "Hello World" }, new Poco { PocoValue = "Hello C#" } };
        public IEnumerable<Poco> NullList { get; set; }
    }
}