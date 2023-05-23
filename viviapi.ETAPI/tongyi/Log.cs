using System;
using System.IO;
using System.Web;

namespace viviapi.ETAPI.tongyi
{
    public class Log
    {
        public static string path = HttpContext.Current.Request.PhysicalApplicationPath + "logs";

        static Log()
        {
        }

        public static void Debug(string className, string content)
        {
        }

        public static void Info(string className, string content)
        {
        }

        public static void Error(string className, string content)
        {
        }

        public static void WriteLog(string type, string className, string content)
        {
            if (!Directory.Exists(Log.path))
                Directory.CreateDirectory(Log.path);
            string str1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            StreamWriter streamWriter = File.AppendText(Log.path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            string str2 = str1 + " " + type + " " + className + ": " + content;
            streamWriter.WriteLine(str2);
            streamWriter.Close();
        }
    }
}

