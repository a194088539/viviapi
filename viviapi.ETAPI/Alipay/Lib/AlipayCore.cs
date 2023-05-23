using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace viviapi.ETAPI.Alipay
{
    public class Core
    {
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair in dicArrayPre)
            {
                if (keyValuePair.Key.ToLower() != "sign" && keyValuePair.Key.ToLower() != "sign_type" && keyValuePair.Value != "" && keyValuePair.Value != null)
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return dictionary;
        }

        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dicArray)
                stringBuilder.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dicArray)
                stringBuilder.Append(keyValuePair.Key + "=" + HttpUtility.UrlEncode(keyValuePair.Value, code) + "&");
            int length = stringBuilder.Length;
            stringBuilder.Remove(length - 1, 1);
            return ((object)stringBuilder).ToString();
        }

        public static void LogResult(string sWord)
        {
            StreamWriter streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath("log") + "\\" + DateTime.Now.ToString().Replace(":", "") + ".txt", false, Encoding.Default);
            streamWriter.Write(sWord);
            streamWriter.Close();
        }

        public static string GetAbstractToMD5(Stream sFile)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(sFile);
            StringBuilder stringBuilder = new StringBuilder(32);
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x").PadLeft(2, '0'));
            return ((object)stringBuilder).ToString();
        }

        public static string GetAbstractToMD5(byte[] dataFile)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(dataFile);
            StringBuilder stringBuilder = new StringBuilder(32);
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x").PadLeft(2, '0'));
            return ((object)stringBuilder).ToString();
        }
    }
}
