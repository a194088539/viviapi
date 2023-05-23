using System;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.BLL.Sys.Transaction.YeePay
{
    public abstract class DES
    {
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 24)
            {
                string str = a_strKey;
                for (int index = 0; index < 24 / a_strKey.Length; ++index)
                    str += a_strKey;
                a_strKey = str;
            }
            a_strKey = a_strKey.Substring(0, 24);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(a_strKey);
            cryptoServiceProvider.Mode = CipherMode.ECB;
            cryptoServiceProvider.Padding = PaddingMode.PKCS7;
            ICryptoTransform decryptor = cryptoServiceProvider.CreateDecryptor();
            string str1 = "";
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(a_strString);
                str1 = Encoding.ASCII.GetString(decryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
            }
            return str1;
        }

        public static string Encrypt3DESJW(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 24)
                a_strKey += "000000000000000000000000";
            a_strKey = a_strKey.Substring(0, 24);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(a_strKey);
            cryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform encryptor = cryptoServiceProvider.CreateEncryptor();
            byte[] bytes = Encoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string Encrypt3DESSZX(string a_strString, string a_strKey)
        {
            if (a_strKey.Length < 24)
            {
                string str = a_strKey;
                for (int index = 0; index < 24 / a_strKey.Length; ++index)
                    str += a_strKey;
                a_strKey = str;
            }
            a_strKey = a_strKey.Substring(0, 24);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(a_strKey);
            cryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform encryptor = cryptoServiceProvider.CreateEncryptor();
            byte[] bytes = Encoding.ASCII.GetBytes(a_strString);
            return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}
