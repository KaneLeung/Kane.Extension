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
using System.Linq.Expressions;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 表达式树扩展方法
    /// </summary>
    public static class ExpressionExtension
    {
        #region 通过表达式树，根据属性名获取属性值 + GetPropValueExp<TSource, TResult>(this TSource source, string name)
        /// <summary>
        /// 通过表达式树，根据属性名获取属性值，失败返回TResult的默认值
        /// <para>也可以使用<see cref="ReflectionExtension.GetPropValue{TSource, TResult}(TSource, string)"/>反射方法实现，性能略低一些</para>
        /// </summary>
        /// <typeparam name="TSource">对象类型</typeparam>
        /// <typeparam name="TResult">属性类型</typeparam>
        /// <param name="source">对象</param>
        /// <param name="name">属性名</param>
        /// <returns>属性的值</returns>
        public static TResult GetPropValueExp<TSource, TResult>(this TSource source, string name)
        {
            Type type = source.GetType();
            PropertyInfo info = type.GetProperty(name);
            if (info == null) return default;
            var sourceParaExp = Expression.Parameter(type);
            var resultParaExp = Expression.Parameter(typeof(TResult));
            var temp = Expression.Convert(sourceParaExp, type);//转成真实类型，防止Dynamic类型转换成Object
            var body = Expression.Property(temp, info);
            var resultFunc = Expression.Lambda<Func<TSource, TResult>>(body, sourceParaExp).Compile();
            return resultFunc(source);
        }
        #endregion

        #region 通过表达式树，根据属性名称设置属性的值 + SetPropValueExp<TSource, TValue>(this TSource source, string name, TValue value)
        /// <summary>
        /// 通过表达式树，根据属性名称设置属性的值
        /// <para>也可以使用<see cref="ReflectionExtension.SetPropValue{TSource, TValue}(TSource, string, TValue)"/>反射方法实现，性能略低一些</para>
        /// </summary>
        /// <typeparam name="TSource">对象类型</typeparam>
        /// <typeparam name="TValue">属性类型</typeparam>
        /// <param name="source">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否设置成功</returns>
        public static bool SetPropValueExp<TSource, TValue>(this TSource source, string name, TValue value)
        {
            Type type = source.GetType();
            PropertyInfo info = type.GetProperty(name);
            if (info == null) return false;
            var sourceParaExp = Expression.Parameter(type);
            var valueParaExp = Expression.Parameter(typeof(TValue));
            var temp = Expression.Convert(valueParaExp, info.PropertyType);
            var setMethod = info.GetSetMethod(true);//获取设置属性的值的方法
            if (setMethod != null)//判断【setMethod】是否为只读
            {
                var body = Expression.Call(sourceParaExp, info.GetSetMethod(), temp);
                var setValue = Expression.Lambda<Action<TSource, TValue>>(body, sourceParaExp, valueParaExp).Compile();
                setValue(source, value);
                return true;
            }
            return false;
        }
        #endregion

        #region 通过表达式树，深拷贝对象 + ExpDeepClone<T>(this T source) where T : class, new()
        /// <summary>
        /// 通过表达式树，深拷贝对象
        /// </summary>
        /// <typeparam name="T">源对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static T ExpDeepClone<T>(this T source) where T : class, new()
        {
            var parameterExpression = Expression.Parameter(typeof(T), "p");
            var memberBindingList = new List<MemberBinding>();
            foreach (var item in typeof(T).GetProperties())
            {
                if (item.CanWrite == false) continue;
                var property = Expression.Property(parameterExpression, typeof(T).GetProperty(item.Name));
                var memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }
            var memberInitExpression = Expression.MemberInit(Expression.New(typeof(T)), memberBindingList.ToArray());
            var exp = Expression.Lambda<Func<T, T>>(memberInitExpression, [parameterExpression]);
            return exp.Compile()(source);
        }
        #endregion
    }
}