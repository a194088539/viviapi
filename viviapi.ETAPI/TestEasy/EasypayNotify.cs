using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TestEasy
{
    public class EasypayNotify
    {
        private string gateway = "";
        private string _transport = "";
        private string _partner = "";
        private string _key = "";
        private string _input_charset = "";
        private string _sign_type = "";
        private string mysign = "";
        private string responseTxt = "";
        private Dictionary<string, string> sPara = new Dictionary<string, string>();
        private string preSignStr = "";
        private string queryTxt = "";
        private string refundTxt = "";

        public string Mysign
        {
            get
            {
                return this.mysign;
            }
        }

        public string ResponseTxt
        {
            get
            {
                return this.responseTxt;
            }
        }

        public string QueryTxt
        {
            get
            {
                return this.queryTxt;
            }
        }

        public string RefundTxt
        {
            get
            {
                return this.refundTxt;
            }
        }

        public string PreSignStr
        {
            get
            {
                return this.preSignStr;
            }
        }

        public EasypayNotify()
        {
        }

        public EasypayNotify(SortedDictionary<string, string> inputPara, string notify_id, string partner, string key, string input_charset, string sign_type, string transport)
        {
            this._transport = transport;
            this.gateway = !(this._transport == "https") ? "http://mapi.easyebank.com/verify/notify?" : "https://www.easypay.com/cooperate/gateway.do?service=notify_verify";
            this._partner = partner.Trim();
            this._key = key.Trim();
            this._input_charset = input_charset;
            this._sign_type = sign_type.ToUpper();
            this.sPara = Easypay_function.Para_filter(inputPara);
            this.preSignStr = Easypay_function.Create_linkstring(this.sPara);
            this.mysign = Easypay_function.Build_mysign(this.sPara, this._key, this._sign_type, this._input_charset);
            this.responseTxt = this.Verify(notify_id);
        }

        public string Verify(string notify_id)
        {
            string strUrl;
            if (this._transport == "https")
                strUrl = this.gateway + "service=notify_verify&partner=" + this._partner + "&notify_id=" + notify_id;
            else
                strUrl = this.gateway + "partner=" + this._partner + "&notify_id=" + notify_id;
            return this.Get_Http(strUrl, 120000);
        }

        public string Query(string partner, string out_trade_no, string input_charset, string sign_type, string sign)
        {
            this._partner = partner.Trim();
            this._input_charset = input_charset;
            this._sign_type = sign_type.ToUpper();
            return this.Get_Http("http://mapi.easyebank.com/query/payment?" + "partner=" + this._partner + "&_input_charset=" + this._input_charset + "&out_trade_no=" + out_trade_no + "&sign=" + sign + "&sign_type=" + sign_type, 120000);
        }

        public string Refund(string partner, string input_charset, string sign_type, string out_trade_no, string orig_out_trade_no, string amount, string subject, string sign)
        {
            this._partner = partner.Trim();
            this._input_charset = input_charset;
            this._sign_type = sign_type.ToUpper();
            return this.Get_Http("http://mapi.easyebank.com/service/refund?" + "partner=" + this._partner + "&_input_charset=" + this._input_charset + "&sign_type=" + sign_type + "&out_trade_no=" + out_trade_no + "&orig_out_trade_no=" + orig_out_trade_no + "&amount=" + amount + "&subject=" + subject + "&sign=" + sign, 120000);
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
