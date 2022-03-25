// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.0
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Kane.Extension
{
    /// <summary>
    /// 流类扩展方法
    /// </summary>
    public static class StreamExtension
    {
        #region 将Stream转成byte[] + ToBytes(this Stream stream)
        /// <summary>
        /// 将Stream转成byte[]
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <returns></returns>
        public static byte[] ToBytes(this Stream stream)
        {
            byte[] result = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);//设置当前流的位置为流的开始
            stream.Read(result, 0, result.Length);
            return result;
        }
        #endregion

        #region 将Stream转成String，默认使用UTF8编码 + StreamToString(this Stream stream)
        /// <summary>
        /// 将Stream转成String，默认使用UTF8编码
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <returns></returns>
        public static string StreamToString(this Stream stream) => stream.StreamToString(Encoding.UTF8);
        #endregion

        #region 将Stream转成String，可自定义编码 + StreamToString(this Stream stream, Encoding encoding)
        /// <summary>
        /// 将Stream转成String，可自定义编码
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string StreamToString(this Stream stream, Encoding encoding) => stream.ToBytes().BytesToString(encoding);
        #endregion

#if !NET40
        #region 将Stream转成byte[] + ToBytes(this Stream stream)
        /// <summary>
        /// 将Stream转成byte[]
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <returns></returns>
        public static async Task<byte[]> ToBytesAsync(this Stream stream)
        {
            byte[] result = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);//设置当前流的位置为流的开始
#if NETCOREAPP3_1_OR_GREATER
            await stream.ReadAsync(result.AsMemory(0, result.Length));
#else
            await stream.ReadAsync(result, 0, result.Length);
#endif
            return result;
        }
        #endregion

        #region 将Stream转成String，默认使用UTF8编码 + StreamToString(this Stream stream)
        /// <summary>
        /// 将Stream转成String，默认使用UTF8编码
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <returns></returns>
        public static async Task<string> StreamToStringAsync(this Stream stream) => await stream.StreamToStringAsync(Encoding.UTF8);
        #endregion

        #region 将Stream转成String，可自定义编码 + StreamToString(this Stream stream, Encoding encoding)
        /// <summary>
        /// 将Stream转成String，可自定义编码
        /// </summary>
        /// <param name="stream">要转的Stream</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static async Task<string> StreamToStringAsync(this Stream stream, Encoding encoding) => (await stream.ToBytesAsync()).BytesToString(encoding);
        #endregion
#endif
    }
}