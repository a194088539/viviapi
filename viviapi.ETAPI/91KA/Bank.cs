using System;
using viviapi.BLL;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.ETAPI._91KA
{
    public class Bank : ETAPIBase
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
                return this.SiteDomain + "/notify/91ka/notify.aspx";
            }
        }

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string GetPayForm(string orderid, Decimal orderAmt, string bankcode)
        {
            string suppAccount = this.suppAccount;
            string str1 = Decimal.Round(orderAmt, 2).ToString();
            string str2 = "1";
            string str3 = string.Empty;
            bankcode = this.GetBankCode(bankcode);
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = string.Empty;
            string returnurl = this.returnurl;
            string notifyUrl = this.notifyUrl;
            string str7 = "2.0.1";
            string str8 = string.Empty;
            string str9 = string.Empty;
            string str10 = this.postBankUrl;
            if (string.IsNullOrEmpty(str10))
                str10 = "http://www.91ka.com/auto_interface_third.php";
            bankcode = this.GetBankCode(bankcode);
            string str11 = Cryptography.MD5(string.Format("orderid={0}&origin={1}&chargemoney={3}&channelid={4}&paytype=&bankcode={5}&cardno=&cardpwd=&cardamount=&fronturl={6}&bgurl={7}&ext1=&ext2={8}", (object)orderid, (object)suppAccount, (object)str1, (object)str2, (object)bankcode, (object)returnurl, (object)notifyUrl, (object)this.suppKey), "gb2312");
            string str12 = string.Empty;
            string str13 = string.Empty;
            return "<form name=\"frm1\" id=\"frm1\" method=\"POST\" action=\"" + str10 + "\">" + "<input type=\"hidden\" name=\"origin\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"chargemoney\" value=\"" + str1 + "\" />" + "<input type=\"hidden\" name=\"channelid\" value=\"" + str2 + "\" />" + "<input type=\"hidden\" name=\"paytype\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"bankcode\" value=\"" + bankcode + "\" />" + "<input type=\"hidden\" name=\"cardno\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"cardpwd\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"cardamount\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"fronturl\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"bgurl\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"version\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"ext1\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"ext2\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"validate\" value=\"" + str11 + "\" />" + "</form>";
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "1001";
                    break;
                case "967":
                    str2 = "1002";
                    break;
                case "964":
                    str2 = "1005";
                    break;
                case "965":
                    str2 = "1003";
                    break;
                case "963":
                    str2 = "BOCB2C";
                    break;
                case "977":
                    str2 = "1004";
                    break;
                case "981":
                    str2 = "1020";
                    break;
                case "980":
                    str2 = "1006";
                    break;
                case "974":
                    str2 = "1008";
                    break;
                case "985":
                    str2 = "GDB";
                    break;
                case "962":
                    str2 = "ECITIC";
                    break;
                case "982":
                    str2 = "HXB";
                    break;
                case "972":
                    str2 = "1009";
                    break;
                case "984":
                    str2 = "GZRCC";
                    break;
                case "976":
                    str2 = "SHRCB";
                    break;
                case "989":
                    str2 = "1032";
                    break;
                case "988":
                    str2 = "CBHB";
                    break;
                case "990":
                    str2 = "BJRCB";
                    break;
                case "979":
                    str2 = "NJB";
                    break;
                case "986":
                    str2 = "1022";
                    break;
                case "987":
                    str2 = "BEA";
                    break;
                case "1025":
                    str2 = "NBCB";
                    break;
                case "978":
                    str2 = "PAB";
                    break;
                case "968":
                    str2 = "ZSB";
                    break;
                case "975":
                    str2 = "SHB";
                    break;
                case "971":
                    str2 = "POST";
                    break;
                case "992":
                    str2 = "ALIPAY";
                    break;
                case "993":
                    str2 = "TENPAY";
                    break;
                case "994":
                    str2 = "BILL";
                    break;
                case "1001":
                    str2 = "CMPAY";
                    break;
                case "1002":
                    str2 = "YP";
                    break;
                default:
                    str2 = "1002";
                    break;
            }
            return str2;
        }

        public void ReturnBank()
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
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (formString5 == "1")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            new OrderBank().DoBankComplete(Bank.suppId, formString1, formString3, status, opstate, msg, Decimal.Parse(formString4), new Decimal(0), false, true);
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
            string msg = "支付失败";
            string opstate = "-1";
            int status = 4;
            if (formString5 == "1")
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            new OrderBank().DoBankComplete(Bank.suppId, formString1, formString3, status, opstate, msg, Decimal.Parse(formString4), new Decimal(0), true, false);
        }
    }
}
