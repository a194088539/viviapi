using System;
using System.IO;

namespace com.yeepay
{
    public class log
    {
        public static string logdir = "c:/";

        public static void logstr(string logFileName, string str)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(log.logdir + logFileName, true);
                streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
                streamWriter.WriteLine("[" + DateTime.Now.ToString() + "]" + str);
                ((TextWriter)streamWriter).Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
            }
        }
    }
}
