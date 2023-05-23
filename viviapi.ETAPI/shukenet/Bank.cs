using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;

namespace viviapi.ETAPI.shukenet
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10038;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/shukenet/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return (base.SiteDomain + "/notify/shukenet/bank_Notify.aspx");
            }
        }

        public Bank()
            : base(suppId)
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

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "CMB";    //招商银行
                    break;
                case "967":
                    str = "ICBC";   //工商银行
                    break;
                case "964":
                    str = "ABC";    //农业银行
                    break;
                case "965":
                    str = "CCB";    //建设银行
                    break;
                case "963":
                    str = "BOC";    //中国银行
                    break;
                case "977":
                    str = "SPDB";   //浦东发展银行
                    break;
                case "981":
                    str = "BCOM";   //交通银行
                    break;
                case "980":
                    str = "CMBC";   //民生银行
                    break;
                case "985":
                    str = "GDB";    //广东发展银行
                    break;
                case "962":
                    str = "CITIC";  //中信银行
                    break;
                case "972":
                    str = "CIB";    //兴业银行
                    break;
                case "971":
                    str = "PSBC";   //中国邮政储蓄银行
                    break;
                case "986":
                    str = "CEB";    //中国光大银行
                    break;
                case "978":
                    str = "PAB";    //平安银行
                    break;
                case "989":
                    str = "BOB";    //北京银行
                    break;
                case "975":
                    str = "SHB";    //上海银行
                    break;
                case "990":
                    str = "BJRCB";  //北京农村商业银行
                    break;
                case "1000":
                    str = "QUICK";  //快捷
                    break;
                case "992":
                    str = "ALIPAY"; //支付宝
                    break;
                case "1004":
                    str = "WEBCHAT";    //微信
                    break;
                case "1008":
                    str = "TENPAY";     //QQ
                    break;
                case "2001":
                    str = "JDPAY";      //京东
                    break;
            }
            return str;
        }

        public string GetBankCodeH5(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "2007":
                    str = "ALIPAY";     //支付宝h5
                    break;
                case "2005":
                    str = "WEBCHAT";    //微信h5
                    break;
                case "2008":
                    str = "TENPAY";     //QQh5
                    break;
                case "1006":
                    str = "ALIPAY";     //支付宝wap
                    break;
                case "1007":
                    str = "WEBCHAT";    //微信wap
                    break;
                case "1009":
                    str = "TENPAY";     //QQwap
                    break;
                case "2002":
                    str = "JDPAY";      //京东wap
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, decimal orderAmt, string bankcode)
        {
            string payUrl = "https://pay.shukenet.com/lqdpay/gateway";

            string service_type = "connect_service";                                //支付服务类型
            string merchant_code = this.suppAccount;                                //商家号
            string interface_version = "V2.0";                                      //接口版本
            string sign_type = "MD5";                                               //签名类型
            string order_no = orderid;                                              //订单号
            string order_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//订单时间，时间格式：yyyy-MM-dd HH:mm:ss
            string order_amount = orderAmt.ToString("f2");                          //订单金额
            string product_number = "1";                                            //商品数量
            string notify_url = this.notifyUrl;                                     //异步通知网址
            string return_url = this.returnurl;                                     //同步通知地址
            string bank_code = this.GetBankCode(bankcode);                          //网关支付渠道编码
            string product_name = "";                                               //商品名称
            string order_userid = "";                                               //支付会员账号
            string order_info = "";                                                 //商品附加信息
            string notice_type = "1";                                               //通知类型
            string pay_model = "SCODE";                                                //支付请求模型
            string txnString = merchant_code + "~|~" + interface_version + "~|~" + sign_type + "~|~" + order_no + "~|~" + order_time + "~|~" + order_amount + "~|~" + product_number + "~|~" + notify_url + "~|~" + return_url + "~|~" + bank_code + "~|~" + notice_type + "~|~" + service_type + "~|~" + this.suppKey;
            //LogHelper.Write("txnString=" + txnString);
            string sign = UserMd5(txnString).ToLower();
            string url = payUrl + "?service_type=" + service_type + "&merchant_code=" + merchant_code + "&interface_version=" + interface_version + "&sign_type=" + sign_type + "&order_no=" + order_no + "&order_time=" + order_time + "&order_amount=" + order_amount + "&product_number=" + product_number + "&notify_url=" + notify_url + "&return_url=" + return_url + "&bank_code=" + bank_code + "&product_name=" + product_name + "&order_userid=" + order_userid + "&order_info=" + order_info + "&notice_type=" + notice_type + "&pay_model=" + pay_model + "&sign=" + sign;
            HttpContext.Current.Response.Redirect(url);
            return url;
        }

        public string PayBankH5(string orderid, decimal orderAmt, string bankcode)
        {
            string payUrl = "https://pay.shukenet.com/lqdpay/gateway";

            string service_type = "connect_service";                                //支付服务类型
            string merchant_code = this.suppAccount;                                //商家号
            string interface_version = "V2.0";                                      //接口版本
            string sign_type = "MD5";                                               //签名类型
            string order_no = orderid;                                              //订单号
            string order_time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//订单时间，时间格式：yyyy-MM-dd HH:mm:ss
            string order_amount = orderAmt.ToString("f2");                          //订单金额
            string product_number = "1";                                            //商品数量
            string notify_url = this.notifyUrl;                                     //异步通知网址
            string return_url = this.returnurl;                                     //同步通知地址
            string bank_code = this.GetBankCodeH5(bankcode);                          //网关支付渠道编码
            string product_name = "";                                               //商品名称
            string order_userid = "";                                               //支付会员账号
            string order_info = "";                                                 //商品附加信息
            string notice_type = "1";                                               //通知类型
            string pay_model = "H5";                                                //支付请求模型
            string txnString = merchant_code + "~|~" + interface_version + "~|~" + sign_type + "~|~" + order_no + "~|~" + order_time + "~|~" + order_amount + "~|~" + product_number + "~|~" + notify_url + "~|~" + return_url + "~|~" + bank_code + "~|~" + notice_type + "~|~" + service_type + "~|~" + this.suppKey;
            //LogHelper.Write("txnString=" + txnString);
            string sign = UserMd5(txnString).ToLower();
            string url = payUrl + "?service_type=" + service_type + "&merchant_code=" + merchant_code + "&interface_version=" + interface_version + "&sign_type=" + sign_type + "&order_no=" + order_no + "&order_time=" + order_time + "&order_amount=" + order_amount + "&product_number=" + product_number + "&notify_url=" + notify_url + "&return_url=" + return_url + "&bank_code=" + bank_code + "&product_name=" + product_name + "&order_userid=" + order_userid + "&order_info=" + order_info + "&notice_type=" + notice_type + "&pay_model=" + pay_model + "&sign=" + sign;
            HttpContext.Current.Response.Redirect(url);
            return url;
        }

        public void ReturnBank()
        {
            string merchant_code = HttpContext.Current.Request["merchant_code"].ToString().Trim();          //商家号，商户签约时分配给商家的唯一身份标识。
            string interface_version = HttpContext.Current.Request["interface_version"].ToString().Trim();  //接口版本，固定值：V1.0(大写)
            string order_no = HttpContext.Current.Request["order_no"].ToString().Trim();                    //商家网站生成的订单号，由商户保证其唯一性，由字母、数字、下划线组成。
            string trade_no = HttpContext.Current.Request["trade_no"].ToString().Trim();                    //数科宝平台的支付订单号。
            string order_amount = HttpContext.Current.Request["order_amount"].ToString().Trim();            //商家订单金额，以元为单位，精确到小数点后两位.例如：12.01。
            string product_number = HttpContext.Current.Request["product_number"].ToString().Trim();        //商品数量，必须是整型数字。
            string order_success_time = HttpContext.Current.Request["order_success_time"].ToString().Trim(); //订单支付成功时间，时间格式：yyyy-MM-dd HH:mm:ss。
            string order_time = HttpContext.Current.Request["order_time"].ToString().Trim();                //商家订单时间，时间格式：yyyy-MM-dd HH:mm:ss。
            string order_status = HttpContext.Current.Request["order_status"].ToString().Trim();            //订单支付状态，固定值：支付成功（success）、支付失败（failure）。
            string bank_code = HttpContext.Current.Request["bank_code"].ToString().Trim();                  //消费者支付渠道编码。
            string sign_type = HttpContext.Current.Request["sign_type"].ToString().Trim();                  //签名类型，固定值：MD5或RSA(大写)。
            string bank_name = HttpContext.Current.Request["bank_name"].ToString().Trim();                  //消费者支付渠道名称。
            string product_name = HttpContext.Current.Request["product_name"].ToString().Trim();            //商品名称，商户非必填情况传的空字符串。
            string order_userid = HttpContext.Current.Request["order_userid"].ToString().Trim();            //商户平台支付会员账号，商户非必填情况传的空字符串。
            string order_info = HttpContext.Current.Request["order_info"].ToString().Trim();                //商品附加信息，商户非必填情况传的空字符串。
            string sign = HttpContext.Current.Request["sign"].ToString().Trim();                            //签名
            string suppKey = base.suppKey;
            string txnString2 = merchant_code + "~|~" + interface_version + "~|~" + order_no + "~|~" + trade_no + "~|~" + order_amount + "~|~" + product_number + "~|~" + order_success_time + "~|~" + order_time + "~|~" + order_status + "~|~" + bank_code + "~|~" + this.suppKey;
            string sign2 = UserMd5(txnString2).ToLower();
            if (sign == sign2)
            {
                OrderBank orderBank = new OrderBank();
                int status = 4;
                string opstate = "1";
                if (order_status == "success")
                {
                    status = 2;
                    opstate = "0";
                }
                new OrderBank().DoBankComplete(Bank.suppId, order_no, trade_no, status, opstate, string.Empty, Decimal.Parse(order_amount), new Decimal(0), false, true);
                HttpContext.Current.Response.Write("success");
            }
            else
            {
                HttpContext.Current.Response.Write("failure");
            }
        }

        public void Notify()
        {
            string merchant_code = HttpContext.Current.Request["merchant_code"].ToString().Trim();          //商家号，商户签约时分配给商家的唯一身份标识。
            string interface_version = HttpContext.Current.Request["interface_version"].ToString().Trim();  //接口版本，固定值：V1.0(大写)
            string order_no = HttpContext.Current.Request["order_no"].ToString().Trim();                    //商家网站生成的订单号，由商户保证其唯一性，由字母、数字、下划线组成。
            string trade_no = HttpContext.Current.Request["trade_no"].ToString().Trim();                    //数科宝平台的支付订单号。
            string order_amount = HttpContext.Current.Request["order_amount"].ToString().Trim();            //商家订单金额，以元为单位，精确到小数点后两位.例如：12.01。
            string product_number = HttpContext.Current.Request["product_number"].ToString().Trim();        //商品数量，必须是整型数字。
            string order_success_time = HttpContext.Current.Request["order_success_time"].ToString().Trim(); //订单支付成功时间，时间格式：yyyy-MM-dd HH:mm:ss。
            string order_time = HttpContext.Current.Request["order_time"].ToString().Trim();                //商家订单时间，时间格式：yyyy-MM-dd HH:mm:ss。
            string order_status = HttpContext.Current.Request["order_status"].ToString().Trim();            //订单支付状态，固定值：支付成功（success）、支付失败（failure）。
            string bank_code = HttpContext.Current.Request["bank_code"].ToString().Trim();                  //消费者支付渠道编码。
            string sign_type = HttpContext.Current.Request["sign_type"].ToString().Trim();                  //签名类型，固定值：MD5或RSA(大写)。
            string bank_name = HttpContext.Current.Request["bank_name"].ToString().Trim();                  //消费者支付渠道名称。
            string product_name = HttpContext.Current.Request["product_name"].ToString().Trim();            //商品名称，商户非必填情况传的空字符串。
            string order_userid = HttpContext.Current.Request["order_userid"].ToString().Trim();            //商户平台支付会员账号，商户非必填情况传的空字符串。
            string order_info = HttpContext.Current.Request["order_info"].ToString().Trim();                //商品附加信息，商户非必填情况传的空字符串。
            string sign = HttpContext.Current.Request["sign"].ToString().Trim();                            //签名
            string suppKey = base.suppKey;
            string txnString2 = merchant_code + "~|~" + interface_version + "~|~" + order_no + "~|~" + trade_no + "~|~" + order_amount + "~|~" + product_number + "~|~" + order_success_time + "~|~" + order_time + "~|~" + order_status + "~|~" + bank_code + "~|~" + this.suppKey;
            string sign2 = UserMd5(txnString2).ToLower();
            if (sign == sign2)
            {
                OrderBank orderBank = new OrderBank();
                int status = 4;
                string opstate = "1";
                if (order_status == "success")
                {
                    status = 2;
                    opstate = "0";
                }
                new OrderBank().DoBankComplete(Bank.suppId, order_no, trade_no, status, opstate, string.Empty, Decimal.Parse(order_amount), new Decimal(0), true, false);
                HttpContext.Current.Response.Write("success");
            }
            else
            {
                HttpContext.Current.Response.Write("failure");
            }
        }

    }
}

