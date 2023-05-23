using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.xunyou
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10032;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/xunyou/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/xunyou/bank_notify.aspx";
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

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "CMBCHINA";
                    break;
                case "967":
                    str = "ICBC";
                    break;
                case "964":
                    str = "ABC";
                    break;
                case "965":
                    str = "CCB";
                    break;
                case "963":
                    str = "BOC";
                    break;
                case "981":
                    str = "BOCO";
                    break;
                case "980":
                    str = "CMBC";
                    break;
                case "974":
                    str = "SDB";
                    break;
                case "985":
                    str = "GDB";
                    break;
                case "962":
                    str = "ECITIC";
                    break;
                case "982":
                    str = "HXB";
                    break;
                case "972":
                    str = "CIB";
                    break;
                case "971":
                    str = "POST";
                    break;
                case "989":
                    str = "BCCB";
                    break;
                case "988":
                    str = "CBHB";
                    break;
                case "990":
                    str = "BJRCB";
                    break;
                case "979":
                    str = "NJCB";
                    break;
                case "986":
                    str = "CEB";
                    break;
                case "987":
                    str = "HKBEA";
                    break;
                case "997":
                    str = "NBCB";
                    break;
                case "978":
                    str = "PINGANBANK";
                    break;
                case "968":
                    str = "CZ";
                    break;
                case "975":
                    str = "SHB";
                    break;
                case "977":
                    str = "SPDB";
                    break;
            }
            return str;
        }

        public Bank()
            : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string payUrl = "https://gateway.ioo8.com/b2cPay/initPay";

            string payKey1 = "fa45e79c60c44762af2cf47f16349bb0";	//商户支付Key
            string paySecret1 = "7d135aa46dd14a9baa896cbd1f0a88c5";
            string orderPrice1 = orderAmt.ToString("f2");	        //订单金额，单位：元,保留小数点后两位，测试请在10元以上
            string outTradeNo1 = orderid;	                        //商户支付订单号（长度30以内）
            string productType1 = "50000103";	                    //产品类型,B2C T0:50000103,B2C T1:50000101
            string orderTime1 = DateTime.Now.ToString("yyyyMMddHHmmss");	//下单时间，格式(yyyyMMddHHmmss)
            string productName1 = "Q币";	                            //支付产品名称
            string orderIp1 = "127.0.0.1";	                        //下单IP
            string bankCode1 = this.GetBankCode(bankcode);	        //银行编码点击查看银行编码
            string bankAccountType1 = "PRIVATE_DEBIT_ACCOUNT";	    //支付银行卡类型,对私借记卡:PRIVATE_DEBIT_ACCOUNT;对私贷记卡:PRIVATE_CREDIT_ACCOUNT
            string returnUrl1 = this.returnurl;                     //页面通知地址
            string notifyUrl1 = this.notifyUrl;	                    //后台异步通知地址
            string subPayKey1 = "";	                                //子商户支付Key，大商户时必填
            string remark1 = "购买Q币";	                            //备注
            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("bankAccountType", bankAccountType1);
            waitSign.Add("bankCode", bankCode1);
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

            string result = WebClientHelper.GetString(payUrl, postdata, "POST", Encoding.GetEncoding("UTF-8"), 10000);
            return result;
        }

        public void ReturnBank()
        {
            string payKey2 = HttpContext.Current.Request["payKey"];
            string productName2 = HttpContext.Current.Request["productName"];
            string productType2 = HttpContext.Current.Request["productType"];
            string orderPrice2 = HttpContext.Current.Request["orderPrice"];
            string orderTime2 = HttpContext.Current.Request["orderTime"];
            string outTradeNo2 = HttpContext.Current.Request["outTradeNo"].Trim();
            string tradeStatus2 = HttpContext.Current.Request["tradeStatus"].Trim();
            string trxNo2 = HttpContext.Current.Request["trxNo"].Trim();
            string successTime2 = HttpContext.Current.Request["successTime"];
            string remark2 = HttpContext.Current.Request["remark"];
            string sign2 = HttpContext.Current.Request["sign"];
            string paySecret2 = "7d135aa46dd14a9baa896cbd1f0a88c5";
            SortedDictionary<string, string> waitSign2 = new SortedDictionary<string, string>();
            waitSign2.Add("orderPrice", orderPrice2);
            waitSign2.Add("orderTime", orderTime2);
            waitSign2.Add("outTradeNo", outTradeNo2);
            waitSign2.Add("payKey", payKey2);
            waitSign2.Add("productName", productName2);
            waitSign2.Add("productType", productType2);
            waitSign2.Add("remark", remark2);
            waitSign2.Add("successTime", successTime2);
            waitSign2.Add("tradeStatus", tradeStatus2);
            waitSign2.Add("trxNo", trxNo2);

            string signdata2 = "";
            foreach (var key2 in waitSign2.Keys)
            {
                if (waitSign2[key2].Length > 0)
                {
                    signdata2 += key2 + "=" + waitSign2[key2] + "&";
                }
            }
            signdata2 = signdata2 + "paySecret=" + paySecret2;
            string signed2 = UserMd5(signdata2).ToUpper();

            try
            {
                if (!(signed2.ToUpper() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (tradeStatus2.Equals("SUCCESS"))
                {
                    opstate = "0";
                    status = 2;
                }
                if (new OrderBank().DoBankComplete(Bank.suppId, trxNo2, outTradeNo2, status, opstate, string.Empty, Decimal.Parse(orderPrice2) / 100m, new Decimal(0), false, true))
                    HttpContext.Current.Response.Write("success");
                else
                    HttpContext.Current.Response.Write("fail");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }


        public void Notify()
        {
            string payKey2 = HttpContext.Current.Request["payKey"];
            string productName2 = HttpContext.Current.Request["productName"];
            string productType2 = HttpContext.Current.Request["productType"];
            string orderPrice2 = HttpContext.Current.Request["orderPrice"];
            string orderTime2 = HttpContext.Current.Request["orderTime"];
            string outTradeNo2 = HttpContext.Current.Request["outTradeNo"].Trim();
            string tradeStatus2 = HttpContext.Current.Request["tradeStatus"].Trim();
            string trxNo2 = HttpContext.Current.Request["trxNo"].Trim();
            string successTime2 = HttpContext.Current.Request["successTime"];
            string remark2 = HttpContext.Current.Request["remark"];
            string sign2 = HttpContext.Current.Request["sign"];
            string paySecret2 = "7d135aa46dd14a9baa896cbd1f0a88c5";
            SortedDictionary<string, string> waitSign2 = new SortedDictionary<string, string>();
            waitSign2.Add("orderPrice", orderPrice2);
            waitSign2.Add("orderTime", orderTime2);
            waitSign2.Add("outTradeNo", outTradeNo2);
            waitSign2.Add("payKey", payKey2);
            waitSign2.Add("productName", productName2);
            waitSign2.Add("productType", productType2);
            waitSign2.Add("remark", remark2);
            waitSign2.Add("successTime", successTime2);
            waitSign2.Add("tradeStatus", tradeStatus2);
            waitSign2.Add("trxNo", trxNo2);

            string signdata2 = "";
            foreach (var key2 in waitSign2.Keys)
            {
                if (waitSign2[key2].Length > 0)
                {
                    signdata2 += key2 + "=" + waitSign2[key2] + "&";
                }
            }
            signdata2 = signdata2 + "paySecret=" + paySecret2;
            string signed2 = UserMd5(signdata2).ToUpper();

            try
            {
                if (!(signed2.ToUpper() == sign2))
                    return;
                OrderBank orderBank = new OrderBank();
                string opstate = "-1";
                int status = 4;
                if (tradeStatus2.Equals("SUCCESS"))
                {
                    opstate = "0";
                    status = 2;
                }
                if (new OrderBank().DoBankComplete(Bank.suppId, trxNo2, outTradeNo2, status, opstate, string.Empty, Decimal.Parse(orderPrice2) / 100m, new Decimal(0), true, false))
                    HttpContext.Current.Response.Write("success");
                else
                    HttpContext.Current.Response.Write("fail");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

    }
}