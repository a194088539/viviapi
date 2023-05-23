using System;
using System.Collections.Generic;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;

namespace viviapi.ETAPI.Alipay
{
    public class AliPayMApi : ETAPIBase
    {
        private static int suppId = 101;
        private string _input_charset = "gbk";
        private string body = PaymentSetting.alipay_body;
        private string subject = PaymentSetting.alipay_subject;
        private string payment_type = "1";
        private string service = "create_direct_pay_by_user";

        internal string notify_url
        {
            get
            {
                return this.SiteDomain + "/notify/Alipay/MApi_Notify.aspx";
            }
        }

        internal string return_url
        {
            get
            {
                return this.SiteDomain + "/return/Alipay/MApi_Return.aspx";
            }
        }

        internal string show_url
        {
            get
            {
                return this.SiteDomain + "/success.htm";
            }
        }

        public AliPayMApi()
          : base(AliPayMApi.suppId)
        {
        }

        public string GetPayForm(string out_trade_no, Decimal amount, string bankid)
        {
            string suppAccount = this.suppAccount;
            string suppUserName = this.suppUserName;
            string str1 = amount.ToString("0");
            string str2 = "bankPay";
            string bankCode = AliPayMApi.GetBankCode(bankid);
            return Submit.BuildRequest(new SortedDictionary<string, string>()
      {
        {
          "partner",
          suppAccount
        },
        {
          "_input_charset",
          this._input_charset
        },
        {
          "service",
          this.service
        },
        {
          "payment_type",
          this.payment_type
        },
        {
          "notify_url",
          this.notify_url
        },
        {
          "return_url",
          this.return_url
        },
        {
          "seller_email",
          suppUserName
        },
        {
          "out_trade_no",
          out_trade_no
        },
        {
          "subject",
          this.subject
        },
        {
          "total_fee",
          str1
        },
        {
          "body",
          this.body
        },
        {
          "paymethod",
          str2
        },
        {
          "defaultbank",
          bankCode
        },
        {
          "show_url",
          this.show_url
        }
      }, "get", "确认");
        }

        public static string GetBankCode(string paymodeId)
        {
            string str = "";
            switch (paymodeId)
            {
                case "970":
                    str = "CMB";
                    break;
                case "967":
                    str = "ICBCB2C";
                    break;
                case "964":
                    str = "ABC";
                    break;
                case "965":
                    str = "CCB";
                    break;
                case "963":
                    str = "BOCB2C";
                    break;
                case "981":
                    str = "COMM";
                    break;
                case "980":
                    str = "CMBC";
                    break;
                case "974":
                    str = "SDB";
                    break;
                case "985":
                    str = "GDB";
                    break;
                case "962":
                    str = "CITIC";
                    break;
                case "972":
                    str = "CIB";
                    break;
                case "971":
                    str = "POSTGC";
                    break;
                case "989":
                    str = "BJBANK";
                    break;
                case "988":
                    str = "CBHB-NET-B2C";
                    break;
                case "990":
                    str = "BJRCB";
                    break;
                case "979":
                    str = "NJCB-NET-B2C";
                    break;
                case "986":
                    str = "CEB";
                    break;
                case "987":
                    str = "HKBEA-NET-B2C";
                    break;
                case "997":
                    str = "NBCB-NET-B2C";
                    break;
                case "978":
                    str = "SPABANK";
                    break;
                case "968":
                    str = "CZ-NET-B2C";
                    break;
                case "975":
                    str = "SHBANK";
                    break;
                case "976":
                    str = "SHRCB";
                    break;
                case "977":
                    str = "SPDB";
                    break;
                case "983":
                    str = "HZCBB2C";
                    break;
                case "998":
                    str = "NBBANK";
                    break;
            }
            return str;
        }

        public void Return()
        {
            SortedDictionary<string, string> requestGet = this.GetRequestGet();
            if (requestGet.Count <= 0)
                return;
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            if (new Notify().Verify(requestGet, HttpContext.Current.Request.QueryString["notify_id"], HttpContext.Current.Request.QueryString["sign"]))
            {
                string opstate = "-1";
                int status = 4;
                string msg = "支付失败";
                Decimal result = new Decimal(0);
                string orderId = HttpContext.Current.Request.QueryString["out_trade_no"];
                string supplierOrderId = HttpContext.Current.Request.QueryString["trade_no"];
                string str = HttpContext.Current.Request.QueryString["trade_status"];
                string s = HttpContext.Current.Request.QueryString["total_fee"];
                if (HttpContext.Current.Request.QueryString["trade_status"] == "TRADE_FINISHED" || HttpContext.Current.Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                {
                    if (Decimal.TryParse(s, out result))
                    {
                        msg = "成功";
                        opstate = "0";
                        status = 2;
                    }
                }
                else
                    msg = "trade_status=" + HttpContext.Current.Request.QueryString["trade_status"];
                new OrderBank().DoBankComplete(AliPayMApi.suppId, orderId, supplierOrderId, status, opstate, msg, result, new Decimal(0), true, false);
            }
        }

        public SortedDictionary<string, string> GetRequestGet()
        {
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            string[] allKeys = HttpContext.Current.Request.QueryString.AllKeys;
            for (int index = 0; index < allKeys.Length; ++index)
                sortedDictionary.Add(allKeys[index], HttpContext.Current.Request.QueryString[allKeys[index]]);
            return sortedDictionary;
        }

        public void Notify()
        {
            SortedDictionary<string, string> requestPost = this.GetRequestPost();
            if (requestPost.Count <= 0)
                return;
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            if (new Notify().Verify(requestPost, HttpContext.Current.Request.Form["notify_id"], HttpContext.Current.Request.Form["sign"]))
            {
                string opstate = "-1";
                int status = 4;
                string msg = "支付失败";
                Decimal result = new Decimal(0);
                string orderId = HttpContext.Current.Request.Form["out_trade_no"];
                string supplierOrderId = HttpContext.Current.Request.Form["trade_no"];
                string str = HttpContext.Current.Request.Form["trade_status"];
                string s = HttpContext.Current.Request.Form["total_fee"];
                if (HttpContext.Current.Request.Form["trade_status"] == "TRADE_FINISHED" || HttpContext.Current.Request.Form["trade_status"] == "TRADE_SUCCESS")
                {
                    if (Decimal.TryParse(s, out result))
                    {
                        msg = "成功";
                        opstate = "0";
                        status = 2;
                    }
                }
                else
                    msg = "trade_status=" + HttpContext.Current.Request.Form["trade_status"];
                new OrderBank().DoBankComplete(AliPayMApi.suppId, orderId, supplierOrderId, status, opstate, msg, result, new Decimal(0), false, true);
                HttpContext.Current.Response.Write("success");
                HttpContext.Current.Response.End();
            }
        }

        public SortedDictionary<string, string> GetRequestPost()
        {
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
            for (int index = 0; index < allKeys.Length; ++index)
                sortedDictionary.Add(allKeys[index], HttpContext.Current.Request.Form[allKeys[index]]);
            return sortedDictionary;
        }
    }
}
