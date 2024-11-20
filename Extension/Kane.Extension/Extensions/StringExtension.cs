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
using System.Linq;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 字符串类扩展方法
    /// </summary>
    public static class StringExtension
    {
        #region 测试字符串是否为NullOrEmpty + IsNullOrEmpty(this string value)
        /// <summary>
        /// 测试字符串是否为NullOrEmpty
        /// <para>判断是否为【Null】【""】【String.Empty】</para>
        /// </summary>
        /// <param name="value">要测试的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        #endregion

        #region 测试字符串是否为NullOrWhiteSpace + IsNullOrWhiteSpace(this string value)
        /// <summary>
        /// 测试字符串是否为NullOrWhiteSpace
        /// <para>null,String.Empty,new String(' ', 20),"  \t   ",new String('\u2000', 10)都会返回True</para>
        /// </summary>
        /// <param name="value">要测试的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
        #endregion

        #region 测试字符串是否不为NullOrEmpty + HasValue(this string value)
        /// <summary>
        /// 测试字符串是否不为NullOrEmpty
        /// </summary>
        /// <param name="value">要测试的字符串</param>
        /// <returns></returns>
        public static bool HasValue(this string value) => !string.IsNullOrEmpty(value);
        #endregion

        #region 十六进制字符串转字节数组 + HexToBytes(this string value)
        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        /// <param name="value">要转的十六进制字符串</param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string value)
        {
            value = value.Replace(" ", "");
            byte[] buffer = new byte[value.Length / 2];
            for (int i = 0; i < value.Length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            }
            return buffer;
        }
        #endregion

        #region 统计某字符在字符串中出现的次数 + CharCount(this string value, char keyword)
        /// <summary>
        /// 统计某字符在字符串中出现的次数
        /// </summary>
        /// <param name="value">要查找的字符串</param>
        /// <param name="keyword">要统计的字符</param>
        /// <returns></returns>
        public static int CharCount(this string value, char keyword)
        {
            int count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == keyword)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region 统计某字符串在字符串中出现的次数 + StringCount(this string value, string keyword)
        /// <summary>
        /// 统计某字符串在字符串中出现的次数
        /// </summary>
        /// <param name="value">要查找的字符串</param>
        /// <param name="keyword">要统计的字符串</param>
        /// <returns></returns>
        public static int StringCount(this string value, string keyword)
        {
            int count = 0;
            for (int i = 0; i <= value.Length - keyword.Length; i++)
            {
                if (value.Substring(i, keyword.Length) == keyword)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region 字符串转成字节数组，默认使用UTF8编码 + ToBytes(this string value)
        /// <summary>
        /// 字符串转成字节数组，默认使用UTF8编码
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value) => Encoding.UTF8.GetBytes(value);
        #endregion

        #region 字符串转成字节数组，编码方式可选 + ToBytes(this string value, Encoding encoding)
        /// <summary>
        /// 字符串转成字节数组，编码方式可选
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value, Encoding encoding) => encoding.GetBytes(value);
        #endregion

        #region 字符串转成Base64字符串 + ToBase64(this string value)
        /// <summary>
        /// 字符串转成Base64字符串
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static string ToBase64(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        #endregion

        #region 字符串转成Base64字符串,可自定义编码 + ToBase64(this string value)
        /// <summary>
        /// 字符串转成Base64字符串，可自定义编码
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <param name="encoding">自定义编码</param>
        /// <returns></returns>
        public static string ToBase64(this string value, Encoding encoding) => Convert.ToBase64String(encoding.GetBytes(value));
        #endregion

        #region Base64字符串转成字符串，默认使用UTF8编码 + FormBase64(this string value)
        /// <summary>
        /// Base64字符串转成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="value">要转换的Base64字符串</param>
        /// <returns></returns>
        public static string FormBase64(this string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));
        #endregion

        #region Base64字符串转成字符串，可自定义编码 + FormBase64(this string value, Encoding encoding)
        /// <summary>
        /// Base64字符串转成字符串，可自定义编码
        /// </summary>
        /// <param name="value">要转换的Base64字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string FormBase64(this string value, Encoding encoding) => encoding.GetString(Convert.FromBase64String(value));
        #endregion

        #region Base64字符串转成字节数组 + Base64ToBytes(this string value)
        /// <summary>
        /// Base64字符串转成字节数组
        /// <para>常见错误【Base-64 字符数组或字符串的长度无效，输入的不是有效的Base-64字符串，</para>
        /// <para>因为它包含非Base-64 字符、两个以上的填充字符，或者填充字符间包含非法字符。】</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(this string value) => Convert.FromBase64String(value);
        #endregion

        #region 全局字符串转换Bool格式 + BoolFormats
        /// <summary>
        /// 全局字符串转换Bool格式
        /// </summary>
        public static IEnumerable<string> BoolFormats = ["true", "1", "ok", "yes", "enable", "是", "真", "可以"];
        #endregion

        #region 将字符串转换为Bool类型，默认包含【true】【1】【ok】【yes】【enable】【是】【真】【可以】 + ToBool(this string value)
        /// <summary>
        /// 将字符串转换为Bool类型
        /// <para>默认包含【true】【1】【ok】【yes】【enable】【是】【真】【可以】</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(this string value) => BoolFormats.Any(k => k.Equals(value, StringComparison.OrdinalIgnoreCase));
        #endregion

        #region 判断两个字符串是否相同，忽略大小写 + EqualsIgnoreCase(this string str1, string str2)
        /// <summary>
        /// 判断两个字符串是否相同，忽略大小写
        /// </summary>
        /// <param name="str1">要比较的字符串1</param>
        /// <param name="str2">要比较的字符串2</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string str1, string str2) => str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
        #endregion

        #region 判断两个字符串是否相同，忽略大小写 + EqualsIgnoreCase(this string str0, string str1, bool strict = false)
        /// <summary>
        /// 判断两个字符串是否相同，忽略大小写
        /// <para> 默认为【非严紧模式】，即【Null】和【string.Empty】或【""】比较时为True</para>
        /// <para>【严紧模式】时，【Null】与【string.Empty】或【""】比较时为 False</para>
        /// </summary>
        /// <param name="str1">要比较的字符串1</param>
        /// <param name="str2">要比较的字符串2</param>
        /// <param name="strict">是否为严紧模式</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string str1, string str2, bool strict)
        {
            if (strict == false && string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2)) return true;
            if (str1 is null || str2 is null) return str1 is null && str2 is null;
            return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        #region 字符串转成全角(SBC Case)的字符串 + ToSBC(this string value)
        /// <summary>
        /// 字符串转成全角(SBC Case)的字符串
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static string ToSBC(this string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 32)
                {
                    array[i] = (char)12288;
                    continue;
                }
                if (array[i] < 127) array[i] = (char)(array[i] + 65248);
            }
            return new string(array);
        }
        #endregion

        #region 字符串转成半角(DBC Case)的字符串 + ToDBC(this string value)
        /// <summary>
        /// 字符串转成半角(DBC Case)的字符串
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static string ToDBC(this string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375) array[i] = (char)(array[i] - 65248);
            }
            return new string(array);
        }
        #endregion

        #region 检测字符串是否匹配任意字符串的开头【区分大小写】 + StartsWith(this string value, params string[] keywords)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的开头【区分大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keywords">多个字符串</param>
        /// <returns></returns>
        public static bool StartsWith(this string value, params string[] keywords) => keywords.Any(key => value.StartsWith(key));
        #endregion

        #region 检测字符串是否匹配任意字符串的开头【忽略大小写】 + StartsWithIgnoreCase(this string value, params string[] keywords)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的开头【忽略大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keywords">任意字符串</param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(this string value, params string[] keywords) => keywords.Any(key => value.StartsWith(key, StringComparison.OrdinalIgnoreCase));
        #endregion

        #region 检测字符串是否匹配任意字符串的结尾【区分大小写】 + EndsWith(this string value, params string[] keywords)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的结尾【区分大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keywords">任意字符串</param>
        /// <returns></returns>
        public static bool EndsWith(this string value, params string[] keywords) => keywords.Any(key => value.EndsWith(key));
        #endregion

        #region 检测字符串是否匹配任意字符串的结尾【忽略大小写】 + EndsWithIgnoreCase(this string value, params string[] keywords)
        /// <summary>
        /// 检测字符串是否匹配任意字符串的结尾【忽略大小写】
        /// </summary>
        /// <param name="value">要检测字符串</param>
        /// <param name="keywords">任意字符串</param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(this string value, params string[] keywords) => keywords.Any(key => value.EndsWith(key, StringComparison.OrdinalIgnoreCase));
        #endregion

        #region 用新的字符替换原字符串中指定位置和长度的字符 + Replace(this string value, int start, int length, char replaceChar = '*')
        /// <summary>
        /// 用新的字符替换原字符串中指定位置和长度的字符
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="start">开始的位置，从【0】开始</param>
        /// <param name="length">替换的长度</param>
        /// <param name="replaceChar">替换的字符</param>
        /// <returns></returns>
        public static string Replace(this string value, int start, int length, char replaceChar = '*')
        {
            if (string.IsNullOrEmpty(value) || value.Length < start) return value;
            if (value.Length < start + length) length = value.Length - start;
            if (length < 1) return value;
            return value.Remove(start, length).Insert(start, new string(replaceChar, length));
        }
        #endregion

        #region 查找并替换字符串，可查找多个目标【区分大小写】 + Replace(this string value, string newValue, params string[] oldValues)
        /// <summary>
        /// 查找并替换字符串，可查找多个目标【区分大小写】
        /// <para>等同XXXX.Replace("A","C").Replace("B","C")</para>
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="newValue">要替换的字符串</param>
        /// <param name="oldValues">查找的关键词</param>
        /// <returns></returns>
        public static string Replace(this string value, string newValue, params string[] oldValues)
        {
            if (string.IsNullOrEmpty(value) || oldValues.Length == 0) return value;
            foreach (var item in oldValues)
                value = value.Replace(item, newValue);
            return value;
        }
        #endregion

#if NET6_0_OR_GREATER
        #region 查找并替换字符串，可查找多个目标【忽略大小写】 + ReplaceIgnoreCase(this string value, string newValue, params string[] oldValues)
        /// <summary>
        /// 查找并替换字符串，可查找多个目标【忽略大小写】
        /// <para>等同XXXX.Replace("A","C").Replace("B","C")</para>
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="newValue">要替换的字符串</param>
        /// <param name="oldValues">查找的目标</param>
        /// <returns></returns>
        public static string ReplaceIgnoreCase(this string value, string newValue, params string[] oldValues)
        {
            if (string.IsNullOrEmpty(value) || oldValues.Length == 0) return value;
            foreach (var item in oldValues)
                value = value.Replace(item, newValue, StringComparison.OrdinalIgnoreCase);
            return value;
        }
        #endregion
#endif

        #region 判断多个字符串是否出现在原字符串中【区分大小写】 + ContainsAny(this string value, params string[] keywords)
        /// <summary>
        /// 判断多个字符串是否出现在原字符串中【区分大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keywords">要查找的多个字符串</param>
        /// <returns></returns>
        public static bool ContainsAny(this string value, params string[] keywords) => keywords.Any(k => value.IndexOf(k) >= 0);
        #endregion

        #region 判断多个字符串是否出现在原字符串中【忽略大小写】 + ContainsAnyIgnoreCase(this string value, params string[] keywords)
        /// <summary>
        /// 判断多个字符串是否出现在原字符串中【忽略大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keywords">要查找的多个字符串</param>
        /// <returns></returns>
        public static bool ContainsAnyIgnoreCase(this string value, params string[] keywords) => keywords.Any(k => value.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
        #endregion

        #region 查找并移除字符串，可查找多个目标【区分大小写】 + Remove(this string value, params string[] keywords)
        /// <summary>
        /// 查找并移除字符串，可查找多个目标【区分大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keywords">查找的关键词</param>
        /// <returns></returns>
        public static string Remove(this string value, params string[] keywords)
        {
            if (string.IsNullOrEmpty(value) || keywords.Length == 0) return value;
            foreach (var item in keywords)
                value = value.Replace(item, string.Empty);
            return value;
        }
        #endregion

#if NET6_0_OR_GREATER
        #region 查找并移除字符串，可查找多个目标【忽略大小写】 + RemoveIgnoreCase(this string value, params string[] keywords)
        /// <summary>
        /// 查找并移除字符串，可查找多个目标【忽略大小写】
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="keywords">查找的目标</param>
        /// <returns></returns>
        public static string RemoveIgnoreCase(this string value, params string[] keywords)
        {
            if (string.IsNullOrEmpty(value) || keywords.Length == 0) return value;
            foreach (var item in keywords)
                value = value.Replace(item, string.Empty, StringComparison.OrdinalIgnoreCase);
            return value;
        }
        #endregion
#endif

        #region 字符串按指定长度进行截取(超过长度可加后缀) + Truncat(this string value, int length, string suffix)
        /// <summary>
        /// 字符串按指定长度进行截取(超过长度可加后缀)
        /// </summary>
        /// <param name="value">原字符串</param>
        /// <param name="length">要截取的长度</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        public static string Truncat(this string value, int length, string suffix)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            if (length < 1) return suffix;
            //判断原字符串是否大于最大长度
            if (value.Length > length)
            {
                //判断后缀是否为空
                if (string.IsNullOrEmpty(suffix)) return value.Substring(0, length);
                else return $"{value.Substring(0, length)}{suffix}";
            }
            return value;
        }
        #endregion
    }
}