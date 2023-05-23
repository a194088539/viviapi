using System.Collections;
using System.Text;
using System.Web;

namespace tenpay
{
    public class RequestHandler
    {
        private string gateUrl;
        private string key;
        protected Hashtable parameters;
        private string debugInfo;
        protected HttpContext httpContext;

        public RequestHandler(HttpContext httpContext)
        {
            this.parameters = new Hashtable();
            this.httpContext = httpContext;
            this.setGateUrl("https://www.tenpay.com/cgi-bin/v1.0/service_gate.cgi");
        }

        public virtual void init()
        {
        }

        public string getGateUrl()
        {
            return this.gateUrl;
        }

        public void setGateUrl(string gateUrl)
        {
            this.gateUrl = gateUrl;
        }

        public string getKey()
        {
            return this.key;
        }

        public void setKey(string key)
        {
            this.key = key;
        }

        public virtual string getRequestURL()
        {
            this.createSign();
            StringBuilder stringBuilder = new StringBuilder();
            ArrayList arrayList = new ArrayList(this.parameters.Keys);
            arrayList.Sort();
            foreach (string strB in arrayList)
            {
                string instr = (string)this.parameters[(object)strB];
                if (instr != null && "key".CompareTo(strB) != 0)
                    stringBuilder.Append(strB + "=" + TenpayUtil.UrlEncode(instr, this.getCharset()) + "&");
            }
            if (stringBuilder.Length > 0)
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return this.getGateUrl() + "?" + stringBuilder.ToString();
        }

        protected virtual void createSign()
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
            string parameterValue = MD5Util.GetMD5(stringBuilder.ToString(), this.getCharset()).ToLower();
            this.setParameter("sign", parameterValue);
            this.setDebugInfo(stringBuilder.ToString() + " => sign:" + parameterValue);
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

        public void doSend()
        {
            this.httpContext.Response.Redirect(this.getRequestURL());
        }

        public string getDebugInfo()
        {
            return this.debugInfo;
        }

        public void setDebugInfo(string debugInfo)
        {
            this.debugInfo = debugInfo;
        }

        public Hashtable getAllParameters()
        {
            return this.parameters;
        }

        protected virtual string getCharset()
        {
            return this.httpContext.Request.ContentEncoding.BodyName;
        }
    }
}
