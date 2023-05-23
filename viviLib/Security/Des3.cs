using System;
using System.IO;
using System.Security.Cryptography;
using viviLib.ExceptionHandling;

namespace viviLib.Security
{
    public class Des3
    {
        public static byte[] Des3EncodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider.Mode = CipherMode.CBC;
                cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                byte[] numArray = memoryStream.ToArray();
                cryptoStream.Close();
                memoryStream.Close();
                return numArray;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", (object)ex.Message);
                return (byte[])null;
            }
        }

        public static byte[] Des3DecodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(data);
                TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider.Mode = CipherMode.CBC;
                cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                byte[] buffer = new byte[data.Length];
                cryptoStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", (object)ex.Message);
                return (byte[])null;
            }
        }

        public static byte[] Des3EncodeECB(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider.Mode = CipherMode.ECB;
                cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                byte[] numArray = memoryStream.ToArray();
                cryptoStream.Close();
                memoryStream.Close();
                return numArray;
            }
            catch (CryptographicException ex)
            {
                ExceptionHandler.HandleException((Exception)ex);
                return (byte[])null;
            }
        }

        public static byte[] Des3DecodeECB(byte[] key, byte[] iv, byte[] data)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(data);
                TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider.Mode = CipherMode.ECB;
                cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                byte[] buffer = new byte[data.Length];
                cryptoStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", (object)ex.Message);
                return (byte[])null;
            }
        }
    }
}
