using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.Model;
using viviapi.SysConfig;
using viviLib.Security;

namespace viviapi.ETAPI.tongyi
{
    public class WuYou2API : ETAPIBase
    {
        private static int suppId = 10041;
        private string noticestr = RuntimeSetting.SiteDomain + "/notify/WuYou2_Notify.aspx";
        private string returnstr = RuntimeSetting.SiteDomain + "/return/WuYou2_Return.aspx";
        private PayHttpClient pay = new PayHttpClient();

        static WuYou2API()
        {
        }

        public WuYou2API()
          : base(WuYou2API.suppId)
        {
        }

        public string UnifiedOrder(string orderid, Decimal orderAmt, string bankcode)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(WuYou2API.suppId);
            string str1 = this.postBankUrl;
            string str2 = "";
            string str3 = bankcode == "992" ? "alipay" : (bankcode == "1004" ? "wxpay" : (bankcode == "1008" ? "tenpay" : (bankcode == "1006" ? "alipay" : (bankcode == "1007" ? "wxpay" : (bankcode == "1009" ? "tenpay" : "alipay")))));
            string puserid = cacheModel.puserid;
            if (!string.IsNullOrEmpty(cacheModel.jumpUrl))
            {
                str1 = cacheModel.jumpUrl + "/pay/WuYou2pay.aspx";
                this.noticestr = cacheModel.jumpUrl + "/notify/WuYou2_Notify.aspx";
            }
            else if (int.Parse(bankcode) < 1000)
                str1 = "http://order.ganningdz.com/api/gatewaypay";
            else if (bankcode == "1005")
                str1 = "http://order.ganningdz.com/api/ordinarypay";
            else if (bankcode == "992" || bankcode == "1004" || bankcode == "1008")
                str1 = "http://order.ganningdz.com/api/scanpay";
            else if (bankcode == "1006" || bankcode == "1007" || bankcode == "1009")
                str1 = "http://order.ganningdz.com/api/wappay";
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("mchId", puserid);
            dictionary1.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("outOrderId", orderid);
            dic.Add("amount", (orderAmt * 100).ToString("f0"));
            if (int.Parse(bankcode) < 1000)
            {
                dic.Add("body", "product_" + orderid.Substring(10, 6));
                dic.Add("name", "product_" + orderid.Substring(10, 6));
                dic.Add("bankCode", this.GetBankCode(bankcode));
                dic.Add("cardType", "1");
                dic.Add("frontUrl", this.returnstr);
                dic.Add("notifyUrl", this.noticestr);
            }
            else if (bankcode == "1005")
            {
                dic.Add("subject", "product_" + orderid.Substring(10, 6));
                dic.Add("userId", "18091720012");
                dic.Add("frontUrl", this.returnstr);
                dic.Add("notifyUrl", this.noticestr);
            }
            else
            {
                dic.Add("subject", "product_" + orderid.Substring(10, 6));
                dic.Add("body", "product_" + orderid.Substring(10, 6));
                dic.Add("source", str3);
                dic.Add("clientIp", ((object)HttpContext.Current.Request.ServerVariables.Get("Remote_Addr")).ToString());
                dic.Add("notifyUrl", this.noticestr);
            }
            string str4 = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(Util.DictionaryToJson(dic)));
            dictionary1.Add("data", str4);
            string str5 = Cryptography.MD5(Util.CreateLinkString2(dictionary1) + "&key=" + cacheModel.puserkey, "UTF-8").ToUpper();
            dictionary1.Add("signature", str5);
            this.pay.setReqContent(new Dictionary<string, string>()
      {
        {
          "url",
          str1
        },
        {
          "data",
          Util.DictionaryToJson(dictionary1)
        }
      });
            if (this.pay.call1())
                str2 = this.pay.getResContent();
            else
                Log.WriteLog(this.GetType().ToString(), "收单错误: ", this.pay.getErrInfo());
            WuYou2API.Resultinfo resultinfo = (WuYou2API.Resultinfo)JsonConvert.DeserializeObject(str2, typeof(WuYou2API.Resultinfo));
            string str6;
            if (resultinfo.status == "SUCCESS")
            {
                if (resultinfo.resultCode == "200")
                {
                    string @string = Encoding.UTF8.GetString(Convert.FromBase64String(resultinfo.data));
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    Dictionary<string, string> dictionary3 = Util.JsonToDictionary1(@string);
                    str6 = !(bankcode == "992") && !(bankcode == "1004") ? (!(bankcode == "1008") && !(bankcode == "1007") && !(bankcode == "1006") && !(bankcode == "1009") ? dictionary3["url"] : dictionary3["payInfo"]) : dictionary3["codeUrl"];
                }
                else
                    str6 = "收单失败: " + resultinfo.message;
            }
            else
            {
                Log.WriteLog(this.GetType().ToString(), "收单失败: ", resultinfo.message);
                str6 = "创建订单失败，请重试！" + resultinfo.message;
            }
            return str6;
        }

        public void ReturnBank()
        {
            HttpContext.Current.Response.Redirect("/PayOK.aspx?orderid=" + HttpContext.Current.Request.QueryString["outOrderId"]);
        }

        public void Notify()
        {
            Stream inputStream = HttpContext.Current.Request.InputStream;
            StreamReader streamReader = new StreamReader(HttpContext.Current.Request.InputStream);
            string str1 = streamReader.ReadToEnd();
            using (streamReader)
            {
                Dictionary<string, string> dicArray = Util.JsonToDictionary1(str1);
                string str2 = dicArray["signature"];
                dicArray.Remove("signature");
                SupplierInfo cacheModel = SupplierFactory.GetCacheModel(WuYou2API.suppId);
                if (Cryptography.MD5(Util.CreateLinkString3(dicArray) + "&key=" + cacheModel.puserkey, "UTF-8").ToUpper() == str2)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (dicArray["orderStatus"] == "SUCCESS")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderBank().DoBankComplete(WuYou2API.suppId, dicArray["outOrderId"], dicArray["orderId"], status, opstate, "success", Decimal.Parse(dicArray["amount"]) / new Decimal(100), new Decimal(0), true, false);
                }
                else
                    Log.WriteLog(this.GetType().ToString(), "验签失败 ", str1);
            }
            HttpContext.Current.Response.Write("SUCCESS");
            HttpContext.Current.Response.End();
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "967":
                    return "ICBC";
                case "964":
                    return "ABC";
                case "962":
                    return "CTIB";
                case "982":
                    return "HXBJ";
                case "963":
                    return "BOC";
                case "965":
                    return "CCB";
                case "981":
                    return "BCOM";
                case "986":
                    return "CEB";
                case "980":
                    return "SZPA";
                case "985":
                    return "CGBC";
                case "978":
                    return "SZPA";
                case "970":
                    return "CMB";
                case "972":
                    return "IBCN";
                case "977":
                    return "SPDB";
                case "975":
                    return "BOS";
                case "971":
                    return "PSBC";
                default:
                    return "ICBC";
            }
        }

        public class Resultinfo
        {
            public string status;
            public string data;
            public string message;
            public string resultCode;
            public string timestamp;
            public string signature;
        }

        public class Resultinfo1
        {
            public string result_code;
            public WuYou2API.Content1 data;
            public string result_message;
            public string sign;
        }

        public class Content1
        {
            public string merchant_order_sn { get; set; }

            public string total_fee { get; set; }

            public string body { get; set; }
        }
    }
}

