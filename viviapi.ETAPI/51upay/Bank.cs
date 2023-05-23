using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;

namespace viviapi.ETAPI._51upay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10034;

        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/51upay/bank_Notify.aspx");
            }
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/return/51upay/Bank_Return.aspx");
            }
        }

        public Bank()
            : base(suppId)
        {
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "1004":
                    return "32";

                case "1007":
                    return "41";

                case "2005":
                    return "13";

                case "1008":
                    return "36";

                case "1009":
                    return "45";

                case "992":
                    return "42";

                case "1006":
                    return "44";

                case "2007":
                    return "12";
            }
            return str;
        }

        public string PayBank(string orderid, decimal orderAmt, string bankcode)
        {
            string str = "http://api.51upay.com/PayMegerHandler.ashx";
            string suppAccount = base.suppAccount;
            string str3 = orderid;
            string str4 = (orderAmt * 100M).ToString("f0");
            string bankCode = this.GetBankCode(bankcode);
            string notifyUrl = this.notifyUrl;
            string returnurl = this.returnurl;
            string str8 = "tongy";
            string str9 = "tongy123";
            string str10 = "1";
            string suppKey = base.suppKey;
            string s = "customerid=" + suppAccount + "&sdcustomno=" + str3 + "&orderAmount=" + str4 + "&cardno=" + bankCode + "&noticeurl=" + notifyUrl + "&backurl=" + returnurl + suppKey;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            string str13 = BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace("-", "");
            string url = str + "?customerid=" + suppAccount + "&sdcustomno=" + str3 + "&orderAmount=" + str4 + "&cardno=" + bankCode + "&noticeurl=" + notifyUrl + "&backurl=" + returnurl + "&sign=" + str13 + "&mark=" + str8 + "&remarks=" + str9 + "&zftype=" + str10;
            HttpContext.Current.Response.Redirect(url);
            return url;
        }

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

    }
}

