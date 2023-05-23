using System;
using System.IO;
using System.Net;
using System.Xml;

namespace viviapi.ETAPI.Ebatong
{
    public class AskForTimestamp
    {
        public static string askFor()
        {
            string str1 = "https://www.ebatong.com/gateway.htm";
            string str2 = "query_timestamp";
            string partner = Config.Partner;
            string inputCharset = Config.Input_charset;
            string signType = Config.Sign_type;
            string str3 = CommonHelper.BuildParamString(CommonHelper.BubbleSort(new string[4]
            {
        "service=" + str2,
        "partner=" + partner,
        "input_charset=" + inputCharset,
        "sign_type=" + signType
            }));
            string key = Config.Key;
            string plainText = str3 + key;
            string str4 = CommonHelper.md5(inputCharset, plainText).ToLower();
            string address = str1 + "?service=" + str2 + "&partner=" + partner + "&input_charset=" + inputCharset + "&sign_type=" + signType + "&sign=" + str4;
            Console.WriteLine(address);
            Stream stream = new WebClient().OpenRead(address);
            StreamReader streamReader = new StreamReader(stream);
            string xml = streamReader.ReadToEnd();
            streamReader.Close();
            stream.Close();
            Console.WriteLine("HTTP Response is ");
            Console.WriteLine(xml);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            string str5 = "";
            if ("T".Equals(xmlDocument.SelectSingleNode("ebatong/is_success/text()").Value))
            {
                string str6 = xmlDocument.SelectSingleNode("ebatong/response/timestamp/encrypt_key/text()").Value;
                string str7 = xmlDocument.SelectSingleNode("ebatong/sign/text()").Value;
                if (CommonHelper.md5(inputCharset, str6 + key).ToLower().Equals(str7))
                    str5 = str6;
            }
            return str5;
        }
    }
}
