using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace viviLib.Utils
{
    public class StringText
    {
        public static string BuilderCode(int n)
        {
            string[] strArray = new string[62]
            {
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z"
            };
            StringBuilder stringBuilder = new StringBuilder();
            int num = -1;
            Random random = new Random();
            for (int index1 = 1; index1 < n + 1; ++index1)
            {
                if (num != -1)
                    random = new Random(index1 * num * (int)DateTime.Now.Ticks);
                int index2 = random.Next(57);
                if (num != -1 && num == index2)
                    return StringText.BuilderCode(n);
                num = index2;
                stringBuilder.Append(strArray[index2]);
            }
            return stringBuilder.ToString();
        }

        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return strArray[strArray.Length - 1].ToLower();
        }

        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";
            return HttpContext.Current.Request.QueryString[strName];
        }

        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string getWeekDay(int y, int m, int d)
        {
            DateTime now = DateTime.Now;
            return now.Year.ToString() + "年" + now.Month.ToString() + "月" + now.Day.ToString() + "日";
        }

        public static bool IsUnicode(string s)
        {
            string pattern = "^[\\u4E00-\\u9FA5\\uE815-\\uFA29]+$";
            return Regex.IsMatch(s, pattern);
        }

        public static string Left(string str, int need, bool encode)
        {
            if (str == null || str == string.Empty)
                return string.Empty;
            int length1 = str.Length;
            if (length1 < need / 2)
                return encode ? StringText.TextEncode(str) : str;
            int num1 = 0;
            int length2;
            for (length2 = 0; length2 < length1; ++length2)
            {
                char ch = str[length2];
                num1 += StringText.IsUnicode(ch.ToString()) ? 2 : 1;
                if (num1 >= need)
                    break;
            }
            string str1 = str.Substring(0, length2);
            if (length1 > length2)
            {
                int num2;
                for (num2 = 0; num2 < 5; ++num2)
                {
                    if (length2 - num2 >= str.Length || length2 - num2 < 0)
                    {
                        --num2;
                        break;
                    }
                    char ch = str[length2 - num2];
                    num1 -= StringText.IsUnicode(ch.ToString()) ? 2 : 1;
                    if (num1 <= need)
                        break;
                }
                str1 = str.Substring(0, length2 - num2) + "...";
            }
            return encode ? StringText.TextEncode(str1) : str1;
        }

        public static string ShitEncode(string str)
        {
            string input = "";
            string pattern = input != null && !(input == string.Empty) ? Regex.Replace(Regex.Replace(input, "\\|{2,}", "|"), "(^\\|)|(\\|$)", "") : "妈的|你妈|他妈|妈b|妈比|我日|我操|法轮|fuck|shit";
            return Regex.Replace(str, pattern, "**", RegexOptions.IgnoreCase);
        }

        public static string TextEncode(string str)
        {
            StringBuilder stringBuilder = new StringBuilder(str);
            stringBuilder.Replace("&", "&amp;");
            stringBuilder.Replace("<", "&lt;");
            stringBuilder.Replace(">", "&gt;");
            stringBuilder.Replace("\"", "&quot;");
            stringBuilder.Replace("'", "&#39;");
            return StringText.ShitEncode(stringBuilder.ToString());
        }
    }
}
