using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.ruilianpay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10036;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/ruilianpay/Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/ruilianpay/Notify.aspx";
            }
        }

        public Bank()
            : base(Bank.suppId)
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
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                default:
                    str2 = "970";
                    break;
                case "970"://招商
                    str2 = "CMB";
                    break;
                case "967"://工商
                    str2 = "ICBC";
                    break;
                case "964"://农业
                    str2 = "ABC";
                    break;
                case "965"://建行
                    str2 = "CCB";
                    break;
                case "963"://中行
                    str2 = "BOC";
                    break;
                case "977"://浦发
                    str2 = "SPDB";
                    break;
                case "981"://交通
                    str2 = "COMM";
                    break;
                case "980"://民生
                    str2 = "CMBC";
                    break;
                case "985"://广发
                    str2 = "GDB";
                    break;
                case "962"://中信
                    str2 = "CITIC";
                    break;
                case "982"://华夏
                    str2 = "HXB";
                    break;
                case "972"://兴业
                    str2 = "CIB";
                    break;
                case "989"://北京
                    str2 = "BJB";
                    break;
                case "986"://光大
                    str2 = "CEB";
                    break;
                case "978"://平安
                    str2 = "SPAB";
                    break;
                case "975"://上海
                    str2 = "SHB";
                    break;
                case "971"://邮储
                    str2 = "PSBC";
                    break;
            }
            return str2;
        }

        public string GetPayType(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                default:
                    str = "903";
                    break;
                case "1000":
                    str = "907";        //银联快捷
                    break;
                case "1004":
                    str = "902";        //微信扫码
                    break;
                case "992":
                    str = "903";        //支付宝扫码
                    break;
                case "1008":
                    str = "908";        //QQ扫码
                    break;
                case "2006":
                    str = "909";        //百度钱包
                    break;
                case "2001":
                    str = "910";        //京东支付
                    break;
                case "2007":
                    str = "904";        //支付宝手机 H5
                    break;
                case "1009":
                    str = "905";        //QQ手机
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string payUrl = "http://www.silverspay.com/Pay_Index.html";
            string suppKey = this.suppKey;
            string pay_memberid = this.suppAccount;                                //商户号
            string pay_orderid = orderid;                                          //订单号
            string pay_applydate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //提交时间
            string pay_bankcode = "912";                                           //银行编码
            string pay_notifyurl = this.notifyUrl;                                 //服务端通知
            string pay_callbackurl = this.returnurl;                               //页面跳转通知
            string pay_amount = orderAmt.ToString("f2");                           //订单金额
            string stringSignTemp = "pay_amount=" + pay_amount + "&pay_applydate=" + pay_applydate + "&pay_bankcode=" + pay_bankcode + "&pay_callbackurl=" + pay_callbackurl + "&pay_memberid=" + pay_memberid + "&pay_notifyurl=" + pay_notifyurl + "&pay_orderid=" + pay_orderid + "&key=" + suppKey;
            string pay_md5sign = UserMd5(stringSignTemp).ToUpper();
            string gate = this.GetBankCode(bankcode);                              //银行编码
            string pay_attach = "";                                                //附加字段
            string pay_productname = "";                                           //商品名称
            string pay_productnum = "";                                            //商户品数量
            string pay_productdesc = "";                                           //商品描述
            string pay_producturl = "";                                            //商户链接地址
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + payUrl + "\">" + "<input type=\"hidden\" name=\"pay_memberid\" value=\"" + pay_memberid + "\" />" + "<input type=\"hidden\" name=\"pay_orderid\" value=\"" + pay_orderid + "\" />" + "<input type=\"hidden\" name=\"pay_applydate\" value=\"" + pay_applydate + "\" />" + "<input type=\"hidden\" name=\"pay_bankcode\" value=\"" + pay_bankcode + "\" />" + "<input type=\"hidden\" name=\"pay_notifyurl\" value=\"" + pay_notifyurl + "\" />" + "<input type=\"hidden\" name=\"pay_callbackurl\" value=\"" + pay_callbackurl + "\" />" + "<input type=\"hidden\" name=\"pay_amount\" value=\"" + pay_amount + "\" />" + "<input type=\"hidden\" name=\"gate\" value=\"" + gate + "\" />" + "<input type=\"hidden\" name=\"pay_attach\" value=\"" + pay_attach + "\" />" + "<input type=\"hidden\" name=\"pay_productname\" value=\"" + pay_productname + "\" />" + "<input type=\"hidden\" name=\"pay_productnum\" value=\"" + pay_productnum + "\" />" + "<input type=\"hidden\" name=\"pay_productdesc\" value=\"" + pay_productdesc + "\" />" + "<input type=\"hidden\" name=\"pay_producturl\" value=\"" + pay_producturl + "\" />" + "<input type=\"hidden\" name=\"pay_md5sign\" value=\"" + pay_md5sign + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public string PayCode(string orderid, Decimal orderAmt, string bankcode)
        {
            string payUrl = "http://www.silverspay.com/Pay_Index.html";
            string suppKey = this.suppKey;
            string pay_memberid = this.suppAccount;                                //商户号
            string pay_orderid = orderid;                                          //订单号
            string pay_applydate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //提交时间
            string pay_bankcode = this.GetPayType(bankcode);                       //银行编码
            string pay_notifyurl = this.notifyUrl;                                //服务端通知
            string pay_callbackurl = this.returnurl;                              //页面跳转通知
            string pay_amount = orderAmt.ToString("f2");                           //订单金额
            string stringSignTemp = "pay_amount=" + pay_amount + "&pay_applydate=" + pay_applydate + "&pay_bankcode=" + pay_bankcode + "&pay_callbackurl=" + pay_callbackurl + "&pay_memberid=" + pay_memberid + "&pay_notifyurl=" + pay_notifyurl + "&pay_orderid=" + pay_orderid + "&key=" + suppKey;
            string pay_md5sign = UserMd5(stringSignTemp).ToUpper();
            string pay_attach = "";                                                //附加字段
            string pay_productname = "";                                           //商品名称
            string pay_productnum = "";                                            //商户品数量
            string pay_productdesc = "";                                           //商品描述
            string pay_producturl = "";                                            //商户链接地址
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + payUrl + "\">" + "<input type=\"hidden\" name=\"pay_memberid\" value=\"" + pay_memberid + "\" />" + "<input type=\"hidden\" name=\"pay_orderid\" value=\"" + pay_orderid + "\" />" + "<input type=\"hidden\" name=\"pay_applydate\" value=\"" + pay_applydate + "\" />" + "<input type=\"hidden\" name=\"pay_bankcode\" value=\"" + pay_bankcode + "\" />" + "<input type=\"hidden\" name=\"pay_notifyurl\" value=\"" + pay_notifyurl + "\" />" + "<input type=\"hidden\" name=\"pay_callbackurl\" value=\"" + pay_callbackurl + "\" />" + "<input type=\"hidden\" name=\"pay_amount\" value=\"" + pay_amount + "\" />" + "<input type=\"hidden\" name=\"pay_attach\" value=\"" + pay_attach + "\" />" + "<input type=\"hidden\" name=\"pay_productname\" value=\"" + pay_productname + "\" />" + "<input type=\"hidden\" name=\"pay_productnum\" value=\"" + pay_productnum + "\" />" + "<input type=\"hidden\" name=\"pay_productdesc\" value=\"" + pay_productdesc + "\" />" + "<input type=\"hidden\" name=\"pay_producturl\" value=\"" + pay_producturl + "\" />" + "<input type=\"hidden\" name=\"pay_md5sign\" value=\"" + pay_md5sign + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public void Return()
        {
            string memberid = HttpContext.Current.Request["memberid"].ToString().Trim();
            string orderid = HttpContext.Current.Request["orderid"].ToString().Trim();
            string amount = HttpContext.Current.Request["amount"].ToString().Trim();
            string transaction_id = HttpContext.Current.Request["transaction_id"].ToString().Trim();
            string datetime = HttpContext.Current.Request["datetime"].ToString().Trim();
            string returncode = HttpContext.Current.Request["returncode"].ToString().Trim();
            string attach = HttpContext.Current.Request["attach"].ToString().Trim();
            string sign = HttpContext.Current.Request["sign"].ToString().Trim();
            string suppKey = base.suppKey;
            string SignTemp = "amount=" + amount + "&datetime=" + datetime + "&memberid=" + memberid + "&orderid=" + orderid + "&returncode=" + returncode + "&transaction_id=" + transaction_id + "&key=" + suppKey;
            string md5sign = UserMd5(SignTemp).ToUpper();
            try
            {
                if (sign.Equals(md5sign))
                {
                    string opstate = "-1";
                    int status = 4;
                    if (returncode.Equals("00"))
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderBank().DoBankComplete(suppId, orderid, transaction_id, status, opstate, string.Empty, Decimal.Parse(amount), new Decimal(0), true, false);
                    HttpContext.Current.Response.Write("ok");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        public void Notify()
        {
            string memberid = HttpContext.Current.Request["memberid"].ToString().Trim();
            string orderid = HttpContext.Current.Request["orderid"].ToString().Trim();
            string amount = HttpContext.Current.Request["amount"].ToString().Trim();
            string transaction_id = HttpContext.Current.Request["transaction_id"].ToString().Trim();
            string datetime = HttpContext.Current.Request["datetime"].ToString().Trim();
            string returncode = HttpContext.Current.Request["returncode"].ToString().Trim();
            string attach = HttpContext.Current.Request["attach"].ToString().Trim();
            string sign = HttpContext.Current.Request["sign"].ToString().Trim();
            string suppKey = base.suppKey;
            string SignTemp = "amount=" + amount + "&datetime=" + datetime + "&memberid=" + memberid + "&orderid=" + orderid + "&returncode=" + returncode + "&transaction_id=" + transaction_id + "&key=" + suppKey;
            string md5sign = UserMd5(SignTemp).ToUpper();
            try
            {
                if (sign.Equals(md5sign))
                {
                    string opstate = "-1";
                    int status = 4;
                    if (returncode.Equals("00"))
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderBank().DoBankComplete(suppId, orderid, transaction_id, status, opstate, string.Empty, Decimal.Parse(amount), new Decimal(0), false, true);
                    HttpContext.Current.Response.Write("ok");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

    }
}


