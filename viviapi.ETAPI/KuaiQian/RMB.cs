using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using viviapi.BLL;
using viviapi.SysConfig;

namespace viviapi.ETAPI.KuaiQian
{
    public class RMB : ETAPIBase
    {
        private static int suppId = 10026;
        private string const_prikey_path = PaymentSetting.KuaiQian_prikey_path;
        private string const_pubkey_path = PaymentSetting.KuaiQian_pubkey_path;
        public string merchantAcctId = string.Empty;
        public string inputCharset = "1";
        public string version = "v2.0";
        public string language = "1";
        public string signType = "4";
        public string payerName = "";
        public string payerContactType = "1";
        public string payerContact = "";
        public string orderId = "";
        public string orderAmount = "";
        public string orderTime = "";
        public string stringorderTime = "";
        public string productName = "";
        public string productNum = "";
        public string productId = "";
        public string productDesc = "";
        public string ext1 = "";
        public string ext2 = "";
        public string payType = "10";
        public string bankId = "";
        public string redoFlag = "0";
        public string pid = "";
        public string signMsg = "";

        internal string pageUrl
        {
            get
            {
                return this.SiteDomain + "/return/KuaiQian_RMB_Return.aspx";
            }
        }

        internal string bgUrl
        {
            get
            {
                return this.SiteDomain + "/notify/KuaiQian_RMB_Notify.aspx";
            }
        }

        internal string showurl
        {
            get
            {
                return this.SiteDomain + "/return/KuaiQian_RMB_Receive.aspx";
            }
        }

        private HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        public RMB()
          : base(RMB.suppId)
        {
        }

        public string UrlEncode(string value)
        {
            value = HttpUtility.UrlEncode(value, Encoding.UTF8).Replace("+", "%20");
            value = new Regex("(%[0-9a-f][0-9a-f])").Replace(value, (MatchEvaluator)(m => m.Value.ToUpper()));
            return value;
        }

        public static string CerRSASignature(string OriginalString, string prikey_path, string CertificatePW, int SignType)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(OriginalString);
            RSAPKCS1SignatureFormatter signatureFormatter = new RSAPKCS1SignatureFormatter(new X509Certificate2(prikey_path, CertificatePW).PrivateKey);
            byte[] hash;
            if (SignType == 1)
            {
                signatureFormatter.SetHashAlgorithm("MD5");
                hash = new MD5CryptoServiceProvider().ComputeHash(bytes);
            }
            else
            {
                signatureFormatter.SetHashAlgorithm("SHA1");
                hash = new SHA1CryptoServiceProvider().ComputeHash(bytes);
            }
            return ((object)Convert.ToBase64String(signatureFormatter.CreateSignature(hash))).ToString();
        }

        public static bool CerRSAVerifySignature(string OriginalString, string SignatureString, string pubkey_path, string CertificatePW, int SignType)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(OriginalString);
            byte[] rgbSignature = Convert.FromBase64String(SignatureString);
            RSACryptoServiceProvider cryptoServiceProvider = (RSACryptoServiceProvider)new X509Certificate2(pubkey_path, CertificatePW).PublicKey.Key;
            cryptoServiceProvider.ImportCspBlob(cryptoServiceProvider.ExportCspBlob(false));
            RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter((AsymmetricAlgorithm)cryptoServiceProvider);
            byte[] hash;
            if (SignType == 1)
            {
                signatureDeformatter.SetHashAlgorithm("MD5");
                hash = new MD5CryptoServiceProvider().ComputeHash(bytes);
            }
            else
            {
                signatureDeformatter.SetHashAlgorithm("SHA1");
                hash = new SHA1CryptoServiceProvider().ComputeHash(bytes);
            }
            return signatureDeformatter.VerifySignature(hash, rgbSignature);
        }

        public string appendParam1(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                    returnStr = returnStr + "&" + paramId + "=" + this.UrlEncode(paramValue);
            }
            else if (paramValue != "")
                returnStr = paramId + "=" + this.UrlEncode(paramValue);
            return returnStr;
        }

        public string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                    returnStr = returnStr + "&" + paramId + "=" + paramValue;
            }
            else if (paramValue != "")
                returnStr = paramId + "=" + paramValue;
            return returnStr;
        }

        private static string GetMD5(string dataStr, string codeType)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(codeType).GetBytes(dataStr));
            StringBuilder stringBuilder = new StringBuilder(32);
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("x").PadLeft(2, '0'));
            return ((object)stringBuilder).ToString();
        }

        public string GetPayUrl(string orderid, Decimal orderAmt, string bankcode)
        {
            this.merchantAcctId = this.suppAccount;
            string suppKey = this.suppKey;
            this.payerName = "支付人姓名";
            this.payerContactType = "1";
            this.payerContact = "";
            this.orderId = orderid;
            this.orderAmount = (orderAmt * new Decimal(100)).ToString("0");
            this.orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.productName = "商品名称";
            this.productNum = "1";
            this.productId = "";
            this.productDesc = "";
            this.ext1 = "";
            this.ext2 = "";
            this.payType = "10";
            this.bankId = Bank.GetBankCode(bankcode);
            this.redoFlag = "0";
            this.pid = "";
            this.signMsg = RMB.CerRSASignature(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam("", "inputCharset", this.inputCharset), "pageUrl", this.pageUrl), "bgUrl", this.bgUrl), "version", this.version), "language", this.language), "signType", this.signType), "merchantAcctId", this.merchantAcctId), "payerName", this.payerName), "payerContactType", this.payerContactType), "payerContact", this.payerContact), "orderId", this.orderId), "orderAmount", this.orderAmount), "orderTime", this.orderTime), "productName", this.productName), "productNum", this.productNum), "productId", this.productId), "productDesc", this.productDesc), "ext1", this.ext1), "ext2", this.ext2), "payType", this.payType), "bankId", this.bankId), "redoFlag", this.redoFlag), "pid", this.pid), this.const_prikey_path, suppKey, 2);
            string str = this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1(this.appendParam1("", "inputCharset", this.inputCharset), "pageUrl", this.pageUrl), "bgUrl", this.bgUrl), "version", this.version), "language", this.language), "signType", this.signType), "merchantAcctId", this.merchantAcctId), "payerName", this.payerName), "payerContactType", this.payerContactType), "payerContact", this.payerContact), "orderId", this.orderId), "orderAmount", this.orderAmount), "orderTime", this.orderTime), "productName", this.productName), "productNum", this.productNum), "productId", this.productId), "productDesc", this.productDesc), "ext1", this.ext1), "ext2", this.ext2), "payType", this.payType), "bankId", this.bankId), "redoFlag", this.redoFlag), "pid", this.pid);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.postBankUrl + "?");
            stringBuilder.Append(str);
            stringBuilder.Append("&signMsg=" + this.UrlEncode(this.signMsg));
            return ((object)stringBuilder).ToString();
        }

        public void Return(bool isReceive, bool isNotify)
        {
            this.merchantAcctId = ((object)this.Request["merchantAcctId"]).ToString().Trim();
            this.version = ((object)this.Request["version"]).ToString().Trim();
            this.language = ((object)this.Request["language"]).ToString().Trim();
            this.signType = ((object)this.Request["signType"]).ToString().Trim();
            this.payType = ((object)this.Request["payType"]).ToString().Trim();
            this.bankId = ((object)this.Request["bankId"]).ToString().Trim();
            this.orderId = ((object)this.Request["orderId"]).ToString().Trim();
            this.orderTime = ((object)this.Request["orderTime"]).ToString().Trim();
            this.orderAmount = ((object)this.Request["orderAmount"]).ToString().Trim();
            this.ext1 = ((object)this.Request["ext1"]).ToString().Trim();
            this.ext2 = ((object)this.Request["ext2"]).ToString().Trim();
            string str1 = ((object)this.Request["bankDealId"]).ToString().Trim();
            string paramValue1 = ((object)this.Request["dealTime"]).ToString().Trim();
            string str2 = ((object)this.Request["payAmount"]).ToString().Trim();
            string paramValue2 = ((object)this.Request["fee"]).ToString().Trim();
            string paramValue3 = ((object)this.Request["payResult"]).ToString().Trim();
            string paramValue4 = ((object)this.Request["errCode"]).ToString().Trim();
            string paramValue5 = ((object)this.Request["dealId"]).ToString().Trim();
            string SignatureString = ((object)this.Request["signMsg"]).ToString().Trim();
            string OriginalString = this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam(this.appendParam("", "merchantAcctId", this.merchantAcctId), "version", this.version), "language", this.language), "signType", this.signType), "payType", this.payType), "bankId", this.bankId), "orderId", this.orderId), "orderTime", this.orderTime), "orderAmount", this.orderAmount), "dealId", paramValue5), "bankDealId", str1), "dealTime", paramValue1), "payAmount", str2), "fee", paramValue2), "ext1", this.ext1), "ext2", this.ext2), "payResult", paramValue3), "errCode", paramValue4);
            string suppKey = this.suppKey;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string opstate = "-1";
            int status = 4;
            OrderBank orderBank = new OrderBank();
            if (RMB.CerRSAVerifySignature(OriginalString, SignatureString, this.const_pubkey_path, suppKey, 2))
            {
                if (!isReceive)
                {
                    string str5 = "1";
                    string showurl = this.showurl;
                    switch (paramValue3)
                    {
                        case "10":
                            status = 2;
                            opstate = "0";
                            break;
                        default:
                            str5 = "1";
                            break;
                    }
                    orderBank.DoBankComplete(RMB.suppId, this.orderId, str1, status, opstate, string.Empty, Decimal.Parse(str2) / new Decimal(100), new Decimal(0), false, false);
                    HttpContext.Current.Response.Write(string.Format("<result>{0}</result><redirecturl>{1}</redirecturl>", (object)str5, (object)showurl));
                }
                else
                    orderBank.DoBankComplete(RMB.suppId, this.orderId, str1, status, opstate, string.Empty, Decimal.Parse(str2) / new Decimal(100), new Decimal(0), isNotify, !isNotify);
            }
            else
                HttpContext.Current.Response.Write("签名错误");
        }
    }
}
