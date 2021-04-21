// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.0
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

#if !NET40
using System;
using System.Reflection;

namespace Kane.Extension
{
    /// <summary>
    /// 特性类扩展方法
    /// </summary>
    public static class AttributeExtension
    {
        #region 是否有指定特性 + HasAttribute<T>(this Type type, bool inherit = false)
        /// <summary>
        /// 是否有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="inherit">是否允许继承链搜索</param>
        public static bool HasAttribute<T>(this Type type, bool inherit = false) where T : Attribute => type.GetTypeInfo().IsDefined(typeof(T), inherit);
        #endregion

        #region 是否有指定特性 + HasAttribute<T>(this Type type, bool inherit = false)
        /// <summary>
        /// 是否有指定特性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="_">【弃元】对象</param>
        /// <param name="inherit">是否允许继承链搜索</param>
        /// <returns></returns>
        public static bool HasAttribute<T, TAttribute>(this T _, bool inherit = false) where TAttribute : Attribute where T : class
            => typeof(T).GetTypeInfo().IsDefined(typeof(TAttribute), inherit);
        #endregion
    }
}
#endif