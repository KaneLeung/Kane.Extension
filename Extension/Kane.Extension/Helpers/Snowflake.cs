// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.0
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

#if !NET40
using System;
using System.Collections.Generic;
using System.Threading;

namespace Kane.Extension
{
    #region Snowflake 雪花算法
    /// <summary>
    /// Snowflake，雪花算法是由Twitter开源的分布式ID生成算法，以划分命名空间的方式将 64-bit位分割成多个部分，每个部分代表不同的含义。
    /// <para>参考自https://gitee.com/monksoul/Furion/blob/master/framework/Furion/Utilities/Snowflake/Snowflake.cs</para>
    /// </summary>
    public class Snowflake
    {
        #region 私有字段
        /// <summary>
        /// 机房标识位
        /// </summary>
        private const int DATACENTER_ID_BITS = 5;
        /// <summary>
        /// 机器标识位
        /// </summary>
        private const int WORKER_ID_BITS = 5;
        /// <summary>
        /// 序列号标识位
        /// </summary>
        private const int SEQUENCE_BITS = 12;
        /// <summary>
        /// 最大机房ID = 32
        /// </summary>
        private const int MAX_DATACENTER_ID = -1 ^ -1 << DATACENTER_ID_BITS;
        /// <summary>
        /// 最大机器ID = 32
        /// </summary>
        private const int MAX_WORKER_ID = -1 ^ -1 << WORKER_ID_BITS;
        /// <summary>
        /// 最大序列号 = 4096（单节点每毫秒可产生的最大ID数）
        /// </summary>
        private const int SEQUENCE_MASK = -1 ^ -1 << SEQUENCE_BITS;
        /// <summary>
        /// 机器ID左位移长度 = 12
        /// </summary>
        private const int WORKER_ID_SHIFT_COUNT = SEQUENCE_BITS;
        /// <summary>
        /// 机房ID左位移长度 = 17
        /// </summary>
        private const int DATACENTER_ID_SHIFT_COUNT = WORKER_ID_BITS + SEQUENCE_BITS;
        /// <summary>
        /// 时间戳左位移长度 = 22
        /// </summary>
        private const int TIMESTAMP_SHIFT_COUNT = DATACENTER_ID_BITS + WORKER_ID_BITS + SEQUENCE_BITS;
        /// <summary>
        /// 历史ID存储数组长度
        /// </summary>
        private const int CAPACITY = 1000;
        /// <summary>
        /// 历史ID存储数组，该数组为解决时钟回拨而设计，如果历史ID反推出的时间戳大于当前时间戳，说明发生了时钟回拨，此时采用历史ID+1的方式生成新ID，直到时间追赶至回拨前的时间点
        /// </summary>
        private readonly LongArray longArray = new(CAPACITY);
        /// <summary>
        /// 基准时间
        /// </summary>
        private readonly DateTime START_TIME = new(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion

        /// <summary>
        /// 机房ID
        /// </summary>
        public long DataCenterID { get; private set; }
        /// <summary>
        /// 机器ID
        /// </summary>
        public long WorkerID { get; private set; }
        /// <summary>
        /// 当前时间戳 = 当前时间 - 基础时间
        /// </summary>
        public long CurrentTimestamp => (long)(DateTime.UtcNow - START_TIME).TotalMilliseconds;

        #region 构造函数 + Snowflake(long dataCenterID, long workerID)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataCenterID"></param>
        /// <param name="workerID"></param>
        public Snowflake(long dataCenterID, long workerID)
        {
            DataCenterID = dataCenterID;
            WorkerID = workerID;
            if (DataCenterID < 0 || DataCenterID > MAX_DATACENTER_ID) throw new ArgumentException(nameof(dataCenterID));
            if (WorkerID < 0 || WorkerID > MAX_WORKER_ID) throw new ArgumentException(nameof(workerID));
        }
        #endregion

        #region 获取雪花ID + NextID()
        /// <summary>
        /// 获取雪花ID
        /// </summary>
        /// <returns></returns>
        public long NextID()
        {
            do
            {
                var timestamp = CurrentTimestamp;
                // 计算槽位下标
                var index = (int)(timestamp % CAPACITY);
                // 通过槽位下标获取此时间戳的历史数据，如果 historyID 为 0 说明此时间戳并没有生成过 ID
                var historyID = longArray[index];
                var historyTimestamp = (historyID >> TIMESTAMP_SHIFT_COUNT);
                // 如果此时间戳没有生成过 ID，或时间戳小于当前时间，认为需要设置新的时间戳
                if (historyID == 0 || historyTimestamp < timestamp)
                {
                    var result = (timestamp << TIMESTAMP_SHIFT_COUNT) | (DataCenterID << DATACENTER_ID_SHIFT_COUNT) | (WorkerID << WORKER_ID_SHIFT_COUNT);
                    if (longArray.CompareAndSet(index, historyID, result)) return result;
                }
                // 如果 historyTimestamp 大于 timestamp 则表示发生了时间回退；
                // 如果 historyTimestamp 等于 timestamp 则表示在同一个时间戳正常的生成 ID;
                // 上面两种情况都采用 historyID + 1 的方式
                if (historyTimestamp >= timestamp)
                {
                    var sequence = historyID & SEQUENCE_MASK;
                    // 该时间戳生成的 ID 数超过上限
                    if (sequence >= SEQUENCE_MASK) continue;
                    var result = historyID + 1;
                    if (longArray.CompareAndSet(index, historyID, result)) return result;
                }
            }
            while (true);
        }
        #endregion
    }
    #endregion

    #region 线程安全的长整形数组 + LongArray
    /// <summary>
    /// 线程安全的长整形数组
    /// </summary>
    public class LongArray
    {
        /// <summary>
        /// 长整形数组
        /// </summary>
        private readonly long[] _value;
        /// <summary>
        /// 获取一个只读的长整形数组集合
        /// </summary>
        public IReadOnlyCollection<long> Value => _value;
        /// <summary>
        /// 获取数组下标的值
        /// </summary>
        /// <param name="index">数组下标</param>
        /// <returns>数组下标的值</returns>
        public long this[int index] => _value[index];
        /// <summary>
        /// 实例化一个线程安全的长整形数组
        /// </summary>
        /// <param name="length">数组的长度</param>
        public LongArray(int length) => _value = new long[length];
        /// <summary>
        /// 替换数组中指定下标的值，如果下标的当前值与期望的值相等，则替换成功；不相等说明发生了并发冲突，则替换失败
        /// </summary>
        /// <param name="index">数组下标</param>
        /// <param name="currentValue">期望的值</param>
        /// <param name="newValue">新的值</param>
        /// <returns>是否替换成功</returns>
        public bool CompareAndSet(int index, long currentValue, long newValue)
            => Interlocked.CompareExchange(ref _value[index], newValue, currentValue) == currentValue;
    }
    #endregion
}
#endif