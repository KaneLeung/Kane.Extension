// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.0
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 字节类扩展方法
    /// </summary>
    public static class ByteExtension
    {
        #region 字节数组转十六进制字符串【全小写】 + BytesToHex(this byte[] value)
        /// <summary>
        /// 字节数组转十六进制字符串【全小写】，如果要大写，请使用<see cref="BytesToHEX"/>
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <returns>全小写</returns>
        public static string BytesToHex(this byte[] value)
        {
            var builder = new StringBuilder();
#if NETCOREAPP3_1_OR_GREATER
            var span = value.AsSpan();//有大幅度提升
            for (int i = 0; i < span.Length; i++)
            {
                builder.Append(span[i].ToString("x2"));
            }
#else
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append(value[i].ToString("x2"));
            }
#endif
            return builder.ToString();
        }
        #endregion

        #region 字节数组转十六进制字符串【全大写】 + BytesToHEX(this byte[] value)
        /// <summary>
        /// 字节数组转十六进制字符串【全大写】，如果要小写，请使用<see cref="BytesToHex"/>
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <returns>全大写</returns>
        public static string BytesToHEX(this byte[] value)
        {
            var builder = new StringBuilder();
#if NETCOREAPP3_1_OR_GREATER
            var span = value.AsSpan();//有大幅度提升
            for (int i = 0; i < span.Length; i++)
            {
                builder.Append(span[i].ToString("X2"));
            }
#else
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append(value[i].ToString("X2"));
            }
#endif
            return builder.ToString();
        }
        #endregion

        #region 字节数组转成字符串，默认使用UTF8编码 + BytesToString(this byte[] value
        /// <summary>
        /// 字节数组转成字符串，默认使用UTF8编码
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <returns></returns>
        public static string BytesToString(this byte[] value) => Encoding.UTF8.GetString(value);
        #endregion

        #region 字节数组转成字符串，编码方式可选 + BytesToString(this byte[] value, Encoding encoding)
        /// <summary>
        /// 字节数组转成字符串，编码方式可选
        /// </summary>
        /// <param name="value">要转的字节数组</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string BytesToString(this byte[] value, Encoding encoding) => encoding.GetString(value);
        #endregion

        #region 字节数组转成Base64字符串 + ToBase64(this byte[] value)
        /// <summary>
        /// 字节数组转成Base64字符串
        /// </summary>
        /// <param name="value">要转换的字节数组</param>
        /// <returns></returns>
        public static string ToBase64(this byte[] value) => Convert.ToBase64String(value);
        #endregion
    }
}