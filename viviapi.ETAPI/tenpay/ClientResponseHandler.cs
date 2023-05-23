using System.Collections;
using System.Text;
using System.Xml;

namespace tenpay
{
    public class ClientResponseHandler
    {
        private string charset = "gb2312";
        private string key;
        protected Hashtable parameters;
        private string debugInfo;
        protected string content;

        public ClientResponseHandler()
        {
            this.parameters = new Hashtable();
        }

        public string getContent()
        {
            return this.content;
        }

        public virtual void setContent(string content)
        {
            this.content = content;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);
            foreach (XmlNode xmlNode in xmlDocument.SelectSingleNode("root").ChildNodes)
                this.setParameter(xmlNode.Name, xmlNode.InnerXml);
        }

        public string getKey()
        {
            return this.key;
        }

        public void setKey(string key)
        {
            this.key = key;
        }

        public string getParameter(string parameter)
        {
            string str = (string)this.parameters[(object)parameter];
            return str == null ? "" : str;
        }

        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter == null || !(parameter != ""))
                return;
            if (this.parameters.Contains((object)parameter))
                this.parameters.Remove((object)parameter);
            this.parameters.Add((object)parameter, (object)parameterValue);
        }

        public virtual bool isTenpaySign()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ArrayList arrayList = new ArrayList(this.parameters.Keys);
            arrayList.Sort();
            foreach (string strB1 in arrayList)
            {
                string strB2 = (string)this.parameters[(object)strB1];
                if (strB2 != null && "".CompareTo(strB2) != 0 && "sign".CompareTo(strB1) != 0 && "key".CompareTo(strB1) != 0)
                    stringBuilder.Append(strB1 + "=" + strB2 + "&");
            }
            stringBuilder.Append("key=" + this.getKey());
            string str = MD5Util.GetMD5(stringBuilder.ToString(), this.getCharset()).ToLower();
            this.setDebugInfo(stringBuilder.ToString() + " => sign:" + str);
            return this.getParameter("sign").ToLower().Equals(str);
        }

        public string getDebugInfo()
        {
            return this.debugInfo;
        }

        protected void setDebugInfo(string debugInfo)
        {
            this.debugInfo = debugInfo;
        }

        protected virtual string getCharset()
        {
            return this.charset;
        }

        public void setCharset(string charset)
        {
            this.charset = charset;
        }

        public virtual bool _isTenpaySign(ArrayList akeys)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string strB1 in akeys)
            {
                string strB2 = (string)this.parameters[(object)strB1];
                if (strB2 != null && "".CompareTo(strB2) != 0 && "sign".CompareTo(strB1) != 0 && "key".CompareTo(strB1) != 0)
                    stringBuilder.Append(strB1 + "=" + strB2 + "&");
            }
            stringBuilder.Append("key=" + this.getKey());
            string str = MD5Util.GetMD5(stringBuilder.ToString(), this.getCharset()).ToLower();
            this.setDebugInfo(stringBuilder.ToString() + " => sign:" + str);
            return this.getParameter("sign").ToLower().Equals(str);
        }
    }
}
