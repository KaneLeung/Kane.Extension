// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;

namespace Kane.Extension
{
    /// <summary>
    /// 范围类扩展方法
    /// </summary>
    public static class RangeExtension
    {
        #region 判断当前值是否在指定范围内，默认左右都是包含在内 + In<T>(this T value, T min, T max, bool leftContain = true, bool rightContain = true)
        /// <summary>
        /// 判断当前值是否在指定范围内，默认左右都是包含在内
        /// <para>补充知识点，（a,a） = [a,a) = (a,a] = Ø 【空集】</para>
        /// </summary>
        /// <typeparam name="T">要判断值的类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="min">比较的最小值</param>
        /// <param name="max">比较的最大值</param>
        /// <param name="leftContain">是否左包含【左闭区间】，默认为包含</param>
        /// <param name="rightContain">是否右包含【右闭区间】，默认为包含</param>
        /// <returns>是否在范围内</returns>
        public static bool In<T>(this T value, T min, T max, bool leftContain = true, bool rightContain = true) where T : struct, IComparable<T>
            => value.CompareTo(min) >= (leftContain ? 0 : 1) && value.CompareTo(max) <= (rightContain ? 0 : -1);
        #endregion

        #region 判断当前值【可空类型】是否在指定范围内，默认左右都是包含在内 + In<T>(this T? value, T min, T max, bool leftContain = true, bool rightContain = true)
        /// <summary>
        /// 判断当前值【可空类型】是否在指定范围内，默认左右都是包含在内
        /// <para>补充知识点，（a,a） = [a,a) = (a,a] = Ø 【空集】</para>
        /// </summary>
        /// <typeparam name="T">要判断值的类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="min">比较的最小值</param>
        /// <param name="max">比较的最大值</param>
        /// <param name="leftContain">是否左包含【左闭区间】，默认为包含</param>
        /// <param name="rightContain">是否右包含【右闭区间】，默认为包含</param>
        /// <returns>是否在范围内</returns>
        public static bool In<T>(this T? value, T min, T max, bool leftContain = true, bool rightContain = true) where T : struct, IComparable<T>
            => value.HasValue && value.Value.In(min, max, leftContain, rightContain);
        #endregion

        #region 判断当前值是否在指定范围内，如果是，则返回该值得，不是则返回预计值，默认左右都是包含在内 + In<T>(this T value, T min, T max, T returnValue, bool leftContain = true, bool rightContain = true)
        /// <summary>
        /// 判断当前值是否在指定范围内，如果是，则返回该值得，不是则返回预计值，默认左右都是包含在内
        /// <para>补充知识点，（a,a） = [a,a) = (a,a] = Ø 【空集】</para>
        /// </summary>
        /// <typeparam name="T">要判断值的类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="min">比较的最小值</param>
        /// <param name="max">比较的最大值</param>
        /// <param name="returnValue">不在范围内时返回的预计值</param>
        /// <param name="leftContain">是否左包含【左闭区间】，默认为包含</param>
        /// <param name="rightContain">是否右包含【右闭区间】，默认为包含</param>
        /// <returns></returns>
        public static T In<T>(this T value, T min, T max, T returnValue, bool leftContain = true, bool rightContain = true) where T : struct, IComparable<T>
            => value.In(min, max, leftContain, rightContain) ? value : returnValue;
        #endregion

        #region 判断当前值【可空类型】是否在指定范围内，如果是，则返回该值得，不是则返回预计值，默认左右都是包含在内 + In<T>(this T? value, T min, T max, T returnValue, bool leftContain = true, bool rightContain = true)
        /// <summary>
        /// 判断当前值【可空类型】是否在指定范围内，如果是，则返回该值得，不是则返回预计值，默认左右都是包含在内
        /// <para>补充知识点，（a,a） = [a,a) = (a,a] = Ø 【空集】</para>
        /// </summary>
        /// <typeparam name="T">要判断值的类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="min">比较的最小值</param>
        /// <param name="max">比较的最大值</param>
        /// <param name="returnValue">不在范围内或为空时返回的预计值</param>
        /// <param name="leftContain">是否左包含【左闭区间】，默认为包含</param>
        /// <param name="rightContain">是否右包含【右闭区间】，默认为包含</param>
        /// <returns></returns>
        public static T In<T>(this T? value, T min, T max, T returnValue, bool leftContain = true, bool rightContain = true) where T : struct, IComparable<T>
            => value.In(min, max, leftContain, rightContain) ? value!.Value : returnValue;
        #endregion

        #region 判断字符串长度是否在指定范围内 + InLength(this string value, int min, int max)
        /// <summary>
        /// 判断字符串长度是否在指定范围内
        /// </summary>
        /// <param name="value">要判断的字符串</param>
        /// <param name="min">最小长度</param>
        /// <param name="max">最大长度</param>
        /// <returns></returns>
        public static bool InLength(this string value, int min, int max) => value.Length >= min && value.Length <= max;
        #endregion
    }
}