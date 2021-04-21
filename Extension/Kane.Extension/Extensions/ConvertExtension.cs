// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.0
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 转换类扩展方法
    /// </summary>
    public static class ConvertExtension
    {
        #region 泛型转换为Short,失败时返回默认值0 + ToShort(this string value, short returnValue = 0)
        /// <summary>
        /// 泛型转换为Short,失败时返回默认值0
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static short ToShort(this string value, short returnValue = 0)
        {
            if (string.IsNullOrEmpty(value)) return returnValue;
            if (value.Contains(".")) value = value.Split('.')[0];
            short.TryParse(value, out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Short?(注意是可空类型) + ToNShort(this string value)
        /// <summary>
        /// 泛型转换为Short?(注意是可空类型)
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static short? ToNShort(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.Contains(".")) value = value.Split('.')[0];
            if (short.TryParse(value, out short returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Int,失败时返回默认值0 + ToInt(this string value, int returnValue = 0)
        /// <summary>
        /// 泛型转换为Int,失败时返回默认值0
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static int ToInt(this string value, int returnValue = 0)
        {
            if (string.IsNullOrEmpty(value)) return returnValue;
            if (value.Contains(".")) value = value.Split('.')[0];
            int.TryParse(value, out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Int?(注意是可空类型) + ToNInt(this string value)
        /// <summary>
        /// 泛型转换为Int?(注意是可空类型)
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static int? ToNInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.Contains(".")) value = value.Split('.')[0];
            if (int.TryParse(value, out int returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Long,失败时返回默认值0 + ToLong(this string value, long returnValue = 0)
        /// <summary>
        /// 泛型转换为Long,失败时返回默认值0
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static long ToLong(this string value, long returnValue = 0)
        {
            if (string.IsNullOrEmpty(value)) return returnValue;
            if (value.Contains(".")) value = value.Split('.')[0];
            long.TryParse(value, out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Long?(注意是可空类型) + ToNLong(this string value)
        /// <summary>
        /// 泛型转换为Long?(注意是可空类型)
        /// <para>可转【100.001】【-100.001】【  -100.001  】</para>
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static long? ToNLong(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (value.Contains(".")) value = value.Split('.')[0];
            if (long.TryParse(value, out long returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Float,失败时返回默认值0 + ToFloat(this string value, float returnValue = 0)
        /// <summary>
        /// 泛型转换为Float,失败时返回默认值0
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static float ToFloat(this string value, float returnValue = 0)
        {
            if (string.IsNullOrEmpty(value)) return returnValue;
            float.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Float?(注意是可空类型) + ToNFloat(this string value)
        /// <summary>
        /// 泛型转换为Float?(注意是可空类型)
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static float? ToNFloat(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (float.TryParse(value.ToString(), out float returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Double,失败时返回默认值0 + ToDouble(this string value, double returnValue = 0)
        /// <summary>
        /// 泛型转换为Double,失败时返回默认值0
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static double ToDouble(this string value, double returnValue = 0)
        {
            if (string.IsNullOrEmpty(value)) return returnValue;
            double.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Double?(注意是可空类型) + ToNDouble(this string value)
        /// <summary>
        /// 泛型转换为Double?(注意是可空类型)
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static double? ToNDouble(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (double.TryParse(value.ToString(), out double returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region 泛型转换为Decimal,失败时返回默认值0 + ToDec(this string value, decimal returnValue = 0)
        /// <summary>
        /// 泛型转换为Decimal,失败时返回默认值0
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <param name="returnValue">可设置失败后的返回值，默认为0</param>
        /// <returns></returns>
        public static decimal ToDec(this string value, decimal returnValue = 0)
        {
            if (string.IsNullOrEmpty(value)) return returnValue;
            decimal.TryParse(value.ToString(), out returnValue);
            return returnValue;
        }
        #endregion

        #region 泛型转换为Decimal?(注意是可空类型) + ToNDec(this string value)
        /// <summary>
        /// 泛型转换为Decimal?(注意是可空类型)
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static decimal? ToNDec(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (decimal.TryParse(value.ToString(), out decimal returnValue)) return returnValue;
            else return null;
        }
        #endregion

        #region [short/ushort/int/uint/long/ulong/float/double/decimal]转换大写金额，默认为设置【整】的结束标签 + ToAmoutInWords<T>(this T value, bool hasTag = true) where T : struct, IFormattable
        /// <summary>
        /// [short/ushort/int/uint/long/ulong/float/double/decimal]转换大写金额，默认为设置【整】的结束标签
        /// <para>【资料来源】https://baike.baidu.com/item/大写金额</para>
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="hasTag">以【元】结尾时，是否包含【整】字标签</param>
        /// <returns></returns>
        public static string ToAmoutInWords<T>(this T value, bool hasTag = true) where T : struct, IFormattable
        {
            var valueString = value.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A", null);
            var temp = Regex.Replace(valueString, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}", RegexOptions.Compiled);
            var result = Regex.Replace(temp, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString(), RegexOptions.Compiled);
            if (hasTag && result.EndsWith("元")) result += "整";
            return result;
        }
        #endregion

        #region 字符串转换大写金额，失败返回【string.Empty】，默认为设置【整】的结束标签 + ToAmoutInWords(this string value)
        /// <summary>
        /// 字符串转换大写金额，失败返回【string.Empty】，默认为设置【整】的结束标签
        /// <para>【资料来源】https://baike.baidu.com/item/大写金额</para>
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <param name="hasTag">以【元】结尾时，是否包含【整】字标签</param>
        /// <returns></returns>
        public static string ToAmoutInWords(this string value, bool hasTag = true)
        {
            if (decimal.TryParse(value, out decimal result)) return result.ToAmoutInWords(hasTag);
            else return string.Empty;
        }
        #endregion

        #region 泛型转Decimal,默认保留两位小数，默认【采用4舍6入5取偶】 + ToRoundDec<T>(...)
        /// <summary>
        /// 泛型转Decimal,默认保留两位小数，默认【采用4舍6入5取偶】
        /// <para>采用Banker's rounding（银行家算法），即：四舍六入五取偶。事实上这也是IEEE的规范。</para>
        /// <para>备注：<see cref="MidpointRounding.AwayFromZero"/>可以用来实现传统意义上的"四舍五入"。</para>
        /// </summary>
        /// <param name="value">要转的值</param>
        /// <param name="digits">保留的小数位数</param>
        /// <param name="returnValue">失败时返回的值</param>
        /// <param name="mode">可选择模式</param>
        /// <returns></returns>
        public static decimal ToRoundDec(this string value, int digits = 2, decimal returnValue = 0, MidpointRounding mode = MidpointRounding.ToEven)
            => Math.Round(value.ToDec(returnValue), digits, mode);
        #endregion

        #region 全局日期时间转换格式
        /// <summary>
        /// 全局日期转换格式
        /// </summary>
        public static IEnumerable<string> GlobalFormats = new string[]
        {
            "yyyy-MM-dd HH:mm:ss",
            "yyyyMMdd HH:mm:ss",
            "yyyy/MM/dd HH:mm:ss",
            "yyyy-M-d HH:mm:ss",
            "yyyyMd HH:mm:ss",
            "yyyy/M/d HH:mm:ss",
            "yy-MM-dd HH:mm:ss",
            "yyMMdd HH:mm:ss",
            "yy/MM/dd HH:mm:ss",
            "yy-M-d HH:mm:ss",
            "yyMd HH:mm:ss",
            "yy/M/d HH:mm:ss",
            "yyyyMMddHHmmss",
            "yyyy-MM-dd H:m:s",
            "yyyyMMdd H:m:s",
            "yyyy/MM/dd H:m:s",
            "yyyy-M-d H:m:s",
            "yyyyMd H:m:s",
            "yyyy/M/d H:m:s",
            "yy-MM-dd H:m:s",
            "yyMMdd H:m:s",
            "yy/MM/dd H:m:s",
            "yy-M-d H:m:s",
            "yyMd H:m:s",
            "yy/M/d H:m:s",
            "yyyy-MM-dd",
            "yyyyMMdd",
            "yyyy/MM/dd",
            "yyyy-M-d",
            "yyyyMd",
            "yyyy/M/d",
            "yy-MM-dd",
            "yyMMdd",
            "yy/MM/dd",
            "yy-M-d",
            "yyMd",
            "yy/M/d"
        };
        #endregion

        #region 将字符串转换为可空的日期对象 + ToNDT(this string value, string format = "")
        /// <summary>
        /// 将字符串转换为可空的日期对象
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="format">日期格式化字符串</param>
        /// <returns>日期对象</returns>
        public static DateTime? ToNDateTime(this string value, string format = "")
        {
            DateTime? result = null;
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(format)
                    && DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime temp)) result = temp;
                if (result is null && DateTime.TryParse(value, out temp)) result = temp;
                if (result is null)
                {
                    foreach (string item in GlobalFormats)
                    {
                        if (DateTime.TryParseExact(value, item, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp))
                        {
                            result = temp;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region 常规字符串转换DateTime，可设置失败后的返回值 + ToDT(this string value, DateTime returnValue)
        /// <summary>
        /// 常规字符串转换DateTime，可设置失败后的返回值
        /// <para>对象中的格式设置信息分析字符串value，该对象由当前线程区域性隐式提供。</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="returnValue">失败后的返回值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value, DateTime returnValue) => value.ToNDateTime() ?? returnValue;
        #endregion

        #region 常规字符串转换DateTime，失败后的返回默认值 + ToDT(this string value)
        /// <summary>
        /// 常规字符串转换DateTime，失败后的返回默认值
        /// <para>对象中的格式设置信息分析字符串value，该对象由当前线程区域性隐式提供。</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value) => ToDateTime(value, default(DateTime));
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值 + ToDT(this string value, string format, DateTime returnValue)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值
        /// <para>https://docs.microsoft.com/zh-cn/dotnet/api/system.globalization.datetimeformatinfo?view=netcore-3.1</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="format">自定义转换格式</param>
        /// <param name="returnValue">失败后的返回值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value, string format, DateTime returnValue) => value.ToNDateTime(format) ?? returnValue;
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，失败后的返回默认值 + ToDT(this string value, string format)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，失败后的返回默认值
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="format">自定义转换格式</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value, string format) => ToDateTime(value, format, default);
        #endregion

        #region 将字符串转换为可空的日期对象 + ToNDT(this string value, string format = "")
        /// <summary>
        /// 将字符串转换为可空的日期对象
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="format">日期格式化字符串</param>
        /// <returns>日期对象</returns>
        public static DateTimeOffset? ToNDateTimeOffset(this string value, string format = "")
        {
            DateTimeOffset? result = null;
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(format)
                    && DateTimeOffset.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset temp)) result = temp;
                if (result is null && DateTimeOffset.TryParse(value, out temp)) result = temp;
                if (result is null)
                {
                    foreach (string item in GlobalFormats)
                    {
                        if (DateTimeOffset.TryParseExact(value, item, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp))
                        {
                            result = temp;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region 常规字符串转换DateTime，可设置失败后的返回值 + ToDT(this string value, DateTime returnValue)
        /// <summary>
        /// 常规字符串转换DateTime，可设置失败后的返回值
        /// <para>对象中的格式设置信息分析字符串value，该对象由当前线程区域性隐式提供。</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="returnValue">失败后的返回值</param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this string value, DateTimeOffset returnValue) => value.ToNDateTimeOffset() ?? returnValue;
        #endregion

        #region 常规字符串转换DateTime，失败后的返回默认值 + ToDT(this string value)
        /// <summary>
        /// 常规字符串转换DateTime，失败后的返回默认值
        /// <para>对象中的格式设置信息分析字符串value，该对象由当前线程区域性隐式提供。</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this string value) => ToDateTimeOffset(value, default(DateTimeOffset));
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值 + ToDT(this string value, string format, DateTime returnValue)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，可设置失败后的返回值
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="format">自定义转换格式</param>
        /// <param name="returnValue">失败后的返回值</param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this string value, string format, DateTimeOffset returnValue) => value.ToNDateTimeOffset(format) ?? returnValue;
        #endregion

        #region 字符串转换DateTime，可根据自定义格式转换，失败后的返回默认值 + ToDT(this string value, string format)
        /// <summary>
        /// 字符串转换DateTime，可根据自定义格式转换，失败后的返回默认值
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="format">自定义转换格式</param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this string value, string format) => ToDateTimeOffset(value, format, default);
        #endregion
    }
}