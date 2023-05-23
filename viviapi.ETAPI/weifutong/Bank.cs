using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.SessionState;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.weifutong
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10012;
        private static HttpSessionState _session = HttpContext.Current.Session;

        internal string returnUrl
        {
            get
            {
                return (base.SiteDomain + "/return/weifutong/bank_Return.aspx");
            }
        }
        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/weifutong/bank_Notify.aspx");
            }
        }

        public Bank()
            : base(suppId)
        {
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "992":
                    return "pay.alipay.native";

                case "1004":
                    return "pay.weixin.native";

                case "1008":
                    return "pay.tenpay.native";

                case "2001":
                    return "pay.jdpay.native";

                case "1007":
                    return "pay.weixin.wappay";

                case "2005":
                    return "pay.weixin.raw.app";

                case "2002":
                    return "pay.jdpay.jspay";

                case "1009":
                    return "pay.tenpay.wappay";

                case "2003":
                    return "pay.unionpay.native";

                case "1000":
                    return "unified.trade.micropay";
            }
            return str;
        }

        private ClientResponseHandler resHandler = new ClientResponseHandler();
        private PayHttpClient pay = new PayHttpClient();
        private RequestHandler reqHandler = null;
        private static int SUM = 0;

        private string getIp()
        {
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            else
            {
                return "127.0.0.1";
            }
        }
        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>  
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static void qclear()
        {
            SUM = 0;//初始化刷新计数值
        }



        public string ReadHtml()
        {
            HttpWebResponse httpResp;
            HttpWebRequest httpReq;
            string strBuff = "";
            char[] cbuffer = new char[256];
            int byteRead = 0;
            Uri httpURL = new Uri("http://zhangwei.dev.swiftpass.cn/notify.aspx");

            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法 建立，并进行强制的类型转换 
            httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换

            httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容

            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理 
            Stream respStream = httpResp.GetResponseStream();

            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以

            //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8） 
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);

            byteRead = respStreamReader.Read(cbuffer, 0, 256);

            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                strBuff = strBuff + strResp;
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }

            respStream.Close();
            return strBuff;
        }


        public string PayBank(string orderid, decimal orderAmt, string bankcode)
        {
            this.reqHandler = new RequestHandler(null);
            this.reqHandler.setGateUrl(postBankUrl);
            this.reqHandler.setKey(this.suppKey);
            this.reqHandler.setParameter("out_trade_no", orderid);//商户订单号
            this.reqHandler.setParameter("body", "充值");//商品描述
            this.reqHandler.setParameter("attach", "");//附加信息
            this.reqHandler.setParameter("total_fee", (orderAmt * 100M).ToString("f0"));//总金额
            this.reqHandler.setParameter("mch_create_ip", getIp());//终端IP
            this.reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            this.reqHandler.setParameter("time_expire", DateTime.Now.AddSeconds(600).ToString("yyyyMMddHHmmss"));//订单超时时间
            this.reqHandler.setParameter("service", this.GetBankCode(bankcode));//接口类型：pay.weixin.native
            this.reqHandler.setParameter("mch_id", "403540118997");//必填项，商户号，由平台分配
            this.reqHandler.setParameter("version", "2.0");//接口版本号
            this.reqHandler.setParameter("notify_url", notifyUrl);//通知地址，必填项，接收平台通知的URL，需给绝对路径，255字符内;此URL要保证外网能访问   
            this.reqHandler.setParameter("nonce_str", Utils.random());//随机字符串，必填项，不长于 32 位
            this.reqHandler.createSign();//创建签名

            //以上参数进行签名
            string data = Utils.toXml(this.reqHandler.getAllParameters());//生成XML报文
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", this.reqHandler.getGateUrl());
            reqContent.Add("data", data);
            this.pay.setReqContent(reqContent);

            //LogHelper.Write("data:" + data);

            if (this.pay.call())
            {
                this.resHandler.setContent(this.pay.getResContent());
                this.resHandler.setKey(this.suppKey);
                Hashtable param = this.resHandler.getAllParameters();

                if (this.resHandler.isTenpaySign())
                {
                    //当返回状态与业务结果都为0时才返回支付二维码，其它结果请查看接口文档
                    if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                    {
                        return HttpUtility.UrlDecode(param["code_img_url"].ToString(), Encoding.GetEncoding("UTF-8"));
                        //return param["code_img_url"].ToString();
                    }
                    else
                    {
                        return "error";
                    }

                }
                else
                {
                    return "error";
                }
            }
            else
            {
                return "error";
            }
        }


        public void BankReturn()
        {
            //初始化数据
            using (StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                SupplierInfo info = SupplierFactory.GetCacheModel(4010);
                this.resHandler.setContent(sr.ReadToEnd());
                this.resHandler.setKey(info.puserkey);

                Hashtable resParam = this.resHandler.getAllParameters();
                if (this.resHandler.isTenpaySign())
                {
                    string opstate = "-1";
                    int status = 4;
                    if (int.Parse(resParam["status"].ToString()) == 0 && int.Parse(resParam["result_code"].ToString()) == 0)
                    {
                        opstate = "0";
                        status = 2;
                        string orderId = resParam["out_trade_no"].ToString();
                        string accNo = resParam["transaction_id"].ToString();
                        string tradeAmt = resParam["total_fee"].ToString();
                        new OrderBank().DoBankComplete(suppId, orderId, accNo, status, opstate, string.Empty, (decimal.Parse(tradeAmt) / 100M), 0M, true, true);
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("failure1");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write("failure2");
                }
            }
        }


        public void Notify()
        {
            //初始化数据
            using (StreamReader sr = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                this.resHandler.setContent(sr.ReadToEnd());
                this.resHandler.setKey(suppKey);

                Hashtable resParam = this.resHandler.getAllParameters();
                if (this.resHandler.isTenpaySign())
                {
                    string opstate = "-1";
                    int status = 4;
                    if (int.Parse(resParam["status"].ToString()) == 0 && int.Parse(resParam["result_code"].ToString()) == 0)
                    {
                        opstate = "0";
                        status = 2;
                        string orderId = resParam["out_trade_no"].ToString();
                        string accNo = resParam["transaction_id"].ToString();
                        string tradeAmt = resParam["total_fee"].ToString();
                        new OrderBank().DoBankComplete(suppId, orderId, accNo, status, opstate, string.Empty, (decimal.Parse(tradeAmt) / 100M), 0M, true, false);

                        HttpContext.Current.Response.Write("success");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("failure1");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write("failure2");
                }
            }
        }


    }

}
