using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI
{
    public class Jiexun : ETAPIBase
    {
        private static int suppId = 88;
        public string _notify_url = RuntimeSetting.SiteDomain + "/notify/Jiexun_notify.aspx";

        public Jiexun()
          : base(Jiexun.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errormsg)
        {
            string str1 = "-1";
            errormsg = string.Empty;
            if (this._suppInfo == null)
            {
                errormsg = "请配置19pay的参数！";
                return str1;
            }
            string str2 = "2.00";
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            string str3 = DateTime.Now.ToString("yyyyMMdd");
            string str4 = _orderid;
            string str5 = cardvalue.ToString() + ".00";
            string pToEncrypt1 = _cardno;
            string pToEncrypt2 = _cardpwd;
            string str6 = "RMB";
            string[] strArray1 = this.getsuppcardinfo(_typeId);
            string str7 = strArray1[0];
            string str8 = strArray1[1];
            string str9 = this._notify_url;
            string str10 = "1";
            string str11 = cardvalue.ToString();
            string s = "";
            string str12 = Cryptography.MD5("version_id=" + str2 + "&merchant_id=" + suppAccount + "&order_date=" + str3 + "&order_id=" + str4 + "&amount=" + str5 + "&currency=" + str6 + "&cardnum1=" + this.Encrypt(pToEncrypt1, this.suppKey) + "&cardnum2=" + this.Encrypt(pToEncrypt2, this.suppKey) + "&pm_id=" + str7 + "&pc_id=" + str8 + "&merchant_key=" + suppKey, "gb2312").ToLower();
            string[] strArray2 = WebClientHelper.GetString(this.postCardUrl + ("?version_id=" + str2 + "&merchant_id=" + suppAccount + "&order_date=" + str3 + "&order_id=" + str4 + "&amount=" + str5 + "&cardnum1=" + this.Encrypt(pToEncrypt1, this.suppKey) + "&cardnum2=" + this.Encrypt(pToEncrypt2, this.suppKey) + "&currency=" + str6 + "&pm_id=" + str7 + "&pc_id=" + str8 + "&returl=&notify_url=" + this._notify_url + "&retmode=" + str10 + "&select_amount=" + str11 + "&order_pdesc=" + HttpContext.Current.Server.UrlEncode(s) + "&user_name=&user_phone=&user_mobile=&user_email=&verifystring=" + str12), (string)null, "GET", Encoding.GetEncoding("gb2312"), 1000).Split('|');
            if (strArray2.Length >= 13)
            {
                string str13 = strArray2[0];
                string str14 = strArray2[1];
                string str15 = strArray2[2];
                string str16 = strArray2[3];
                string str17 = strArray2[4];
                string str18 = strArray2[5];
                string str19 = strArray2[6];
                string str20 = strArray2[7];
                string str21 = strArray2[8];
                string str22 = strArray2[9];
                string str23 = strArray2[10];
                string str24 = strArray2[11];
                string str25 = strArray2[12];
                if (Cryptography.MD5("version_id=" + str13 + "&merchant_id=" + str14 + "&order_date=" + str16 + "&order_id=" + str17 + "&amount=" + str18 + "&currency=" + str19 + "&pay_sq=" + str20 + "&pay_date=" + str21 + "&pc_id=" + str22 + "&result=" + str24 + "&merchant_key=" + suppKey, "gb2312").ToLower() == str15)
                {
                    errormsg = str25;
                    if (str24 == "P")
                        str1 = "0";
                }
                else
                    errormsg = "从接品商返回失败";
            }
            return str1;
        }

        public string[] getsuppcardinfo(int _type)
        {
            string[] strArray = new string[2]
            {
        "",
        ""
            };
            switch (_type)
            {
                case 103:
                    strArray = new string[2]
                    {
            "CMJFK",
            "CMJFK00010001"
                    };
                    break;
                case 108:
                    strArray = new string[2]
                    {
            "LTJFK",
            "LTJFK00020000"
                    };
                    break;
                case 113:
                    strArray = new string[2]
                    {
            "DXJFK",
            "DXJFK00010001"
                    };
                    break;
            }
            return strArray;
        }

        public static string md5sign(string str)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("GBK").GetBytes(str))).Replace("-", "").ToLower();
        }

        private byte[] GetKey(string sKey)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(sKey);
            byte[] numArray = new byte[8];
            for (int index = 0; index < bytes.Length && index < numArray.Length; ++index)
                numArray[index] = bytes[index];
            return numArray;
        }

        public string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
            cryptoServiceProvider.Key = this.GetKey(sKey);
            cryptoServiceProvider.Mode = CipherMode.ECB;
            cryptoServiceProvider.IV = this.GetKey(sKey);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in memoryStream.ToArray())
            {
                string str = Convert.ToString(num, 16);
                if (str.Length < 2)
                    str = "0" + str;
                stringBuilder.Append(str);
            }
            return stringBuilder.ToString().ToLower();
        }

        public void Notify()
        {
            try
            {
                string str1 = HttpContext.Current.Request["version_id"];
                string str2 = HttpContext.Current.Request["merchant_id"];
                string str3 = HttpContext.Current.Request["verifystring"];
                string str4 = HttpContext.Current.Request["order_date"];
                string orderId = HttpContext.Current.Request["order_id"];
                string s = HttpContext.Current.Request["amount"];
                string str5 = HttpContext.Current.Request["currency"];
                string str6 = HttpContext.Current.Request["pay_sq"];
                string str7 = HttpContext.Current.Request["pay_date"];
                string str8 = HttpContext.Current.Request["pc_id1"];
                string str9 = HttpContext.Current.Request["count"];
                string supplierOrderId = HttpContext.Current.Request["card_num1"];
                string str10 = HttpContext.Current.Request["card_pwd1"];
                string msg = HttpContext.Current.Request["card_code1"];
                string str11 = HttpContext.Current.Request["card_status1"];
                string str12 = HttpContext.Current.Request["card_date1"];
                string str13 = HttpContext.Current.Request["r1"];
                string errtype = HttpContext.Current.Request["result"];
                string str14 = FormsAuthentication.HashPasswordForStoringInConfigFile("version_id=" + str1 + "&merchant_id=" + str2 + "&order_id=" + orderId + "&result=" + errtype + "&order_date=" + str4 + "&amount=" + s + "&currency=" + str5 + "&pay_sq=" + str6 + "&pay_date=" + str7 + "&count=" + str9 + "&card_num1=" + supplierOrderId + "&card_pwd1=" + str10 + "&pc_id1=" + str8 + "&card_status1=" + str11 + "&card_code1=" + msg + "&card_date1=" + str12 + "&r1=" + str13 + "&merchant_key=" + this.suppKey, "md5").ToLower();
                if (!str3.Equals(str14))
                    return;
                int status = 4;
                string opstate = "-1";
                if (errtype.Equals("Y"))
                {
                    status = 2;
                    opstate = "0";
                }
                new OrderCard().ReceiveSuppResult(Jiexun.suppId, orderId, supplierOrderId, status, opstate, msg, Decimal.Parse(s), new Decimal(0), errtype);
                HttpContext.Current.Response.Write("Y");
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
