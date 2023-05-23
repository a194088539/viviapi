using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace viviLib.Image
{
    public class VerifyImage
    {
        private static byte[] randb = new byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
        private static Matrix m = new Matrix();
        private static Bitmap charbmp = new Bitmap(40, 40);
        private static Font[] fonts = new Font[4]
        {
      new Font(new FontFamily("Times New Roman"), (float) (20 + VerifyImage.Next(3)), FontStyle.Regular),
      new Font(new FontFamily("Georgia"), (float) (20 + VerifyImage.Next(3)), FontStyle.Regular),
      new Font(new FontFamily("Arial"), (float) (20 + VerifyImage.Next(3)), FontStyle.Regular),
      new Font(new FontFamily("Comic Sans MS"), (float) (20 + VerifyImage.Next(3)), FontStyle.Regular)
        };

        private static int Next(int max)
        {
            VerifyImage.rand.GetBytes(VerifyImage.randb);
            int num = BitConverter.ToInt32(VerifyImage.randb, 0) % (max + 1);
            if (num < 0)
                num = -num;
            return num;
        }

        private static int Next(int min, int max)
        {
            return VerifyImage.Next(max - min) + min;
        }

        public VerifyImageInfo GenerateImage(string code, int width, int height, Color bgcolor, int textcolor)
        {
            VerifyImageInfo verifyImageInfo = new VerifyImageInfo();
            verifyImageInfo.ImageFormat = ImageFormat.Jpeg;
            verifyImageInfo.ContentType = "image/pjpeg";
            width = 120;
            height = 40;
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics1 = Graphics.FromImage(bitmap);
            graphics1.SmoothingMode = SmoothingMode.HighSpeed;
            graphics1.Clear(bgcolor);
            int num = textcolor == 2 ? 60 : 0;
            Pen pen = new Pen(Color.FromArgb(VerifyImage.Next(50) + num, VerifyImage.Next(50) + num, VerifyImage.Next(50) + num), 1f);
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(VerifyImage.Next(100), VerifyImage.Next(100), VerifyImage.Next(100)));
            for (int index = 0; index < 3; ++index)
                graphics1.DrawArc(pen, VerifyImage.Next(20) - 10, VerifyImage.Next(20) - 10, VerifyImage.Next(width) + 10, VerifyImage.Next(height) + 10, VerifyImage.Next(-100, 100), VerifyImage.Next(-200, 200));
            Graphics graphics2 = Graphics.FromImage(VerifyImage.charbmp);
            float x = -18f;
            for (int index = 0; index < code.Length; ++index)
            {
                VerifyImage.m.Reset();
                VerifyImage.m.RotateAt((float)(VerifyImage.Next(50) - 25), new PointF((float)(VerifyImage.Next(3) + 7), (float)(VerifyImage.Next(3) + 7)));
                graphics2.Clear(Color.Transparent);
                graphics2.Transform = VerifyImage.m;
                solidBrush.Color = Color.Black;
                x = x + 18f + (float)VerifyImage.Next(5);
                PointF point = new PointF(x, 2f);
                graphics2.DrawString(code[index].ToString(), VerifyImage.fonts[VerifyImage.Next(VerifyImage.fonts.Length - 1)], (Brush)solidBrush, new PointF(0.0f, 0.0f));
                graphics2.ResetTransform();
                graphics1.DrawImage(VerifyImage.charbmp, point);
            }
            solidBrush.Dispose();
            graphics1.Dispose();
            graphics2.Dispose();
            verifyImageInfo.Image = bitmap;
            return verifyImageInfo;
        }
    }
}
