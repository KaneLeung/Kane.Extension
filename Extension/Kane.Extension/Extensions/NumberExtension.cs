// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.0
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;

namespace Kane.Extension.Extensions;
/// <summary>
/// 数字类扩展方法
/// </summary>
public static class NumberExtension
{
    #region Double默认保留两位小数，默认【采用4舍6入5取偶】 + ToRound(...)
    /// <summary>
    /// Double默认保留两位小数，默认【采用4舍6入5取偶】
    /// <para>采用Banker's rounding（银行家算法），即：四舍六入五取偶。事实上这也是IEEE的规范。</para>
    /// <para>备注：<see cref="MidpointRounding.AwayFromZero"/>可以用来实现传统意义上的"四舍五入"。</para>
    /// </summary>
    /// <param name="value">要转的值</param>
    /// <param name="digits">保留的小数位数</param>
    /// <param name="mode">可选择模式</param>
    /// <returns></returns>
    public static double ToRound(this double value, int digits = 2, MidpointRounding mode = MidpointRounding.ToEven)
        => Math.Round(value, digits, mode);
    #endregion

    #region Double默认保留两位小数，默认【采用4舍6入5取偶】 + ToRound(...)
    /// <summary>
    /// Double默认保留两位小数，默认【采用4舍6入5取偶】
    /// <para>采用Banker's rounding（银行家算法），即：四舍六入五取偶。事实上这也是IEEE的规范。</para>
    /// <para>备注：<see cref="MidpointRounding.AwayFromZero"/>可以用来实现传统意义上的"四舍五入"。</para>
    /// </summary>
    /// <param name="value">要转的值</param>
    /// <param name="digits">保留的小数位数</param>
    /// <param name="mode">可选择模式</param>
    /// <returns></returns>
    public static double? ToRound(this double? value, int digits = 2, MidpointRounding mode = MidpointRounding.ToEven)
        => value.HasValue ? Math.Round(value.Value, digits, mode) : value;
    #endregion

    #region Decimal默认保留两位小数，默认【采用4舍6入5取偶】 + ToRound(...)
    /// <summary>
    /// Decimal默认保留两位小数，默认【采用4舍6入5取偶】
    /// <para>采用Banker's rounding（银行家算法），即：四舍六入五取偶。事实上这也是IEEE的规范。</para>
    /// <para>备注：<see cref="MidpointRounding.AwayFromZero"/>可以用来实现传统意义上的"四舍五入"。</para>
    /// </summary>
    /// <param name="value">要转的值</param>
    /// <param name="digits">保留的小数位数</param>
    /// <param name="mode">可选择模式</param>
    /// <returns></returns>
    public static decimal ToRound(this decimal value, int digits = 2, MidpointRounding mode = MidpointRounding.ToEven)
        => Math.Round(value, digits, mode);
    #endregion

    #region Decimal默认保留两位小数，默认【采用4舍6入5取偶】 + ToRound(...)
    /// <summary>
    /// Decimal默认保留两位小数，默认【采用4舍6入5取偶】
    /// <para>采用Banker's rounding（银行家算法），即：四舍六入五取偶。事实上这也是IEEE的规范。</para>
    /// <para>备注：<see cref="MidpointRounding.AwayFromZero"/>可以用来实现传统意义上的"四舍五入"。</para>
    /// </summary>
    /// <param name="value">要转的值</param>
    /// <param name="digits">保留的小数位数</param>
    /// <param name="mode">可选择模式</param>
    /// <returns></returns>
    public static decimal? ToRound(this decimal? value, int digits = 2, MidpointRounding mode = MidpointRounding.ToEven)
        => value.HasValue ? Math.Round(value.Value, digits, mode) : value;
    #endregion
}
