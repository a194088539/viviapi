using System;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.BLL;

namespace viviapi.ETAPI.TongLian2
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10003;

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/tonglian/bankNotify.aspx";
            }
        }
        internal string returnUrl
        {
            get
            {
                return this.SiteDomain + "/notify/tonglian2/bankReturn.aspx";
            }
        }
        public Bank()
            : base(Bank.suppId)
        {
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "962":
                    str2 = "CITIC";
                    break;
                case "963":
                    str2 = "BOC";
                    break;
                case "964":
                    str2 = "ABC";
                    break;
                case "965":
                    str2 = "CCB";
                    break;
                case "966":
                    str2 = "ICBC";
                    break;
                case "967":
                    str2 = "ICBC";
                    break;

                case "970":
                    str2 = "CMB";
                    break;
                case "971":
                    str2 = "PSBC";
                    break;
                case "972":
                    str2 = "CIB";
                    break;

                case "975":
                    str2 = "bos";
                    break;
                case "977":
                    str2 = "SPDB";
                    break;
                case "978":
                    str2 = "pingan";
                    break;
                case "980":
                    str2 = "CMBC";
                    break;
                case "981":
                    str2 = "comm";
                    break;
                case "982":
                    str2 = "HXB";
                    break;
                case "983":
                    str2 = "HZB";
                    break;
                case "985":
                    str2 = "CGB";
                    break;
                case "986":
                    str2 = "CEB";
                    break;
                case "989":
                    str2 = "no";
                    break;
                default:
                    str2 = "no";
                    break;
            }
            return str2;
        }
        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            String srcMsg = null;
            String signMsg = null;
            String serverUrl = "https://service.allinpay.com/gateway/index.do";


            String key = suppKey;
            String version = "v1.0";
            String language = "1";
            String inputCharset = "1";
            String merchantId = suppAccount;
            String pickupUrl = returnUrl;
            String receiveUrl = notifyUrl;
            String payType = "1";
            String signType = "0";
            String orderNo = orderid;
            String orderAmount = (orderAmt * 100).ToString("f0");
            String orderDatetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            String orderCurrency = "0";
            String orderExpireDatetime = "";
            String payerTelephone = "";
            String payerEmail = "";
            String payerIDCard = "";
            String payerName = "";
            String pid = "";
            String productName = orderid;
            String productId = "";
            String productNum = "";
            String productPrice = "";
            String productDesc = "";
            String ext1 = "";
            String ext2 = "";
            String extTL = "";
            String issuerId = GetBankCode(bankcode);
            String pan = "";
            String tradeNature = "";

            if (string.IsNullOrEmpty(postBankUrl))
            {
                serverUrl = "http://ceshi.allinpay.com/gateway/index.do";
                payType = "0";
                issuerId = "";
            }
            else
            {
                serverUrl = postBankUrl;
            }

            StringBuilder formSrc = new StringBuilder();
            StringBuilder bufSignSrc = new StringBuilder();
            formSrc.Append("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + serverUrl + "\">");
            appendFormPara(formSrc, "inputCharset", inputCharset);
            appendFormPara(formSrc, "pickupUrl", pickupUrl);
            appendFormPara(formSrc, "receiveUrl", receiveUrl);
            appendFormPara(formSrc, "version", version);
            appendFormPara(formSrc, "language", language);
            appendFormPara(formSrc, "signType", signType);
            appendFormPara(formSrc, "merchantId", merchantId);
            appendFormPara(formSrc, "payerName", payerName);
            appendFormPara(formSrc, "payerEmail", payerEmail);
            appendFormPara(formSrc, "payerTelephone", payerTelephone);
            appendFormPara(formSrc, "payerIDCard", payerIDCard);
            appendFormPara(formSrc, "pid", pid);
            appendFormPara(formSrc, "orderNo", orderNo);
            appendFormPara(formSrc, "orderAmount", orderAmount);
            appendFormPara(formSrc, "orderCurrency", orderCurrency);
            appendFormPara(formSrc, "orderDatetime", orderDatetime);
            appendFormPara(formSrc, "orderExpireDatetime", orderExpireDatetime);
            appendFormPara(formSrc, "productName", productName);
            appendFormPara(formSrc, "productPrice", productPrice);
            appendFormPara(formSrc, "productNum", productNum);
            appendFormPara(formSrc, "productId", productId);
            appendFormPara(formSrc, "productDesc", productDesc);
            appendFormPara(formSrc, "ext1", ext1);
            appendFormPara(formSrc, "ext2", ext2);
            appendFormPara(formSrc, "extTL", extTL);
            appendFormPara(formSrc, "payType", payType);
            appendFormPara(formSrc, "issuerId", issuerId);
            appendFormPara(formSrc, "pan", pan);
            appendFormPara(formSrc, "tradeNature", tradeNature);
            appendSignPara(bufSignSrc, "inputCharset", inputCharset);
            appendSignPara(bufSignSrc, "pickupUrl", pickupUrl);
            appendSignPara(bufSignSrc, "receiveUrl", receiveUrl);
            appendSignPara(bufSignSrc, "version", version);
            appendSignPara(bufSignSrc, "language", language);
            appendSignPara(bufSignSrc, "signType", signType);
            appendSignPara(bufSignSrc, "merchantId", merchantId);
            appendSignPara(bufSignSrc, "payerName", payerName);
            appendSignPara(bufSignSrc, "payerEmail", payerEmail);
            appendSignPara(bufSignSrc, "payerTelephone", payerTelephone);
            appendSignPara(bufSignSrc, "payerIDCard", payerIDCard);
            appendSignPara(bufSignSrc, "pid", pid);
            appendSignPara(bufSignSrc, "orderNo", orderNo);
            appendSignPara(bufSignSrc, "orderAmount", orderAmount);
            appendSignPara(bufSignSrc, "orderCurrency", orderCurrency);
            appendSignPara(bufSignSrc, "orderDatetime", orderDatetime);
            appendSignPara(bufSignSrc, "orderExpireDatetime", orderExpireDatetime);
            appendSignPara(bufSignSrc, "productName", productName);
            appendSignPara(bufSignSrc, "productPrice", productPrice);
            appendSignPara(bufSignSrc, "productNum", productNum);
            appendSignPara(bufSignSrc, "productId", productId);
            appendSignPara(bufSignSrc, "productDesc", productDesc);
            appendSignPara(bufSignSrc, "ext1", ext1);
            appendSignPara(bufSignSrc, "ext2", ext2);
            appendSignPara(bufSignSrc, "extTL", extTL);
            appendSignPara(bufSignSrc, "payType", payType);
            appendSignPara(bufSignSrc, "issuerId", issuerId);
            appendSignPara(bufSignSrc, "pan", pan);
            appendSignPara(bufSignSrc, "tradeNature", tradeNature);
            appendLastSignPara(bufSignSrc, "key", key);

            srcMsg = bufSignSrc.ToString();
            signMsg = FormsAuthentication.HashPasswordForStoringInConfigFile(srcMsg, "MD5");
            appendFormPara(formSrc, "signMsg", signMsg);
            formSrc.Append("</form><script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>");
            return formSrc.ToString();

        }

        private bool isEmpty(String src)
        {
            if (null == src || "".Equals(src) || "-1".Equals(src))
            {
                return true;
            }
            return false;
        }
        private void appendFormPara(System.Text.StringBuilder buf, String key, String value)
        {
            if (!isEmpty(value))
            {
                buf.Append("<input type=\"hidden\" name=\"").Append(key).Append("\" value=\"").Append(value).Append("\" />");
                //buf.Append(key).Append('=').Append(value).Append('&');
            }
        }
        private void appendSignPara(System.Text.StringBuilder buf, String key, String value)
        {
            if (!isEmpty(value))
            {
                buf.Append(key).Append('=').Append(value).Append('&');
            }
        }

        private void appendLastSignPara(System.Text.StringBuilder buf, String key,
                String value)
        {
            if (!isEmpty(value))
            {
                buf.Append(key).Append('=').Append(value);
            }
        }



        public void ReturnBank()
        {
            String merchantId;
            String version;
            String language;
            String signType;
            String payType;
            String issuerId;
            String paymentOrderId;
            String orderNo;
            String orderDatetime;
            String orderAmount;
            String payDatetime;
            String payAmount;
            String ext1;
            String ext2;
            String payResult;
            String errorCode;
            String returnDatetime;
            String key;
            String signMsg;
            HttpRequest Request = HttpContext.Current.Request;
            merchantId = Request.Form["merchantId"];
            version = Request.Form["version"];
            language = Request.Form["language"];
            signType = Request.Form["signType"];
            payType = Request.Form["payType"];
            issuerId = Request.Form["issuerId"];
            paymentOrderId = Request.Form["paymentOrderId"];
            orderNo = Request.Form["orderNo"];
            orderDatetime = Request.Form["orderDatetime"];
            orderAmount = Request.Form["orderAmount"];
            payDatetime = Request.Form["payDatetime"];
            payAmount = Request.Form["payAmount"];
            ext1 = Request.Form["ext1"];
            ext2 = Request.Form["ext2"];
            payResult = Request.Form["payResult"];
            errorCode = Request.Form["errorCode"];
            returnDatetime = Request.Form["returnDatetime"];
            key = Request.Form["key"];
            signMsg = Request.Form["signMsg"];

            StringBuilder bufSignSrc = new StringBuilder();

            appendSignPara(bufSignSrc, "merchantId", merchantId);
            appendSignPara(bufSignSrc, "version", version);
            appendSignPara(bufSignSrc, "language", language);
            appendSignPara(bufSignSrc, "signType", signType);
            appendSignPara(bufSignSrc, "payType", payType);
            appendSignPara(bufSignSrc, "issuerId", issuerId);
            appendSignPara(bufSignSrc, "paymentOrderId", paymentOrderId);
            appendSignPara(bufSignSrc, "orderNo", orderNo);
            appendSignPara(bufSignSrc, "orderDatetime", orderDatetime);
            appendSignPara(bufSignSrc, "orderAmount", orderAmount);
            appendSignPara(bufSignSrc, "payDatetime", payDatetime);
            appendSignPara(bufSignSrc, "payAmount", payAmount);
            appendSignPara(bufSignSrc, "ext1", ext1);
            appendSignPara(bufSignSrc, "ext2", ext2);
            appendSignPara(bufSignSrc, "payResult", payResult);
            appendSignPara(bufSignSrc, "errorCode", errorCode);
            appendLastSignPara(bufSignSrc, "returnDatetime", returnDatetime);

            String srcMsg = bufSignSrc.ToString();
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (payResult == "1")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
                string str5 = string.Empty;
                new OrderBank().DoBankComplete(Bank.suppId, orderNo, paymentOrderId, status, opstate, msg, Decimal.Parse(orderAmount) / 100m, new Decimal(0), false, true);
            }

        }

        public void Notify()
        {
            String merchantId;
            String version;
            String language;
            String signType;
            String payType;
            String issuerId;
            String paymentOrderId;
            String orderNo;
            String orderDatetime;
            String orderAmount;
            String payDatetime;
            String payAmount;
            String ext1;
            String ext2;
            String payResult;
            String errorCode;
            String returnDatetime;
            String key;
            String signMsg;
            HttpRequest Request = HttpContext.Current.Request;
            merchantId = Request.Form["merchantId"];
            version = Request.Form["version"];
            language = Request.Form["language"];
            signType = Request.Form["signType"];
            payType = Request.Form["payType"];
            issuerId = Request.Form["issuerId"];
            paymentOrderId = Request.Form["paymentOrderId"];
            orderNo = Request.Form["orderNo"];
            orderDatetime = Request.Form["orderDatetime"];
            orderAmount = Request.Form["orderAmount"];
            payDatetime = Request.Form["payDatetime"];
            payAmount = Request.Form["payAmount"];
            ext1 = Request.Form["ext1"];
            ext2 = Request.Form["ext2"];
            payResult = Request.Form["payResult"];
            errorCode = Request.Form["errorCode"];
            returnDatetime = Request.Form["returnDatetime"];
            key = Request.Form["key"];
            signMsg = Request.Form["signMsg"];

            StringBuilder bufSignSrc = new StringBuilder();

            appendSignPara(bufSignSrc, "merchantId", merchantId);
            appendSignPara(bufSignSrc, "version", version);
            appendSignPara(bufSignSrc, "language", language);
            appendSignPara(bufSignSrc, "signType", signType);
            appendSignPara(bufSignSrc, "payType", payType);
            appendSignPara(bufSignSrc, "issuerId", issuerId);
            appendSignPara(bufSignSrc, "paymentOrderId", paymentOrderId);
            appendSignPara(bufSignSrc, "orderNo", orderNo);
            appendSignPara(bufSignSrc, "orderDatetime", orderDatetime);
            appendSignPara(bufSignSrc, "orderAmount", orderAmount);
            appendSignPara(bufSignSrc, "payDatetime", payDatetime);
            appendSignPara(bufSignSrc, "payAmount", payAmount);
            appendSignPara(bufSignSrc, "ext1", ext1);
            appendSignPara(bufSignSrc, "ext2", ext2);
            appendSignPara(bufSignSrc, "payResult", payResult);
            appendSignPara(bufSignSrc, "errorCode", errorCode);
            appendLastSignPara(bufSignSrc, "returnDatetime", returnDatetime);

            String srcMsg = bufSignSrc.ToString();
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (payResult == "1")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
                string str5 = string.Empty;
                new OrderBank().DoBankComplete(Bank.suppId, orderNo, paymentOrderId, status, opstate, msg, Decimal.Parse(orderAmount) / 100m, new Decimal(0), true, false);
            }
        }
    }
}
