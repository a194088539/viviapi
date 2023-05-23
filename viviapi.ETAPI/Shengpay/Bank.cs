using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.Logging;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI.Shengpay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 900;
        internal string currentVer = "3.0";

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/sheng_bank_return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/sheng_bank_notity.aspx";
            }
        }

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode, bool isform)
        {
            string str = string.Empty;
            return !(this.currentVer == "3.0") ? this.PayFormV41111(orderid, orderAmt, bankcode, isform) : this.PayBankV30(orderid, orderAmt, bankcode, isform);
        }

        public string PayBankV30(string orderid, Decimal orderAmt, string bankcode, bool isform)
        {
            string str1 = "3.0";
            string str2 = Decimal.Round(orderAmt, 2).ToString("0.00");
            string str3 = orderid;
            string suppAccount = this.suppAccount;
            string str4 = string.Empty;
            string str5 = "19";
            string returnurl = this.returnurl;
            string notifyUrl = this.notifyUrl;
            string str6 = string.Empty;
            string str7 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string str8 = "RMB";
            string str9 = "http";
            string str10 = "2";
            string str11 = string.Empty;
            string str12 = string.Empty;
            string str13 = string.Empty;
            string str14 = string.Empty;
            string str15 = string.Empty;
            string bankCode = this.GetBankCode(bankcode);
            string str16 = "";
            string str17 = string.Empty;
            string str18 = "gbk";
            string str19 = this.postBankUrl;
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str19 = this._suppInfo.jumpUrl + "/switch/sdopay.aspx";
            string sign = Bank.getSign(str1 + str2 + str3 + suppAccount + str4 + str5 + returnurl + notifyUrl + str6 + str7 + str8 + str9 + str10 + str11 + str12 + str14 + str15 + bankCode + str16 + str18 + str17, "2", this.suppKey);
            string str20 = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str19 + "\">" + "<input type=\"hidden\" name=\"Version\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"Amount\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"OrderNo\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"MerchantNo\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"MerchantUserId\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"PayChannel\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"PostBackUrl\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"NotifyUrl\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"BackUrl\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"OrderTime\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"CurrencyType\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"NotifyUrlType\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"SignType\" value=\"" + str10 + "\" />" + "<input type=\"hidden\" name=\"ProductNo\" value=\"" + str11 + "\" />" + "<input type=\"hidden\" name=\"ProductDesc\" value=\"" + str12 + "\" />" + "<input type=\"hidden\" name=\"Remark1\" value=\"" + str14 + "\" />" + "<input type=\"hidden\" name=\"Remark2\" value=\"" + str15 + "\" />" + "<input type=\"hidden\" name=\"BankCode\" value=\"" + bankCode + "\" />" + "<input type=\"hidden\" name=\"DefaultChannel\" value=\"" + str16 + "\" />" + "<input type=\"hidden\" name=\"ExterInvokeIp\" value=\"" + str17 + "\" />" + "<input type=\"hidden\" name=\"CharSet\" value=\"" + str18 + "\" />" + "<input type=\"hidden\" name=\"MAC\" value=\"" + sign + "\" />" + "</form>";
            if (!isform)
                str20 += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            return str20;
        }

        public string MakeSign(string amount, string orderNo, string merchantNo, string merchantUserId, string orderTime, string productNo, string productDesc, string remark1, string remark2, string bankCode, string productURL, string Version, string PayChannel, string PostBackURL, string NotifyURL, string BackURL, string CurrencyType, string NotifyUrlType, string SignType, string DefaultChannel)
        {
            string signString = Version + amount + orderNo + merchantNo + merchantUserId + PayChannel + PostBackURL + NotifyURL + BackURL + orderTime + CurrencyType + NotifyUrlType + SignType + productNo + productDesc + remark1 + remark2 + bankCode + DefaultChannel + productURL;
            LogHelper.Write("明文：" + signString);
            return Bank.getSign(signString, SignType, this.suppKey);
        }

        private static string getSign(string signString, string signTypeCode, string key)
        {
            SignType byCode = SignType.getByCode(signTypeCode);
            string str = "";
            if (byCode == SignType.MD5)
                str = Bank.byte2mac(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("gbk").GetBytes(signString + key)));
            else if (byCode == SignType.RSA)
                str = "";
            return str;
        }

        public string PayFormV41111(string orderid, Decimal orderAmt, string bankcode, bool isform)
        {
            string str1 = "B2CPayment";
            string str2 = "V4.1.1.1.1";
            string str3 = "GB2312";
            string suppAccount = this.suppAccount;
            string str4 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string str5 = orderid;
            string str6 = Decimal.Round(orderAmt, 2).ToString("0.00");
            string str7 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string str8 = "PT001";
            string str9 = "19";
            string bankCode = this.GetBankCode(bankcode);
            string returnurl = this.returnurl;
            string notifyUrl = this.notifyUrl;
            string str10 = string.Empty;
            string str11 = string.Empty;
            string trueIp = ServerVariables.TrueIP;
            string str12 = string.Empty;
            string str13 = "MD5";
            string str14 = "http://mas.sdo.com/web-acquire-channel/cashier.htm";
            if (!string.IsNullOrEmpty(this.postBankUrl))
                str14 = this.postBankUrl;
            string str15 = Cryptography.MD5(str1 + str2 + str3 + suppAccount + str4 + str5 + str6 + str7 + str8 + str9 + bankCode + returnurl + notifyUrl + str10 + str11 + trueIp + str12 + str13 + this.suppKey).ToUpper();
            string str16 = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str14 + "\">" + "<input type=\"hidden\" name=\"Name\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"Version\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"Charset\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"MsgSender\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"SendTime\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"OrderNo\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"OrderAmount\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"OrderTime\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"PayType\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"PayChannel\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"InstCode\" value=\"" + bankCode + "\" />" + "<input type=\"hidden\" name=\"PageUrl\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"NotifyUrl\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"ProductName\" value=\"" + str10 + "\" />" + "<input type=\"hidden\" name=\"BuyerContact\" value=\"" + str11 + "\" />" + "<input type=\"hidden\" name=\"BuyerIp\" value=\"" + trueIp + "\" />" + "<input type=\"hidden\" name=\"Ext1\" value=\"" + str12 + "\" />" + "<input type=\"hidden\" name=\"SignType\" value=\"" + str13 + "\" />" + "<input type=\"hidden\" name=\"SignMsg\" value=\"" + str15 + "\" />" + "</form>";
            if (!isform)
                str16 += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            return str16;
        }

        public void Notify(bool isNotify)
        {
            if (this.currentVer == "3.0")
                this.Notify30(isNotify);
            else
                this.NotifyV41111(isNotify);
        }

        public void NotifyV41111(bool isNotify)
        {
            string str1 = HttpContext.Current.Request.Form["Name"];
            string str2 = HttpContext.Current.Request.Form["Version"];
            string str3 = HttpContext.Current.Request.Form["Charset"];
            string str4 = HttpContext.Current.Request.Form["TraceNo"];
            string str5 = HttpContext.Current.Request.Form["MsgSender"];
            string str6 = HttpContext.Current.Request.Form["SendTime"];
            string str7 = HttpContext.Current.Request.Form["InstCode"];
            string orderId = HttpContext.Current.Request.Form["OrderNo"];
            string str8 = HttpContext.Current.Request.Form["OrderAmount"];
            string supplierOrderId = HttpContext.Current.Request.Form["TransNo"];
            string s = HttpContext.Current.Request.Form["TransAmount"];
            string str9 = HttpContext.Current.Request.Form["TransStatus"];
            string str10 = HttpContext.Current.Request.Form["TransType"];
            string str11 = HttpContext.Current.Request.Form["TransTime"];
            string str12 = HttpContext.Current.Request.Form["MerchantNo"];
            string str13 = HttpContext.Current.Request.Form["ErrorCode"];
            string str14 = HttpContext.Current.Request.Form["ErrorMsg"];
            string str15 = HttpContext.Current.Request.Form["Ext1"];
            string str16 = HttpContext.Current.Request.Form["SignType"];
            string str17 = HttpContext.Current.Request.Form["SignMsg"];
            if (!(Cryptography.MD5(str1 + str2 + str3 + str4 + str5 + str6 + str7 + orderId + str8 + supplierOrderId + s + str9 + str10 + str11 + str12 + str13 + str14 + str15 + str16 + this.suppKey).ToUpper() == str17) || !(str9 != "00"))
                return;
            string opstate = "-1";
            int status = 4;
            Decimal tranAMT = Decimal.Parse(s);
            if (str9 == "01")
            {
                status = 2;
                opstate = "0";
            }
            new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, string.Empty, tranAMT, new Decimal(0), isNotify, !isNotify);
            HttpContext.Current.Response.Write("OK");
        }

        public void Notify30(bool isNotify)
        {
            string amount = HttpContext.Current.Request.Form["Amount"];
            string str1 = HttpContext.Current.Request.Form["PayAmount"];
            string str2 = HttpContext.Current.Request.Form["OrderNo"];
            string str3 = HttpContext.Current.Request.Form["serialno"];
            string status1 = HttpContext.Current.Request.Form["Status"];
            string merchantNo = HttpContext.Current.Request.Form["MerchantNo"];
            string payChannel = HttpContext.Current.Request.Form["PayChannel"];
            string discount = HttpContext.Current.Request.Form["Discount"];
            string signType = HttpContext.Current.Request.Form["SignType"];
            string payTime = HttpContext.Current.Request.Form["PayTime"];
            string currencyType = HttpContext.Current.Request.Form["CurrencyType"];
            string productNo = HttpContext.Current.Request.Form["ProductNo"];
            string productDesc = HttpContext.Current.Request.Form["ProductDesc"];
            string remark1 = HttpContext.Current.Request.Form["Remark1"];
            string remark2 = HttpContext.Current.Request.Form["Remark2"];
            string exInfo = HttpContext.Current.Request.Form["ExInfo"];
            string mac = HttpContext.Current.Request.Form["MAC"];
            if (this.VerifySign(amount, str1, str2, str3, status1, merchantNo, payChannel, discount, signType, payTime, currencyType, productNo, productDesc, remark1, remark2, exInfo, mac))
            {
                string opstate = "-1";
                int status2 = 4;
                Decimal tranAMT = Decimal.Parse(str1);
                if (status1.Equals("01"))
                {
                    status2 = 2;
                    opstate = "0";
                }
                new OrderBank().DoBankComplete(Bank.suppId, str2, str3, status2, opstate, string.Empty, tranAMT, new Decimal(0), isNotify, !isNotify);
                HttpContext.Current.Response.Write("OK");
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.Write("verify signature error,mac:" + mac + "<br/>");
                HttpContext.Current.Response.End();
            }
        }

        public bool VerifySign(string amount, string payAmount, string orderNo, string serialNo, string status, string merchantNo, string payChannel, string discount, string signType, string payTime, string currencyType, string productNo, string productDesc, string remark1, string remark2, string exInfo, string mac)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(amount).Append("|");
            stringBuilder.Append(payAmount).Append("|");
            stringBuilder.Append(orderNo).Append("|");
            stringBuilder.Append(serialNo).Append("|");
            stringBuilder.Append(status).Append("|");
            stringBuilder.Append(merchantNo).Append("|");
            stringBuilder.Append(payChannel).Append("|");
            stringBuilder.Append(discount).Append("|");
            stringBuilder.Append(signType).Append("|");
            stringBuilder.Append(payTime).Append("|");
            stringBuilder.Append(currencyType).Append("|");
            stringBuilder.Append(productNo).Append("|");
            stringBuilder.Append(productDesc).Append("|");
            stringBuilder.Append(remark1).Append("|");
            stringBuilder.Append(remark2).Append("|");
            stringBuilder.Append(exInfo);
            return this._verifySign(stringBuilder.ToString(), mac, "2", "|" + this.suppKey);
        }

        private bool _verifySign(string signString, string mac, string signTypeCode, string key)
        {
            if (string.IsNullOrEmpty(signString) || string.IsNullOrEmpty(mac))
                return false;
            return Bank.byte2mac(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("gbk").GetBytes(signString + key))).ToUpper().Equals(mac.ToUpper());
        }

        private static string byte2mac(byte[] signed)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in signed)
                stringBuilder.AppendFormat("{0:x2}", (object)num);
            return stringBuilder.ToString();
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
                    str = "COMM";
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
                    str = "HXB";
                    break;
                case "972":
                    str = "CIB";
                    break;
                case "984":
                    str = "GNXS";
                    break;
                case "1015":
                    str = "GZCB";
                    break;
                case "976":
                    str = "SHRCB";
                    break;
                case "971":
                    str = "PSBC";
                    break;
                case "989":
                    str = "BCCB";
                    break;
                case "988":
                    str = "CBHB";
                    break;
                case "990":
                    str = "BJRCB";
                    break;
                case "979":
                    str = "NJCB";
                    break;
                case "986":
                    str = "CEB";
                    break;
                case "987":
                    str = "HKBEA";
                    break;
                case "1025":
                    str = "NBCB";
                    break;
                case "983":
                    str = "HCCB";
                    break;
                case "978":
                    str = "SZPAB";
                    break;
                case "968":
                    str = "00086";
                    break;
                case "975":
                    str = "BOS";
                    break;
                case "WZCB":
                    str = "WZCB";
                    break;
                case "SXJS":
                    str = "SXJS";
                    break;
                case "HKBCHINA":
                    str = "HKBCHINA";
                    break;
                case "ZHNX":
                    str = "ZHNX";
                    break;
                case "SDE":
                    str = "SDE";
                    break;
                case "YDXH":
                    str = "YDXH";
                    break;
            }
            return str;
        }
    }
}
