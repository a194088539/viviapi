using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;

namespace viviapi.ETAPI
{
    public class AliPay : ETAPIBase
    {
        private static int suppId = 101;
        private string _input_charset = "gb2312";
        private string alipayNotifyURL = "https://mapi.alipay.com/cooperate/gateway.do?";
        private string body = PaymentSetting.alipay_body;
        private string subject = PaymentSetting.alipay_subject;
        private string sign_type = "MD5";
        private string payment_type = "1";
        private string service = "create_direct_pay_by_user";

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/Alipay_Notify.aspx";
            }
        }

        internal string return_url
        {
            get
            {
                return this.SiteDomain + "/return/Alipay_Return.aspx";
            }
        }

        internal string show_url
        {
            get
            {
                return this.SiteDomain + "/success.htm";
            }
        }

        public AliPay()
          : base(AliPay.suppId)
        {
        }

        public string GetPayUrl(string out_trade_no, double amount)
        {
            string suppAccount = this.suppAccount;
            string suppUserName = this.suppUserName;
            string suppKey = this.suppKey;
            string gateway = this.alipayNotifyURL;
            string total_fee = amount.ToString();
            return this.CreatUrl(gateway, this.service, suppAccount, this.sign_type, out_trade_no, this.subject, this.body, this.payment_type, total_fee, this.show_url, suppUserName, suppKey, this.return_url, this.notify_url);
        }

        public string CreatUrl(string gateway, string service, string partner, string sign_type, string out_trade_no, string subject, string body, string payment_type, string total_fee, string show_url, string seller_email, string key, string return_url, string notify_url)
        {
            string[] strArray = AliPay.BubbleSort(new string[11]
            {
        "service=" + service,
        "partner=" + partner,
        "subject=" + subject,
        "body=" + body,
        "out_trade_no=" + out_trade_no,
        "total_fee=" + total_fee,
        "show_url=" + show_url,
        "payment_type=" + payment_type,
        "seller_email=" + seller_email,
        "notify_url=" + notify_url,
        "return_url=" + return_url
            });
            StringBuilder stringBuilder1 = new StringBuilder();
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (index == strArray.Length - 1)
                    stringBuilder1.Append(strArray[index]);
                else
                    stringBuilder1.Append(strArray[index] + "&");
            }
            stringBuilder1.Append(key);
            string md5 = AliPay.GetMD5(((object)stringBuilder1).ToString(), this._input_charset);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append(gateway);
            for (int index = 0; index < strArray.Length; ++index)
                stringBuilder2.Append(strArray[index] + "&");
            stringBuilder2.Append("sign=" + md5 + "&sign_type=" + sign_type);
            return ((object)stringBuilder2).ToString();
        }

        public string GetPayForm(string out_trade_no, double amount, bool autosumit)
        {
            string str = this.alipayNotifyURL;
            string[] strArray1 = AliPay.BubbleSort(new string[11]
            {
        "service=" + this.service,
        "partner=" + this.suppAccount,
        "subject=" + this.subject,
        "body=" + this.body,
        "out_trade_no=" + out_trade_no,
        "total_fee=" + amount.ToString(),
        "show_url=" + this.show_url,
        "payment_type=" + this.payment_type,
        "seller_email=" + this.suppUserName,
        "notify_url=" + this.notify_url,
        "return_url=" + this.return_url
            });
            StringBuilder stringBuilder1 = new StringBuilder();
            for (int index = 0; index < strArray1.Length; ++index)
            {
                if (index == strArray1.Length - 1)
                    stringBuilder1.Append(strArray1[index]);
                else
                    stringBuilder1.Append(strArray1[index] + "&");
            }
            stringBuilder1.Append(this.suppKey);
            string md5 = AliPay.GetMD5(((object)stringBuilder1).ToString(), this._input_charset);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.AppendFormat("<form name=\"frm1\" id=\"frm1\" method=\"get\" action=\"{0}\">", (object)str);
            for (int index = 0; index < strArray1.Length; ++index)
            {
                string[] strArray2 = strArray1[index].Split('=');
                stringBuilder2.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", (object)strArray2[0], (object)strArray2[1]);
            }
            stringBuilder2.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", (object)"sign", (object)md5);
            stringBuilder2.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", (object)"sign_type", (object)this.sign_type);
            stringBuilder2.Append("</form>");
            if (autosumit)
                stringBuilder2.Append("<script>document.forms[0].submit();</script> ");
            return ((object)stringBuilder2).ToString();
        }

        public void Return()
        {
            string suppAccount = this.suppAccount;
            string suppUserName = this.suppUserName;
            string suppKey = this.suppKey;
            this.alipayNotifyURL = this.alipayNotifyURL + "service=notify_verify&partner=" + suppAccount + "&notify_id=" + HttpContext.Current.Request.QueryString["notify_id"];
            this.Get_Http(this.alipayNotifyURL, 120000);
            string[] strArray = AliPay.BubbleSort(HttpContext.Current.Request.QueryString.AllKeys);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (HttpContext.Current.Request.Form[strArray[index]] != "" && strArray[index] != "sign" && strArray[index] != "sign_type")
                {
                    if (index == strArray.Length - 1)
                        stringBuilder.Append(strArray[index] + "=" + HttpContext.Current.Request.QueryString[strArray[index]]);
                    else
                        stringBuilder.Append(strArray[index] + "=" + HttpContext.Current.Request.QueryString[strArray[index]] + "&");
                }
            }
            stringBuilder.Append(suppKey);
            string md5 = AliPay.GetMD5(((object)stringBuilder).ToString(), this._input_charset);
            string str1 = HttpContext.Current.Request.QueryString["sign"];
            string str2 = HttpContext.Current.Request.QueryString["trade_status"];
            string orderId = string.Empty;
            string supplierOrderId = string.Empty;
            Decimal result = new Decimal(0);
            if (HttpContext.Current.Request.QueryString["out_trade_no"] != null)
                orderId = HttpContext.Current.Request.QueryString["out_trade_no"].Trim();
            if (HttpContext.Current.Request.QueryString["trade_no"] != null && HttpContext.Current.Request.QueryString["trade_no"].Trim().Length > 1)
                supplierOrderId = HttpContext.Current.Request.QueryString["trade_no"].Trim();
            if (HttpContext.Current.Request.QueryString["total_fee"] != null && HttpContext.Current.Request.QueryString["total_fee"].Trim().Length > 0 && !Decimal.TryParse(HttpContext.Current.Request.QueryString["total_fee"].Trim(), out result))
                result = new Decimal(0);
            string opstate = "-1";
            int status = 4;
            string msg = string.Empty;
            if (md5 == str1)
            {
                switch (str2)
                {
                    case "TRADE_FINISHED":
                    case "TRADE_SUCCESS":
                        opstate = "0";
                        status = 2;
                        break;
                    default:
                        msg = str2;
                        break;
                }
                new OrderBank().DoBankComplete(AliPay.suppId, orderId, supplierOrderId, status, opstate, msg, result, new Decimal(0), true, true);
            }
            else
                HttpContext.Current.Response.Write("<script>alert('出现异常！请查看充值是否到帐。若未到帐，请与管理员联系。');</script>");
        }

        public void Notify()
        {
            string suppAccount = this.suppAccount;
            string suppUserName = this.suppUserName;
            string suppKey = this.suppKey;
            this.alipayNotifyURL = this.alipayNotifyURL + "service=notify_verify&partner=" + suppAccount + "&notify_id=" + HttpContext.Current.Request.Form["notify_id"];
            this.Get_Http(this.alipayNotifyURL, 120000);
            string[] strArray = AliPay.BubbleSort(HttpContext.Current.Request.Form.AllKeys);
            string str = "";
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (HttpContext.Current.Request.Form[strArray[index]] != "" && strArray[index] != "sign" && strArray[index] != "sign_type")
                {
                    if (index == strArray.Length - 1)
                        str = str + strArray[index] + "=" + HttpContext.Current.Request.Form[strArray[index]];
                    else
                        str = str + strArray[index] + "=" + HttpContext.Current.Request.Form[strArray[index]] + "&";
                }
            }
            if (!(AliPay.GetMD5(str + suppKey, this._input_charset) == HttpContext.Current.Request.Form["sign"]))
                return;
            string opstate = "-1";
            int status = 4;
            string orderId = string.Empty;
            string supplierOrderId = string.Empty;
            Decimal result = new Decimal(0);
            if (HttpContext.Current.Request.Form["out_trade_no"] != null && HttpContext.Current.Request.Form["out_trade_no"].Trim().Length > 1)
                orderId = HttpContext.Current.Request.Form["out_trade_no"].Trim();
            if (HttpContext.Current.Request.Form["trade_no"] != null && HttpContext.Current.Request.Form["trade_no"].Trim().Length > 1)
                supplierOrderId = HttpContext.Current.Request.Form["trade_no"].Trim();
            if (HttpContext.Current.Request.Form["total_fee"] != null && HttpContext.Current.Request.Form["total_fee"].Trim().Length > 0 && !Decimal.TryParse(HttpContext.Current.Request.Form["total_fee"].Trim(), out result))
                result = new Decimal(0);
            string msg;
            if (HttpContext.Current.Request.Form["trade_status"] == "TRADE_FINISHED" || HttpContext.Current.Request.Form["trade_status"] == "TRADE_SUCCESS")
            {
                msg = "失败成功";
                opstate = "0";
                status = 2;
            }
            else
                msg = "支付失败 状态号：" + HttpContext.Current.Request.Form["trade_status"];
            new OrderBank().DoBankComplete(AliPay.suppId, orderId, supplierOrderId, status, opstate, msg, result, new Decimal(0), true, false);
            HttpContext.Current.Response.Write("success");
        }

        public static string[] BubbleSort(string[] r)
        {
            for (int index1 = 0; index1 < r.Length; ++index1)
            {
                bool flag = false;
                for (int index2 = r.Length - 2; index2 >= index1; --index2)
                {
                    if (string.CompareOrdinal(r[index2 + 1], r[index2]) < 0)
                    {
                        string str = r[index2 + 1];
                        r[index2 + 1] = r[index2];
                        r[index2] = str;
                        flag = true;
                    }
                }
                if (!flag)
                    return r;
            }
            return r;
        }

        public static string GetMD5(string s, string _input_charset)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder stringBuilder = new StringBuilder(32);
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x").PadLeft(2, '0'));
            return ((object)stringBuilder).ToString();
        }

        public string Get_Http(string a_strUrl, int timeout)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(a_strUrl);
                httpWebRequest.Timeout = timeout;
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default);
                StringBuilder stringBuilder = new StringBuilder();
                while (-1 != streamReader.Peek())
                    stringBuilder.Append(streamReader.ReadLine());
                return ((object)stringBuilder).ToString();
            }
            catch (Exception ex)
            {
                return "错误：" + ex.Message;
            }
        }
    }
}
