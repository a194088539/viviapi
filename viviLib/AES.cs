using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace viviLib
{
    public class AES
    {
        private static byte[] Keys = new byte[16]
        {
      (byte) 65,
      (byte) 114,
      (byte) 101,
      (byte) 121,
      (byte) 111,
      (byte) 117,
      (byte) 109,
      (byte) 121,
      (byte) 83,
      (byte) 110,
      (byte) 111,
      (byte) 119,
      (byte) 109,
      (byte) 97,
      (byte) 110,
      (byte) 63
        };

        public static string Decode(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = Utility.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = Encoding.UTF8.GetBytes(decryptKey);
                rijndaelManaged.IV = AES.Keys;
                ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor();
                byte[] inputBuffer = Convert.FromBase64String(decryptString);
                return Encoding.UTF8.GetString(decryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
                return "";
            }
        }

        public static string DESDecrypt(string pToDecrypt, string sKey)
        {
            byte[] buffer = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider())
            {
                cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
                cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
                MemoryStream memoryStream = new MemoryStream();
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(buffer, 0, buffer.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                }
                string @string = Encoding.UTF8.GetString(memoryStream.ToArray());
                memoryStream.Close();
                return @string;
            }
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

        public static string DoDecrypt1(string pToDecrypt, string sKey)
        {
            return AES.DESDecrypt(pToDecrypt, sKey);
        }

        public static string DESEncrypt(string pToEncrypt, string sKey)
        {
            using (DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pToEncrypt);
                cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
                cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
                MemoryStream memoryStream = new MemoryStream();
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                }
                string str = Convert.ToBase64String(memoryStream.ToArray());
                memoryStream.Close();
                return str;
            }
        }

        public static string Encode(string encryptString, string encryptKey)
        {
            encryptKey = Utility.GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelManaged.IV = AES.Keys;
            ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(encryptString);
            return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }
    }
}
