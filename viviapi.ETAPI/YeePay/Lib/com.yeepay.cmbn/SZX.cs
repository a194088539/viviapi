using System.Text;
using System.Web;

namespace com.yeepay.cmbn
{
    public abstract class SZX : FormatQueryString
    {
        private static string logFileName = "c:/YeePay_CARD.txt";
        private static string nodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/command.action";

        public static string LogFileName
        {
            get
            {
                return SZX.logFileName;
            }
            set
            {
                SZX.logFileName = value;
            }
        }

        public static string NodeAuthorizationURL
        {
            get
            {
                return SZX.nodeAuthorizationURL;
            }
            set
            {
                SZX.nodeAuthorizationURL = value;
            }
        }

        public static SZXResult AnnulCard(string keyValue, string p1_MerId, string p2_Order, string p3_Amt, string p4_verifyAmt, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string pa_MP, string pa7_cardAmt, string pa8_cardNo, string pa9_cardPwd, string pd_FrpId, string pr_NeedResponse, string pz_userId, string pz1_userRegTime)
        {
            string str1 = "" + "ChargeCardDirect" + p1_MerId + p2_Order + p3_Amt + p4_verifyAmt + p5_Pid + p6_Pcat + p7_Pdesc + p8_Url + pa_MP + pa7_cardAmt + pa8_cardNo + pa9_cardPwd + pd_FrpId + pr_NeedResponse + pz_userId + pz1_userRegTime;
            string hmac = Digest.HmacSign(str1, keyValue);
            SZX.logHmac(p2_Order, str1, keyValue, hmac);
            string para = "" + "?p0_Cmd=ChargeCardDirect&p1_MerId=" + p1_MerId + "&p2_Order=" + p2_Order + "&p3_Amt=" + p3_Amt + "&p4_verifyAmt=" + p4_verifyAmt + "&p5_Pid=" + HttpUtility.UrlEncode(p5_Pid, Encoding.GetEncoding("gb2312")) + "&p6_Pcat=" + HttpUtility.UrlEncode(p6_Pcat, Encoding.GetEncoding("gb2312")) + "&p7_Pdesc=" + HttpUtility.UrlEncode(p7_Pdesc, Encoding.GetEncoding("gb2312")) + "&p8_Url=" + HttpUtility.UrlEncode(p8_Url, Encoding.GetEncoding("gb2312")) + "&pa_MP=" + HttpUtility.UrlEncode(pa_MP, Encoding.GetEncoding("gb2312")) + "&pa7_cardAmt=" + HttpUtility.UrlEncode(pa7_cardAmt, Encoding.GetEncoding("gb2312")) + "&pa8_cardNo=" + HttpUtility.UrlEncode(pa8_cardNo, Encoding.GetEncoding("gb2312")) + "&pa9_cardPwd=" + HttpUtility.UrlEncode(pa9_cardPwd, Encoding.GetEncoding("gb2312")) + "&pd_FrpId=" + pd_FrpId + "&pr_NeedResponse=" + pr_NeedResponse + "&pz_userId=" + pz_userId + "&pz1_userRegTime=" + HttpUtility.UrlEncode(pz1_userRegTime, Encoding.GetEncoding("gb2312")) + "&hmac=" + hmac;
            SZX.logURL(SZX.nodeAuthorizationURL + para);
            string str2 = HttpUtils.SendRequest(SZX.nodeAuthorizationURL, para);
            SZX.logReqResult(str2);
            return new SZXResult(FormatQueryString.GetQueryString("r0_Cmd", str2, '\n'), FormatQueryString.GetQueryString("r1_Code", str2, '\n'), FormatQueryString.GetQueryString("r6_Order", str2, '\n'), FormatQueryString.GetQueryString("rq_ReturnMsg", str2, '\n'), FormatQueryString.GetQueryString("hmac", str2, '\n'), SZX.nodeAuthorizationURL + para, str2);
        }

        public static void logHmac(string orderid, string str, string keyValue, string hmac)
        {
        }

        public static void logReqResult(string reqResult)
        {
        }

        public static void logURL(string url)
        {
        }

        public static void setLogDir(string logDir)
        {
            log.logdir = logDir;
        }

        public static SZXCallbackResult VerifyCallback(string keyValue, string r0_Cmd, string r1_Code, string p1_MerId, string p2_Order, string p3_Amt, string p4_FrpId, string p5_CardNo, string p6_confirmAmount, string p7_realAmount, string p8_cardStatus, string p9_MP, string pb_BalanceAmt, string pc_BalanceAct, string hmac)
        {
            string str1 = "" + r0_Cmd + r1_Code + p1_MerId + p2_Order + p3_Amt + p4_FrpId + p5_CardNo + p6_confirmAmount + p7_realAmount + p8_cardStatus + p9_MP + pb_BalanceAmt + pc_BalanceAct;
            string str2 = Digest.HmacSign(str1, keyValue);
            SZX.logHmac(p2_Order, str1, keyValue, hmac);
            if (str2 == hmac)
                return new SZXCallbackResult(r0_Cmd, r1_Code, p1_MerId, p2_Order, p3_Amt, p4_FrpId, p5_CardNo, p6_confirmAmount, p7_realAmount, p8_cardStatus, p9_MP, pb_BalanceAmt, pc_BalanceAct, hmac, "");
            else
                return new SZXCallbackResult(r0_Cmd, r1_Code, p1_MerId, p2_Order, p3_Amt, p4_FrpId, p5_CardNo, p6_confirmAmount, p7_realAmount, p8_cardStatus, p9_MP, pb_BalanceAmt, pc_BalanceAct, hmac, Digest.HmacSign(str1, keyValue) + "<br>sbOld:" + str1);
        }
    }
}
