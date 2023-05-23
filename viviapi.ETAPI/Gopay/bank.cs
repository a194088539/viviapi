using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.Logging;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI
{
    public class Gopay : ETAPIBase
    {
        private static int suppId = 10021;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/Gopay_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Gopay_notify.aspx";
            }
        }

        internal string showUrl
        {
            get
            {
                return this.SiteDomain + "/payresult.aspx";
            }
        }

        public Gopay()
          : base(Gopay.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode)
        {
            string str1 = "2.1";
            string str2 = "1";
            string str3 = "1";
            string str4 = "1";
            string str5 = "8888";
            string suppAccount = this.suppAccount;
            string str6 = orderid;
            string str7 = Decimal.Round(orderAmt, 2).ToString();
            string str8 = "0";
            string str9 = "156";
            string str10 = string.Empty;
            string notifyUrl = this.notifyUrl;
            string str11 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string suppUserName = this.suppUserName;
            string str12 = "127.0.0.1";
            string str13 = "1";
            string str14 = "";
            string str15 = "";
            string str16 = "";
            string str17 = "";
            string str18 = "";
            string str19 = "";
            bankCode = this.GetBankCode(bankCode);
            string gopayServerTime = Gopay.GetGopayServerTime();
            string suppKey = this.suppKey;
            string strToEncrypt = "version=[" + str1 + "]tranCode=[" + str5 + "]merchantID=[" + suppAccount + "]merOrderNum=[" + str6 + "]tranAmt=[" + str7 + "]feeAmt=[" + str8 + "]tranDateTime=[" + str11 + "]frontMerUrl=[" + str10 + "]backgroundMerUrl=[" + notifyUrl + "]orderId=[]gopayOutOrderId=[]tranIP=[" + str12 + "]respCode=[]gopayServerTime=[" + gopayServerTime + "]VerficationCode=[" + suppKey + "]";
            LogHelper.Write("国付宝明文:" + strToEncrypt);
            string str20 = Cryptography.MD5(strToEncrypt);
            string str21 = this.postBankUrl;
            if (string.IsNullOrEmpty(str21))
                str21 = "https://211.88.7.30/PGServer/Trans/WebClientAction.do";
            return "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str21 + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"charset\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"language\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"signType\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"tranCode\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"merchantID\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"merOrderNum\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"tranAmt\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"feeAmt\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"currencyType\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"frontMerUrl\" value=\"" + str10 + "\" />" + "<input type=\"hidden\" name=\"backgroundMerUrl\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"tranDateTime\" value=\"" + str11 + "\" />" + "<input type=\"hidden\" name=\"virCardNoIn\" value=\"" + suppUserName + "\" />" + "<input type=\"hidden\" name=\"tranIP\" value=\"" + str12 + "\" />" + "<input type=\"hidden\" name=\"isRepeatSubmit\" value=\"" + str13 + "\" />" + "<input type=\"hidden\" name=\"goodsName\" value=\"" + str14 + "\" />" + "<input type=\"hidden\" name=\"goodsDetail\" value=\"" + str15 + "\" />" + "<input type=\"hidden\" name=\"buyerName\" value=\"" + str16 + "\" />" + "<input type=\"hidden\" name=\"buyerContact\" value=\"" + str17 + "\" />" + "<input type=\"hidden\" name=\"merRemark1\" value=\"" + str18 + "\" />" + "<input type=\"hidden\" name=\"merRemark2\" value=\"" + str19 + "\" />" + "<input type=\"hidden\" name=\"bankCode\" value=\"" + bankCode + "\" />" + "<input type=\"hidden\" name=\"userType\" value=\"" + PaymentSetting.Gopay_userType + "\" />" + "<input type=\"hidden\" name=\"VerficationCode\" value=\"" + suppKey + "\" />" + "<input type=\"hidden\" name=\"signValue\" value=\"" + str20 + "\" />" + "<input type=\"hidden\" name=\"gopayServerTime\" value=\"" + gopayServerTime + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
        }

        public string GetPayForm(string orderid, Decimal orderAmt, string bankCode)
        {
            string str1 = "2.1";
            string str2 = "1";
            string str3 = "1";
            string str4 = "1";
            string str5 = "8888";
            string suppAccount = this.suppAccount;
            string str6 = orderid;
            string str7 = Decimal.Round(orderAmt, 2).ToString();
            string str8 = "0";
            string str9 = "156";
            string str10 = string.Empty;
            string notifyUrl = this.notifyUrl;
            string str11 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string suppUserName = this.suppUserName;
            string trueIp = ServerVariables.TrueIP;
            string str12 = "1";
            string str13 = "";
            string str14 = "";
            string str15 = "";
            string str16 = "";
            string str17 = "";
            string str18 = "";
            bankCode = this.GetBankCode(bankCode);
            string gopayServerTime = Gopay.GetGopayServerTime();
            string suppKey = this.suppKey;
            string str19 = Cryptography.MD5("version=[" + str1 + "]tranCode=[" + str5 + "]merchantID=[" + suppAccount + "]merOrderNum=[" + str6 + "]tranAmt=[" + str7 + "]feeAmt=[" + str8 + "]tranDateTime=[" + str11 + "]frontMerUrl=[" + str10 + "]backgroundMerUrl=[" + notifyUrl + "]orderId=[]gopayOutOrderId=[]tranIP=[" + trueIp + "]respCode=[]gopayServerTime=[" + gopayServerTime + "]VerficationCode=[" + suppKey + "]");
            string str20 = this.postBankUrl;
            if (string.IsNullOrEmpty(str20))
                str20 = "https://211.88.7.30/PGServer/Trans/WebClientAction.do";
            return "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str20 + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"charset\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"language\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"signType\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"tranCode\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"merchantID\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"merOrderNum\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"tranAmt\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"feeAmt\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"currencyType\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"frontMerUrl\" value=\"" + str10 + "\" />" + "<input type=\"hidden\" name=\"backgroundMerUrl\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"tranDateTime\" value=\"" + str11 + "\" />" + "<input type=\"hidden\" name=\"virCardNoIn\" value=\"" + suppUserName + "\" />" + "<input type=\"hidden\" name=\"tranIP\" value=\"" + trueIp + "\" />" + "<input type=\"hidden\" name=\"isRepeatSubmit\" value=\"" + str12 + "\" />" + "<input type=\"hidden\" name=\"goodsName\" value=\"" + str13 + "\" />" + "<input type=\"hidden\" name=\"goodsDetail\" value=\"" + str14 + "\" />" + "<input type=\"hidden\" name=\"buyerName\" value=\"" + str15 + "\" />" + "<input type=\"hidden\" name=\"buyerContact\" value=\"" + str16 + "\" />" + "<input type=\"hidden\" name=\"merRemark1\" value=\"" + str17 + "\" />" + "<input type=\"hidden\" name=\"merRemark2\" value=\"" + str18 + "\" />" + "<input type=\"hidden\" name=\"bankCode\" value=\"" + bankCode + "\" />" + "<input type=\"hidden\" name=\"userType\" value=\"" + PaymentSetting.Gopay_userType + "\" />" + "<input type=\"hidden\" name=\"VerficationCode\" value=\"" + suppKey + "\" />" + "<input type=\"hidden\" name=\"signValue\" value=\"" + str19 + "\" />" + "<input type=\"hidden\" name=\"gopayServerTime\" value=\"" + gopayServerTime + "\" />" + "</form>";
        }

        public static string sign(string str)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(str));
            string str1 = "";
            for (int index = 0; index < hash.Length; ++index)
                str1 = str1 + hash[index].ToString("x").PadLeft(2, '0');
            return str1;
        }

        public void ReturnBank()
        {
            string str1 = HttpContext.Current.Request.Form["version"];
            string str2 = HttpContext.Current.Request.Form["charset"];
            string str3 = HttpContext.Current.Request.Form["language"];
            string str4 = HttpContext.Current.Request.Form["signType"];
            string str5 = HttpContext.Current.Request.Form["tranCode"];
            string str6 = HttpContext.Current.Request.Form["merchantID"];
            string orderId = HttpContext.Current.Request.Form["merOrderNum"];
            string s = HttpContext.Current.Request.Form["tranAmt"];
            string str7 = HttpContext.Current.Request.Form["feeAmt"];
            string str8 = HttpContext.Current.Request.Form["frontMerUrl"];
            string str9 = HttpContext.Current.Request.Form["backgroundMerUrl"];
            string str10 = HttpContext.Current.Request.Form["tranDateTime"];
            string str11 = HttpContext.Current.Request.Form["tranIP"];
            string str12 = HttpContext.Current.Request.Form["respCode"];
            string str13 = HttpContext.Current.Request.Form["msgExt"];
            string str14 = HttpContext.Current.Request.Form["orderId"];
            string supplierOrderId = HttpContext.Current.Request.Form["gopayOutOrderId"];
            string str15 = HttpContext.Current.Request.Form["bankCode"];
            string str16 = HttpContext.Current.Request.Form["tranFinishTime"];
            string str17 = HttpContext.Current.Request.Form["merRemark1"];
            string str18 = HttpContext.Current.Request.Form["merRemark2"];
            string suppKey = this.suppKey;
            string str19 = HttpContext.Current.Request.Form["signValue"];
            if (Cryptography.MD5("version=[" + str1 + "]tranCode=[" + str5 + "]merchantID=[" + str6 + "]merOrderNum=[" + orderId + "]tranAmt=[" + s + "]feeAmt=[" + str7 + "]tranDateTime=[" + str10 + "]frontMerUrl=[" + str8 + "]backgroundMerUrl=[" + str9 + "]orderId=[" + str14 + "]gopayOutOrderId=[" + supplierOrderId + "]tranIP=[" + str11 + "]respCode=[" + str12 + "]gopayServerTime=[]VerficationCode=[" + suppKey + "]", "gbk").Equals(str19))
            {
                string msg = "支付失败" + str13;
                string opstate = "-1";
                int status = 4;
                if (str12.Equals("0000"))
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                }
                string returnUrl = string.Empty;
                new OrderBank().DoBankComplete(Gopay.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), out returnUrl);
                HttpContext.Current.Response.Write("RespCode=0000|JumpURL=" + returnUrl);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("o={0}", (object)orderId);
                stringBuilder.AppendFormat("&t={0}", (object)102);
                stringBuilder.AppendFormat("&v={0}", (object)s);
                stringBuilder.AppendFormat("&e={0}", (object)str13);
                HttpContext.Current.Response.Write("RespCode=9999|JumpURL=" + this.showUrl + "?" + ((object)stringBuilder).ToString());
            }
        }

        public void Notify()
        {
        }

        public static string GetGopayServerTime()
        {
            try
            {
                return WebClientHelper.GetString("https://www.gopay.com.cn/PGServer/time", (string)null, "get", Encoding.UTF8, 10000).Trim();
            }
            catch
            {
                return string.Empty;
            }
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
                    str = "BOCOM";
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
                case "976":
                    str = "00030";
                    break;
                case "989":
                    str = "00050";
                    break;
                case "990":
                    str = "00056";
                    break;
                case "979":
                    str = "00055";
                    break;
                case "986":
                    str = "CEB";
                    break;
                case "983":
                    str = "00081";
                    break;
                case "978":
                    str = "00087";
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
