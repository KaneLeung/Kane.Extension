#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：RandomMode
* 类 描 述 ：随机字符串枚举类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/24 23:09:25
* 更新时间 ：2021/02/22 14:09:23
* 版 本 号 ：v1.0.2.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
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