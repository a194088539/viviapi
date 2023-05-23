namespace viviapi.ETAPI
{
    using System;
    using System.Web;
    using viviapi.BLL;
    using viviLib.Logging;
    using viviLib.Security;

    public class IPS : ETAPIBase
    {
        private static int suppId = 600;

        public IPS()
            : base(suppId)
        {
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    return "00042";

                case "967":
                    return "00004";

                case "964":
                    return "00017";

                case "965":
                    return "00012";

                case "963":
                    return "00083";

                case "977":
                    return "00032";

                case "981":
                    return "00005";

                case "980":
                    return "00013";

                case "974":
                    return "00023";

                case "985":
                    return "00052";

                case "962":
                    return "00092";

                case "982":
                    return "00041";

                case "972":
                    return "00016";

                case "984":
                    return "00011";

                case "976":
                    return "00030";

                case "989":
                    return "00050";

                case "990":
                    return "00056";

                case "979":
                    return "00055";

                case "986":
                    return "00057";

                case "983":
                    return "00081";

                case "978":
                    return "00087";

                case "968":
                    return "00086";

                case "975":
                    return "00084";

                case "971":
                    return "00051";
            }
            return str;
        }

        public string GetPayForm(string orderid, decimal orderAmt, string bankcode)
        {
            string postBankUrl = base.postBankUrl;
            string suppAccount = base.suppAccount;
            string suppKey = base.suppKey;
            string str4 = orderid;
            string str5 = decimal.Round(orderAmt, 2).ToString("0.00");
            string str6 = DateTime.Now.ToString("yyyyMMdd");
            string str7 = "01";
            string str8 = "RMB";
            string returnurl = this.returnurl;
            string str10 = this.returnurl;
            string str11 = this.returnurl;
            string str12 = string.Empty;
            string str13 = str5;
            string str14 = "5";
            string str15 = "17";
            string notifyUrl = this.notifyUrl;
            string str17 = "1";
            string str18 = "1";
            string bankCode = this.GetBankCode(bankcode);
            string format = "billno{0}currencytype{1}amount{2}date{3}orderencodetype{4}{5}";
            string str21 = Cryptography.MD5(string.Format(format, new object[] { str4, str8, str5, str6, str14, suppKey }));
            return (((((((((((((((((((("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + postBankUrl + "\">") + "<input type=\"hidden\" name=\"Mer_code\" value=\"" + suppAccount + "\" />") + "<input type=\"hidden\" name=\"Billno\" value=\"" + str4 + "\" />") + "<input type=\"hidden\" name=\"Amount\" value=\"" + str5 + "\" />") + "<input type=\"hidden\" name=\"Date\" value=\"" + str6 + "\" />") + "<input type=\"hidden\" name=\"Currency_Type\" value=\"" + str8 + "\" />") + "<input type=\"hidden\" name=\"Gateway_Type\" value=\"" + str7 + "\" />") + "<input type=\"hidden\" name=\"Merchanturl\" value=\"" + returnurl + "\" />") + "<input type=\"hidden\" name=\"FailUrl\" value=\"" + str10 + "\" />") + "<input type=\"hidden\" name=\"ErrorUrl\" value=\"" + str11 + "\" />") + "<input type=\"hidden\" name=\"Attach\" value=\"" + str12 + "\" />") + "<input type=\"hidden\" name=\"DispAmount\" value=\"" + str13 + "\" />") + "<input type=\"hidden\" name=\"OrderEncodeType\" value=\"" + str14 + "\" />") + "<input type=\"hidden\" name=\"RetEncodeType\" value=\"" + str15 + "\" />") + "<input type=\"hidden\" name=\"ServerUrl\" value=\"" + notifyUrl + "\" />") + "<input type=\"hidden\" name=\"SignMD5\" value=\"" + str21 + "\" />") + "<input type=\"hidden\" name=\"DoCredit\" value=\"" + str18 + "\" />") + "<input type=\"hidden\" name=\"Bankco\" value=\"" + bankCode + "\" />") + "<input type=\"hidden\" name=\"RetType\" value=\"" + str17 + "\" />") + "</form>");
        }

        public void Notify()
        {
            string orderId = HttpContext.Current.Request["billno"];
            string s = HttpContext.Current.Request["amount"];
            string str3 = HttpContext.Current.Request["Currency_type"];
            string str4 = HttpContext.Current.Request["date"];
            string str5 = HttpContext.Current.Request["succ"];
            string str6 = HttpContext.Current.Request["msg"];
            string str7 = HttpContext.Current.Request["attach"];
            string supplierOrderId = HttpContext.Current.Request["ipsbillno"];
            string str9 = HttpContext.Current.Request["retencodetype"];
            string str10 = HttpContext.Current.Request["signature"];
            string str11 = HttpContext.Current.Request["ipsbanktime"];
            string str12 = orderId + s + str4 + str5 + supplierOrderId + str3;
            bool flag = false;
            if (str9 == "17")
            {
                string suppKey = base.suppKey;
                string format = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                if (Cryptography.MD5(string.Format(format, new object[] { orderId, str3, s, str4, str5, supplierOrderId, str9, base.suppKey })) == str10)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                string opstate = "-1";
                int status = 4;
                if (str5 == "Y")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(suppId, orderId, supplierOrderId, status, opstate, string.Empty, decimal.Parse(s), 0M, true, false);
                HttpContext.Current.Response.Write("ipscheckok");
            }
            else
            {
                HttpContext.Current.Response.Write("fail");
            }
        }

        public string PayBank(string orderid, decimal orderAmt, string bankcode)
        {
            string postBankUrl = base.postBankUrl;
            if (!string.IsNullOrEmpty(base._suppInfo.jumpUrl))
            {
                postBankUrl = base._suppInfo.jumpUrl + "/switch/ipspay.aspx";
            }
            string suppAccount = base.suppAccount;
            string suppKey = base.suppKey;
            string str4 = orderid;
            string str5 = decimal.Round(orderAmt, 2).ToString("0.00");
            string str6 = DateTime.Now.ToString("yyyyMMdd");
            string str7 = "01";
            string str8 = "RMB";
            string returnurl = this.returnurl;
            string str10 = this.returnurl;
            string str11 = string.Empty;
            string str12 = string.Empty;
            string str13 = str5;
            string str14 = "5";
            string str15 = "17";
            string notifyUrl = this.notifyUrl;
            string str17 = "1";
            string str18 = "1";
            string bankCode = this.GetBankCode(bankcode);
            string format = "billno{0}currencytype{1}amount{2}date{3}orderencodetype{4}{5}";
            string str = ((((((((((((((((((("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + postBankUrl + "\">") + "<input type=\"hidden\" name=\"Mer_code\" value=\"" + suppAccount + "\" />") + "<input type=\"hidden\" name=\"Billno\" value=\"" + str4 + "\" />") + "<input type=\"hidden\" name=\"Amount\" value=\"" + str5 + "\" />") + "<input type=\"hidden\" name=\"Date\" value=\"" + str6 + "\" />") + "<input type=\"hidden\" name=\"Currency_Type\" value=\"" + str8 + "\" />") + "<input type=\"hidden\" name=\"Gateway_Type\" value=\"" + str7 + "\" />") + "<input type=\"hidden\" name=\"Merchanturl\" value=\"" + returnurl + "\" />") + "<input type=\"hidden\" name=\"FailUrl\" value=\"" + str10 + "\" />") + "<input type=\"hidden\" name=\"ErrorUrl\" value=\"" + str11 + "\" />") + "<input type=\"hidden\" name=\"Attach\" value=\"" + str12 + "\" />") + "<input type=\"hidden\" name=\"DispAmount\" value=\"" + str13 + "\" />") + "<input type=\"hidden\" name=\"OrderEncodeType\" value=\"" + str14 + "\" />") + "<input type=\"hidden\" name=\"RetEncodeType\" value=\"" + str15 + "\" />") + "<input type=\"hidden\" name=\"ServerUrl\" value=\"" + notifyUrl + "\" />") + "<input type=\"hidden\" name=\"SignMD5\" value=\"" + Cryptography.MD5(string.Format(format, new object[] { str4, str8, str5, str6, str14, suppKey })) + "\" />") + "<input type=\"hidden\" name=\"DoCredit\" value=\"" + str18 + "\" />") + "<input type=\"hidden\" name=\"Bankco\" value=\"" + bankCode + "\" />") + "<input type=\"hidden\" name=\"RetType\" value=\"" + str17 + "\" />") + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            LogHelper.Write(str);
            return str;
        }

        public void ReturnBank()
        {
            string orderId = HttpContext.Current.Request["billno"];
            string s = HttpContext.Current.Request["amount"];
            string str3 = HttpContext.Current.Request["Currency_type"];
            string str4 = HttpContext.Current.Request["date"];
            string str5 = HttpContext.Current.Request["succ"];
            string str6 = HttpContext.Current.Request["msg"];
            string str7 = HttpContext.Current.Request["attach"];
            string supplierOrderId = HttpContext.Current.Request["ipsbillno"];
            string str9 = HttpContext.Current.Request["retencodetype"];
            string str10 = HttpContext.Current.Request["signature"];
            string str11 = orderId + s + str4 + str5 + supplierOrderId + str3;
            bool flag = false;
            if (str9 == "17")
            {
                string format = "billno{0}currencytype{1}amount{2}date{3}succ{4}ipsbillno{5}retencodetype{6}{7}";
                if (Cryptography.MD5(string.Format(format, new object[] { orderId, str3, s, str4, str5, supplierOrderId, str9, base.suppKey })) == str10)
                {
                    flag = true;
                }
            }
            string msg = "支付失败" + str6;
            if (flag)
            {
                string opstate = "-1";
                int status = 4;
                if (str5 == "Y")
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(suppId, orderId, supplierOrderId, status, opstate, msg, decimal.Parse(s), 0M, false, true);
            }
            else
            {
                HttpContext.Current.Response.Write("签名不正确！");
            }
        }

        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/IPSBank_notify.aspx");
            }
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/return/IPSBank_Return.aspx");
            }
        }
    }
}

