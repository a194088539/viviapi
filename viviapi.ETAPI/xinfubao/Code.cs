using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using viviapi.BLL;
using viviLib.Logging;
using viviLib.Web;

namespace viviapi.ETAPI.XinFuBao
{
    public class Code : ETAPIBase
    {
        private static int suppId = 10004;
        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/xfb/Notify.aspx";
            }
        }

        internal string returnUrl
        {
            get
            {
                return this.SiteDomain + "/notify/xfb/Notify.aspx";
            }
        }
        public Code()
            : base(Code.suppId)
        {
        }

        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = this.postBankUrl;
            if (string.IsNullOrEmpty(str1))
                str1 = "http://online.atrustpay.com/payment/ScanPayApply.do";
            string versionId = "1.0";
            string orderAmount = (orderAmt * 100).ToString("f0");
            string orderDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string currency = "RMB";
            string transType = "008";
            string asynNotifyUrl = notifyUrl;
            string synNotifyUrl = returnUrl;
            string signType = "MD5";
            string merId = suppAccount;
            string prdOrdNo = orderid;

            string payMode = "00022";
            if (bankcode == "1004" || bankcode == "1007")
                payMode = "00022";
            else if (bankcode == "1008" || bankcode == "1009")
                payMode = "00032";
            else if (bankcode == "992" || bankcode == "1006")
                payMode = "00021";
            string receivableType = "D00";
            string prdAmt = orderAmount;
            string prdDisUrl = "";
            string prdName = orderid;
            string prdShortName = "";
            string prdDesc = "";
            string merParam = "";

            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("versionId", versionId);
            waitSign.Add("orderAmount", orderAmount);
            waitSign.Add("orderDate", orderDate);
            waitSign.Add("currency", currency);
            waitSign.Add("transType", transType);
            waitSign.Add("asynNotifyUrl", asynNotifyUrl);
            waitSign.Add("synNotifyUrl", synNotifyUrl);
            waitSign.Add("signType", signType);
            waitSign.Add("merId", merId);
            waitSign.Add("prdOrdNo", prdOrdNo);
            waitSign.Add("payMode", payMode);
            waitSign.Add("receivableType", receivableType);
            waitSign.Add("prdAmt", prdAmt);
            waitSign.Add("prdDisUrl", prdDisUrl);
            waitSign.Add("prdName", prdName);
            waitSign.Add("prdShortName", prdShortName);
            waitSign.Add("prdDesc", prdDesc);
            waitSign.Add("merParam", merParam);
            string signdata = "";
            string postdata = "";
            foreach (var key in waitSign.Keys)
            {
                if (waitSign[key].Length > 0)
                {
                    signdata += key + "=" + waitSign[key] + "&";
                    postdata += key + "=" + waitSign[key] + "&";
                }
            }
            signdata = signdata + "key=" + suppKey;
            string signed = UserMd5(signdata).ToUpper();
            postdata += "signData=" + signed;

            LogHelper.Write(DateTime.Now.ToString() + ":信付宝Post数据---" + postdata);

            string result = WebClientHelper.GetString(str1, postdata, "POST", Encoding.GetEncoding("UTF-8"), 10000);
            LogHelper.Write(DateTime.Now.ToString() + ":信付宝返回---" + result);

            JavaScriptSerializer js = new JavaScriptSerializer();
            ReturnMessage msg = js.Deserialize<ReturnMessage>(result);
            if (msg.qrcode != null && msg.qrcode.Length > 0)
            {
                return msg.qrcode;
            }
            else
            {
                return "error:" + msg.retMsg;
            }

        }

        public class ReturnMessage
        {
            //{"signData":"FF13E13E14AA0DD8E3C66E00769A2516","code":"1","qrcode":"https://myun.tenpay.com/mqq/pay/qrcode.html?_wv=1027&_bid=2183&t=5Va8eaf350c5b3607db617bc619552b6","signType":"MD5","platmerord":"892415957085986816","retCode":"1","serviceName":"支付申请-扫码支付订单建立","retMsg":"下单成功","desc":"下单成功"}
            public string signData { get; set; }
            public string code { get; set; }
            public string qrcode { get; set; }
            public string signType { get; set; }
            public string platmerord { get; set; }
            public string retCode { get; set; }
            public string serviceName { get; set; }
            public string retMsg { get; set; }
            public string desc { get; set; }
        }

        public void Notify()
        {

            LogHelper.Write("开始处理信付宝异步通知");
            NameValueCollection formlist = HttpContext.Current.Request.Form;
            NameValueCollection querylist = HttpContext.Current.Request.QueryString;
            string signdata = "";
            string signData = "";
            SortedDictionary<string, string> list = new SortedDictionary<string, string>();

            foreach (string key in formlist.Keys)
            {

                if (key == "signData")
                    signData = formlist[key];
                else
                {
                    list.Add(key, formlist[key]);
                    //signdata += key + "=" + formlist[key] + "&";
                }
            }
            foreach (string key in querylist.Keys)
            {
                //list.Add(key, querylist[key]);
                if (key == "signData")
                    signData = querylist[key];
                else
                {
                    list.Add(key, querylist[key]);
                    //signdata += key + "=" + querylist[key] + "&";
                }
            }
            foreach (string key in list.Keys)
            {
                signdata += key + "=" + list[key] + "&";
            }

            signdata = signdata + "key=" + suppKey;
            string signed = UserMd5(signdata).ToUpper();

            LogHelper.Write(DateTime.Now.ToString() + ":信付宝异步返回数据---" + signdata + "&signed=" + signed);
            if (signed.ToUpper().Equals(signData.ToUpper()))
            {
                string msg = "支付失败";
                string opstate = "-1";
                int status = 4;
                //执行操作方法
                if (list["orderStatus"].Equals("01"))
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                    string str5 = string.Empty;

                    new OrderBank().DoBankComplete(Code.suppId, list["prdOrdNo"], list["payId"], status, opstate, msg, Decimal.Parse(list["orderAmount"]) / 100m, new Decimal(0), true, false);
                    HttpContext.Current.Response.Write("SUCCESS");
                }
            }
        }
    }
}
