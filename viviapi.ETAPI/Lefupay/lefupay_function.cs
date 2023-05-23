using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.ETAPI.Lefupay
{
    public class lefupay_function
    {
        public static string Build_mysign(Dictionary<string, string> dicArray, string key, string signType, string inputCharset)
        {
            string linkstring = lefupay_function.Create_linkstring(dicArray);
            int length = linkstring.Length;
            return lefupay_function.Sign(linkstring.Substring(0, length - 1) + key, signType, inputCharset);
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
                if (keyValuePair.Key.ToLower() != "sign" && keyValuePair.Value != "")
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return dictionary;
        }

        public static string Sign(string prestr, string signType, string inputCharset)
        {
            StringBuilder stringBuilder = new StringBuilder(32);
            if (signType.ToUpper() == "MD5")
            {
                foreach (byte num in new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(inputCharset).GetBytes(prestr)))
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
