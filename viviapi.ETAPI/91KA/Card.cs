using System;
using System.Security.Cryptography;
using System.Text;
using viviapi.BLL;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI._91KA
{
    public class Card : ETAPIBase
    {
        private static int suppId = 91;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/91ka/return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/91ka/card_callback.aspx";
            }
        }

        public Card()
          : base(Card.suppId)
        {
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string supporderid, out string errmsg)
        {
            supporderid = string.Empty;
            errmsg = string.Empty;
            string str1 = "-1";
            try
            {
                string url = this.postCardUrl;
                if (string.IsNullOrEmpty(url))
                    url = "http://www.91ka.com/auto_interface_third.php";
                string suppAccount = this.suppAccount;
                string str2 = cardvalue.ToString();
                string channelid = this.GetChannelid(_typeId);
                if (string.IsNullOrEmpty(channelid))
                    return string.Empty;
                string str3 = "2";
                string str4 = string.Empty;
                this.Encrypt3DESSZX(_cardno, this.suppKey);
                string str5 = this.Encrypt3DESSZX(_cardpwd, this.suppKey);
                string str6 = cardvalue.ToString();
                string str7 = string.Empty;
                string notifyUrl = this.notifyUrl;
                string str8 = "2.0.1";
                string str9 = string.Empty;
                string str10 = string.Empty;
                if (string.IsNullOrEmpty(this.postBankUrl))
                    ;
                string str11 = Cryptography.MD5(string.Format("orderid={0}&origin={1}&chargemoney={3}&channelid={4}&paytype=&bankcode={5}&cardno=&cardpwd=&cardamount=&fronturl={6}&bgurl={7}&ext1=&ext2={8}", (object)_orderid, (object)suppAccount, (object)str2, (object)channelid, (object)str4, (object)str7, (object)notifyUrl, (object)this.suppKey), "gb2312");
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("&orderid={0}", (object)_orderid);
                stringBuilder.AppendFormat("&origin={0}", (object)suppAccount);
                stringBuilder.AppendFormat("&chargemoney={0}", (object)str2);
                stringBuilder.AppendFormat("&channelid={0}", (object)channelid);
                stringBuilder.AppendFormat("&paytype={0}", (object)str3);
                stringBuilder.AppendFormat("&bankcode={0}", (object)str4);
                stringBuilder.AppendFormat("&cardpwd={0}", (object)str5);
                stringBuilder.AppendFormat("&cardamount={0}", (object)str6);
                stringBuilder.AppendFormat("&fronturl={0}", (object)str7);
                stringBuilder.AppendFormat("&bgurl={0}", (object)notifyUrl);
                stringBuilder.AppendFormat("&version={0}", (object)str8);
                stringBuilder.AppendFormat("&ext1={0}", (object)str9);
                stringBuilder.AppendFormat("&ext2={0}", (object)str10);
                stringBuilder.AppendFormat("&validate={0}", (object)str11);
                string @string = WebClientHelper.GetString(url, stringBuilder.ToString(), "POST", Encoding.GetEncoding("gb2312"), 10000);
                errmsg = @string;
                if (@string == "ERROR0000")
                    str1 = "0";
            }
            catch (Exception ex)
            {
            }
            return str1;
        }

        public void Notify()
        {
            string formString1 = WebBase.GetFormString("orderid", "");
            string formString2 = WebBase.GetFormString("channelid", "");
            string formString3 = WebBase.GetFormString("systemno", "");
            string formString4 = WebBase.GetFormString("payprice", "");
            string formString5 = WebBase.GetFormString("status", "");
            string formString6 = WebBase.GetFormString("ext1", "");
            string formString7 = WebBase.GetFormString("ext2", "");
            if (!(WebBase.GetFormString("validate", "") == Cryptography.MD5(string.Format("orderid={0}&channelid={1}&systemno={2}&payprice={3}&status={4}&ext1={5}&ext2={6}", (object)formString1, (object)formString2, (object)formString3, (object)formString4, (object)formString5, (object)formString6, (object)formString7, (object)this.suppKey), "gb2312")))
                return;
            string msg = formString5;
            string str = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (formString5 == "1")
            {
                str = "支付成功";
                opstate = "0";
                status = 2;
            }
            OrderCard orderCard = new OrderCard();
            string userviewmsg = formString5;
            orderCard.ReceiveSuppResult(Card.suppId, formString1, formString3, status, opstate, msg, userviewmsg, Decimal.Parse(formString4), new Decimal(0), formString5, (byte)1);
        }

        public string Encrypt3DESSZX(string strString, string strKey)
        {
            if (strKey.Length < 24)
            {
                string str = strKey;
                for (int index = 0; index < 24 / strKey.Length; ++index)
                    str += strKey;
                strKey = str;
            }
            strKey = strKey.Substring(0, 24);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider.Key = Encoding.UTF8.GetBytes(strKey);
            cryptoServiceProvider.Mode = CipherMode.ECB;
            cryptoServiceProvider.Padding = PaddingMode.PKCS7;
            ICryptoTransform encryptor = cryptoServiceProvider.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(strString);
            return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public string GetChannelid(int _type)
        {
            switch (_type)
            {
                case 103:
                    return "2";
                case 108:
                    return "4";
                case 113:
                    return "3";
                default:
                    return string.Empty;
            }
        }
    }
}
