using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI._19pay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10035;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/19pay/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/19pay/bank_Notify.aspx";
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
            string str = string.Empty;
            switch (paymodeId)
            {
                default:
                    str = "907";        //网银支付
                    break;
                case "1000":
                    str = "912";        //快捷支付
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
                case "1006":
                    str = "904";        //支付宝手机
                    break;
                case "1009":
                    str = "905";        //QQ手机
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string payUrl = "https://www.19pay.fun/Pay_Index.html";
            string md5key = this.suppKey;
            string pay_memberid = this.suppAccount;                                //商户号
            string pay_orderid = orderid;                                         //订单号
            string pay_applydate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //提交时间
            string pay_bankcode = this.GetBankCode(bankcode);                      //银行编码
            string pay_notifyurl = this.notifyUrl;                                 //服务端通知
            string pay_callbackurl = this.returnurl;                               //页面跳转通知
            string pay_amount = orderAmt.ToString("f2");                           //订单金额
            string pay_attach = "CZ";                                              //附加字段
            string pay_productname = "话费充值";                                    //商品名称
            string pay_productnum = "";                                            //商户品数量
            string pay_productdesc = "";                                           //商品描述
            string pay_producturl = "";                                            //商户链接地址
            string stringSignTemp = "pay_amount=" + pay_amount + "&pay_applydate=" + pay_applydate + "&pay_bankcode=" + pay_bankcode + "&pay_callbackurl=" + pay_callbackurl + "&pay_memberid=" + pay_memberid + "&pay_notifyurl=" + pay_notifyurl + "&pay_orderid=" + pay_orderid + "&key=" + md5key + "";
            string pay_md5sign = UserMd5(stringSignTemp).ToUpper();
            //LogHelper.Write("stringSignTemp=" + stringSignTemp);
            //LogHelper.Write("pay_md5sign=" + pay_md5sign);
            string str6 = string.Empty;
            string s = "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + payUrl + "\">" + "<input type=\"hidden\" name=\"pay_memberid\" value=\"" + pay_memberid + "\" />" + "<input type=\"hidden\" name=\"pay_orderid\" value=\"" + pay_orderid + "\" />" + "<input type=\"hidden\" name=\"pay_applydate\" value=\"" + pay_applydate + "\" />" + "<input type=\"hidden\" name=\"pay_bankcode\" value=\"" + pay_bankcode + "\" />" + "<input type=\"hidden\" name=\"pay_notifyurl\" value=\"" + pay_notifyurl + "\" />" + "<input type=\"hidden\" name=\"pay_callbackurl\" value=\"" + pay_callbackurl + "\" />" + "<input type=\"hidden\" name=\"pay_amount\" value=\"" + pay_amount + "\" />" + "<input type=\"hidden\" name=\"pay_attach\" value=\"" + pay_attach + "\" />" + "<input type=\"hidden\" name=\"pay_productname\" value=\"" + pay_productname + "\" />" + "<input type=\"hidden\" name=\"pay_productnum\" value=\"" + pay_productnum + "\" />" + "<input type=\"hidden\" name=\"pay_productdesc\" value=\"" + pay_productdesc + "\" />" + "<input type=\"hidden\" name=\"pay_producturl\" value=\"" + pay_producturl + "\" />" + "<input type=\"hidden\" name=\"pay_md5sign\" value=\"" + pay_md5sign + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            HttpContext.Current.Response.Write(s);
            return s;
        }

        public void Return()
        {
            string memberid = HttpContext.Current.Request["memberid"];
            string orderid = HttpContext.Current.Request["orderid"];
            string amount = HttpContext.Current.Request["amount"];
            string datetime = HttpContext.Current.Request["datetime"];
            string returncode = HttpContext.Current.Request["returncode"];
            string transaction_id = HttpContext.Current.Request["transaction_id"];
            string attach = HttpContext.Current.Request["attach"];
            string sign = HttpContext.Current.Request["sign"];
            string keyValue = this.suppKey;
            string SignTemp = "amount=" + amount + "&datetime=" + datetime + "&memberid=" + memberid + "&orderid=" + orderid + "&returncode=" + returncode + "&transaction_id=" + transaction_id + "&key=" + keyValue + "";
            string md5sign = UserMd5(SignTemp).ToUpper();
            try
            {
                if (sign.Equals(md5sign))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (returncode.Equals("00"))
                {
                    opstate = "0";
                    status = 2;
                }
                if (new OrderBank().DoBankComplete(Bank.suppId, orderid, transaction_id, status, opstate, string.Empty, Decimal.Parse(amount), new Decimal(0), false, true))
                    HttpContext.Current.Response.Write("ok");
                else
                    HttpContext.Current.Response.Write("error");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public void Notify()
        {
            string memberid = HttpContext.Current.Request["memberid"];
            string orderid = HttpContext.Current.Request["orderid"];
            string amount = HttpContext.Current.Request["amount"];
            string datetime = HttpContext.Current.Request["datetime"];
            string returncode = HttpContext.Current.Request["returncode"];
            string transaction_id = HttpContext.Current.Request["transaction_id"];
            string attach = HttpContext.Current.Request["attach"];
            string sign = HttpContext.Current.Request["sign"];
            string keyValue = this.suppKey;
            string SignTemp = "amount=" + amount + "&datetime=" + datetime + "&memberid=" + memberid + "&orderid=" + orderid + "&returncode=" + returncode + "&transaction_id=" + transaction_id + "&key=" + keyValue + "";
            string md5sign = UserMd5(SignTemp).ToUpper();
            try
            {
                if (sign.Equals(md5sign))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (returncode.Equals("00"))
                {
                    opstate = "0";
                    status = 2;
                }
                if (new OrderBank().DoBankComplete(Bank.suppId, orderid, transaction_id, status, opstate, string.Empty, Decimal.Parse(amount), new Decimal(0), true, false))
                    HttpContext.Current.Response.Write("ok");
                else
                    HttpContext.Current.Response.Write("error");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

    }
}


