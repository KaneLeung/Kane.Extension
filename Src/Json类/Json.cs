﻿#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：Json
* 类 描 述 ：Json扩展类，使用【System.Text.Json】
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Json
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/01/15 23:29:15
* 更新时间 ：2021/02/26 10:16:17
* 版 本 号 ：v1.0.5.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
#if NETCOREAPP3_1_OR_GREATER
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

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
                PropertyNamingPolicy = null,//保持属性名称不变
                AllowTrailingCommas = true,//忽略多余的逗号
                PropertyNameCaseInsensitive = true,//反序列化是否不区分大小写
#if NET5_0_OR_GREATER
                NumberHandling = JsonNumberHandling.AllowReadingFromString,//Net5.0新增，可将"88"反序列化为Int值
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
#else
                IgnoreNullValues = true,//为True,忽略Null值 //等于5.0的 DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
#endif
            };
            GlobalOption.Converters.Add(new DateTimeConverter());//使用【2020-02-21 17:06:15】时间格式
            GlobalOption.Converters.Add(new DateTimeOffsetConverter());//使用【2020-02-21 17:06:15】时间格式
            GlobalOption.Converters.Add(new BoolConverter());//"true"/"false"识别为boolean的True/False
#if NETCOREAPP3_1
            GlobalOption.Converters.Add(new IntConverter());//"88"转为Int
#endif
            GlobalOption.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;//中文不会被编码
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