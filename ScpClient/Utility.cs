using System;
using System.Security.Cryptography;
using System.Text;

namespace com.todaynic.ScpClient
{
    internal class Utility
    {
        private static Encoding m_Encoding = Encoding.Default;

        public static Encoding Encoding
        {
            get
            {
                return Utility.m_Encoding;
            }
            set
            {
                Utility.m_Encoding = value;
            }
        }

        public static string getBase64ToString(string data)
        {
            return Encoding.GetEncoding("gbk").GetString(Convert.FromBase64String(data));
        }

        public static string getFileTime()
        {
            return DateTime.Now.ToFileTime().ToString();
        }

        public static string getMd5Hash(string data)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x2"));
            return stringBuilder.ToString().ToLower();
        }

        public static string getStringToBase64(string data)
        {
            return Convert.ToBase64String(Encoding.GetEncoding("gbk").GetBytes(data));
        }
    }
}
