// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.6
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;

namespace Kane.Extension
{
    /// <summary>
    /// 日期时间(DateTimeOffset)类扩展方法
    /// </summary>
    public static class DateTimeOffsetExtension
    {
#if !NET40 && !NET45
        #region 获取时间戳，可增加或减少【秒】 + TimeStamp(int seconds = 0)
        /// <summary>  
        /// 获取时间戳，可增加或减少【秒】
        /// <para>时间戳, 又叫Unix Stamp. 从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒。</para>
        /// </summary>  
        /// <param name="seconds">增加或减少【秒】</param>
        /// <returns></returns>  
        public static long TimeStamp(int seconds = 0) => DateTimeOffset.UtcNow.AddSeconds(seconds).ToUnixTimeSeconds();
        #endregion

        #region 获取毫秒级时间戳，可增加或减少【秒】 + MillisecondTimeStamp(int seconds = 0)
        /// <summary>
        /// 获取毫秒级时间戳，可增加或减少【秒】
        /// <para>时间戳, 又叫Unix Stamp. 从1970年1月1日（UTC/GMT的午夜）开始所经过的秒数，不考虑闰秒。</para>
        /// </summary>
        /// <param name="seconds">增加或减少【秒】</param>
        /// <returns></returns>
        public static long MillisecondTimeStamp(int seconds = 0) => DateTimeOffset.UtcNow.AddSeconds(seconds).ToUnixTimeMilliseconds();
        #endregion
#endif
    }
}