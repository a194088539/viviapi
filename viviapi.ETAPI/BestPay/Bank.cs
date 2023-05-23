using System;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.Web;

namespace viviapi.ETAPI.BestPay
{
    public class Bank : ETAPIBase
    {
        private static int suppId = 1007;

        internal string returnurl
        {
            get
            {
                return this.SiteDomain + "/return/BestPay/Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/BestPay/callback.aspx";
            }
        }

        public Bank()
          : base(Bank.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankCode, bool isForm)
        {
            string str1 = "https://webpaywg.bestpay.com.cn/payWebDirect.do";
            if (!string.IsNullOrEmpty(this._suppInfo.jumpUrl))
                str1 = this._suppInfo.postBankUrl;
            string str2 = "UTF-8";
            string suppAccount = this.suppAccount;
            string suppUserName = this.suppUserName;
            string suppKey = this.suppKey;
            string returnurl = this.returnurl;
            string notifyUrl = this.notifyUrl;
            string ORDERSEQ = orderid;
            string str3 = orderid + this.suppAccount;
            string ORDERDATE = DateTime.Now.ToString("yyyyMMddhhmmss");
            Decimal ORDERAMOUNT = Decimal.Round(orderAmt * new Decimal(100), 0);
            Decimal num1 = new Decimal(0);
            Decimal num2 = new Decimal(0);
            string str4 = "RMB";
            string str5 = "1";
            string str6 = "";
            string bankCode1 = this.GetBankCode(bankCode);
            string str7 = "0001";
            string str8 = "08";
            string str9 = "";
            string str10 = "";
            string str11 = "日租车辆 一辆 张三 13338255911";
            string str12 = this.suppUserName + (object)":" + (string)(object)ORDERAMOUNT;
            string str13 = "";
            string trueIp = ServerVariables.TrueIP;
            string str14 = string.Concat(new object[4]
            {
        (object) string.Concat(new object[4]
        {
          (object) string.Concat(new object[4]
          {
            (object) ("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + str1 + "\">" + "<input type=\"hidden\" name=\"MERCHANTID\" value=\"" + suppAccount + "\" />" + "<input type=\"hidden\" name=\"SUBMERCHANTID\" value=\"" + suppUserName + "\" />" + "<input type=\"hidden\" name=\"ORDERSEQ\" value=\"" + ORDERSEQ + "\" />" + "<input type=\"hidden\" name=\"ORDERREQTRANSEQ\" value=\"" + str3 + "\" />" + "<input type=\"hidden\" name=\"ORDERDATE\" value=\"" + ORDERDATE + "\" />"),
            (object) "<input type=\"hidden\" name=\"ORDERAMOUNT\" value=\"",
            (object) ORDERAMOUNT,
            (object) "\" />"
          }),
          (object) "<input type=\"hidden\" name=\"PRODUCTAMOUNT\" value=\"",
          (object) ORDERAMOUNT,
          (object) "\" />"
        }),
        (object) "<input type=\"hidden\" name=\"ATTACHAMOUNT\" value=\"",
        (object) num2,
        (object) "\" />"
            }) + "<input type=\"hidden\" name=\"CURTYPE\" value=\"" + str4 + "\" />" + "<input type=\"hidden\" name=\"ENCODETYPE\" value=\"" + str5 + "\" />" + "<input type=\"hidden\" name=\"MERCHANTURL\" value=\"" + returnurl + "\" />" + "<input type=\"hidden\" name=\"BACKMERCHANTURL\" value=\"" + notifyUrl + "\" />" + "<input type=\"hidden\" name=\"BANKID\" value=\"" + bankCode1 + "\" />" + "<input type=\"hidden\" name=\"ATTACH\" value=\"" + str6 + "\" />" + "<input type=\"hidden\" name=\"BUSICODE\" value=\"" + str7 + "\" />" + "<input type=\"hidden\" name=\"PRODUCTID\" value=\"" + str8 + "\" />" + "<input type=\"hidden\" name=\"TMNUM\" value=\"" + str9 + "\" />" + "<input type=\"hidden\" name=\"CUSTOMERID\" value=\"" + str10 + "\" />" + "<input type=\"hidden\" name=\"PRODUCTDESC\" value=\"" + HttpUtility.UrlEncode(str11, Encoding.GetEncoding(str2)) + "\" />" + "<input type=\"hidden\" name=\"MAC\" value=\"" + BestPayUtil.getMAC(suppAccount, ORDERSEQ, ORDERDATE, ORDERAMOUNT, trueIp, suppKey, str2) + "\" />" + "<input type=\"hidden\" name=\"DIVDETAILS\" value=\"" + str12 + "\" />" + "<input type=\"hidden\" name=\"PEDCNT\" value=\"" + str13 + "\" />" + "<input type=\"hidden\" name=\"CLIENTIP\" value=\"" + trueIp + "\" />" + "</form>";
            if (!isForm)
                str14 += "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
            return str14;
        }

        public void ReturnBank()
        {
            string UPTRANSEQ = HttpContext.Current.Request.Params["UPTRANSEQ"];
            string TRANDATE = HttpContext.Current.Request.Params["TRANDATE"];
            string RETNCODE = HttpContext.Current.Request.Params["RETNCODE"];
            string RETNINFO = HttpContext.Current.Request.Params["RETNINFO"];
            string str1 = HttpContext.Current.Request.Params["ORDERSEQ"];
            string str2 = HttpContext.Current.Request.Params["ORDERREQTRANSEQ"];
            string str3 = HttpContext.Current.Request.Params["ORDERAMOUNT"];
            string str4 = HttpContext.Current.Request.Params["PRODUCTAMOUNT"];
            string str5 = HttpContext.Current.Request.Params["ATTACHAMOUNT"];
            string str6 = HttpContext.Current.Request.Params["CURTYPE"];
            string str7 = HttpContext.Current.Request.Params["ENCODETYPE"];
            string str8 = HttpContext.Current.Request.Params["BANKID"];
            string str9 = HttpContext.Current.Request.Params["ATTACH"];
            string str10 = HttpContext.Current.Request.Params["SIGN"];
            string MERCHANTID = HttpContext.Current.Request.Params["MERCHANTID"];
            string suppKey = this.suppKey;
            string sign = BestPayUtil.getSign(UPTRANSEQ, MERCHANTID, str1, str3, RETNCODE, RETNINFO, TRANDATE, BestPayUtil.Charset, suppKey);
            if (!(str10 == sign))
                return;
            string msg = "支付失败" + RETNINFO;
            string opstate = "-1";
            int status = 4;
            if (RETNCODE.Equals("0000"))
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            string str11 = string.Empty;
            new OrderBank().DoBankComplete(Bank.suppId, str1, "", status, opstate, msg, Decimal.Parse(str3) / new Decimal(100), new Decimal(0), false, true);
            HttpContext.Current.Response.Write("UPTRANSEQ_" + UPTRANSEQ);
        }

        public void Notify()
        {
            string UPTRANSEQ = HttpContext.Current.Request.Params["UPTRANSEQ"];
            string TRANDATE = HttpContext.Current.Request.Params["TRANDATE"];
            string RETNCODE = HttpContext.Current.Request.Params["RETNCODE"];
            string RETNINFO = HttpContext.Current.Request.Params["RETNINFO"];
            string str1 = HttpContext.Current.Request.Params["ORDERSEQ"];
            string str2 = HttpContext.Current.Request.Params["ORDERREQTRANSEQ"];
            string str3 = HttpContext.Current.Request.Params["ORDERAMOUNT"];
            string str4 = HttpContext.Current.Request.Params["PRODUCTAMOUNT"];
            string str5 = HttpContext.Current.Request.Params["ATTACHAMOUNT"];
            string str6 = HttpContext.Current.Request.Params["CURTYPE"];
            string str7 = HttpContext.Current.Request.Params["ENCODETYPE"];
            string str8 = HttpContext.Current.Request.Params["BANKID"];
            string str9 = HttpContext.Current.Request.Params["ATTACH"];
            string str10 = HttpContext.Current.Request.Params["SIGN"];
            string MERCHANTID = HttpContext.Current.Request.Params["MERCHANTID"];
            string suppKey = this.suppKey;
            string sign = BestPayUtil.getSign(UPTRANSEQ, MERCHANTID, str1, str3, RETNCODE, RETNINFO, TRANDATE, BestPayUtil.Charset, suppKey);
            if (!(str10 == sign))
                return;
            string msg = "支付失败" + RETNINFO;
            string opstate = "-1";
            int status = 4;
            if (RETNCODE.Equals("0000"))
            {
                msg = "支付成功";
                opstate = "0";
                status = 2;
            }
            string str11 = string.Empty;
            new OrderBank().DoBankComplete(Bank.suppId, str1, "", status, opstate, msg, Decimal.Parse(str3) / new Decimal(100), new Decimal(0), false, true);
            HttpContext.Current.Response.Write("UPTRANSEQ_" + UPTRANSEQ);
        }

        public string GetBankCode(string paymodeId)
        {
            string str1 = string.Empty;
            string str2;
            switch (paymodeId)
            {
                case "970":
                    str2 = "CMB";
                    break;
                case "967":
                    str2 = "ICBC";
                    break;
                case "964":
                    str2 = "ABC";
                    break;
                case "965":
                    str2 = "CCB";
                    break;
                case "963":
                    str2 = "BOC";
                    break;
                case "977":
                    str2 = "SPDB";
                    break;
                case "981":
                    str2 = "BCOM";
                    break;
                case "980":
                    str2 = "CMBC";
                    break;
                case "974":
                    str2 = "SDB";
                    break;
                case "985":
                    str2 = "GDB";
                    break;
                case "962":
                    str2 = "CITIC";
                    break;
                case "982":
                    str2 = "HXB";
                    break;
                case "972":
                    str2 = "CIB";
                    break;
                case "984":
                    str2 = "GZRCC";
                    break;
                case "995":
                    str2 = "GZBC";
                    break;
                case "976":
                    str2 = "SHRCC";
                    break;
                case "971":
                    str2 = "POST";
                    break;
                case "989":
                    str2 = "BOB";
                    break;
                case "988":
                    str2 = "CBHB";
                    break;
                case "990":
                    str2 = "BJRCB";
                    break;
                case "979":
                    str2 = "NJCB";
                    break;
                case "986":
                    str2 = "CEB";
                    break;
                case "987":
                    str2 = "BEA";
                    break;
                case "998":
                    str2 = "NBCB";
                    break;
                case "983":
                    str2 = "HZB";
                    break;
                case "978":
                    str2 = "PAB";
                    break;
                case "999":
                    str2 = "HSB";
                    break;
                case "968":
                    str2 = "CZB";
                    break;
                case "1032":
                    str2 = "UPQP";
                    break;
                case "993":
                    str2 = "TENPAY";
                    break;
                case "992":
                    str2 = "ALIPAY";
                    break;
                default:
                    str2 = "UPQP";
                    break;
            }
            return str2;
        }
    }
}
