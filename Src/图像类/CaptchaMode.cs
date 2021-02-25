#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：CaptchaMode
* 类 描 述 ：生成验证码模式
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2021/02/25 16:35:18
* 更新时间 ：2021/02/25 16:35:18
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2021. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;

namespace Kane.Extension
{
    /// <summary>
    /// 验证码模式
    /// </summary>
    [Flags]
    public enum CaptchaMode
    {
        /// <summary>
        /// 所有类型都包含
        /// </summary>
        All = 1,
        /// <summary>
        /// 字母
        /// </summary>
        Letter = 2,
        /// <summary>
        /// 数字
        /// </summary>
        Numeric = 4,
        /// <summary>
        /// 数字+字母
        /// </summary>
        LetterAndNumeric = 8,
        /// <summary>
        /// 简单四则运算
        /// </summary>
        Arithmetic = 16,
        /// <summary>
        /// 简体中文
        /// </summary>
        Chinese = 32,
        /// <summary>
        /// 题库，需要自行手动添加
        /// </summary>
        QuestionBank = 64,
    }
}