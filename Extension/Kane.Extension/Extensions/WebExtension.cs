// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.6
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
#if !NET40
using System.Net.Http.Headers;
#endif

namespace Kane.Extension
{
    /// <summary>
    /// 常用的网络帮助类
    /// 【HttpWebRequest】 已经不推荐直接使用了，这已经作为底层机制，不适合业务代码使用
    /// -->这是.NET创建者最初开发用于使用HTTP请求的标准类。使用HttpWebRequest可以让开发者控制请求/响应流程的各个方面，如 timeouts, cookies, headers, protocols。
    /// 另一个好处是HttpWebRequest类不会阻塞UI线程。例如，当您从响应很慢的API服务器下载大文件时，您的应用程序的UI不会停止响应。
    /// 
    /// 【WebClient】 不想为http细节处理而头疼的coder而生，由于内部已经处理了通用设置，某些情况可能导致性能不是很理想
    /// -->WebClient是一种更高级别的抽象，是HttpWebRequest为了简化最常见任务而创建的，使用过程中你会发现他缺少基本的header，timeoust的设置，不过这些可以通过继承httpwebrequest来实现。
    /// 使用WebClient可能比HttpWebRequest直接使用更慢（大约几毫秒）。但这种“低效率”带来了巨大的好处：它需要更少的代码和隐藏了细节处理，更容易使用，并且在使用它时你不太可能犯错误。
    /// 同样的请求示例现在很简单只需要两行而且内部周到的处理完了细节
    /// 
    /// 【HttpClient】 更加适用于异步编程模型中
    /// -->HttpClient提供强大的功能，提供了异步支持，可以轻松配合async await 实现异步请求，具体使用可参考：https://www.cnblogs.com/xiaoliangge/p/9476568.html
    /// </summary>
    public static class WebHelper
    {
#if NETFRAMEWORK
        #region 根据字符串获取网络图片，默认超时时间为100秒 + GetUriImage(string uri, int timeout = 100000)
        /// <summary>
        /// 根据字符串获取网络图片，默认超时时间为100秒
        /// HttpWebRequest.Timeout,默认值为100000毫秒（100秒）
        /// </summary>
        /// <param name="uri">字符串Uri地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static Image GetUriImage(string uri, int timeout = 100000)
        {
            try
            {
                var temp = WebRequest.Create(uri);
                temp.Timeout = timeout;
                return Image.FromStream(temp.GetResponse().GetResponseStream());
            }
            catch { return null; }
        }
        #endregion

        #region 根据Uri获取网络图片，默认超时时间为100秒 + GetUriImage(Uri uri, int timeout = 100000)
        /// <summary>
        /// 根据Uri获取网络图片，默认超时时间为100秒
        /// HttpWebRequest.Timeout,默认值为100000毫秒（100秒）
        /// </summary>
        /// <param name="uri">Uri地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static Image GetUriImage(Uri uri, int timeout = 100000)
        {
            try
            {
                var temp = WebRequest.Create(uri);
                temp.Timeout = timeout;
                return Image.FromStream(temp.GetResponse().GetResponseStream());
            }
            catch { return null; }
        }
        #endregion
#endif

        #region 移除HTML标签 + ReplaceHtml(string content)
        /// <summary>
        /// 移除HTML标签
        /// </summary>
        /// <param name="content">待移除Html的内容</param>
        /// <returns></returns>
        public static string ReplaceHtml(string content)
        {
            var result = Regex.Replace(content, "<[^>]+>", "", RegexOptions.Compiled);
            return Regex.Replace(result, "&[^;]+;", "", RegexOptions.Compiled);
        }
        #endregion

        #region 将[string,string]集合转成查询字符串(QueryString) + ToQueryString(this ICollection<KeyValuePair<string, string>> parms)
        /// <summary>
        /// 将[string,string]集合转成查询字符串(QueryString)
        /// </summary>
        /// <param name="parms">字典值</param>
        /// <returns></returns>
        public static string ToQueryString(this ICollection<KeyValuePair<string, string>> parms)
        {
            var sb = new StringBuilder();
            foreach (var keyValuePair in parms)
            {
                sb.Append(keyValuePair.Key);
                sb.Append('=');
                sb.Append(Uri.EscapeDataString(keyValuePair.Value));
                sb.Append('&');
            }
            return sb.ToString().TrimEnd('&');
        }
        #endregion

        #region 将Uri里的QueryString转成集合，并按照字典序排序 + GetQuerys(this Uri uri, bool toLower = true)
        /// <summary>
        /// 将Uri里的QueryString转成集合，并按照字典序排序
        /// </summary>
        /// <param name="uri">要转的Uri</param>
        /// <param name="toLower">是否转成小写</param>
        /// <returns></returns>
        public static IOrderedEnumerable<KeyValuePair<string, string>> GetQuerys(this Uri uri, bool toLower = true)
        {
            var collection = HttpUtility.ParseQueryString(uri.Query);
            return collection.Cast<string>().Where(k => k.HasValue()).Select(k => new KeyValuePair<string, string>(toLower ? k.ToLower() : k, collection[k])).OrderBy(k => k.Key);
        }
        #endregion

#if !NET40
        #region 将HttpRequestHeaders里的Header转成集合，并按照字典序排序 + GetHeaders(this HttpRequestHeaders headers, string separator = ";")
        /// <summary>
        /// 将HttpRequestHeaders里的Header转成集合，并按照字典序排序
        /// </summary>
        /// <param name="headers">要转的<see cref="HttpRequestHeaders"/></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static IOrderedEnumerable<KeyValuePair<string, string>> GetHeaders(this HttpRequestHeaders headers, string separator = ";")
            => headers.Select(k => new KeyValuePair<string, string>(k.Key.ToLower(), Uri.EscapeDataString(string.Join(separator, k.Value)))).OrderBy(k => k.Key);
        #endregion
#endif

        #region 对URL字符串进行编码，默认使用【UTF8】编码 + UrlEncode(string value)
        /// <summary>
        /// 对URL字符串进行编码，默认使用【UTF8】编码
        /// <para>这个使用时，会对空格转成+号，注意！！！并对编码后的结果小写，并对一些特殊符号不进行编码</para>
        /// <para>必须搭配<see cref="UrlEncode(string)"/>进行解码</para>
        /// </summary>
        /// <param name="value">要编码的值</param>
        /// <returns></returns>
        public static string UrlEncode(string value) => HttpUtility.UrlEncode(value, Encoding.UTF8);
        #endregion

        #region 对URL字符串进行编码，可设置编码 + UrlEncode(string value, Encoding encoding)
        /// <summary>
        /// 对URL字符串进行编码，可设置编码
        /// <para>这个使用时，会对空格转成+号，注意！！！并对编码后的结果小写，并对一些特殊符号不进行编码</para>
        /// <para>必须搭配<see cref="UrlEncode(string, Encoding)"/>进行解码</para>
        /// </summary>
        /// <param name="value">要编码的值</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string UrlEncode(string value, Encoding encoding) => HttpUtility.UrlEncode(value, encoding);
        #endregion

        #region URL编码的字符串转换为解码的字符串，默认使用【UTF8】编码 + UrlDecode(string value)
        /// <summary>
        /// URL编码的字符串转换为解码的字符串，默认使用【UTF8】编码
        /// </summary>
        /// <param name="value">要解码的URL编码</param>
        /// <returns></returns>
        public static string UrlDecode(string value) => HttpUtility.UrlDecode(value, Encoding.UTF8);
        #endregion

        #region URL编码的字符串转换为解码的字符串，可设置编码 + UrlDecode(string value, Encoding encoding)
        /// <summary>
        /// URL编码的字符串转换为解码的字符串，可设置编码
        /// </summary>
        /// <param name="value">要解码的URL编码</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string UrlDecode(string value, Encoding encoding) => HttpUtility.UrlDecode(value, encoding);
        #endregion

        #region 将字符串转换为它的转义表示形式，可以转一些特殊符号 + ToEscape(string value)
        /// <summary>
        /// 将字符串转换为它的转义表示形式，可以转一些特殊符号
        /// <para>并对转义后的结果大写，可使用<see cref="ToUnescape(string)"/>进行还原</para>
        /// </summary>
        /// <param name="value">要转义的字符串</param>
        /// <returns></returns>
        public static string ToEscape(string value) => Uri.EscapeDataString(value);
        #endregion

        #region 将字符串转换为它的非转义表示形式 + ToUnescape(string value)
        /// <summary>
        /// 将字符串转换为它的非转义表示形式
        /// </summary>
        /// <param name="value">要还原的字符串</param>
        /// <returns></returns>
        public static string ToUnescape(string value) => Uri.UnescapeDataString(value);
        #endregion

        #region Uri累加路径 + Append(this Uri uri, params string[] paths)
        /// <summary>
        /// Uri累加路径
        /// </summary>
        /// <param name="uri">原来的Uri</param>
        /// <param name="paths">要加入的路径</param>
        /// <returns></returns>
        public static Uri Append(this Uri uri, params string[] paths)
            => new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));
        #endregion

        #region 根据UserAgent判断用户的操作系统 + GetOSName(string userAgent)
        /// <summary>
        /// 根据UserAgent判断用户的操作系统
        /// </summary>
        /// <param name="userAgent">UserAgent字符串</param>
        /// <returns></returns>
        public static string GetOSName(string userAgent)
        {
            string temp = "未知";
            if (userAgent.Contains("NT 10.0")) temp = "Windows 10";
            else if (userAgent.Contains("NT 6.3")) temp = "Windows 8.1";
            else if (userAgent.Contains("NT 6.2")) temp = "Windows 8";
            else if (userAgent.Contains("NT 6.1")) temp = "Windows 7";
            else if (userAgent.Contains("NT 6.0")) temp = "Windows Vista/Server 2008";
            else if (userAgent.Contains("NT 5.2")) temp = userAgent.Contains("64") ? "Windows XP" : "Windows Server 2003";
            else if (userAgent.Contains("NT 5.1")) temp = "Windows XP";
            else if (userAgent.Contains("NT 5")) temp = "Windows 2000";
            //else if (userAgent.Contains("NT 4")) temp = "Windows NT4";
            //else if (userAgent.Contains("Me")) temp = "Windows Me";
            //else if (userAgent.Contains("98")) temp = "Windows 98";
            //else if (userAgent.Contains("95")) temp = "Windows 95";
            else if (userAgent.Contains("Android")) temp = "Android";
            else if (userAgent.Contains("iPhone") || userAgent.Contains("iPad")) temp = "IOS";
            else if (userAgent.Contains("Mac")) temp = "Mac";
            else if (userAgent.Contains("Unix")) temp = "UNIX";
            else if (userAgent.Contains("Linux")) temp = "Linux";
            else if (userAgent.Contains("SunOS")) temp = "SunOS";
            return temp;
        }
        #endregion

        #region 移动端检测正则
        /// <summary>
        /// 最后更新：2014.08.01
        /// <para>http://detectmobilebrowsers.com/</para>
        /// </summary>
        private static readonly Regex browserRegex = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static readonly Regex versionRegex = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        #endregion

        #region 根据UserAgent，判断是否为手机或移动端 + IsMobileAgent(this string userAgent)
        /// <summary>
        /// 根据UserAgent，判断是否为手机或移动端
        /// </summary>
        /// <param name="userAgent">UserAgent字符串</param>
        /// <returns></returns>
        public static bool IsMobileAgent(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return false;
            else return browserRegex.IsMatch(userAgent) || versionRegex.IsMatch(userAgent.Substring(0, 4));
        }
        #endregion
    }
}