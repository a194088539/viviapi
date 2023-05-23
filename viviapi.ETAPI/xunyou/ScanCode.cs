using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.Logging;
using viviLib.Web;

namespace viviapi.ETAPI.xunyou
{
    public class Scan : ETAPIBase
    {
        private static int suppId = 10032;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/xunyou/scan_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/xunyou/scan_notify.aspx";
            }
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

        public string GetScanCode(string paymodeId)  //T+1
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "992":    //支付宝
                    str = "20000301";
                    break;
                case "1004":    //微信
                    str = "10000101";
                    break;
                case "1008":    //QQ钱包
                    str = "70000101";
                    break;
                case "2001":    //京东钱包
                    str = "80000101";
                    break;
                case "2003":    //银联钱包
                    str = "60000101";
                    break;
                case "1007":    //微信wap
                    str = "10000201";
                    break;
                case "1006":    //支付宝wap
                    str = "20000201";
                    break;
                case "1009":    //QQ钱包wap
                    str = "70000203";
                    break;
                case "2002":    //京东钱包wap
                    str = "80000203";
                    break;
                case "2007":    //支付宝h5
                    str = "20000201";
                    break;
                case "2005":    //微信h5
                    str = "10000201";
                    break;
                case "2008":    //QQ钱包h5
                    str = "70000203";
                    break;
            }
            return str;
        }

        public Scan()
            : base(Scan.suppId)
        {
        }

        public string PayScan(string orderid, Decimal orderAmt, string bankcode)
        {
            string payUrl = "https://gateway.ioo8.com/scanPay/initPay";

            string payKey1 = "fa45e79c60c44762af2cf47f16349bb0";	//商户支付Key
            string paySecret1 = "7d135aa46dd14a9baa896cbd1f0a88c5";
            string orderPrice1 = orderAmt.ToString("f2");	        //订单金额，单位：元,保留小数点后两位，测试请在10元以上
            string outTradeNo1 = orderid;	                        //商户支付订单号（长度30以内）
            string productType1 = this.GetScanCode(bankcode);	    //产品类型,B2C T0:50000103,B2C T1:50000101
            string orderTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");	//下单时间，格式(yyyyMMddHHmmss)
            string productName1 = "Q币";	                            //支付产品名称
            string orderIp1 = "127.0.0.1";	                        //下单IP
            string returnUrl1 = this.returnurl;                     //页面通知地址
            string notifyUrl1 = this.notifyUrl;	                    //后台异步通知地址
            string subPayKey1 = "";	                                //子商户支付Key，大商户时必填
            string remark1 = "购买Q币";	                            //备注
            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("notifyUrl", notifyUrl1);
            waitSign.Add("orderIp", orderIp1);
            waitSign.Add("orderPrice", orderPrice1);
            waitSign.Add("orderTime", orderTime1);
            waitSign.Add("outTradeNo", outTradeNo1);
            waitSign.Add("payKey", payKey1);
            waitSign.Add("productName", productName1);
            waitSign.Add("productType", productType1);
            waitSign.Add("remark", remark1);
            waitSign.Add("returnUrl", returnUrl1);

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
            signdata = signdata + "paySecret=" + paySecret1;
            string signed = UserMd5(signdata).ToUpper();
            postdata += "sign=" + signed;

            LogHelper.Write(DateTime.Now.ToString() + ":迅游通Post数据---" + postdata);

            string result = WebClientHelper.GetString(payUrl, postdata, "POST", Encoding.GetEncoding("UTF-8"), 10000);
            LogHelper.Write(DateTime.Now.ToString() + ":迅游通返回---" + result);
            return result;
        }

        public void Notify()
        {
            LogHelper.Write("开始处理迅游通异步通知");
            NameValueCollection formlist = HttpContext.Current.Request.Form;
            NameValueCollection querylist = HttpContext.Current.Request.QueryString;
            string signdata = "";
            string sign = "";
            SortedDictionary<string, string> list = new SortedDictionary<string, string>();

            foreach (string key in formlist.Keys)
            {

                if (key == "sign")
                    sign = formlist[key];
                else
                {
                    list.Add(key, formlist[key]);
                    //signdata += key + "=" + formlist[key] + "&";
                }
            }
            foreach (string key in querylist.Keys)
            {
                //list.Add(key, querylist[key]);
                if (key == "sign")
                    sign = querylist[key];
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

            LogHelper.Write(DateTime.Now.ToString() + ":迅游通异步返回数据---" + signdata + "&signed=" + signed);
            if (signed.ToUpper().Equals(sign.ToUpper()))
            {
                string msg = "支付失败";
                string opstate = "-1";
                int status = 4;
                //执行操作方法
                if (list["tradeStatus"].Equals("SUCCESS"))
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                    string str5 = string.Empty;

                    new OrderBank().DoBankComplete(Scan.suppId, list["outTradeNo"], list["trxNo"], status, opstate, msg, Decimal.Parse(list["orderPrice"]) / 100m, new Decimal(0), true, false);
                    HttpContext.Current.Response.Write("SUCCESS");
                }
            }
        }


        public void ReturnScan()
        {
            LogHelper.Write("开始处理迅游通同步跳转");
            NameValueCollection formlist = HttpContext.Current.Request.Form;
            NameValueCollection querylist = HttpContext.Current.Request.QueryString;
            string signdata = "";
            string sign = "";
            SortedDictionary<string, string> list = new SortedDictionary<string, string>();

            foreach (string key in formlist.Keys)
            {

                if (key == "sign")
                    sign = formlist[key];
                else
                {
                    list.Add(key, formlist[key]);
                    //signdata += key + "=" + formlist[key] + "&";
                }
            }
            foreach (string key in querylist.Keys)
            {
                //list.Add(key, querylist[key]);
                if (key == "sign")
                    sign = querylist[key];
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

            LogHelper.Write(DateTime.Now.ToString() + ":迅游通同步返回数据---" + signdata + "&signed=" + signed);
            if (signed.ToUpper().Equals(sign.ToUpper()))
            {
                string msg = "支付失败";
                string opstate = "-1";
                int status = 4;
                //执行操作方法
                if (list["tradeStatus"].Equals("SUCCESS"))
                {
                    msg = "支付成功";
                    opstate = "0";
                    status = 2;
                    string str5 = string.Empty;

                    new OrderBank().DoBankComplete(Scan.suppId, list["outTradeNo"], list["trxNo"], status, opstate, msg, Decimal.Parse(list["orderPrice"]) / 100m, new Decimal(0), false, true);
                    HttpContext.Current.Response.Write("SUCCESS");
                }
            }
        }


    }
}