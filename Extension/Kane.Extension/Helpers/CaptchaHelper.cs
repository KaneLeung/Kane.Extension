// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 项目版本：2.0.6
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
//         Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
#if NET6_0_OR_GREATER
using Kane.Extension.Json;
#else
using Kane.Extension.JsonNet;
#endif

namespace Kane.Extension
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public class CaptchaHelper
    {
        private readonly Random random = new();
        private readonly char[] operators = new char[] { '+', '*', '-', '/' };
#if NETFRAMEWORK
        private CaptchaMode currentMode;
#endif

        #region 无参构造函数 + CaptchaHelper()
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CaptchaHelper()
        {
        }
        #endregion

        #region 构造函数，指定路径加载【题库】 + CaptchaHelper(string path)
        /// <summary>
        /// 构造函数，指定路径加载【题库】
        /// </summary>
        /// <param name="path"></param>
        public CaptchaHelper(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                Mode = CaptchaMode.QuestionBank;
                var data = File.ReadAllText(path);
                QuestionBank = data.ToObject<Dictionary<string, string>>();
            }
            else throw new FileNotFoundException($"{path}找不到相关文件");
        }
        #endregion

        /// <summary>
        /// 模式为【<see cref="CaptchaMode.Letter"/>或<see cref="CaptchaMode.Numeric"/>或<see cref="CaptchaMode.LetterAndNumeric"/>】时，
        /// <para>验证码的长度，默认为【4】，如果使用题库或四则运行，则忽略本值</para>
        /// </summary>
        public int StringLength { get; set; } = 4;
        /// <summary>
        /// 模式为【<see cref="CaptchaMode.Chinese"/>】时，验证码的长度，默认为【2】，如果使用题库或四则运行，则忽略本值
        /// </summary>
        public int ChineseLength { get; set; } = 2;
        /// <summary>
        /// 字体大小，默认为【20】
        /// </summary>
        public int FontSize { get; set; } = 20;
        /// <summary>
        /// 英文数字的字体，默认为【Verdana】，注意Linux下要安装相应的字库
        /// </summary>
        public string StringFontFamily { get; set; } = "Verdana";
        /// <summary>
        /// 中文的字体，默认为【宋体】，注意Linux下要安装相应的字库
        /// </summary>
        public string ChineseFontFamily { get; set; } = "宋体";
        /// <summary>
        /// 是否设置为随机颜色，默认为【true】，当为true时<see cref="FontColor"/>则失效
        /// </summary>
        public bool RandomFontColor { get; set; } = true;
        /// <summary>
        /// 当设置为随机字体颜色时，本值不生效，默认为【蓝色】
        /// </summary>
        public Color FontColor { get; set; } = Color.Blue;
        /// <summary>
        /// 是否设置为随机颜色，默认为【true】，当为true时<see cref="BackgroundColor"/>则失效
        /// </summary>
        public bool RandomBackgroundColor { get; set; } = true;
        /// <summary>
        /// 当设置为随机背景颜色时，本值不生效，默认为【白色】
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;
        /// <summary>
        /// 噪点系数，默认为【5】
        /// </summary>
        public int NoisePointCount { get; set; } = 5;
        /// <summary>
        /// 随机线条数量，默认为【10】
        /// </summary>
        public int RandomLineCount { get; set; } = 10;
        /// <summary>
        /// 字体旋转随机角度，默认为【35】
        /// </summary>
        public int RandomAngle { get; set; } = 35;
        /// <summary>
        /// 验证码模式，默认为【字符串】+【简单四则运算】
        /// </summary>
        public CaptchaMode Mode { get; set; } = CaptchaMode.LetterAndNumeric | CaptchaMode.Arithmetic;
        /// <summary>
        /// 题库
        /// </summary>
        public Dictionary<string, string> QuestionBank { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 暗黑模式，背景为深色颜色，文字为亮色，反之亦然
        /// <para>如果指定了背景颜色和文字颜色，则不生效</para>
        /// </summary>
        public bool UseDarkMode { get; set; } = false;
        /// <summary>
        /// 是否扭曲图片，默认为【false】
        /// </summary>
        public bool ToTwistImage { get; set; } = false;
        /// <summary>
        /// 【XDirection】是否向X轴方向扭曲,默认为[false]，【MultValue】扭曲度，默认为[5]，【Phase】相位，默认为[2]
        /// </summary>
        public (bool XDirection, double MultValue, double Phase) TwistSetting { get; set; } = (false, 5, 2);

#if NETFRAMEWORK
        #region 生成验证码，返回图片和图片对应该的字符串 + GetCaptcha(string code = null)
        /// <summary>
        /// 生成验证码，返回图片和图片对应该的字符串
        /// </summary>
        /// <param name="code">有值则按值生成，否则按规则生成</param>
        /// <returns></returns>
        public (Bitmap Image, string Answer) GetCaptcha(string code = null)
        {
            var result = string.Empty;
            if (string.IsNullOrEmpty(code))//当code为空时，即按照【VerificationCodeMode】生成相应该字符串
            {
                var modeList = new List<CaptchaMode>();
                if (Mode.HasFlag(CaptchaMode.All) || Mode.HasFlag(CaptchaMode.Letter)) modeList.Add(CaptchaMode.Letter);
                if (Mode.HasFlag(CaptchaMode.All) || Mode.HasFlag(CaptchaMode.Numeric)) modeList.Add(CaptchaMode.Numeric);
                if (Mode.HasFlag(CaptchaMode.All) || Mode.HasFlag(CaptchaMode.LetterAndNumeric)) modeList.Add(CaptchaMode.LetterAndNumeric);
                if (Mode.HasFlag(CaptchaMode.All) || Mode.HasFlag(CaptchaMode.Arithmetic)) modeList.Add(CaptchaMode.Arithmetic);
                if (Mode.HasFlag(CaptchaMode.All) || Mode.HasFlag(CaptchaMode.Chinese)) modeList.Add(CaptchaMode.Chinese);
                if (QuestionBank.Count > 0 && (Mode.HasFlag(CaptchaMode.All) || Mode.HasFlag(CaptchaMode.QuestionBank)))
                    modeList.Add(CaptchaMode.QuestionBank);
                currentMode = modeList[random.Next(modeList.Count)];
                if (currentMode == CaptchaMode.Letter || currentMode == CaptchaMode.Numeric || currentMode == CaptchaMode.LetterAndNumeric)
                {
                    code = result = RandomCode(currentMode, StringLength);
                }
                if (currentMode == CaptchaMode.Arithmetic)
                {
                    var temp = Arithmetic();
                    code = temp.Key;
                    result = temp.Value.ToString();
                }
                if (currentMode == CaptchaMode.Chinese)//中文随机字符串
                {
                    code = result = RandomExtension.RandomChinese(ChineseLength);
                }
                if (currentMode == CaptchaMode.QuestionBank)
                {
                    var temp = QuestionBank.ToList()[random.Next(QuestionBank.Count)];
                    code = temp.Key;
                    result = temp.Value;
                }
            }
            else result = code;
            var hasChinese = currentMode == CaptchaMode.Chinese || currentMode == CaptchaMode.QuestionBank;//当前选中的是否包含中文
            var width = code.Length * FontSize * (hasChinese ? 2 : 1);//如果是中文或题库，则乘二
            var height = Convert.ToInt32(0.6 * FontSize + FontSize);
            Bitmap bitmap = new(width, height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            var backgroundColor = RandomBackgroundColor ? UseDarkMode ? ImageExtension.RandomDarkColor() : ImageExtension.RandomLightColor() : BackgroundColor;
            graphics.Clear(backgroundColor);//设置背景色
            for (int i = 0; i < RandomLineCount; i++)//生成随机线条
            {
                int x1 = random.Next(bitmap.Width);
                int x2 = random.Next(bitmap.Width);
                int y1 = random.Next(bitmap.Height);
                int y2 = random.Next(bitmap.Height);
                graphics.DrawLine(new Pen(
                    RandomBackgroundColor ? UseDarkMode ? ImageExtension.RandomLightColor(150) : ImageExtension.RandomDarkColor(125) : FontColor, 1),
                    x1, y1, x2, y2);
            }
            StringFormat format = new(StringFormatFlags.NoClip)//设置文字居中
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using SolidBrush brush = new(RandomFontColor ? UseDarkMode ? ImageExtension.RandomLightColor() : ImageExtension.RandomDarkColor() : FontColor);
            Font font = new(hasChinese ? ChineseFontFamily : StringFontFamily, random.Next(FontSize - 3, FontSize + 1), FontStyle.Regular);//字体样式
            for (int i = 0; i < code.Length; i++)
            {
                if (hasChinese)//TODO:中文旋转会发生错位，暂时中文不旋转
                {
                    graphics.DrawString(code[i].ToString(), font, brush, 2 + (FontSize * (hasChinese ? 2 : 1) * i), 2);
                }
                else
                {
                    float angle = random.Next(-RandomAngle, RandomAngle + 1);//验证码旋转，防止机器识别，转动的度数
                    graphics.TranslateTransform(15, 12);
                    graphics.RotateTransform(angle);
                    graphics.DrawString(code[i].ToString(), font, brush, -2, 3, format);
                    graphics.RotateTransform(-angle);
                    graphics.TranslateTransform(2, -12);
                }
            }
            //添加噪点
            for (int i = 0; i < bitmap.Width * NoisePointCount; i++)
            {
                bitmap.SetPixel(random.Next(bitmap.Width), random.Next(bitmap.Height),
                    RandomBackgroundColor ? UseDarkMode ? ImageExtension.RandomLightColor() : ImageExtension.RandomDarkColor() : FontColor);
            }
            //是否扭曲图片
            if (ToTwistImage) return (ImageExtension.TwistImage(bitmap, TwistSetting.XDirection, TwistSetting.MultValue, TwistSetting.Phase, backgroundColor), result);
            return (bitmap, result);
        }
        #endregion
#endif

        #region 生成随机字母和数字的字符串 + RandomCode(CaptchaMode mode, int length)
        /// <summary>
        /// 生成随机字母和数字的字符串
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string RandomCode(CaptchaMode mode, int length)
        {
            var charList = new List<char>();
            if (mode == CaptchaMode.Letter) charList.AddRange(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' });//去掉【I】和【l】这两个相似的
            if (mode == CaptchaMode.Numeric) charList.AddRange(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            if (mode == CaptchaMode.LetterAndNumeric) charList.AddRange(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '2', '3', '4', '5', '6', '7', '8', '9' });//去掉【I】、【l】、【0】、【1】、【o】、【O】这几个相似的
            StringBuilder result = new();
            int charCount = charList.Count;
            for (int i = 0; i < length; i++)
                result.Append(charList[random.Next(0, charCount)]);
            return result.ToString();
        }
        #endregion

        #region 简单的四则运算 + Arithmetic()
        /// <summary>
        /// 简单的四则运算
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, int> Arithmetic()
        {
            var oper = operators[random.Next(operators.Length)];
            if (oper == '+')//加法
            {
                var left = random.Next(0, 101);
                var right = left >= 10 ? random.Next(0, 11) : random.Next(0, 101);
                return new KeyValuePair<string, int>($"{left}+{right}=?", left + right);
            }
            else if (oper == '-')//减法
            {
                var left = random.Next(0, 101);
                var right = left > 10 ? random.Next(0, 11) : random.Next(0, 101);
                if (right > left)//如果右边比左边大，则交换两数，避免结果出现负数
                {
                    int temp = left; left = right; right = temp;
                }
                return new KeyValuePair<string, int>($"{left}-{right}=?", left - right);
            }
            else if (oper == '*')//乘法
            {
                var left = random.Next(0, 11);
                var right = left == 0 || left == 10 ? random.Next(0, 101) : random.Next(0, 11);
                return new KeyValuePair<string, int>($"{left}×{right}=?", left * right);
            }
            else//除法
            {
                var left = random.Next(0, 11);
                var right = left == 0 ? random.Next(1, 11) : random.Next(0, 11);//避免两数中有【零】存在
                var result = left * right;
                return new KeyValuePair<string, int>($"{result}÷{(left > right ? left : right)}=?", left > right ? right : left);
            }
        }
        #endregion
    }
}