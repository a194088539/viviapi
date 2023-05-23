using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace viviapi.ETAPI.Lefupay
{
    public class lefupayNotify
    {
        private string gateway = "";
        private string _partner = "";
        private string _key = "";
        private string _inputCharset = "";
        private string _sign_type = "";
        private string mysign = "";
        private Dictionary<string, string> sPara = new Dictionary<string, string>();
        private string preSignStr = "";

        public string Mysign
        {
            get
            {
                return this.mysign;
            }
        }

        public string PreSignStr
        {
            get
            {
                return this.preSignStr;
            }
        }

        public lefupayNotify()
        {
        }

        public lefupayNotify(SortedDictionary<string, string> inputPara, string partner, string key, string inputCharset, string signType)
        {
            this._partner = partner.Trim();
            this._key = key.Trim();
            this._inputCharset = inputCharset;
            this._sign_type = signType;
            this.sPara = lefupay_function.Para_filter(inputPara);
            this.preSignStr = lefupay_function.Create_linkstring(this.sPara);
            this.mysign = lefupay_function.Build_mysign(this.sPara, this._key, this._sign_type, this._inputCharset);
        }

        private string Get_Http(string strUrl, int timeout)
        {
            string str;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                httpWebRequest.Timeout = timeout;
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default);
                StringBuilder stringBuilder = new StringBuilder();
                while (-1 != streamReader.Peek())
                    stringBuilder.Append(streamReader.ReadLine());
                str = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                str = "错误：" + ex.Message;
            }
            return str;
        }
    }
}
