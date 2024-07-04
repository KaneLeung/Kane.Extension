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
    /// 日期时间类扩展方法
    /// </summary>
    public static class DateTimeExtension
    {
        #region 将DateTime转成当天起始时间 + DayStart(this DateTime datetime)
        /// <summary>
        /// 将DateTime转成当天起始时间
        /// </summary>
        /// <param name="datetime">要转的日期</param>
        /// <returns></returns>
        public static DateTime DayStart(this DateTime datetime) => new DateTime(datetime.Year, datetime.Month, datetime.Day);
        #endregion

        #region 将DateTime转成下一天的开始时间 + NextDayStart(this DateTime datetime)
        /// <summary>
        /// 将DateTime转成下一天的开始时间
        /// </summary>
        /// <param name="datetime">要转的日期</param>
        /// <returns></returns>
        public static DateTime NextDayStart(this DateTime datetime) => datetime.AddDays(1).DayStart();
        #endregion

        #region 将DateTime转成前一天的开始时间 + LastDayStart(this DateTime datetime)
        /// <summary>
        /// 将DateTime转成前一天的开始时间
        /// </summary>
        /// <param name="datetime">要转的日期</param>
        /// <returns></returns>
        public static DateTime LastDayStart(this DateTime datetime) => datetime.AddDays(-1).DayStart();
        #endregion

        #region 将DateTime转成当月初时间 + MonthStart(this DateTime datetime)
        /// <summary>
        /// 将DateTime转成当月初时间
        /// </summary>
        /// <param name="datetime">要转的时间点</param>
        /// <returns></returns>
        public static DateTime MonthStart(this DateTime datetime) => new DateTime(datetime.Year, datetime.Month, 1);
        #endregion

        #region 将DateTime转成下个月初的开始时间 + NextMonthStart(this DateTime datetime)
        /// <summary>
        /// 将DateTime转成下个月初的开始时间
        /// </summary>
        /// <param name="datetime">要转的时间点</param>
        /// <returns></returns>
        public static DateTime NextMonthStart(this DateTime datetime) => datetime.AddMonths(1).MonthStart();
        #endregion

        #region 将DateTime转成上个月初的开始时间 + LastMonthStart(this DateTime datetime)
        /// <summary>
        /// 将DateTime转成上个月初的开始时间
        /// </summary>
        /// <param name="datetime">要转的时间点</param>
        /// <returns></returns>
        public static DateTime LastMonthStart(this DateTime datetime) => datetime.AddMonths(-1).MonthStart();
        #endregion

        #region 获取今天时间段，通常常用 Start ≥ X ＜ End + Today()
        /// <summary>
        /// 获取今天时间段，通常常用 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) Today()
        {
            var start = DateTime.Today;
            return (start, start.AddDays(1));
        }
        #endregion

        #region 获取昨天时间段，通常用法 Start ≥ X ＜ End + Yesterday()
        /// <summary>
        /// 获取昨天时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) Yesterday()
        {
            var end = DateTime.Today;
            return (end.AddDays(-1), end);
        }
        #endregion

        #region 获取明天时间段，通常用法 Start ≥ X ＜ End + Tomorrow()
        /// <summary>
        /// 获取明天时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) Tomorrow()
        {
            var today = DateTime.Today;
            return (today.AddDays(1), today.AddDays(2));
        }
        #endregion

        #region 获取本周时间段，通常用法 Start ≥ X < End + ThisWeek()
        /// <summary>
        /// 获取本周时间段，通常用法 Start ≥ X ＜ End
        /// <para>中国人习惯星期一为星期开始，因为星期日为0，所以要减七</para>
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) ThisWeek()
        {
            var dayOfWeek = (int)DateTime.Today.DayOfWeek;
            var start = DateTime.Today.AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek));
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取某一周时间段，通常用法 Start ≥ X ＜ End + OneWeek(this DateTime datetime)
        /// <summary>
        /// 获取本周时间段，通常用法 Start ≥ X ＜ End
        /// <para>中国人习惯星期一为星期开始，因为星期日为0，所以要减七</para>
        /// </summary>
        /// <param name="datetime">要获取的那一周其中一个时间</param>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) OneWeek(this DateTime datetime)
        {
            var dayOfWeek = (int)datetime.DayOfWeek;
            var start = datetime.AddDays(1 - (dayOfWeek == 0 ? 7 : dayOfWeek)).DayStart();
            return (start, start.AddDays(7));
        }
        #endregion

        #region 获取本月时间段，通常用法 Start ≥ X ＜ End + ThisMonth()
        /// <summary>
        /// 获取本月时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) ThisMonth()
        {
            var start = DateTime.Today.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 获取某一月时间段，通常用法 Start ≥ X ＜ End + OneMonth(this DateTime datetime)
        /// <summary>
        /// 获取某一月时间段，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <param name="datetime">要获取的那一月的其中一个时间</param>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) OneMonth(this DateTime datetime)
        {
            var start = datetime.MonthStart();
            return (start, start.AddMonths(1));
        }
        #endregion

        #region 获取当前季度，通常用法 Start ≥ X ＜ End + ThisQuarter()
        /// <summary>
        /// 获取当前季度，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) ThisQuarter()
        {
            DateTime start;
            if (DateTime.Today.Month <= 3) start = new DateTime(DateTime.Today.Year, 1, 1);
            else if (DateTime.Today.Month <= 6) start = new DateTime(DateTime.Today.Year, 4, 1);
            else if (DateTime.Today.Month <= 9) start = new DateTime(DateTime.Today.Year, 7, 1);
            else start = new DateTime(DateTime.Today.Year, 10, 1);
            return (start, start.AddMonths(3));
        }
        #endregion

        #region 获取某个时间的一个季度，通常用法 Start ≥ X ＜ End + OneQuarter(this DateTime datetime)
        /// <summary>
        /// 获取某个时间的一个季度，通常用法 Start ≥ X ＜ End
        /// </summary>
        /// <returns></returns>
        public static (DateTime Start, DateTime End) OneQuarter(this DateTime datetime)
        {
            DateTime start;
            if (datetime.Month <= 3) start = new DateTime(datetime.Year, 1, 1);
            else if (datetime.Month <= 6) start = new DateTime(datetime.Year, 4, 1);
            else if (datetime.Month <= 9) start = new DateTime(datetime.Year, 7, 1);
            else start = new DateTime(datetime.Year, 10, 1);
            return (start, start.AddMonths(3));
        }
        #endregion

        #region 将秒转换成时长字符串，包含天、时、分、秒 + Duration(int seconds)
        /// <summary>
        /// 将秒转换成时长字符串
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string Duration(int seconds)
        {
            int minute = seconds / 60;
            seconds %= 60;
            int hour = minute / 60;
            minute %= 60;
            int day = hour / 24;
            hour %= 24;
            return string.Format("{0}天{1:00}小时{2:00}分{3:00}秒", day, hour, minute, seconds);
        }
        #endregion

        #region 将秒转换成时长字符串,没有天数，只有时、分、秒 + DurationNoDays(int seconds)
        /// <summary>
        /// 将秒转换成时长字符串,没有天数，只有时分秒
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string DurationNoDays(int seconds)
        {
            int minute = seconds / 60;
            seconds %= 60;
            int hour = minute / 60;
            minute %= 60;
            return string.Format("{0:00}小时{1:00}分{2:00}秒", hour, minute, seconds);
        }
        #endregion

#if NET40 || NET45
        #region 获取时间戳，可增加或减少【秒】 + TimeStamp(int seconds = 0)
        /// <summary>  
        /// 获取时间戳，可增加或减少【秒】
        /// <para>时间戳, 又叫Unix Stamp. 从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒。</para>
        /// </summary>  
        /// <param name="seconds">增加或减少【秒】</param>
        /// <returns></returns>  
        public static long TimeStamp(int seconds = 0)
        {
            var timeSpan = DateTime.UtcNow.AddSeconds(seconds) - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }
        #endregion

        #region 获取毫秒级时间戳，可增加或减少【秒】 + MillisecondTimeStamp(int seconds = 0)
        /// <summary>
        /// 获取毫秒级时间戳，可增加或减少【秒】
        /// <para>时间戳, 又叫Unix Stamp. 从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒。</para>
        /// </summary>
        /// <param name="seconds">增加或减少【秒】</param>
        /// <returns></returns>
        public static long MillisecondTimeStamp(int seconds = 0)
        {
            var timeSpan = DateTime.UtcNow.AddSeconds(seconds) - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(timeSpan.TotalMilliseconds);
        }
        #endregion
#endif

        #region 时间戳转为【当地时区】的DateTime，默认为【秒级】 + TimeStampToLocal(this long timeStamp, bool isSecond = true)
        /// <summary>
        /// 时间戳转为【当地时区】的DateTime，默认为【秒级】
        /// <para>要到 2286/11/21 01:46:40 才会变成11位（10000000000）</para>
        /// <para>int范围 -2,147,483,648 到 2,147,483,647</para>
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="isSecond">是否为秒级，否则为毫秒级</param>
        /// <returns></returns>
        public static DateTime TimeStampToLocal(this long timeStamp, bool isSecond = true)
        {
            timeStamp *= isSecond ? 10000000 : 10000;
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);//621355968000000000
            return startTime.Add(new TimeSpan(timeStamp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒，1Ticks = 100ns
        }
        #endregion

        #region 时间戳转为【当地时区】的DateTime，默认为【秒级】 + TimeStampToLocal(string timeStamp, bool isSecond = true)
        /// <summary>
        /// 时间戳转为【当地时区】的DateTime，默认为【秒级】
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="isSecond">是否为秒级，否则为毫秒级</param>
        /// <returns></returns>
        public static DateTime TimeStampToLocal(string timeStamp, bool isSecond = true)
        {
            if (long.TryParse(string.Concat(timeStamp, isSecond ? "0000000" : "0000"), out long temp))
            {
                DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
                return startTime.Add(new TimeSpan(temp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒，1Ticks = 100ns
            }
            else throw new FormatException("时间戳格式有误");
        }
        #endregion

        #region 时间戳转为【Utc时区】的DateTime + TimeStampToUtc(this long timeStamp, bool isSecond = true)
        /// <summary>
        /// 时间戳转为【Utc时区】的DateTime，默认为【秒级】
        /// <para>要到 2286/11/21 01:46:40 才会变成11位（10000000000）</para>
        /// <para>int范围 -2,147,483,648 到 2,147,483,647</para>
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="isSecond">是否为秒级，否则为毫秒级</param>
        /// <returns></returns>
        public static DateTime TimeStampToUtc(this long timeStamp, bool isSecond = true)
        {
            timeStamp *= isSecond ? 10000000 : 10000;//new DateTime(621355968000000000 + long.Parse(timestamp) * 10000000);//更简单的方法
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);//621355968000000000
            return startTime.Add(new TimeSpan(timeStamp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
        }
        #endregion

        #region 时间戳转为【Utc时区】的DateTime，默认为【秒级】 + TimeStampToUtc(string timeStamp, bool isSecond = true)
        /// <summary>
        /// 时间戳转为【Utc时区】的DateTime，默认为【秒级】
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="isSecond">是否为毫秒级，否则为毫秒级</param>
        /// <returns></returns>
        public static DateTime TimeStampToUtc(string timeStamp, bool isSecond = true)
        {
            if (long.TryParse(string.Concat(timeStamp, isSecond ? "0000000" : "0000"), out long stamp))
            {
                DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);//621355968000000000
                return startTime.Add(new TimeSpan(stamp));//以nanosecond为单位，nanosecond：十亿分之一秒   new TimeSpan(10,000,000)为一秒
            }
            else throw new FormatException("时间戳格式有误");
        }
        #endregion

        #region DateTime时间格式转换为Unix秒级时间戳 + ToStamp(this DateTime datetime)
        /// <summary>
        /// DateTime时间格式转换为Unix秒级时间戳，返回<see cref="int"/>格式
        /// <para>用Int最大值是2038年01月19日03时14分07秒，超过可用<see cref="ToTimeStamp(DateTime,bool)"/></para>
        /// </summary>
        /// <param name="datetime">要转换的时间</param>
        /// <returns>Int格式</returns>
        public static int ToTimeStamp(this DateTime datetime)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (int)(datetime - startTime).TotalSeconds;
        }
        #endregion

        #region DateTime时间格式转换为Unix时间戳 + ToTimeStamp(this DateTime datetime, bool isSecond)
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳，返回<see cref="long"/>格式
        /// </summary>
        /// <param name="datetime">要转换的时间</param>
        /// <param name="isSecond">是否为秒级时间戳，否则为毫秒级</param>
        /// <returns>Long格式</returns>
        public static long ToTimeStamp(this DateTime datetime, bool isSecond)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return isSecond ? (long)(datetime - startTime).TotalSeconds : (long)(datetime - startTime).TotalMilliseconds;
        }
        #endregion

        #region 以当前时间为基准，计算日期时间差 + DateDiff(this DateTime datetime)
        /// <summary>
        /// 以当前时间为基准，计算日期时间差
        /// </summary>
        /// <param name="datetime">要计算的时间</param>
        /// <returns></returns>
        public static string DateDiff(this DateTime datetime) => datetime.DateDiff(DateTime.Now);
        #endregion

        #region 计算两个日期时间差 + DateDiff(this DateTime datetime, DateTime point)
        /// <summary>
        /// 计算两个日期时间差
        /// </summary>
        /// <param name="datetime">要计算的时间</param>
        /// <param name="point">时间基准点</param>
        /// <returns></returns>
        public static string DateDiff(this DateTime datetime, DateTime point)
        {
            TimeSpan timeSpan = datetime - point;
            var tag = timeSpan > TimeSpan.Zero ? "后" : "前";
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

        #region 计算周岁年龄 + CalculateAge(this DateTime dateOfBirth)
        /// <summary>
        /// 计算周岁年龄
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        public static int CalculateAge(this DateTime dateOfBirth) => CalculateAge(dateOfBirth, DateTime.Now.Date);
        #endregion

        #region 计算周岁年龄，指定参考日期 + CalculateAge(this DateTime dateOfBirth, DateTime point)
        /// <summary>
        /// 计算周岁年龄，指定参考日期
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <param name="point">时间基准点</param>
        public static int CalculateAge(this DateTime dateOfBirth, DateTime point)
        {
            var age = point.Year - dateOfBirth.Year;
            if (point.Month < dateOfBirth.Month || (point.Month == dateOfBirth.Month && point.Day < dateOfBirth.Day)) --age;
            return age;
        }
        #endregion

        #region 根据当前时间获取当前是第几周 + WeekIndex(this DateTime datetime, bool crossover = false)
        /// <summary>
        /// 根据当前时间获取当前是第几周
        /// </summary>
        /// <param name="datetime">当前时间</param>
        /// <param name="crossover">开启【交叉年】：像2016年12月31号与2017年1月1号刚好在同一星期，【开启】交叉年后，则12月31号为第【1】周，否则为【53】周</param>
        /// <returns></returns>
        public static int WeekIndex(this DateTime datetime, bool crossover = false)
        {
            int dayOfYear = datetime.DayOfYear;//求出此时间在一年中的位置
            int dayOfWeek = (int)new DateTime(datetime.Year, 1, 1).DayOfWeek;//当年第一天的星期几
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;//当年第一天是星期几，中国人习惯星期一为星期开始，因为星期日为0，所以为7
            var (Start, End) = datetime.OneWeek();
            int index = (int)Math.Ceiling(((double)dayOfYear + dayOfWeek - 1) / 7);//确定当前是第几周
            if (crossover && Start.Year < End.Year) index = 1;//判断是否开启交叉年
            return index;
        }
        #endregion

        #region 获取当前的年最大周数 + MaxWeekIndex(this DateTime datetime)
        /// <summary>
        /// 获取当前的年最大周数
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int MaxWeekIndex(this DateTime datetime) => WeekIndex(new DateTime(datetime.Year, 12, 31), false);
        #endregion

        #region 获取两个时间最大的时间 + DateTime Max(DateTime dt1, DateTime dt2)
        /// <summary>
        /// 获取两个时间最大的时间
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DateTime Max(DateTime dt1, DateTime dt2) => DateTime.Compare(dt1, dt2) > 0 ? dt1 : dt2;
        #endregion

        #region 获取两个时间最大的时间(返回可空) + DateTime? Max(DateTime? dt1, DateTime? dt2)
        /// <summary>
        /// 获取两个时间最大的时间(返回可空)
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DateTime? Max(DateTime? dt1, DateTime? dt2)
        {
            if (dt1 is null) return dt2;
            if (dt2 is null) return dt1;
            return DateTime.Compare(dt1.Value, dt2.Value) > 0 ? dt1 : dt2;
        }
        #endregion

        #region 获取三个时间最大的时间 + Max(DateTime dt1, DateTime dt2, DateTime dt3)
        /// <summary>
        /// 获取三个时间最大的时间
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dt3"></param>
        /// <returns></returns>
        public static DateTime Max(DateTime dt1, DateTime dt2, DateTime dt3) => DateTime.Compare(dt1, dt2) > 0 ? (DateTime.Compare(dt1, dt3) > 0 ? dt1 : dt3) : (DateTime.Compare(dt2, dt3) > 0 ? dt2 : dt3);
        #endregion

        #region 获取三个时间最大的时间(返回可空) + Max(DateTime? dt1, DateTime? dt2, DateTime? dt3)
        /// <summary>
        /// 获取三个时间最大的时间(返回可空)
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dt3"></param>
        /// <returns></returns>
        public static DateTime? Max(DateTime? dt1, DateTime? dt2, DateTime? dt3)
        {
            if (dt1 is null) return Max(dt2, dt3);
            if (dt2 is null) return Max(dt1, dt3);
            if (dt3 is null) return Max(dt1, dt2);
            return Max(dt1.Value, dt2.Value, dt3.Value);
        }
        #endregion

        #region 获取两个时间最小的时间 + DateTime Min(DateTime dt1, DateTime dt2)
        /// <summary>
        /// 获取两个时间最小的时间
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DateTime Min(DateTime dt1, DateTime dt2) => DateTime.Compare(dt1, dt2) < 0 ? dt1 : dt2;
        #endregion

        #region 获取两个时间最小的时间(返回可空) + DateTime? Min(DateTime? dt1, DateTime? dt2)
        /// <summary>
        /// 获取两个时间最小的时间(返回可空)
        /// 注：NULL不是最小值
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DateTime? Min(DateTime? dt1, DateTime? dt2)
        {
            if (dt1 is null) return dt2;
            if (dt2 is null) return dt1;
            return DateTime.Compare(dt1.Value, dt2.Value) < 0 ? dt1 : dt2;
        }
        #endregion

        #region 获取三个时间最小的时间 + Min(DateTime dt1, DateTime dt2, DateTime dt3)
        /// <summary>
        /// 获取三个时间最小的时间
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dt3"></param>
        /// <returns></returns>
        public static DateTime Min(DateTime dt1, DateTime dt2, DateTime dt3) => DateTime.Compare(dt1, dt2) < 0 ? (DateTime.Compare(dt1, dt3) < 0 ? dt1 : dt3) : (DateTime.Compare(dt2, dt3) < 0 ? dt2 : dt3);
        #endregion

        #region 获取三个时间最小的时间(返回可空) + DateTime? Min(DateTime? dt1, DateTime? dt2)
        /// <summary>
        /// 获取三个时间最小的时间(返回可空)
        /// 注：NULL不是最小值
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dt3"></param>
        /// <returns></returns>
        public static DateTime? Min(DateTime? dt1, DateTime? dt2, DateTime? dt3)
        {
            if (dt1 is null) return Min(dt2, dt3);
            if (dt2 is null) return Min(dt1, dt3);
            if (dt3 is null) return Min(dt1, dt2);
            return Min(dt1.Value, dt2.Value, dt3.Value);
        }
        #endregion
    }
}