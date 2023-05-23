using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviLib.Security
{
    public sealed class Cryptography
    {
        private const string DESkey = "1234567890";
        private static byte[] DESIV;
        private static byte[] DESKey;

        private static byte[] RijndaelIV
        {
            get
            {
                return Cryptography.MD5StrToByte("广东省中山市能龙软件科技有限公司 | WWW.NENGLONG.COM");
            }
        }

        private static byte[] RijndaelKey
        {
            get
            {
                byte[] numArray = new byte[32];
                Array.Copy((Array)Cryptography.MD5StrToByte("广东省中山市能龙软件科技有限公司"), 0, (Array)numArray, 0, 16);
                Array.Copy((Array)Cryptography.MD5ByteToByte(Cryptography.MD5StrToByte("WWW.NENGLONG.COM")), 0, (Array)numArray, 16, 16);
                return numArray;
            }
        }

        private Cryptography()
        {
        }

        public static string MD5(string strToEncrypt)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("GB2312").GetBytes(strToEncrypt));
            string str = "";
            for (int index = 0; index < hash.Length; ++index)
                str += hash[index].ToString("x").PadLeft(2, '0');
            return str;
        }

        public static string MD5(string strToEncrypt, string encodeing)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(encodeing).GetBytes(strToEncrypt));
            string str = "";
            for (int index = 0; index < hash.Length; ++index)
                str += hash[index].ToString("x").PadLeft(2, '0');
            return str;
        }

        public static string DecryptConnString(string connString)
        {
            return Cryptography.DESDecryptString(connString, string.Empty);
        }

        public static string DESDecryptString(string inputStr, string keyStr)
        {
            if (inputStr == null || inputStr.Length == 0)
                return string.Empty;
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            if (keyStr == null || keyStr.Length == 0)
                keyStr = "1234567890";
            byte[] buffer = new byte[inputStr.Length / 2];
            for (int index = 0; index < inputStr.Length / 2; ++index)
            {
                int num = Convert.ToInt32(inputStr.Substring(index * 2, 2), 16);
                buffer[index] = (byte)num;
            }
            byte[] hash = new SHA1Managed().ComputeHash(Encoding.Default.GetBytes(keyStr));
            Cryptography.DESKey = new byte[8];
            Cryptography.DESIV = new byte[8];
            for (int index = 0; index < 8; ++index)
                Cryptography.DESKey[index] = hash[index];
            for (int index = 8; index < 16; ++index)
                Cryptography.DESIV[index - 8] = hash[index];
            cryptoServiceProvider.Key = Cryptography.DESKey;
            cryptoServiceProvider.IV = Cryptography.DESIV;
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }

        public static string DESEncryptString(string inputStr, string keyStr)
        {
            if (inputStr == null || inputStr.Length == 0)
                return string.Empty;
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            if (keyStr == null || keyStr.Length == 0)
                keyStr = "1234567890";
            byte[] bytes = Encoding.Default.GetBytes(inputStr);
            byte[] hash = new SHA1Managed().ComputeHash(Encoding.Default.GetBytes(keyStr));
            Cryptography.DESKey = new byte[8];
            Cryptography.DESIV = new byte[8];
            for (int index = 0; index < 8; ++index)
                Cryptography.DESKey[index] = hash[index];
            for (int index = 8; index < 16; ++index)
                Cryptography.DESIV[index - 8] = hash[index];
            cryptoServiceProvider.Key = Cryptography.DESKey;
            cryptoServiceProvider.IV = Cryptography.DESIV;
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in memoryStream.ToArray())
                stringBuilder.AppendFormat("{0:X2}", (object)num);
            cryptoStream.Close();
            memoryStream.Close();
            return stringBuilder.ToString();
        }

        public static string EncryptConnString(string connString)
        {
            return Cryptography.DESEncryptString(connString, string.Empty);
        }

        public static string EncryptPassword(string password)
        {
            return Cryptography.MD5(password);
        }

        public static byte[] MD5ByteToByte(byte[] bytesToEncrypt)
        {
            return ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytesToEncrypt);
        }

        public static string MD5ByteToStr(byte[] bytesToEncrypt)
        {
            return Convert.ToBase64String(Cryptography.MD5ByteToByte(bytesToEncrypt));
        }

        public static byte[] MD5StrToByte(string strToEncrypt)
        {
            return Cryptography.MD5ByteToByte(Encoding.UTF8.GetBytes(strToEncrypt));
        }

        public static string RijndaelDecrypt(string strToDecrypt)
        {
            byte[] rijndaelKey = Cryptography.RijndaelKey;
            byte[] rijndaelIv = Cryptography.RijndaelIV;
            byte[] buffer = Convert.FromBase64String(strToDecrypt);
            byte[] numArray = new byte[buffer.Length];
            MemoryStream memoryStream = new MemoryStream(buffer);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, rijndaelManaged.CreateDecryptor(rijndaelKey, rijndaelIv), CryptoStreamMode.Read);
            try
            {
                cryptoStream.Read(numArray, 0, numArray.Length);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                memoryStream.Close();
                cryptoStream.Close();
                return string.Empty;
            }
            return Encoding.UTF8.GetString(numArray);
        }

        public static string RijndaelEncrypt(string strToEncrypt)
        {
            byte[] rijndaelKey = Cryptography.RijndaelKey;
            byte[] rijndaelIv = Cryptography.RijndaelIV;
            byte[] bytes = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] numArray = new byte[0];
            MemoryStream memoryStream = new MemoryStream();
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, rijndaelManaged.CreateEncryptor(rijndaelKey, rijndaelIv), CryptoStreamMode.Write);
            byte[] inArray;
            try
            {
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                inArray = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                memoryStream.Close();
                cryptoStream.Close();
                return string.Empty;
            }
            return Convert.ToBase64String(inArray);
        }

        public static string SHA1(string strToEncrypt)
        {
            return Convert.ToBase64String(((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(Encoding.UTF8.GetBytes(strToEncrypt)));
        }
    }
}
