using System;
using System.Collections.Generic;
using System.Web;
using TestEasy;
using viviapi.BLL;

namespace viviapi.ETAPI
{
    public class EasyPay : ETAPIBase
    {
        private static int suppId = 1000;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/EasyPay_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/EasyPay_notify.aspx";
            }
        }

        internal string showUrl
        {
            get
            {
                return this.SiteDomain + "/payresult.aspx";
            }
        }

        public EasyPay()
          : base(EasyPay.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode)
        {
            EasypayConfig easypayConfig = new EasypayConfig();
            string partner = easypayConfig.Partner;
            string key = easypayConfig.Key;
            string sellerEmail = easypayConfig.Seller_email;
            string inputCharset = easypayConfig.Input_charset;
            string notifyUrl = easypayConfig.Notify_url;
            string returnUrl = easypayConfig.Return_url;
            string paymentType = easypayConfig.Payment_type;
            string service = easypayConfig.Service;
            string signType = easypayConfig.Sign_type;
            string out_trade_no = orderid;
            string subject = "subject";
            string body = "easybody";
            string total_fee = Decimal.Round(orderAmt, 2).ToString();
            string paymethod = "bankDirect";
            string bankCode1 = this.GetBankCode(bankCode);
            string buyer_email = "";
            return new Easypay_service(partner, sellerEmail, returnUrl, notifyUrl, out_trade_no, subject, body, total_fee, paymethod, bankCode1, buyer_email, key, inputCharset, signType).Build_Form();
        }

        public void ReturnBank()
        {
            SortedDictionary<string, string> requestGet = this.GetRequestGet();
            EasypayConfig easypayConfig = new EasypayConfig();
            string partner = easypayConfig.Partner;
            string key = easypayConfig.Key;
            string inputCharset = easypayConfig.Input_charset;
            string signType = easypayConfig.Sign_type;
            string transport = easypayConfig.Transport;
            if (requestGet.Count <= 0)
                return;
            string notify_id = HttpContext.Current.Request.QueryString["notify_id"];
            EasypayNotify easypayNotify = new EasypayNotify(requestGet, notify_id, partner, key, inputCharset, signType, transport);
            string responseTxt = easypayNotify.ResponseTxt;
            string str1 = HttpContext.Current.Request.QueryString["sign"];
            string mysign = easypayNotify.Mysign;
            Easypay_function.log_result(HttpContext.Current.Server.MapPath("log/" + DateTime.Now.ToString("yyyyMMddHHmmss")) + ".txt", "responseTxt=" + responseTxt + "\n return_url_log:sign=" + HttpContext.Current.Request.QueryString["sign"] + "&mysign=" + mysign + "\n return回来的参数：" + easypayNotify.PreSignStr);
            if (!(responseTxt == "true") || !(str1 == mysign))
                return;
            string supplierOrderId = HttpContext.Current.Request.QueryString["trade_no"];
            string orderId = HttpContext.Current.Request.QueryString["out_trade_no"];
            string s = HttpContext.Current.Request.QueryString["total_fee"];
            string str2 = HttpContext.Current.Request.QueryString["subject"];
            string str3 = HttpContext.Current.Request.QueryString["body"];
            string str4 = HttpContext.Current.Request.QueryString["buyer_email"];
            string msg = "支付失败" + HttpContext.Current.Request.QueryString["trade_status"];
            string opstate = "-1";
            int status = 4;
            if (HttpContext.Current.Request.QueryString["trade_status"] == "TRADE_FINISHED")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            new OrderBank().DoBankComplete(EasyPay.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), false, true);
        }

        public SortedDictionary<string, string> GetRequestGet()
        {
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            string[] allKeys = HttpContext.Current.Request.QueryString.AllKeys;
            for (int index = 0; index < allKeys.Length; ++index)
                sortedDictionary.Add(allKeys[index], HttpContext.Current.Request.Form[allKeys[index]]);
            return sortedDictionary;
        }

        public void Notify()
        {
            SortedDictionary<string, string> requestPost = this.GetRequestPost();
            EasypayConfig easypayConfig = new EasypayConfig();
            string partner = easypayConfig.Partner;
            string key = easypayConfig.Key;
            string inputCharset = easypayConfig.Input_charset;
            string signType = easypayConfig.Sign_type;
            string transport = easypayConfig.Transport;
            if (requestPost.Count > 0)
            {
                string notify_id = HttpContext.Current.Request.Form["notify_id"];
                EasypayNotify easypayNotify = new EasypayNotify(requestPost, notify_id, partner, key, inputCharset, signType, transport);
                string responseTxt = easypayNotify.ResponseTxt;
                string str1 = HttpContext.Current.Request.Form["sign"];
                string mysign = easypayNotify.Mysign;
                Easypay_function.log_result(HttpContext.Current.Server.MapPath("log/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "yibu") + ".txt", "responseTxt=" + responseTxt + "\n notify_url_log:sign=" + HttpContext.Current.Request.Form["sign"] + "&mysign=" + mysign + "\n notify回来的参数：" + easypayNotify.PreSignStr);
                if (responseTxt == "true" && str1 == mysign)
                {
                    string supplierOrderId = HttpContext.Current.Request.Form["trade_no"];
                    string orderId = HttpContext.Current.Request.Form["out_trade_no"];
                    string s = HttpContext.Current.Request.Form["total_fee"];
                    string str2 = HttpContext.Current.Request.Form["subject"];
                    string str3 = HttpContext.Current.Request.Form["body"];
                    string str4 = HttpContext.Current.Request.Form["buyer_email"];
                    string str5 = HttpContext.Current.Request.Form["trade_status"];
                    string opstate = "-1";
                    int status = 4;
                    if (HttpContext.Current.Request.Form["trade_status"] == "TRADE_FINISHED")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderBank().DoBankComplete(EasyPay.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
                    HttpContext.Current.Response.Write("success");
                }
                else
                    HttpContext.Current.Response.Write("fail");
            }
            else
                HttpContext.Current.Response.Write("无通知参数");
        }

        public SortedDictionary<string, string> GetRequestPost()
        {
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
            for (int index = 0; index < allKeys.Length; ++index)
                sortedDictionary.Add(allKeys[index], HttpContext.Current.Request.Form[allKeys[index]]);
            return sortedDictionary;
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "CMB";
                    break;
                case "967":
                    str = "ICBC";
                    break;
                case "964":
                    str = "ABC";
                    break;
                case "965":
                    str = "CCB";
                    break;
                case "963":
                    str = "BOC";
                    break;
                case "977":
                    str = "SPDB";
                    break;
                case "981":
                    str = "HSBC";
                    break;
                case "980":
                    str = "CMBC";
                    break;
                case "974":
                    str = "SDB";
                    break;
                case "985":
                    str = "GDB";
                    break;
                case "962":
                    str = "CITIC";
                    break;
                case "982":
                    str = "HXBC";
                    break;
                case "972":
                    str = "CIB";
                    break;
                case "984":
                    str = "00011";
                    break;
                case "1015":
                    str = "GZCBK";
                    break;
                case "976":
                    str = "SHRCB";
                    break;
                case "989":
                    str = "00050";
                    break;
                case "988":
                    str = "BHBK";
                    break;
                case "990":
                    str = "00056";
                    break;
                case "979":
                    str = "NJB";
                    break;
                case "986":
                    str = "CEB";
                    break;
                case "987":
                    str = "BEA";
                    break;
                case "1025":
                    str = "NBBK";
                    break;
                case "983":
                    str = "HZBANK";
                    break;
                case "978":
                    str = "SPA";
                    break;
                case "1028":
                    str = "HSBK";
                    break;
                case "968":
                    str = "00086";
                    break;
                case "975":
                    str = "00084";
                    break;
                case "971":
                    str = "PSBC";
                    break;
            }
            return str;
        }
    }
}
