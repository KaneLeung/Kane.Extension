#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：DateTimeOffsetEx
* 类 描 述 ：时间类扩展类
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2021/01/30 16:37:28
* 更新时间 ：2021/01/30 16:37:28
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Globalization;

namespace Kane.Extension
{
    /// <summary>
    /// 时间类扩展类
    /// </summary>
    public static class DateTimeOffsetEx
    {
        #region 将DateTimeOffset转成当天起始时间 + DayStart(this DateTimeOffset datetime)
        /// <summary>
        /// 将DateTimeOffset转成当天起始时间
        /// </summary>
        /// <param name="datetime">要转的日期</param>
        /// <returns></returns>
        public static DateTimeOffset DayStart(this DateTimeOffset datetime)
            => new DateTimeOffset(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0, datetime.Offset);
        #endregion

        #region 将DateTimeOffset转成下一天的开始时间 + NextDayStart(this DateTimeOffset datetime)
        /// <summary>
        /// 将DateTimeOffset转成下一天的开始时间
        /// </summary>
        /// <param name="datetime">要转的日期</param>
        /// <returns></returns>
        public static DateTimeOffset NextDayStart(this DateTimeOffset datetime) => datetime.DayStart().AddDays(1);
        #endregion

        #region 将DateTimeOffset转成上一天的开始时间 + LastDayStart(this DateTimeOffset datetime)
        /// <summary>
        /// 将DateTimeOffset转成上一天的开始时间
        /// </summary>
        /// <param name="datetime">要转的日期</param>
        /// <returns></returns>
        public static DateTimeOffset LastDayStart(this DateTimeOffset datetime) => datetime.DayStart().AddDays(-1);
        #endregion

        #region 将DateTimeOffset转成当月初时间 + MonthStart(this DateTimeOffset datetime)
        /// <summary>
        /// 将DateTimeOffset转成当月初时间
        /// </summary>
        /// <param name="datetime">要转的时间点</param>
        /// <returns></returns>
        public static DateTimeOffset MonthStart(this DateTimeOffset datetime)
            => new DateTimeOffset(datetime.Year, datetime.Month, 1, 0, 0, 0, datetime.Offset);
        #endregion

        #region 将DateTimeOffset转成下个月初的开始时间 + NextMonthStart(this DateTimeOffset datetime)
        /// <summary>
        /// 将DateTimeOffset转成下个月初的开始时间
        /// </summary>
        /// <param name="datetime">要转的时间点</param>
        /// <returns></returns>
        public static DateTimeOffset NextMonthStart(this DateTimeOffset datetime) => datetime.MonthStart().AddMonths(1);
        #endregion

        #region 将DateTimeOffset转成上个月初的开始时间 + LastMonthStart(this DateTimeOffset datetime)
        /// <summary>
        /// 将DateTimeOffset转成上个月初的开始时间
        /// </summary>
        /// <param name="datetime">要转的时间点</param>
        /// <returns></returns>
        public static DateTimeOffset LastMonthStart(this DateTimeOffset datetime) => datetime.MonthStart().AddMonths(-1);
        #endregion

        #region 获取今天时间段，通常常用 Start ≥ X ＜ End + GetToday()
        /// <summary>
        /// 获取今天时间段，通常常用 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetToday()
        {
            var start = DateTimeOffset.Now.DayStart();
            return (start, start.AddDays(1));
        }
        #endregion

        #region 获取昨天时间段，通常用法 Start ≥ X ＜ End + GetYesterday()
        /// <summary>
        /// 获取昨天时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetYesterday()
        {
            var end = DateTimeOffset.Now.DayStart();
            return (end.AddDays(-1), end);
        }
        #endregion

        #region 获取明天时间段，通常用法 Start ≥ X ＜ End + GetTomorrow()
        /// <summary>
        /// 获取明天时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetTomorrow()
        {
            var today = DateTimeOffset.Now.DayStart();
            return (today.AddDays(1), today.AddDays(2));
        }
        #endregion

        #region 获取本周时间段，通常用法 Start ≥ X < End + GetThisWeek()
        /// <summary>
        /// 获取本周时间段，通常用法 Start ≥ X ＜ End
        /// <para>中国人习惯星期一为星期开始，因为星期日为0，所以要减七</para>
        /// </summary>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetThisWeek()
        {
            var dayOfWeek = (int)DateTimeOffset.Now.DayOfWeek;
            var start = DateTimeOffset.Now.DayStart().AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek));
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取某一周时间段，通常用法 Start ≥ X ＜ End + GetOneWeek(this DateTimeOffset datetime)
        /// <summary>
        /// 获取本周时间段，通常用法 Start ≥ X ＜ End
        /// <para>中国人习惯星期一为星期开始，因为星期日为0，所以要减七</para>
        /// </summary>
        /// <param name="datetime">要获取的那一周其中一个时间</param>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetOneWeek(this DateTimeOffset datetime)
        {
            var dayOfWeek = (int)datetime.DayOfWeek;
            var start = datetime.AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek)).DayStart();
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取本月时间段，通常用法 Start ≥ X ＜ End + GetThisMonth()
        /// <summary>
        /// 获取本月时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetThisMonth()
        {
            var start = DateTimeOffset.Now.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 获取某一月时间段，通常用法 Start ≥ X ＜ End + GetOneMonth(DateTimeOffset datetime)
        /// <summary>
        /// 获取某一月时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <param name="datetime">要获取的那一月的其中一个时间</param>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetOneMonth(this DateTimeOffset datetime)
        {
            var start = datetime.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 获取当前季度，通常用法 Start ≥ X ＜ End + GetThisQuarter()
        /// <summary>
        /// 获取当前季度，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetThisQuarter()
        {
            DateTimeOffset start;
            var now = DateTimeOffset.Now;
            if (DateTimeOffset.Now.Month <= 3) start = new DateTimeOffset(now.Year, 1, 1, 0, 0, 0, now.Offset);
            else if (DateTimeOffset.Now.Month <= 6) start = new DateTimeOffset(now.Year, 4, 1, 0, 0, 0, now.Offset);
            else if (DateTimeOffset.Now.Month <= 9) start = new DateTimeOffset(now.Year, 7, 1, 0, 0, 0, now.Offset);
            else start = new DateTimeOffset(DateTimeOffset.Now.Year, 10, 1, 0, 0, 0, now.Offset);
            return (start, start.AddMonths(3));
        }
        #endregion

        #region 时间戳转为DateTimeOffset，默认为【秒级】 + StampToDTO(this long timeStamp, bool isMillisecond = false)
        /// <summary>
        /// 时间戳转为DateTimeOffset，默认为【秒级】
        /// <para>要到 2286/11/21 01:46:40 才会变成11位（10000000000）</para>
        /// <para>int范围 -2,147,483,648 到 2,147,483,647</para>
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="isMillisecond">是否为毫秒级</param>
        /// <returns></returns>
        public static DateTimeOffset StampToDTO(this long timeStamp, bool isMillisecond = false)
        {
#if NET40 || NET45
            long ticks = timeStamp * (isMillisecond ? TimeSpan.TicksPerMillisecond : TimeSpan.TicksPerSecond) + new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;
            return new DateTimeOffset(ticks, TimeSpan.Zero).ToLocalTime();
#else
            return isMillisecond ? DateTimeOffset.FromUnixTimeMilliseconds(timeStamp).ToLocalTime() : DateTimeOffset.FromUnixTimeSeconds(timeStamp).ToLocalTime();
#endif
        }
        #endregion

        #region 时间戳转为DateTimeOffset，默认为【秒级】 + StampToDTO(string timeStamp, bool isMillisecond = false)
        /// <summary>
        /// 时间戳转为DateTimeOffset，默认为【秒级】
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="isMillisecond">是否为毫秒级</param>
        /// <returns></returns>
        public static DateTimeOffset StampToDTO(string timeStamp, bool isMillisecond = false)
        {
            if (long.TryParse(string.Concat(timeStamp, isMillisecond ? "0000" : "0000000"), out long stamp)) return StampToDTO(stamp, isMillisecond);
            else throw new FormatException("时间戳格式有误");
        }
        #endregion

        #region 获取某个时间的一个季度，通常用法 Start ≥ X ＜ End + GetOneQuarter(this DateTimeOffset dateTime)
        /// <summary>
        /// 获取某个时间的一个季度，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTimeOffset Start, DateTimeOffset End) GetOneQuarter(this DateTimeOffset datetime)
        {
            DateTimeOffset start;
            var now = DateTimeOffset.Now;
            if (datetime.Month <= 3) start = new DateTimeOffset(datetime.Year, 1, 1, 0, 0, 0, now.Offset);
            else if (datetime.Month <= 6) start = new DateTimeOffset(datetime.Year, 4, 1, 0, 0, 0, now.Offset);
            else if (datetime.Month <= 9) start = new DateTimeOffset(datetime.Year, 7, 1, 0, 0, 0, now.Offset);
            else start = new DateTimeOffset(datetime.Year, 10, 1, 0, 0, 0, now.Offset);
            return (start, start.AddMonths(3));
        }
        #endregion

        #region DateTimeOffset时间格式转换为Unix秒级时间戳 + ToStamp(this DateTimeOffset datetime)
        /// <summary>
        /// DateTimeOffset时间格式转换为Unix秒级时间戳，返回<see cref="int"/>格式
        /// <para>用Int最大值是2038年01月19日03时14分07秒，超过可用<see cref="ToLongStamp(DateTimeOffset)"/></para>
        /// </summary>
        /// <param name="datetime">要转换的时间</param>
        /// <returns>Int格式</returns>
        public static int ToStamp(this DateTimeOffset datetime)
        {
#if NET40 || NET45
            var startTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            return (int)(datetime - startTime).TotalSeconds;
#else
            return (int)datetime.ToUnixTimeSeconds();
#endif
        }
        #endregion

        #region DateTimeOffset时间格式转换为Unix秒级时间戳 + ToLongStamp(this DateTimeOffset datetime)
        /// <summary>
        /// DateTimeOffset时间格式转换为Unix秒级时间戳，返回<see cref="long"/>格式
        /// </summary>
        /// <param name="datetime">要转换的时间</param>
        /// <returns>Long格式</returns>
        public static long ToLongStamp(this DateTimeOffset datetime)
        {
#if NET40 || NET45
            var startTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, new TimeSpan(0, 0, 0));
            return (long)(datetime - startTime).TotalSeconds;
#else
            return datetime.ToUnixTimeSeconds();
#endif
        }
        #endregion

        #region DateTimeOffset时间格式转换为Unix毫秒级时间戳 + ToMillisecondStamp(this DateTimeOffset datetime)
        /// <summary>
        /// DateTimeOffset时间格式转换为Unix毫秒级时间戳，返回<see cref="long"/>格式
        /// </summary>
        /// <param name="datetime">要转换的时间</param>
        /// <returns></returns>
        public static long ToMillisecondStamp(this DateTimeOffset datetime)
        {
#if NET40 || NET45
            var startTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, new TimeSpan(0, 0, 0));
            return (long)(datetime - startTime).TotalMilliseconds;
#else
            return datetime.ToUnixTimeMilliseconds();
#endif
        }
        #endregion

        #region 以当前时间为基准，计算时间差 + TimeDiff(this DateTimeOffset datetime)
        /// <summary>
        /// 以当前时间为基准，计算时间差
        /// </summary>
        /// <param name="datetime">要计算的时间</param>
        /// <returns></returns>
        public static string TimeDiff(this DateTimeOffset datetime) => datetime.TimeDiff(DateTimeOffset.Now);
        #endregion

        #region 计算两个时间差 + TimeDiff(this DateTimeOffset datetime, DateTimeOffset point)
        /// <summary>
        /// 计算两个时间差
        /// </summary>
        /// <param name="datetime">要计算的时间</param>
        /// <param name="point">时间基准点</param>
        /// <returns></returns>
        public static string TimeDiff(this DateTimeOffset datetime, DateTimeOffset point)
        {
            TimeSpan timeSpan = datetime - point;
            var tag = timeSpan > TimeSpan.Parse("0") ? "后" : "前";
            var days = timeSpan.Days;
            if (days != 0)
            {
                days = days < 0 ? days * -1 : days;
                if (days >= 365) return $"{days / 365}年{tag}";
                else if (days >= 30) return $"{days / 30}个月{tag}";
                else if (days >= 7) return $"{days / 7}周{tag}";
                else return $"{days}天{tag}";
            }
            if (timeSpan.Hours != 0) return $"{(timeSpan.Hours < 0 ? timeSpan.Hours * -1 : timeSpan.Hours)}小时{tag}";
            if (timeSpan.Minutes != 0) return $"{(timeSpan.Minutes < 0 ? timeSpan.Minutes * -1 : timeSpan.Minutes)}分钟{tag}";
            return $"{(timeSpan.Seconds < 0 ? timeSpan.Seconds * -1 : timeSpan.Seconds)}秒{tag}";
        }
        #endregion

        #region 计算周岁年龄 + CalculateAge(this DateTimeOffset dateOfBirth)
        /// <summary>
        /// 计算周岁年龄
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        public static int CalculateAge(this DateTimeOffset dateOfBirth) => CalculateAge(dateOfBirth, DateTimeOffset.Now.Date);
        #endregion

        #region 计算周岁年龄，指定参考日期 + CalculateAge(this DateTimeOffset dateOfBirth, DateTimeOffset point)
        /// <summary>
        /// 计算周岁年龄，指定参考日期
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <param name="point">时间基准点</param>
        public static int CalculateAge(this DateTimeOffset dateOfBirth, DateTimeOffset point)
        {
            var age = point.Year - dateOfBirth.Year;
            if (point.Month < dateOfBirth.Month || (point.Month == dateOfBirth.Month && point.Day < dateOfBirth.Day)) --age;
            return age;
        }
        #endregion

        #region 根据当前时间获取当前是第几周 + WeekIndex(this DateTimeOffset datetime, bool crossover = false)
        /// <summary>
        /// 根据当前时间获取当前是第几周
        /// </summary>
        /// <param name="datetime">当前时间</param>
        /// <param name="crossover">开启【交叉年】：像2016年12月31号与2017年1月1号刚好在同一星期，【开启】交叉年后，则12月31号为第【1】周，否则为【53】周</param>
        /// <returns></returns>
        public static int WeekIndex(this DateTimeOffset datetime, bool crossover = false)
        {
            int dayOfYear = datetime.DayOfYear;//求出此时间在一年中的位置
            int dayOfWeek = (int)new DateTimeOffset(datetime.Year, 1, 1, 0, 0, 0, datetime.Offset).DayOfWeek;//当年第一天的星期几
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;//当年第一天是星期几，中国人习惯星期一为星期开始，因为星期日为0，所以为7
            var (Start, End) = datetime.GetOneWeek();
            int index = (int)Math.Ceiling(((double)dayOfYear + dayOfWeek - 1) / 7);//确定当前是第几周
            if (crossover && Start.Year < End.Year) index = 1;//判断是否开启交叉年
            return index;
        }
        #endregion

        #region 获取当前的年最大周数 + MaxWeekIndex(this DateTimeOffset datetime)
        /// <summary>
        /// 获取当前的年最大周数
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int MaxWeekIndex(this DateTimeOffset datetime)
            => WeekIndex(new DateTimeOffset(datetime.Year, 12, 31, 0, 0, 0, datetime.Offset), false);
        #endregion

        #region 将日期时间转成常用的日期时间格式字符串，默认不移除【秒】 + ToString(this DateTimeOffset datetime, DateTimeFormat format, bool removeSecond = false)
        /// <summary>
        /// 将日期时间转成常用的日期时间格式字符串，默认不移除【秒】
        /// <para>https://docs.microsoft.com/zh-cn/dotnet/api/system.globalization.cultureinfo.createspecificculture?view=netcore-3.1</para>
        /// </summary>
        /// <param name="datetime">要转的日期时间</param>
        /// <param name="format">格式枚举类</param>
        /// <param name="removeSecond">是否移除【秒】</param>
        /// <returns></returns>
        public static string ToString(this DateTimeOffset datetime, DateTimeFormat format, bool removeSecond = false) => format switch
        {
            DateTimeFormat.Long => datetime.ToString(removeSecond ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd HH:mm:ss"),
            DateTimeFormat.Short => datetime.ToString(removeSecond ? "yyyy-M-d H:m" : "yyyy-M-d H:m:s"),
            DateTimeFormat.LongDTWeek => datetime.ToString(removeSecond ? "yyyy年MM月dd日 HH:mm dddd" : "yyyy年MM月dd日 HH:mm:ss dddd", new CultureInfo("zh-CN")),
            DateTimeFormat.ShortDTWeek => datetime.ToString(removeSecond ? "yyyy年M月d日 H:m ddd" : "yyyy年M月d日 H:m:s ddd", new CultureInfo("zh-CN")),
            DateTimeFormat.LongDTShortWeek => datetime.ToString(removeSecond ? "yyyy年MM月dd日 HH:mm ddd" : "yyyy年MM月dd日 HH:mm:ss ddd", new CultureInfo("zh-CN")),
            DateTimeFormat.ShortDTLongWeek => datetime.ToString(removeSecond ? "yyyy年M月d日 H:m dddd" : "yyyy年M月d日 H:m:s dddd", new CultureInfo("zh-CN")),
            DateTimeFormat.LongDateWeek => datetime.ToString("yyyy年MM月dd日 dddd", new CultureInfo("zh-CN")),
            DateTimeFormat.ShortDateWeek => datetime.ToString("yyyy年M月d日 ddd", new CultureInfo("zh-CN")),
            DateTimeFormat.LongDateShortWeek => datetime.ToString("yyyy年MM月dd日 ddd", new CultureInfo("zh-CN")),
            DateTimeFormat.ShortDateLongWeek => datetime.ToString("yyyy年M月d日 ddddd", new CultureInfo("zh-CN")),
            DateTimeFormat.LongDT => datetime.ToString(removeSecond ? "yyyy年MM月dd日 HH:mm" : "yyyy年MM月dd日 HH:mm:ss"),
            DateTimeFormat.ShortDT => datetime.ToString(removeSecond ? "yyyy年M月d日 H:m" : "yyyy年M月d日 H:m:s"),
            DateTimeFormat.LongDate => datetime.ToString("yyyy年MM月dd日"),
            DateTimeFormat.ShortDate => datetime.ToString("yyyy年M月d日"),
            DateTimeFormat.LongTime => datetime.ToString(removeSecond ? "HH:mm" : "HH:mm:ss"),
            DateTimeFormat.ShortTime => datetime.ToString(removeSecond ? "H:m" : "H:m:s"),
            DateTimeFormat.LongWeek => datetime.ToString("dddd", new CultureInfo("zh-CN")),
            DateTimeFormat.ShortWeek => datetime.ToString("ddd", new CultureInfo("zh-CN")),
            _ => datetime.ToString(),//05/16/2020 10:38:18
        };
        #endregion
    }
}