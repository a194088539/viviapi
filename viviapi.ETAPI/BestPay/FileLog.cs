using System.IO;
using System.Web;

namespace viviapi.ETAPI.BestPay
{
    public class FileLog
    {
        private static string LogDirectory = "Logs";

        public static void SaveString(string FileName, string Content, HttpServerUtility Server)
        {
            FileLog.WriteToFile(FileName, Content, Server);
        }

        public static void SaveQuery(string FileName, HttpServerUtility Server, HttpRequest Request)
        {
            string str1 = string.Empty;
            string str2 = "----Request.QueryString---\r\n";
            foreach (string index in Request.QueryString.Keys)
                str2 = str2 + index + ":" + Request.QueryString[index] + "\r\n";
            string Content = str2 + "----Request.Form---\r\n";
            foreach (string index in Request.Form.Keys)
                Content = Content + index + ":" + Request.Form[index] + "\r\n";
            FileLog.WriteToFile(FileName, Content, Server);
        }

        private static void WriteToFile(string FileName, string Content, HttpServerUtility Server)
        {
            FileLog.CreateDirectory(Server);
            try
            {
                StreamWriter streamWriter = new StreamWriter(Server.MapPath(FileLog.LogDirectory) + "\\" + FileName + CryptTool.getCurrentDate() + ".txt");
                streamWriter.Write(Content);
                streamWriter.Close();
            }
            catch
            {
            }
        }

        private static void CreateDirectory(HttpServerUtility Server)
        {
            try
            {
                if (Directory.Exists(Server.MapPath(FileLog.LogDirectory)))
                    return;
                Directory.CreateDirectory(Server.MapPath(FileLog.LogDirectory));
            }
            catch
            {
            }
        }
    }
}
