#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：EnumEx
* 类 描 述 ：枚举类扩展类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:16:16
* 更新时间 ：2020/07/09 13:16:16
* 版 本 号 ：v1.0.4.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 枚举类扩展类
    /// </summary>
    public static class EnumEx
    {
        #region 获取枚举值的描述特性 + Description(this Enum item, bool inherit = false)
        /// <summary>
        /// 获取枚举值的描述特性，默认【不继承】
        /// </summary>
        /// <param name="item">该枚举的其中一个成员即可</param>
        /// <param name="inherit">是否继承</param>
        /// <returns></returns>
        public static string Description(this Enum item, bool inherit = false)
        {
            FieldInfo fieldInfo = item.GetType().GetField(item.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), inherit);
            return attribute?.Description ?? string.Empty;
        }
        #endregion

#if !NET40
        #region 通过描述获取Enum值 + FromDescription<T>(string description) where T : Enum
        /// <summary>
        /// 通过描述获取Enum值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="description">描述</param>
        public static T FromDescription<T>(string description) where T : Enum
        {
            if (description.IsNullOrEmpty()) throw new ArgumentNullException($"【{nameof(description)}】不能为空。");
            var type = typeof(T);
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Default);
            var fieldInfo = fieldInfos.FirstOrDefault(k => k.GetCustomAttribute<DescriptionAttribute>(false)?.Description == description);
            if (fieldInfo == null) throw new ArgumentNullException($"在枚举【{type.FullName}】中，未发现描述特性为【{description}】的枚举项。");
            return (T)Enum.Parse(type, fieldInfo.Name);
        }
        #endregion
#endif

        #region 将【Enum】转换为【EnumItem】List，默认【不继承】 + EnumToList<T>(this T _, bool inherit = false) where T : Enum
        /// <summary>
        /// 将【Enum】转换为【EnumItem】List，默认【不继承】
        /// </summary>
        /// <param name="_">【弃元】该枚举的其中一个成员即可</param>
        /// <param name="inherit">是否继承</param>
        /// <returns></returns>
        public static IList<EnumItem> EnumToList<T>(this T _, bool inherit = false) where T : Enum
        {
            var result = new List<EnumItem>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var temp = new EnumItem
                {
                    Key = item.ToString(),
                    Value = Convert.ToInt32(item),
                };
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(item.GetType().GetField(temp.Key), typeof(DescriptionAttribute), inherit);
                temp.Description = attribute?.Description ?? string.Empty;
                result.Add(temp);
            }
            return result;
        }
        #endregion

        #region 将字符串转为Enum成员，失败时返回Null，默认【忽略大小写】 + ToNEnum<T>(this string value, bool ignoreCase = true) where T : struct
        /// <summary>
        /// 将字符串转为Enum成员，失败时返回Null，默认【忽略大小写】
        /// </summary>
        /// <typeparam name="T">要转的枚举类型</typeparam>
        /// <param name="value">要转的字符串</param>
        /// <param name="ignoreCase">是否忽略大小写，默认【是】</param>
        /// <returns></returns>
        public static T? ToNEnum<T>(this string value, bool ignoreCase = true) where T : struct
        {
            if (value.IsValuable() && Enum.TryParse(value, ignoreCase, out T result)) return result;
            else return null;
        }
        #endregion

        #region 将字符串转为Enum成员，可设置失败返回值，默认【忽略大小写】 + ToEnum<T>(this string value, T returnValue = default, bool ignoreCase = true)
        /// <summary>
        /// 将字符串转为Enum成员，可设置失败返回值，默认【忽略大小写】
        /// <para>如果不设置返回值，失败后返回【default】</para>
        /// </summary>
        /// <typeparam name="T">要转的枚举类型</typeparam>
        /// <param name="value">要转的字符串</param>
        /// <param name="returnValue">失败返回值</param>
        /// <param name="ignoreCase">是否忽略大小写，默认【是】</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T returnValue = default, bool ignoreCase = true) where T : struct
        {
            if (value.IsValuable() && Enum.TryParse(value, ignoreCase, out T result)) return result;
            else return returnValue;
        }
        #endregion
    }
}