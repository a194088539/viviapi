using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace tenpay
{
    public class ResponseHandler
    {
        private string key;
        protected Hashtable parameters;
        private string debugInfo;
        protected HttpContext httpContext;

        public ResponseHandler(HttpContext httpContext)
        {
            this.parameters = new Hashtable();
            this.httpContext = httpContext;
            NameValueCollection nameValueCollection = !(this.httpContext.Request.HttpMethod == "POST") ? this.httpContext.Request.QueryString : this.httpContext.Request.Form;
            foreach (string parameter in (NameObjectCollectionBase)nameValueCollection)
            {
                string parameterValue = nameValueCollection[parameter];
                this.setParameter(parameter, parameterValue);
            }
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

        public void doShow(string show_url)
        {
            this.httpContext.Response.Write("<html><head>\r\n<meta name=\"TENCENT_ONLINE_PAYMENT\" content=\"China TENCENT\">\r\n<script language=\"javascript\">\r\nwindow.location.href='" + show_url + "';\r\n</script>\r\n</head><body></body></html>");
            this.httpContext.Response.End();
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
            return this.httpContext.Request.ContentEncoding.BodyName;
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
