//using System.Threading.Tasks;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testconsole;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.Configuration;

namespace viviapi.ETAPI.Qingyifu
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10008;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/qingyifu/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/qingyifu/bank_notify.aspx";
            }
        }

        public Bank()
            : base(Bank.suppId)
        {
        }

        public string GetPayUrl(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "1004":    //微信
                    str = "http://wx.qyfpay.com:90/api/pay.action";
                    break;
                case "1007":    //微信wap
                    str = "http://wxwap.qyfpay.com:90/api/pay.action";
                    break;
                case "2005":    //微信h5
                    str = "http://wx.qyfpay.com:90/api/pay.action";
                    break;
                case "992":    //支付宝
                    str = "http://zfb.qyfpay.com:90/api/pay.action";
                    break;
                case "1006":    //支付宝wap
                    str = "http://zfbwap.qyfpay.com:90/api/pay.action";
                    break;
                case "1008":    //QQ钱包
                    str = "http://qq.qyfpay.com:90/api/pay.action";
                    break;
                case "1009":    //QQ钱包wap
                    str = "http://qqwap.qyfpay.com:90/api/pay.action";
                    break;
                case "2001":    //京东钱包
                    str = "http://jd.qyfpay.com:90/api/pay.action";
                    break;
                case "2002":    //京东钱包wap
                    str = "http://jd.qyfpay.com:90/api/pay.action";
                    break;
                case "2003":    //银联钱包
                    str = "http://unionpay.qyfpay.com:90/api/pay.action";
                    break;
                case "2006":    //百度钱包
                    str = "http://baidu.qyfpay.com:90/api/pay.action";
                    break;
                case "1005":    //手机网银
                    str = "http://mbank.qyfpay.com:90/api/pay.action";
                    break;
            }
            return str;
        }

        public string GetNetWay1(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "1004":    //微信
                    str = "WX";
                    break;
                case "2005":    //微信h5
                    str = "WX_H5";
                    break;
                case "992":    //支付宝
                    str = "ZFB";
                    break;
                case "1008":    //QQ钱包
                    str = "QQ";
                    break;
                case "2001":    //京东钱包
                    str = "JD";
                    break;
                case "2003":    //银联钱包
                    str = "UNION_WALLET";
                    break;
                case "2006":    //百度钱包
                    str = "BAIDU";
                    break;
                case "1007":    //微信wap
                    str = "WX_WAP";
                    break;
                case "1006":    //支付宝wap
                    str = "ZFB_WAP";
                    break;
                case "1009":    //QQ钱包wap
                    str = "QQ_WAP";
                    break;
                case "2002":    //京东钱包wap
                    str = "JD_WAP";
                    break;
                case "1005":    //手机网银
                    str = "MBANK";
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string payurl = this.GetPayUrl(bankcode);//请求地址
            string userId = this.suppAccount;//商户ID
            string orderNo = orderid;//orderid;
            string netway = this.GetNetWay1(bankcode);
            string amount = (orderAmt * 100).ToString("f0");
            Dictionary<String, String> paramdic = new Dictionary<String, String>();
            paramdic.Add("version", "V3.1.0.0");
            paramdic.Add("merNo", userId);
            paramdic.Add("netway", netway);
            paramdic.Add("random", new Random().Next(1000, 9999).ToString());//随机数 可以重复
            paramdic.Add("orderNum", orderNo);//订单交易号
            paramdic.Add("amount", amount);//订单金额 单位分
            paramdic.Add("goodsName", HttpUtility.UrlEncode(orderNo));//商品名称
            paramdic.Add("callBackUrl", this.notifyUrl);//支付结果异步通知地址 
            paramdic.Add("callBackViewUrl", this.returnurl);//回显地址 this.returnurl
            paramdic.Add("charset", "UTF-8");

            paramdic = paramdic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);

            string sourceStr = "{";
            foreach (KeyValuePair<string, string> v in paramdic)
            {
                sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
            }
            sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}" + ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayMD5Key"); //MD5秘钥
            paramdic.Add("sign", MD5Encrypt.MD5(sourceStr, false).ToUpper());

            paramdic = paramdic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);
            sourceStr = "{";

            foreach (KeyValuePair<string, string> v in paramdic)
            {
                sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
            }
            sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}";

            //LogHelper.Write("sourceStr提交:" + sourceStr.Substring(0, sourceStr.Length - 1));

            string publicKey = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayPublicKey"); // 加密公匙;
            string cipher_data = "";
            publicKey = RSAEncodHelper.RSAPublicKeyJava2DotNet(publicKey);

            byte[] cdatabyte = RSAEncodHelper.RSAPublicKeySignByte(sourceStr, publicKey);
            cipher_data = Convert.ToBase64String(cdatabyte);

            string paramstr = "data=" + System.Web.HttpUtility.UrlEncode(cipher_data) + "&merchNo=" + userId + "&version=" + paramdic["version"];

            string strResult = HttpPost.SendPost(payurl, paramstr, 5000);
            //LogHelper.Write("strResult地址:" + strResult);

            //支付返回
            try
            {
                if (string.IsNullOrEmpty(strResult))
                    return "";

                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                LitJson.JsonData jd = JsonMapper.ToObject(strResult);
                if (jd == null)
                    return "";
                if ((string)jd["stateCode"] != "00")
                {
                    return (string)jd["msg"];
                }
                paramsDic.Add("merNo", (string)jd["merNo"]);
                paramsDic.Add("stateCode", (string)jd["stateCode"]);
                paramsDic.Add("msg", (string)jd["msg"]);
                paramsDic.Add("orderNum", (string)jd["orderNum"]);
                paramsDic.Add("qrcodeUrl", (string)jd["qrcodeUrl"]);

                paramsDic = paramsDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);
                sourceStr = "{";
                foreach (KeyValuePair<string, string> v in paramsDic)
                {
                    sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
                }
                sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}" + ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayMD5Key"); //MD5秘钥

                //LogHelper.Write("sourceStr返回:" + sourceStr.Substring(0, sourceStr.Length - 1));

                if (MD5Encrypt.MD5(sourceStr, false).ToUpper().Equals((string)jd["sign"]) == false)
                {
                    //Log.WriteTextLog("RuiFuPayInfo", "PayCallBackHandle验签失败:返回原文:" + sourceStr + "；返回密钥：" + (string)jd["sign"] + "", DateTime.Now);
                    return "验签失败!" + strResult;
                }
                //LogHelper.Write("qrcodeUrl地址:" + (string)jd["qrcodeUrl"]);
                HttpContext.Current.Response.Redirect((string)jd["qrcodeUrl"], false);
                return (string)jd["qrcodeUrl"];
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void ReturnBank()
        {
            try
            {
                string orderNum = HttpContext.Current.Request["orderNum"];
                string data = HttpContext.Current.Request["data"];

                //string privateKey = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAKIfb1j63x+85wcp427IT7hUWAP8CaXTBQ0vLYGr61Rxy1qSJybSR0x6PBK/v+jtzGHcQ+pWmHE6XGlAVnrvNuoWqR0N02cbef15LfsIvSbuma2wsmCKV53JK3v4Oe56BPQXy9J8oK7HXQqrq86+nTGZZsRw4aN7uwobq9tGv9KRAgMBAAECgYBLd4ySg/1XPczhVctr50zMxl5ORIWNLmSclYh+YzPhlDMQDxawiJPt8ryCEcZYvFE2gJ0QuYyusHcR+4QGJ9KMeYFXlEgTqrD0NrB0noa85osULNSkwbsrBKsTFgvh/lV4Zt0kZI1CdhdqgKpK9LWVZYUZ/5hHOKQokrXA7zpeAQJBANN6CT8FQl2V/8kpnD4qF9o84QGaVhJrF32b1faCfhzaAFQDJ2/h/JzKhMVYjY4NUTPu3sasWsdnDsRNQufoPuECQQDEQV7DlrOCMk9Oc6F8u1OCj2CpJVYl/WKfy11Tps59YQESJ/OhbcBUBwjF6zyQ4jrhl2tx6l/dnHL68U6b9HmxAkAXY1fCcIJ2dzBivwdYmK8qo7D+zGLYhp+CdMmlCamI51NB91dVOFkHvh4Q9Uoye6aPZ8ubjVQ82Vj4vNK5cyYBAkBG5wsShJaT2hM216WnB5JzH9OfKGMIVJPWAUXVW/VL7MjTQ2XMk3chpGzx/DukaGc3a1ohDLjXupb7vERXKarxAkB4kHUtn1CBwAogBL+o6k55wQlAk+jVxawSjoCay6sk0DnjTLfzvNUk7OomKQQE3ieM7dbS2F8Md81Ir9opQbQT"; //"加密私钥"
                string privateKey = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPrivateKey"); // 加密私匙;

                string result = RSAHelper.decryptData(data, privateKey, "utf-8");
                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                LitJson.JsonData jd = JsonMapper.ToObject(result);
                if (jd == null)
                {
                    HttpContext.Current.Response.Write("error");
                    HttpContext.Current.Response.End();
                }

                //string GateUserKey = "9FD785C3499D0022A3176F7CF517F6C1";
                string GateUserKey = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayMD5Key"); //MD5秘钥
                paramsDic.Add("amount", (string)jd["amount"]);
                paramsDic.Add("goodsName", (string)jd["goodsName"]);
                paramsDic.Add("merNo", (string)jd["merNo"]);
                paramsDic.Add("netway", (string)jd["netway"]);
                paramsDic.Add("orderNum", (string)jd["orderNum"]);
                paramsDic.Add("payDate", (string)jd["payDate"]);
                paramsDic.Add("payResult", (string)jd["payResult"]);
                paramsDic = paramsDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);
                string sourceStr = "{";
                foreach (KeyValuePair<string, string> v in paramsDic)
                {
                    sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
                }
                sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}" + GateUserKey;
                if (MD5Encrypt.MD5(sourceStr, false).ToUpper().Equals((string)jd["sign"]) == false)
                {
                    HttpContext.Current.Response.Write("error");
                    HttpContext.Current.Response.End();
                }
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if ((string)jd["payResult"] == "00")
                {
                    opstate = "0";
                    status = 2;
                }
                orderBank.DoBankComplete(Bank.suppId, (string)jd["orderNum"], (string)jd["orderNum"], status, opstate, string.Empty, Decimal.Parse((string)jd["amount"]) / 100m, new Decimal(0), false, true);
                HttpContext.Current.Response.Write("0");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }

        public void Notify()
        {
            try
            {
                string orderNum = HttpContext.Current.Request["orderNum"];
                string data = HttpContext.Current.Request["data"];

                //string privateKey = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAKIfb1j63x+85wcp427IT7hUWAP8CaXTBQ0vLYGr61Rxy1qSJybSR0x6PBK/v+jtzGHcQ+pWmHE6XGlAVnrvNuoWqR0N02cbef15LfsIvSbuma2wsmCKV53JK3v4Oe56BPQXy9J8oK7HXQqrq86+nTGZZsRw4aN7uwobq9tGv9KRAgMBAAECgYBLd4ySg/1XPczhVctr50zMxl5ORIWNLmSclYh+YzPhlDMQDxawiJPt8ryCEcZYvFE2gJ0QuYyusHcR+4QGJ9KMeYFXlEgTqrD0NrB0noa85osULNSkwbsrBKsTFgvh/lV4Zt0kZI1CdhdqgKpK9LWVZYUZ/5hHOKQokrXA7zpeAQJBANN6CT8FQl2V/8kpnD4qF9o84QGaVhJrF32b1faCfhzaAFQDJ2/h/JzKhMVYjY4NUTPu3sasWsdnDsRNQufoPuECQQDEQV7DlrOCMk9Oc6F8u1OCj2CpJVYl/WKfy11Tps59YQESJ/OhbcBUBwjF6zyQ4jrhl2tx6l/dnHL68U6b9HmxAkAXY1fCcIJ2dzBivwdYmK8qo7D+zGLYhp+CdMmlCamI51NB91dVOFkHvh4Q9Uoye6aPZ8ubjVQ82Vj4vNK5cyYBAkBG5wsShJaT2hM216WnB5JzH9OfKGMIVJPWAUXVW/VL7MjTQ2XMk3chpGzx/DukaGc3a1ohDLjXupb7vERXKarxAkB4kHUtn1CBwAogBL+o6k55wQlAk+jVxawSjoCay6sk0DnjTLfzvNUk7OomKQQE3ieM7dbS2F8Md81Ir9opQbQT"; //"加密私钥"
                string privateKey = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPrivateKey"); // 加密私匙;

                string result = RSAHelper.decryptData(data, privateKey, "utf-8");
                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                LitJson.JsonData jd = JsonMapper.ToObject(result);
                if (jd == null)
                {
                    HttpContext.Current.Response.Write("error");
                    HttpContext.Current.Response.End();
                }

                //string GateUserKey = "9FD785C3499D0022A3176F7CF517F6C1";
                string GateUserKey = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayMD5Key"); //MD5秘钥
                paramsDic.Add("amount", (string)jd["amount"]);
                paramsDic.Add("goodsName", (string)jd["goodsName"]);
                paramsDic.Add("merNo", (string)jd["merNo"]);
                paramsDic.Add("netway", (string)jd["netway"]);
                paramsDic.Add("orderNum", (string)jd["orderNum"]);
                paramsDic.Add("payDate", (string)jd["payDate"]);
                paramsDic.Add("payResult", (string)jd["payResult"]);
                paramsDic = paramsDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);
                string sourceStr = "{";
                foreach (KeyValuePair<string, string> v in paramsDic)
                {
                    sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
                }
                sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}" + GateUserKey;
                if (MD5Encrypt.MD5(sourceStr, false).ToUpper().Equals((string)jd["sign"]) == false)
                {
                    HttpContext.Current.Response.Write("error");
                    HttpContext.Current.Response.End();
                }
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if ((string)jd["payResult"] == "00")
                {
                    opstate = "0";
                    status = 2;
                }
                string amount = (string)jd["amount"];
                orderBank.DoBankComplete(Bank.suppId, (string)jd["orderNum"], (string)jd["orderNum"], status, opstate, string.Empty, Decimal.Parse(amount) / 100m, new Decimal(0), true, false);
                HttpContext.Current.Response.Write("0");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }


        public string ReturnStatusByCode(string code)
        {
            string result = "支付成功";
            switch (code)
            {
                case "06":
                    result = "初始";
                    break;
                case "50":
                    result = "网络异常";
                    break;
                case "04":
                    result = "其他错误";
                    break;
                case "03":
                    result = "签名错误";
                    break;
                case "01":
                    result = "失败";
                    break;
                case "99":
                    result = "未支付";
                    break;
                case "05":
                    result = "未知";
                    break;
            }
            return result;
        }
    }
}
