// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Globalization;

namespace Kane.Extension;
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

    #region Decimal转成字符串，并去掉小数点后面的0 + ToStringTrimEndZero(this decimal value)
    /// <summary>
    /// Decimal转成字符串，并去掉小数点后面的0
    /// <para>https://learn.microsoft.com/zh-cn/dotnet/standard/base-types/standard-numeric-format-strings#GFormatString</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToStringTrimEndZero(this decimal value)
        => value.ToString("G29", CultureInfo.InvariantCulture);
    #endregion

    #region Double转成字符串，并去掉小数点后面的0 + ToStringTrimEndZero(this double value)
    /// <summary>
    /// Double转成字符串，并去掉小数点后面的0
    /// <para>https://learn.microsoft.com/zh-cn/dotnet/standard/base-types/standard-numeric-format-strings#GFormatString</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToStringTrimEndZero(this double value)
        => value.ToString("G17", CultureInfo.InvariantCulture);
    #endregion

    #region Float转成字符串，并去掉小数点后面的0 + ToStringTrimEndZero(this float value)
    /// <summary>
    /// Float转成字符串，并去掉小数点后面的0
    /// <para>https://learn.microsoft.com/zh-cn/dotnet/standard/base-types/standard-numeric-format-strings#GFormatString</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToStringTrimEndZero(this float value)
        => value.ToString("G7", CultureInfo.InvariantCulture);
    #endregion
}
