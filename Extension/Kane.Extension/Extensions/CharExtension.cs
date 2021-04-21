// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.0
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;

namespace Kane.Extension
{
    /// <summary>
    /// 字符类扩展方法
    /// </summary>
    public static class CharExtension
    {
        #region 字符转成全角(SBC Case)的字符 + ToSBC(this char value)
        /// <summary>
        /// 字符转成全角(SBC Case)的字符
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static char ToSBC(this char value)
        {
            if (value == 32) value = (char)12288;
            if (value < 127) value = (char)(value + 65248);
            return value;
        }
        #endregion

        #region 字符转成半角(DBC Case)的字符 + ToDBC(this char value)
        /// <summary>
        /// 字符转成半角(DBC Case)的字符
        /// <para>全角空格为12288，半角空格为32</para>
        /// <para>其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</para>
        /// </summary>
        /// <param name="value">要转的字符串</param>
        /// <returns></returns>
        public static char ToDBC(this char value)
        {
            if (value == 12288) value = (char)32;
            if (value > 65280 && value < 65375) value = (char)(value - 65248);
            return value;
        }
        #endregion
    }
}