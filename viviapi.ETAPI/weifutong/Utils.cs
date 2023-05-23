using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace swiftpass.utils
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        public Utils() { }

        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr">URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;
            }
        }


        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr">编码的URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;

            }
        }


        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }


        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length">随机数的长度</param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, String> loadCfg()
        {
            string cfgPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase)
                                + Path.DirectorySeparatorChar + @"wftfile\config" + Path.DirectorySeparatorChar + "config.properties";
            Dictionary<String, String> cfg = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(cfgPath))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }
                    int startInd = line.IndexOf("=");
                    string key = line.Substring(0, startInd);
                    string val = line.Substring(startInd + 1, line.Length - (startInd + 1));
                    if (!cfg.ContainsKey(key) && !string.IsNullOrEmpty(val))
                    {
                        cfg.Add(key, val);
                    }
                }
            }

            return cfg;
        }

        /// <summary>
        /// 保存接口返回结果到文件中
        /// </summary>
        /// <param name="_param">接口结果</param>
        public static void writeFile(string title, Hashtable _param)
        {
            string resFilePath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase)
                                + Path.DirectorySeparatorChar + "result.txt";
            if (!File.Exists(resFilePath))
            {
                using (StreamWriter sw = new StreamWriter(resFilePath))
                {
                    sw.WriteLine("=====================" + title + "=====================");
                    foreach (DictionaryEntry de in _param)
                    {
                        sw.WriteLine("key:" + de.Key.ToString() + " value:" + de.Value.ToString());
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(resFilePath))
                {
                    sw.WriteLine("=====================" + title + "=====================");
                    foreach (DictionaryEntry de in _param)
                    {
                        sw.WriteLine("key:" + de.Key.ToString() + " value:" + de.Value.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 生成32位随机数
        /// </summary>
        /// <returns></returns>
        public static string random()
        {
            char[] constant = {'0','1','2','3','4','5','6','7','8','9',
                               'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            StringBuilder sb = new StringBuilder(32);
            Random rd = new Random();
            for (int i = 0; i < 32; i++)
            {
                sb.Append(constant[rd.Next(62)]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成16位订单号 by  hyf 2016年2月16日17:48:43
        /// </summary>
        /// <returns></returns>
        public static string Nmrandom()
        {
            string rm = "";
            Random ra = new Random();
            for (int i = 0; i < 16; i++)
            {
                rm += ra.Next(0, 9).ToString();
            }
            return rm;
        }
        /// <summary>
        /// 将Hashtable参数传为XML
        /// </summary>
        /// <param name="_params"></param>
        /// <returns></returns>
        public static string toXml(Hashtable _params)
        {
            StringBuilder sb = new StringBuilder("<xml>");
            foreach (DictionaryEntry de in _params)
            {
                string key = de.Key.ToString();
                sb.Append("<").Append(key).Append("><![CDATA[").Append(de.Value.ToString()).Append("]]></").Append(key).Append(">");
            }

            return sb.Append("</xml>").ToString();
        }

    }
}
