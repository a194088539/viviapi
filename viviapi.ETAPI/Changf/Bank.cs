namespace viviapi.ETAPI.Changf
{
    using System;
    using System.Web;
    using viviapi.BLL;
    using viviapi.ETAPI;
    using viviLib.ExceptionHandling;
    using viviLib.Logging;

    public class Bank : ETAPIBase
    {
        private static int suppId = 10014;

        public Bank()
            : base(suppId)
        {
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/return/changf/Bank_Return.aspx");
            }
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "1004":
                    return "2001";

                case "992":
                    return "2003";

                case "1007":
                    return "2005";

                case "1006":
                    return "2007";

                case "1008":
                    return "2008";

                case "1009":
                    return "2009";

                case "2001":
                    return "2010";

                case "2002":
                    return "2011";

                case "2003":
                    return "2012";

                case "970":
                    return "1001";

                case "967":
                    return "1002";

                case "965":
                    return "1003";

                case "977":
                    return "1004";

                case "964":
                    return "1005";

                case "980":
                    return "1006";

                case "974":
                    return "1008";

                case "972":
                    return "1009";

                case "978":
                    return "1010";

                case "981":
                    return "1020";

                case "962":
                    return "1021";

                case "986":
                    return "1022";

                case "975":
                    return "1024";

                case "982":
                    return "1025";

                case "985":
                    return "1027";

                case "997":
                    return "1028";

                case "989":
                    return "1032";

                case "963":
                    return "1052";
            }
            return str;
        }

        public string PayBank(string orderid, decimal orderAmt, string bankcode)
        {
            string postBankUrl = base.postBankUrl;
            if (string.IsNullOrEmpty(postBankUrl))
            {
                postBankUrl = "http://pay.changfpay.com/pay.aspx";
            }
            string suppAccount = base.suppAccount;
            string str3 = orderid;
            string str4 = orderAmt.ToString();
            string bankCode = this.GetBankCode(bankcode);
            string suppKey = base.suppKey;
            string returnurl = this.returnurl;
            string str8 = "";
            string str9 = "";
            string str10 = MD5S.MD5(string.Format("userid={0}&orderid={1}&bankid={2}&keyvalue={3}", new object[] { suppAccount, str3, bankCode, suppKey }));
            string str11 = MD5S.MD5(string.Format("money={4}&userid={0}&orderid={1}&bankid={2}&keyvalue={3}", new object[] { suppAccount, str3, bankCode, suppKey, str4 }));
            string str12 = string.Format("{0}?userid={1}&orderid={2}&money={3}&url={4}&bankid={5}&sign={6}&ext={7}&sign2={8}", new object[] { str9, suppAccount, str3, str4, returnurl, bankCode, str10, str8, str11 });
            string url = postBankUrl + str12;
            HttpContext.Current.Response.Redirect(url);
            return url;
        }

        public void ReturnBank()
        {
            HttpContext.Current.Response.Charset = "utf-8";
            string keyvalue = base.suppKey;//;用户中心获取
            string returnode = HttpContext.Current.Request.QueryString["returncode"].ToString();
            string userid = HttpContext.Current.Request.QueryString["userid"].ToString(); //order.UserId.ToString();
            string orderid = HttpContext.Current.Request.QueryString["orderid"].ToString();//order.UserOrderNo;
            string money = HttpContext.Current.Request.QueryString["money"].ToString();//order.OrderMoney.ToString();
            string sign = HttpContext.Current.Request.QueryString["sign"].ToString();
            string sign2 = HttpContext.Current.Request.QueryString["sign2"].ToString();
            string ext = HttpContext.Current.Request.QueryString["ext"].ToString();//order.Ext;

            LogHelper.Write("keyvalue=" + keyvalue);
            LogHelper.Write("returnode=" + returnode);
            LogHelper.Write("userid=" + userid);
            LogHelper.Write("orderid=" + orderid);
            LogHelper.Write("money=" + money);
            LogHelper.Write("sign=" + sign);
            LogHelper.Write("sign2=" + sign2);

            string localsign = string.Format("returncode={0}&userid={1}&orderid={2}&keyvalue={3}"
               , returnode
               , userid
               , orderid
               , keyvalue
               );

            string localsign2 = string.Format("money={4}&returncode={0}&userid={1}&orderid={2}&keyvalue={3}"
               , returnode
               , userid
               , orderid
               , keyvalue
               , money
               );

            localsign = MD5S.MD5(localsign);
            localsign2 = MD5S.MD5(localsign2);

            LogHelper.Write("localsign=" + localsign);
            LogHelper.Write("localsign2=" + localsign2);

            try
            {
                if (sign != localsign || sign2 != localsign2)
                    return;
                OrderBank bank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (returnode.Equals("1"))
                //if (returnode == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, orderid, orderid, status, opstate, string.Empty, Decimal.Parse(money), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("ok");
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }
    }
}

