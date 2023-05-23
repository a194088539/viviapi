using System;
using System.IO;
using System.Text;

namespace com.yeepay
{
    public abstract class ErrLog
    {
        private static string logFilePath = "G:\\易宝接口开发项目\\com.yeepay\\ErrLog.txt";

        public static void Write(string strLog)
        {
            if (!File.Exists(ErrLog.logFilePath))
                return;
            try
            {
                StreamWriter streamWriter = new StreamWriter(ErrLog.logFilePath, true, Encoding.GetEncoding("gb2312"));
                streamWriter.WriteLine(DateTime.Now.ToString());
                streamWriter.WriteLine(strLog);
                ((TextWriter)streamWriter).Flush();
                streamWriter.Close();
            }
            catch
            {
            }
        }
    }
}
