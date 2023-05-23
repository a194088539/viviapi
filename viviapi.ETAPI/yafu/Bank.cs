namespace viviapi.ETAPI.YaFu
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Script.Serialization;
    using viviapi.BLL;
    using viviapi.ETAPI;
    using viviapi.Model.Order;
    using viviLib.ExceptionHandling;
    using viviLib.Logging;
    using viviLib.Web;

    public class Bank : ETAPIBase
    {
        private static int suppId = 10001;

        public Bank()
            : base(suppId)
        {
        }

        public string GetBankCode(string paymodeId)
        {
            switch (paymodeId)
            {
                case "962":
                    return "CITIC";
                case "963":
                    return "BOC";
                case "964":
                    return "ABC";
                case "965":
                    return "CCB";
                case "967":
                    return "ICBC";

                case "970":
                    return "CMB";
                case "971":
                    return "PSBC";
                case "972":
                    return "CIB";
                case "974":
                    return "SDB";
                case "975":
                    return "BOS";

                case "977":
                    return "SPDB";
                case "978":
                    return "PAB";
                case "979":
                    return "NJCB";
                case "980":
                    return "CMBC";
                case "981":
                    return "BOCM";
                case "982":
                    return "HXBC";


                case "985":
                    return "CGB";
                case "986":
                    return "CEB";
                //case "987":
                //    return "";
                //case "988":
                //    return "";
                case "989":
                    return "BOBJ";
                //case "990":
                //    return "";
                //case "994":
                //    return "";
                //case "995":
                //    return "";
                //case "996":
                //    return "";
                //case "997":
                //    return "";
                case "998":
                    return "NBCB";
                //case "999":
                //    return "";
                default:
                    return "";
            }
        }

        public void Notify()
        {
            //consumerNo=20005&merOrderNo=1705181619109890349&orderNo=20170518161911930036&orderStatus=1&payType=0201&sign=52FDE660FD3646A3FA1FA163F2A9EB84&transAmt=0.10&version=3.0
            HttpRequest request = HttpContext.Current.Request;
            string version = request["version"];
            string consumerNo = request["consumerNo"];
            string merOrderNo = request["merOrderNo"];
            string orderNo = request["orderNo"];
            string transAmt = request["transAmt"];
            string orderStatus = request["orderStatus"];
            string sign = request["sign"];
            string payType = request["payType"];
            //HttpRequest request = HttpContext.Current.Request;
            //NameValueCollection collection = request.Form;
            //string str = "";
            //foreach (string key in collection.Keys)
            //{
            //    str += key + "=" + collection[key];
            //}
            //LogHelper.Write("ydNotify:" + str);

            //str = str.Substring(1);

            LogHelper.Write("雅付：" + request.Form.ToString());
            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("version", version);
            waitSign.Add("consumerNo", consumerNo);
            waitSign.Add("merOrderNo", merOrderNo);
            waitSign.Add("orderNo", orderNo);
            waitSign.Add("transAmt", transAmt);
            waitSign.Add("orderStatus", orderStatus);
            waitSign.Add("payType", payType);
            string signdata = "";
            foreach (string key in waitSign.Keys)
            {
                if (!string.IsNullOrEmpty(waitSign[key]))
                {
                    signdata += key + "=" + waitSign[key] + "&";
                }
            }

            signdata += "key=" + suppKey;
            string md5str = UserMd5(signdata).ToUpper();
            try
            {
                if (md5str == sign)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (orderStatus == "1")
                    {
                        opstate = "0";
                        status = 2;

                        new OrderBank().DoBankComplete(suppId, merOrderNo, orderNo, status, opstate, "支付成功", decimal.Parse(transAmt), 0M, true, false);
                        HttpContext.Current.Response.Write("SUCCESS");
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public string PayBank(string orderid, decimal orderAmt, string BankID)
        {
            string paytype = "";
            if (BankID == "992")//支付宝
                paytype = "0302";
            else if (BankID == "1004")//微信
                paytype = "0202";
            else if (BankID == "1008" || BankID == "1009")//qq
                paytype = "0502";
            else if (BankID == "2001" || BankID == "2002")//jd
                paytype = "0802";
            else if (BankID == "2003")//银联
                paytype = "0702";
            else
                paytype = "0101";

            string postBankUrl = base.postBankUrl;
            if (string.IsNullOrEmpty(postBankUrl))
            {
                postBankUrl = "http://yf.yafupay.com/yfpay/cs/pay.ac";
            }
            orderAmt = decimal.Round(orderAmt, 2);

            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("version", "3.0");
            waitSign.Add("consumerNo", suppAccount);
            waitSign.Add("merOrderNo", orderid);
            waitSign.Add("transAmt", orderAmt.ToString("f2"));
            waitSign.Add("backUrl", notifyUrl);
            waitSign.Add("frontUrl", returnurl);
            waitSign.Add("bankCode", GetBankCode(BankID));
            waitSign.Add("payType", paytype);
            waitSign.Add("goodsName", orderid);
            string signdata = "";
            string postdata = "";
            string url = "";
            foreach (string key in waitSign.Keys)
            {
                if (!string.IsNullOrEmpty(waitSign[key]))
                {
                    signdata += key + "=" + waitSign[key] + "&";
                    postdata += key + "=" + waitSign[key] + "&";
                }
            }

            signdata += "key=" + suppKey;
            string md5str = UserMd5(signdata).ToUpper();
            url = postBankUrl + "?" + postdata + "sign=" + md5str;

            //{"busContent":"weixin://wxpay/bizpayurl?pr=vLMlO3Z","contentType":"01","orderNo":"20171117164847794247","merOrderNo":"1711171648343190074","consumerNo":"20880","transAmt":"2.00","orderStatus":"0","code":"000000","msg":"success","sign":"DB7FD1849EC8DDA6FD3011D76CD8C579"}

            string result = WebClientHelper.GetString(url, "", "POST", Encoding.GetEncoding("UTF-8"), 100000);
            LogHelper.Write(DateTime.Now.ToString() + ":雅付返回---" + result);
            JavaScriptSerializer js = new JavaScriptSerializer();
            ReturnMessage msg = js.Deserialize<ReturnMessage>(result);
            if (!string.IsNullOrEmpty(msg.busContent))
                return msg.busContent;
            else
                return "yf_error:" + msg.msg;


        }

        public class ReturnMessage
        {
            public string version { get; set; }
            public string code { get; set; }
            public string msg { get; set; }
            public string consumerNo { get; set; }
            public string merOrderNo { get; set; }
            public string orderNo { get; set; }
            public string contentType { get; set; }
            public string busContent { get; set; }
            public string orderStatus { get; set; }
            public string transAmt { get; set; }
            public string sign { get; set; }
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

        public string PayBankApp(string orderid, decimal orderAmt, string BankID)
        {
            string paytype = "";
            if (BankID == "1006")//支付宝
                paytype = "0303";
            else if (BankID == "1007")//微信
                paytype = "0901";
            else if (BankID == "1009")//qq
                paytype = "0502";
            else if (BankID == "2002")//jd
                paytype = "0803";
            string postBankUrl = base.postBankUrl;
            if (string.IsNullOrEmpty(postBankUrl))
            {
                postBankUrl = "http://yf.yafupay.com/yfpay/cs/pay.ac";
            }
            orderAmt = decimal.Round(orderAmt, 2);

            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("version", "3.0");
            waitSign.Add("consumerNo", suppAccount);
            waitSign.Add("merOrderNo", orderid);
            waitSign.Add("transAmt", orderAmt.ToString("f2"));
            waitSign.Add("backUrl", notifyUrl);
            waitSign.Add("frontUrl", returnurl);
            waitSign.Add("bankCode", GetBankCode(BankID));
            waitSign.Add("payType", paytype);
            waitSign.Add("goodsName", orderid);
            string signdata = "";
            string postdata = "";
            foreach (string key in waitSign.Keys)
            {
                if (!string.IsNullOrEmpty(waitSign[key]))
                {
                    signdata += key + "=" + waitSign[key] + "&";
                    postdata += key + "=" + waitSign[key] + "&";
                }
            }

            signdata += "key=" + suppKey;
            string md5str = UserMd5(signdata).ToUpper();
            postdata = postBankUrl + "?" + postdata + "sign=" + md5str;
            //if (!string.IsNullOrEmpty(waitSign["bankCode"]))
            //{
            //    return postdata;
            //}
            return postdata;
        }

        public void BankOrderReturn(OrderBankInfo orderinfo)
        {
            string s = SystemApiHelper.NewBankNoticeUrl(orderinfo, false);
            if (orderinfo.version == "vyb1.00")
            {
                HttpContext.Current.Response.Write(s);
            }
            else
            {
                HttpContext.Current.Response.Redirect(s, false);
            }
        }

        public void ReturnBank()
        {
            HttpRequest request = HttpContext.Current.Request;
            string version = request["version"];
            string consumerNo = request["consumerNo"];
            string merOrderNo = request["merOrderNo"];
            string orderNo = request["orderNo"];
            string transAmt = request["transAmt"];
            string orderStatus = request["orderStatus"];
            string sign = request["sign"];

            string payType = request["payType"];
            string code = request["code"];
            string msg = request["msg"];

            //orderStatus=1&merOrderNo=1706241117125290968&orderNo=20170624111713912038&transAmt=0.10&consumerNo=20223&code=000000&msg=success&vsersion=&payType=0201&sign=C49275FBB217116F03048A7FF8D9C017
            LogHelper.Write("雅付同步返回参数：" + request.Form.ToString());
            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("version", version);
            waitSign.Add("consumerNo", consumerNo);
            waitSign.Add("merOrderNo", merOrderNo);
            waitSign.Add("orderNo", orderNo);
            waitSign.Add("transAmt", transAmt);
            waitSign.Add("orderStatus", orderStatus);
            waitSign.Add("payType", payType);
            waitSign.Add("code", code);
            waitSign.Add("msg", msg);

            string signdata = "";
            foreach (string key in waitSign.Keys)
            {
                if (!string.IsNullOrEmpty(waitSign[key]))
                {
                    signdata += key + "=" + waitSign[key] + "&";
                }
            }

            signdata += "key=" + suppKey;

            string md5str = UserMd5(signdata).ToUpper();
            try
            {
                //if (md5str == sign)
                //{
                string opstate = "-1";
                int status = 4;
                if (orderStatus == "1")
                {
                    opstate = "0";
                    status = 2;

                    new OrderBank().DoBankComplete(suppId, merOrderNo, orderNo, status, opstate, "支付成功", decimal.Parse(transAmt), 0M, false, true);
                    //HttpContext.Current.Response.Write("SUCCESS");
                }
                //}
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/yafu/Notify.aspx");
            }
        }

        internal string returnurl
        {
            get
            {
                return (base.SiteDomain + "/notify/yafu/Return.aspx");
            }
        }
    }
}

