using System;
using System.Web;
using System.Web.Security;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Youqi
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10013;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/youqi/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/youqi/Bank_notify.aspx";
            }
        }

        public Bank()
            : base(Bank.suppId)
        {
        }

        public string GetPayType(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                default:            //在线网银
                    str = "Bank";
                    break;
                case "1008":         //QQ钱包扫码
                    str = "Qqwallet";
                    break;
                case "1009":         //QQ钱包Wap
                    str = "Qqwap";
                    break;
                case "1000":         //网银快捷
                    str = "Quickbank";
                    break;
                case "1005":         //网银快捷WAP
                    str = "Quickwap";
                    break;
                case "2003":         //银联扫码
                    str = "Yinlian";
                    break;
                case "1004":         //微信扫码
                    str = "Weixin";
                    break;
                case "2005":         //微信H5
                    str = "Wxh5";
                    break;
                case "1006":         //支付宝WAP
                    str = "Alipaywap";
                    break;
                case "992":         //支付宝扫码
                    str = "Alipay";
                    break;
            }
            return str;
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "962":
                    str = "CNCB";
                    break;
                case "963":
                    str = "BOCSH";
                    break;
                case "964":
                    str = "ABC";
                    break;
                case "965":
                    str = "CCB";
                    break;
                case "967":
                    str = "ICBC";
                    break;
                case "970":
                    str = "CMB";
                    break;
                case "971":
                    str = "PSBC";
                    break;
                case "972":
                    str = "CIB";
                    break;
                case "985":
                    str = "GDB";
                    break;
                case "986":
                    str = "CEB";
                    break;
                case "977":
                    str = "SPDB";
                    break;
                case "978":
                    str = "PAB";
                    break;
                case "980":
                    str = "CMBC";
                    break;
                case "982":
                    str = "HXB";
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = this.postBankUrl;
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str1 = this._suppInfo.jumpUrl + "/switch/youqi.aspx";
            string version = "1.0";             //版本号
            string customerid = this.suppAccount;   //商户编号
            string sdorderno = orderid;      //商户订单号
            string total_fee = orderAmt.ToString("f2");      //订单金额
            string paytype = this.GetPayType(bankcode);        //支付编号
            string bankcode1 = this.GetBankCode(bankcode);       //银行编号
            string notifyurl = this.notifyUrl;      //异步通知URL
            string returnurl = this.returnurl;      //同步跳转URL
            string remark = "";     //订单备注说明
            string get_code = "";   //获取微信二维码
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile("version=" + version + "&customerid=" + customerid + "&total_fee=" + total_fee + "&sdorderno=" + sdorderno + "&notifyurl=" + notifyurl + "&returnurl=" + returnurl + "&" + this.suppKey, "MD5").ToLower();       //md5签名串
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + version + "\" />" + "<input type=\"hidden\" name=\"customerid\" value=\"" + customerid + "\" />" + "<input type=\"hidden\" name=\"sdorderno\" value=\"" + sdorderno + "\" />" + "<input type=\"hidden\" name=\"total_fee\" value=\"" + total_fee + "\" />" + "<input type=\"hidden\" name=\"paytype\" value=\"" + paytype + "\" />" + "<input type=\"hidden\" name=\"notifyurl\" value=\"" + notifyurl + "\" />" + "<input type=\"hidden\" name=\"returnurl\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"remark\" value=\"" + remark + "\" />" + "<input type=\"hidden\" name=\"bankcode\" value=\"" + bankcode1 + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />" + "<input type=\"hidden\" name=\"get_code\" value=\"" + get_code + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public string PayBankApp(string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = this.postBankUrl;
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str1 = this._suppInfo.jumpUrl + "/switch/youqi.aspx";
            string version = "1.0";             //版本号
            string customerid = this.suppAccount;   //商户编号
            string sdorderno = orderid;      //商户订单号
            string total_fee = orderAmt.ToString("f2");      //订单金额
            string paytype = this.GetPayType(bankcode);        //支付编号
            string bankcode1 = this.GetBankCode(bankcode);       //银行编号
            string notifyurl = this.notifyUrl;      //异步通知URL
            string returnurl = this.returnurl;      //同步跳转URL
            string remark = "";     //订单备注说明
            string get_code = "";   //获取微信二维码
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile("version=" + version + "&customerid=" + customerid + "&total_fee=" + total_fee + "&sdorderno=" + sdorderno + "&notifyurl=" + notifyurl + "&returnurl=" + returnurl + "&" + this.suppKey, "MD5").ToLower();       //md5签名串
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + version + "\" />" + "<input type=\"hidden\" name=\"customerid\" value=\"" + customerid + "\" />" + "<input type=\"hidden\" name=\"sdorderno\" value=\"" + sdorderno + "\" />" + "<input type=\"hidden\" name=\"total_fee\" value=\"" + total_fee + "\" />" + "<input type=\"hidden\" name=\"paytype\" value=\"" + paytype + "\" />" + "<input type=\"hidden\" name=\"notifyurl\" value=\"" + notifyurl + "\" />" + "<input type=\"hidden\" name=\"returnurl\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"remark\" value=\"" + remark + "\" />" + "<input type=\"hidden\" name=\"bankcode\" value=\"" + bankcode1 + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />" + "<input type=\"hidden\" name=\"get_code\" value=\"" + get_code + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public void ReturnBank()
        {
            string status1 = HttpContext.Current.Request["status"].Trim();
            string customerid = HttpContext.Current.Request["customerid"];
            string sdpayno = HttpContext.Current.Request["sdpayno"].Trim();
            string sdorderno = HttpContext.Current.Request["sdorderno"].Trim();
            string total_fee = HttpContext.Current.Request["total_fee"];
            string paytype = HttpContext.Current.Request["paytype"];
            string remark = HttpContext.Current.Request["remark"];
            string sign = HttpContext.Current.Request["sign"];

            string sign2 = FormsAuthentication.HashPasswordForStoringInConfigFile("customerid=" + customerid + "&status=" + status1 + "&sdpayno=" + sdpayno + "&sdorderno=" + sdorderno + "&total_fee=" + total_fee + "&paytype=" + paytype + "&" + this.suppKey, "MD5").ToLower();  //数字签名
            try
            {
                if (!(sign.ToLower() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (status1.Equals("1"))
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, sdorderno, sdpayno, status, opstate, string.Empty, decimal.Parse(total_fee), 0M, false, true);
                HttpContext.Current.Response.Write("success");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public void Notify()
        {
            string status1 = HttpContext.Current.Request["status"].Trim();
            string customerid = HttpContext.Current.Request["customerid"];
            string sdpayno = HttpContext.Current.Request["sdpayno"].Trim();
            string sdorderno = HttpContext.Current.Request["sdorderno"].Trim();
            string total_fee = HttpContext.Current.Request["total_fee"];
            string paytype = HttpContext.Current.Request["paytype"];
            string remark = HttpContext.Current.Request["remark"];
            string sign = HttpContext.Current.Request["sign"];

            string sign2 = FormsAuthentication.HashPasswordForStoringInConfigFile("customerid=" + customerid + "&status=" + status1 + "&sdpayno=" + sdpayno + "&sdorderno=" + sdorderno + "&total_fee=" + total_fee + "&paytype=" + paytype + "&" + this.suppKey, "MD5").ToLower();  //数字签名
            try
            {
                if (!(sign.ToLower() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (status1.Equals("1"))
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, sdorderno, sdpayno, status, opstate, string.Empty, decimal.Parse(total_fee), 0M, true, false);
                HttpContext.Current.Response.Write("success");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

    }
}

