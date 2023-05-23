using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using viviapi.BLL;
using viviLib.Logging;
using viviLib.Web;

namespace viviapi.ETAPI.TongLian
{
    public class Code : ETAPIBase
    {
        private static int suppId = 10002;

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/tonglian/Notify.aspx";
            }
        }

        public Code()
            : base(Code.suppId)
        {
        }
        public class ReturnMessage
        {
            ////{"merchNo":"M00000029","orderId":"2017111910594000000799","orderNo":"17111910593988030475","qrcodeUrl":"https://myun.tenpay.com/mqq/pay/qrcode.html?_wv=1027&_bid=2183&t=6V7b924273815f9c48d5ef658527b9e1","respCode":"00000","respDesc":"下单成功","sign":"308fa10f92443ca53b8bc9fa322e0221","transAmount":"1000"}
            public string respCode { get; set; }//返回00代表生成二维码成功,其他代表失败
            public string merchNo { get; set; }//商户号
            public string orderNo { get; set; }//商户订单号
            public string orderId { get; set; }//渠道订单号
            public string transAmount { get; set; }//二维码信息
            public string qrcodeUrl { get; set; }
            public string respDesc { get; set; }
            public string sign { get; set; }
        }
        public class NotifyMessage
        {
            public string merchNo { get; set; }
            public string orderNo { get; set; }
            public string orderId { get; set; }
            public string transAmount { get; set; }
            public string orderStatus { get; set; }
            public string orderPayTime { get; set; }
            public string notifyTime { get; set; }
            public string sign { get; set; }
        }
        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = "http://47.94.208.216:8080/app/doWXPay.do";
            if (bankcode == "1004" || bankcode == "1007")
                str1 = "http://47.94.208.216:8080/app/doWXPay.do";
            else if (bankcode == "1008" || bankcode == "1009")
                str1 = "http://47.94.208.216:8080/app/doQQPay.do";

            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("merchNo", suppAccount);
            waitSign.Add("orderNo", orderid);
            waitSign.Add("transAmount", (orderAmt * 100).ToString("f0"));
            waitSign.Add("productName", orderid);
            waitSign.Add("notifyUrl", notifyUrl);
            string signdata = "";
            //string postdata = "{";
            foreach (var key in waitSign.Keys)
            {
                signdata += key + "=" + waitSign[key] + "&";
                //postdata += "\""+key + "\":\"" + waitSign[key] + "";
            }
            JavaScriptSerializer js = new JavaScriptSerializer();

            signdata = signdata.Substring(0, signdata.Length - 1) + suppKey;
            string signed = UserMd5(signdata, "UTF-8");
            waitSign.Add("sign", signed);
            //postdata += postdata + "&sign=" + signed;
            string x = js.Serialize(waitSign);
            LogHelper.Write(DateTime.Now.ToString() + "中信交易提交参数:" + x);
            try
            {
                string result = WebClientHelper.GetJsonString(str1, x, "POST", Encoding.GetEncoding("UTF-8"), 100000);

                LogHelper.Write(DateTime.Now.ToString() + "中信交易提交参数同步返回参数:" + result);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                ReturnMessage msg = ser.Deserialize<ReturnMessage>(result);

                if (!string.IsNullOrEmpty(msg.qrcodeUrl))
                {
                    return msg.qrcodeUrl;
                }
                else
                    return "error:" + msg.respDesc;
            }
            catch (Exception ex)
            {
                return "error" + ex.Message;
            }
        }


        public static string UserMd5(string str, string encoding)
        {

            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.GetEncoding(encoding).GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        public void Notify()
        {

            LogHelper.Write("中信异步调用成功");
            try
            {
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                string jsonData = "";
                using (var reader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    jsonData = reader.ReadToEnd();
                }
                //NameValueCollection queryList = HttpContext.Current.Request.QueryString;
                //NameValueCollection formList = HttpContext.Current.Request.Form;
                //string signdata = "";
                //SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
                //foreach (string key in queryList)
                //{
                //    signdata += key + "=" + queryList[key] + "&";
                //    waitSign.Add(key, queryList[key]);
                //}
                //foreach (string key in formList)
                //{
                //    signdata += key + "=" + formList[key] + "&";
                //    waitSign.Add(key, formList[key]);
                //}
                LogHelper.Write(DateTime.Now.ToString() + "中信异步参数" + jsonData);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                NotifyMessage resp = ser.Deserialize<NotifyMessage>(jsonData);

                //signdata = signdata.Substring(0, signdata.Length - 1) + suppKey;
                //HttpRequest Request = HttpContext.Current.Request.QueryString;
                //返回参数
                //String orderPayTime = Request["orderPayTime"];//交易日期
                //String notifyTime = Request["notifyTime"];//交易时间
                //String merchNo = Request["merchNo"];//商户号、
                //String orderNo = Request["orderNo"];//k客户号
                //String transAmount = Request["transAmount"];//返回实际充值金额
                //String orderId = Request["orderId"];//系统订单号
                //string orderStatus = Request["orderStatus"];
                //String sign = Request["sign"];//返回签名

                //SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
                //waitSign.Add("orderPayTime", orderPayTime);
                //waitSign.Add("notifyTime", notifyTime);
                //waitSign.Add("merchNo", merchNo);
                //waitSign.Add("orderNo", orderNo);
                //waitSign.Add("transAmount", transAmount);
                //waitSign.Add("orderId", orderId);
                //waitSign.Add("orderStatus", orderStatus);


                //foreach (var key in waitSign.Keys)
                //{
                //    if (waitSign[key] != null)
                //    {
                //        if (waitSign[key].Length > 0)
                //            signdata += key + "=" + waitSign[key] + "&";
                //    }
                //}

                //signdata = signdata.Substring(0, signdata.Length - 1) + suppKey;

                //string signed = UserMd5(signdata, "UTF-8");
                //比对签名是否有效
                //if (signed.ToLower() == signature.ToLower())
                //{
                //LogHelper.Write("捷宝扫码异步返回验签成功：signdata" + signdata);
                string msg = "支付失败";// + this.GetErrorInfo(result, resultDesc)
                string opstate = "-1";
                int status1 = 4;
                //执行操作方法
                if (resp.orderStatus.Equals("1"))
                {
                    msg = "支付成功";
                    opstate = "0";
                    status1 = 2;
                    //LogHelper.Write("中信异步开始处理数据：signdata" + signdata);
                    if (new OrderBank().DoBankComplete(Code.suppId, resp.orderNo, resp.orderId, status1, opstate, msg, Decimal.Parse(resp.transAmount) / 100m, new Decimal(0), true, false))
                        HttpContext.Current.Response.Write("success");
                    else
                        HttpContext.Current.Response.Write("fail");
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
