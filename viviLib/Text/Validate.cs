using System;
using System.Text.RegularExpressions;

namespace viviLib.Text
{
    public class Validate
    {
        public static string ChkSQL(string str)
        {
            if (str == null)
                return "";
            str = str.Replace("'", "''");
            return str;
        }

        public static string[] GetDifDateAndTime(object todate, object fodate)
        {
            string[] strArray = new string[2];
            double num1 = (DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString())).TotalSeconds / 86400.0;
            num1.ToString();
            int length = num1.ToString().Length;
            int startIndex = num1.ToString().LastIndexOf(".");
            int num2 = (int)Math.Round(num1, 10);
            int num3 = (int)(double.Parse("0" + num1.ToString().Substring(startIndex, length - startIndex)) * 24.0);
            strArray[0] = num2.ToString();
            strArray[1] = num3.ToString();
            return strArray;
        }

        public static string GetDifDateAndTime(object todate, object fodate, string v1, string v2, string v3, string v4, string v5, string v6)
        {
            TimeSpan timeSpan = DateTime.Parse(todate.ToString()) - DateTime.Parse(fodate.ToString());
            int num1 = (int)timeSpan.TotalDays / 365;
            int num2 = (int)((timeSpan.TotalDays / 365.0 - (double)(int)(timeSpan.TotalDays / 365.0)) * 12.0);
            int num3 = timeSpan.Days - num1 * 365 - num2 * 30;
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            string str = "";
            if (0 != num1)
                str = str + num1.ToString() + v1;
            if (0 != num2)
                str = str + num2.ToString() + v2;
            if (0 != num3)
                str = str + num3.ToString() + v3;
            if (0 != hours)
                str = str + hours.ToString() + v4;
            if (0 != minutes)
                str = str + minutes.ToString() + v5;
            if (num1 == 0 && num2 == 0 && (num3 == 0 && hours == 0) && 0 == minutes)
                return v6;
            else
                return str;
        }

        public static string[] GetPercence(int a, int b)
        {
            while (true)
            {
                if (a % 2 == 0 && 0 == b % 2)
                {
                    a /= 2;
                    b /= 2;
                }
                else if (a % 3 == 0 && 0 == b % 3)
                {
                    a /= 3;
                    b /= 3;
                }
                else if (a % 5 == 0 && 0 == b % 5)
                {
                    a /= 5;
                    b /= 5;
                }
                else if (a % 7 == 0 && 0 == b % 7)
                {
                    a /= 7;
                    b /= 7;
                }
                else
                    break;
            }
            return new string[2]
      {
        a.ToString(),
        b.ToString()
      };
        }

        public static bool isChinese(string s)
        {
            string pattern = "^[\\u4e00-\\u9fa5]{2,}$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsEmail(string _value)
        {
            return new Regex("^\\w+([-+.]\\w+)*@(\\w+([-.]\\w+)*\\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase).Match(_value).Success;
        }

        public static bool IsInt(string _value)
        {
            return new Regex("^(-){0,1}\\d+$").Match(_value).Success && (long.Parse(_value) <= (long)int.MaxValue && long.Parse(_value) >= (long)int.MinValue);
        }

        public static bool IsIp(string s)
        {
            string pattern = "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){2}((2[0-4]\\d|25[0-5]|[01]?\\d\\d?|\\*)\\.)(2[0-4]\\d|25[0-5]|[01]?\\d\\d?|\\*)$");
        }

        public static bool IsPhysicalPath(string s)
        {
            string pattern = "^\\s*[a-zA-Z]:.*$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsRelativePath(string s)
        {
            return s != null && !(s == string.Empty) && (!s.StartsWith("/") && !s.StartsWith("?") && !Regex.IsMatch(s, "^\\s*[a-zA-Z]{1,10}:.*$"));
        }

        public static bool IsSafety(string s)
        {
            return !Regex.IsMatch(Regex.Replace(s.Replace("%20", " "), "\\s", " "), "select |insert |delete from |count\\(|drop table|update |truncate |asc\\(|mid\\(|char\\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|\"|\\'| or ", RegexOptions.IgnoreCase);
        }

        public static bool IsUnicode(string s)
        {
            string pattern = "^[\\u4E00-\\u9FA5\\uE815-\\uFA29]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUnsFlaot(string s)
        {
            string pattern = "^[0-9]+.?[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUnsNumeric(string s)
        {
            string pattern = "^[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(source, "^((h|H)(t|T)(t|T)(p|P)|(f|F)(t|T)(p|P)|(f|F)(i|I)(l|L)(e|E)|(t|T)(e|E)(l|L)(n|N)(e|E)(t|T)|(g|G)(o|O)(p|P)(h|H)(e|E)(r|R)|(h|H)(t|T)(t|T)(p|P)(s|S)|(m|M)(a|A)(i|I)(l|L)(t|T)(o|O)|(n|N)(e|E)(w|W)(s|S)|(w|W)(a|A)(i|I)(s|S))://([\\w-]+(\\.)?)+[\\w-]+(:\\d+)?(/[\\w- ./?%&=]*)?$", RegexOptions.IgnoreCase);
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }

        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        }

        public static bool IsValiduserName(string strUsername)
        {
            return Regex.IsMatch(strUsername, "^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){5,19}$");
        }

        public static bool IsIDCard(string _value)
        {
            if (_value.Length != 15 && _value.Length != 18)
                return false;
            DateTime dateTime;
            if (_value.Length == 15)
            {
                Regex regex = new Regex("^(\\d{6})(\\d{2})(\\d{2})(\\d{2})(\\d{3})$");
                if (!regex.Match(_value).Success)
                    return false;
                string[] strArray = regex.Split(_value);
                try
                {
                    dateTime = new DateTime(int.Parse("19" + strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                Regex regex = new Regex("^(\\d{6})(\\d{4})(\\d{2})(\\d{2})(\\d{3})([0-9Xx])$");
                if (!regex.Match(_value).Success)
                    return false;
                string[] strArray = regex.Split(_value);
                try
                {
                    dateTime = new DateTime(int.Parse(strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool IsLengthStr(string _value, int _begin, int _end)
        {
            int length = _value.Length;
            return length >= _begin || length <= _end;
        }

        public static bool IsLetterOrNumber(string _value)
        {
            return Validate.QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        public static bool IsMobileNum(string _value)
        {
            return new Regex("^1\\d{10}$", RegexOptions.IgnoreCase).Match(_value).Success;
        }

        public static bool IsNumber(string _value)
        {
            return Validate.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9][0-9])?$", _value);
        }

        public static bool IsNumeric(string _value)
        {
            return Validate.QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        public static bool IsPhoneNum(string _value)
        {
            return new Regex("^(86)?(-)?(0\\d{2,3})?(-)?(\\d{7,8})(-)?(\\d{3,5})?$", RegexOptions.IgnoreCase).Match(_value).Success;
        }

        public static bool IsStringDate(string _value)
        {
            try
            {
                DateTime.Parse(_value);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static bool IsWord(string _value)
        {
            return Regex.IsMatch(_value, "[A-Za-z]");
        }

        public static bool IsWordAndNum(string _value)
        {
            return new Regex("[a-zA-Z0-9]?").Match(_value).Success;
        }

        public static bool QuickValidate(string _express, string _value)
        {
            Regex regex = new Regex(_express);
            if (_value.Length == 0)
                return false;
            else
                return regex.IsMatch(_value);
        }

        public static DateTime StrToDate(string _value, DateTime _defaultValue)
        {
            if (Validate.IsStringDate(_value))
                return Convert.ToDateTime(_value);
            else
                return _defaultValue;
        }

        public static int StrToInt(string _value, int _defaultValue)
        {
            if (Validate.IsNumber(_value))
                return int.Parse(_value);
            else
                return _defaultValue;
        }
    }
}
