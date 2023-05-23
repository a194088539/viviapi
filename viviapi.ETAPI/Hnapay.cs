using System;
using System.Web;
using viviapi.BLL;
using viviLib.Security;

namespace viviapi.ETAPI
{
    public class Hnapay : ETAPIBase
    {
        private static int suppId = 1002;

        internal string returnUrl
        {
            get
            {
                return this.SiteDomain + "/return/Hnapay/Return.aspx";
            }
        }

        internal string noticeUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Hnapay/notify.aspx";
            }
        }

        public Hnapay()
          : base(Hnapay.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode)
        {
            string str1 = "2.6";
            string str2 = this.suppAccount + DateTime.Now.Ticks.ToString();
            string str3 = DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddYears(1);
            string str4 = dateTime.ToString("yyyyMMddHHmmss");
            string str5 = "";
            string str6 = orderid + "," + Decimal.Round(orderAmt * new Decimal(100), 0).ToString() + ",,goodsName,1";
            string str7 = Decimal.Round(orderAmt * new Decimal(100), 0).ToString();
            string str8 = "1000";
            string str9 = "";
            string str10 = "";
            string bankCode1 = this.GetBankCode(bankCode);
            string str11 = "";
            string str12 = "1";
            string str13 = "";
            string str14 = "";
            string str15 = "";
            string suppAccount = this.suppAccount;
            string str16 = "";
            string str17 = "GB2312";
            string str18 = "2";
            Cryptography.MD5("version=" + str1 + "&serialID=" + str2 + "&submitTime=" + str3 + "&failureTime=" + str4 + "&customerIP=" + str5 + "&orderDetails=" + str6 + "&totalAmount=" + str7 + "&type=" + str8 + "&buyerMarked=" + str9 + "&payType=" + str10 + "&orgCode=" + bankCode1 + "&currencyCode=" + str11 + "&directFlag=" + str12 + "&borrowingMarked=" + str13 + "&couponFlag=" + str14 + "&platformID=" + str15 + "&returnUrl=" + this.returnUrl + "&noticeUrl=" + this.noticeUrl + "&partnerID=" + suppAccount + "&remark=" + str16 + "&charset=" + str17 + "&signType=" + str18 + "&pkey=" + this.suppKey, "GB2312");
            string str19 = this.postBankUrl;
            if (string.IsNullOrEmpty(str19))
                str19 = "https://www.hnapay.com/website/pay.htm";
            return "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str19 + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"serialID\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"submitTime\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"failureTime\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"customerIP\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"orderDetails\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"totalAmount\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"type\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"buyerMarked\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"payType\" value=\"" + str10 + "\" />" + "<input type=\"hidden\" name=\"orgCode\" value=\"" + bankCode1 + "\" />" + "<input type=\"hidden\" name=\"currencyCode\" value=\"" + str11 + "\" />" + "<input type=\"hidden\" name=\"directFlag\" value=\"" + str12 + "\" />" + "<input type=\"hidden\" name=\"borrowingMarked\" value=\"" + str13 + "\" />" + "<input type=\"hidden\" name=\"couponFlag\" value=\"" + str14 + "\" />" + "<input type=\"hidden\" name=\"platformID\" value=\"" + str15 + "\" />" + "<input type=\"hidden\" name=\"returnUrl\" value=\"" + this.returnUrl + "\" />" + "<input type=\"hidden\" name=\"noticeUrl\" value=\"" + this.noticeUrl + "\" />" + "<input type=\"hidden\" name=\"partnerID\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"remark\" value=\"" + str16 + "\" />" + "<input type=\"hidden\" name=\"charset\" value=\"" + str17 + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
        }

        public string GetPayForm(string orderid, Decimal orderAmt, string bankCode)
        {
            string str1 = "2.6";
            string str2 = this.suppAccount + DateTime.Now.Ticks.ToString();
            string str3 = DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddYears(1);
            string str4 = dateTime.ToString("yyyyMMddHHmmss");
            string str5 = "";
            string str6 = orderid + "," + Decimal.Round(orderAmt * new Decimal(100), 0).ToString() + ",,goodsName,1";
            string str7 = Decimal.Round(orderAmt * new Decimal(100), 0).ToString();
            string str8 = "1000";
            string str9 = "";
            string str10 = "";
            string bankCode1 = this.GetBankCode(bankCode);
            string str11 = "";
            string str12 = "1";
            string str13 = "";
            string str14 = "";
            string str15 = "";
            string suppAccount = this.suppAccount;
            string str16 = "";
            string str17 = "GB2312";
            string str18 = "2";
            Cryptography.MD5("version=" + str1 + "&serialID=" + str2 + "&submitTime=" + str3 + "&failureTime=" + str4 + "&customerIP=" + str5 + "&orderDetails=" + str6 + "&totalAmount=" + str7 + "&type=" + str8 + "&buyerMarked=" + str9 + "&payType=" + str10 + "&orgCode=" + bankCode1 + "&currencyCode=" + str11 + "&directFlag=" + str12 + "&borrowingMarked=" + str13 + "&couponFlag=" + str14 + "&platformID=" + str15 + "&returnUrl=" + this.returnUrl + "&noticeUrl=" + this.noticeUrl + "&partnerID=" + suppAccount + "&remark=" + str16 + "&charset=" + str17 + "&signType=" + str18 + "&pkey=" + this.suppKey, "GB2312");
            string str19 = this.postBankUrl;
            if (string.IsNullOrEmpty(str19))
                str19 = "https://www.hnapay.com/website/pay.htm";
            return "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str19 + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"serialID\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"submitTime\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"failureTime\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"customerIP\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"orderDetails\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"totalAmount\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"type\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"buyerMarked\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"payType\" value=\"" + str10 + "\" />" + "<input type=\"hidden\" name=\"orgCode\" value=\"" + bankCode1 + "\" />" + "<input type=\"hidden\" name=\"currencyCode\" value=\"" + str11 + "\" />" + "<input type=\"hidden\" name=\"directFlag\" value=\"" + str12 + "\" />" + "<input type=\"hidden\" name=\"borrowingMarked\" value=\"" + str13 + "\" />" + "<input type=\"hidden\" name=\"couponFlag\" value=\"" + str14 + "\" />" + "<input type=\"hidden\" name=\"platformID\" value=\"" + str15 + "\" />" + "<input type=\"hidden\" name=\"returnUrl\" value=\"" + this.returnUrl + "\" />" + "<input type=\"hidden\" name=\"noticeUrl\" value=\"" + this.noticeUrl + "\" />" + "<input type=\"hidden\" name=\"partnerID\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"remark\" value=\"" + str16 + "\" />" + "<input type=\"hidden\" name=\"charset\" value=\"" + str17 + "\" />" + "</form>";
        }

        public void ReturnBank()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request["orderID"].ToString();
            string str1 = request["resultCode"].ToString();
            string str2 = request["stateCode"].ToString();
            string str3 = request["orderAmount"].ToString();
            string s = request["payAmount"].ToString();
            string str4 = request["acquiringTime"].ToString();
            string str5 = request["completeTime"].ToString();
            string supplierOrderId = request["orderNo"].ToString();
            string str6 = request["partnerID"].ToString();
            string str7 = request["remark"].ToString();
            string str8 = request["charset"].ToString();
            string str9 = request["signType"].ToString();
            string str10 = request["signMsg"].ToString();
            string str11 = "orderID=" + orderId + "&resultCode=" + str1 + "&stateCode=" + str2 + "&orderAmount=" + str3 + "&payAmount=" + s + "&acquiringTime=" + str4 + "&completeTime=" + str5 + "&orderNo=" + supplierOrderId + "&partnerID=" + str6 + "&remark=" + str7 + "&charset=" + str8 + "&signType=" + str9;
            bool flag = false;
            if ("2" == str9)
                flag = Cryptography.MD5(str11 + "&pkey=" + this.suppKey).ToLower() == str10.ToLower();
            if (!flag)
                return;
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (str1 == "0000")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            string str12 = string.Empty;
            new OrderBank().DoBankComplete(Hnapay.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), false, true);
        }

        public void Notify()
        {
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request["orderID"].ToString();
            string str1 = request["resultCode"].ToString();
            string str2 = request["stateCode"].ToString();
            string str3 = request["orderAmount"].ToString();
            string s = request["payAmount"].ToString();
            string str4 = request["acquiringTime"].ToString();
            string str5 = request["completeTime"].ToString();
            string supplierOrderId = request["orderNo"].ToString();
            string str6 = request["partnerID"].ToString();
            string str7 = request["remark"].ToString();
            string str8 = request["charset"].ToString();
            string str9 = request["signType"].ToString();
            string str10 = request["signMsg"].ToString();
            string str11 = "orderID=" + orderId + "&resultCode=" + str1 + "&stateCode=" + str2 + "&orderAmount=" + str3 + "&payAmount=" + s + "&acquiringTime=" + str4 + "&completeTime=" + str5 + "&orderNo=" + supplierOrderId + "&partnerID=" + str6 + "&remark=" + str7 + "&charset=" + str8 + "&signType=" + str9;
            bool flag = false;
            if ("2" == str9)
                flag = Cryptography.MD5(str11 + "&pkey=" + this.suppKey).ToLower() == str10.ToLower();
            if (!flag)
                return;
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (str1 == "0000")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            string str12 = string.Empty;
            new OrderBank().DoBankComplete(Hnapay.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), true, false);
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "cmb";
                    break;
                case "967":
                    str2 = "icbc";
                    break;
                case "964":
                    str2 = "abc";
                    break;
                case "965":
                    str2 = "ccb";
                    break;
                case "963":
                    str2 = "boc";
                    break;
                case "977":
                    str2 = "spdb";
                    break;
                case "981":
                    str2 = "comm";
                    break;
                case "980":
                    str2 = "cmbc";
                    break;
                case "974":
                    str2 = "sdb";
                    break;
                case "985":
                    str2 = "gdb";
                    break;
                case "962":
                    str2 = "ecitic";
                    break;
                case "982":
                    str2 = "hxb";
                    break;
                case "972":
                    str2 = "cib";
                    break;
                case "989":
                    str2 = "bccb";
                    break;
                case "986":
                    str2 = "ceb";
                    break;
                case "987":
                    str2 = "bea";
                    break;
                case "1025":
                    str2 = "nb";
                    break;
                case "968":
                    str2 = "00086";
                    break;
                case "975":
                    str2 = "00084";
                    break;
                case "971":
                    str2 = "post";
                    break;
                case "1032":
                    str2 = "unionpay";
                    break;
                default:
                    str2 = "unionpay";
                    break;
            }
            return str2;
        }
    }
}
