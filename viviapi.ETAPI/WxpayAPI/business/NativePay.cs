using System;
using System.Collections.Generic;
using System.Web;
using viviapi.SysConfig;

namespace viviapi.ETAPI.WxPayAPI
{
    public class NativePay
    {
        private string msg = "";
        private string posturl = "";

        public string GetPrePayUrl(string productId, string orderAmt)
        {
            Log.Info(this.GetType().ToString(), "Native pay mode 1 url is producing...");
            WxPayData wxPayData = new WxPayData();
            wxPayData.SetValue("appid", (object)"wx2428e34e0e7dc6ef");
            wxPayData.SetValue("mch_id", (object)"1233410002");
            wxPayData.SetValue("time_stamp", (object)WxPayApi.GenerateTimeStamp());
            wxPayData.SetValue("nonce_str", (object)WxPayApi.GenerateNonceStr());
            wxPayData.SetValue("product_id", (object)productId);
            wxPayData.SetValue("sign", (object)wxPayData.MakeSign());
            string str = "weixin://wxpay/bizpayurl?" + this.ToUrlParams(wxPayData.GetValues());
            Log.Info(this.GetType().ToString(), "Get native pay mode 1 url : " + str);
            return str;
        }

        public string GetPayUrl(string productId, string orderAmt)
        {
            Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");
            this.posturl = "/PayOK.aspx?orderid=" + productId;
            WxPayData inputObj = new WxPayData();
            inputObj.SetValue("body", (object)PaymentSetting.weixin_body);
            inputObj.SetValue("attach", (object)PaymentSetting.weixin_subject);
            inputObj.SetValue("out_trade_no", (object)productId);
            inputObj.SetValue("total_fee", (object)orderAmt);
            inputObj.SetValue("time_start", (object)DateTime.Now.ToString("yyyyMMddHHmmss"));
            WxPayData wxPayData1 = inputObj;
            string key = "time_expire";
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMinutes(10.0);
            string str1 = dateTime.ToString("yyyyMMddHHmmss");
            wxPayData1.SetValue(key, (object)str1);
            inputObj.SetValue("goods_tag", (object)"Game");
            inputObj.SetValue("trade_type", (object)"NATIVE");
            inputObj.SetValue("product_id", (object)productId);
            string str2 = "";
            WxPayData wxPayData2 = WxPayApi.UnifiedOrder(inputObj, 6);
            if (wxPayData2.GetValue("return_code").ToString() == "SUCCESS")
            {
                str2 = wxPayData2.GetValue("code_url").ToString();
            }
            else
            {
                this.msg = wxPayData2.GetValue("err_code_des").ToString();
                HttpContext.Current.Response.Write(string.Format("<script>alert('{0}');window.location.href='{1}'</script>", (object)this.msg, (object)this.posturl));
            }
            Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + str2);
            return str2;
        }

        private string ToUrlParams(SortedDictionary<string, object> map)
        {
            string str = "";
            foreach (KeyValuePair<string, object> keyValuePair in map)
                str = str + (object)keyValuePair.Key + "=" + (string)keyValuePair.Value + "&";
            return str.Trim('&');
        }
    }
}
