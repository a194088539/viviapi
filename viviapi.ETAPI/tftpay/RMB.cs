using ProPalymentLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;

namespace viviapi.ETAPI.tftpay
{
    public class RMB : ETAPIBase
    {
        private static int suppId = 103;
        private ProPalymentClass vnbPalyment = (ProPalymentClass)null;

        internal string returnUrl
        {
            get
            {
                return this.SiteDomain + "/return/tftpay/Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/tftpay/notify.aspx";
            }
        }

        public RMB()
          : base(RMB.suppId)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.vnbPalyment = new ProPalymentClass(baseDirectory + "\\Licences\\" + PaymentSetting.tftpay_MerLicences, this.suppKey, baseDirectory + "\\Licences\\" + PaymentSetting.tftpay_TBLicences, PaymentSetting.tftpay_PostAdd);
        }

        public string GetPayUrl(string orderid, Decimal orderAmt, string bankCode)
        {
            string str = string.Empty;
            Dictionary<string, string> dictionary = this.vnbPalyment.SignMessageAndSendData2Gateway(new Dictionary<string, string>()
      {
        {
          "code",
          "ORD001"
        },
        {
          "merOrderId",
          orderid
        },
        {
          "returnUrl",
          this.returnUrl
        },
        {
          "notifyUrl",
          this.notifyUrl
        },
        {
          "chkMethod",
          "1"
        },
        {
          "merBusType",
          PaymentSetting.tftpay_MerBusType
        },
        {
          "payType",
          "0"
        },
        {
          "merOrderAmt",
          orderAmt.ToString()
        },
        {
          "custPhone",
          string.Empty
        },
        {
          "merOrderUrl",
          string.Empty
        },
        {
          "merOrderName",
          string.Empty
        },
        {
          "merShortName",
          string.Empty
        },
        {
          "merOrderDesc",
          string.Empty
        },
        {
          "Remark",
          string.Empty
        },
        {
          "Price",
          string.Empty
        },
        {
          "merOrderCount",
          string.Empty
        },
        {
          "saleAcct",
          string.Empty
        },
        {
          "saleAmt",
          string.Empty
        },
        {
          "payMethod",
          string.Empty
        },
        {
          "merRemak",
          string.Empty
        }
      });
            if (dictionary != null && dictionary.Count > 0)
            {
                using (Dictionary<string, string>.Enumerator enumerator = dictionary.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, string> current = enumerator.Current;
                        if (current.Key == "payUrl")
                            str = current.Value;
                        str += string.Format("&merNo={0}&orderNo={1}", (object)this.suppAccount, (object)orderid);
                    }
                }
            }
            return str;
        }

        public void ReturnBank()
        {
            HttpRequest request = HttpContext.Current.Request;
            Dictionary<string, string> dictionary = this.vnbPalyment.VNB_ParseXML(HttpUtility.UrlDecode(request["xml"].ToString(), Encoding.GetEncoding("UTF-8")), HttpUtility.UrlDecode(request["sign"].ToString(), Encoding.GetEncoding("UTF-8")));
            if (dictionary == null || dictionary.Count <= 0)
                return;
            string msg = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            string supplierOrderId = string.Empty;
            string orderId = string.Empty;
            string s = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            int status = 4;
            string opstate = "-1";
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                if (keyValuePair.Key == "code")
                    msg = keyValuePair.Value;
                if (keyValuePair.Key == "NotifyTyp")
                    str1 = keyValuePair.Value;
                if (keyValuePair.Key == "ActDat")
                    str2 = keyValuePair.Value;
                if (keyValuePair.Key == "payOrdNo")
                    supplierOrderId = keyValuePair.Value;
                if (keyValuePair.Key == "merOrderId")
                    orderId = keyValuePair.Value;
                if (keyValuePair.Key == "merOrderAmt")
                    s = keyValuePair.Value;
                if (keyValuePair.Key == "merOrderStatus")
                    str3 = keyValuePair.Value;
                if (keyValuePair.Key == "merNo")
                    str4 = keyValuePair.Value;
            }
            if (str3 == "00")
            {
                status = 2;
                opstate = "0";
            }
            if (!string.IsNullOrEmpty(orderId) && !string.IsNullOrEmpty(supplierOrderId) && !string.IsNullOrEmpty(s))
                new OrderBank().DoBankComplete(RMB.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), false, true);
        }

        public void Notify()
        {
            HttpRequest request = HttpContext.Current.Request;
            Dictionary<string, string> dictionary = this.vnbPalyment.VNB_ParseXML(HttpUtility.UrlDecode(request["xml"].ToString(), Encoding.GetEncoding("UTF-8")), HttpUtility.UrlDecode(request["sign"].ToString(), Encoding.GetEncoding("UTF-8")));
            if (dictionary == null || dictionary.Count <= 0)
                return;
            string msg = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            string supplierOrderId = string.Empty;
            string orderId = string.Empty;
            string s = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            int status = 4;
            string opstate = "-1";
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                if (keyValuePair.Key == "code")
                    msg = keyValuePair.Value;
                if (keyValuePair.Key == "NotifyTyp")
                    str1 = keyValuePair.Value;
                if (keyValuePair.Key == "ActDat")
                    str2 = keyValuePair.Value;
                if (keyValuePair.Key == "payOrdNo")
                    supplierOrderId = keyValuePair.Value;
                if (keyValuePair.Key == "merOrderId")
                    orderId = keyValuePair.Value;
                if (keyValuePair.Key == "merOrderAmt")
                    s = keyValuePair.Value;
                if (keyValuePair.Key == "merOrderStatus")
                    str3 = keyValuePair.Value;
                if (keyValuePair.Key == "merNo")
                    str4 = keyValuePair.Value;
            }
            if (str3 == "00")
            {
                status = 2;
                opstate = "0";
            }
            if (!string.IsNullOrEmpty(orderId) && !string.IsNullOrEmpty(supplierOrderId) && !string.IsNullOrEmpty(s))
                new OrderBank().DoBankComplete(RMB.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), true, false);
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "cmb";
                    break;
                case "967":
                    str2 = "icbc";
                    break;
                case "964":
                    str2 = "abc";
                    break;
                case "965":
                    str2 = "ccb";
                    break;
                case "963":
                    str2 = "boc";
                    break;
                case "977":
                    str2 = "spdb";
                    break;
                case "981":
                    str2 = "comm";
                    break;
                case "980":
                    str2 = "cmbc";
                    break;
                case "974":
                    str2 = "sdb";
                    break;
                case "985":
                    str2 = "gdb";
                    break;
                case "962":
                    str2 = "ecitic";
                    break;
                case "982":
                    str2 = "hxb";
                    break;
                case "972":
                    str2 = "cib";
                    break;
                case "989":
                    str2 = "bccb";
                    break;
                case "986":
                    str2 = "ceb";
                    break;
                case "987":
                    str2 = "bea";
                    break;
                case "1025":
                    str2 = "nb";
                    break;
                case "968":
                    str2 = "00086";
                    break;
                case "975":
                    str2 = "00084";
                    break;
                case "971":
                    str2 = "post";
                    break;
                case "1032":
                    str2 = "unionpay";
                    break;
                default:
                    str2 = "unionpay";
                    break;
            }
            return str2;
        }
    }
}
