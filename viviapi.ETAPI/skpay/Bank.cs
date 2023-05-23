using System.Web;
using System.Web.Security;

namespace viviapi.ETAPI.skpay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10042;

        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/skpay/bank_Notify.aspx");
            }
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/return/skpay/Bank_Return.aspx");
            }
        }

        public Bank()
            : base(suppId)
        {
        }

        public string PayBank(string orderid, decimal orderAmt, string bankcode)
        {
            string str = "http://uat-api.d3cq.com/api/order/pay";

            string sid = "S1005";
            string amount = "100.00"; // orderAmt.ToString("f2"); 
            string outTradeNo = "201805245879256982215200";
            string orderType = "2";
            string notifyUrl = "http://localhost/notify";
            string suppKey = "c863e0bfda7e73f6cda2a1aba4753b64";
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile("amount=" + amount + "&notifyUrl=" + notifyUrl + "&orderType=" + orderType + "&outTradeNo=" + outTradeNo + "&sid=" + sid + "@@" + suppKey, "MD5").ToLower();  //数字签名
            string url = str + "?sid=" + sid + "&amount=" + amount + "&outTradeNo=" + outTradeNo + "&orderType=" + orderType + "&notifyUrl=" + notifyUrl + "&sign=" + sign;
            HttpContext.Current.Response.Redirect(url);
            return url;
        }
        /*
                public void ReturnBank()
                {
                    string orderId = HttpContext.Current.Request["sdcustomno"].ToString().Trim();
                    string str2 = HttpContext.Current.Request["state"].ToString().Trim();
                    string supplierOrderId = HttpContext.Current.Request["sd51no"].ToString().Trim();
                    string s = HttpContext.Current.Request["orderMoney"].ToString().Trim();
                    string str5 = HttpContext.Current.Request["sign"].ToString().Trim();
                    MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                    string suppKey = base.suppKey;
                    string str7 = "sdcustomno=" + orderId + "&state=" + str2 + "&sd51no=" + supplierOrderId + "&orderMoney=" + s + "&key=" + suppKey;
                    string str8 = BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(str7))).Replace("-", "");
                    if (str5.ToUpper() == str8.ToUpper())
                    {
                        OrderBank orderBank = new OrderBank();
                        int status = 4;
                        string opstate = "1";
                        if (str2 == "1")
                        {
                            status = 2;
                            opstate = "0";
                        }
                        new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), false, true);
                        HttpContext.Current.Response.Write("success");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("failure");
                    }
                }
         */

        /*
                public void Notify()
                {
                    string orderId = HttpContext.Current.Request["sdcustomno"].ToString().Trim();
                    string str2 = HttpContext.Current.Request["state"].ToString().Trim();
                    string supplierOrderId = HttpContext.Current.Request["sd51no"].ToString().Trim();
                    string s = HttpContext.Current.Request["orderMoney"].ToString().Trim();
                    string str5 = HttpContext.Current.Request["sign"].ToString().Trim();
                    MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                    string suppKey = base.suppKey;
                    string str7 = "sdcustomno=" + orderId + "&state=" + str2 + "&sd51no=" + supplierOrderId + "&orderMoney=" + s + "&key=" + suppKey;
                    string str8 = BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(str7))).Replace("-", "");
                    if (str5.ToUpper() == str8.ToUpper())
                    {
                        OrderBank orderBank = new OrderBank();
                        int status = 4;
                        string opstate = "1";
                        if (str2 == "1")
                        {
                            status = 2;
                            opstate = "0";
                        }
                        new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, string.Empty, Decimal.Parse(s), new Decimal(0), true, false);
                        HttpContext.Current.Response.Write("success");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("failure");
                    }
                }
         */

    }
}


