// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kane.Extension
{
    /// <summary>
    /// 正则表达式类扩展方法
    /// </summary>
    public static class RegexExtension
    {
        #region 利用正则表达式替换字符串里的文字，区分大小写 + RegexReplace(this string value, string regex, string newValue)
        /// <summary>
        /// 利用正则表达式替换字符串里的文字，区分大小写
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="newValue">替换后的字符串</param>
        /// <returns></returns>
        public static string RegexReplace(this string value, string regex, string newValue) => Regex.Replace(value, regex, newValue, RegexOptions.Compiled);
        #endregion

        #region 利用正则表达式替换多个关键词的文字，区分大小写 + RegexReplaces(this string value, string newValue, params string[] keys)
        /// <summary>
        /// 利用正则表达式替换多个关键词的文字，区分大小写
        /// <para>经测试，短的内容效率比<see cref="StringExtension.Replace(string, string, string[])"/>略低</para>
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="newValue">替换后的字符串</param>
        /// <param name="keywords">要替换的关键词</param>
        /// <returns></returns>
        public static string RegexReplaces(this string value, string newValue, params string[] keywords)
        {
            if (value.IsNullOrEmpty() || keywords.Length < 1) return value;
            return Regex.Replace(value, $"({string.Join("|", keywords)})", newValue, RegexOptions.Compiled);
        }
        #endregion

        #region 利用正则表达式查找某字符串出现的次数 + RegexCount(this string value, string pattern)
        /// <summary>
        /// 利用正则表达式查找某字符串出现的次数,如果单个字符，不建议使用这个
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="pattern">要统计的字符串</param>
        /// <returns></returns>
        public static int RegexCount(this string value, string pattern) => Regex.Matches(value, pattern, RegexOptions.Compiled).Count;
        #endregion

        #region 检测字符串中是否找到了匹配项 + IsMatch(this string value, string pattern)
        /// <summary>
        /// 检测字符串中是否找到了匹配项
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>如果正则表达式找到匹配项，则为【True】；否则，为【False】</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return false;
            return Regex.IsMatch(value, pattern, RegexOptions.Compiled);
        }
        #endregion

        #region 检测字符串中是否找到了匹配项，要设置匹配选项 + IsMatch(this string value, string pattern, RegexOptions options)
        /// <summary>
        /// 检测字符串中是否找到了匹配项，要设置匹配选项
        /// </summary>
        /// <param name="value">要检测的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">枚举值提供匹配选项</param>
        /// <returns>如果正则表达式找到匹配项，则为【True】；否则，为【False】</returns>
        public static bool IsMatch(this string value, string pattern, RegexOptions options)
        {
            if (value.IsNullOrEmpty()) return false;
            return Regex.IsMatch(value, pattern, options);
        }
        #endregion

        #region 利用正则表达式，返回第一个匹配项 + Match(this string value, string pattern)
        /// <summary>
        /// 利用正则表达式，返回第一个匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static string Match(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return string.Empty;
            return Regex.Match(value, pattern, RegexOptions.Compiled).Value;
        }
        #endregion

        #region 利用正则表达式，返回匹配的所有项 + Matches(this string value, string pattern)
        /// <summary>
        /// 利用正则表达式，返回匹配的所有项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static IEnumerable<string> Matches(this string value, string pattern)
        {
            if (value.IsNullOrEmpty()) return [];
            MatchCollection matches = Regex.Matches(value, pattern, RegexOptions.Compiled);
            return from Match match in matches select match.Value;
        }
        #endregion
    }
}