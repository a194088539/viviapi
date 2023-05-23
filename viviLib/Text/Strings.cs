using System;
using System.Text;

namespace viviLib.Text
{
    public class Strings
    {
        public static string MoneyToChinese(string LowerMoney)
        {
            bool flag = false;
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                flag = true;
            }
            string str1 = (string)null;
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                    LowerMoney += "0";
            }
            else
                LowerMoney += ".00";
            string str2 = LowerMoney;
            int num = 1;
            string str3 = "";
            for (; num <= str2.Length; ++num)
            {
                switch (str2.Substring(str2.Length - num, 1))
                {
                    case ".":
                        str1 = "圆";
                        break;
                    case "0":
                        str1 = "零";
                        break;
                    case "1":
                        str1 = "壹";
                        break;
                    case "2":
                        str1 = "贰";
                        break;
                    case "3":
                        str1 = "叁";
                        break;
                    case "4":
                        str1 = "肆";
                        break;
                    case "5":
                        str1 = "伍";
                        break;
                    case "6":
                        str1 = "陆";
                        break;
                    case "7":
                        str1 = "柒";
                        break;
                    case "8":
                        str1 = "捌";
                        break;
                    case "9":
                        str1 = "玖";
                        break;
                }
                switch (num)
                {
                    case 1:
                        str1 += "分";
                        break;
                    case 2:
                        str1 += "角";
                        break;
                    case 3:
                        str1 = str1 ?? "";
                        break;
                    case 4:
                        str1 = str1 ?? "";
                        break;
                    case 5:
                        str1 += "拾";
                        break;
                    case 6:
                        str1 += "佰";
                        break;
                    case 7:
                        str1 += "仟";
                        break;
                    case 8:
                        str1 += "万";
                        break;
                    case 9:
                        str1 += "拾";
                        break;
                    case 10:
                        str1 += "佰";
                        break;
                    case 11:
                        str1 += "仟";
                        break;
                    case 12:
                        str1 += "亿";
                        break;
                    case 13:
                        str1 += "拾";
                        break;
                    case 14:
                        str1 += "佰";
                        break;
                    case 15:
                        str1 += "仟";
                        break;
                    case 16:
                        str1 += "万";
                        break;
                    default:
                        str1 = str1 ?? "";
                        break;
                }
                str3 = str1 + str3;
            }
            string str4 = str3.Replace("零拾", "零").Replace("零佰", "零").Replace("零仟", "零").Replace("零零零", "零").Replace("零零", "零").Replace("零角零分", "整").Replace("零分", "整").Replace("零角", "零").Replace("零亿零万零圆", "亿圆").Replace("亿零万零圆", "亿圆").Replace("零亿零万", "亿").Replace("零万零圆", "万圆").Replace("零亿", "亿").Replace("零万", "万").Replace("零圆", "圆").Replace("零零", "零");
            if (str4.Substring(0, 1) == "圆")
                str4 = str4.Substring(1, str4.Length - 1);
            if (str4.Substring(0, 1) == "零")
                str4 = str4.Substring(1, str4.Length - 1);
            if (str4.Substring(0, 1) == "角")
                str4 = str4.Substring(1, str4.Length - 1);
            if (str4.Substring(0, 1) == "分")
                str4 = str4.Substring(1, str4.Length - 1);
            if (str4.Substring(0, 1) == "整")
                str4 = "零圆整";
            string str5 = str4;
            if (flag)
                return "负" + str5;
            return str5;
        }

        public static string ReplaceStringSeparator(string s)
        {
            return s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
        }

        public static string MergeString(string source, string target)
        {
            return Strings.MergeString(source, target, ",");
        }

        public static string MergeString(string source, string target, string mergechar)
        {
            target = !string.IsNullOrEmpty(target) ? target + mergechar + source : source;
            return target;
        }

        public static string ReplaceString(string source, int start, int len, string repchar)
        {
            try
            {
                string str = string.Empty;
                if (string.IsNullOrEmpty(source) || source.Length < start + len)
                    return str;
                StringBuilder stringBuilder = new StringBuilder();
                for (int index = 0; index < len; ++index)
                    stringBuilder.Append(repchar);
                return source.Replace(source.Substring(start, len), stringBuilder.ToString());
            }
            catch
            {
                return source;
            }
        }

        public static string ReplaceString(string source, int lev, string repchar)
        {
            try
            {
                string str = string.Empty;
                if (string.IsNullOrEmpty(source) || source.Length < lev)
                    return str;
                int length = source.Length - lev;
                StringBuilder stringBuilder = new StringBuilder();
                for (int index = 0; index < length; ++index)
                    stringBuilder.Append(repchar);
                return source.Replace(source.Substring(0, length), stringBuilder.ToString());
            }
            catch
            {
                return source;
            }
        }

        public static string Mark(string num)
        {
            string str = "";
            if (num.Length > 4)
            {
                int length = num.Length / 3;
                for (int index = 0; index < num.Length - length - length; ++index)
                    str += "*";
                return num.Substring(0, length) + str + num.Substring(num.Length - length, length);
            }
            if (num.Length <= 1)
                return num;
            for (int index = 0; index < num.Length - 1; ++index)
                str += "*";
            return num.Substring(0, 1) + str;
        }

        public static string Mark(string num, char split)
        {
            string[] strArray = num.Split(split);
            if (strArray.Length >= 2)
                return Strings.Mark(strArray[0]) + split.ToString() + strArray[1];
            return num;
        }

        public static string ReplaceString(string source, char split, int lev, string repchar)
        {
            try
            {
                string str1 = string.Empty;
                if (string.IsNullOrEmpty(source))
                    return str1;
                string[] strArray = source.Split(split);
                if (strArray.Length == 1)
                    return str1;
                string str2 = strArray[0];
                if (str2.Length < lev)
                    return str1;
                int num = str2.Length - lev;
                StringBuilder stringBuilder = new StringBuilder();
                for (int index = 0; index < num; ++index)
                    stringBuilder.Append(repchar);
                return source.Replace(source.Substring(lev - 1, num - 1), stringBuilder.ToString());
            }
            catch
            {
                return source;
            }
        }
    }
}
