using System;
using System.Globalization;
using System.IO;
using System.Text;
using viviLib.Configuration;
using viviLib.ExceptionHandling;

namespace viviLib.Web
{
    public sealed class IPHelper
    {
        internal static readonly string PATH_BASE = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configurations\\IP\\");

        private IPHelper()
        {
        }

        public static string FormatIP(string ip)
        {
            string[] strArray = ip.Trim().Split('.');
            for (int index = 0; index < strArray.Length; ++index)
                strArray[index] = strArray[index].Length >= 3 ? strArray[index] : new string('0', 3 - strArray[index].Length) + strArray[index];
            return string.Join(".", strArray);
        }

        public static string GetArea(string ip)
        {
            return IPHelper.GetArea(IPHelper.PATH_BASE, ip);
        }

        public static string GetArea(string pathBase, string ip)
        {
            return IPHelper.GetArea(pathBase, ip, CultureInfo.CurrentCulture);
        }

        public static string GetArea(string pathBase, string ip, CultureInfo culture)
        {
            try
            {
                int range = Convert.ToInt32(ip.Substring(0, ip.IndexOf(".")), 10);
                string path = IPHelper.GetPath(pathBase, range, culture);
                if (!File.Exists(path))
                    path = IPHelper.GetPath(pathBase, range, ConfigHelper.DefaultCulture);
                ip = IPHelper.FormatIP(ip);
                string str1 = string.Empty;
                if (File.Exists(path))
                {
                    using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8, true))
                    {
                        for (string str2 = streamReader.ReadLine(); str2 != null; str2 = streamReader.ReadLine())
                        {
                            string[] strArray = str2.Split('-');
                            if (strArray.Length > 2 && string.Compare(ip, strArray[0]) >= 0 && string.Compare(ip, strArray[1]) <= 0)
                            {
                                str1 = strArray[2];
                                break;
                            }
                        }
                        streamReader.Close();
                    }
                    return str1;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return string.Empty;
        }

        public static string GetIP(long ipValue)
        {
            return string.Join(".", new string[4]
            {
        string.Format("{0:000}", (object) Convert.ToInt64(Math.Floor((double) ipValue / Math.Pow(256.0, 3.0)))),
        string.Format("{0:000}", (object) (Convert.ToInt64(Math.Floor((double) ipValue / Math.Pow(256.0, 2.0))) % 256L)),
        string.Format("{0:000}", (object) (Convert.ToInt64(Math.Floor((double) (ipValue / 256L))) % 256L)),
        string.Format("{0:000}", (object) (ipValue % 256L))
            });
        }

        public static string GetPath(int range)
        {
            return IPHelper.GetPath(IPHelper.PATH_BASE, range);
        }

        public static string GetPath(string pathBase, int range)
        {
            return IPHelper.GetPath(pathBase, range, CultureInfo.CurrentCulture);
        }

        public static string GetPath(string pathBase, int range, CultureInfo culture)
        {
            if (pathBase != null && pathBase.Trim().Length != 0)
                return Path.Combine(Path.Combine(pathBase, culture.Name), string.Format("{0:000}.000.000.000-{0:000}.255.255.255.txt", (object)range));
            return Path.Combine(Path.Combine(IPHelper.PATH_BASE, culture.Name), string.Format("{0:000}.000.000.000-{0:000}.255.255.255.txt", (object)range));
        }

        public static long GetValue(string ip)
        {
            if (ip != null && ip.Length != 0)
            {
                try
                {
                    long num = 0L;
                    string[] strArray = ip.Trim().Split('.');
                    for (int index = 0; index < 4; ++index)
                        num += (long)Convert.ToInt32(strArray[index], 10) * Convert.ToInt64(Math.Pow(256.0, (double)(3 - index)));
                    return num;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
            return 0L;
        }
    }
}
