using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace viviLib.Utils
{
    public class Common
    {
        private static FileVersionInfo AssemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        private static Regex RegexBr = new Regex("(\\r\\n)", RegexOptions.IgnoreCase);
        public static Regex RegexFont = new Regex("<font color=\".*?\">([\\s\\S]+?)</font>", Common.GetRegexCompiledOptions());
        private static string TemplateCookieName = string.Format("dnttemplateid_{0}_{1}_{2}", (object)Common.AssemblyFileVersion.FileMajorPart, (object)Common.AssemblyFileVersion.FileMinorPart, (object)Common.AssemblyFileVersion.FileBuildPart);

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

        public static string AdDeTime(int times)
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMinutes((double)times);
            return dateTime.ToString();
        }

        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return Common.BackupFile(sourceFileName, destFileName, true);
        }

        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            if (!overwrite && System.IO.File.Exists(destFileName))
                return false;
            bool flag;
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
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
            for (Match match = Common.RegexBr.Match(str); match.Success; match = match.NextMatch())
                str = str.Replace(match.Groups[0].ToString(), "");
            return str;
        }

        public static string ClearLastChar(string str)
        {
            if (str == "")
                return "";
            return str.Substring(0, str.Length - 1);
        }

        public static bool CreateDir(string name)
        {
            return Common.MakeSureDirectoryPathExists(name);
        }

        public static string CutString(string str, int startIndex)
        {
            return Common.CutString(str, startIndex, str.Length);
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
            return str.Substring(startIndex, length);
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
            return System.IO.File.Exists(filename);
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
                    bool flag = Common.IsUTF8(sbInputStream);
                    sbInputStream.Close();
                    if (!flag)
                    {
                        stringBuilder.Append(files[index].FullName);
                        stringBuilder.Append("\r\n");
                    }
                }
            }
            return Common.SplitString(stringBuilder.ToString(), "\r\n");
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

        public static string FormatDate(DateTime dt)
        {
            return (string)(object)dt.Year + (object)"-" + (string)(object)dt.Month + "-" + (string)(object)dt.Day;
        }

        public static string FormatDateYearTwo(DateTime dt)
        {
            return dt.Year.ToString().Substring(2) + (object)"-" + (string)(object)dt.Month + "-" + (string)(object)dt.Day;
        }

        public static string FormatYearMonth(DateTime dt)
        {
            return string.Concat(new object[4]
            {
        (object) dt.Year,
        (object) "-",
        (object) dt.Month,
        (object) "-"
            });
        }

        public static string Get_Https(string a_strUrl, int timeout)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(a_strUrl);
                httpWebRequest.Timeout = timeout;
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default);
                StringBuilder stringBuilder = new StringBuilder();
                while (-1 != streamReader.Peek())
                    stringBuilder.Append(streamReader.ReadLine());
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                return "true";
            }
        }

        public static string GetAssemblyCopyright()
        {
            return Common.AssemblyFileVersion.LegalCopyright;
        }

        public static string GetAssemblyProductName()
        {
            return Common.AssemblyFileVersion.ProductName;
        }

        public static string GetAssemblyVersion()
        {
            return string.Format("{0}.{1}.{2}", (object)Common.AssemblyFileVersion.FileMajorPart, (object)Common.AssemblyFileVersion.FileMinorPart, (object)Common.AssemblyFileVersion.FileBuildPart);
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
            return Common.GetInArrayID(strSearch, stringArray, true);
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

        public static string getIntZero(string j)
        {
            if (j.Length < 2)
                return "0" + j;
            return j;
        }

        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(strPath);
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            return Common.GetPageNumbers(curPage, countPage, url, extendPage, "page");
        }

        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag)
        {
            return Common.GetPageNumbers(curPage, countPage, url, extendPage, pagetag, (string)null);
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
            string str3 = str1 + "\">&laquo;</a>";
            string str4 = str2 + "\">&raquo;</a>";
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
                    stringBuilder.Append(index);
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
            string str1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string str2 = "<a href=\"" + (object)url + "-" + (string)(object)countPage + expname + "\">&raquo;</a>";
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
                stringBuilder.Append("<a href=\"");
                stringBuilder.Append(url);
                stringBuilder.Append("-");
                stringBuilder.Append(index);
                stringBuilder.Append(expname);
                stringBuilder.Append("\">");
                stringBuilder.Append(index);
                stringBuilder.Append("</a>");
            }
            stringBuilder.Append(str2);
            return stringBuilder.ToString();
        }

        public static RegexOptions GetRegexCompiledOptions()
        {
            return RegexOptions.None;
        }

        public static string GetStandardDateTime(string fDateTime)
        {
            return Common.GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
                return fDateTime;
            return Convert.ToDateTime(fDateTime).ToString(formatStr);
        }

        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            int num1 = 1;
            string str1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string str2 = "<a href=\"" + (object)url + "-" + (string)(object)countPage + expname + "\">&raquo;</a>";
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
                    stringBuilder.Append("<span>");
                    stringBuilder.Append(index);
                    stringBuilder.Append("</span>");
                }
                else
                {
                    stringBuilder.Append("<a href=\"");
                    stringBuilder.Append(url);
                    stringBuilder.Append("-");
                    stringBuilder.Append(index);
                    stringBuilder.Append(expname);
                    stringBuilder.Append("\">");
                    stringBuilder.Append(index);
                    stringBuilder.Append("</a>");
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
            return Common.GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }

        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string str = p_SrcString;
            if (Regex.IsMatch(p_SrcString, "[\x0800-一]+") || Regex.IsMatch(p_SrcString, "[가-힣]+"))
            {
                if (p_StartIndex >= p_SrcString.Length)
                    return "";
                return p_SrcString.Substring(p_StartIndex, p_Length + p_StartIndex > p_SrcString.Length ? p_SrcString.Length - p_StartIndex : p_Length);
            }
            if (p_Length < 0)
                return str;
            byte[] bytes1 = Encoding.Default.GetBytes(p_SrcString);
            if (bytes1.Length <= p_StartIndex)
                return str;
            int num1 = bytes1.Length;
            if (bytes1.Length > p_StartIndex + p_Length)
            {
                num1 = p_Length + p_StartIndex;
            }
            else
            {
                p_Length = bytes1.Length - p_StartIndex;
                p_TailString = "";
            }
            int length = p_Length;
            int[] numArray = new int[p_Length];
            int num2 = 0;
            for (int index = p_StartIndex; index < num1; ++index)
            {
                if ((int)bytes1[index] > (int)sbyte.MaxValue)
                {
                    ++num2;
                    if (num2 == 3)
                        num2 = 1;
                }
                else
                    num2 = 0;
                numArray[index] = num2;
            }
            if ((int)bytes1[num1 - 1] > (int)sbyte.MaxValue && numArray[p_Length - 1] == 1)
                length = p_Length + 1;
            byte[] bytes2 = new byte[length];
            Array.Copy((Array)bytes1, p_StartIndex, (Array)bytes2, 0, length);
            return Encoding.Default.GetString(bytes2) + p_TailString;
        }

        public static string GetTemplateCookieName()
        {
            return Common.TemplateCookieName;
        }

        public static string GetTextFromHTML(string HTML)
        {
            return new Regex("</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase).Replace(HTML, "");
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
            return Common.InArray(str, Common.SplitString(stringarray, ","), false);
        }

        public static bool InArray(string str, string[] stringarray)
        {
            return Common.InArray(str, stringarray, false);
        }

        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return Common.InArray(str, Common.SplitString(stringarray, strsplit), false);
        }

        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return Common.GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return Common.InArray(str, Common.SplitString(stringarray, strsplit), caseInsensetive);
        }

        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] strArray1 = Common.SplitString(ip, ".");
            for (int index1 = 0; index1 < iparray.Length; ++index1)
            {
                string[] strArray2 = Common.SplitString(iparray[index1], ".");
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
                foreach (string str1 in Common.SplitString(stringarray.ToLower(), strsplit))
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

        public static bool IsDouble(object Expression)
        {
            return TypeParse.IsDouble(Expression);
        }

        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
                return false;
            string str = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return str == "jpg" || str == "jpeg" || (str == "png" || str == "bmp") || str == "gif";
        }

        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*$");
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){2}((2[0-4]\\d|25[0-5]|[01]?\\d\\d?|\\*)\\.)(2[0-4]\\d|25[0-5]|[01]?\\d\\d?|\\*)$");
        }

        public static bool IsNumberId(string _value)
        {
            return Common.QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        public static bool IsNumeric(object Expression)
        {
            return TypeParse.IsNumeric(Expression);
        }

        public static bool IsNumericArray(string[] strNumber)
        {
            return TypeParse.IsNumericArray(strNumber);
        }

        public static bool IsRuleTip(Hashtable NewHash, string ruletype, out string key)
        {
            key = "";
            foreach (DictionaryEntry entry in NewHash)
            {
                try
                {
                    string[] strArray = SplitString(entry.Value.ToString(), "\r\n");
                    foreach (string str in strArray)
                    {
                        if (str != "")
                        {
                            switch (ruletype.Trim().ToLower())
                            {
                                case "email":
                                    if (!IsValidDoEmail(str.ToString()))
                                    {
                                        throw new Exception();
                                    }
                                    break;

                                case "ip":
                                    if (!IsIPSect(str.ToString()))
                                    {
                                        throw new Exception();
                                    }
                                    break;

                                case "timesect":
                                    {
                                        string[] strArray2 = str.Split(new char[] { '-' });
                                        if (!(IsTime(strArray2[1].ToString()) && IsTime(strArray2[0].ToString())))
                                        {
                                            throw new Exception();
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
                catch
                {
                    key = entry.Key.ToString();
                    return false;
                }
            }
            return true;
        }

        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");
        }

        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, "^\\s*$|^c:\\\\con\\\\con$|[%,\\*\"\\s\\t\\<\\>\\&]|游客|^Guest");
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, "^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, "^(http|https)\\://([a-zA-Z0-9\\.\\-]+(\\:[a-zA-Z0-9\\.&%\\$\\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\\-]+\\.)*[a-zA-Z0-9\\-]+\\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\\:[0-9]+)*(/($|[a-zA-Z0-9\\.\\,\\?\\'\\\\\\+&%\\$#\\=~_\\-]+))*$");
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

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
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

        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null)
                return false;
            Regex regex = new Regex(_express);
            if (_value.Length == 0)
                return false;
            return regex.IsMatch(_value);
        }

        public static string RemoveFontTag(string title)
        {
            Match match = Common.RegexFont.Match(title);
            if (match.Success)
                return match.Groups[1].Value;
            return title;
        }

        public static string RemoveHtml(string content)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, "(\\<|\\s+)o([a-z]+\\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "(script|frame|form|meta|behavior|style)([\\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
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
                stream = (Stream)new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                long num = stream.Length;
                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Common.UrlEncode(filename.Trim()).Replace("+", " "));
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

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return Common.RestoreFile(backupFileName, targetFileName, (string)null);
        }

        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static string RTrim(string str)
        {
            try
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
            catch (Exception ex)
            {
                return str;
            }
        }

        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] chars = SBCCase.ToCharArray();
            for (int index = 0; index < chars.Length; ++index)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(chars, index, 1);
                if (bytes.Length == 2 && (int)bytes[1] == (int)byte.MaxValue)
                {
                    bytes[0] = (byte)((uint)bytes[0] + 32U);
                    bytes[1] = (byte)0;
                    chars[index] = Encoding.Unicode.GetChars(bytes)[0];
                }
            }
            return new string(chars);
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
                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            return new string[1]
            {
        strContent
            };
        }

        public static string[] SplitString(string strContent, string strSplit, int p_3)
        {
            string[] strArray1 = new string[p_3];
            string[] strArray2 = Common.SplitString(strContent, strSplit);
            for (int index = 0; index < p_3; ++index)
                strArray1[index] = index >= strArray2.Length ? string.Empty : strArray2[index];
            return strArray1;
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
            string[] strArray = Common.SplitString(bantext, "\r\n");
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

        public static bool StrToBool(object Expression, bool defValue)
        {
            return TypeParse.StrToBool(Expression, defValue);
        }

        public static float StrToFloat(object strValue, float defValue)
        {
            return TypeParse.StrToFloat(strValue, defValue);
        }

        public static int StrToInt(object Expression, int defValue)
        {
            return TypeParse.StrToInt(Expression, defValue);
        }

        public static Color ToColor(string color)
        {
            color = color.TrimStart('#');
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length)
            {
                case 3:
                    char[] chArray1 = color.ToCharArray();
                    return Color.FromArgb(Convert.ToInt32(chArray1[0].ToString() + chArray1[0].ToString(), 16), Convert.ToInt32(chArray1[1].ToString() + chArray1[1].ToString(), 16), Convert.ToInt32(chArray1[2].ToString() + chArray1[2].ToString(), 16));
                case 6:
                    char[] chArray2 = color.ToCharArray();
                    return Color.FromArgb(Convert.ToInt32(chArray2[0].ToString() + chArray2[1].ToString(), 16), Convert.ToInt32(chArray2[2].ToString() + chArray2[3].ToString(), 16), Convert.ToInt32(chArray2[4].ToString() + chArray2[5].ToString(), 16));
                default:
                    return Color.FromName(color);
            }
        }

        public void transHtml(string path, string outpath)
        {
            Page page = new Page();
            StringWriter stringWriter = new StringWriter();
            page.Server.Execute(path, (TextWriter)stringWriter);
            FileStream fileStream;
            if (System.IO.File.Exists(page.Server.MapPath("") + "\\" + outpath))
            {
                System.IO.File.Delete(page.Server.MapPath("") + "\\" + outpath);
                fileStream = System.IO.File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            else
                fileStream = System.IO.File.Create(page.Server.MapPath("") + "\\" + outpath);
            byte[] bytes = Encoding.Default.GetBytes(stringWriter.ToString());
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        private static string Unicode2UnitCharacter(string str)
        {
            if (str.Length != 4)
                return str;
            try
            {
                byte num = Convert.ToByte(str.Substring(0, 2), 16);
                return Encoding.Unicode.GetString(new byte[2]
                {
          Convert.ToByte(str.Substring(2), 16),
          num
                });
            }
            catch (Exception ex)
            {
                return str;
            }
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
