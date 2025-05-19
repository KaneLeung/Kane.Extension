// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

#if NET6_0_OR_GREATER
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kane.Extension;

/// <summary>
/// 使用 ref struct 该对象只能在栈上分配
/// <para>https://www.cnblogs.com/InCerry/p/Dotnet-Perf-Opt-Use-ValueStringBuilder.html</para>
/// </summary>
public ref struct ValueStringBuilder
{
    // 如果从ArrayPool里分配buffer 那么需要存储一下  
    // 以便在Dispose时归还  
    private char[] _arrayToReturnToPool;
    // 暂存外部传入的buffer  
    private Span<char> _chars;
    // 当前字符串长度  
    private int _pos;

    /// <summary>
    /// 外部传入buffer
    /// </summary>
    /// <param name="initialBuffer"></param>
    public ValueStringBuilder(Span<char> initialBuffer)
    {
        // 使用外部传入的buffer就不使用从pool里面读取的了  
        _arrayToReturnToPool = null;
        _chars = initialBuffer;
        _pos = 0;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="initialCapacity"></param>
    public ValueStringBuilder(int initialCapacity)
    {
        // 如果外部传入了capacity 那么从ArrayPool里面获取  
        _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialCapacity);
        _chars = _arrayToReturnToPool;
        _pos = 0;
    }

    /// <summary>
    /// 返回字符串的Length 由于Length可读可写  
    /// 所以重复使用ValueStringBuilder只需将Length设置为0  
    /// </summary>
    public int Length {
        get => _pos;
        set {
            Debug.Assert(value >= 0);
            Debug.Assert(value <= _chars.Length);
            _pos = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char c)
    {
        // 添加字符非常高效 直接设置到对应Span位置即可  
        int pos = _pos;
        if ((uint)pos < (uint)_chars.Length)
        {
            _chars[pos] = c;
            _pos = pos + 1;
        }
        else
        {
            // 如果buffer空间不足，那么会走  
            GrowAndAppend(c);
        }
    }

    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="s"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string s)
    {
        if (s == null) return;
        // 追加字符串也是一样的高效  
        int pos = _pos;
        // 如果字符串长度为1 那么可以直接像追加字符一样  
        if (s.Length == 1 && (uint)pos < (uint)_chars.Length)
        {
            _chars[pos] = s[0];
            _pos = pos + 1;
        }
        else
        {
            // 如果是多个字符 那么使用较慢的方法  
            AppendSlow(s);
        }
    }

    private void AppendSlow(string s)
    {
        // 追加字符串 空间不够先扩容  
        // 然后使用Span复制 相当高效  
        int pos = _pos;
        if (pos > _chars.Length - s.Length)
        {
            Grow(s.Length);
        }

        s.AsSpan().CopyTo(_chars.Slice(pos));
        _pos += s.Length;
    }

    /// <summary>
    /// 对于需要格式化的对象特殊处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendSpanFormattable<T>(T value, string format = null, IFormatProvider provider = null) where T : ISpanFormattable
    {
        // ISpanFormattable非常高效  
        if (value.TryFormat(_chars.Slice(_pos), out int charsWritten, format, provider))
        {
            _pos += charsWritten;
        }
        else
        {
            Append(value.ToString(format, provider));
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void GrowAndAppend(char c)
    {
        // 单个字符扩容在添加  
        Grow(1);
        Append(c);
    }

    /// <summary>
    /// 扩容方法
    /// </summary>
    /// <param name="additionalCapacityBeyondPos"></param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow(int additionalCapacityBeyondPos)
    {
        Debug.Assert(additionalCapacityBeyondPos > 0);
        Debug.Assert(_pos > _chars.Length - additionalCapacityBeyondPos, "Grow called incorrectly, no resize is needed.");

        // 同样也是2倍扩容，默认从对象池中获取buffer  
        char[] poolArray = ArrayPool<char>.Shared.Rent((int)Math.Max((uint)(_pos + additionalCapacityBeyondPos),
            (uint)_chars.Length * 2));

        _chars.Slice(0, _pos).CopyTo(poolArray);

        char[] toReturn = _arrayToReturnToPool;
        _chars = _arrayToReturnToPool = poolArray;
        if (toReturn != null)
        {
            // 如果原本就是使用的对象池 那么必须归还  
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        char[] toReturn = _arrayToReturnToPool;
        this = default; // 为了安全，在释放时置空当前对象  
        if (toReturn != null)
        {
            // 一定要记得归还对象池  
            ArrayPool<char>.Shared.Return(toReturn);
        }
    }
}
#endif