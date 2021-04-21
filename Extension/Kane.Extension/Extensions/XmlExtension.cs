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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Kane.Extension
{
    /// <summary>
    /// Xml类扩展方法
    /// </summary>
    public static class XmlExtension
    {
        #region 将Stream反序列化成对象 + ToObject<T>(this Stream stream) where T : class, new()
        /// <summary>
        /// 将Stream反序列化成对象
        /// </summary>
        /// <typeparam name="T">要反序列化成对象类型</typeparam>
        /// <param name="stream">要反序列化的Stream</param>
        /// <returns></returns>
        public static T ToObject<T>(this Stream stream) where T : class, new()
        {
            stream.Seek(0, SeekOrigin.Begin);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
        #endregion

        #region 将TextReader反序列化成对象 + ToObject<T>(this TextReader reader) where T : class, new()
        /// <summary>
        /// 将TextReader反序列化成对象
        /// </summary>
        /// <typeparam name="T">要反序列化成对象类型</typeparam>
        /// <param name="reader">要反序列化的TextReader</param>
        /// <returns></returns>
        public static T ToObject<T>(this TextReader reader) where T : class, new() => (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        #endregion

        #region 将对象Xml序列化 + ToXml<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
        /// <summary>
        /// 将对象Xml序列化
        /// </summary>
        /// <typeparam name="T">要序列化的对象类型</typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="removeNamespace">是否去掉命名空间</param>
        /// <param name="removeVersion">是否去掉版本信息</param>
        /// <returns></returns>
        public static string ToXml<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
        {
            var temp = ToXmlBytes(value, removeNamespace, removeVersion).BytesToString();
            if (!temp.StartsWith("<", StringComparison.OrdinalIgnoreCase))
                return temp.Substring(1, temp.Length - 1);//写入器使用UTF8编码时，转换后第一个字符会出现一个不存在的符号，其十六进制为【0xEFBBBF】
            return temp;
        }
        #endregion

        #region 将对象Xml序列化，可自定义命名空间，可设置写入器配置 + ToXml<T>(this T value, IEnumerable<KeyValuePair<string, string>> namespaces, XmlWriterSettings settings = null) where T : class, new()
        /// <summary>
        /// 将对象Xml序列化，可自定义命名空间，可设置写入器配置
        /// </summary>
        /// <typeparam name="T">要序列化的对象类型</typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="namespaces">要添加的命名空间集合，如 xmlns:xsi="xxx"，【xsi】Key为[前缀]，【xxx】Value为[值]</param>
        /// <param name="settings">Xml写入器配置</param>
        /// <returns></returns>
        public static string ToXml<T>(this T value, IEnumerable<KeyValuePair<string, string>> namespaces, XmlWriterSettings settings = null) where T : class, new()
        {
            var temp = ToXmlBytes(value, namespaces, settings).BytesToString();
            if (!temp.StartsWith("<", System.StringComparison.OrdinalIgnoreCase))
                return temp.Substring(1, temp.Length - 1);//写入器使用UTF8编码时，转换后第一个字符会出现一个不存在的符号，其十六进制为【0xEFBBBF】
            return temp;
        }
        #endregion

        #region 将对象Xml序列化成字节数组【Btye[]】 + ToXmlBytes<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
        /// <summary>
        /// 将对象Xml序列化成字节数组【Btye[]】
        /// </summary>
        /// <typeparam name="T">要序列化的对象类型</typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="removeNamespace">是否去掉命名空间</param>
        /// <param name="removeVersion">是否去掉版本信息</param>
        /// <returns></returns>
        public static byte[] ToXmlBytes<T>(this T value, bool removeNamespace = false, bool removeVersion = false) where T : class, new()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = removeVersion,//【True】去除xml声明<?xml version="1.0" encoding="utf-8"?>
                Indent = true,//为True时，换行，缩进
                Encoding = Encoding.UTF8//默认为UTF8编码
            };
            using MemoryStream stream = new MemoryStream();
            using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                if (removeNamespace) ns.Add(string.Empty, string.Empty);//去除默认命名空间xmlns:xsd和xmlns:xsi
                new XmlSerializer(typeof(T)).Serialize(xmlWriter, value, ns);//序列化对象
            }
            return stream.ToArray();
        }
        #endregion

        #region 将对象Xml序列化成字节数组【Btye[]】，可自定义命名空间，可设置写入器配置 + ToXmlBytes<T>(this T value, IEnumerable<KeyValuePair<string, string>> namespaces, XmlWriterSettings settings = null) where T : class, new()
        /// <summary>
        /// 将对象Xml序列化成字节数组【Btye[]】，可自定义命名空间，可设置写入器配置
        /// </summary>
        /// <typeparam name="T">要序列化的对象类型</typeparam>
        /// <param name="value">要序列化的对象</param>
        /// <param name="namespaces">要添加的命名空间集合，如 xmlns:xsi="xxx"，【xsi】Key为[前缀]，【xxx】Value为[值]</param>
        /// <param name="settings">Xml写入器配置</param>
        /// <returns></returns>
        public static byte[] ToXmlBytes<T>(this T value, IEnumerable<KeyValuePair<string, string>> namespaces, XmlWriterSettings settings = null) where T : class, new()
        {
            using MemoryStream stream = new MemoryStream();
            using (XmlWriter xmlWriter = settings == null ? XmlWriter.Create(stream) : XmlWriter.Create(stream, settings))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                if (namespaces?.Count() > 0)
                {
                    foreach (var item in namespaces)
                        ns.Add(item.Key, item.Value);//添加前缀和命名空间值，如 xmlns:xsi="xxx"，【xsi】为前缀，【xxx】为值
                }
                new XmlSerializer(typeof(T)).Serialize(xmlWriter, value, ns);//序列化对象
            }
            return stream.ToArray();
        }
        #endregion
    }
}