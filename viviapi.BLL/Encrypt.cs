using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace viviapi.BLL
{
    public class Encrypt
    {
        public static string encodefortcp(string str)
        {
            string str1 = "";
            for (int index = 0; index < str.Length; ++index)
            {
                int num = (int)str[index];
                str1 += (string)(object)(char)~num;
            }
            return str1;
        }

        public static string decodefortcp(string str)
        {
            string str1 = "";
            for (int index = 0; index < str.Length; ++index)
            {
                int num = (int)str[index];
                str1 += (string)(object)(char)~num;
            }
            return str1;
        }

        public static string DecryptDES(string source, string key, string iv)
        {
            Encoding ascii = Encoding.ASCII;
            byte[] buffer = Convert.FromBase64String(source);
            byte[] bytes1 = ascii.GetBytes(key);
            byte[] bytes2 = ascii.GetBytes(iv);
            CryptoStream cryptoStream = new CryptoStream((Stream)new MemoryStream(buffer), new DESCryptoServiceProvider().CreateDecryptor(bytes1, bytes2), CryptoStreamMode.Read);
            byte[] numArray = new byte[buffer.Length];
            cryptoStream.Read(numArray, 0, numArray.Length);
            return ascii.GetString(numArray);
        }

        public static string DoDecrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[pToDecrypt.Length / 2];
            for (int index = 0; index < pToDecrypt.Length / 2; ++index)
            {
                int num = Convert.ToInt32(pToDecrypt.Substring(index * 2, 2), 16);
                buffer[index] = (byte)num;
            }
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
            cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }

        public static string EncrypotyDES(string source, string key, string iv)
        {
            Encoding ascii = Encoding.ASCII;
            byte[] bytes1 = ascii.GetBytes(source);
            byte[] bytes2 = ascii.GetBytes(key);
            byte[] bytes3 = ascii.GetBytes(iv);
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(bytes2, bytes3), CryptoStreamMode.Write);
            cryptoStream.Write(bytes1, 0, bytes1.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string Getdekeystring(string txtkey)
        {
            return Encrypt.DoDecrypt(txtkey, "15684598");
        }

        public static bool PayTimeout()
        {
            return true;
        }
    }
}
