using System;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.ETAPI.tongyi
{
    public class MD5Util
    {
        public static string GetMD5(string encypStr, string charset)
        {
            MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] bytes;
            try
            {
                bytes = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                bytes = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
                Console.WriteLine((object)ex);
            }
            return BitConverter.ToString(cryptoServiceProvider.ComputeHash(bytes)).Replace("-", "").ToUpper();
        }
    }
}

