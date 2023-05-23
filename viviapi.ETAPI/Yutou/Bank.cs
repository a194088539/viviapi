using System;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.ETAPI.Yutou
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10011;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/Yutou_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/Yutou_Notify.aspx";
            }
        }

        public Bank()
            : base(Bank.suppId)
        {
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "308";
                    break;
                case "967":
                    str = "102";
                    break;
                case "964":
                    str = "103";
                    break;
                case "965":
                    str = "105";
                    break;
                case "963":
                    str = "104";
                    break;
                case "977":
                    str = "310";
                    break;
                case "981":
                    str = "301";
                    break;
                case "980":
                    str = "305";
                    break;
                case "985":
                    str = "306";
                    break;
                case "962":
                    str = "302";
                    break;
                case "982":
                    str = "304";
                    break;
                case "972":
                    str = "309";
                    break;
                case "971":
                    str = "403";
                    break;
                case "986":
                    str = "303";
                    break;
                case "978":
                    str = "307";
                    break;
                case "989":
                    str = "313";
                    break;
                case "968":
                    str = "316";
                    break;
                case "988":
                    str = "318";
                    break;
                case "975":
                    str = "325";
                    break;
                case "999":
                    str = "440";
                    break;
            }
            return str;
        }

        public string GetPayType(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "":
                    str = "WY";
                    break;
                case "992":
                    str = "ALIPAY";
                    break;
                case "1004":
                    str = "WEIXIN";
                    break;
                case "2008":
                    str = "QQH5";
                    break;
                case "2005":
                    str = "WXH5";
                    break;
                case "2007":
                    str = "ALIH5";
                    break;
                case "1000":
                    str = "KUAIJIE";
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string payUrl = "http://xs.szjietu.com/gateway/bankTrade/prepay";//请求地址

            string merNo = this.suppAccount;  //商户号
            string orderNo = orderid;    //商户订单号
            string amount = orderAmt.ToString("f2"); //订单金额
            string returnUrl = this.returnurl;  //返回地址
            string notifyUrl = this.notifyUrl;  //异步通知地址
            string payType = this.GetPayType(bankcode);    //支付类别
            string sign = Cryptography.MD5("merNo=" + merNo + "&merSecret=" + this.suppKey + "&amount=" + amount + "&payType=" + payType, "UTF-8");       //md5加密字符串
            string isDirect = "1";   //是否直连
            string bankSegment = this.GetBankCode(bankcode);    //银行编码
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + payUrl + "\">" + "<input type=\"hidden\" name=\"merNo\" value=\"" + merNo + "\" />" + "<input type=\"hidden\" name=\"orderNo\" value=\"" + orderNo + "\" />" + "<input type=\"hidden\" name=\"amount\" value=\"" + amount + "\" />" + "<input type=\"hidden\" name=\"returnUrl\" value=\"" + returnUrl + "\" />" + "<input type=\"hidden\" name=\"notifyUrl\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"payType\" value=\"" + payType + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\" />" + "<input type=\"hidden\" name=\"isDirect\" value=\"" + isDirect + "\" />" + "<input type=\"hidden\" name=\"bankSegment\" value=\"" + bankSegment + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public void ReturnBank()
        {
            string status = HttpContext.Current.Request["status"];   //响应码
            string orderStatus = HttpContext.Current.Request["orderStatus"];    //应答信息
            string orderAmount = HttpContext.Current.Request["orderAmount"];    //支付金额
            string payType = HttpContext.Current.Request["payType"];     //支付方式
            string payoverTime = HttpContext.Current.Request["payoverTime"];    //支付时间
            string orderNo = HttpContext.Current.Request["orderNo"];     //订单号
            string sign = HttpContext.Current.Request["sign"];      //验签
            string sign2 = Cryptography.MD5("status=" + status + "&payType=" + payType + "&orderNo=" + orderNo + "&orderStatus=" + orderStatus + "&orderAmount=" + orderAmount + "&payoverTime=" + payoverTime + "&merSecret=" + this.suppKey, "UTF-8");       //md5加密字符串
            try
            {
                if (!(sign.ToLower() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status2 = 4;
                if (orderStatus.Equals("SUCCESS"))
                {
                    opstate = "0";
                    status2 = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, orderNo, orderNo, status2, opstate, string.Empty, Decimal.Parse(orderAmount), new Decimal(0), false, true);
                HttpContext.Current.Response.Write("SUCCESS");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public void Notify()
        {
            string status = HttpContext.Current.Request["status"];   //响应码
            string orderStatus = HttpContext.Current.Request["orderStatus"];    //应答信息
            string orderAmount = HttpContext.Current.Request["orderAmount"];    //支付金额
            string payType = HttpContext.Current.Request["payType"];     //支付方式
            string payoverTime = HttpContext.Current.Request["payoverTime"];    //支付时间
            string orderNo = HttpContext.Current.Request["orderNo"];     //订单号
            string sign = HttpContext.Current.Request["sign"];      //验签
            string sign2 = Cryptography.MD5("status=" + status + "&payType=" + payType + "&orderNo=" + orderNo + "&orderStatus=" + orderStatus + "&orderAmount=" + orderAmount + "&payoverTime=" + payoverTime + "&merSecret=" + this.suppKey, "UTF-8");       //md5加密字符串
            try
            {
                if (!(sign.ToLower() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status2 = 4;
                if (orderStatus.Equals("SUCCESS"))
                {
                    opstate = "0";
                    status2 = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, orderNo, orderNo, status2, opstate, string.Empty, Decimal.Parse(orderAmount), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("SUCCESS");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

    }
}
