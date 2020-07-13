using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Tool
{
    /// <summary>
    /// 验证码 工具类
    /// </summary>
    public class VerificationCode
    {
        /// <summary>
        /// 创建要显示的字符
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns></returns>
        public string CreateVerificationText(int length)   //创建验证码字符串
        {
            char[] verification = new char[length];     //定义验证码数组
            //char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] dictionary = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random random = new Random();    //随机
            for (int i = 0; i < length; i++)     //循环取 length 长度个数字
            {
                //产生0到dictionary数组长度的随机值，用这个随机值做dictionary数组的下标取值，保存到验证码数组
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);    //返回随机验证码字符串
        }

        /// <summary>
        /// 模糊字符
        /// </summary>
        /// <param name="validateCode">要模糊的字符</param>
        /// <returns>图片数据</returns>
        public byte[] CreateValidateGraphic(string validateCode)     //创建验证码图片
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 15.5), 28);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 16, (FontStyle.Bold | FontStyle.Italic));  //设置 字体和大小 
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
