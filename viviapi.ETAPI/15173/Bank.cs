using System;
using System.Web;
using System.Web.Security;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10010;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/Bank15173_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Bank15173_Notify.aspx";
            }
        }

        public Bank()
            : base(Bank.suppId)
        {
        }

        public string GetPayUrl(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "1004":    //微信
                    str = "http://wx.15173.com/WechatPayInterface.aspx";
                    break;
                case "1007":    //微信wap
                    str = "http://wx.15173.net/WechatPayInterfacewap.aspx";
                    break;
                case "1008":    //QQ钱包
                    str = "http://wx.15173.net/QQPayScanInterface.aspx";
                    break;
                case "1009":    //QQ钱包wap
                    str = "http://wx.15173.com/QQPayInterface.aspx";
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            /*
            string str1 = this.postBankUrl;
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str1 = this._suppInfo.jumpUrl + "/switch/Bank15173.aspx";
            */

            string payUrl = this.GetPayUrl(bankcode);//请求地址

            string bargainor_id = this.suppAccount;   //商户ID
            string sp_billno = orderid;  //商户订单号
            string total_fee = orderAmt.ToString("f2");  //交易金额
            string pay_type = "a";   //支付类型
            string return_url = this.returnurl; //同步返回
            string select_url = this.notifyUrl; //异步返回
            string attach = "qianfu";     //
            string zidy_code = "pay";  //
            string czip = ServerVariables.TrueIP;       //用户ip
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile("bargainor_id=" + bargainor_id + "&sp_billno=" + sp_billno + "&pay_type=" + pay_type + "&return_url=" + return_url + "&attach=" + attach + "&key=" + this.suppKey, "MD5").ToUpper();       //md5加密字符串
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + payUrl + "\">" + "<input type=\"hidden\" name=\"bargainor_id\" value=\"" + bargainor_id + "\" />" + "<input type=\"hidden\" name=\"sp_billno\" value=\"" + sp_billno + "\" />" + "<input type=\"hidden\" name=\"total_fee\" value=\"" + total_fee + "\" />" + "<input type=\"hidden\" name=\"pay_type\" value=\"" + pay_type + "\" />" + "<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\" />" + "<input type=\"hidden\" name=\"select_url\" value=\"" + select_url + "\" />" + "<input type=\"hidden\" name=\"attach\" value=\"" + attach + "\" />" + "<input type=\"hidden\" name=\"zidy_code\" value=\"" + zidy_code + "\" />" + "<input type=\"hidden\" name=\"czip\" value=\"" + czip + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public void ReturnBank()
        {
            string pay_result = HttpContext.Current.Request["pay_result"].Trim();   //交易结果
            string transaction_id = HttpContext.Current.Request["transaction_id"].Trim();   //15173交易订单号
            string bargainor_id = HttpContext.Current.Request["bargainor_id"].Trim();   //商户ID
            string sp_billno = HttpContext.Current.Request["sp_billno"].Trim(); //商户订单号
            string pay_info = HttpContext.Current.Request["pay_info"];  //交易类型中文说明
            string total_fee = HttpContext.Current.Request["total_fee"];    //实际交易金额
            string attach = HttpContext.Current.Request["attach"];  //商户自定义字段1
            string zidy_code = HttpContext.Current.Request["zidy_code"];    //商户自定义字段1
            string sign = HttpContext.Current.Request["sign"];       //md5加密字符串
            string sign2 = FormsAuthentication.HashPasswordForStoringInConfigFile("pay_result=" + pay_result + "&bargainor_id=" + bargainor_id + "&sp_billno=" + sp_billno + "&total_fee=" + total_fee + "&attach=" + attach + "&key=" + this.suppKey, "MD5").ToUpper();       //md5加密字符串
            try
            {
                if (!(sign.ToUpper() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (pay_result == "0")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, sp_billno, transaction_id, status, opstate, string.Empty, Decimal.Parse(total_fee), new Decimal(0), false, true);
                HttpContext.Current.Response.Write("ok");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public void Notify()
        {
            string pay_result = HttpContext.Current.Request["pay_result"].Trim();   //交易结果
            string transaction_id = HttpContext.Current.Request["transaction_id"].Trim();   //15173交易订单号
            string bargainor_id = HttpContext.Current.Request["bargainor_id"].Trim();   //商户ID
            string sp_billno = HttpContext.Current.Request["sp_billno"].Trim(); //商户订单号
            string pay_info = HttpContext.Current.Request["pay_info"];  //交易类型中文说明
            string total_fee = HttpContext.Current.Request["total_fee"];    //实际交易金额
            string attach = HttpContext.Current.Request["attach"];  //商户自定义字段1
            string zidy_code = HttpContext.Current.Request["zidy_code"];    //商户自定义字段1
            string sign = HttpContext.Current.Request["sign"];       //md5加密字符串
            string sign2 = FormsAuthentication.HashPasswordForStoringInConfigFile("pay_result=" + pay_result + "&bargainor_id=" + bargainor_id + "&sp_billno=" + sp_billno + "&total_fee=" + total_fee + "&attach=" + attach + "&key=" + this.suppKey, "MD5").ToUpper();       //md5加密字符串
            try
            {
                if (!(sign.ToUpper() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (pay_result == "0")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, sp_billno, transaction_id, status, opstate, string.Empty, Decimal.Parse(total_fee), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("ok");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "Err001":
                    str = "您的商户不存在！";
                    break;
                case "Err002":
                    str = "您的密钥有误！";
                    break;
                case "Err003":
                    str = "您的选择的支付类型不存在！";
                    break;
                case "Err004":
                    str = "您的选择的支付类型暂时停止使用！";
                    break;
                case "Err006":
                    str = "写入失败";
                    break;
                case "Err007":
                    str = "商户单号重复";
                    break;
                case "Err010":
                    str = "支付方式未开通";
                    break;
            }
            return str;
        }

    }
}
