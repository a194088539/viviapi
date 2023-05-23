using System;
using System.Collections.Generic;
using System.Web;
using viviapi.BLL;
using viviapi.ETAPI.Lefupay;

namespace viviapi.ETAPI
{
    public class Lefupayment : ETAPIBase
    {
        private static int suppId = 1012;

        internal string redirectURL
        {
            get
            {
                return this.SiteDomain + "/return/LefupayReturn.aspx";
            }
        }

        internal string notifyURL
        {
            get
            {
                return this.SiteDomain + "/notify/Lefupaynotify.aspx";
            }
        }

        public Lefupayment()
          : base(Lefupayment.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode)
        {
            lefupayConfig lefupayConfig = new lefupayConfig();
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            string inputCharset = lefupayConfig.InputCharset;
            string notifyUrl = lefupayConfig.NotifyURL;
            string redirectUrl = lefupayConfig.RedirectURL;
            string signType = lefupayConfig.SignType;
            string apiCode = lefupayConfig.ApiCode;
            string versionCode = lefupayConfig.VersionCode;
            string buyer = lefupayConfig.Buyer;
            string paymentType = lefupayConfig.PaymentType;
            string outOrderId = orderid;
            string amount = Decimal.Round(orderAmt, 0).ToString();
            string buyerContact = "255520@qq.com";
            string buyerContactType = "email";
            string bankCode1 = this.GetBankCode(bankCode);
            string retryFalg = "TRUE";
            string submitTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string timeout = "1D";
            string clientIP = "127.0.0.1";
            string MyCurrency = "CNY";
            return new lefupay_service(suppAccount, inputCharset, notifyUrl, redirectUrl, signType, apiCode, versionCode, buyer, paymentType, outOrderId, amount, buyerContactType, buyerContact, suppKey, bankCode1, retryFalg, submitTime, timeout, clientIP, MyCurrency).Build_Form();
        }

        public void Notify()
        {
            SortedDictionary<string, string> requestPost = this.GetRequestPost();
            lefupayConfig lefupayConfig = new lefupayConfig();
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            string inputCharset = lefupayConfig.InputCharset;
            string signType = lefupayConfig.SignType;
            string str;
            if (requestPost.Count > 0)
            {
                if (!(HttpContext.Current.Request.Form["sign"] == new lefupayNotify(requestPost, suppAccount, suppKey, inputCharset, signType).Mysign.ToUpper()))
                    return;
                string supplierOrderId = HttpContext.Current.Request.Form["tradeOrderCode"];
                string orderId = HttpContext.Current.Request.Form["outOrderId"];
                string s = HttpContext.Current.Request.Form["amount"];
                str = HttpContext.Current.Request.Form["handlerResult"];
                string opstate = "-1";
                int status = 4;
                if (HttpContext.Current.Request.Form["handlerResult"] == "0000")
                {
                    opstate = "0";
                    status = 2;
                }
                else
                    HttpContext.Current.Response.Write("handlerResult=" + HttpContext.Current.Request.Form["handlerResult"]);
                new OrderBank().DoBankComplete(Lefupayment.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("success");
            }
            else
            {
                SortedDictionary<string, string> requestPosts = this.GetRequestPosts();
                if (requestPosts.Count > 0 && HttpContext.Current.Request.QueryString["sign"] == new lefupayNotify(requestPosts, suppAccount, suppKey, inputCharset, signType).Mysign.ToUpper())
                {
                    string supplierOrderId = HttpContext.Current.Request.QueryString["tradeOrderCode"];
                    string orderId = HttpContext.Current.Request.QueryString["outOrderId"];
                    string s = HttpContext.Current.Request.QueryString["amount"];
                    str = HttpContext.Current.Request.QueryString["handlerResult"];
                    string opstate = "-1";
                    int status = 4;
                    if (HttpContext.Current.Request.QueryString["handlerResult"] == "0000")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    else
                        HttpContext.Current.Response.Write("handlerResult=" + HttpContext.Current.Request.QueryString["handlerResult"]);
                    new OrderBank().DoBankComplete(Lefupayment.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
                    HttpContext.Current.Response.Write("success");
                }
            }
        }

        public void ReturnBank()
        {
            SortedDictionary<string, string> requestPost = this.GetRequestPost();
            lefupayConfig lefupayConfig = new lefupayConfig();
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            string inputCharset = lefupayConfig.InputCharset;
            string signType = lefupayConfig.SignType;
            if (requestPost.Count > 0)
            {
                if (HttpContext.Current.Request.Form["sign"] == new lefupayNotify(requestPost, suppAccount, suppKey, inputCharset, signType).Mysign.ToUpper())
                {
                    string supplierOrderId = HttpContext.Current.Request.Form["tradeOrderCode"];
                    string orderId = HttpContext.Current.Request.Form["outOrderId"];
                    string s = HttpContext.Current.Request.Form["amount"];
                    string str = HttpContext.Current.Request.Form["handlerResult"];
                    string opstate = "-1";
                    int status = 4;
                    if (HttpContext.Current.Request.Form["handlerResult"] == "0000")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    else
                        HttpContext.Current.Response.Write("handlerResult=" + HttpContext.Current.Request.Form["handlerResult"]);
                    new OrderBank().DoBankComplete(Lefupayment.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
                    HttpContext.Current.Response.Write("success");
                }
                else
                    HttpContext.Current.Response.Write("fail");
            }
            else
                HttpContext.Current.Response.Write("无返回参数");
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "B2C_CMB-DEBIT_CARD";
                    break;
                case "967":
                    str2 = "B2C_ICBC-DEBIT_CARD";
                    break;
                case "964":
                    str2 = "B2C_ABC-DEBIT_CARD";
                    break;
                case "965":
                    str2 = "B2C_CCB-DEBIT_CARD";
                    break;
                case "963":
                    str2 = "B2C_BOC-DEBIT_CARD";
                    break;
                case "977":
                    str2 = "B2C_SPDB-DEBIT_CARD";
                    break;
                case "981":
                    str2 = "B2C_BCM-DEBIT_CARD";
                    break;
                case "980":
                    str2 = "B2C_CMBC-DEBIT_CARD";
                    break;
                case "985":
                    str2 = "B2C_CGB-DEBIT_CARD";
                    break;
                case "962":
                    str2 = "B2C_CNCB-DEBIT_CARD";
                    break;
                case "982":
                    str2 = "B2C_HXB-DEBIT_CARD";
                    break;
                case "972":
                    str2 = "B2C_CIB-DEBIT_CARD";
                    break;
                case "989":
                    str2 = "B2C_BOB-DEBIT_CARD";
                    break;
                case "988":
                    str2 = "B2C_CBHB-DEBIT_CARD";
                    break;
                case "990":
                    str2 = "B2C_BJRCB-DEBIT_CARD";
                    break;
                case "979":
                    str2 = "B2C_NJCB-DEBIT_CARD";
                    break;
                case "986":
                    str2 = "B2C_CEB-DEBIT_CARD";
                    break;
                case "1025":
                    str2 = "B2C_NBCB-DEBIT_CARD";
                    break;
                case "983":
                    str2 = "B2C_HCCB-DEBIT_CARD";
                    break;
                case "978":
                    str2 = "B2C_PAB-DEBIT_CARD";
                    break;
                case "968":
                    str2 = "B2C_CMB-CREDIT_CARD";
                    break;
                case "975":
                    str2 = "B2C_BOS-DEBIT_CARD";
                    break;
                case "971":
                    str2 = "B2C_PSBC-DEBIT_CARD";
                    break;
                default:
                    str2 = "OFFLINE_LEFU ";
                    break;
            }
            return str2;
        }

        public SortedDictionary<string, string> GetRequestPost()
        {
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
            for (int index = 0; index < allKeys.Length; ++index)
                sortedDictionary.Add(allKeys[index], HttpContext.Current.Request.Form[allKeys[index]]);
            return sortedDictionary;
        }

        public SortedDictionary<string, string> GetRequestPosts()
        {
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            string[] allKeys = HttpContext.Current.Request.QueryString.AllKeys;
            for (int index = 0; index < allKeys.Length; ++index)
                sortedDictionary.Add(allKeys[index], HttpContext.Current.Request.QueryString[allKeys[index]]);
            return sortedDictionary;
        }
    }
}
