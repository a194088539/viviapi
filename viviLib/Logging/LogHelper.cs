using System;
using System.IO;
using System.Text;

namespace viviLib.Logging
{
    public sealed class LogHelper
    {
        public static object obj = new object();

        private LogHelper()
        {
        }

        public static string GetTenPayLogPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles\\payLog\\tenpay.log");
        }

        public static void Write(string str)
        {
            LogHelper.Write(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles\\log.log"), str);
        }

        public static void Write(string path, string str)
        {
            LogHelper.Write(path, str, true);
        }

        public static void Write(string path, string str, bool withSeparator)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            lock (LogHelper.obj)
            {
                using (StreamWriter resource_0 = new StreamWriter(path, true, Encoding.UTF8))
                {
                    if (withSeparator)
                    {
                        resource_0.WriteLine();
                        resource_0.WriteLine(new string('-', 50));
                    }
                    resource_0.WriteLine(str);
                    resource_0.Close();
                }
            }
        }
    }
}
