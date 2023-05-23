namespace viviapi.WebUI
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    public class vercode : Page
    {
        public static string CheckValiDateNo(string str)
        {
            object obj2 = HttpContext.Current.Session["CCode"];
            if (obj2 == null)
            {
                return "验证码失效";
            }
            if (obj2.ToString().Equals(str.ToUpper()))
            {
                return "";
            }
            return "验证码不正确，请重新输入！";
        }

        public static void CreateCheckCodeImage(int count)
        {
            string s = GenerateCheckCode(count);
            if ((s != null) && (s.Trim() != string.Empty))
            {
                Bitmap image = new Bitmap((int)Math.Ceiling((double)(s.Length * 12.5)), 0x16);
                Graphics graphics = Graphics.FromImage(image);
                try
                {
                    Random random = new Random();
                    graphics.Clear(Color.White);
                    for (int i = 0; i < 0x19; i++)
                    {
                        int num2 = random.Next(image.Width);
                        int num3 = random.Next(image.Width);
                        int num4 = random.Next(image.Height);
                        int num5 = random.Next(image.Height);
                        graphics.DrawLine(new Pen(Color.Silver), num2, num4, num3, num5);
                    }
                    Font font = new Font("Arial", 12f, FontStyle.Italic | FontStyle.Bold);
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Green, Color.OrangeRed, 1.2f, true);
                    graphics.DrawString(s, font, brush, (float)2f, (float)2f);
                    for (int j = 0; j < 100; j++)
                    {
                        int x = random.Next(image.Width);
                        int y = random.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(random.Next()));
                    }
                    graphics.DrawRectangle(new Pen(Color.Green), 0, 0, image.Width - 1, image.Height - 1);
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, ImageFormat.Gif);
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ContentType = "image/Gif";
                    HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                }
                finally
                {
                    graphics.Dispose();
                    image.Dispose();
                }
            }
        }

        private static string GenerateCheckCode(int count)
        {
            string str = new Random().Next(0x2710, 0x1869f).ToString();
            HttpContext.Current.Session["CCode"] = str;
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int width = 0x44;
            int height = 0x1d;
            int num3 = 1;
            base.Response.Cache.SetNoStore();
            Bitmap image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            Color[] colorArray = new Color[] { Color.AliceBlue, Color.Aqua, Color.Black, Color.Brown, Color.DarkRed, Color.SkyBlue, Color.Silver, Color.Tan, Color.Violet, Color.SpringGreen };
            try
            {
                int num4;
                Random random = new Random();
                graphics.Clear(Color.White);
                for (num4 = 0; num4 < 2; num4++)
                {
                    int num5 = random.Next(image.Width) - num3;
                    int num6 = random.Next(image.Width) - num3;
                    int num7 = random.Next(image.Height);
                    int num8 = random.Next(image.Height);
                    graphics.DrawLine(new Pen(Color.Silver), num5, num7, num6, num8);
                }
                string[] strArray = "3,4,5,6,7,9,0,1,2,8,A,B,C,D,E,F".Split(new char[] { ',' });
                string s = string.Empty;
                for (num4 = 0; num4 < 5; num4++)
                {
                    s = s + strArray[random.Next(strArray.Length)];
                }
                Font font = new Font("Verdana", 12f, FontStyle.Bold);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width - num3, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                graphics.DrawString(s, font, brush, (float)random.Next(image.Width - width), (float)random.Next(image.Height - 20));
                for (num4 = 0; num4 < 50; num4++)
                {
                }
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                this.Session["CCode"] = s.ToLower();
            }
            finally
            {
                graphics.Dispose();
                image.Dispose();
            }
        }
    }
}

