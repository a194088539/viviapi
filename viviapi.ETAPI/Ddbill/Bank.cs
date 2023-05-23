using System;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.Configuration;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Ddbill
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 10020;
        private bool is_notify_log_open = true;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/ddbill/Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/ddbill/bank_notify.aspx";
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
                    str = "alipay_scan";
                    break;
                case "1004":
                    str = "weixin";
                    break;
                case "1008":
                    str = "qq_scan";
                    break;
                case "1006":
                    str = "fixed_alipay";
                    break;
                case "1007":
                    str = "fixed_wxpay";
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
                    str1 = this._suppInfo.jumpUrl + "/switch/ddbill.aspx";
                string input_charset1 = "UTF-8";
                string interface_version1 = "V3.3";
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
                    string merchant_private_key = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DdbillPrivateKey");
                    //string merchant_private_key = "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAKB0Kde8zAzd9OFvu1/NlU/Gg/eObQiSi8U4qIy+m5KUGP2OCSv0q9V4ILOIHgUXNWlYh5lu99qUePmq+UXJORitW3YsZFE5nCKlTH4dLamxzPKRDrDqmrT2/9GCiX5oA9fLEsEKa1I4EppMbIIJ8JqdVXj+E3jvZRmU5YUCoOMRAgMBAAECgYBytlnr2Rhw4oZeXckyFwJ2hFB4viSJimvO1lD6PpNu2d579/3qpQPsm/OVJu1/ajZPWHGpakJczOUoaenI0LG2JpO4eo70xs/VOpXTGSkUm2QuZUAHpl8tFGrcKD7IIX8wdLIyxVaDTr9F/fPiX50MWd77b0V2jLrkBDFbiEbXgQJBANXZflE/D9pu364OAVRSvYuT9bmU+43lmwHKyg8Jb4rKVr5HyrQ/n50vYwAqf2PF8DzB6f3KPLPoPp9qpGnJb5kCQQDAFGcWDyF0Ie3yc7puuUGDSdE406X2h7uwH5jWGvyrEiHh6NSHfZ46qs3L50QebXoSiQ+d/N0QLIXQ/EAP7Zo5AkEA0b3NzLYDQIQ6UqZd22yDh6CJA4oB57xo+asB3xmsEv49ccdMItm8HRjbCtCjvSHobE7sxwRR4UpKEWUw+KifEQJANy9lcqwEM4ZwA8GGJbup+9tgdhAw1YSnwvFBCvqT7151R5+KOCc6J6bdG6ElLAzODrc8Omrk5Hm2NJXUnf7o2QJBALx3ldWSllfmYBg7pB834bELQpwhlo62DALKVuu1jK/M2NYGJMHXf1Oa7WbEHw4o8gozYTryv0hLXAeFWbE92AQ=";

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
                    //rsaWithH.Init(merPubKeyDir, password, "D:/ddbillRSAKeyVersion");//初始化
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
                //获取智付反馈信息
                HttpContext.Current.Response.Charset = "utf-8";
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
                string orginal_money = HttpContext.Current.Request.Form["orginal_money"];

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
                signStr = signStr + "interface_version=" + interface_version + "&";
                signStr = signStr + "merchant_code=" + merchant_code + "&";


                if (null != notify_id && notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
                }

                signStr = signStr + "order_amount=" + order_amount + "&";
                signStr = signStr + "order_no=" + order_no + "&";
                signStr = signStr + "order_time=" + order_time + "&";
                signStr = signStr + "orginal_money=" + orginal_money + "&";
                signStr = signStr + "trade_no=" + trade_no + "&";
                signStr = signStr + "trade_status=" + trade_status + "&";

                if (null != trade_time && trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time;
                }

                if (sign_type == "RSA-S") //RSA-S的验签方法
                {
                    string ddbill_public_key = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DdbillPublicKey");
                    //string ddbill_public_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDJtiq+2wr3plBYHYYP+VGfXnatLiXOjlKVx/gQgXXwr7/DYdPY2jCXUZB2pKix94PpJ8JUIeVKxHjBTVdmb0VoGTmZ9Cc4b/0JA2WqEAxkFTNO64MVgRSzL5AeH1ZYSaxT1Z5KKFCkztZZd1qZbyzGIaR/x8GZY7Xa0XLue4vCewIDAQAB";

                    //将多得宝公钥转换成C#专用格式
                    ddbill_public_key = HttpHelp.RSAPublicKeyJava2DotNet(ddbill_public_key);

                    //验签
                    bool result = HttpHelp.ValidateRsaSign(signStr, ddbill_public_key, dinpaysign);

                    if (result == true)
                    {
                        string opstate = "1";
                        int status = 4;
                        if (trade_status == "SUCCESS")
                        {
                            opstate = "0";
                            status = 2;
                        }
                        new OrderBank().DoBankComplete(Bank.suppId, order_no, trade_no, status, opstate, string.Empty, Decimal.Parse(order_amount), new Decimal(0), true, true);
                        HttpContext.Current.Response.Write("验签成功");
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
                //获取智付反馈信息
                HttpContext.Current.Response.Charset = "utf-8";
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
                string orginal_money = HttpContext.Current.Request.Form["orginal_money"];

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
                signStr = signStr + "interface_version=" + interface_version + "&";
                signStr = signStr + "merchant_code=" + merchant_code + "&";


                if (null != notify_id && notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
                }

                signStr = signStr + "order_amount=" + order_amount + "&";
                signStr = signStr + "order_no=" + order_no + "&";
                signStr = signStr + "order_time=" + order_time + "&";
                signStr = signStr + "orginal_money=" + orginal_money + "&";
                signStr = signStr + "trade_no=" + trade_no + "&";
                signStr = signStr + "trade_status=" + trade_status + "&";

                if (null != trade_time && trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time;
                }

                if (sign_type == "RSA-S") //RSA-S的验签方法
                {
                    string ddbill_public_key = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DdbillPublicKey");
                    //string ddbill_public_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDJtiq+2wr3plBYHYYP+VGfXnatLiXOjlKVx/gQgXXwr7/DYdPY2jCXUZB2pKix94PpJ8JUIeVKxHjBTVdmb0VoGTmZ9Cc4b/0JA2WqEAxkFTNO64MVgRSzL5AeH1ZYSaxT1Z5KKFCkztZZd1qZbyzGIaR/x8GZY7Xa0XLue4vCewIDAQAB";

                    //将多得宝公钥转换成C#专用格式
                    ddbill_public_key = HttpHelp.RSAPublicKeyJava2DotNet(ddbill_public_key);

                    //验签
                    bool result = HttpHelp.ValidateRsaSign(signStr, ddbill_public_key, dinpaysign);

                    if (result == true)
                    {
                        string opstate = "1";
                        int status = 4;
                        if (trade_status == "SUCCESS")
                        {
                            opstate = "0";
                            status = 2;
                        }
                        new OrderBank().DoBankComplete(Bank.suppId, order_no, trade_no, status, opstate, string.Empty, Decimal.Parse(order_amount), new Decimal(0), true, true);
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
                    str = "BOC";  //中国银行
                    break;
                case "977":
                    str = "SPDB";   //浦发银行
                    break;
                case "981":
                    str = "BOCOM";  //交通银行
                    break;
                case "980":
                    str = "CMBC";   //民生银行
                    break;
                case "985":
                    str = "GDB";    //广发银行
                    break;
                case "962":
                    str = "CITIC";   //中信银行
                    break;
                case "982":
                    str = "HXBC";    //华夏银行
                    break;
                case "972":
                    str = "CIB";    //兴业银行
                    break;
                case "976":
                    str = "SRCB";   //上海农商银行
                    break;
                case "989":
                    str = "BOBJ";   //北京银行
                    break;
                case "986":
                    str = "CEB";    //光大银行
                    break;
                case "978":
                    str = "PAB";    //平安银行
                    break;
                case "975":
                    str = "BOS";    //上海银行
                    break;
                case "971":
                    str = "PSBC";   //邮政银行
                    break;
                case "974":
                    str = "SDB";   //深圳发展银行
                    break;
                case "998":
                    str = "NBCB";   //宁波银行
                    break;
                case "979":
                    str = "NJCB";   //南京银行
                    break;
                case "987":
                    str = "BEA";   //东亚银行
                    break;
                case "988":
                    str = "CBHB";   //渤海银行
                    break;
                case "1006":
                    str = "FIXED_ALI";   //手机支付宝
                    break;
                case "1007":
                    str = "FIXED_WX";   //手机微信
                    break;
            }
            return str;
        }
    }
}

