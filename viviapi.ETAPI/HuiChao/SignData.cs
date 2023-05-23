using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace viviapi.ETAPI.HuiChao
{
    public class Sign
    {
        public static String pfxpath = @"E:\支付平例子\yingshen\yingshen\证书\szmall.pfx";
        public static String cerpath = @"E:\支付平例子\yingshen\yingshen\证书\businessgate.cer";
        public static String pfxpassword = "111111";

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="xml">xml数据</param>
        /// <returns>签名数据</returns>
        public static String signData(String xml)
        {
            Console.WriteLine("--------【XML例子】加载数字证书私钥：" + pfxpath + "证书密码：" + pfxpassword);
            //签名 
            Console.WriteLine("--------【XML例子】开始使用私钥创建check域例子");
            Console.WriteLine("--------【XML例子】需要签名的msg域：" + xml);
            // X509Certificate2 objx5092 = new X509Certificate2(pfxpath, pfxpassword);   //当前用户存储，本地测试
            X509Certificate2 objx5092 = new X509Certificate2(pfxpath, pfxpassword, X509KeyStorageFlags.MachineKeySet);   //本地存储，服务器测试(windows server2008)要使用这个

            RSACryptoServiceProvider rsa = objx5092.PrivateKey as RSACryptoServiceProvider;
            byte[] data = Encoding.GetEncoding("GBK").GetBytes(xml);
            byte[] hashValue = rsa.SignData(data, "MD5");
            string msg = Convert.ToBase64String(hashValue);
            Console.WriteLine("--------【XML例子】得到msg域为密文：" + msg);

            return msg.PadRight(256);
        }

        /// 
        /// 解签 
        /// </summary>
        /// <param name="signData">解签数据</param>
        /// <returns>是否成功</returns>
        public bool verifyData(String msg, String check)
        {
            byte[] msgByte = System.Convert.FromBase64String(msg);
            byte[] checkByte = System.Convert.FromBase64String(check);
            X509Certificate2 pub = new X509Certificate2(cerpath);
            RSACryptoServiceProvider rsaPublic = pub.PublicKey.Key as RSACryptoServiceProvider;

            return rsaPublic.VerifyData(msgByte, "MD5", checkByte);
        }
    }
}
