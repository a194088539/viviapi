using System;
using System.Collections;
using System.Text;
using System.Web;
using tenpay;
using viviapi.BLL;

namespace viviapi.ETAPI
{
    public class TenPayRMB : ETAPIBase
    {
        private static int suppId = 100;

        internal string return_url
        {
            get
            {
                return this.SiteDomain + "/return/ten_return.aspx";
            }
        }

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/ten_notify.aspx";
            }
        }

        public TenPayRMB()
          : base(TenPayRMB.suppId)
        {
        }

        public static string getRealIp()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }

        public string GetPayUrl(string out_trade_no, Decimal amount, string bankcode, HttpContext Context)
        {
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            DateTime.Now.ToString("yyyyMMdd");
            string bankCode = this.GetBankCode(bankcode);
            RequestHandler requestHandler = new RequestHandler(Context);
            requestHandler.init();
            requestHandler.setKey(suppKey);
            string gateUrl = this.postBankUrl;
            if (bankCode == "DEFAULT")
                gateUrl = "https://gw.tenpay.com/gateway/pay.htm";
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                gateUrl = this._suppInfo.jumpUrl + "/switch/tenpay.aspx";
            requestHandler.setGateUrl(gateUrl);
            requestHandler.setParameter("partner", suppAccount);
            requestHandler.setParameter("out_trade_no", out_trade_no);
            requestHandler.setParameter("total_fee", Decimal.Round(amount * new Decimal(100), 0).ToString());
            requestHandler.setParameter("return_url", this.return_url);
            requestHandler.setParameter("notify_url", this.notify_url);
            requestHandler.setParameter("body", "test");
            requestHandler.setParameter("bank_type", bankCode);
            requestHandler.setParameter("spbill_create_ip", Context.Request.UserHostAddress);
            requestHandler.setParameter("fee_type", "1");
            requestHandler.setParameter("subject", "goodname");
            requestHandler.setParameter("sign_type", "MD5");
            requestHandler.setParameter("service_version", "1.0");
            requestHandler.setParameter("input_charset", "GBK");
            requestHandler.setParameter("sign_key_index", "1");
            requestHandler.setParameter("attach", "");
            requestHandler.setParameter("product_fee", "0");
            requestHandler.setParameter("transport_fee", "0");
            requestHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            requestHandler.setParameter("time_expire", "");
            requestHandler.setParameter("buyer_id", "");
            requestHandler.setParameter("goods_tag", "");
            requestHandler.setParameter("trade_mode", "1");
            requestHandler.setParameter("transport_desc", "");
            requestHandler.setParameter("trans_type", "1");
            requestHandler.setParameter("agentid", "");
            requestHandler.setParameter("agent_type", "");
            requestHandler.setParameter("seller_id", "");
            requestHandler.getRequestURL();
            StringBuilder stringBuilder = new StringBuilder("<form id=\"frm1\" method=\"post\" action=\"" + requestHandler.getGateUrl() + "\" >");
            foreach (DictionaryEntry dictionaryEntry in requestHandler.getAllParameters())
                stringBuilder.Append("<input type=\"hidden\" name=\"" + dictionaryEntry.Key + "\" value=\"" + (string)dictionaryEntry.Value + "\" >");
            stringBuilder.Append("</form>");
            stringBuilder.Append("<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>");
            return stringBuilder.ToString();
        }

        public string GetPayForm(string out_trade_no, Decimal amount, string bankcode, HttpContext Context)
        {
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            DateTime.Now.ToString("yyyyMMdd");
            RequestHandler requestHandler = new RequestHandler(Context);
            requestHandler.init();
            requestHandler.setKey(suppKey);
            requestHandler.setGateUrl("https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi");
            requestHandler.setParameter("partner", suppAccount);
            requestHandler.setParameter("out_trade_no", out_trade_no);
            requestHandler.setParameter("total_fee", Decimal.Round(amount * new Decimal(100), 0).ToString());
            requestHandler.setParameter("return_url", this.return_url);
            requestHandler.setParameter("notify_url", this.notify_url);
            requestHandler.setParameter("body", "test");
            requestHandler.setParameter("bank_type", "DEFAULT");
            requestHandler.setParameter("spbill_create_ip", Context.Request.UserHostAddress);
            requestHandler.setParameter("fee_type", "1");
            requestHandler.setParameter("subject", "goodname");
            requestHandler.setParameter("sign_type", "MD5");
            requestHandler.setParameter("service_version", "1.0");
            requestHandler.setParameter("input_charset", "GBK");
            requestHandler.setParameter("sign_key_index", "1");
            requestHandler.setParameter("attach", "");
            requestHandler.setParameter("product_fee", "0");
            requestHandler.setParameter("transport_fee", "0");
            requestHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            requestHandler.setParameter("time_expire", "");
            requestHandler.setParameter("buyer_id", "");
            requestHandler.setParameter("goods_tag", "");
            requestHandler.setParameter("trade_mode", "1");
            requestHandler.setParameter("transport_desc", "");
            requestHandler.setParameter("trans_type", "1");
            requestHandler.setParameter("agentid", "");
            requestHandler.setParameter("agent_type", "");
            requestHandler.setParameter("seller_id", "");
            requestHandler.getRequestURL();
            StringBuilder stringBuilder = new StringBuilder("<form id=\"frm1\" method=\"post\" action=\"https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi\" >");
            foreach (DictionaryEntry dictionaryEntry in requestHandler.getAllParameters())
                stringBuilder.Append("<input type=\"hidden\" name=\"" + dictionaryEntry.Key + "\" value=\"" + (string)dictionaryEntry.Value + "\" >");
            stringBuilder.Append("</form>");
            return stringBuilder.ToString();
        }

        public void Return(HttpContext Context)
        {
            string suppKey = this.suppKey;
            ResponseHandler responseHandler = new ResponseHandler(Context);
            responseHandler.setKey(suppKey);
            if (!responseHandler.isTenpaySign())
                return;
            responseHandler.getParameter("notify_id");
            string parameter1 = responseHandler.getParameter("out_trade_no");
            string parameter2 = responseHandler.getParameter("transaction_id");
            string parameter3 = responseHandler.getParameter("total_fee");
            responseHandler.getParameter("discount");
            string parameter4 = responseHandler.getParameter("trade_state");
            string parameter5 = responseHandler.getParameter("trade_mode");
            if ("1".Equals(parameter5))
            {
                string msg = "支付失败" + responseHandler.getDebugInfo();
                string opstate = "-1";
                int status = 4;
                if ("0".Equals(parameter4))
                {
                    msg = "支付成功";
                    status = 2;
                    opstate = "0";
                }
                new OrderBank().DoBankComplete(TenPayRMB.suppId, parameter1, parameter2, status, opstate, msg, Decimal.Parse(parameter3) / new Decimal(100), new Decimal(0), false, true);
            }
            else if (!"2".Equals(parameter5) || !"0".Equals(parameter4))
                ;
        }

        public void Notify(HttpContext Context)
        {
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            ResponseHandler responseHandler = new ResponseHandler(Context);
            responseHandler.setKey(suppKey);
            if (!responseHandler.isTenpaySign())
                return;
            string parameter1 = responseHandler.getParameter("notify_id");
            RequestHandler requestHandler = new RequestHandler(Context);
            requestHandler.init();
            requestHandler.setKey(suppKey);
            requestHandler.setGateUrl("https://gw.tenpay.com/gateway/verifynotifyid.xml");
            requestHandler.setParameter("partner", suppAccount);
            requestHandler.setParameter("notify_id", parameter1);
            TenpayHttpClient tenpayHttpClient = new TenpayHttpClient();
            tenpayHttpClient.setTimeOut(5);
            tenpayHttpClient.setReqContent(requestHandler.getRequestURL());
            if (!tenpayHttpClient.call())
                return;
            ClientResponseHandler clientResponseHandler = new ClientResponseHandler();
            clientResponseHandler.setContent(tenpayHttpClient.getResContent());
            clientResponseHandler.setKey(suppKey);
            if (!clientResponseHandler.isTenpaySign())
                return;
            string parameter2 = clientResponseHandler.getParameter("out_trade_no");
            string parameter3 = clientResponseHandler.getParameter("transaction_id");
            string parameter4 = clientResponseHandler.getParameter("total_fee");
            clientResponseHandler.getParameter("discount");
            string parameter5 = responseHandler.getParameter("trade_state");
            string parameter6 = responseHandler.getParameter("trade_mode");
            if (!"0".Equals(clientResponseHandler.getParameter("retcode")))
                return;
            if ("1".Equals(parameter6))
            {
                string opstate = "-1";
                int status = 4;
                if ("0".Equals(parameter5))
                {
                    status = 2;
                    opstate = "0";
                    Context.Response.Write("success");
                }
                else
                    Context.Response.Write("即时到账支付失败");
                new OrderBank().DoBankComplete(TenPayRMB.suppId, parameter2, parameter3, status, opstate, string.Empty, Decimal.Parse(parameter4) / new Decimal(100), new Decimal(0), true, false);
            }
            else
            {
                if (!"2".Equals(parameter6))
                    return;
                switch (Convert.ToInt32(parameter5))
                {
                }
            }
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "1001";
                    break;
                case "967":
                    str2 = "1002";
                    break;
                case "964":
                    str2 = "1005";
                    break;
                case "965":
                    str2 = "1003";
                    break;
                case "963":
                    str2 = "1052";
                    break;
                case "977":
                    str2 = "1004";
                    break;
                case "981":
                    str2 = "1020";
                    break;
                case "980":
                    str2 = "1006";
                    break;
                case "974":
                    str2 = "1008";
                    break;
                case "985":
                    str2 = "1027";
                    break;
                case "962":
                    str2 = "1021";
                    break;
                case "982":
                    str2 = "1025";
                    break;
                case "972":
                    str2 = "1009";
                    break;
                case "989":
                    str2 = "1032";
                    break;
                case "986":
                    str2 = "1022";
                    break;
                case "978":
                    str2 = "1010";
                    break;
                case "975":
                    str2 = "1024";
                    break;
                case "971":
                    str2 = "1028";
                    break;
                default:
                    str2 = "DEFAULT";
                    break;
            }
            return str2;
        }
    }
}
