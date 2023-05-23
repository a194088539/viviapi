using System.Collections.Generic;
using System.Text;
using System.Xml;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.AlipayWap
{
    public class Submit
    {
        private static string GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?";
        private static string _input_charset = "";
        private static string _sign_type = "";

        static Submit()
        {
            Submit._input_charset = Config.input_charset.Trim().ToLower();
            Submit._sign_type = Config.sign_type.Trim().ToUpper();
        }

        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(101);
            string linkString = Core.CreateLinkString(sPara);
            string str;
            switch (Submit._sign_type)
            {
                case "MD5":
                    str = AlipayMD5.Sign(linkString, cacheModel.puserkey, Submit._input_charset);
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Dictionary<string, string> sPara = Core.FilterPara(sParaTemp);
            string str = Submit.BuildRequestMysign(sPara);
            sPara.Add("sign", str);
            sPara.Add("sign_type", Submit._sign_type);
            return sPara;
        }

        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            return Core.CreateLinkStringUrlencode(Submit.BuildRequestPara(sParaTemp), code);
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            Dictionary<string, string> dictionary2 = Submit.BuildRequestPara(sParaTemp);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + Submit.GATEWAY_NEW + "_input_charset=" + Submit._input_charset + "' method='" + strMethod.ToLower().Trim() + "'>");
            foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
                stringBuilder.Append("<input type='hidden' name='" + keyValuePair.Key + "' value='" + keyValuePair.Value + "'/>");
            stringBuilder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            stringBuilder.Append("<script>document.forms['alipaysubmit'].submit();</script>");
            return ((object)stringBuilder).ToString();
        }

        public static string Query_timestamp()
        {
            XmlTextReader xmlTextReader = new XmlTextReader(Submit.GATEWAY_NEW + "service=query_timestamp&partner=" + Config.partner + "&_input_charset=" + Config.input_charset);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load((XmlReader)xmlTextReader);
            return xmlDocument.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText;
        }
    }
}
