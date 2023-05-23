using System.Text;
using System.Web;

namespace com.yeepay
{
    public abstract class SZX
    {
        private static string nodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/command.action";

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

        public static SZXResult AnnulCard(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p8_Url, string pa_MP, string pa7_cardNo, string pa8_cardPwd)
        {
            return SZX.AnnulCard(p1_MerId, keyValue, p2_Order, p3_Amt, p8_Url, pa_MP, pa7_cardNo, pa8_cardPwd, "", "", "1");
        }

        public static SZXResult AnnulCard(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p8_Url, string pa_MP, string pa7_cardNo, string pa8_cardPwd, string pa0_Mode, string pr_NeedResponse)
        {
            return SZX.AnnulCard(p1_MerId, keyValue, p2_Order, p3_Amt, p8_Url, pa_MP, pa7_cardNo, pa8_cardPwd, "", "", "1");
        }

        public static SZXResult AnnulCard(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p8_Url, string pa_MP, string pa7_cardNo, string pa8_cardPwd, string pd_FrpId, string pa0_Mode, string pr_NeedResponse)
        {
            string str1 = "" + "AnnulCard" + p1_MerId + p2_Order + p3_Amt + p8_Url;
            if (pa0_Mode == "3")
                str1 = str1 + pa_MP + DES.Encrypt3DESSZX(pa7_cardNo, keyValue) + DES.Encrypt3DESSZX(pa8_cardPwd, keyValue);
            else if (pa0_Mode == "")
                str1 = str1 + pa_MP + pa7_cardNo + pa8_cardPwd;
            string aValue = str1 + pd_FrpId + pa0_Mode + pr_NeedResponse;
            string str2 = "" + "?p0_Cmd=AnnulCard&p1_MerId=" + p1_MerId + "&p2_Order=" + p2_Order + "&p3_Amt=" + p3_Amt + "&p8_Url=" + HttpUtility.UrlEncode(p8_Url, Encoding.GetEncoding("gb2312")) + "&pa_MP=" + HttpUtility.UrlEncode(pa_MP, Encoding.GetEncoding("gb2312"));
            if (pa0_Mode == "3")
                str2 = str2 + "&pa7_cardNo=" + HttpUtility.UrlEncode(DES.Encrypt3DESSZX(pa7_cardNo, keyValue), Encoding.GetEncoding("gb2312")) + "&pa8_cardPwd=" + HttpUtility.UrlEncode(DES.Encrypt3DESSZX(pa8_cardPwd, keyValue), Encoding.GetEncoding("gb2312"));
            else if (pa0_Mode == "")
                str2 = str2 + "&pa7_cardNo=" + pa7_cardNo + "&pa8_cardPwd=" + pa8_cardPwd;
            string para = str2 + "&pd_FrpId=" + pd_FrpId + "&pa0_Mode=" + pa0_Mode + "&pr_NeedResponse=" + pr_NeedResponse + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string str3 = HttpUtils.SendRequest(SZX.nodeAuthorizationURL, para);
            return new SZXResult(FormatQueryString.GetQueryString("r0_Cmd", str3, '\n'), FormatQueryString.GetQueryString("r1_Code", str3, '\n'), FormatQueryString.GetQueryString("r2_TrxId", str3, '\n'), FormatQueryString.GetQueryString("r6_Order", str3, '\n'), FormatQueryString.GetQueryString("rq_ReturnMsg", str3, '\n'), FormatQueryString.GetQueryString("hmac", str3, '\n'), SZX.nodeAuthorizationURL + para, str3);
        }

        public static SZXResultTest AnnulCardTest(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p8_Url, string pa_MP, string pa7_cardNo, string pa8_cardPwd)
        {
            return SZX.AnnulCardTest(p1_MerId, keyValue, p2_Order, p3_Amt, p8_Url, pa_MP, pa7_cardNo, pa8_cardPwd, "", "", "1");
        }

        public static SZXResultTest AnnulCardTest(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p8_Url, string pa_MP, string pa7_cardNo, string pa8_cardPwd, string pa0_Mode, string pr_NeedResponse)
        {
            return SZX.AnnulCardTest(p1_MerId, keyValue, p2_Order, p3_Amt, p8_Url, pa_MP, pa7_cardNo, pa8_cardPwd, "", "", "1");
        }

        public static SZXResultTest AnnulCardTest(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p8_Url, string pa_MP, string pa7_cardNo, string pa8_cardPwd, string pd_FrpId, string pa0_Mode, string pr_NeedResponse)
        {
            string url = "http://tech.yeepay.com:8080/robot/debug.action";
            string str1 = "" + "AnnulCard" + p1_MerId + p2_Order + p3_Amt + p8_Url;
            if (pa0_Mode == "3")
                str1 = str1 + pa_MP + DES.Encrypt3DESSZX(pa7_cardNo, keyValue) + DES.Encrypt3DESSZX(pa8_cardPwd, keyValue);
            else if (pa0_Mode == "")
                str1 = str1 + pa_MP + pa7_cardNo + pa8_cardPwd;
            string aValue = str1 + pd_FrpId + pa0_Mode + pr_NeedResponse;
            string str2 = "" + "?p0_Cmd=AnnulCard&p1_MerId=" + p1_MerId + "&p2_Order=" + p2_Order + "&p3_Amt=" + p3_Amt + "&p8_Url=" + HttpUtility.UrlEncode(p8_Url, Encoding.GetEncoding("gb2312")) + "&pa_MP=" + HttpUtility.UrlEncode(pa_MP, Encoding.GetEncoding("gb2312"));
            if (pa0_Mode == "3")
                str2 = str2 + "&pa7_cardNo=" + HttpUtility.UrlEncode(DES.Encrypt3DESSZX(pa7_cardNo, keyValue), Encoding.GetEncoding("gb2312")) + "&pa8_cardPwd=" + HttpUtility.UrlEncode(DES.Encrypt3DESSZX(pa8_cardPwd, keyValue), Encoding.GetEncoding("gb2312"));
            else if (pa0_Mode == "")
                str2 = str2 + "&pa7_cardNo=" + pa7_cardNo + "&pa8_cardPwd=" + pa8_cardPwd;
            string str3 = str2 + "&pd_FrpId=" + pd_FrpId + "&pa0_Mode=" + pa0_Mode + "&pr_NeedResponse=" + pr_NeedResponse + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string str4 = HttpUtils.SendRequest(url, str3);
            return new SZXResultTest(FormatQueryString.GetQueryString("r0_Cmd", str4, '\n'), FormatQueryString.GetQueryString("r1_Code", str4, '\n'), FormatQueryString.GetQueryString("r2_TrxId", str4, '\n'), FormatQueryString.GetQueryString("r6_Order", str4, '\n'), FormatQueryString.GetQueryString("rq_ReturnMsg", str4, '\n'), FormatQueryString.GetQueryString("hmac", str4, '\n'), url + str3, str4, str3);
        }

        public static string CreateForm(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pr_NeedRespone, string formId)
        {
            return Buy.CreateBuyForm(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, "SZX", "", "", pr_NeedRespone, formId);
        }

        public static string CreateFormWap(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pr_NeedRespone, string formId)
        {
            return Buy.CreateBuyForm(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, "SZX_WAP", "", "", pr_NeedRespone, formId);
        }

        public static string CreateUrl(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pr_NeedRespone)
        {
            return Buy.CreateBuyUrl(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, "SZX", "", "", pr_NeedRespone);
        }

        public static string CreateUrlWap(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pr_NeedRespone)
        {
            return Buy.CreateBuyUrl(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, "SZX_WAP", "", "", pr_NeedRespone);
        }

        public static bool VerifyCallback(string p1_MerId, string keyValue, string r0_Cmd, string r1_Code, string rb_Order, string r2_TrxId, string rc_Amt, string hmac)
        {
            return Digest.HmacSign("" + r0_Cmd + r1_Code + p1_MerId + rb_Order + r2_TrxId + rc_Amt, keyValue) == hmac;
        }

        public static SZXCallbackResult VerifyCallback(string p1_MerId, string keyValue, string r0_Cmd, string r1_Code, string rb_Order, string r2_TrxId, string pa_MP, string rc_Amt, string rq_CardNo, string hmac)
        {
            string aValue = "" + r0_Cmd + r1_Code + p1_MerId + rb_Order + r2_TrxId + pa_MP + rc_Amt;
            if (Digest.HmacSign(aValue, keyValue) == hmac)
                return new SZXCallbackResult(r0_Cmd, r1_Code, p1_MerId, rb_Order, r2_TrxId, pa_MP, rc_Amt, rq_CardNo, hmac, "");
            else
                return new SZXCallbackResult(r0_Cmd, r1_Code, p1_MerId, rb_Order, r2_TrxId, pa_MP, rc_Amt, rq_CardNo, hmac, Digest.HmacSign(aValue, keyValue) + "<br>sbOld:" + aValue);
        }

        public static bool VerifyCallback(string p1_MerId, string keyValue, string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string r5_Pid, string r6_Order, string r8_MP, string r9_BType, string hmac)
        {
            return Buy.VerifyCallback(p1_MerId, keyValue, r0_Cmd, r1_Code, r2_TrxId, r3_Amt, r4_Cur, r5_Pid, r6_Order, "", r8_MP, r9_BType, hmac);
        }

        public static BuyCallbackResult VerifyCallback(string p1_MerId, string keyValue, string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string r5_Pid, string r6_Order, string r7_Uid, string r8_MP, string r9_BType, string rp_PayDate, string hmac)
        {
            return Buy.VerifyCallback(p1_MerId, keyValue, r0_Cmd, r1_Code, r2_TrxId, r3_Amt, r4_Cur, r5_Pid, r6_Order, r7_Uid, r8_MP, r9_BType, rp_PayDate, hmac);
        }
    }
}
