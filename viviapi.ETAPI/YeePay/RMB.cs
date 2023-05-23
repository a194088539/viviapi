using com.yeepay;
using System;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;

namespace viviapi.ETAPI.YeePay
{
    public class RMB : ETAPIBase
    {
        private static int suppId = 10025;

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/YeePay_RMB_Notify.aspx";
            }
        }

        public RMB()
          : base(RMB.suppId)
        {
        }

        public string GetPayUrl(string orderid, Decimal orderAmt, string bankcode)
        {
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            Buy.NodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/node";
            if (!string.IsNullOrEmpty(this.postBankUrl))
                Buy.NodeAuthorizationURL = this.postBankUrl;
            string p2_Order = orderid;
            string p3_Amt = Decimal.Round(orderAmt, 2).ToString();
            string p4_Cur = "CNY";
            string yeepayPid = PaymentSetting.yeepay_pid;
            string yeepayPcat = PaymentSetting.yeepay_pcat;
            string yeepayPdesc = PaymentSetting.yeepay_pdesc;
            string notifyUrl = this.notify_url;
            string p9_SAF = "1";
            string pa_MP = "";
            string bankCode = Bank.GetBankCode(bankcode);
            string pr_NeedRespone = "1";
            return Buy.CreateBuyUrl(suppAccount, suppKey, p2_Order, p3_Amt, p4_Cur, yeepayPid, yeepayPcat, yeepayPdesc, notifyUrl, p9_SAF, pa_MP, bankCode, pr_NeedRespone) + "&noLoadingPage=1";
        }

        public string GetPayForm(string orderid, Decimal orderAmt, string bankcode)
        {
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            Buy.NodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/node";
            if (!string.IsNullOrEmpty(this.postBankUrl))
                Buy.NodeAuthorizationURL = this.postBankUrl;
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                Buy.NodeAuthorizationURL = this._suppInfo.jumpUrl + "/switch/yeepay.aspx";
            string p2_Order = orderid;
            string p3_Amt = Decimal.Round(orderAmt, 2).ToString();
            string p4_Cur = "CNY";
            string yeepayPid = PaymentSetting.yeepay_pid;
            string yeepayPcat = PaymentSetting.yeepay_pcat;
            string yeepayPdesc = PaymentSetting.yeepay_pdesc;
            string notifyUrl = this.notify_url;
            string p9_SAF = "1";
            string pa_MP = "";
            string bankCode = Bank.GetBankCode(bankcode);
            string pr_NeedRespone = "1";
            string buyForm = Buy.CreateBuyForm(suppAccount, suppKey, p2_Order, p3_Amt, p4_Cur, yeepayPid, yeepayPcat, yeepayPdesc, notifyUrl, p9_SAF, pa_MP, bankCode, pr_NeedRespone, "payform");
            buyForm.Insert(buyForm.IndexOf("method='POST'") + "method='POST'".Length, " id=\"payform\" target='_blank'");
            return buyForm + "<script type=\"text/javascript\" language=\"javascript\">function go(){ var _form = document.forms['payform']; _form.submit();};setTimeout(function(){go()},100);</script>";
        }

        public void Return()
        {
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            string opstate = "-1";
            int status = 4;
            string str = string.Empty;
            BuyCallbackResult buyCallbackResult = Buy.VerifyCallback(suppAccount, suppKey, FormatQueryString.GetQueryString("r0_Cmd"), FormatQueryString.GetQueryString("r1_Code"), FormatQueryString.GetQueryString("r2_TrxId"), FormatQueryString.GetQueryString("r3_Amt"), FormatQueryString.GetQueryString("r4_Cur"), FormatQueryString.GetQueryString("r5_Pid"), FormatQueryString.GetQueryString("r6_Order"), FormatQueryString.GetQueryString("r7_Uid"), FormatQueryString.GetQueryString("r8_MP"), FormatQueryString.GetQueryString("r9_BType"), FormatQueryString.GetQueryString("rp_PayDate"), FormatQueryString.GetQueryString("hmac"));
            if (!string.IsNullOrEmpty(buyCallbackResult.ErrMsg))
                return;
            string msg = "支付失败";
            if (buyCallbackResult.R1_Code == "1")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            Decimal result = new Decimal(0);
            Decimal.TryParse(buyCallbackResult.R3_Amt, out result);
            OrderBank orderBank = new OrderBank();
            if (buyCallbackResult.R9_BType == "1")
                orderBank.DoBankComplete(RMB.suppId, buyCallbackResult.R6_Order, buyCallbackResult.R2_TrxId, status, opstate, msg, result, new Decimal(0), false, true);
            else if (buyCallbackResult.R9_BType == "2" || buyCallbackResult.R9_BType == "3")
            {
                orderBank.DoBankComplete(RMB.suppId, buyCallbackResult.R6_Order, buyCallbackResult.R2_TrxId, status, opstate, msg, result, new Decimal(0), true, false);
                HttpContext.Current.Response.Write("SUCCESS");
            }
        }
    }
}
