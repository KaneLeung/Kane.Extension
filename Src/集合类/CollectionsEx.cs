#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：CollectionsEx
* 类 描 述 ：集合类相关扩展类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/05/05 15:27:09
* 更新时间 ：2020/07/27 09:02:09
* 版 本 号 ：v1.0.3.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 集合类相关扩展类
    /// </summary>
    public static class CollectionsEx
    {
        #region 判断集合是否为Null或空集合 + IsNullOrEmpty<T>(this IEnumerable<T> collection)
        /// <summary>
        /// 判断集合是否为Null或空集合
        /// </summary>
        /// <typeparam name="T">泛型集合类型</typeparam>
        /// <param name="collection">当前集合</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();
        #endregion

        #region 判断集合是否不为Null并且非空集合 + IsNotEmpty<T>(this IEnumerable<T> collection)
        /// <summary>
        /// 判断集合是否不为Null并且非空集合
        /// </summary>
        /// <typeparam name="T">泛型集合类型</typeparam>
        /// <param name="collection">当前集合</param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> collection) => collection != null && collection.Any();
        #endregion

        #region 泛型集合转换为DataTable + ToDataTable<T>(this IEnumerable<T> source, string tableName = "")
        /// <summary>
        /// 泛型集合转换为DataTable
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="tableName">表名</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>源集合对象为空</exception>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, string tableName = "")
        {
            var type = typeof(T);
            source.ThrowIfNull(nameof(source), $"源【{type.Name}】数据表不可为空！");
            int index = 0;
            var properties = type.GetProperties();
            var result = tableName.IsNullOrEmpty() ? new DataTable() : new DataTable(tableName);
            foreach (var property in properties)
                result.Columns.Add(new DataColumn(property.Name));
            foreach (var item in source)
            {
                foreach (var property in properties)
                {
#if NET40
                    result.Rows[index++][property.Name] = property.GetValue(item, null);
#else
                    result.Rows[index++][property.Name] = property.GetValue(item);
#endif
                }
            }
            return result;
        }
        #endregion

        #region DataTable转换为泛型集合 + ToList<T>(this DataTable dataTable)
        /// <summary>
        /// DataTable转换为泛型集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <exception cref="ArgumentNullException"><paramref name="dataTable"/>源数据表不可为空</exception>
        public static IEnumerable<T> ToList<T>(this DataTable dataTable)
        {
            var type = typeof(T);
            dataTable.ThrowIfNull(nameof(dataTable), $"源【{type.Name}】数据表不可为空！");
            var properties = type.GetProperties();
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var noParamCtor = constructors.Single(k => k.GetParameters().Length == 0);
            var result = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                var item = (T)noParamCtor.Invoke(null);
                foreach (var property in properties)
                {
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        var setter = property.GetSetMethod(true);
                        if (setter != null)
                        {
                            var value = row[property.Name] == DBNull.Value ? null : row[property.Name];
                            setter.Invoke(item, new[] { value });
                        }
                    }
                }
                result.Add(item);
            }
            return result;
        }
        #endregion

        #region 如果条件成功，则向集合添加元素 + AddIf<T>(this ICollection<T> collection, bool flag, T item)
        /// <summary>
        /// 如果条件成功，则向集合添加元素
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">原集合</param>
        /// <param name="flag">条件</param>
        /// <param name="item">添加的元素</param>
        /// <returns></returns>
        public static bool AddIf<T>(this ICollection<T> collection, bool flag, T item)
        {
            collection.ThrowIfNull(nameof(collection), $"集合不能为Null");
            if (flag)
            {
                collection.Add(item);
                return true;
            }
            else return false;
        }
        #endregion

        #region 如果条件成功，则向集合添加元素 + AddIf<T>(this ICollection<T> collection, Func<bool> func, T item)
        /// <summary>
        /// 如果条件成功，则向集合添加元素
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">原集合</param>
        /// <param name="func">条件委托</param>
        /// <param name="item">添加的元素</param>
        /// <returns></returns>
        public static bool AddIf<T>(this ICollection<T> collection, Func<bool> func, T item)
        {
            collection.ThrowIfNull(nameof(collection), $"集合不能为Null");
            if (func())
            {
                collection.Add(item);
                return true;
            }
            else return false;
        }
        #endregion

        #region 如果条件成功，则向集合添加元素集合 + AddIf<T>(this ICollection<T> collection, bool flag, IEnumerable<T> items)
        /// <summary>
        /// 如果条件成功，则向集合添加元素集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">原集合</param>
        /// <param name="flag">条件</param>
        /// <param name="items">添加的元素集合</param>
        /// <returns></returns>
        public static bool AddIf<T>(this ICollection<T> collection, bool flag, IEnumerable<T> items)
        {
            collection.ThrowIfNull(nameof(collection), $"集合不能为Null");
            if (flag)
            {
                foreach (var item in items)
                    collection.Add(item);
                return true;
            }
            else return false;
        }
        #endregion

        #region 如果条件成功，则向集合添加元素集合 + AddIf<T>(this ICollection<T> collection, Func<bool> func, IEnumerable<T> items)
        /// <summary>
        /// 如果条件成功，则向集合添加元素集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">原集合</param>
        /// <param name="func">条件委托</param>
        /// <param name="items">添加的元素集合</param>
        /// <returns></returns>
        public static bool AddIf<T>(this ICollection<T> collection, Func<bool> func, IEnumerable<T> items)
        {
            collection.ThrowIfNull(nameof(collection), $"集合不能为Null");
            if (func())
            {
                foreach (var item in items)
                    collection.Add(item);
                return true;
            }
            else return false;
        }
        #endregion

#if !NET40
        #region 将集合转为Csv字符串 + ToCsv<T>(this IEnumerable<T> collection, bool hasTitle = true, string format = "yyyy-MM-dd HH:mm:ss")
        /// <summary>
        /// 将集合转为Csv字符串
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">当前集合</param>
        /// <param name="hasTitle">是否包含标题列，即属性名</param>
        /// <param name="format">时间转换的格式</param>
        /// <returns></returns>
        public static string ToCsv<T>(this IEnumerable<T> collection, bool hasTitle = true, string format = "yyyy-MM-dd HH:mm:ss")
        {
            var builder = new StringBuilder();
            var props = typeof(T).GetProperties();
            if (hasTitle)//包含标题
            {
                foreach (var item in props)
                {
                    builder.Append(item.Name);
                    builder.Append(",");
                }
                builder.Append(Environment.NewLine);
            }
            foreach (var item in collection)
            {
                for (int i = 0; i < props.Length; i++)
                {
                    if (i != 0) builder.Append(",");
                    var value = props[i].GetValue(item);
                    if (value.IsNull()) builder.Append("\"\"");//如果为空，
                    else
                    {
                        if (props[i].PropertyType == typeof(DateTime))//如果是时间格式，则检测格式中是否带有:半角的逗号【,】或半角的双引号【"】或换行符【\r\n】或【\n】,则字段必须放在引号内
                        {
                            builder.Append(ToCsvFormat(((DateTime)value).ToString(format)));
                        }
                        else if (props[i].PropertyType.IsGenericType && props[i].PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))//是否为可空类型
                        {
                            var temp = props[i].PropertyType.GenericTypeArguments.FirstOrDefault();
                            if (temp == typeof(DateTime)) builder.Append(ToCsvFormat(((DateTime)value).ToString(format)));
                            else if (temp == typeof(string)) builder.Append(ToCsvFormat((string)value));
                            else builder.Append(value);
                        }
                        else if (props[i].PropertyType == typeof(string)) builder.Append(ToCsvFormat((string)value));
                        else builder.Append(value);
                    }
                }
                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }
        #endregion

        #region 判断字符串是否为特殊格式的字符串，如果是，则转为Csv格式的字符串 + ToCsvFormat(string value)
        /// <summary>
        /// 判断字符串是否为特殊格式的字符串，如果是，则转为Csv格式的字符串
        /// </summary>
        /// <param name="value">要判断字符串</param>
        /// <returns></returns>
        private static string ToCsvFormat(string value)
        {
            if (value.Contains("\"")) value = value.Replace("\"", "\"\"");
            if (value.Contains("\"") || value.Contains(",") || value.Contains("\r\n") || value.Contains("\n")) value = $"\"{value}\"";
            return value;
        } 
        #endregion
#endif
    }
}