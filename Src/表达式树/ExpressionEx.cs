#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ExpressionEx
* 类 描 述 ：表达式树帮助类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：PC-0432 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/12/07 09:31:18
* 更新时间 ：2020/12/07 09:31:18
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 表达式树帮助类
    /// </summary>
    public static class ExpressionEx
    {
        #region 通过表达式树，根据属性名获取属性值 + GetPropValueExp<TSource, TResult>(this TSource source, string name)
        /// <summary>
        /// 通过表达式树，根据属性名获取属性值，失败返回TResult的默认值
        /// <para>也可以使用<see cref="ClassEx.GetPropValue{TSource, TResult}(TSource, string)"/>反射方法实现，性能略低一些</para>
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
        /// <para>也可以使用<see cref="ClassEx.SetPropValue{TSource, TValue}(TSource, string, TValue)"/>反射方法实现，性能略低一些</para>
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
    }
}