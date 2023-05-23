using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using viviapi.BLL;

namespace viviapi.ETAPI
{
    public class IPSCode : ETAPIBase
    {
        private static int suppId = 10023;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/IPS70Bank_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/IPS70Bank_notify.aspx";
            }
        }

        public IPSCode()
            : base(IPSCode.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = this.postBankUrl;
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str1 = this._suppInfo.jumpUrl + "/switch/ips70pay.aspx";

            string str2 = this.OrganizationXml(orderid, orderAmt, bankcode);
            string suppAccount = this.suppAccount;
            return "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"pGateWayReq\" value=\"" + str2 + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
        }

        protected string OrganizationXml(string orderid, Decimal orderAmt, string bankcode)
        {
            //<Ips><GateWayReq><head><Version>v1.0.0</Version><MerCode>192038</MerCode><MerName></MerName><Account>1920380019</Account><MsgId></MsgId><ReqDate>20170121074212</ReqDate><Signature>545df1c083fea0d49c5054b5664a9e89</Signature></head><body><MerBillNo>17012107420925010478</MerBillNo><Amount>0.02</Amount><Date>20170121</Date><CurrencyType>156</CurrencyType><GatewayType>10</GatewayType><Lang>GB</Lang><Attach>Attach</Attach><RetEncodeType>17</RetEncodeType><ServerUrl>http://pay.sjiepay.com/notify/IPS30Bank_notify.aspx</ServerUrl><BillEXP></BillEXP><GoodsName>Game</GoodsName></body></GateWayReq></Ips>
            string str1 = string.Empty;
            string signture = this.GetSignture(this.suppAccount, orderid, orderAmt, bankcode);
            if (signture == "")
                return str1;
            object[] objArray1 = new object[17];
            objArray1[0] = (object)"<Ips><GateWayReq><head><Version>v1.0.0</Version><MerCode>";
            objArray1[1] = (object)this.suppAccount;
            objArray1[2] = (object)"</MerCode><MerName></MerName><Account>";
            objArray1[3] = (object)this.suppUserName;
            objArray1[4] = (object)"</Account><MsgId></MsgId><ReqDate>";
            DateTime now = DateTime.Now;
            string str2 = now.ToString("yyyyMMddHHmmss");
            objArray1[5] = str2 + "</ReqDate><Signature>";
            objArray1[6] = signture;
            objArray1[7] = "</Signature></head><body><MerBillNo>";
            objArray1[8] = orderid;
            objArray1[9] = "</MerBillNo><Amount>";
            objArray1[10] = orderAmt.ToString();
            objArray1[11] = "</Amount><Date>";
            now = DateTime.Now;
            string str3 = now.ToString("yyyyMMdd");
            string type = "10";
            if (bankcode == "1004")
                type = "11";

            objArray1[12] = str3 + "</Date><CurrencyType>156</CurrencyType><GatewayType>" + type + "</GatewayType><Lang>GB</Lang>";

            objArray1[13] = "<Attach>Attach</Attach><RetEncodeType>17</RetEncodeType><ServerUrl>";
            objArray1[14] = this.notifyUrl;
            objArray1[15] = "</ServerUrl><BillEXP></BillEXP><GoodsName>Game</GoodsName>";

            objArray1[16] = "</body></GateWayReq></Ips>";
            return string.Concat(objArray1);
        }

        public string GetSignture(string argMerCode, string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = string.Empty;
            string zhilian = "";
            if (this.GetBankCode(bankcode).Length > 0)
                zhilian = "1";
            string str2 = "<body><MerBillNo>" + orderid + "</MerBillNo><Amount>" + orderAmt.ToString() + "</Amount><Date>" + DateTime.Now.ToString("yyyyMMdd") + "</Date><CurrencyType>156</CurrencyType><GatewayType>01</GatewayType><Lang>GB</Lang><Merchanturl>" + this.notifyUrl + "</Merchanturl><FailUrl></FailUrl><Attach>Attach</Attach><OrderEncodeType>5</OrderEncodeType><RetEncodeType>17</RetEncodeType><RetType>1</RetType><ServerUrl>" + this.notifyUrl + "</ServerUrl><BillEXP></BillEXP><GoodsName>Game</GoodsName><IsCredit>" + zhilian + "</IsCredit><BankCode>" + this.GetBankCode(bankcode) + "</BankCode><ProductType>1</ProductType></body>";
            string suppKey = this.suppKey;
            if (suppKey != "")
                return IPS70.Sign(((object)(str2 + this.suppAccount + suppKey)).ToString(), "MD5", "UTF-8");
            else
                return "";
        }

        public static string Sign(string prestr, string sign_type, string _input_charset)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder(32);
                if (sign_type.ToUpper() == "MD5")
                {
                    foreach (byte num in new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr)))
                        stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
                }
                return ((object)stringBuilder).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(((object)ex).ToString());
            }
        }

        public void Notify()
        {
            string str1 = HttpContext.Current.Request.Form["paymentResult"];
            if (!(str1 != ""))
                return;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(((object)str1).ToString());
            string innerText1 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/head/ReferenceID").InnerText;
            string innerText2 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/head/RspCode").InnerText;
            string innerText3 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/head/RspMsg").InnerText;
            string innerText4 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/head/ReqDate").InnerText;
            string innerText5 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/head/RspDate").InnerText;
            string innerText6 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/head/Signature").InnerText;
            string innerText7 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/MerBillNo").InnerText;
            string innerText8 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/CurrencyType").InnerText;
            string innerText9 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/Amount").InnerText;
            string innerText10 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/Date").InnerText;
            string innerText11 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/Status").InnerText;
            string innerText12 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/Msg").InnerText;
            string innerText13 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/Attach").InnerText;
            string innerText14 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/IpsBillNo").InnerText;
            string innerText15 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/IpsTradeNo").InnerText;
            string innerText16 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/RetEncodeType").InnerText;
            string innerText17 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/BankBillNo").InnerText;
            string innerText18 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/ResultType").InnerText;
            string innerText19 = xmlDocument.DocumentElement.SelectSingleNode("GateWayRsp/body/IpsBillTime").InnerText;
            string str2 = string.Empty;
            if (IPS70.Sign(((object)(((object)("<body><MerBillNo>" + innerText7 + "</MerBillNo><CurrencyType>" + innerText8 + "</CurrencyType><Amount>" + innerText9 + "</Amount><Date>" + innerText10 + "</Date><Status>" + innerText11 + "</Status><Msg><![CDATA[" + innerText12 + "]]></Msg><Attach><![CDATA[" + innerText13 + "]]></Attach><IpsBillNo>" + innerText14 + "</IpsBillNo><IpsTradeNo>" + innerText15 + "</IpsTradeNo><RetEncodeType>" + innerText16 + "</RetEncodeType><BankBillNo>" + innerText17 + "</BankBillNo><ResultType>" + innerText18 + "</ResultType><IpsBillTime>" + innerText19 + "</IpsBillTime></body>")).ToString() + this.suppAccount + this.suppKey)).ToString(), "MD5", "UTF-8") == innerText6)
            {
                string opstate = "-1";
                int status = 4;
                if (innerText11 == "Y")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(IPSCode.suppId, innerText7, innerText14, status, opstate, string.Empty, Decimal.Parse(innerText9), new Decimal(0), true, true);
                HttpContext.Current.Response.Write("验证成功<br />");
                HttpContext.Current.Response.Write("billno=" + innerText7);
            }
            else
            {
                HttpContext.Current.Response.Write("验证失败<br />");
                HttpContext.Current.Response.Write("billno=" + innerText7);
            }
        }

        public string GetBankCode(string paymodeId)
        {
            string str = "";
            switch (paymodeId)
            {
                case "970":
                    str = "1102";
                    break;
                case "967":
                    str = "1100";
                    break;
                case "964":
                    str = "1101";
                    break;
                case "965":
                    str = "1106";
                    break;
                case "963":
                    str = "1107";
                    break;
                case "977":
                    str = "1109";
                    break;
                case "981":
                    str = "1108";
                    break;
                case "980":
                    str = "1110";
                    break;
                case "974":
                    str = "00023";
                    break;
                case "985":
                    str = "1114";
                    break;
                case "962":
                    str = "1104";
                    break;
                case "982":
                    str = "1111";
                    break;
                case "972":
                    str = "1103";
                    break;
                case "984":
                    str = "00011";
                    break;
                case "976":
                    str = "00030";
                    break;
                case "989":
                    str = "1113";
                    break;
                case "990":
                    str = "1124";
                    break;
                case "979":
                    str = "1115";
                    break;
                case "986":
                    str = "1112";
                    break;
                case "983":
                    str = "1117";
                    break;
                case "978":
                    str = "1121";
                    break;
                case "968":
                    str = "1120";
                    break;
                case "975":
                    str = "1116";
                    break;
                case "971":
                    str = "1119";
                    break;
            }
            return str;
        }
    }
}
