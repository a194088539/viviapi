using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;
using viviLib.Web;

namespace viviapi.ETAPI.KuaiQian
{
    public class ShenZhouXing : ETAPIBase
    {
        private static int suppId = 10026;
        private string inputCharset = "3";
        public string bgUrl = RuntimeSetting.SiteDomain + "/notify/KuaiQian_SZX_Return.aspx";
        public string pageUrl = "";
        private string version = "v2.0";
        private string language = "1";
        private string signType = "1";
        private string merchantAcctId = "";
        public string payerName = "";
        private string payerContactType = "1";
        private string payerContact = "";
        public string orderId = "";
        public string orderAmount = "";
        private string payType = "52";
        public string cardNumber = "";
        public string cardPwd = "";
        private string fullAmountFlag = "1";
        private string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string productName = "test";
        private string productNum = "";
        private string productId = "";
        private string productDesc = "test";
        private string ext1 = "";
        private string ext2 = "";
        private string bossType = string.Empty;
        private string key = "";

        private HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        public ShenZhouXing()
          : base(ShenZhouXing.suppId)
        {
        }

        private static string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                    returnStr = returnStr + "&" + paramId + "=" + paramValue;
                return returnStr;
            }
            else
            {
                if (paramValue != "")
                    returnStr = paramId + "=" + paramValue;
                return returnStr;
            }
        }

        private static string GetMD5(string dataStr, string codeType)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(codeType).GetBytes(dataStr));
            StringBuilder stringBuilder = new StringBuilder(32);
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x").PadLeft(2, '0'));
            return ((object)stringBuilder).ToString();
        }

        public string GetPayUrl(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errmsg)
        {
            errmsg = string.Empty;
            string str1 = string.Empty;
            if (_typeId == 103)
            {
                this.bossType = "0";
                this.merchantAcctId = this._suppInfo.puserid1;
                this.key = this._suppInfo.puserkey1;
            }
            else if (_typeId == 108)
            {
                this.bossType = "1";
                this.merchantAcctId = this._suppInfo.puserid2;
                this.key = this._suppInfo.puserkey2;
            }
            else if (_typeId == 113)
            {
                this.bossType = "3";
                this.merchantAcctId = this._suppInfo.puserid3;
                this.key = this._suppInfo.puserkey3;
            }
            else if (_typeId == 106)
            {
                this.bossType = "4";
                this.merchantAcctId = this._suppInfo.puserid4;
                this.key = this._suppInfo.puserkey4;
            }
            this.orderId = _orderid;
            this.orderAmount = (cardvalue * 100).ToString();
            this.cardNumber = _cardno;
            this.cardPwd = _cardpwd;
            this.payerName = HttpUtility.UrlEncode(this.payerName, Encoding.GetEncoding("gb2312")).ToUpper();
            this.productName = HttpUtility.UrlEncode(this.productName, Encoding.GetEncoding("gb2312")).ToUpper();
            this.productDesc = HttpUtility.UrlEncode(this.productDesc, Encoding.GetEncoding("gb2312")).ToUpper();
            this.ext1 = HttpUtility.UrlEncode(this.ext1, Encoding.GetEncoding("gb2312")).ToUpper();
            this.ext2 = HttpUtility.UrlEncode(this.ext2, Encoding.GetEncoding("gb2312")).ToUpper();
            this.payerName = HttpUtility.UrlEncode(this.payerName, Encoding.GetEncoding("gb2312")).ToUpper();
            this.productName = HttpUtility.UrlEncode(this.productName, Encoding.GetEncoding("gb2312")).ToUpper();
            this.productDesc = HttpUtility.UrlEncode(this.productDesc, Encoding.GetEncoding("gb2312")).ToUpper();
            this.ext1 = HttpUtility.UrlEncode("", Encoding.GetEncoding("gb2312")).ToUpper();
            this.ext2 = HttpUtility.UrlEncode(this.ext2, Encoding.GetEncoding("gb2312")).ToUpper();
            string returnStr1 = "";
            string str2 = string.Empty;
            string returnStr2 = ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(returnStr1, "inputCharset", this.inputCharset), "bgUrl", this.bgUrl), "pageUrl", this.pageUrl), "version", this.version), "language", this.language), "signType", this.signType), "merchantAcctId", this.merchantAcctId), "payerName", this.payerName), "payerContactType", this.payerContactType), "payerContact", this.payerContact), "orderId", this.orderId), "orderAmount", this.orderAmount), "payType", this.payType), "cardNumber", this.cardNumber), "cardPwd", this.cardPwd), "fullAmountFlag", this.fullAmountFlag), "orderTime", this.orderTime), "productName", this.productName), "productNum", this.productNum), "productId", this.productId), "productDesc", this.productDesc), "ext1", this.ext1), "ext2", this.ext2), "bossType", this.bossType);
            string str3 = returnStr2;
            string str4 = ShenZhouXing.GetMD5(ShenZhouXing.appendParam(returnStr2, "key", this.key), "gb2312").ToUpper();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("https://www.99bill.com/szxgateway/recvMerchantInfoAction.htm?");
            stringBuilder.Append(str3);
            stringBuilder.Append("&signMsg=" + str4);
            string[] strArray = WebClientHelper.GetString(((object)stringBuilder).ToString(), (NameValueCollection)null, "GET", Encoding.GetEncoding("gb2312")).Split('&');
            if (strArray.Length == 2)
            {
                string str5 = strArray[1].Replace("returncode=", "");
                if (str5 != "120")
                {
                    str1 = "-1";
                }
                else
                {
                    errmsg = str5;
                    str1 = "0";
                }
            }
            return str1;
        }

        public void Notify(bool isReceive)
        {
            string paramValue1 = ((object)this.Request["merchantAcctId"]).ToString().Trim();
            string paramValue2 = "";
            string paramValue3 = ((object)this.Request["bossType"]).ToString().Trim();
            if (paramValue3 != "1" || paramValue3 != "3" || paramValue3 != "4")
                paramValue2 = this._suppInfo.puserkey1;
            if (paramValue3 == "1")
                paramValue2 = this._suppInfo.puserkey2;
            if (paramValue3 == "3")
                paramValue2 = this._suppInfo.puserkey3;
            if (paramValue3 == "4")
                paramValue2 = this._suppInfo.puserkey4;
            string paramValue4 = ((object)this.Request["version"]).ToString().Trim();
            string paramValue5 = ((object)this.Request["language"]).ToString().Trim();
            string paramValue6 = ((object)this.Request["payType"]).ToString().Trim();
            string paramValue7 = ((object)this.Request["cardNumber"]).ToString().Trim();
            string paramValue8 = ((object)this.Request["cardPwd"]).ToString().Trim();
            string str1 = ((object)this.Request["orderId"]).ToString().Trim();
            string paramValue9 = ((object)this.Request["orderAmount"]).ToString().Trim();
            string str2 = ((object)this.Request["dealId"]).ToString().Trim();
            string paramValue10 = ((object)this.Request["orderTime"]).ToString().Trim();
            string paramValue11 = ((object)this.Request["ext1"]).ToString().Trim();
            string paramValue12 = ((object)this.Request["ext2"]).ToString().Trim();
            string str3 = ((object)this.Request["payAmount"]).ToString().Trim();
            string paramValue13 = ((object)this.Request["billOrderTime"]).ToString().Trim();
            string paramValue14 = ((object)this.Request["payResult"]).ToString().Trim();
            string paramValue15 = ((object)this.Request["signType"]).ToString().Trim();
            string paramValue16 = ((object)this.Request["receiveBossType"]).ToString().Trim();
            string paramValue17 = ((object)this.Request["receiverAcctId"]).ToString().Trim();
            if (!(((object)this.Request["signMsg"]).ToString().Trim().ToUpper() == ShenZhouXing.GetMD5(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam(ShenZhouXing.appendParam("", "merchantAcctId", paramValue1), "version", paramValue4), "language", paramValue5), "payType", paramValue6), "cardNumber", paramValue7), "cardPwd", paramValue8), "orderId", str1), "orderAmount", paramValue9), "dealId", str2), "orderTime", paramValue10), "ext1", paramValue11), "ext2", paramValue12), "payAmount", str3), "billOrderTime", paramValue13), "payResult", paramValue14), "signType", paramValue15), "bossType", paramValue3), "receiveBossType", paramValue16), "receiverAcctId", paramValue17), "key", paramValue2), "gb2312").ToUpper()))
                return;
            string opstate = "-1";
            if (paramValue14 == "10")
                opstate = "0";
            int status = paramValue14 == "10" ? 2 : 4;
            string str4 = "1";
            string str5 = string.Empty;
            if (!isReceive)
            {
                string str6;
                switch (paramValue14)
                {
                    case "10":
                        str6 = RuntimeSetting.SiteDomain + "/return/KuaiQian_SZX_Receive.aspx";
                        break;
                    default:
                        str6 = RuntimeSetting.SiteDomain + "/return/KuaiQian_SZX_Receive.aspx";
                        break;
                }
                OrderCard orderCard = new OrderCard();
                string msg = paramValue14;
                string userviewmsg = msg;
                orderCard.DoCardComplete(ShenZhouXing.suppId, str1, str2, status, opstate, msg, userviewmsg, Decimal.Parse(str3) / new Decimal(100), new Decimal(0), string.Empty, (byte)1);
                HttpContext.Current.Response.Write(string.Format("<result>{0}</result><redirecturl>{1}</redirecturl>", (object)str4, (object)str6));
            }
        }
    }
}
