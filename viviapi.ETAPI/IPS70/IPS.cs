using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using viviapi.BLL;
using viviLib.Logging;

namespace viviapi.ETAPI
{
    public class IPS70 : ETAPIBase
    {
        private static int suppId = 10022;

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

        public IPS70()
            : base(IPS70.suppId)
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
            string str1 = string.Empty;
            string signture = this.GetSignture(this.suppAccount, orderid, orderAmt, bankcode);
            if (signture == "")
                return str1;
            object[] objArray1 = new object[21];
            objArray1[0] = (object)"<Ips><GateWayReq><head><Version>v1.0.0</Version><MerCode>";
            objArray1[1] = (object)this.suppAccount;
            objArray1[2] = (object)"</MerCode><MerName></MerName><Account>";
            objArray1[3] = (object)this.suppUserName;
            objArray1[4] = (object)"</Account><MsgId></MsgId><ReqDate>";
            object[] objArray2 = objArray1;
            int index1 = 5;
            DateTime now = DateTime.Now;
            string str2 = now.ToString("yyyyMMddHHmmss");
            objArray2[index1] = str2;
            objArray1[6] = "</ReqDate><Signature>";
            objArray1[7] = signture;
            objArray1[8] = "</Signature></head><body><MerBillNo>";
            objArray1[9] = orderid;
            objArray1[10] = "</MerBillNo><Amount>";
            objArray1[11] = orderAmt.ToString();
            objArray1[12] = "</Amount><Date>";
            object[] objArray3 = objArray1;
            int index2 = 13;
            now = DateTime.Now;
            string str3 = now.ToString("yyyyMMdd");
            objArray3[index2] = str3;
            objArray1[14] = "</Date><CurrencyType>156</CurrencyType><GatewayType>01</GatewayType><Lang>GB</Lang><Merchanturl>";
            objArray1[15] = this.returnurl;
            objArray1[16] = "</Merchanturl><FailUrl></FailUrl><Attach>Attach</Attach><OrderEncodeType>5</OrderEncodeType><RetEncodeType>17</RetEncodeType><RetType>1</RetType><ServerUrl>";
            objArray1[17] = this.notifyUrl;
            string zhilian = "";
            if (this.GetBankCode(bankcode).Length > 0)
                zhilian = "1";
            objArray1[18] = "</ServerUrl><BillEXP></BillEXP><GoodsName>Game</GoodsName><IsCredit>" + zhilian + "</IsCredit><BankCode>";
            objArray1[19] = this.GetBankCode(bankcode);
            objArray1[20] = "</BankCode><ProductType>1</ProductType></body></GateWayReq></Ips>";
            return string.Concat(objArray1);
        }

        public string GetSignture(string argMerCode, string orderid, Decimal orderAmt, string bankcode)
        {
            string str1 = string.Empty;
            string zhilian = "";
            if (this.GetBankCode(bankcode).Length > 0)
                zhilian = "1";
            string str2 = "<body><MerBillNo>" + orderid + "</MerBillNo><Amount>" + orderAmt.ToString() + "</Amount><Date>" + DateTime.Now.ToString("yyyyMMdd") + "</Date><CurrencyType>156</CurrencyType><GatewayType>01</GatewayType><Lang>GB</Lang><Merchanturl>" + this.returnurl + "</Merchanturl><FailUrl></FailUrl><Attach>Attach</Attach><OrderEncodeType>5</OrderEncodeType><RetEncodeType>17</RetEncodeType><RetType>1</RetType><ServerUrl>" + this.notifyUrl + "</ServerUrl><BillEXP></BillEXP><GoodsName>Game</GoodsName><IsCredit>" + zhilian + "</IsCredit><BankCode>" + this.GetBankCode(bankcode) + "</BankCode><ProductType>1</ProductType></body>";
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

        public void ReturnBank()
        {
            HttpContext.Current.Request.ContentEncoding = Encoding.GetEncoding("GB2312");
            string str1 = HttpContext.Current.Request.Form["paymentResult"];
            LogHelper.Write("return:" + str1);
            //if (!(str1 != ""))
            //    return;
            XmlDocument xmlDocument = new XmlDocument();
            //string str1 = "<Ips><GateWayRsp><head><ReferenceID></ReferenceID><RspCode>000000</RspCode><RspMsg><![CDATA[交易成功！]]></RspMsg><ReqDate>20170124013854</ReqDate><RspDate>20170124013917</RspDate><Signature>2906aa948dec8a11fab5b039bd05a0f4</Signature></head><body><MerBillNo>17012401385362020549</MerBillNo><CurrencyType>156</CurrencyType><Amount>0.02</Amount><Date>20170124</Date><Status>Y</Status><Msg><![CDATA[支付成功！]]></Msg><Attach><![CDATA[Attach]]></Attach><IpsBillNo>BO201701240138544276</IpsBillNo><IpsTradeNo>2017012401385422585</IpsTradeNo><RetEncodeType>17</RetEncodeType><BankBillNo>710019824204</BankBillNo><ResultType>0</ResultType><IpsBillTime>20170124013917</IpsBillTime></body></GateWayRsp></Ips>";

            xmlDocument.LoadXml(str1);
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
            if (IPS70.Sign("<body><MerBillNo>" + innerText7 + "</MerBillNo><CurrencyType>" + innerText8 + "</CurrencyType><Amount>" + innerText9 + "</Amount><Date>" + innerText10 + "</Date><Status>" + innerText11 + "</Status><Msg><![CDATA[" + innerText12 + "]]></Msg><Attach><![CDATA[" + innerText13 + "]]></Attach><IpsBillNo>" + innerText14 + "</IpsBillNo><IpsTradeNo>" + innerText15 + "</IpsTradeNo><RetEncodeType>" + innerText16 + "</RetEncodeType><BankBillNo>" + innerText17 + "</BankBillNo><ResultType>" + innerText18 + "</ResultType><IpsBillTime>" + innerText19 + "</IpsBillTime></body>" + this.suppAccount + this.suppKey, "MD5", "UTF-8") == innerText6)
            {
                string opstate = "-1";
                int status = 4;
                if (innerText11 == "Y")
                {
                    opstate = "0";
                    status = 2;
                }
                new OrderBank().DoBankComplete(IPS70.suppId, innerText7, innerText14, status, opstate, string.Empty, Decimal.Parse(innerText9), new Decimal(0), true, true);
            }
            else
            {
                HttpContext.Current.Response.Write("验证失败<br />");
                HttpContext.Current.Response.Write("billno=" + innerText7);
            }
        }

        public void Notify()
        {
            HttpContext.Current.Request.ContentEncoding = Encoding.UTF8;
            string str1 = HttpContext.Current.Request.Form["paymentResult"];

            LogHelper.Write("notify:" + str1);

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
                new OrderBank().DoBankComplete(IPS70.suppId, innerText7, innerText14, status, opstate, string.Empty, Decimal.Parse(innerText9), new Decimal(0), true, true);
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
