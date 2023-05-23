using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Mobaopay
{
    public class pay : ETAPIBase
    {
        private static int suppId = 10024;

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Mobaopay/Bank_Notify.aspx";
            }
        }

        public pay()
          : base(pay.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string BankID, HttpContext Context)
        {
            string str = "https://trade.mobaopay.com/cgi-bin/netpayment/pay_gate.cgi";
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str = this._suppInfo.jumpUrl + "/switch/Mobaopay.aspx";
            else if (!string.IsNullOrEmpty(this._suppInfo.postBankUrl))
                str = this._suppInfo.postBankUrl;
            Dictionary<string, string> sourceData1 = new Dictionary<string, string>();
            sourceData1.Add("apiName", "WEB_PAY_B2C");
            sourceData1.Add("apiVersion", "1.0.0.0");
            sourceData1.Add("platformID", this.suppAccount);
            sourceData1.Add("merchNo", this.suppUserName);
            sourceData1.Add("orderNo", orderid);
            sourceData1.Add("tradeDate", DateTime.Now.ToString("yyyyMMdd"));
            sourceData1.Add("amt", orderAmt.ToString());
            sourceData1.Add("merchUrl", this.notifyUrl);
            sourceData1.Add("merchParam", "Game");
            sourceData1.Add("tradeSummary", "Game|1");
            if (BankID == "1004")
            {
                sourceData1.Add("choosePayType", "5");
                sourceData1.Add("bankCode", "MOBOACC");
            }
            else if (BankID == "992")
            {
                sourceData1.Add("choosePayType", "4");
                sourceData1.Add("bankCode", "MOBOACC");
            }
            else
                sourceData1.Add("bankCode", this.GetBankCode(BankID));
            string sourceData2 = MobaopayMerchant.Instance.generatePayRequest(sourceData1);
            sourceData1.Add("signMsg", this.sign(sourceData2));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<form id='mobaopaysubmit' name='mobaopaysubmit' action='" + str + "'method='post'>");
            foreach (KeyValuePair<string, string> keyValuePair in sourceData1)
                stringBuilder.Append("<input type='hidden' name='" + keyValuePair.Key + "' value='" + keyValuePair.Value + "'/>");
            stringBuilder.Append("</form>");
            stringBuilder.Append("<script>document.forms['mobaopaysubmit'].submit();</script>");
            return ((object)stringBuilder).ToString();
        }

        public string sign(string sourceData)
        {
            return pay.GetbyteToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(sourceData + this.suppKey)));
        }

        private static string GetbyteToString(byte[] data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < data.Length; ++index)
                stringBuilder.Append(data[index].ToString("x2"));
            return ((object)stringBuilder).ToString();
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "-1":
                    str = "系统忙";
                    break;
                case "1":
                    str = "商户订单号无效";
                    break;
                case "2":
                    str = "银行编码错误";
                    break;
                case "3":
                    str = "商户不存在";
                    break;
                case "4":
                    str = "验证签名失败";
                    break;
                case "5":
                    str = "商户储值关闭";
                    break;
                case "6":
                    str = "金额超出限额";
                    break;
            }
            return str;
        }

        public void Notify()
        {
            Dictionary<string, string> requestPost = this.GetRequestPost();
            string srcData = string.Format("apiName={0}&notifyTime={1}&tradeAmt={2}&merchNo={3}&merchParam={4}&orderNo={5}&tradeDate={6}&accNo={7}&accDate={8}&orderStatus={9}", (object)requestPost["apiName"], (object)requestPost["notifyTime"], (object)requestPost["tradeAmt"], (object)requestPost["merchNo"], (object)requestPost["merchParam"], (object)requestPost["orderNo"], (object)requestPost["tradeDate"], (object)requestPost["accNo"], (object)requestPost["accDate"], (object)requestPost["orderStatus"]);
            string str1 = requestPost["signMsg"];
            string str2 = requestPost["notifyType"];
            bool flag = this.verifyData(str1.Replace("\r", "").Replace("\n", ""), srcData);
            string str3 = flag ? "签名验证通过" : "签名验证失败";
            string str4 = requestPost["apiName"];
            string str5 = requestPost["notifyTime"];
            string s = requestPost["tradeAmt"];
            string str6 = requestPost["merchNo"];
            string str7 = requestPost["merchParam"];
            string orderId = requestPost["orderNo"];
            string str8 = requestPost["tradeDate"];
            string supplierOrderId = requestPost["accNo"];
            string str9 = requestPost["accDate"];
            string str10 = requestPost["orderStatus"];
            try
            {
                if (!flag)
                    return;
                string opstate = "-1";
                int status = 4;
                if (str10 == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(pay.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("SUCCESS");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public bool verifyData(string signData, string srcData)
        {
            return this.sign(srcData).ToUpper().Equals(signData.ToUpper());
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "CMB";
                    break;
                case "967":
                    str2 = "ICBC";
                    break;
                case "964":
                    str2 = "ABC";
                    break;
                case "965":
                    str2 = "CCB";
                    break;
                case "963":
                    str2 = "BOC";
                    break;
                case "977":
                    str2 = "SPDB";
                    break;
                case "981":
                    str2 = "COMM";
                    break;
                case "980":
                    str2 = "CMBC";
                    break;
                case "985":
                    str2 = "CGB";
                    break;
                case "962":
                    str2 = "CNCB";
                    break;
                case "982":
                    str2 = "HXB";
                    break;
                case "972":
                    str2 = "CIB";
                    break;
                case "986":
                    str2 = "CEB";
                    break;
                case "978":
                    str2 = "PAB";
                    break;
                case "971":
                    str2 = "PSBC";
                    break;
                default:
                    str2 = "";
                    break;
            }
            return str2;
        }

        private Dictionary<string, string> GetRequestPost()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            NameValueCollection form = HttpContext.Current.Request.Form;
            foreach (string str in form.AllKeys)
                dictionary.Add(str, form.Get(str));
            return dictionary;
        }
    }
}
