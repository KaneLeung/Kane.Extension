#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ClassEx
* 类 描 述 ：反射类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:08:22
* 更新时间 ：2020/12/07 10:55:22
* 版 本 号 ：v1.0.5.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 反射类扩展
    /// </summary>
    public static class ClassEx
    {
        /// <summary>
        /// 【DeclaredOnly】：查找此特定类型中声明的成员，而不会包括这个类继承得到的成员
        /// <para>【Public】：查找类型中的公共成员</para>
        /// <para>【NonPublic】：查找类型中的非公共成员（internal protected private）</para>
        /// <para>【Instance】：查找类型中的实例成员</para>
        /// <para>【Static】：查找类型中的静态成员</para>
        /// <para>获取所有成员：BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance</para>
        /// <para>获取公有的实例成员：BindingFlags.Public | BindingFlags.Instance</para>
        /// </summary>
        internal const BindingFlags BINDING_FLAGS = BindingFlags.Public | BindingFlags.Instance;

        #region 获取类所有成员的属性信息 + GetProps<TSource>(this TSource source)
        /// <summary>
        /// 获取类所有成员的属性信息
        /// </summary>
        /// <param name="source">反射对象</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo[] GetProps<TSource>(this TSource source) => source.GetType().GetProperties(BINDING_FLAGS);
        #endregion

        #region 根据BindingFlags获取类指定成员的属性信息 + GetProps<TSource>(this TSource source, BindingFlags flags)
        /// <summary>
        /// 根据<see cref="BindingFlags"/>获取类指定成员的属性信息
        /// </summary>
        /// <typeparam name="TSource">反射对象类型</typeparam>
        /// <param name="source">反射对象</param>
        /// <param name="flags">自定义搜索类型</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo[] GetProps<TSource>(this TSource source, BindingFlags flags) => source.GetType().GetProperties(flags);
        #endregion

        #region 获取类指定成员的属性信息 + GetProp<TSource>(this TSource source, string name)
        /// <summary>
        /// 获取类指定成员的属性信息
        /// </summary>
        /// <typeparam name="TSource">反射对象类型</typeparam>
        /// <param name="source">反射对象</param>
        /// <param name="name">成员名称</param>
        /// <returns></returns>
        public static PropertyInfo GetProp<TSource>(this TSource source, string name) => source.GetType().GetProperty(name, BINDING_FLAGS);
        #endregion

        #region 检测对象是否包含指定【属性】 + HasProp<TSource>(this TSource source, string name)
        /// <summary>
        /// 检测对象是否包含指定【属性】
        /// </summary>
        /// <typeparam name="TSource">要检测对象类型</typeparam>
        /// <param name="source">要检测对象</param>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        public static bool HasProp<TSource>(this TSource source, string name) => source.GetType().GetProperty(name, BINDING_FLAGS) != null;
        #endregion

        #region 通过反射，根据属性名获取属性值，失败返回TResult的默认值 + GetPropValue<TSource, TResult>(this TSource source, string name)
        /// <summary>
        /// 通过反射，根据属性名获取属性值，失败返回TResult的默认值
        /// <para>也可使用<see cref="ExpressionEx.GetPropValueExp{TSource, TResult}(TSource, string)"/>表达式树方法实现，性能高一些</para>
        /// </summary>
        /// <typeparam name="TSource">反射对象类型</typeparam>
        /// <typeparam name="TResult">获取值类型</typeparam>
        /// <param name="source">反射对象</param>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public static TResult GetPropValue<TSource, TResult>(this TSource source, string name)
        {
            PropertyInfo fieldInfo = source.GetProp(name);
            return fieldInfo == null ? default : (TResult)fieldInfo.GetValue(source, null);
        }
        #endregion

        #region 通过反射，根据属性名称设置属性的值 + SetPropValue<TSource, TValue>(this TSource source, string name, TValue value)
        /// <summary>
        /// 通过反射，根据属性名称设置属性的值
        /// <para>也可以使用<see cref="ExpressionEx.SetPropValueExp{TSource, TValue}(TSource, string, TValue)"/>表达式树方法实现，性能高一些</para>
        /// </summary>
        /// <typeparam name="TSource">反射对象类型</typeparam>
        /// <typeparam name="TValue">属性类型</typeparam>
        /// <param name="source">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">要设置的值</param>
        /// <returns>是否设置成功</returns>
        public static bool SetPropValue<TSource, TValue>(this TSource source, string name, TValue value)
        {
            PropertyInfo fieldInfo = source.GetProp(name);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(source, value, null);
                return true;
            }
            else return false;
        }
        #endregion

        #region 获取所有的字段信息 + GetFields<TSource>(this TSource source)
        /// <summary>
        /// 获取所有的字段信息
        /// </summary>
        /// <param name="source">反射对象</param>
        /// <returns>字段信息</returns>
        public static FieldInfo[] GetFields<TSource>(this TSource source) => source.GetType().GetFields(BINDING_FLAGS);
        #endregion

        #region 根据BindingFlags获取所有的字段信息 + GetFields<TSource>(this TSource source, BindingFlags flags)
        /// <summary>
        /// 根据<see cref="BindingFlags"/>获取所有的字段信息
        /// </summary>
        /// <param name="source">反射对象</param>
        /// <param name="flags">自定义搜索类型</param>
        /// <returns>字段信息</returns>
        public static FieldInfo[] GetFields<TSource>(this TSource source, BindingFlags flags) => source.GetType().GetFields(flags);
        #endregion

        #region 获取所有的字段信息 + GetField<TSource>(this TSource source, string name)
        /// <summary>
        /// 获取指定的字段信息
        /// </summary>
        /// <param name="source">反射对象</param>
        /// <param name="name">字段名称</param>
        /// <returns>字段信息</returns>
        public static FieldInfo GetField<TSource>(this TSource source, string name) => source.GetType().GetField(name, BINDING_FLAGS);
        #endregion

        #region 获取单个字段的值 + GetFieldValue<TSource, TResult>(this TSource source, string name)
        /// <summary>
        /// 获取单个字段的值
        /// </summary>
        /// <param name="source">反射对象</param>
        /// <param name="name">字段名</param>
        /// <typeparam name="TSource">反射对象的类型</typeparam>
        /// <typeparam name="TResult">返回字段类型</typeparam>
        /// <returns>T类型</returns>
        public static TResult GetFieldValue<TSource, TResult>(this TSource source, string name)
        {
            FieldInfo fieldInfo = source.GetField(name);
            return fieldInfo == null ? default : (TResult)fieldInfo.GetValue(source);
        }
        #endregion

        #region 设置单个字段的值，返回是否成功 + SetFieldValue<TSource, TValue>(this TSource source, string name, TValue value)
        /// <summary>
        /// 设置单个字段的值，返回是否成功
        /// </summary>
        /// <param name="source">反射对象</param>
        /// <param name="name">字段名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetFieldValue<TSource, TValue>(this TSource source, string name, TValue value)
        {
            FieldInfo fieldInfo = source.GetField(name);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(source, value);
                return true;
            }
            else return false;
        }
        #endregion

        #region 类对象的深克隆，利用反射 + DeepClone<T>(this T source) where T : class, new()
        /// <summary>
        /// 类对象的深克隆，利用反射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T source) where T : class, new()
        {
            Type type = source.GetType();
            object newObject = Activator.CreateInstance(type);
            PropertyInfo[] infos = type.GetProperties();
            for (int i = 0; i < infos.Length; i++)
            {
                PropertyInfo info = infos[i];
#if NET40
                info.SetValue(newObject, info.GetValue(source, null), null);
#else
                info.SetValue(newObject, info.GetValue(source));
#endif
            }
            return (T)newObject;
        }
        #endregion

        #region 根据类的类型型创建类实例 + CreateInstance(this Type target)
        /// <summary>  
        /// 根据类的类型型创建类实例。  
        /// </summary>  
        /// <param name="target">将要创建的类型。</param>  
        /// <returns>返回创建的类实例。</returns>  
        public static object CreateInstance(this Type target) => Activator.CreateInstance(target);
        #endregion

        #region 判断类型是否为可空类型 + IsNullable(this Type type)
        /// <summary>
        /// 判断类型是否为可空类型
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <returns></returns>
        public static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        #endregion

        #region 判断类型是否为集合类型 + IsCollection(this Type type)
        /// <summary>
        /// 判断类型是否为集合类型
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <returns></returns>
        public static bool IsCollection(this Type type) => type.IsArray || type.GetInterfaces().Any(k => k == typeof(ICollection) || k == typeof(IEnumerable));
        #endregion
    }
}