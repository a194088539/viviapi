using System;
using System.IO;
using System.Text;
using System.Web;

namespace viviLib.Utils
{
    public class DirFile
    {
        public static void CreateDir(string dir)
        {
            if (dir.Length == 0 || Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
                return;
            Directory.CreateDirectory(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
        }

        public static void CreateFile(string dir, string pagestr)
        {
            dir = dir.Replace("/", "\\");
            if (dir.IndexOf("\\") > -1)
                DirFile.CreateDir(dir.Substring(0, dir.LastIndexOf("\\")));
            StreamWriter streamWriter = new StreamWriter(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir, false, Encoding.GetEncoding("GB2312"));
            streamWriter.Write(pagestr);
            streamWriter.Close();
        }

        public static void DeleteDir(string dir)
        {
            if (dir.Length == 0 || !Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
                return;
            Directory.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
        }

        public static void DeleteFile(string file)
        {
            if (!File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + file))
                return;
            File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + file);
        }

        public static bool ExistsFile(string tempDir)
        {
            return File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + tempDir);
        }

        public static string GetDateDir()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string GetDateFile()
        {
            return DateTime.Now.ToString("HHmmssff");
        }

        public static void MoveFile(string dir1, string dir2)
        {
            dir1 = dir1.Replace("/", "\\");
            dir2 = dir2.Replace("/", "\\");
            if (!File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1))
                return;
            File.Move(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1, HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir2);
        }
    }
}
