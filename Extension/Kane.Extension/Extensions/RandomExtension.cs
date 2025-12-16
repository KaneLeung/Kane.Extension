// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 随机类扩展方法
    /// </summary>
    public static class RandomExtension
    {
        private static readonly Random random = new();
#if NET6_0_OR_GREATER
        static RandomExtension()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//Net中使用GB2312编码
        }
#endif

        #region 产生随机字符串，可设定类型，也可以排除不要的字符 + RandCode(int length, RandMethod method = RandMethod.All, params char[] exceptChar)
        /// <summary>
        /// 产生随机字符串，可设定类型，也可以排除不要的字符
        /// </summary>
        /// <param name="length">随机字符串的长度</param>
        /// <param name="mode">随机字符串包含类型枚举类</param>
        /// <param name="exceptChar">排除的字符</param>
        /// <returns></returns>
        public static string RandCode(int length, RandomMode mode = RandomMode.All, params char[] exceptChar)
        {
            var charList = new List<char>();
            if (mode.HasFlag(RandomMode.All) || mode.HasFlag(RandomMode.Numeric))
                charList.AddRange(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
            if (mode.HasFlag(RandomMode.All) || mode.HasFlag(RandomMode.Letter) || mode.HasFlag(RandomMode.Lowercase))
                charList.AddRange(['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z']);
            if (mode.HasFlag(RandomMode.All) || mode.HasFlag(RandomMode.Letter) || mode.HasFlag(RandomMode.Uppercase))
                charList.AddRange(['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']);
            if (mode.HasFlag(RandomMode.All) || mode.HasFlag(RandomMode.Punctuation))
                charList.AddRange(['!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '~', '`', '|', '}', '{', '[', ']', '\\', ':', ';', '?', '>', '<', ',', '.', '/', '-', '=']);
            if (exceptChar.Length > 0) charList = charList.Except(exceptChar).ToList();
            var result = new StringBuilder();
            int charCount = charList.Count;
            for (int i = 0; i < length; i++)
                result.Append(charList[random.Next(0, charCount)]);
            return result.ToString();
        }
        #endregion

        #region Guid转换为纯数字 + ToLongData(this Guid guid)
        /// <summary>
        /// Guid转换为Long值，通常为16位长
        /// </summary>
        /// <param name="guid">Guid对象</param>
        /// <returns></returns>
        public static long ToLongData(this Guid guid) => BitConverter.ToInt64(guid.ToByteArray(), 0);
        #endregion

        #region Guid转换为纯数字 + ToIntData(this Guid guid)
        /// <summary>
        /// Guid转换为Int值，通常为10位长
        /// </summary>
        /// <param name="guid">Guid对象</param>
        /// <returns></returns>
        public static int ToIntData(this Guid guid) => BitConverter.ToInt32(guid.ToByteArray(), 0);
        #endregion

        #region 获取一个全大写的【UUID】】 + UUID()
        /// <summary>
        /// 获取一个全大写的【UUID】
        /// </summary>
        /// <returns></returns>
        public static string UUID() => Guid.NewGuid().ToString("N").ToUpper();
        #endregion

        #region 获取一个全小写的【UUID】 + Uuid()
        /// <summary>
        /// 获取一个全小写的【UUID】
        /// </summary>
        /// <returns></returns>
        public static string Uuid() => Guid.NewGuid().ToString("N");
        #endregion

        #region 随机生成【GB2312】内的汉字字符串 + RandomChinese(int length)
        /// <summary>
        /// 随机生成【GB2312】内的汉字字符串
        /// </summary>
        /// <param name="length">生成的字符串长度</param>
        /// <returns></returns>
        public static string RandomChinese(int length)
        {
            char[] chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'];//汉字编码的组成元素，十六进制数
            var result = new StringBuilder();
            //每循环一次产生一个含两个元素的十六进制字节数组，并放入bytes数组中
            //汉字由四个区位码组成，1、2位作为字节数组的第一个元素，3、4位作为第二个元素
            for (int i = 0; i < length; i++)
            {
                int index1 = random.Next(11, 14);
                int index2 = index1 == 13 ? random.Next(0, 7) : random.Next(0, 16);
                int index3 = random.Next(10, 16);
                int index4 = index3 == 10 ? random.Next(1, 16) : (index3 == 15 ? random.Next(0, 15) : random.Next(0, 16));
                //定义两个字节变量存储产生的随机汉字区位码
                byte prefix = Convert.ToByte(new string([chars[index1], chars[index2]]), 16);
                byte postfix = Convert.ToByte(new string([chars[index3], chars[index4]]), 16);
                result.Append(Encoding.GetEncoding("GB2312").GetString([prefix, postfix]));
            }
            return result.ToString();
        }
        #endregion

        #region 在范围内随机一个整数，可包含最大值 + RandomInt(int min, int max)
        /// <summary>
        /// 在范围内随机一个整数，可包含最大值
        /// <para>直接使用<see cref="Random.Next(int,int)"/>是不包含最大值</para>
        /// </summary>
        /// <param name="min">最小值，不能小于【-2147483648】</param>
        /// <param name="max">最大值 ※注意，最大值不能超过<see cref="int.MaxValue"/>减1，即【2147483646】</param>
        /// <returns><see cref="int"/></returns>
        public static int RandomInt(int min, int max) => random.Next(min, max + 1);
        #endregion

        #region 在范围内随机一个整数，可包含最大值 + RandomShort(short min, short max)
        /// <summary>
        /// 在范围内随机一个整数，可包含最大值
        /// </summary>
        /// <param name="min">最小值，不能小于【-32768】</param>
        /// <param name="max">最大值，不能大于【32767】</param>
        /// <returns><see cref="short"/></returns>
        public static short RandomShort(short min, short max) => (short)random.Next(min, max + 1);
        #endregion

        #region 在范围内随机一个小数，小数位最多只有10位 + RandomDouble(double min, double max)
        /// <summary>
        /// 在范围内随机一个小数，小数位最多只有9位，可包含最大值
        /// <para>直接使用<see cref="Random.NextDouble"/>是不包含最大值</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static double RandomDouble(double min, double max)
        {
            var temp = (double)random.Next(0, 1000000001) / 1000000000;//随机生成【1-1000000000】的整
            return temp * (max - min) + min;
        }
        #endregion

        #region 在范围内随机一个小数，小数位最多只有10位 + RandomFloat(float min, float max)
        /// <summary>
        /// 在范围内随机一个小数，小数位最多只有9位，可包含最大值
        /// <para>直接使用<see cref="Random.NextDouble"/>是不包含最大值</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static float RandomFloat(float min, float max)
        {
            var temp = (float)random.Next(0, 1000000001) / 1000000000;//随机生成【1-1000000000】的整
            return temp * (max - min) + min;
        }
        #endregion

        #region 在范围内随机一个小数，小数位可设置 + RandomDouble(double min, double max, int digits)
        /// <summary>
        /// 在范围内随机一个小数，小数位可设置
        /// <para>※注意：范围有限！！※</para>
        /// <para>范围：min * 10^digits ≤ 2147483647 并且 max * 10^digits + 1 ≤ 2147483647</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="digits">小数保留数位</param>
        /// <returns></returns>
        public static double RandomDouble(double min, double max, int digits)
        {
            var pow = (int)Math.Pow(10, digits);
            double _min = min * pow, _max = max * pow + 1;
            if (_min > int.MaxValue || _max > int.MaxValue) throw new ArgumentOutOfRangeException(_min > int.MaxValue ? nameof(min) : nameof(max), "参数超出范围");
            return (double)random.Next((int)_min, (int)_max) / pow;
        }
        #endregion

        #region 在范围内随机一个小数，小数位可设置 + RandomFloat(float min, float max, int digits)
        /// <summary>
        /// 在范围内随机一个小数，小数位可设置
        /// <para>※注意：范围有限！！※</para>
        /// <para>范围：min * 10^digits ≤ 2147483647 并且 max * 10^digits + 1 ≤ 2147483647</para>
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="digits">小数保留数位</param>
        /// <returns></returns>
        public static float RandomFloat(float min, float max, int digits)
        {
            var pow = (int)Math.Pow(10, digits);
            float _min = min * pow, _max = max * pow + 1;
            if (_min > int.MaxValue || _max > int.MaxValue) throw new ArgumentOutOfRangeException(_min > int.MaxValue ? nameof(min) : nameof(max), "参数超出范围");
            return (float)random.Next((int)_min, (int)_max) / pow;
        }
        #endregion
    }
}