using System;
using System.Collections.Generic;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;

namespace viviapi.ETAPI.AlipayWap
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 101;

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

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string PayBankApp(string orderid, Decimal orderAmt, string bankcode)
        {
            //if (string.IsNullOrEmpty(this.postBankUrl))
            //  ;
            string str1 = orderid;
            string alipayBody = PaymentSetting.alipay_body;
            string str2 = orderAmt.ToString();
            string returnUrl = this.return_url;
            string str3 = "";
            return Submit.BuildRequest(new SortedDictionary<string, string>()
      {
        {
          "partner",
          this.suppAccount
        },
        {
          "seller_id",
          this.suppUserName
        },
        {
          "_input_charset",
          Config.input_charset.ToLower()
        },
        {
          "service",
          Config.service
        },
        {
          "payment_type",
          Config.payment_type
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
          "out_trade_no",
          str1
        },
        {
          "subject",
          alipayBody
        },
        {
          "total_fee",
          str2
        },
        {
          "show_url",
          returnUrl
        },
        {
          "body",
          str3
        }
      }, "get", "确认");
        }

        public void Notify()
        {
            SortedDictionary<string, string> requestPost = this.GetRequestPost();
            if (requestPost.Count <= 0)
                return;
            bool flag = new Notify().Verify(requestPost, HttpContext.Current.Request.Form["notify_id"], HttpContext.Current.Request.Form["sign"]);
            string opstate = "-1";
            int status = 4;
            string msg = string.Empty;
            if (flag)
            {
                string str = HttpContext.Current.Request.Form["trade_status"];
                string orderId = string.Empty;
                string supplierOrderId = string.Empty;
                Decimal result = new Decimal(0);
                if (HttpContext.Current.Request.QueryString["out_trade_no"] != null)
                    orderId = HttpContext.Current.Request.QueryString["out_trade_no"].Trim();
                if (HttpContext.Current.Request.QueryString["trade_no"] != null && HttpContext.Current.Request.QueryString["trade_no"].Trim().Length > 1)
                    supplierOrderId = HttpContext.Current.Request.QueryString["trade_no"].Trim();
                if (HttpContext.Current.Request.QueryString["total_fee"] != null && HttpContext.Current.Request.QueryString["total_fee"].Trim().Length > 0 && !Decimal.TryParse(HttpContext.Current.Request.QueryString["total_fee"].Trim(), out result))
                    result = new Decimal(0);
                switch (str)
                {
                    case "TRADE_FINISHED":
                    case "TRADE_SUCCESS":
                        opstate = "0";
                        status = 2;
                        break;
                    default:
                        msg = str;
                        break;
                }
                new OrderBank().DoBankComplete(Bank.suppId, orderId, supplierOrderId, status, opstate, msg, result, new Decimal(0), false, true);
            }
            else
                HttpContext.Current.Response.Write("<script>alert('出现异常！请查看充值是否到帐。若未到帐，请与管理员联系。');</script>");
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
