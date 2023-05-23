using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Xml;

namespace tenpay
{
    public class DirectTransClientRequestHandler : RequestHandler
    {
        public DirectTransClientRequestHandler(HttpContext httpContext)
          : base(httpContext)
        {
            this.setGateUrl("https://mch.tenpay.com/cgi-bin/mchbatchtransfer.cgi");
        }

        public void setAllParameterFromXml(string xmlStr)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlStr);
            foreach (XmlNode xmlNode in xmlDocument.SelectSingleNode("root").ChildNodes)
                this.setParameter(xmlNode.Name, xmlNode.InnerXml);
        }

        public string EncodeBase64(string src, string charset)
        {
            byte[] bytes = Encoding.GetEncoding(charset).GetBytes(src);
            string str;
            try
            {
                str = Convert.ToBase64String(bytes);
            }
            catch
            {
                str = charset;
            }
            return str;
        }

        public override string getRequestURL()
        {
            string str1 = "<?xml version=\"1.0\" encoding=\"GB2312\" ?><root>";
            foreach (string str2 in new ArrayList(this.parameters.Keys))
            {
                string str3 = (string)this.parameters[(object)str2];
                str1 = str1 + "<" + str2 + ">" + str3 + "</" + str2 + ">";
            }
            string src = str1 + "</root>";
            string str4 = this.EncodeBase64(src, this.getCharset());
            string encypStr = MD5Util.GetMD5(str4, this.getCharset()).ToLower() + this.getKey();
            string instr = MD5Util.GetMD5(encypStr, this.getCharset()).ToLower();
            this.setDebugInfo(src + "=>" + str4 + "=>" + encypStr + " => sign:" + instr);
            return this.getGateUrl() + "?content=" + TenpayUtil.UrlEncode(str4, this.getCharset()) + "&abstract=" + TenpayUtil.UrlEncode(instr, this.getCharset());
        }
    }
}
