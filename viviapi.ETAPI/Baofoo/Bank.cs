using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;

namespace viviapi.ETAPI.Baofoo
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 1003;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/Baofoo/Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Baofoo/callback.aspx";
            }
        }

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode, bool isForm)
        {
            string str1 = "http://paygate.baofoo.com/PayReceive/payindex.aspx";
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str1 = this._suppInfo.jumpUrl + "/switch/baofoo.aspx";
            else if (!string.IsNullOrEmpty(this._suppInfo.postBankUrl))
                str1 = this._suppInfo.postBankUrl;
            string suppAccount = this.suppAccount;
            string bankCode1 = this.GetBankCode(bankCode);
            string _TradeDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string _TransID = orderid;
            string _OrderMoney = Decimal.Round(orderAmt * new Decimal(100), 0).ToString();
            string str2 = "";
            string str3 = "1";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string returnurl = this.returnurl;
            string notifyUrl = this.notifyUrl;
            string _NoticeType = "1";
            string suppKey = this.suppKey;
            string md5Sign = this.GetMd5Sign(suppAccount, bankCode1, _TradeDate, _TransID, _OrderMoney, returnurl, notifyUrl, _NoticeType, suppKey);
            string str9 = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"MerchantID\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"PayID\" value=\"" + bankCode1 + "\" />" + "<input type=\"hidden\" name=\"TradeDate\" value=\"" + _TradeDate + "\" />" + "<input type=\"hidden\" name=\"TransID\" value=\"" + _TransID + "\" />" + "<input type=\"hidden\" name=\"OrderMoney\" value=\"" + _OrderMoney + "\" />" + "<input type=\"hidden\" name=\"ProductName\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"Amount\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"ProductLogo\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"Username\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"Email\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"Mobile\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"AdditionalInfo\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"Merchant_url\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"Return_url\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"Md5Sign\" value=\"" + md5Sign + "\" />" + "<input type=\"hidden\" name=\"NoticeType\" value=\"" + _NoticeType + "\" />" + "</form>";
            if (!isForm)
                str9 = str9 + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            return str9;
        }

        private string GetMd5Sign(string _MerchantID, string _PayID, string _TradeDate, string _TransID, string _OrderMoney, string _Merchant_url, string _Return_url, string _NoticeType, string _Md5Key)
        {
            return Bank.Md5Encrypt(_MerchantID + _PayID + _TradeDate + _TransID + _OrderMoney + _Merchant_url + _Return_url + _NoticeType + _Md5Key);
        }

        public static string Md5Encrypt(string strToBeEncrypt)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(strToBeEncrypt));
            string str = "";
            for (int index = 0; index < hash.Length; ++index)
                str = str + hash[index].ToString("x2");
            return str.ToLower();
        }

        public void ReturnBank()
        {
            string str1 = HttpContext.Current.Request.Params["MerchantID"];
            string orderId = HttpContext.Current.Request.Params["TransID"];
            string result = HttpContext.Current.Request.Params["Result"];
            string resultDesc = HttpContext.Current.Request.Params["resultDesc"];
            string s = HttpContext.Current.Request.Params["factMoney"];
            string str2 = HttpContext.Current.Request.Params["additionalInfo"];
            string str3 = HttpContext.Current.Request.Params["SuccTime"];
            string str4 = HttpContext.Current.Request.Params["Md5Sign"].ToLower();
            string suppKey = this.suppKey;
            string strToBeEncrypt = str1 + orderId + result + resultDesc + s + str2 + str3 + suppKey;
            if (!(str4.ToLower() == Bank.Md5Encrypt(strToBeEncrypt).ToLower()))
                return;
            string msg = "支付失败 原因" + this.GetErrorInfo(result, resultDesc);
            string opstate = "-1";
            int status = 4;
            if (result.Equals("1"))
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            string str5 = string.Empty;
            new OrderBank().DoBankComplete(Bank.suppId, orderId, "", status, opstate, msg, Decimal.Parse(s) / new Decimal(100), new Decimal(0), false, true);
        }

        public string GetErrorInfo(string result, string resultDesc)
        {
            if (result == "1")
                return "支付成功";
            string str;
            switch (resultDesc)
            {
                case "0000":
                    str = "充值失败";
                    break;
                case "0001":
                    str = "系统错误";
                    break;
                case "0002":
                    str = "订单超时";
                    break;
                case "0003":
                    str = "订单状态异常";
                    break;
                case "0004":
                    str = "无效商户";
                    break;
                case "0015":
                    str = "卡号或卡密错误";
                    break;
                case "0016":
                    str = "不合法的IP地址";
                    break;
                case "0018":
                    str = "卡密已被使用";
                    break;
                case "0019":
                    str = "订单金额错误";
                    break;
                case "0020":
                    str = "支付的类型错误";
                    break;
                case "0021":
                    str = "卡类型有误";
                    break;
                case "0022":
                    str = "卡信息不完整";
                    break;
                case "0023":
                    str = "卡号，卡密，金额不正确";
                    break;
                case "0024":
                    str = "不能用此卡继续做交易";
                    break;
                case "0025":
                    str = "订单无效";
                    break;
                default:
                    str = "支付失败";
                    break;
            }
            return str;
        }

        public void Notify()
        {
            string str1 = HttpContext.Current.Request.Params["MerchantID"];
            string orderId = HttpContext.Current.Request.Params["TransID"];
            string result = HttpContext.Current.Request.Params["Result"];
            string resultDesc = HttpContext.Current.Request.Params["resultDesc"];
            string s = HttpContext.Current.Request.Params["factMoney"];
            string str2 = HttpContext.Current.Request.Params["additionalInfo"];
            string str3 = HttpContext.Current.Request.Params["SuccTime"];
            string str4 = HttpContext.Current.Request.Params["Md5Sign"].ToLower();
            string suppKey = this.suppKey;
            string strToBeEncrypt = str1 + orderId + result + resultDesc + s + str2 + str3 + suppKey;
            if (str4.ToLower() == Bank.Md5Encrypt(strToBeEncrypt).ToLower())
            {
                string msg = "支付失败 原因" + this.GetErrorInfo(result, resultDesc);
                string opstate = "-1";
                int status = 4;
                if (result.Equals("1"))
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                }
                string str5 = string.Empty;
                new OrderBank().DoBankComplete(Bank.suppId, orderId, "", status, opstate, msg, Decimal.Parse(s) / new Decimal(100), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("OK");
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.Write("Md5CheckFail");
                HttpContext.Current.Response.End();
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
                    str2 = "1026";
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
                    str2 = "1036";
                    break;
                case "962":
                    str2 = "1039";
                    break;
                case "972":
                    str2 = "1009";
                    break;
                case "1016":
                    str2 = "1080";
                    break;
                case "976":
                    str2 = "1037";
                    break;
                case "971":
                    str2 = "1038";
                    break;
                case "989":
                    str2 = "1032";
                    break;
                case "988":
                    str2 = "1034";
                    break;
                case "986":
                    str2 = "1022";
                    break;
                case "987":
                    str2 = "1033";
                    break;
                case "978":
                    str2 = "1035";
                    break;
                default:
                    str2 = "1000";
                    break;
            }
            return str2;
        }
    }
}
