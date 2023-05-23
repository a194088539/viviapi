using System;
using System.Web;
using viviapi.BLL;
using viviapi.Model;
using viviapi.SysConfig;

namespace viviapi.ETAPI.WxPayAPI
{
    public class WxPayApi
    {
        public static WxPayData UnifiedOrder(WxPayData inputObj, int timeOut)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(99);
            string str = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            if (!inputObj.IsSet("out_trade_no"))
                throw new WxPayException("缺少统一支付接口必填参数out_trade_no！");
            if (!inputObj.IsSet("body"))
                throw new WxPayException("缺少统一支付接口必填参数body！");
            if (!inputObj.IsSet("total_fee"))
                throw new WxPayException("缺少统一支付接口必填参数total_fee！");
            if (!inputObj.IsSet("trade_type"))
                throw new WxPayException("缺少统一支付接口必填参数trade_type！");
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
                throw new WxPayException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
                throw new WxPayException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            if (!inputObj.IsSet("notify_url"))
                inputObj.SetValue("notify_url", (object)(RuntimeSetting.SiteDomain + "/notify/WXBank_Notify.aspx"));
            inputObj.SetValue("appid", (object)cacheModel.pusername);
            inputObj.SetValue("mch_id", (object)cacheModel.puserid);
            inputObj.SetValue("spbill_create_ip", (object)((object)HttpContext.Current.Request.ServerVariables.Get("Local_Addr")).ToString());
            inputObj.SetValue("nonce_str", (object)WxPayApi.GenerateNonceStr());
            inputObj.SetValue("sign", (object)inputObj.MakeSign());
            string xml1 = inputObj.ToXml();
            DateTime now = DateTime.Now;
            Log.Debug("WxPayApi", "UnfiedOrder request : " + xml1);
            string xml2 = HttpService.Post(xml1, str, false, timeOut);
            Log.Debug("WxPayApi", "UnfiedOrder response : " + xml2);
            int timeCost = (int)(DateTime.Now - now).TotalMilliseconds;
            WxPayData inputObj1 = new WxPayData();
            inputObj1.FromXml(xml2);
            WxPayApi.ReportCostTime(str, timeCost, inputObj1);
            return inputObj1;
        }

        public static WxPayData OrderQuery(WxPayData inputObj, int timeOut)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(99);
            string str = "https://api.mch.weixin.qq.com/pay/orderquery";
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
                throw new WxPayException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            inputObj.SetValue("appid", (object)cacheModel.pusername);
            inputObj.SetValue("mch_id", (object)cacheModel.puserid);
            inputObj.SetValue("nonce_str", (object)WxPayApi.GenerateNonceStr());
            inputObj.SetValue("sign", (object)inputObj.MakeSign());
            string xml1 = inputObj.ToXml();
            DateTime now = DateTime.Now;
            Log.Debug("WxPayApi", "OrderQuery request : " + xml1);
            string xml2 = HttpService.Post(xml1, str, false, timeOut);
            Log.Debug("WxPayApi", "OrderQuery response : " + xml2);
            int timeCost = (int)(DateTime.Now - now).TotalMilliseconds;
            WxPayData inputObj1 = new WxPayData();
            inputObj1.FromXml(xml2);
            WxPayApi.ReportCostTime(str, timeCost, inputObj1);
            return inputObj1;
        }

        private static void ReportCostTime(string interface_url, int timeCost, WxPayData inputObj)
        {
        }

        public static WxPayData Report(WxPayData inputObj, int timeOut)
        {
            string url = "https://api.mch.weixin.qq.com/payitil/report";
            if (!inputObj.IsSet("interface_url"))
                throw new WxPayException("接口URL，缺少必填参数interface_url！");
            if (!inputObj.IsSet("return_code"))
                throw new WxPayException("返回状态码，缺少必填参数return_code！");
            if (!inputObj.IsSet("result_code"))
                throw new WxPayException("业务结果，缺少必填参数result_code！");
            if (!inputObj.IsSet("user_ip"))
                throw new WxPayException("访问接口IP，缺少必填参数user_ip！");
            if (!inputObj.IsSet("execute_time_"))
                throw new WxPayException("接口耗时，缺少必填参数execute_time_！");
            inputObj.SetValue("appid", (object)"wx2428e34e0e7dc6ef");
            inputObj.SetValue("mch_id", (object)"1233410002");
            inputObj.SetValue("user_ip", (object)"8.8.8.8");
            inputObj.SetValue("time", (object)DateTime.Now.ToString("yyyyMMddHHmmss"));
            inputObj.SetValue("nonce_str", (object)WxPayApi.GenerateNonceStr());
            inputObj.SetValue("sign", (object)inputObj.MakeSign());
            string xml1 = inputObj.ToXml();
            Log.Info("WxPayApi", "Report request : " + xml1);
            string xml2 = HttpService.Post(xml1, url, false, timeOut);
            Log.Info("WxPayApi", "Report response : " + xml2);
            WxPayData wxPayData = new WxPayData();
            wxPayData.FromXml(xml2);
            return wxPayData;
        }

        public static string GenerateOutTradeNo()
        {
            return string.Format("{0}{1}{2}", (object)"1233410002", (object)DateTime.Now.ToString("yyyyMMddHHmmss"), (object)new Random().Next(999));
        }

        public static string GenerateTimeStamp()
        {
            return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
        }

        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
