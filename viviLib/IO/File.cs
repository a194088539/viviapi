using System;
using System.IO;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviLib.IO
{
    public sealed class File
    {
        public static string ReadContent(Stream stream, Encoding encoding)
        {
            string str = string.Empty;
            if (stream != null)
            {
                using (StreamReader streamReader = new StreamReader(stream, encoding))
                {
                    str = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            return str;
        }

        public static bool Exists(string path, bool checkDirectory)
        {
            if (!checkDirectory)
                return System.IO.File.Exists(path);
            if (!System.IO.File.Exists(path))
                return Directory.Exists(path);
            return true;
        }

        public static bool Delete(string path)
        {
            try
            {
                if (File.Exists(path, false))
                    File.Delete(path);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static string ReadFile(string filepath)
        {
            if (!File.Exists(filepath, false))
                return "Error: Not Exists " + filepath;
            using (StreamReader streamReader = new StreamReader(filepath, Encoding.GetEncoding("utf-8")))
            {
                string str = streamReader.ReadToEnd();
                streamReader.Close();
                return str;
            }
        }
    }
}
