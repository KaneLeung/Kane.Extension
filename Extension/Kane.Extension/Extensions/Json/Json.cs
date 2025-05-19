// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Kane.Extension.Json
{
    /// <summary>
    /// Json扩展类，使用【System.Text.Json】
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// 全局序列化和反序列化的配置选项
        /// </summary>
        public static JsonSerializerOptions GlobalOption;

        #region 静态构造函数，保证只运行一次
        /// <summary>
        /// 静态构造函数，保证只运行一次
        /// </summary>
        static Json()
        {
            #region 自用的【System.Text.Json】序列化和反序列化的配置选项
            GlobalOption = new JsonSerializerOptions
            {
                IncludeFields = true,
                ReadCommentHandling = JsonCommentHandling.Skip,//允许注释
                PropertyNamingPolicy = null,//保持属性名称不变
                AllowTrailingCommas = true,//忽略多余的逗号
                PropertyNameCaseInsensitive = true,//反序列化是否不区分大小写
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,//中文不会被编码
                NumberHandling = JsonNumberHandling.AllowReadingFromString,//Net5.0新增，可将"88"反序列化为Int值
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            GlobalOption.Converters.Add(new DateTimeConverter());//使用【2020-02-21 17:06:15】时间格式
            GlobalOption.Converters.Add(new DateTimeOffsetConverter());//使用【2020-02-21 17:06:15】时间格式
            GlobalOption.Converters.Add(new BoolConverter());//"true"/"false"识别为boolean的True/False
            GlobalOption.Converters.Add(new JsonStringEnumConverter());//能将字符串转为Enum
            #endregion
        }
        #endregion

        #region 把对象序列化，转成Json字符串，使用默认配置选项 + ToJson<T>(this T value, JsonSerializerOptions options = null)
        /// <summary>
        /// 把对象序列化，转成Json字符串，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="options">序列化参数</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, JsonSerializerOptions options = null) => JsonSerializer.Serialize(value, options ?? GlobalOption);
        #endregion

        #region 把对象序列化，转成Json字符串 + ToJson<T>(this T value, bool ignore)
        /// <summary>
        /// 把对象序列化，转成Json字符串，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, bool ignore) => ignore ? JsonSerializer.Serialize(value) : JsonSerializer.Serialize(value, GlobalOption);
        #endregion

        #region 把对象序列化，转成Json(CamelCase)字符串【小驼峰】 + ToCamelCaseJson<T>(this T value)
        /// <summary>
        /// 把对象序列化，转成Json(CamelCase)字符串【小驼峰】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <returns></returns>
        public static string ToCamelCaseJson<T>(this T value)
        {
            var options = new JsonSerializerOptions(GlobalOption)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(value, options);
        }
        #endregion

        #region Json字符串反序列化，转成对象,使用默认配置选项 + ToObject<T>(this string value, JsonSerializerOptions options = null)
        /// <summary>
        /// Json字符串反序列化，转成对象，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="options">序列化参数</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(value, options ?? GlobalOption);
        #endregion

        #region Json字符串反序列化，转成对象 + ToObject<T>(this string value, bool ignore)
        /// <summary>
        /// Json字符串反序列化，转成对象，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, bool ignore) => ignore ? JsonSerializer.Deserialize<T>(value) : JsonSerializer.Deserialize<T>(value, GlobalOption);
        #endregion
    }
}
#endif