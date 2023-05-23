using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace viviLib
{
    public class Utility
    {
        public static string[] Monthes
        {
            get
            {
                return new string[12]
                {
          "January",
          "February",
          "March",
          "April",
          "May",
          "June",
          "July",
          "August",
          "September",
          "October",
          "November",
          "December"
                };
            }
        }

        public static string ChkSQL(string str)
        {
            if (str == null)
                return "";
            str = str.Replace("'", "''");
            return str;
        }

        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), "[^\\w\\.@-]", "");
        }

        public static string ClearBR(string str)
        {
            for (Match match = new Regex("(\\r\\n)", RegexOptions.IgnoreCase).Match(str); match.Success; match = match.NextMatch())
                str = str.Replace(match.Groups[0].ToString(), "");
            return str;
        }

        public static string ClearHtml(string strHtml)
        {
            if (strHtml != "")
            {
                for (Match match = new Regex("<\\/?[^>]*>", RegexOptions.IgnoreCase).Match(strHtml); match.Success; match = match.NextMatch())
                    strHtml = strHtml.Replace(match.Groups[0].ToString(), "");
            }
            return strHtml;
        }

        public static bool CreateDir(string name)
        {
            return Utility.MakeSureDirectoryPathExists(name);
        }

        public static string CutString(string str, int startIndex)
        {
            return Utility.CutString(str, startIndex, str.Length);
        }

        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length *= -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                        startIndex -= length;
                }
                if (startIndex > str.Length)
                    return "";
            }
            else
            {
                if (length < 0 || length + startIndex <= 0)
                    return "";
                length += startIndex;
                startIndex = 0;
            }
            if (str.Length - startIndex < length)
                length = str.Length - startIndex;
            try
            {
                return str.Substring(startIndex, length);
            }
            catch
            {
                return str;
            }
        }

        public static string EncodeHtml(string strHtml)
        {
            if (!(strHtml != ""))
                return "";
            strHtml = strHtml.Replace(",", "&def");
            strHtml = strHtml.Replace("'", "&dot");
            strHtml = strHtml.Replace(";", "&dec");
            return strHtml;
        }

        public static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        public static string[] FindNoUTF8File(string Path)
        {
            StringBuilder stringBuilder = new StringBuilder();
            FileInfo[] files = new DirectoryInfo(Path).GetFiles();
            for (int index = 0; index < files.Length; ++index)
            {
                if (files[index].Extension.ToLower().Equals(".htm"))
                {
                    FileStream sbInputStream = new FileStream(files[index].FullName, FileMode.Open, FileAccess.Read);
                    bool flag = Utility.IsUTF8(sbInputStream);
                    sbInputStream.Close();
                    if (!flag)
                    {
                        stringBuilder.Append(files[index].FullName);
                        stringBuilder.Append("\r\n");
                    }
                }
            }
            return Utility.SplitString(stringBuilder.ToString(), "\r\n");
        }

        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            if (bytes > 1048576)
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            if (bytes > 1024)
                return ((double)(bytes / 1024)).ToString("0") + "K";
            return bytes.ToString() + "Bytes";
        }

        public static string GetAssemblyCopyright()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright;
        }

        public static string GetAssemblyProductName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
        }

        public static string GetAssemblyVersion()
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return string.Format("{0}.{1}.{2}", (object)versionInfo.FileMajorPart, (object)versionInfo.FileMinorPart, (object)versionInfo.FileBuildPart);
        }

        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            return "";
        }

        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
                return replacestr;
            if (datetimestr.Equals(""))
                return replacestr;
            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }

        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTime(int relativeday)
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays((double)relativeday);
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
                return "";
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        public static string GetFilename(string url)
        {
            if (url == null)
                return "";
            string[] strArray = url.Split('/');
            return strArray[strArray.Length - 1].Split('?')[0];
        }

        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return Utility.GetInArrayID(strSearch, stringArray, true);
        }

        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int index = 0; index < stringArray.Length; ++index)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[index].ToLower())
                        return index;
                }
                else if (strSearch == stringArray[index])
                    return index;
            }
            return -1;
        }

        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(strPath);
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        public static string GetPageNumbers(int page, int pageSize, int Count, string Url)
        {
            string str1 = "";
            int num1 = page - 1;
            int num2 = page + 1;
            int num3 = (int)Math.Ceiling((double)Count / (double)pageSize);
            string str2 = str1 + (object)"<span>页码：" + (string)(object)page + "/" + (string)(object)num3 + "</span>";
            string str3;
            if (num1 < 1)
                str3 = str2 + "<span title='首页'>首页</span><span title='上一页'>上一页</span>";
            else
                str3 = str2 + "<span title='首页'><a href='" + Url + "=1'>首页</a></span>" + (object)"<span title='上一页'><a href='" + Url + "=" + (string)(object)num1 + "'>上一页</a></span>";
            int num4 = page % pageSize != 0 ? page - page % pageSize + 1 : page - pageSize + 1;
            if (num4 > pageSize)
                str3 = str3 + (object)"<span title='前" + (string)(object)pageSize + "页'><a href='" + Url + "=" + (string)(object)(num4 - 1) + "'>...</a></span>";
            for (int index = num4; index < num4 + pageSize && index <= num3; ++index)
            {
                if (index == page)
                    str3 = str3 + (object)"<span title='页 " + (string)(object)index + "'> <font color='#ff0000'>[" + (string)(object)index + "]</font> </span>";
                else
                    str3 = str3 + (object)"<span title='页 " + (string)(object)index + "'> <a href='" + Url + "=" + (string)(object)index + "'>[" + (string)(object)index + "]</a> </span>";
            }
            if (num3 >= num4 + pageSize)
                str3 = str3 + (object)"<span title='后" + (string)(object)pageSize + "页'><a href='" + Url + "=" + (string)(object)(num4 + pageSize) + "'>...</a></span>";
            if (num2 > num3)
                return str3 + "<span title='下一页'>下一页</span><span title='末页'>末页</span>";
            return str3 + (object)"<span title='下一页'><a href='" + Url + "=" + (string)(object)num2 + "'>下一页</a></span>" + (object)"<span title='末页'><a href='" + Url + "=" + (string)(object)num3 + "'>末页</a></span>";
        }

        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor)
        {
            if (pagetag == "")
                pagetag = "page";
            int num1 = 1;
            url = url.IndexOf("?") <= 0 ? url + "?" : url + "&";
            string str1 = "<a href=\"" + url + "&" + pagetag + "=1";
            string str2 = "<a href=\"" + (object)url + "&" + pagetag + "=" + (string)(object)countPage;
            if (anchor != null)
            {
                str1 += anchor;
                str2 += anchor;
            }
            string str3 = str1 + "\">第一页</a>";
            string str4 = str2 + "\">最后一页</a>";
            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;
            int num2;
            if (countPage > extendPage)
            {
                if (curPage - extendPage / 2 > 0)
                {
                    if (curPage + extendPage / 2 < countPage)
                    {
                        num1 = curPage - extendPage / 2;
                        num2 = num1 + extendPage - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num1 = num2 - extendPage + 1;
                        str4 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    str3 = "";
                }
            }
            else
            {
                num1 = 1;
                num2 = countPage;
                str3 = "";
                str4 = "";
            }
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(str3);
            for (int index = num1; index <= num2; ++index)
            {
                if (index == curPage)
                {
                    stringBuilder.Append("<span>");
                    stringBuilder.Append(index);
                    stringBuilder.Append("</span>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"");
                    stringBuilder.Append(url);
                    stringBuilder.Append(pagetag);
                    stringBuilder.Append("=");
                    stringBuilder.Append(index);
                    if (anchor != null)
                        stringBuilder.Append(anchor);
                    stringBuilder.Append("\">");
                    if (num1 > 1 && index == num1)
                        stringBuilder.Append("...");
                    stringBuilder.Append(index);
                    if (num2 < countPage && index == num2)
                        stringBuilder.Append("...");
                    stringBuilder.Append("</a>");
                }
            }
            stringBuilder.Append(str4);
            return stringBuilder.ToString();
        }

        public static string GetPostPageNumbers(int countPage, string url, string expname, int extendPage)
        {
            int num1 = 1;
            int num2 = 1;
            string str1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>&nbsp;";
            string str2 = "<a href=\"" + (object)url + "-" + (string)(object)countPage + expname + "\">&raquo;</a>&nbsp;";
            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;
            int num3;
            if (countPage > extendPage)
            {
                if (num2 - extendPage / 2 > 0)
                {
                    if (num2 + extendPage / 2 < countPage)
                    {
                        num1 = num2 - extendPage / 2;
                        num3 = num1 + extendPage - 1;
                    }
                    else
                    {
                        num3 = countPage;
                        num1 = num3 - extendPage + 1;
                        str2 = "";
                    }
                }
                else
                {
                    num3 = extendPage;
                    str1 = "";
                }
            }
            else
            {
                num1 = 1;
                num3 = countPage;
                str1 = "";
                str2 = "";
            }
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(str1);
            for (int index = num1; index <= num3; ++index)
            {
                stringBuilder.Append("&nbsp;<a href=\"");
                stringBuilder.Append(url);
                stringBuilder.Append("-");
                stringBuilder.Append(index);
                stringBuilder.Append(expname);
                stringBuilder.Append("\">");
                stringBuilder.Append(index);
                stringBuilder.Append("</a>&nbsp;");
            }
            stringBuilder.Append(str2);
            return stringBuilder.ToString();
        }

        public static string GetStandardDateTime(string fDateTime)
        {
            return Utility.GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            return Convert.ToDateTime(fDateTime).ToString(formatStr);
        }

        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            int num1 = 1;
            string str1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>&nbsp;";
            string str2 = "<a href=\"" + (object)url + "-" + (string)(object)countPage + expname + "\">&raquo;</a>&nbsp;";
            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;
            int num2;
            if (countPage > extendPage)
            {
                if (curPage - extendPage / 2 > 0)
                {
                    if (curPage + extendPage / 2 < countPage)
                    {
                        num1 = curPage - extendPage / 2;
                        num2 = num1 + extendPage - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num1 = num2 - extendPage + 1;
                        str2 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    str1 = "";
                }
            }
            else
            {
                num1 = 1;
                num2 = countPage;
                str1 = "";
                str2 = "";
            }
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(str1);
            for (int index = num1; index <= num2; ++index)
            {
                if (index == curPage)
                {
                    stringBuilder.Append("&nbsp;");
                    stringBuilder.Append(index);
                    stringBuilder.Append("&nbsp;");
                }
                else
                {
                    stringBuilder.Append("&nbsp;<a href=\"");
                    stringBuilder.Append(url);
                    stringBuilder.Append("-");
                    stringBuilder.Append(index);
                    stringBuilder.Append(expname);
                    stringBuilder.Append("\">");
                    stringBuilder.Append(index);
                    stringBuilder.Append("</a>&nbsp;");
                }
            }
            stringBuilder.Append(str2);
            return stringBuilder.ToString();
        }

        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            string str = p_SrcString;
            if (p_Length < 0)
                return str;
            byte[] bytes1 = Encoding.Default.GetBytes(p_SrcString);
            if (bytes1.Length <= p_Length)
                return str;
            int length = p_Length;
            int[] numArray = new int[p_Length];
            int num = 0;
            for (int index = 0; index < p_Length; ++index)
            {
                if ((int)bytes1[index] > (int)sbyte.MaxValue)
                {
                    ++num;
                    if (num == 3)
                        num = 1;
                }
                else
                    num = 0;
                numArray[index] = num;
            }
            if ((int)bytes1[p_Length - 1] > (int)sbyte.MaxValue && numArray[p_Length - 1] == 1)
                length = p_Length + 1;
            byte[] bytes2 = new byte[length];
            Array.Copy((Array)bytes1, (Array)bytes2, length);
            return Encoding.Default.GetString(bytes2) + p_TailString;
        }

        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        public static string GetTrueForumPath()
        {
            string path = HttpContext.Current.Request.Path;
            if (path.LastIndexOf("/") != path.IndexOf("/"))
                return path.Substring(path.IndexOf("/"), path.LastIndexOf("/") + 1);
            return "/";
        }

        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static bool InArray(string str, string stringarray)
        {
            return Utility.InArray(str, Utility.SplitString(stringarray, ","), false);
        }

        public static bool InArray(string str, string[] stringarray)
        {
            return Utility.InArray(str, stringarray, false);
        }

        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return Utility.InArray(str, Utility.SplitString(stringarray, strsplit), false);
        }

        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return Utility.GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return Utility.InArray(str, Utility.SplitString(stringarray, strsplit), caseInsensetive);
        }

        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] strArray1 = Utility.SplitString(ip, ".");
            for (int index1 = 0; index1 < iparray.Length; ++index1)
            {
                string[] strArray2 = Utility.SplitString(iparray[index1], ".");
                int num = 0;
                for (int index2 = 0; index2 < strArray2.Length; ++index2)
                {
                    if (strArray2[index2] == "*")
                        return true;
                    if (strArray1.Length > index2 && strArray2[index2] == strArray1[index2])
                        ++num;
                    else
                        break;
                }
                if (num == 4)
                    return true;
            }
            return false;
        }

        public static string IntToStr(int intValue)
        {
            return Convert.ToString(intValue);
        }

        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, "[A-Za-z0-9\\+\\/\\=]");
        }

        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if (stringarray != "" && stringarray != null)
            {
                str = str.ToLower();
                foreach (string str1 in Utility.SplitString(stringarray.ToLower(), strsplit))
                {
                    if (str.IndexOf(str1) > -1)
                        return true;
                }
            }
            return false;
        }

        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, "(\\d{4})-(\\d{1,2})-(\\d{1,2})");
        }

        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
                return false;
            string str = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return str == "jpg" || str == "jpeg" || (str == "png" || str == "bmp") || str == "gif";
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }

        public static bool IsNumber(string strNumber)
        {
            return new Regex("^([0-9])[0-9]*(\\.\\w*)?$").IsMatch(strNumber);
        }

        public static bool IsNumberArray(string[] strNumber)
        {
            if (strNumber == null || strNumber.Length < 1)
                return false;
            foreach (string strNumber1 in strNumber)
            {
                if (!Utility.IsNumber(strNumber1))
                    return false;
            }
            return true;
        }

        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");
        }

        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, "/^\\s*$|^c:\\\\con\\\\con$|[%,\\*\"\\s\\t\\<\\>\\&]|$guestexp/is");
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, "^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        private static bool IsUTF8(FileStream sbInputStream)
        {
            bool flag = true;
            long length = sbInputStream.Length;
            byte num1 = (byte)0;
            for (int index = 0; (long)index < length; ++index)
            {
                byte num2 = (byte)sbInputStream.ReadByte();
                if (((int)num2 & 128) != 0)
                    flag = false;
                if ((int)num1 == 0)
                {
                    if ((int)num2 >= 128)
                    {
                        do
                        {
                            num2 <<= 1;
                            ++num1;
                        }
                        while (((int)num2 & 128) != 0);
                        --num1;
                        if ((int)num1 == 0)
                            return false;
                    }
                }
                else
                {
                    if (((int)num2 & 192) != 128)
                        return false;
                    --num1;
                }
            }
            return (int)num1 <= 0 && !flag;
        }

        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }

        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);

        public static string mashSQL(string str)
        {
            if (str == null)
                return "";
            str = str.Replace("'", "'");
            return str;
        }

        public static string MD5(string str)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(str));
            string str1 = "";
            for (int index = 0; index < hash.Length; ++index)
                str1 += hash[index].ToString("x").PadLeft(2, '0');
            return str1;
        }

        public static int RandomInt(int _up, int _down)
        {
            return new Random().Next(_up, _down);
        }

        public static string RemoveHtml(string content)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        public static string ReplaceStrToScript(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream stream = (Stream)null;
            byte[] buffer = new byte[10000];
            try
            {
                stream = (Stream)new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                long num = stream.Length;
                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Utility.UrlEncode(filename.Trim()).Replace("+", " "));
                while (num > 0L)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
                        HttpContext.Current.Response.Flush();
                        buffer = new byte[10000];
                        num -= (long)count;
                    }
                    else
                        num = -1L;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            HttpContext.Current.Response.End();
        }

        public static string RTrim(string str)
        {
            for (int length = str.Length; length >= 0; --length)
            {
                char ch = str[length];
                if (!ch.Equals((object)" "))
                    ch = str[length];
                if (ch.Equals((object)"\r") || (ch = str[length]).Equals((object)"\n"))
                    str.Remove(length, 1);
            }
            return str;
        }

        public static int SafeInt32(object objNum)
        {
            if (objNum != null)
            {
                string str = objNum.ToString();
                if (Utility.IsNumber(str))
                {
                    if (str.ToString().Length > 9)
                        return int.MaxValue;
                    return int.Parse(str);
                }
            }
            return 0;
        }

        public static string SHA256(string str)
        {
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(str)));
        }

        public static string Spaces(int nSpaces)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < nSpaces; ++index)
                stringBuilder.Append(" &nbsp;&nbsp;");
            return stringBuilder.ToString();
        }

        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) >= 0)
                return Regex.Split(strContent, strSplit.Replace(".", "\\."), RegexOptions.IgnoreCase);
            return new string[1]
            {
        strContent
            };
        }

        public static int StrDateDiffHours(string time, int hours)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan timeSpan = DateTime.Now - DateTime.Parse(time).AddHours((double)hours);
            if (timeSpan.TotalHours > (double)int.MaxValue)
                return int.MaxValue;
            if (timeSpan.TotalHours < (double)int.MinValue)
                return int.MinValue;
            return (int)timeSpan.TotalHours;
        }

        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan timeSpan = DateTime.Now - DateTime.Parse(time).AddMinutes((double)minutes);
            if (timeSpan.TotalMinutes > (double)int.MaxValue)
                return int.MaxValue;
            if (timeSpan.TotalMinutes < (double)int.MinValue)
                return int.MinValue;
            return (int)timeSpan.TotalMinutes;
        }

        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Time).AddSeconds((double)Sec);
            if (timeSpan.TotalSeconds > (double)int.MaxValue)
                return int.MaxValue;
            if (timeSpan.TotalSeconds < (double)int.MinValue)
                return int.MinValue;
            return (int)timeSpan.TotalSeconds;
        }

        public static string StrFilter(string str, string bantext)
        {
            string[] strArray = Utility.SplitString(bantext, "\r\n");
            for (int index = 0; index < strArray.Length; ++index)
            {
                string oldValue = strArray[index].Substring(0, strArray[index].IndexOf("="));
                string newValue = strArray[index].Substring(strArray[index].IndexOf("=") + 1);
                str = str.Replace(oldValue, newValue);
            }
            return str;
        }

        public static string StrFormat(string str)
        {
            if (str == null)
                return "";
            str = str.Replace("\r\n", "<br />");
            str = str.Replace("\n", "<br />");
            return str;
        }

        public static float StrToFloat(object strValue, float defValue)
        {
            if (strValue == null || strValue.ToString().Length > 10)
                return defValue;
            float num = defValue;
            if (strValue != null && new Regex("^([-]|[0-9])[0-9]*(\\.\\w*)?$").IsMatch(strValue.ToString()))
                num = Convert.ToSingle(strValue);
            return num;
        }

        public static int StrToInt(object strValue, int defValue)
        {
            if (strValue == null || strValue.ToString() == string.Empty || strValue.ToString().Length > 10)
                return defValue;
            string str1 = strValue.ToString();
            string str2 = str1[0].ToString();
            if (str1.Length == 10 && Utility.IsNumber(str2) && int.Parse(str2) > 1 || str1.Length == 10 && !Utility.IsNumber(str2))
                return defValue;
            int num = defValue;
            if (strValue != null && new Regex("^([-]|[0-9])[0-9]*$").IsMatch(strValue.ToString()))
                num = Convert.ToInt32(strValue);
            return num;
        }

        public static string ToSChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        public static string ToTChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        public void transHtml(string path, string outpath)
        {
            Page page = new Page();
            StringWriter stringWriter = new StringWriter();
            page.Server.Execute(path, (TextWriter)stringWriter);
            FileStream fileStream;
            if (File.Exists(page.Server.MapPath("") + "\\" + outpath))
            {
                File.Delete(page.Server.MapPath("") + "\\" + outpath);
                fileStream = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            else
                fileStream = File.Create(page.Server.MapPath("") + "\\" + outpath);
            byte[] bytes = Encoding.Default.GetBytes(stringWriter.ToString());
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes((double)expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}
