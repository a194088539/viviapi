using System;
using System.Collections.Generic;
using System.Web;
using viviapi.BLL;
using viviLib.Web;

namespace viviapi.ETAPI.Ebatong
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10030;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/ebatong/bank_return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/ebatong/bank_notify.aspx";
            }
        }

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode)
        {
            string str1 = "create_direct_pay_by_user";
            string suppAccount1 = this.suppAccount;
            string inputCharset = Config.Input_charset;
            string signType = Config.Sign_type;
            string notifyUrl = this.notifyUrl;
            string returnurl = this.returnurl;
            string str2 = "";
            string str3 = AskForTimestamp.askFor();
            string trueIp = ServerVariables.TrueIP;
            string str4 = orderid;
            string str5 = orderid;
            string str6 = "1";
            string str7 = "";
            string suppAccount2 = this.suppAccount;
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = Decimal.Round(orderAmt, 2).ToString();
            string str12 = "";
            string str13 = "";
            string str14 = "";
            string str15 = "bankPay";
            string bankCode1 = this.GetBankCode(bankCode);
            string str16 = "";
            string str17 = "";
            string str18 = CommonHelper.BuildParamString(CommonHelper.BubbleSort(new string[25]
            {
        "service=" + str1,
        "partner=" + suppAccount1,
        "input_charset=" + inputCharset,
        "sign_type=" + signType,
        "notify_url=" + notifyUrl,
        "return_url=" + returnurl,
        "error_notify_url=" + str2,
        "anti_phishing_key=" + str3,
        "exter_invoke_ip=" + trueIp,
        "out_trade_no=" + str4,
        "subject=" + str5,
        "payment_type=" + str6,
        "seller_email=" + str7,
        "seller_id=" + suppAccount2,
        "buyer_email=" + str8,
        "buyer_id=" + str9,
        "price=" + str10,
        "total_fee=" + str11,
        "quantity=" + str12,
        "body=" + str13,
        "show_url=" + str14,
        "pay_method=" + str15,
        "default_bank=" + bankCode1,
        "royalty_parameters=" + str16,
        "royalty_type=" + str17
            }));
            string str19 = CommonHelper.md5(inputCharset, str18 + Config.Key).ToLower();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", suppAccount1);
            sParaTemp.Add("input_charset", inputCharset);
            sParaTemp.Add("service", str1);
            sParaTemp.Add("payment_type", str6);
            sParaTemp.Add("notify_url", notifyUrl);
            sParaTemp.Add("return_url", returnurl);
            sParaTemp.Add("seller_email", str7);
            sParaTemp.Add("out_trade_no", str4);
            sParaTemp.Add("subject", str5);
            sParaTemp.Add("total_fee", str11);
            sParaTemp.Add("body", str13);
            sParaTemp.Add("show_url", str14);
            sParaTemp.Add("anti_phishing_key", str3);
            sParaTemp.Add("exter_invoke_ip", trueIp);
            sParaTemp.Add("error_notify_url", str2);
            sParaTemp.Add("seller_id", suppAccount2);
            sParaTemp.Add("buyer_email", str8);
            sParaTemp.Add("buyer_id", str9);
            sParaTemp.Add("price", str10);
            sParaTemp.Add("quantity", str12);
            sParaTemp.Add("pay_method", str15);
            sParaTemp.Add("default_bank", bankCode1);
            sParaTemp.Add("royalty_parameters", str16);
            sParaTemp.Add("royalty_type", str17);
            sParaTemp.Add("sign", str19);
            sParaTemp.Add("sign_type", signType);
            string GATEWAY_NEW = "https://www.ebatong.com/direct/gateway.htm";
            return CommonHelper.BuildRequest(sParaTemp, "post", "确认", GATEWAY_NEW);
        }

        public void ReturnBank()
        {
            string str1 = HttpContext.Current.Request.QueryString["sign"];
            string[] allKeys = HttpContext.Current.Request.QueryString.AllKeys;
            string[] strArray = CommonHelper.BubbleSort(allKeys);
            string str2 = "";
            for (int index = 0; index < allKeys.Length; ++index)
            {
                if (!(strArray[index] == "sign"))
                    str2 = str2 + strArray[index] + "=" + HttpContext.Current.Request.QueryString[strArray[index]] + "&";
            }
            int length = str2.Length;
            if (!CommonHelper.md5(Config.Input_charset, str2.Remove(length - 1, 1) + Config.Key).ToLower().Equals(str1))
                return;
            string opstate = "-1";
            int status = 4;
            string msg = string.Empty;
            string orderId = HttpContext.Current.Request.QueryString["out_trade_no"];
            string supplierOrderId = HttpContext.Current.Request.QueryString["trade_no"];
            string s = HttpContext.Current.Request.QueryString["total_fee"];
            if ("TRADE_FINISHED" == HttpContext.Current.Request.QueryString["trade_status"])
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), false, true);
        }

        public void Notify()
        {
            string str1 = HttpContext.Current.Request.QueryString["sign"];
            string s1 = HttpContext.Current.Request.QueryString["notify_id"];
            string[] allKeys = HttpContext.Current.Request.QueryString.AllKeys;
            string[] strArray = CommonHelper.BubbleSort(allKeys);
            string str2 = "";
            for (int index = 0; index < allKeys.Length; ++index)
            {
                if (!(strArray[index] == "sign"))
                    str2 = str2 + strArray[index] + "=" + HttpContext.Current.Request.QueryString[strArray[index]] + "&";
            }
            int length = str2.Length;
            string str3 = CommonHelper.md5(Config.Input_charset, str2.Remove(length - 1, 1) + Config.Key).ToLower();
            string opstate = "-1";
            int status = 4;
            string msg = string.Empty;
            if (!str3.Equals(str1))
                return;
            string orderId = HttpContext.Current.Request.QueryString["out_trade_no"];
            string supplierOrderId = HttpContext.Current.Request.QueryString["trade_no"];
            string s2 = HttpContext.Current.Request.QueryString["total_fee"];
            if ("TRADE_FINISHED" == HttpContext.Current.Request.QueryString["trade_status"])
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s2), new Decimal(0), true, false);
            HttpContext.Current.Response.Write(s1);
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "CMB_B2C";
                    break;
                case "967":
                    str = "ICBC_B2C";
                    break;
                case "964":
                    str = "ABC_B2C";
                    break;
                case "965":
                    str = "CCB_B2C";
                    break;
                case "963":
                    str = "BOCSH_B2C";
                    break;
                case "977":
                    str = "SPDB_B2C";
                    break;
                case "981":
                    str = "COMM_B2C";
                    break;
                case "980":
                    str = "CMBCD_B2C";
                    break;
                case "974":
                    str = "SDB_B2C";
                    break;
                case "985":
                    str = "GDB_B2C";
                    break;
                case "962":
                    str = "CNCB_B2C";
                    break;
                case "982":
                    str = "HXB_B2C";
                    break;
                case "972":
                    str = "CIB_B2C";
                    break;
                case "984":
                    str = "GZCB_B2C";
                    break;
                case "1015":
                    str = "GZCB_B2C";
                    break;
                case "976":
                    str = "SRCB_B2C";
                    break;
                case "989":
                    str = "BOB_B2C";
                    break;
                case "988":
                    str = "CBHB_B2C";
                    break;
                case "990":
                    str = "BJRCB_B2C";
                    break;
                case "979":
                    str = "BON_B2C";
                    break;
                case "986":
                    str = "CEB_B2C";
                    break;
                case "987":
                    str = "BEA_B2C";
                    break;
                case "1025":
                    str = "NBCB_B2C";
                    break;
                case "983":
                    str = "HZCB_B2C";
                    break;
                case "978":
                    str = "PINGAN_B2C";
                    break;
                case "1028":
                    str = "HSB_B2C";
                    break;
                case "968":
                    str = "CZB_B2C";
                    break;
                case "975":
                    str = "BOS_B2C";
                    break;
                case "971":
                    str = "POSTGC_B2C";
                    break;
            }
            return str;
        }
    }
}
