using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.suiszf
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10033;

        public Bank()
            : base(suppId)
        {
        }

        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/suiszf/bank_Notify.aspx");
            }
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/return/suiszf/Bank_Return.aspx");
            }
        }

        public static string random()
        {
            char[] constant = {'0','1','2','3','4','5','6','7','8','9',
                               'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            StringBuilder sb = new StringBuilder(32);
            Random rd = new Random();
            for (int i = 0; i < 32; i++)
            {
                sb.Append(constant[rd.Next(62)]);
            }
            return sb.ToString();
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "992":
                    return "pay.alipay.native";

                case "1004":
                    return "pay.weixin.native";

                case "1008":
                    return "pay.qq.native";

                case "1006":
                    return "pay.alipay.wap";

                case "1007":
                    return "pay.weixin.wap";

                case "1009":
                    return "pay.qq.wap";

                case "2003":
                    return "pay.union.native";

                case "1005":
                    return "unified.trade.micropay";
            }
            return str;
        }

        public string PayBank(string orderid, decimal orderAmt, string bankcode)
        {
            string str1 = this.postBankUrl;
            if (string.IsNullOrEmpty(str1))
                str1 = "http://api.suiszf.com/pay/gateway";
            string service = this.GetBankCode(bankcode);
            string mch_id = base.suppAccount;
            string mch_no = orderid;
            string total_fee = (orderAmt * 100M).ToString("f0");
            string notify_url = this.notifyUrl;
            string format = "HTML";
            string success_url = this.returnurl;
            string attach = "maryfu";
            string key = base.suppKey;

            Hashtable param = new Hashtable
            {
                {"service",service},
                {"mch_id",mch_id},
                {"mch_no",mch_no},
                {"total_fee",total_fee},
                {"notify_url",notify_url },
                {"success_url",success_url},
                {"attach",attach},
                {"format",format },
                {"nonce_str",random()}
            };
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(param.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)param[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            string p = sb.ToString();
            sb.Append("key=" + key);

            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            string sign = BitConverter.ToString(MD5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))).Replace("-", "");


            p = p + "&sign=" + sign;
            string submiturl = str1 + "?" + p;


            HttpContext.Current.Response.Redirect(submiturl);
            return submiturl;
        }

        public void BankReturn()
        {
            string key = base.suppKey;
            HttpContext.Current.Response.Charset = "utf-8";
            string mch_id = HttpContext.Current.Request.QueryString["mch_id"];
            string mch_no = HttpContext.Current.Request.QueryString["mch_no"];
            string sys_no = HttpContext.Current.Request.QueryString["sys_no"];
            string total_fee = HttpContext.Current.Request.QueryString["total_fee"];
            string pay_state = HttpContext.Current.Request.QueryString["pay_state"];
            string attach = HttpContext.Current.Request.QueryString["attach"];
            string nonce_str = HttpContext.Current.Request.QueryString["nonce_str"];
            string sign = HttpContext.Current.Request.QueryString["sign"];
            string time_end = HttpContext.Current.Request.QueryString["time_end"];

            Hashtable param = new Hashtable
            {
                {"mch_id",mch_id},
                {"mch_no",mch_no},
                {"sys_no",sys_no},
                {"total_fee",total_fee },
                {"pay_state",pay_state},
                {"attach",attach},
                {"nonce_str",nonce_str },
                {"time_end",time_end}
            };

            //LogHelper.Write("mch_id=" + mch_id);
            //LogHelper.Write("mch_no=" + mch_no);
            //LogHelper.Write("sys_no=" + sys_no);
            //LogHelper.Write("total_fee=" + total_fee);
            //LogHelper.Write("pay_state=" + pay_state);
            //LogHelper.Write("attach=" + attach);
            //LogHelper.Write("nonce_str=" + nonce_str);
            //LogHelper.Write("time_end=" + time_end);

            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(param.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)param[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            sb.Append("key=" + key);

            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            string localsign = BitConverter.ToString(MD5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))).Replace("-", "");


            //LogHelper.Write("localsign=" + localsign);
            //LogHelper.Write("sign=" + sign);

            try
            {
                if (localsign != sign)
                    return;
                OrderBank bank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (pay_state == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, mch_no, sys_no, status, opstate, string.Empty, Decimal.Parse(total_fee) / 100m, new Decimal(0), false, true);
                HttpContext.Current.Response.Write("ok");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public void Notify()
        {
            string key = base.suppKey;
            HttpContext.Current.Response.Charset = "utf-8";
            string mch_id = HttpContext.Current.Request.QueryString["mch_id"];
            string mch_no = HttpContext.Current.Request.QueryString["mch_no"];
            string sys_no = HttpContext.Current.Request.QueryString["sys_no"];
            string total_fee = HttpContext.Current.Request.QueryString["total_fee"];
            string pay_state = HttpContext.Current.Request.QueryString["pay_state"];
            string attach = HttpContext.Current.Request.QueryString["attach"];
            string nonce_str = HttpContext.Current.Request.QueryString["nonce_str"];
            string sign = HttpContext.Current.Request.QueryString["sign"];
            string time_end = HttpContext.Current.Request.QueryString["time_end"];

            Hashtable param = new Hashtable
            {
                {"mch_id",mch_id},
                {"mch_no",mch_no},
                {"sys_no",sys_no},
                {"total_fee",total_fee },
                {"pay_state",pay_state},
                {"attach",attach},
                {"nonce_str",nonce_str },
                {"time_end",time_end}
            };

            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(param.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)param[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }
            sb.Append("key=" + key);

            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            string localsign = BitConverter.ToString(MD5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))).Replace("-", "");
            try
            {
                if (localsign != sign)
                    return;
                OrderBank bank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (pay_state == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, mch_no, sys_no, status, opstate, string.Empty, Decimal.Parse(total_fee) / 100m, new Decimal(0), true, false);
                HttpContext.Current.Response.Write("ok");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}

