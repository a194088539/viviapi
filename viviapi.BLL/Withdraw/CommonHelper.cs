using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.BLL.Withdraw
{
    public class CommonHelper
    {
        public static string BuildParamString(string[] s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < s.Length; ++index)
            {
                if (index == s.Length - 1)
                    stringBuilder.Append(s[index]);
                else
                    stringBuilder.Append(s[index] + "&");
            }
            return stringBuilder.ToString();
        }

        public static string[] BubbleSort(string[] r)
        {
            for (int index1 = 0; index1 < r.Length; ++index1)
            {
                bool flag = false;
                for (int index2 = r.Length - 2; index2 >= index1; --index2)
                {
                    if (string.CompareOrdinal(r[index2 + 1], r[index2]) < 0)
                    {
                        string str = r[index2 + 1];
                        r[index2 + 1] = r[index2];
                        r[index2] = str;
                        flag = true;
                    }
                }
                if (!flag)
                    break;
            }
            return r;
        }

        public static byte[] ToByteArray(string HexString)
        {
            int length = HexString.Length;
            byte[] numArray = new byte[length / 2];
            int startIndex = 0;
            while (startIndex < length)
            {
                numArray[startIndex / 2] = Convert.ToByte(HexString.Substring(startIndex, 2), 16);
                startIndex += 2;
            }
            return numArray;
        }

        public static string ToHexStr(byte[] bytes)
        {
            if (bytes == null)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < bytes.Length; ++index)
                stringBuilder.Append(bytes[index].ToString("X2"));
            return stringBuilder.ToString();
        }

        public static string md5(string input_charset, string plainText)
        {
            return CommonHelper.ToHexStr(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(input_charset).GetBytes(plainText)));
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue, string GATEWAY_NEW)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form id='ebatongsubmit' name='alipaysubmit' action='" + GATEWAY_NEW + "' method='" + strMethod.ToLower().Trim() + "'>");
            foreach (KeyValuePair<string, string> keyValuePair in sParaTemp)
                stringBuilder.Append("<input type='hidden' name='" + keyValuePair.Key + "' value='" + keyValuePair.Value + "'/>");
            stringBuilder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            stringBuilder.Append("<script>document.forms['ebatongsubmit'].submit();</script>");
            return stringBuilder.ToString();
        }
    }
}
