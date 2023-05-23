using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.BLL;

namespace viviapi.ETAPI.Heepay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 991;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/heepay/bank_return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/heepay/bank_notify.aspx";
            }
        }

        private string result { get; set; }

        private string pay_message { get; set; }

        private string agent_id { get; set; }

        private string jnet_bill_no { get; set; }

        private string agent_bill_id { get; set; }

        private string pay_type { get; set; }

        private string pay_amt { get; set; }

        private string remark { get; set; }

        private string returnSign { get; set; }

        private string sign { get; set; }

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = this.postBankUrl;
            if (string.IsNullOrEmpty(str1))
                str1 = "https://pay.heepay.com/Payment/Index.aspx";
            int num1 = 1;
            string str2 = "20";
            string bankCode = this.GetBankCode(bankcode);
            string suppAccount = this.suppAccount;
            string str3 = orderid;
            string str4 = orderAmt.ToString("f2");
            string notifyUrl = this.notifyUrl;
            string returnurl = this.returnurl;
            string ip = this.GetIP();
            string str5 = string.Format("{0:yyyyMMddHHmmss}", (object)DateTime.Now);
            string str6 = "goods_name";
            string str7 = "1";
            string str8 = "";
            int num2 = 0;
            string str9 = "";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("version=" + (object)num1).Append("&agent_id=" + suppAccount).Append("&agent_bill_id=" + str3).Append("&agent_bill_time=" + str5).Append("&pay_type=" + str2).Append("&pay_amt=" + str4).Append("&notify_url=" + notifyUrl).Append("&return_url=" + returnurl).Append("&user_ip=" + ip);
            if (num2 == 1)
                stringBuilder.Append("&is_test=" + (object)num2);
            stringBuilder.Append("&key=" + this.suppKey);
            string str10 = this.MD5Hash(((object)stringBuilder).ToString()).ToLower();
            return string.Concat(new object[4]
            {
        (object) (string.Concat(new object[4]
        {
          (object) ("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str1 + "\">"),
          (object) "<input type=\"hidden\" name=\"version\" value=\"",
          (object) num1,
          (object) "\" />"
        }) + "<input type=\"hidden\" name=\"agent_id\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"agent_bill_id\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"agent_bill_time\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"pay_type\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"pay_code\" value=\"" + bankCode + "\" />" + "<input type=\"hidden\" name=\"pay_amt\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"notify_url\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"return_url\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"user_ip\" value=\"" + ip + "\" />" + "<input type=\"hidden\" name=\"goods_name\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"goods_num\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"goods_note\" value=\"" + str9 + "\" />"),
        (object) "<input type=\"hidden\" name=\"is_test\" value=\"",
        (object) num2,
        (object) "\" />"
            }) + "<input type=\"hidden\" name=\"remark\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + str10 + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
        }

        public void ReturnBank()
        {
            this.result = HttpContext.Current.Request["result"];
            this.pay_message = HttpContext.Current.Request["pay_message"];
            this.agent_id = HttpContext.Current.Request["agent_id"];
            this.jnet_bill_no = HttpContext.Current.Request["jnet_bill_no"];
            this.agent_bill_id = HttpContext.Current.Request["agent_bill_id"];
            this.pay_type = HttpContext.Current.Request["pay_type"];
            this.pay_amt = HttpContext.Current.Request["pay_amt"];
            this.remark = HttpContext.Current.Request["remark"];
            this.returnSign = HttpContext.Current.Request["sign"];
            this.sign = this.GetSign();
            if (this.sign.Equals(this.returnSign))
            {
                string opstate = "-1";
                int status = 4;
                if (this.result == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, this.agent_bill_id, this.jnet_bill_no, status, opstate, this.pay_message, Decimal.Parse(this.pay_amt), new Decimal(0), false, true);
            }
            else
                HttpContext.Current.Response.Write("签名不正确！");
        }

        public void Notify()
        {
            this.result = HttpContext.Current.Request["result"];
            this.pay_message = HttpContext.Current.Request["pay_message"];
            this.agent_id = HttpContext.Current.Request["agent_id"];
            this.jnet_bill_no = HttpContext.Current.Request["jnet_bill_no"];
            this.agent_bill_id = HttpContext.Current.Request["agent_bill_id"];
            this.pay_type = HttpContext.Current.Request["pay_type"];
            this.pay_amt = HttpContext.Current.Request["pay_amt"];
            this.remark = HttpContext.Current.Request["remark"];
            this.returnSign = HttpContext.Current.Request["sign"];
            this.sign = this.GetSign();
            if (this.sign.Equals(this.returnSign))
            {
                string opstate = "-1";
                int status = 4;
                if (this.result == "1")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(Bank.suppId, this.agent_bill_id, this.jnet_bill_no, status, opstate, this.pay_message, Decimal.Parse(this.pay_amt), new Decimal(0), false, false);
                HttpContext.Current.Response.Write("ok");
            }
            else
                HttpContext.Current.Response.Write("error");
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "002";
                    break;
                case "967":
                    str = "001";
                    break;
                case "964":
                    str = "005";
                    break;
                case "965":
                    str = "003";
                    break;
                case "963":
                    str = "004";
                    break;
                case "977":
                    str = "007";
                    break;
                case "978":
                    str = "012";
                    break;
                case "981":
                    str = "006";
                    break;
                case "980":
                    str = "013";
                    break;
                case "974":
                    str = "012";
                    break;
                case "985":
                    str = "016";
                    break;
                case "962":
                    str = "015";
                    break;
                case "982":
                    str = "014";
                    break;
                case "972":
                    str = "011";
                    break;
                case "986":
                    str = "010";
                    break;
                case "971":
                    str = "020";
                    break;
            }
            return str;
        }

        private string GetSign()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("result=" + this.result).Append("&agent_id=" + this.agent_id).Append("&jnet_bill_no=" + this.jnet_bill_no).Append("&agent_bill_id=" + this.agent_bill_id).Append("&pay_type=" + this.pay_type).Append("&pay_amt=" + this.pay_amt).Append("&remark=" + this.remark).Append("&key=" + this.suppKey);
            return this.MD5Hash(((object)stringBuilder).ToString()).ToLower();
        }

        private string MD5Hash(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToLower();
        }

        private string GetIP()
        {
            try
            {
                foreach (IPAddress ipAddress in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (ipAddress.AddressFamily.Equals((object)AddressFamily.InterNetwork))
                        return ipAddress.ToString();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
