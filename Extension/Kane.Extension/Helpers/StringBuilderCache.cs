// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.6
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 为每个线程提供一个缓存的可复用的StringBuilder的实例
    /// <para>https://github.com/dotnet/runtime/blob/main/src/libraries/Common/src/System/Text/StringBuilderCache.cs</para>
    /// <para>使用StringBuilder sb = StringBuilderCache.Acquire();</para>
    /// <para>使用sb.Append("Hello");</para>
    /// <para>使用sb.GetStringAndRelease();</para>
    /// </summary>
    public static class StringBuilderCache
    {
        // 这个值360是在与性能专家的讨论中选择的，是在每个线程使用尽可能少的内存和仍然覆盖VS设计者启动路径上的大部分短暂的StringBuilder创建之间的折衷。
        internal const int MaxBuilderSize = 360;
        private const int DefaultCapacity = 16; // == StringBuilder.DefaultCapacity

        [ThreadStatic]
        private static StringBuilder cachedInstance;

        /// <summary>
        /// 获得一个指定容量的StringBuilder
        /// <para>如果一个适当大小的StringBuilder被缓存了，它将被返回并清空缓存。</para>
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public static StringBuilder Acquire(int capacity = DefaultCapacity)
        {
            if (capacity <= MaxBuilderSize)
            {
                StringBuilder sb = cachedInstance;
                if (sb != null)
                {
                    // 当请求的大小大于当前容量时，
                    // 通过获取一个新的StringBuilder来避免Stringbuilder块的碎片化
                    if (capacity <= sb.Capacity)
                    {
                        cachedInstance = null;
                        sb.Clear();
                        return sb;
                    }
                }
            }
            return new StringBuilder(capacity);
        }

        /// <summary>
        /// 如果指定的StringBuilder不是太大，就把它放在缓存中
        /// </summary>
        /// <param name="sb"></param>
        public static void Release(StringBuilder sb)
        {
            if (sb.Capacity <= MaxBuilderSize)
            {
                cachedInstance = sb;
            }
        }

        /// <summary>
        /// ToString()的字符串生成器，将其释放到缓存中，并返回生成的字符串。
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static string GetStringAndRelease(this StringBuilder sb)
        {
            string result = sb.ToString();
            Release(sb);
            return result;
        }
    }
}