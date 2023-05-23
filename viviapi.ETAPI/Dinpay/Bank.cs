using System;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.Configuration;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Dinpay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10015;
        private bool is_notify_log_open = true;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/dinpay/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/dinpay/bank_notify.aspx";
            }
        }

        public Bank()
            : base(Bank.suppId)
        {
        }

        public string GetPayType(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "":
                    str = "b2c";
                    break;
                case "992":
                    str = "alipay";
                    break;
                case "1004":
                    str = "weixin";
                    break;
                case "1008":
                    str = "qq_scan";
                    break;
            }
            return str;
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            try
            {
                string str1 = this.postBankUrl;
                if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                    str1 = this._suppInfo.jumpUrl + "/switch/dinpay.aspx";
                string input_charset1 = "UTF-8";
                string interface_version1 = "V3.0";
                string merchant_code1 = this.suppAccount;
                string notify_url1 = this.notifyUrl;
                string order_amount1 = orderAmt.ToString("f2");
                string order_no1 = orderid;
                string order_time1 = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sign_type1 = "RSA-S";
                string product_code1 = "";
                string product_desc1 = "";
                string product_name1 = "wwc";
                string product_num1 = "";
                string return_url1 = this.returnurl;
                string service_type1 = "direct_pay";
                string show_url1 = "";
                string extend_param1 = "";
                string extra_return_param1 = "";
                string bank_code1 = this.GetBankCode(bankcode);
                string client_ip1 = "127.0.0.1";
                string client_ip_check1 = "0";
                string redo_flag1 = "1";
                string pay_type1 = this.GetPayType(bankcode);

                ////////////////组装签名参数//////////////////

                string signSrc = "";

                //组织订单信息
                if (bank_code1 != "")
                {
                    signSrc = signSrc + "bank_code=" + bank_code1 + "&";
                }
                if (client_ip1 != "")
                {
                    signSrc = signSrc + "client_ip=" + client_ip1 + "&";
                }
                if (client_ip_check1 != "")
                {
                    signSrc = signSrc + "client_ip_check=" + client_ip_check1 + "&";
                }
                if (extend_param1 != "")
                {
                    signSrc = signSrc + "extend_param=" + extend_param1 + "&";
                }
                if (extra_return_param1 != "")
                {
                    signSrc = signSrc + "extra_return_param=" + extra_return_param1 + "&";
                }
                if (input_charset1 != "")
                {
                    signSrc = signSrc + "input_charset=" + input_charset1 + "&";
                }
                if (interface_version1 != "")
                {
                    signSrc = signSrc + "interface_version=" + interface_version1 + "&";
                }
                if (merchant_code1 != "")
                {
                    signSrc = signSrc + "merchant_code=" + merchant_code1 + "&";
                }
                if (notify_url1 != "")
                {
                    signSrc = signSrc + "notify_url=" + notify_url1 + "&";
                }
                if (order_amount1 != "")
                {
                    signSrc = signSrc + "order_amount=" + order_amount1 + "&";
                }
                if (order_no1 != "")
                {
                    signSrc = signSrc + "order_no=" + order_no1 + "&";
                }
                if (order_time1 != "")
                {
                    signSrc = signSrc + "order_time=" + order_time1 + "&";
                }
                if (pay_type1 != "")
                {
                    signSrc = signSrc + "pay_type=" + pay_type1 + "&";
                }
                if (product_code1 != "")
                {
                    signSrc = signSrc + "product_code=" + product_code1 + "&";
                }
                if (product_desc1 != "")
                {
                    signSrc = signSrc + "product_desc=" + product_desc1 + "&";
                }
                if (product_name1 != "")
                {
                    signSrc = signSrc + "product_name=" + product_name1 + "&";
                }
                if (product_num1 != "")
                {
                    signSrc = signSrc + "product_num=" + product_num1 + "&";
                }
                if (redo_flag1 != "")
                {
                    signSrc = signSrc + "redo_flag=" + redo_flag1 + "&";
                }
                if (return_url1 != "")
                {
                    signSrc = signSrc + "return_url=" + return_url1 + "&";
                }
                if (service_type1 != "")
                {
                    signSrc = signSrc + "service_type=" + service_type1;
                }
                if (show_url1 != "")
                {
                    signSrc = signSrc + "&show_url=" + show_url1;
                }

                string signData = "";
                if (sign_type1 == "RSA-S")//RSA-S签名方法
                {
                    /**  merchant_private_key，商户私钥，商户按照《密钥对获取工具说明》操作并获取商户私钥。获取商户私钥的同时，也要
						获取商户公钥（merchant_public_key）并且将商户公钥上传到智付商家后台"公钥管理"（如何获取和上传请看《密钥对获取工具说明》），
						不上传商户公钥会导致调试的时候报错“签名错误”。
				   */
                    //demo提供的merchant_private_key是测试商户号1111110166的商户私钥，请自行获取商户私钥并且替换。

                    string merchant_private_key = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DinpayPrivateKey");
                    //私钥转换成C#专用私钥
                    merchant_private_key = HttpHelp.RSAPrivateKeyJava2DotNet(merchant_private_key);
                    //签名
                    signData = HttpHelp.RSASign(signSrc, merchant_private_key);

                }
                else  //RSA签名方法
                {
                    //RSAWithHardware rsa = new RSAWithHardware();
                    //string merPubKeyDir = "D:/1111110166.pfx";   //证书路径
                    //string password = "87654321";                //证书密码
                    //RSAWithHardware rsaWithH = new RSAWithHardware();
                    //rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");//初始化
                    //string signData = rsaWithH.Sign(signSrc);    //签名
                    //sign.Value = signData;
                }


                string postForm = "<form name=\"pay\" id=\"pay\" method=\"post\" action=\"" + str1 + "\">";

                postForm += "<input type=\"hidden\" name=\"sign\" value=\"" + signData + "\" />";
                postForm += "<input type=\"hidden\" name=\"merchant_code\" value=\"" + merchant_code1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"bank_code\" value=\"" + bank_code1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"order_no\" value=\"" + order_no1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"order_amount\" value=\"" + order_amount1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"service_type\" value=\"" + service_type1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"input_charset\" value=\"" + input_charset1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"notify_url\" value=\"" + notify_url1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"interface_version\" value=\"" + interface_version1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"order_time\" value=\"" + order_time1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"product_name\" value=\"" + product_name1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"client_ip_check\" value=\"" + client_ip_check1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"client_ip\" value=\"" + client_ip1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"extend_param\" value=\"" + extend_param1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"extra_return_param\" value=\"" + extra_return_param1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"product_code\" value=\"" + product_code1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"product_desc\" value=\"" + product_desc1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"product_num\" value=\"" + product_num1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"return_url\" value=\"" + return_url1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"show_url\" value=\"" + show_url1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"redo_flag\" value=\"" + redo_flag1 + "\" />";
                postForm += "<input type=\"hidden\" name=\"pay_type\" value=\"" + pay_type1 + "\" />";


                postForm += "</form>";

                //自动提交该表单到测试网关
                postForm += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('pay').submit();\",100);</script>";

                return postForm;
            }
            finally
            {

            }
        }

        private string GetValue(string key)
        {
            string str = HttpContext.Current.Request.QueryString[key];
            if (!string.IsNullOrEmpty(str))
                return str.Trim();
            else
                return "";
        }

        public void ReturnBank()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                //获取智付反馈信息
                string merchant_code = HttpContext.Current.Request.Form["merchant_code"].ToString().Trim();
                string notify_type = HttpContext.Current.Request.Form["notify_type"].ToString().Trim();
                string notify_id = HttpContext.Current.Request.Form["notify_id"].ToString().Trim();
                string interface_version = HttpContext.Current.Request.Form["interface_version"].ToString().Trim();
                string sign_type = HttpContext.Current.Request.Form["sign_type"].ToString().Trim();
                string dinpaysign = HttpContext.Current.Request.Form["sign"].ToString().Trim();
                string order_no = HttpContext.Current.Request.Form["order_no"].ToString().Trim();
                string order_time = HttpContext.Current.Request.Form["order_time"].ToString().Trim();
                string order_amount = HttpContext.Current.Request.Form["order_amount"].ToString().Trim();
                string extra_return_param = HttpContext.Current.Request.Form["extra_return_param"];
                string trade_no = HttpContext.Current.Request.Form["trade_no"].ToString().Trim();
                string trade_time = HttpContext.Current.Request.Form["trade_time"].ToString().Trim();
                string trade_status = HttpContext.Current.Request.Form["trade_status"].ToString().Trim();
                string bank_seq_no = HttpContext.Current.Request.Form["bank_seq_no"];

                /**
                 *签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，
                *同时将商家支付密钥key放在最后参与签名，组成规则如下：
                *参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n&key=key值
                **/
                //组织订单信息
                string signStr = "";

                if (null != bank_seq_no && bank_seq_no != "")
                {
                    signStr = signStr + "bank_seq_no=" + bank_seq_no.ToString().Trim() + "&";
                }

                if (null != extra_return_param && extra_return_param != "")
                {
                    signStr = signStr + "extra_return_param=" + extra_return_param + "&";
                }
                signStr = signStr + "interface_version=V3.0" + "&";
                signStr = signStr + "merchant_code=" + merchant_code + "&";


                if (null != notify_id && notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
                }

                signStr = signStr + "order_amount=" + order_amount + "&";
                signStr = signStr + "order_no=" + order_no + "&";
                signStr = signStr + "order_time=" + order_time + "&";
                signStr = signStr + "trade_no=" + trade_no + "&";
                signStr = signStr + "trade_status=" + trade_status + "&";

                if (null != trade_time && trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time;
                }

                if (sign_type == "RSA-S") //RSA-S的验签方法
                {

                    /**
                    1)dinpay_public_key，智付公钥，每个商家对应一个固定的智付公钥（不是使用工具生成的密钥merchant_public_key，不要混淆），
                    即为智付商家后台"公钥管理"->"智付公钥"里的绿色字符串内容
                    2)demo提供的dinpay_public_key是测试商户号1111110166的智付公钥，请自行复制对应商户号的智付公钥进行调整和替换。
                    */

                    string dinpay_public_key = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DinpayPublicKey");
                    //将智付公钥转换成C#专用格式
                    dinpay_public_key = HttpHelp.RSAPublicKeyJava2DotNet(dinpay_public_key);
                    //验签
                    bool result = HttpHelp.ValidateRsaSign(signStr, dinpay_public_key, dinpaysign);
                    if (result == true)
                    {

                        OrderBank orderBank = new OrderBank();
                        int status = 4;
                        string opstate = "1";
                        if (trade_status == "SUCCESS")
                        {
                            status = 2;
                            opstate = "0";
                        }
                        orderBank.DoBankComplete(Bank.suppId, order_no, trade_no, status, opstate, string.Empty, Decimal.Parse(order_amount), new Decimal(0), false, true);
                        HttpContext.Current.Response.Write("SUCCESS");

                    }
                    else
                    {
                        //验签失败
                        HttpContext.Current.Response.Write("验签失败");
                    }

                }


            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public void Notify()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                //获取智付反馈信息
                string merchant_code = HttpContext.Current.Request.Form["merchant_code"].ToString().Trim();
                string notify_type = HttpContext.Current.Request.Form["notify_type"].ToString().Trim();
                string notify_id = HttpContext.Current.Request.Form["notify_id"].ToString().Trim();
                string interface_version = HttpContext.Current.Request.Form["interface_version"].ToString().Trim();
                string sign_type = HttpContext.Current.Request.Form["sign_type"].ToString().Trim();
                string dinpaysign = HttpContext.Current.Request.Form["sign"].ToString().Trim();
                string order_no = HttpContext.Current.Request.Form["order_no"].ToString().Trim();
                string order_time = HttpContext.Current.Request.Form["order_time"].ToString().Trim();
                string order_amount = HttpContext.Current.Request.Form["order_amount"].ToString().Trim();
                string extra_return_param = HttpContext.Current.Request.Form["extra_return_param"];
                string trade_no = HttpContext.Current.Request.Form["trade_no"].ToString().Trim();
                string trade_time = HttpContext.Current.Request.Form["trade_time"].ToString().Trim();
                string trade_status = HttpContext.Current.Request.Form["trade_status"].ToString().Trim();
                string bank_seq_no = HttpContext.Current.Request.Form["bank_seq_no"];

                /**
                 *签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，
                *参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n
                **/
                //组织订单信息
                string signStr = "";

                if (null != bank_seq_no && bank_seq_no != "")
                {
                    signStr = signStr + "bank_seq_no=" + bank_seq_no.ToString().Trim() + "&";
                }

                if (null != extra_return_param && extra_return_param != "")
                {
                    signStr = signStr + "extra_return_param=" + extra_return_param + "&";
                }
                signStr = signStr + "interface_version=V3.0" + "&";
                signStr = signStr + "merchant_code=" + merchant_code + "&";


                if (null != notify_id && notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
                }

                signStr = signStr + "order_amount=" + order_amount + "&";
                signStr = signStr + "order_no=" + order_no + "&";
                signStr = signStr + "order_time=" + order_time + "&";
                signStr = signStr + "trade_no=" + trade_no + "&";
                signStr = signStr + "trade_status=" + trade_status + "&";

                if (null != trade_time && trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time;
                }

                if (sign_type == "RSA-S") //RSA-S的验签方法
                {

                    /**
                    1)dinpay_public_key，智付公钥，每个商家对应一个固定的智付公钥（不是使用工具生成的密钥merchant_public_key，不要混淆），
                    即为智付商家后台"公钥管理"->"智付公钥"里的绿色字符串内容
                    2)demo提供的dinpay_public_key是测试商户号1111110166的智付公钥，请自行复制对应商户号的智付公钥进行调整和替换。
                    */

                    string dinpay_public_key = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DinpayPublicKey");
                    //将智付公钥转换成C#专用格式
                    dinpay_public_key = HttpHelp.RSAPublicKeyJava2DotNet(dinpay_public_key);
                    //验签
                    bool result = HttpHelp.ValidateRsaSign(signStr, dinpay_public_key, dinpaysign);
                    if (result == true)
                    {

                        OrderBank orderBank = new OrderBank();
                        int status = 4;
                        string opstate = "1";
                        if (trade_status == "SUCCESS")
                        {
                            status = 2;
                            opstate = "0";
                        }
                        orderBank.DoBankComplete(Bank.suppId, order_no, trade_no, status, opstate, string.Empty, Decimal.Parse(order_amount), new Decimal(0), true, false);
                        HttpContext.Current.Response.Write("SUCCESS");

                    }
                    else
                    {
                        //验签失败
                        HttpContext.Current.Response.Write("验签失败");
                    }

                }


            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "CMB";
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
                case "977":
                    str = "SPDB";
                    break;
                case "981":
                    str = "BCOM";
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
                    str = "PSBC";
                    break;
                case "986":
                    str = "CEBB";
                    break;
                case "987":
                    str = "BEA";
                    break;
                case "978":
                    str = "SPABANK";
                    break;
            }
            return str;
        }
    }
}
