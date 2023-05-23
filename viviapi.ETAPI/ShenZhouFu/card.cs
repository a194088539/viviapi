using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.ETAPI.ShenZhouFu
{
    public class card : ETAPIBase
    {
        private static int suppId = 86;
        public string notify_url = RuntimeSetting.SiteDomain + "/notify/shenzhoufu/card.aspx";

        public card()
          : base(card.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errormsg)
        {
            errormsg = string.Empty;
            string str1 = "3";
            string suppAccount = this.suppAccount;
            string str2 = (cardvalue * 100).ToString();
            string str3 = _orderid;
            string str4 = this.notify_url;
            string str5 = this.Encrypt(cardvalue.ToString() + "@" + _cardno + "@" + _cardpwd, this._suppInfo.puserkey1);
            string str6 = string.Empty;
            string str7 = string.Empty;
            string str8 = string.Empty;
            string str9 = "1";
            string cardType = this.GetCardType(_typeId);
            string str10 = card.md5sign(str1 + suppAccount + str2 + str3 + str4 + str5 + str8 + str9 + this.suppKey).ToLower();
            string str11 = string.Empty;
            try
            {
                string @string = WebClientHelper.GetString(this.postCardUrl + "?charset=GBK&version=" + str1 + "&merId=" + suppAccount + "&payMoney=" + str2 + "&orderId=" + str3 + "&returnUrl=" + str4 + "&cardInfo=" + HttpUtility.UrlEncode(str5, Encoding.GetEncoding("utf-8")) + "&merUserName=" + str6 + "&merUserMail=" + str7 + "&privateField=" + str8 + "&verifyType=" + str9 + "&cardTypeCombine=" + cardType + "&md5String=" + str10 + "&signString=", (NameValueCollection)null, "get", Encoding.GetEncoding("utf-8"));
                if (!string.IsNullOrEmpty(@string))
                {
                    if (@string != "200")
                    {
                        str11 = "-1";
                        errormsg = this.GetMessageByCode(@string);
                    }
                    else
                        str11 = "0";
                }
                return str11;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return string.Empty;
        }

        public string GetMessageByCode(string code)
        {
            string str = string.Empty;
            return !(code == "101") ? (!(code == "102") ? (!(code == "103") ? (!(code == "104") ? (!(code == "105") ? (!(code == "106") ? (!(code == "107") ? (!(code == "109") ? (!(code == "201") ? (!(code == "501") ? (!(code == "502") ? (!(code == "200") ? (!(code == "902") ? (!(code == "903") ? (!(code == "904") ? (!(code == "905") ? (!(code == "906") ? (!(code == "907") ? (!(code == "908") ? (!(code == "909") ? (!(code == "910") ? (!(code == "911") ? (!(code == "912") ? (!(code == "913") ? (!(code == "914") ? (!(code == "915") ? (!(code == "916") ? (!(code == "917") ? (!(code == "0") ? code : "网络连接失败") : "参数格式不正确") : "商户不支持该充值卡") : "卡面额非法") : "支付金额非法") : "该地方卡暂时不支持") : "非法订单") : "订单号不符合规范") : "服务器返回地址不符合规范") : "该订单不符合重复支付的条件") : "该笔订单已经处理完") : "商户没有设置DES密钥") : "商户没有设置密钥") : "商户没有使用该接口的权限") : "商户没有激活") : "商户 ID 不存在") : "商户参数不全") : "提交成功") : "插入数据库失败") : "插入数据库失败") : "证书验证失败") : "des 解密失败") : "卡内余额不足") : "系统繁忙，暂停提交") : "密码正在处理,请勿重复提交") : "卡号密码有误或已经使用过") : "恶意用户") : "订单号重复") : "md5 验证失败";
        }

        public string GetMessageByCode2(string code)
        {
            string str = string.Empty;
            return !(code == "200") ? (!(code == "201") ? (!(code == "202") ? (!(code == "203") ? (!(code == "204") ? (!(code == "205") ? (!(code == "206") ? (!(code == "207") ? (!(code == "208") ? (!(code == "209") ? (!(code == "210") ? (!(code == "211") ? (!(code == "212") ? (!(code == "213") ? (!(code == "214") ? (!(code == "215") ? (!(code == "216") ? (!(code == "217") ? (!(code == "218") ? (!(code == "219") ? (!(code == "220") ? (!(code == "221") ? code : "本卡之前被处理完,请勿重复提交") : "未知错误") : "系统忙，请稍后再试") : "运营商系统维护") : "接口维护") : "系统维护") : "新生卡") : "该卡为增值业务卡，系统不支持") : "该卡为特殊本地业务卡，系统不支持") : "您选择的卡面额不正确") : "充值卡已过期") : "充值卡已经作废（能查到有该卡，但是没卡的信息）") : "充值卡未激活") : "充值卡卡号错误") : "暂不支持该充值卡") : "本卡之前被提交过,请勿重复提交") : "卡号密码不匹配或被禁止") : "卡号或密码错误次数过多") : "充值卡密码非法") : "卡已被使用") : "卡密有误或余额不足") : "支付成功";
        }

        public void Notify()
        {
            try
            {
                int status = 4;
                string opstate = "-1";
                string str1 = HttpContext.Current.Request.Params.Get("version");
                string str2 = HttpContext.Current.Request.Params.Get("merId");
                string s = HttpContext.Current.Request.Params.Get("payMoney");
                string str3 = HttpContext.Current.Request.Params.Get("orderId");
                string str4 = HttpContext.Current.Request.Params.Get("payResult");
                string str5 = HttpContext.Current.Request.Params.Get("privateField");
                string str6 = HttpContext.Current.Request.Params.Get("payDetails");
                string str7 = HttpContext.Current.Request.Params.Get("md5String");
                string str8 = HttpContext.Current.Request.Params.Get("errcode");
                HttpContext.Current.Request.Params.Get("signString");
                string suppKey = this.suppKey;
                if (card.md5sign(str1 + str2 + s + str3 + str4 + str5 + str6 + suppKey).Equals(str7))
                {
                    Decimal tranAMT = new Decimal(0);
                    if (str4 == "1")
                    {
                        status = 2;
                        opstate = "0";
                        tranAMT = Decimal.Parse(s) / new Decimal(100);
                    }
                    new OrderCard().ReceiveSuppResult(card.suppId, str3, str3, status, opstate, this.GetMessageByCode2(str8), tranAMT, new Decimal(0), str8);
                    HttpContext.Current.Response.Write(str3);
                    HttpContext.Current.Response.End();
                }
                else
                {
                    HttpContext.Current.Response.Write("MD5验证失败");
                    HttpContext.Current.Response.End();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public bool veriSig(string szfPublicPath, string data, string sign)
        {
            new X509Store(StoreName.Root).Open(OpenFlags.ReadWrite);
            try
            {
                X509Certificate2 x509Certificate2 = new X509Certificate2("c:\\shenzhoufuPay.cer");
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.FromXmlString(x509Certificate2.PublicKey.Key.ToXmlString(false));
                return cryptoServiceProvider.VerifyData(Encoding.UTF8.GetBytes(data), (object)"MD5", Convert.FromBase64String(sign));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public string GetCardType(int _type)
        {
            switch (_type)
            {
                case 103:
                    return "0";
                case 108:
                    return "1";
                case 113:
                    return "2";
                default:
                    return _type.ToString();
            }
        }

        public static string md5sign(string str)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("GBK").GetBytes(str))).Replace("-", "").ToLower();
        }

        public string Decrypt(string strText, string sDecrKey)
        {
            try
            {
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                cryptoServiceProvider.Mode = CipherMode.ECB;
                cryptoServiceProvider.Key = Convert.FromBase64String(sDecrKey);
                cryptoServiceProvider.IV = cryptoServiceProvider.Key;
                byte[] buffer = Convert.FromBase64String(strText);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(buffer, 0, buffer.Length);
                cryptoStream.FlushFinalBlock();
                return new UTF8Encoding().GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string Encrypt(string strText, string strEncrKey)
        {
            try
            {
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                cryptoServiceProvider.Mode = CipherMode.ECB;
                cryptoServiceProvider.Key = Convert.FromBase64String(strEncrKey);
                cryptoServiceProvider.IV = cryptoServiceProvider.Key;
                byte[] bytes = Encoding.UTF8.GetBytes(strText);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return "";
            }
        }

        public string Query(string orderids)
        {
            string str1 = "http://pay3.shenzhoufu.com/query/batch.aspx";
            string str2 = "3";
            string suppAccount = this.suppAccount;
            string str3 = orderids;
            DateTime dateTime = DateTime.Today;
            dateTime = dateTime.AddDays(-7.0);
            string str4 = dateTime.ToString("yyyy-MM-dd");
            string str5 = DateTime.Today.ToString("yyyy-MM-dd");
            string str6 = card.md5sign(str2 + suppAccount + str3 + str4 + str5 + this.suppKey).ToLower();
            string str7 = "0";
            string str8 = string.Empty;
            try
            {
                string postCardUrl = this.postCardUrl;
                str8 = WebClientHelper.GetString(str1 + "?version=" + str2 + "&merId=" + suppAccount + "&orderIds=" + str3 + "&queryBegin=" + str4 + "&queryEnd=" + str5 + "&md5=" + str6 + "&resultFormat=" + str7, (NameValueCollection)null, "get", Encoding.GetEncoding("utf-8"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return str8;
        }

        public bool Finish(string orderid, string cardNo, string payStatus, string payMoney)
        {
            bool flag = false;
            try
            {
                if (payStatus != "2")
                {
                    string opstate = "-1";
                    int status = 4;
                    if (payStatus == "1")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderCard().ReceiveSuppResult(card.suppId, orderid, cardNo, status, opstate, payStatus.ToString(), Convert.ToDecimal(payMoney) / new Decimal(100), new Decimal(0), payStatus.ToString());
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return flag;
        }
    }
}
