// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.6
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kane.Extension.JsonNet
{
    /// <summary>
    /// Json扩展类，使用【Newtonsoft.Json】
    /// </summary>
    public static class JsonNet
    {
        #region 自用的【Newtonsoft.Json】全局序列化和反序列化的配置选项
        /// <summary>
        /// 全局序列化和反序列化的配置选项
        /// </summary>
        public static JsonSerializerSettings GlobalSetting;
        #endregion

        #region 静态构造函数，保证只运行一次
        /// <summary>
        /// 静态构造函数，保证只运行一次
        /// </summary>
        static JsonNet()
        {
            #region 自用的【Newtonsoft.Json】全局序列化和反序列化的配置选项
            GlobalSetting = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",//日期类型默认格式化处理
                NullValueHandling = NullValueHandling.Ignore,//空值处理
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,//设置不处理循环引用
                ContractResolver = new DefaultContractResolver()//使用默认方式，保持属性名称不变
            };
            #endregion
        }
        #endregion

        #region 把对象序列化，转成Json字符串，使用默认配置选项 + ToJson<T>(this T value, JsonSerializerSettings settings = null)
        /// <summary>
        /// 把对象序列化，转成Json字符串，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="settings">序列化参数</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, JsonSerializerSettings settings = null) => JsonConvert.SerializeObject(value, settings ?? GlobalSetting);
        #endregion

        #region 把对象序列化，转成Json字符串，可忽略默认配置选项 + ToJson<T>(this T value, bool ignore)
        /// <summary>
        /// 把对象序列化，转成Json字符串，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static string ToJson<T>(this T value, bool ignore) => ignore ? JsonConvert.SerializeObject(value) : JsonConvert.SerializeObject(value, GlobalSetting);
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
            var options = new JsonSerializerSettings
            {
                DateFormatHandling = GlobalSetting.DateFormatHandling,
                DateFormatString = GlobalSetting.DateFormatString,
                NullValueHandling = GlobalSetting.NullValueHandling,
                ReferenceLoopHandling = GlobalSetting.ReferenceLoopHandling,
                ContractResolver = new CamelCasePropertyNamesContractResolver()//使用【小驼峰】
            };
            return JsonConvert.SerializeObject(value, options);
        }
        #endregion

        #region Json字符串反序列化，转成对象 + ToObject<T>(this string value, JsonSerializerSettings settings = null)
        /// <summary>
        /// Json字符串反序列化，转成对象，使用默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="settings">序列化参数</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, JsonSerializerSettings settings = null) => JsonConvert.DeserializeObject<T>(value, settings ?? GlobalSetting);
        #endregion

        #region Json字符串反序列化，转成对象，可忽略默认配置选项 + ToObject<T>(this string value, bool ignore)
        /// <summary>
        /// Json字符串反序列化，转成对象，可忽略默认配置选项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Json字符串</param>
        /// <param name="ignore">是否忽略默认配置选项</param>
        /// <returns></returns>
        public static T ToObject<T>(this string value, bool ignore) => ignore ? JsonConvert.DeserializeObject<T>(value) : JsonConvert.DeserializeObject<T>(value, GlobalSetting);
        #endregion
    }
}