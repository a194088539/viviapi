using System;
using System.Web;
using System.Web.Security;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Yt
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10006;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/yt/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/yt/Bank_notify.aspx";
            }
        }

        public Bank()
            : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string payUrl = "https://cashier.etonepay.com/NetPay/BankSelect.action";
            string queryUrl = "https://cashier.etonepay.com/NetPay/MerOrderQuery.action";

            string version = "1.0.0";                                                 //版本号
            string transCode = "8888";                                                //交易代码
            string merchantId = this.suppAccount;                                     //商户编号
            string merOrderNum = orderid;                                             //商户订单号
            string bussId = "883090";                                                 //this.GetBussCode(bankcode);
            string tranAmt = (orderAmt * 100).ToString("f0");                         //交易金额
            string sysTraceNum = orderid;                                             //商户请求流水号
            string tranDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");            //交易时间
            string currencyType = "156";                                              //货币代码
            string merURL = this.returnurl;                                           //商户返回页面
            string backURL = this.notifyUrl;                                          //回调商户地址
            string orderInfo = "";                                                    //订单信息
            string userId = "";                                                       //用户 ID
            string userIp = "";                                                       //订单用户 IP
            string bankId = this.GetBankCode(bankcode);                               //支付方式代码
            string stlmId = "";                                                       //结算规则代码
            string entryType = "1";                                                    //入口类型
            string attach = "";                                                       //附加数据
            string reserver1 = "";
            string reserver2 = "";
            string reserver3 = "";
            string reserver4 = "";
            string datakey = this.suppKey;
            String txnString = version + "|" + transCode + "|" + merchantId + "|" + merOrderNum + "|" + bussId + "|" + tranAmt + "|" + sysTraceNum + "|" + tranDateTime + "|" + currencyType + "|" + merURL + "|" + backURL + "|" + orderInfo + "|" + userId;
            String signValue = FormsAuthentication.HashPasswordForStoringInConfigFile(txnString + datakey, "MD5").ToLower();  //数字签名
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + payUrl + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + version + "\" />" + "<input type=\"hidden\" name=\"transCode\" value=\"" + transCode + "\" />" + "<input type=\"hidden\" name=\"merchantId\" value=\"" + merchantId + "\" />" + "<input type=\"hidden\" name=\"merOrderNum\" value=\"" + merOrderNum + "\" />" + "<input type=\"hidden\" name=\"bussId\" value=\"" + bussId + "\" />" + "<input type=\"hidden\" name=\"tranAmt\" value=\"" + tranAmt + "\" />" + "<input type=\"hidden\" name=\"sysTraceNum\" value=\"" + sysTraceNum + "\" />" + "<input type=\"hidden\" name=\"tranDateTime\" value=\"" + tranDateTime + "\" />" + "<input type=\"hidden\" name=\"currencyType\" value=\"" + currencyType + "\" />" + "<input type=\"hidden\" name=\"merURL\" value=\"" + merURL + "\" />" + "<input type=\"hidden\" name=\"orderInfo\" value=\"" + orderInfo + "\" />" + "<input type=\"hidden\" name=\"bankId\" value=\"" + bankId + "\" />" + "<input type=\"hidden\" name=\"stlmId\" value=\"" + stlmId + "\" />" + "<input type=\"hidden\" name=\"userId\" value=\"" + userId + "\" />" + "<input type=\"hidden\" name=\"userIp\" value=\"" + userIp + "\" />" + "<input type=\"hidden\" name=\"backURL\" value=\"" + backURL + "\" />" + "<input type=\"hidden\" name=\"reserver1\" value=\"" + reserver1 + "\" />" + "<input type=\"hidden\" name=\"reserver2\" value=\"" + reserver2 + "\" />" + "<input type=\"hidden\" name=\"reserver3\" value=\"" + reserver3 + "\" />" + "<input type=\"hidden\" name=\"reserver4\" value=\"" + reserver4 + "\" />" + "<input type=\"hidden\" name=\"signValue\" value=\"" + signValue + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public void Notify()
        {
            string transCode = "8888";
            string merchantId = HttpContext.Current.Request["merchantId"];
            string respCode = HttpContext.Current.Request["respCode"].Trim();
            string sysTraceNum = HttpContext.Current.Request["sysTraceNum"];
            string merOrderNum = HttpContext.Current.Request["merOrderNum"].Trim();
            string orderId = HttpContext.Current.Request["orderId"].Trim();
            string bussId = HttpContext.Current.Request["bussId"];
            string tranAmt = HttpContext.Current.Request["tranAmt"];
            string orderAmt = HttpContext.Current.Request["orderAmt"];
            string bankFeeAmt = HttpContext.Current.Request["bankFeeAmt"];
            string integralAmt = HttpContext.Current.Request["integralAmt"];
            string vaAmt = HttpContext.Current.Request["vaAmt"];
            string bankAmt = HttpContext.Current.Request["bankAmt"];
            string bankId = HttpContext.Current.Request["bankId"];
            string integralSeq = HttpContext.Current.Request["integralSeq"];
            string vaSeq = HttpContext.Current.Request["vaSeq"];
            string bankSeq = HttpContext.Current.Request["bankSeq"];
            string tranDateTime = HttpContext.Current.Request["tranDateTime"];
            string payMentTime = HttpContext.Current.Request["payMentTime"];
            string settleDate = HttpContext.Current.Request["settleDate"];
            string currencyType = HttpContext.Current.Request["currencyType"];
            string orderInfo = HttpContext.Current.Request["orderInfo"];
            string userId = HttpContext.Current.Request["userId"];
            string userIp = HttpContext.Current.Request["userIp"];
            string reserver1 = HttpContext.Current.Request["reserver1"];
            string reserver2 = HttpContext.Current.Request["reserver2"];
            string reserver3 = HttpContext.Current.Request["reserver3"];
            string reserver4 = HttpContext.Current.Request["reserver4"];
            string signValue = HttpContext.Current.Request["signValue"];
            string datakey = this.suppKey;
            String txnString = transCode + "|" + merchantId + "|" + respCode + "|" + sysTraceNum + "|" + merOrderNum + "|" + orderId + "|" + bussId + "|" + tranAmt + "|" + orderAmt + "|" + bankFeeAmt + "|" + integralAmt + "|" + vaAmt + "|" + bankAmt + "|" + bankId + "|" + integralSeq + "|" + vaSeq + "|" + bankSeq + "|" + tranDateTime + "|" + payMentTime + "|" + settleDate + "|" + currencyType + "|" + orderInfo + "|" + userId;
            String signValue2 = FormsAuthentication.HashPasswordForStoringInConfigFile(txnString + datakey, "MD5").ToLower();  //数字签名
            try
            {
                if (!(signValue.ToLower() == signValue2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (respCode.Equals("0000"))
                {
                    opstate = "0";
                    status = 2;
                }
                if (new OrderBank().DoBankComplete(Bank.suppId, merOrderNum, orderId, status, opstate, string.Empty, Decimal.Parse(tranAmt) / 100m, new Decimal(0), true, false))
                    HttpContext.Current.Response.Write("success");
                else
                    HttpContext.Current.Response.Write("fail");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }


        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "888880510081101";
                    break;
                case "967":
                    str = "888880510031101";
                    break;
                case "964":
                    str = "888880510011101";
                    break;
                case "965":
                    str = "888880510101101";
                    break;
                case "963":
                    str = "888880500000000";
                    break;
                case "977":
                    str = "888880500000000";
                    break;
                case "981":
                    str = "888880510021101";
                    break;
                case "980":
                    str = "888880510141101";
                    break;
                case "985":
                    str = "888880500000000";
                    break;
                case "962":
                    str = "888880500000000";
                    break;
                case "982":
                    str = "888880510161101";
                    break;
                case "972":
                    str = "888880500000000";
                    break;
                case "976":
                    str = "888880500000000";
                    break;
                case "989":
                    str = "888880510201101";
                    break;
                case "986":
                    str = "888880500000000";
                    break;
                case "978":
                    str = "888880500000000";
                    break;
                case "975":
                    str = "888880510171101";
                    break;
                case "971":
                    str = "888880500000000";
                    break;
                case "1005":
                    str = "888880500000000";
                    break;
            }
            return str;
        }

    }
}

