using System;
using System.Configuration;
using System.Text;
using System.Web;

namespace viviapi.ETAPI.BestPay
{
    public class BestPayUtil
    {
        public static string Charset = "UTF-8";
        public static string PayRequestUrl = "https://webpaywg.bestpay.com.cn/payWebDirect.do";
        public static string MERCHANTID = ConfigurationSettings.AppSettings["MERCHANTID"];
        public static string SUBMERCHANTID = ConfigurationSettings.AppSettings["SUBMERCHANTID"];
        public static string KEY = ConfigurationSettings.AppSettings["KEY"];
        public static string MERCHANTURL = ConfigurationSettings.AppSettings["MERCHANTURL"];
        public static string BACKMERCHANTURL = ConfigurationSettings.AppSettings["BACKMERCHANTURL"];

        public static string getRequestUrl(string ORDERSEQ, string ORDERREQTRANSEQ, string ORDERDATE, Decimal ORDERAMOUNT, Decimal PRODUCTAMOUNT, Decimal ATTACHAMOUNT, string CURTYPE, string ENCODETYPE, string ATTACH, string BUSICODE, string PRODUCTID, string TMNUM, string CUSTOMERID, string PRODUCTDESC, string DIVDETAILS, string PEDCNT, string CLIENTIP)
        {
            string str = string.Empty + "MERCHANTID=" + BestPayUtil.MERCHANTID + "&SUBMERCHANTID=" + BestPayUtil.SUBMERCHANTID + "&ORDERSEQ=" + ORDERSEQ + "&ORDERREQTRANSEQ=" + ORDERREQTRANSEQ + "&ORDERDATE=" + ORDERDATE + "&ORDERAMOUNT=1" + "&PRODUCTAMOUNT=1" + (object)"&ATTACHAMOUNT=" + (string)(object)0 + "&CURTYPE=RMB" + "&ENCODETYPE=1" + "&MERCHANTURL=" + HttpUtility.UrlEncode(BestPayUtil.MERCHANTURL, Encoding.GetEncoding(BestPayUtil.Charset)) + "&BACKMERCHANTURL=" + HttpUtility.UrlEncode(BestPayUtil.BACKMERCHANTURL, Encoding.GetEncoding(BestPayUtil.Charset)) + " &BANKID=CCB" + "&ATTACH=" + "&BUSICODE=" + BUSICODE + "&PRODUCTID=" + PRODUCTID + "&TMNUM=" + "&CUSTOMERID=" + CUSTOMERID + "&PRODUCTDESC=" + HttpUtility.UrlEncode(PRODUCTDESC, Encoding.GetEncoding(BestPayUtil.Charset)) + "&MAC=" + BestPayUtil.getMAC(BestPayUtil.MERCHANTID, ORDERSEQ, ORDERDATE, ORDERAMOUNT, CLIENTIP, BestPayUtil.KEY, BestPayUtil.Charset) + "&DIVDETAILS=" + "&PEDCNT=" + PEDCNT + "&CLIENTIP=" + CLIENTIP;
            return BestPayUtil.PayRequestUrl + "?" + str;
        }

        public static string getRequestForm(string ORDERSEQ, string ORDERREQTRANSEQ, string ORDERDATE, Decimal ORDERAMOUNT, Decimal PRODUCTAMOUNT, Decimal ATTACHAMOUNT, string CURTYPE, string ENCODETYPE, string ATTACH, string BUSICODE, string PRODUCTID, string TMNUM, string CUSTOMERID, string PRODUCTDESC, string DIVDETAILS, string PEDCNT, string CLIENTIP)
        {
            return "<form id=\"frmBestPay\" name=\"frmBestPay\" action=\"" + BestPayUtil.PayRequestUrl + "\" method=\"post\">\n" + "<input type=\"hidden\" name=\"MERCHANTID\" value=\"" + BestPayUtil.MERCHANTID + "\" />\n" + " <input type=\"hidden\" name=\"SUBMERCHANTID\" value=\"" + BestPayUtil.SUBMERCHANTID + "\" />\n" + " <input type=\"hidden\" name=\"ORDERSEQ\" value=\"" + ORDERSEQ + "\" />\n" + " <input type=\"hidden\" name=\"ORDERREQTRANSEQ\" value=\"" + ORDERREQTRANSEQ + "\" />\n" + " <input type=\"hidden\" name=\"ORDERDATE\" value=\"" + ORDERDATE + "\" />\n" + " <input type=\"hidden\" name=\"ORDERAMOUNT\" value=\"" + ORDERAMOUNT.ToString("0") + "\" />\n" + " <input type=\"hidden\" name=\"PRODUCTAMOUNT\" value=\"" + PRODUCTAMOUNT.ToString("0") + "\" />\n" + " <input type=\"hidden\" name=\"ATTACHAMOUNT\" value=\"" + ATTACHAMOUNT.ToString("0") + "\" />\n" + " <input type=\"hidden\" name=\"CURTYPE\" value=\"" + CURTYPE + "\" />\n" + " <input type=\"hidden\" name=\"ENCODETYPE\" value=\"" + ENCODETYPE + "\" />\n" + " <input type=\"hidden\" name=\"MERCHANTURL\" value=\"" + BestPayUtil.MERCHANTURL + "\" />\n" + " <input type=\"hidden\" name=\"BACKMERCHANTURL\" value=\"" + BestPayUtil.BACKMERCHANTURL + "\" />\n" + " <input type=\"hidden\" name=\"ATTACH\" value=\"" + ATTACH + "\" />\n" + " <input type=\"hidden\" name=\"BUSICODE\" value=\"" + BUSICODE + "\" />\n" + " <input type=\"hidden\" name=\"PRODUCTID\" value=\"" + PRODUCTID + "\" />\n" + " <input type=\"hidden\" name=\"TMNUM\" value=\"" + TMNUM + "\" />\n" + " <input type=\"hidden\" name=\"CUSTOMERID\" value=\"" + CUSTOMERID + "\" />\n" + " <input type=\"hidden\" name=\"PRODUCTDESC\" value=\"" + PRODUCTDESC + "\" />\n" + " <input type=\"hidden\" name=\"MAC\" value=\"" + BestPayUtil.getMAC(BestPayUtil.MERCHANTID, ORDERSEQ, ORDERDATE, ORDERAMOUNT, CLIENTIP, BestPayUtil.KEY, BestPayUtil.Charset) + "\" />\n" + " <input type=\"hidden\" name=\"DIVDETAILS\" value=\"" + DIVDETAILS + "\" />\n" + " <input type=\"hidden\" name=\"CLIENTIP\" value=\"" + CLIENTIP + "\" />\n" + "</form>\n" + "<script type=\"text/javascript\">\n" + " <!--\n" + "  document.getElementById(\"frmBestPay\").submit();\n" + " //-->\n" + "</script>\n";
        }

        public static string getMAC(string MERCHANTID, string ORDERSEQ, string ORDERDATE, Decimal ORDERAMOUNT, string CLIENTIP, string KEY, string Charset)
        {
            string str = string.Empty;
            return CryptTool.md5Digest("MERCHANTID=" + MERCHANTID + "&ORDERSEQ=" + ORDERSEQ + "&ORDERDATE=" + ORDERDATE + "&ORDERAMOUNT=" + ORDERAMOUNT.ToString("0") + "&CLIENTIP=" + CLIENTIP + "&KEY=" + KEY, Charset);
        }

        public static string getSign(string UPTRANSEQ, string MERCHANTID, string ORDERSEQ, string ORDERAMOUNT, string RETNCODE, string RETNINFO, string TRANDATE, string Charset, string KEY)
        {
            return CryptTool.md5Digest(string.Empty + "UPTRANSEQ=" + UPTRANSEQ + "&MERCHANTID=" + MERCHANTID + "&ORDERID=" + ORDERSEQ + "&PAYMENT=" + ORDERAMOUNT + "&RETNCODE=" + RETNCODE + "&RETNINFO=" + RETNINFO + "&PAYDATE=" + TRANDATE + "&KEY=" + KEY, Charset);
        }
    }
}
