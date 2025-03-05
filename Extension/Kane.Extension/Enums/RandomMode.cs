// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.6
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace Kane.Extension
{
    /// <summary>
    /// 随机字符串包含类型枚举类
    /// </summary>
    [Flags]
    public enum RandomMode
    {
        /// <summary>
        /// 所有类型都包含
        /// </summary>
        [Description("全部")]
        All = 1,
        /// <summary>
        /// 大小写字母【a-zA-Z】
        /// </summary>
        [Description("字母")]
        Letter = 2,
        /// <summary>
        /// 数字【0123456789】
        /// </summary>
        [Description("数字")]
        Numeric = 4,
        /// <summary>
        /// 小写字母【a-z】
        /// </summary>
        [Description("小写字母")]
        Lowercase = 8,
        /// <summary>
        /// 大写字母【A-Z】
        /// </summary>
        [Description("大写字母")]
        Uppercase = 16,
        /// <summary>
        /// 标点符号【 ! @ # $ % ^ &amp; * ( ) _ + ~ ` | } { [ ] \ : ; ? > &lt; , . / - = 】
        /// </summary>
        [Description("标点符号")]
        Punctuation = 32
    }
}