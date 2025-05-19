// -----------------------------------------------------------------------------
// 项目名称：Kane.Extension
// 项目作者：Kane Leung
// 源码地址：Gitee：https://gitee.com/KaneLeung/Kane.Extension 
// 　　　　　Github：https://github.com/KaneLeung/Kane.Extension 
// 开源协议：MIT（https://raw.githubusercontent.com/KaneLeung/Kane.Extension/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Kane.Extension
{
    /// <summary>
    /// 图片类扩展方法
    /// </summary>
    public static class ImageExtension
    {
        #region 检查该文件是否图片的后缀
        /// <summary>
        /// 常见的图像文件后缀
        /// </summary>
        internal static string[] ImageExt = new string[] { ".png", ".jpg", ".jepg", ".gif", ".bmp" };
        /// <summary>
        /// 可扩展的图像文件后缀，如【.tif】
        /// </summary>
        public static List<string> ImageExtEx = new List<string>(4);
        #endregion

        #region 检查该文件是否为图片文件，可设置ImageExtEx进行全局扩展
        /// <summary>
        /// 检查该文件是否为图片文件，可设置ImageExtEx进行全局扩展
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <returns></returns>
        public static bool IsImageFile(this string path)
        {
            var ext = Path.GetExtension(path).ToLower();
            return ImageExt.Any(k => k == ext) || ImageExtEx.Any(k => k.ToLower() == ext);
        }
        #endregion

        #region 检查该文件是否为图片文件，可临时进行扩充比较 + IsImageFile(this string path, params string[] ex)
        /// <summary>
        /// 检查该文件是否为图片文件，可临时进行扩充比较，如【.tif】
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <param name="ex">补充的后缀名，，如【.tif】</param>
        /// <returns></returns>
        public static bool IsImageFile(this string path, params string[] ex)
        {
            var ext = Path.GetExtension(path).ToLower();
            return ImageExt.Any(k => k == ext) || ImageExtEx.Any(k => k.ToLower() == ext) || ex.Any(k => k.ToLower() == ext);
        }
        #endregion

#if NETFRAMEWORK
        #region 多个Bitmap叠加 + BitmapOverlay(Bitmap original, params Bitmap[] overlays)
        /// <summary>
        /// 多个Bitmap叠加
        /// </summary>
        /// <param name="original">原始的Bitmap</param>
        /// <param name="overlays">要叠加的Bitmap</param>
        /// <returns></returns>
        public static Bitmap BitmapOverlay(Bitmap original, params Bitmap[] overlays)
        {
            Bitmap bitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format64bppArgb);
            Graphics canvas = Graphics.FromImage(bitmap);
            canvas.DrawImage(original, new Point(0, 0));
            foreach (Bitmap overlay in overlays)
            {
                canvas.DrawImage(new Bitmap(overlay, original.Size), new Point(0, 0));
            }
            canvas.Save();
            return bitmap;
        }
        #endregion

        #region 替换Bitmap的底色 + ChangeBitmapColor(Bitmap original, Color colorMask)
        /// <summary>
        /// 替换Bitmap的底色
        /// </summary>
        /// <param name="original">原始的Bitmap</param>
        /// <param name="colorMask">要替换的颜色</param>
        /// <returns></returns>
        public static Bitmap ChangeBitmapColor(Bitmap original, Color colorMask)
        {
            Bitmap newBitmap = new Bitmap(original);

            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color color = original.GetPixel(x, y);
                    if (color.A != 0)
                    {
                        int red = color.R * colorMask.R / 255;
                        int green = color.G * colorMask.G / 255;
                        int blue = color.B * colorMask.B / 255;
                        int alpha = color.A * colorMask.A / 255;
                        newBitmap.SetPixel(x, y, Color.FromArgb(alpha, red, green, blue));
                    }
                    else
                    {
                        newBitmap.SetPixel(x, y, color);
                    }
                }
            }
            return newBitmap;
        }
        #endregion

        #region 调整Bitmap大小 + ResizeBitmap(Bitmap original, int width, int height)
        /// <summary>
        /// 调整Bitmap大小
        /// </summary>
        /// <param name="original">原始的Bitmap</param>
        /// <param name="width">调整后的宽</param>
        /// <param name="height">调整后的高</param>
        /// <returns></returns>
        public static Bitmap ResizeBitmap(Bitmap original, int width, int height)
        {
            Bitmap newBitmap = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(newBitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(original, new Rectangle(0, 0, width, height));
            return newBitmap;
        }
        #endregion

        #region 将Image转成指定格式的Base64字符串 + ToBase64(this Image image, ImageFormat format)
        /// <summary>
        /// 将Image转成指定格式的Base64字符串
        /// </summary>
        /// <param name="image">要转换的Image</param>
        /// <param name="format">要转换的格式</param>
        /// <returns></returns>
        public static string ToBase64(this Image image, ImageFormat format)
        {
            try
            {
                using MemoryStream ms = new MemoryStream();
                image.Save(ms, format);
                byte[] byteArr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(byteArr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(byteArr);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region 将Image转成Png格式的Base64字符串 + ToBase64(this Image image)
        /// <summary>
        /// 将Image转成Png格式的Base64字符串
        /// </summary>
        /// <param name="image">要转换的Image</param>
        /// <returns></returns>
        public static string ToBase64(this Image image) => image.ToBase64(ImageFormat.Png);
        #endregion

        #region 将Base64字符串转成Image,自动去除CssBase64格式 + Base64ToImage(this string value)
        /// <summary>
        /// 将Base64字符串转成Image,自动去除CssBase64格式
        /// </summary>
        /// <param name="value">要转换的Base64字符串</param>
        /// <returns></returns>
        public static Image Base64ToImage(this string value)
        {
            try
            {
                byte[] imageBytes = value.Replace("data:image/png;base64,", "").Replace("data:image/jpg;base64,", "")
                    .Replace("data:image/jpeg;base64,", "").Base64ToBytes();//如果包含CssBase64头部信息，则去掉
                using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将图像文件转成Base64字符串，注意未增加CssBase64头信息 + FileToBase64(this string path)
        /// <summary>
        /// 将图像文件转成Base64字符串，注意未增加CssBase64头信息
        /// </summary>
        /// <param name="path">图像文件路径</param>
        /// <returns></returns>
        public static string FileToBase64(this string path)
        {
            if (!path.IsImageFile()) throw new Exception("文件不是有效的图像文件");
            return new Bitmap(path, true).ToBase64();
        }
        #endregion

        #region 将图像文件转成Base64字符串，可扩展后缀，如【.tif】，注意未增加CssBase64头信息 + FileToBase64(this string path) + FileToBase64(this string path,params string[] ext)
        /// <summary>
        /// 将图像文件转成Base64字符串，可扩展后缀，如【.tif】，注意未增加CssBase64头信息
        /// </summary>
        /// <param name="path">图像文件路径</param>
        /// <param name="ext">扩展后缀</param>
        /// <returns></returns>
        public static string FileToBase64(this string path, params string[] ext)
        {
            if (!path.IsImageFile(ext)) throw new Exception("文件不是有效的图像文件");
            try
            {
                return new Bitmap(path, true).ToBase64();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region 扭曲图片 + TwistImage(this Bitmap bitmap, bool xDirection, double multValue, double phase, Color backgroundColor)
        /// <summary>
        /// 扭曲图片
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="xDirection">是否为[X]轴方向</param>
        /// <param name="multValue">扭曲度</param>
        /// <param name="phase">相位</param>
        /// <param name="backgroundColor">背景颜色</param>
        /// <returns></returns>
        public static Bitmap TwistImage(this Bitmap bitmap, bool xDirection, double multValue, double phase, Color backgroundColor)
        {
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics graph = Graphics.FromImage(result);//设置背景色为源图片背景色
            graph.Clear(backgroundColor);
            graph.Dispose();
            double dBaseAxisLen = xDirection ? result.Height : result.Width;
            for (int i = 0; i < result.Width; i++)
            {
                for (int j = 0; j < result.Height; j++)
                {
                    var targetX = xDirection ? (6.28318530717958 * j) / dBaseAxisLen : (6.28318530717958 * i) / dBaseAxisLen;//6.28318530717958为2π
                    targetX += phase;
                    double targetY = Math.Sin(targetX);
                    var originalX = xDirection ? i + (int)(targetY * multValue) : i;
                    var originalY = xDirection ? j : j + (int)(targetY * multValue);
                    if (originalX >= 0 && originalX < result.Width && originalY >= 0 && originalY < result.Height)
                    {
                        result.SetPixel(originalX, originalY, bitmap.GetPixel(i, j));// 取得当前点的颜色
                    }
                }
            }
            return result;
        }
        #endregion

        private static readonly Random random = new();

        #region 随机生成颜色，默认Alpha透明度值为255 + RandomColor(int alpha = 255)
        /// <summary>
        /// 随机生成颜色，默认Alpha透明度值为255
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color RandomColor(int alpha = 255)
            => Color.FromArgb(alpha, random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        #endregion

        #region 随机生成较深的颜色，默认Alpha透明度值为255 + GetDarkColor(int alpha = 255)
        /// <summary>
        /// 随机生成较深的颜色，默认Alpha透明度值为255
        /// </summary>
        /// <param name="alpha">Alpha透明度值，有效值为 0 到 255。</param>
        /// <returns></returns>
        public static Color RandomDarkColor(int alpha = 255)
        {
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = (red + green > 380) ? red + green - 380 : random.Next(256);
            blue = (blue > 255) ? 255 : blue;
            int increase = random.Next(30, 256);//加深颜色，可调整
            red = Math.Abs(red - increase);
            green = Math.Abs(green - increase);
            blue = Math.Abs(blue - increase);
            return Color.FromArgb(alpha, red, green, blue);
        }
        #endregion

        #region 随机生成较浅的颜色，默认Alpha透明度值为255 + GetLightColor(int alpha = 255)
        /// <summary>
        /// 随机生成较浅的颜色，默认Alpha透明度值为255
        /// </summary>
        /// <param name="alpha">Alpha透明度值，有效值为 0 到 255。</param>
        /// <returns></returns>
        public static Color RandomLightColor(int alpha = 255)
        {
            // $R * 0.299 + $G * 0.587 + $B * 0.114 > 192，值越大，颜色越深
            int red = random.Next(200, 256);
            int green = red < 225 ? 255 : random.Next(220, 256);
            int blue = red < 225 ? 255 : green < 235 ? 255 : random.Next(200, 256);
            return Color.FromArgb(alpha, red, green, blue);
        }
        #endregion
#endif
    }
}