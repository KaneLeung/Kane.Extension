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
using System.Linq;
using System.Linq.Expressions;

namespace Kane.Extension
{
    /// <summary>
    /// Linq扩展类
    /// <para>https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.queryable?view=netcore-3.1</para>
    /// </summary>
    public static class LinqExtension
    {
        #region WhereIf，满足条件进行查询 + WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        /// <summary>
        /// WhereIf，满足条件进行查询
        /// </summary>
        /// <typeparam name="T">数据元素类型</typeparam>
        /// <param name="query">数据源</param>
        /// <param name="condition">判断条件</param>
        /// <param name="predicate">用于测试每个元素是否满足条件的函数</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
            => condition ? query.Where(predicate) : query;
        #endregion

        #region OrderIf，满足条件进行【升序】排序 + OrderIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
        /// <summary>
        /// OrderIf,满足条件进行【升序】排序
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <typeparam name="TKey">由 keySelector 表示的函数返回的键类型</typeparam>
        /// <param name="query">数据源</param>
        /// <param name="condition">判断条件</param>
        /// <param name="keySelector">用于从元素中提取键的函数</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
            => condition ? query.OrderBy(keySelector) : query;
        #endregion

        #region OrderDescIf，满足条件进行【降序】排序 + OrderDescIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
        /// <summary>
        /// OrderDescIf,满足条件进行【降序】排序
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <typeparam name="TKey">由 keySelector 表示的函数返回的键类型</typeparam>
        /// <param name="query">数据源</param>
        /// <param name="condition">判断条件</param>
        /// <param name="keySelector">用于从元素中提取键的函数</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderDescIf<TSource, TKey>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, TKey>> keySelector)
            => condition ? query.OrderByDescending(keySelector) : query;
        #endregion

        #region Select查询后自动执行ToList() + SelectList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        /// <summary>
        /// Select查询后自动执行ToList()
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <typeparam name="TResult">返回的值的类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="selector">应用于每个元素的转换函数</param>
        /// <returns></returns>
        public static List<TResult> SelectList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
            => source.Select(selector).ToList();
        #endregion

        #region 根据属性名进行排序，默认为【升序】 + OrderBy<TSource>(this IQueryable<TSource> source, string property, bool descending = false) where TSource : class
        /// <summary>
        /// 根据属性名进行排序，默认为【升序】
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="property">属性名</param>
        /// <param name="descending">是否降序</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string property, bool descending = false) where TSource : class
            => CreateExpression(source, property, descending ? "OrderByDescending" : "OrderBy");
        #endregion

        #region 根据属性名进行排序，默认为【升序】 + OrderBy<TSource>(this IQueryable<TSource> source, string property, bool descending = false) where TSource : class
        /// <summary>
        /// 根据排序字符串进行排序，可设置分割字符，默认为【" "】
        /// <para>排序字符串如【PropertyA Desc】、【PropertyB asc】</para>
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderString">排序字符串</param>
        /// <param name="separator">分割符</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string orderString, char separator = ' ') where TSource : class
        {
            var temp = orderString.Split(separator);
            if (temp.Length > 1)
            {
                return CreateExpression(source, temp[0], temp[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy");
            }
            else return source;
        }
        #endregion

        #region 根据属性名进行【降序】排序 + OrderByDesc<TSource>(this IQueryable<TSource> source, string property) where TSource : class
        /// <summary>
        /// 根据属性名进行【降序】排序
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="property">属性名</param>
        /// <returns></returns>
        public static IQueryable<TSource> OrderByDesc<TSource>(this IQueryable<TSource> source, string property) where TSource : class
            => CreateExpression(source, property, "OrderByDescending");
        #endregion

        #region 根据属性名进行ThenBy排序，默认为【升序】 + ThenBy<TSource>(this IQueryable<TSource> source, string property, bool descending = false) where TSource : class
        /// <summary>
        /// 根据属性名进行ThenBy排序，默认为【升序】
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="property"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IQueryable<TSource> ThenBy<TSource>(this IQueryable<TSource> source, string property, bool descending = false) where TSource : class
            => CreateExpression(source, property, descending ? "ThenByDescending" : "ThenBy");
        #endregion

        #region 根据属性名进行ThenBy【降序】排序 + ThenByDesc<TSource>(this IQueryable<TSource> source, string property) where TSource : class
        /// <summary>
        /// 根据属性名进行ThenBy【降序】排序
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IQueryable<TSource> ThenByDesc<TSource>(this IQueryable<TSource> source, string property) where TSource : class
            => CreateExpression(source, property, "ThenByDescending");
        #endregion

        #region 创建表达式 + CreateExpression<TSource>(IQueryable<TSource> source, string property, string methodName) where TSource : class
        /// <summary>
        /// 创建表达式
        /// </summary>
        /// <typeparam name="TSource">数据元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="property">属性名</param>
        /// <param name="methodName">方法名</param>
        /// <returns></returns>
        private static IQueryable<TSource> CreateExpression<TSource>(IQueryable<TSource> source, string property, string methodName) where TSource : class
        {
            var param = Expression.Parameter(typeof(TSource), "KK");
            var pi = typeof(TSource).GetProperty(property);
            var selector = Expression.MakeMemberAccess(param, pi);
            var exp = Expression.Lambda(selector, param);
            var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TSource), pi.PropertyType }, source.Expression, exp);
            return source.Provider.CreateQuery<TSource>(resultExp);
        }
        #endregion
    }
}