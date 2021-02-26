#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension.Test
* 项目描述 ：
* 类 名 称 ：Captcha
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.Extension.Test
* 机器名称 ：PC-0432 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2021/02/26 09:31:18
* 更新时间 ：2021/02/26 09:31:18
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2021. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Kane.Extension.Test
{
    [TestClass]
    public class Captcha
    {
        /// <summary>
        /// 生成字母证码
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="column">列数</param>
        [TestMethod]
        [DataRow(5, 10)]
        public void LetterCaptchaTest(int row, int column)
        {
            var captcha = new CaptchaHelper
            {
                Mode = CaptchaMode.Letter,
                RandomBackgroundColor = true,
                RandomFontColor = true,
                FontSize = 20,
                NoisePointCount = 5,
                RandomLineCount = 10,
                StringLength = 4,
                ToTwistImage = false,
                RandomAngle = 35,
                UseDarkMode = false,
            };
            var file = $"D:/{DateTime.Now:HHmmssfff}.jpg";
            BatchGenerateCaptcha(captcha, row, column, file);
            Assert.IsTrue(File.Exists(file));
        }

        /// <summary>
        /// 生成数字证码
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="column">列数</param>
        [TestMethod]
        [DataRow(5, 10)]
        public void NumericCaptchaTest(int row, int column)
        {
            var captcha = new CaptchaHelper
            {
                Mode = CaptchaMode.Numeric,
                RandomBackgroundColor = true,
                RandomFontColor = true,
                FontSize = 20,
                NoisePointCount = 5,
                RandomLineCount = 0,
                StringLength = 4,
                ToTwistImage = true,
                TwistSetting = (false, 5, 2),
                RandomAngle = 10,
                UseDarkMode = true,
            };
            var file = $"D:/{DateTime.Now:HHmmssfff}.jpg";
            BatchGenerateCaptcha(captcha, row, column, file);
            Assert.IsTrue(File.Exists(file));
        }

        /// <summary>
        /// 生成字母+数字验证码
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="column">列数</param>
        [TestMethod]
        [DataRow(5, 10)]
        public void LetterAndNumericCaptchaTest(int row, int column)
        {
            var captcha = new CaptchaHelper
            {
                Mode = CaptchaMode.LetterAndNumeric,
                RandomBackgroundColor = true,
                RandomFontColor = true,
                FontSize = 21,
                NoisePointCount = 10,
                RandomLineCount = 2,
                StringLength = 4,
                ToTwistImage = true,
                TwistSetting = (false, 5, 2),
                RandomAngle = 10,
                UseDarkMode = true,
            };
            var file = $"D:/{DateTime.Now:HHmmssfff}.jpg";
            BatchGenerateCaptcha(captcha, row, column, file);
            Assert.IsTrue(File.Exists(file));
        }

        /// <summary>
        /// 生成中文验证码
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="column">列数</param>
        [TestMethod]
        [DataRow(5, 10)]
        public void ChineseCaptchaTest(int row, int column)
        {
            var captcha = new CaptchaHelper
            {
                Mode = CaptchaMode.Chinese,
                RandomBackgroundColor = true,
                RandomFontColor = true,
                FontSize = 21,
                NoisePointCount = 5,
                RandomLineCount = 2,
                StringLength = 2,
                ToTwistImage = false,
                TwistSetting = (false, 5, 2),
                //RandomAngle = 10,//中文时，角度会失效
                UseDarkMode = false,
            };
            var file = $"D:/{DateTime.Now:HHmmssfff}.jpg";
            BatchGenerateCaptcha(captcha, row, column, file);
            Assert.IsTrue(File.Exists(file));
        }

        #region 批量生成验证码 + BatchGenerateCaptcha(CaptchaHelper captcha, int row, int column, string filePath)
        /// <summary>
        /// 批量生成验证码
        /// </summary>
        /// <param name="captcha">验证码帮助类</param>
        /// <param name="row">生成的行数</param>
        /// <param name="column">生成的列数</param>
        /// <param name="filePath">保存的图片的完整路径</param>
        private void BatchGenerateCaptcha(CaptchaHelper captcha, int row, int column, string filePath)
        {
            var hasChinese = captcha.Mode == CaptchaMode.Chinese || captcha.Mode == CaptchaMode.QuestionBank;
            int sigleWidth = captcha.StringLength * captcha.FontSize * (hasChinese ? 2 : 1);
            int sigleHeight = Convert.ToInt32(0.6 * captcha.FontSize + captcha.FontSize);
            int width = sigleWidth * column;//总图片宽度
            int height = sigleHeight * row;//总图片高度
            var image = new Bitmap(width, height);
            using Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.White);
            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    graphics.DrawImage(
                        captcha.GetCaptcha().Image,//生成的验证码
                        i * sigleWidth,//X位置
                        j * sigleHeight, //Y位置
                        sigleWidth,//单张验证码长度
                        sigleHeight//单张验证码高度
                        );
                }
            }
            using FileStream stream = new(filePath, FileMode.OpenOrCreate);
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        #endregion
    }
}