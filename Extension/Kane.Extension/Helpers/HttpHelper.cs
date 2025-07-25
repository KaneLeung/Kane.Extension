﻿// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

#if !NET40
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
#if NET6_0_OR_GREATER
using Kane.Extension.Json;
#else
using Kane.Extension.JsonNet;
#endif

namespace Kane.Extension
{
    /// <summary>
    /// 常用的Http请求扩展
    /// <para>如果使用频繁，请使用静态的【HttpClient】，避免【实例化每个请求的 HttpClient 类将耗尽重负载下可用的插槽数。这将导致 SocketException 错误】</para>
    /// <para>https://learn.microsoft.com/zh-cn/dotnet/fundamentals/networking/http/httpclient-guidelines</para>
    /// </summary>
    public class HttpHelper
    {
        private static readonly HttpClient client;
#if NET6_0_OR_GREATER
        /// <summary>
        /// 【写成静态】带来的问题显而易见，在不重启应用的前提下，HttpClient 中的 DNS 永远不会被刷新。
        /// <para>根据预期的 DNS 更改，使用 static 或 singleton HttpClient 实例，并将 PooledConnectionLifetime 设置为所需间隔（例如 2 分钟）。</para> 
        /// <para>这可以解决端口耗尽和 DNS 更改两个问题，而且不会增加 IHttpClientFactory 的开销。 如果需要模拟处理程序，可以单独注册它。</para>
        /// </summary>
#else
        /// <summary>
        /// 【写成静态】带来的问题显而易见，在不重启应用的前提下，HttpClient 中的 DNS 永远不会被刷新。
        /// </summary>
#endif
        static HttpHelper()
        {
#if NET6_0_OR_GREATER
            var handler = new SocketsHttpHandler()
            {
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
#else
            var handler = new HttpClientHandler() { UseCookies = true, AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
#endif
            client = new HttpClient(handler);
        }

        #region 将【Json字符串】发送【POST】请求，返回【实体对象】 + PostJsonAsync<TResult>(string url, string json, string token = null)
        /// <summary>
        /// 将【Json字符串】发送【POST】请求，返回【实体对象】
        /// </summary>
        /// <typeparam name="TResult">返回的实体</typeparam>
        /// <param name="url">POST请求的地址</param>
        /// <param name="json">发送的Json字符串</param>
        /// <param name="token">Authorization标头的Token</param>
        /// <returns></returns>
        public static async Task<TResult> PostJsonAsync<TResult>(string url, string json, string token = null) => (await PostJsonAsync(url, json, token)).ToObject<TResult>();
        #endregion

        #region 将【实体对象】发送【POST】请求，返回【实体对象】 + PostObjAsync<TResult, TSend>(string url, TSend data, string token = null)
        /// <summary>
        /// 将【实体对象】发送【POST】请求，返回【实体对象】
        /// </summary>
        /// <typeparam name="TResult">接收的实体</typeparam>
        /// <typeparam name="TSend">发送的实体</typeparam>
        /// <param name="url">POST请求的地址</param>
        /// <param name="data">发送的实体对象</param>
        /// <param name="token">Authorization标头的Token</param>
        /// <returns></returns>
        public static async Task<TResult> PostObjAsync<TResult, TSend>(string url, TSend data, string token = null) => (await PostJsonAsync(url, data.ToJson(), token)).ToObject<TResult>();
        #endregion

        #region 将【实体对象】发送【POST】请求，返回字符串 + PostObjAsync<TSend>(string url, TSend data, string token = null)
        /// <summary>
        /// 将【实体对象】发送【POST】请求，返回字符串
        /// </summary>
        /// <typeparam name="TSend">发送的实体</typeparam>
        /// <param name="url">POST请求的地址</param>
        /// <param name="data">发送的实体对象</param>
        /// <param name="token">Authorization标头的Token</param>
        /// <returns></returns>
        public static async Task<string> PostObjAsync<TSend>(string url, TSend data, string token = null) => await PostJsonAsync(url, data.ToJson(), token);
        #endregion

        #region 将【Json字符串】发送【POST】请求，返回字符串 + PostJsonAsync(string url, string json, string token = null)
        /// <summary>
        /// 将【Json字符串】发送【POST】请求，返回字符串
        /// </summary>
        /// <param name="url">POST请求的地址</param>
        /// <param name="json">发送的Json字符串</param>
        /// <param name="token">Authorization标头的Token</param>
        /// <returns></returns>
        public static async Task<string> PostJsonAsync(string url, string json, string token = null)
        {
            client.DefaultRequestHeaders.Clear();
            using HttpContent content = new StringContent(json);
            if (token.HasValue()) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        #region 异步POST请求 + PostAsync(...)
        /// <summary>
        /// 异步POST请求
        /// </summary>
        /// <param name="url">POST请求的地址</param>
        /// <param name="data">POST的数据</param>
        /// <param name="contentType">常用【application/xml】【application/json】【application/text】【application/x-www-form-urlencoded】
        /// <para>可参考https://docs.microsoft.com/zh-cn/dotnet/api/system.net.httprequestheader?view=netcore-3.1</para></param>
        /// <param name="headers">请求消息头</param>
        /// <param name="token">Authorization标头的Token</param>
        /// <param name="timeout">请求超时时间，默认30秒</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(
            string url,
            string data,
            string contentType = "application/json",
            Dictionary<string, string> headers = null,
            string token = null,
            int timeout = 30)
        {
            client.DefaultRequestHeaders.Clear();
            client.Timeout = new TimeSpan(0, 0, timeout);
            HttpContent content = new StringContent(data, Encoding.UTF8);
            if (headers is not null && headers.Count > 0)
            {
                foreach (var item in headers)
                    client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }
            if (contentType.HasValue()) content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            if (token.HasValue()) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        #endregion

        #region 发送【GET】请求，返回【实体对象】 + GetAsyncToObj<TResult>(string url, string token = null)
        /// <summary>
        /// 发送【GET】请求，返回【实体对象】
        /// </summary>
        /// <typeparam name="TResult">接收的实体</typeparam>
        /// <param name="url">Get请求的地址</param>
        /// <param name="token">Authorization标头的Token</param>
        /// <returns></returns>
        public static async Task<TResult> GetAsyncToObj<TResult>(string url, string token = null) => (await GetAsync(url, token: token)).ToObject<TResult>();
        #endregion

        #region 异步Get请求 + GetAsync(string url, string contentType = "application/json", Dictionary<string, string> headers = null, string token = null)
        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="url">Get请求的地址</param>
        /// <param name="contentType">常用【text/plain】【application/json】【text/html】</param>
        /// <param name="headers">请求消息头</param>
        /// <param name="token">Authorization标头的Token</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, string contentType = "application/json", Dictionary<string, string> headers = null, string token = null)
        {
            client.DefaultRequestHeaders.Clear();
            if (headers is not null && headers.Count > 0)
            {
                foreach (var item in headers)
                    client.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);
            }
            if (contentType.HasValue()) client.DefaultRequestHeaders.Add("ContentType", contentType);
            if (token.HasValue()) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
#endif