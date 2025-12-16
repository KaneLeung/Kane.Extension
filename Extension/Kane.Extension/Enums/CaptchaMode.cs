// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------
#if NETFRAMEWORK
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
#endif