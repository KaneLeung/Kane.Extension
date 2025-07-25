﻿// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

#if NET6_0_OR_GREATER
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kane.Extension.Json
{
    #region 自定义IntConverter，可将Json字符串数值转成int + IntConverter : JsonConverter<int>
    /// <summary>
    /// 自定义IntConverter，可将Json字符串数值转成int
    /// </summary>
    public class IntConverter : JsonConverter<int>
    {
        #region 转换格式的私有变量
        /// <summary>
        /// 失败返回的默认值私有变量
        /// </summary>
        private readonly int returnValue;
        #endregion

        #region 【构造函数】失败后返回默认值【0】 + IntConverter()
        /// <summary>
        /// 【构造函数】失败后返回默认值【0】
        /// </summary>
        public IntConverter() => returnValue = 0;
        #endregion

        #region 【构造函数】失败后返回用户自定义默认值 + IntConverter(int value)
        /// <summary>
        /// 【构造函数】失败后返回用户自定义默认值
        /// </summary>
        /// <param name="value">用户自定义默认值</param>
        public IntConverter(int value) => returnValue = value;
        #endregion

        #region 重写转换器Read方法 + Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Read方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && int.TryParse(reader.GetString(), out int result)) return result;
            else if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out result)) return result;
            else return returnValue;
        }
        #endregion

        #region 重写转换器Write方法 + Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
        /// <summary>
        /// 重写转换器Write方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
        #endregion
    }
    #endregion

    #region 自定义LongConverter，可将Json字符串数值转成long + LongConverter : JsonConverter<long>
    /// <summary>
    /// 自定义LongConverter，可将Json字符串数值转成long
    /// <para>部分long类型值(最大值2^63-1)会超过Javascript的最大安全Number(2^53-1)，浏览器/前端 使用JSON.parse()将不再保证准确性。</para>
    /// </summary>
    public class LongConverter : JsonConverter<long>
    {
        /// <summary>
        /// 重写转换器Read方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => long.TryParse(reader.GetString(), out long result) ? result : default;

        /// <summary>
        /// 重写转换器Write方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
    #endregion

    #region 自定义DateTimeConverter + DateTimeConverter : JsonConverter<DateTime>
    /// <summary>
    /// 自定义DateTimeConverter
    /// https://github.com/dotnet/runtime/blob/master/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/Value/DateTimeConverter.cs
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        #region 转换格式的私有变量
        /// <summary>
        /// 转换格式的私有变量
        /// </summary>
        private readonly string _format;
        #endregion

        #region 【构造函数】转换器默认格式【yyyy-MM-dd HH:mm:ss】 + DateTimeConverter()
        /// <summary>
        /// 【构造函数】转换器默认格式【yyyy-MM-dd HH:mm:ss】
        /// </summary>
        public DateTimeConverter() => _format = "yyyy-MM-dd HH:mm:ss";
        #endregion

        #region 【构造函数】转换器自定义格式 + DateTimeConverter(string format)
        /// <summary>
        /// 【构造函数】转换器自定义格式
        /// </summary>
        /// <param name="format">用户自定义格式</param>
        public DateTimeConverter(string format) => _format = format;
        #endregion

        #region 重写转换器Read方法 + Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Read方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString().ToDateTime(_format);
        #endregion

        #region 重写转换器Write方法 + Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Write方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(_format));
        #endregion
    }
    #endregion

    #region 自定义DateTimeOffsetConverter + DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    /// <summary>
    /// 自定义DateTimeOffsetConverter
    /// https://github.com/dotnet/runtime/blob/master/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/Value/DateTimeOffsetConverter.cs
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        #region 转换格式的私有变量
        /// <summary>
        /// 转换格式的私有变量
        /// </summary>
        private readonly string _format;
        #endregion

        #region 【构造函数】转换器默认格式【yyyy-MM-dd HH:mm:ss】 + DateTimeConverter()
        /// <summary>
        /// 【构造函数】转换器默认格式【yyyy-MM-dd HH:mm:ss】
        /// </summary>
        public DateTimeOffsetConverter() => _format = "yyyy-MM-dd HH:mm:ss";
        #endregion

        #region 【构造函数】转换器自定义格式 + DateTimeConverter(string format)
        /// <summary>
        /// 【构造函数】转换器自定义格式
        /// </summary>
        /// <param name="format">用户自定义格式</param>
        public DateTimeOffsetConverter(string format) => _format = format;
        #endregion

        #region 重写转换器Read方法 + Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Read方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString().ToDateTime(_format);
        #endregion

        #region 重写转换器Write方法 + Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Write方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(_format));
        #endregion
    }
    #endregion

    #region 自定义BoolConverter，可将Json字符串"true"/"false"转成bool + BoolConverter : JsonConverter<bool>
    /// <summary>
    /// 自定义BoolConverter，可将Json字符串"true"/"false"转成bool
    /// <para>System.Text.Json中接收的json中不能将 "true"/"false"识别为boolean的True/False，这也需要自定义Converter实现bool转换</para>
    /// <para>https://github.com/dotnet/runtime/blob/master/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/Value/BooleanConverter.cs</para>
    /// </summary>
    public class BoolConverter : JsonConverter<bool>
    {
        #region 重写转换器Read方法 + Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        /// <summary>
        /// 重写转换器Read方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False) return reader.GetBoolean();
            return reader.GetString().ToBool();
        }
        #endregion

        #region 重写转换器Write方法 + Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
        /// <summary>
        /// 重写转换器Write方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
        #endregion
    }
    #endregion
}
#endif