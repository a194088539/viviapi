using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestEasy
{
    public class Easypay_function
    {
        public static string Build_mysign(Dictionary<string, string> dicArray, string key, string sign_type, string _input_charset)
        {
            string linkstring = Easypay_function.Create_linkstring(dicArray);
            int length = linkstring.Length;
            return Easypay_function.Sign(linkstring.Substring(0, length - 1) + key, sign_type, _input_charset);
        }

        public static string Create_linkstring(Dictionary<string, string> dicArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dicArray)
                stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            return stringBuilder.ToString();
        }

        public static Dictionary<string, string> Para_filter(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair in dicArrayPre)
            {
                if (keyValuePair.Key.ToLower() != "sign" && keyValuePair.Key.ToLower() != "sign_type" && keyValuePair.Value != "")
                    dictionary.Add(keyValuePair.Key.ToLower(), keyValuePair.Value);
            }
            return dictionary;
        }

        public static string Sign(string prestr, string sign_type, string _input_charset)
        {
            StringBuilder stringBuilder = new StringBuilder(32);
            if (sign_type.ToUpper() == "MD5")
            {
                foreach (byte num in new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr)))
                    stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        public static void log_result(string sPath, string sWord)
        {
            StreamWriter streamWriter = new StreamWriter(sPath, false, Encoding.Default);
            streamWriter.Write(sWord);
            streamWriter.Close();
        }
    }
}
