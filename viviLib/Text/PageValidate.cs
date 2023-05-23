using System;
using System.Text;
using System.Text.RegularExpressions;

namespace viviLib.Text
{
    public class PageValidate
    {
        private static Regex RegCHZN = new Regex("[一-龥]");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|cn|org|edu|mil|tv|biz|info)$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
        private static Regex RegMoblie = new Regex("(86)*0*1\\d{10}");
        private static Regex RegUrl = new Regex("^(http|https|ftp)\\://([a-zA-Z0-9\\.\\-]+(\\:[a-zA-Z0-9\\.&amp;%\\$\\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\\-]+\\.)*[a-zA-Z0-9\\-]+\\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\\:[0-9]+)*(/($|[a-zA-Z0-9\\.\\,\\?\\'\\\\\\+&amp;%\\$#\\=~_\\-]+))*$");

        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
                return PageValidate.CheckIDCard18(Id);
            return Id.Length == 15 && PageValidate.CheckIDCard15(Id);
        }

        private static bool CheckIDCard15(string Id)
        {
            long result1 = 0L;
            if (!long.TryParse(Id, out result1) || (double)result1 < Math.Pow(10.0, 14.0) || "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91".IndexOf(Id.Remove(2)) == -1)
                return false;
            string s = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime result2 = new DateTime();
            return DateTime.TryParse(s, out result2);
        }

        private static bool CheckIDCard18(string Id)
        {
            long result1 = 0L;
            if (!long.TryParse(Id.Remove(17), out result1) || (double)result1 < Math.Pow(10.0, 16.0) || !long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out result1) || "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91".IndexOf(Id.Remove(2)) == -1)
                return false;
            string s = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime result2 = new DateTime();
            if (!DateTime.TryParse(s, out result2))
                return false;
            string[] strArray1 = "1,0,x,9,8,7,6,5,4,3,2".Split(',');
            string[] strArray2 = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(',');
            char[] chArray = Id.Remove(17).ToCharArray();
            int a = 0;
            for (int index = 0; index < 17; ++index)
                a += int.Parse(strArray2[index]) * int.Parse(chArray[index].ToString());
            int result3 = -1;
            Math.DivRem(a, 11, out result3);
            return !(strArray1[result3] != Id.Substring(17, 1).ToLower());
        }

        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (inputString != null && inputString != string.Empty)
            {
                inputString = inputString.Trim();
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);
                for (int index = 0; index < inputString.Length; ++index)
                {
                    switch (inputString[index])
                    {
                        case '"':
                            stringBuilder.Append("&quot;");
                            break;
                        case '<':
                            stringBuilder.Append("&lt;");
                            break;
                        case '>':
                            stringBuilder.Append("&gt;");
                            break;
                        default:
                            stringBuilder.Append(inputString[index]);
                            break;
                    }
                }
                stringBuilder.Replace("'", " ");
            }
            return stringBuilder.ToString();
        }

        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
                charInput = strInput.Substring(0, 1);
            return PageValidate.isContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
                return false;
            return new Regex(string.Format("^([{0}])+$", (object)charInput)).Match(strInput).Success;
        }

        public static bool isContainSpecChar(string strInput)
        {
            string[] strArray = new string[2]
            {
        "123456",
        "654321"
            };
            foreach (string str in strArray)
            {
                if (strInput == str)
                    return true;
            }
            return false;
        }

        public static bool IsDateTime(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    return false;
                DateTime.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDecimal(string inputData)
        {
            return PageValidate.RegDecimal.Match(inputData).Success;
        }

        public static bool IsDecimalSign(string inputData)
        {
            return PageValidate.RegDecimalSign.Match(inputData).Success;
        }

        public static bool IsEmail(string inputData)
        {
            return PageValidate.RegEmail.Match(inputData).Success;
        }

        public static bool IsHasCHZN(string inputData)
        {
            return PageValidate.RegCHZN.Match(inputData).Success;
        }

        public static bool IsNumber(string inputData)
        {
            return PageValidate.RegNumber.Match(inputData).Success;
        }

        public static bool IsNumberSign(string inputData)
        {
            return PageValidate.RegNumberSign.Match(inputData).Success;
        }

        public static bool IsPhone(string inputData)
        {
            return PageValidate.RegPhone.Match(inputData).Success;
        }

        public static bool IsMobile(string inputData)
        {
            return PageValidate.RegMoblie.Match(inputData).Success;
        }

        public static bool IsUrl(string inputData)
        {
            return PageValidate.RegUrl.Match(inputData).Success;
        }

        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }

        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
                return (string)null;
            if (sqlText == "")
                return "";
            sqlText = sqlText.Replace(",", "");
            sqlText = sqlText.Replace("<", "");
            sqlText = sqlText.Replace(">", "");
            sqlText = sqlText.Replace("--", "");
            sqlText = sqlText.Replace("'", "");
            sqlText = sqlText.Replace("\"", "");
            sqlText = sqlText.Replace("=", "");
            sqlText = sqlText.Replace("%", "");
            sqlText = sqlText.Replace(" ", "");
            return sqlText;
        }
    }
}
