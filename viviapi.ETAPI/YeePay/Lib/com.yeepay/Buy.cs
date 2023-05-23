using System.Text;
using System.Web;

namespace com.yeepay
{
    public abstract class Buy : BuyOld
    {
        private static string nodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/node";
        private static string nodeAuthorizationURL_Jun = "https://www.yeepay.com/app-merchant-proxy/command.action";

        public new static string NodeAuthorizationURL
        {
            get
            {
                return Buy.nodeAuthorizationURL;
            }
            set
            {
                BuyOld.NodeAuthorizationURL = Buy.nodeAuthorizationURL;
                Buy.nodeAuthorizationURL = value;
            }
        }

        public static string NodeAuthorizationURL_Jun
        {
            get
            {
                return Buy.nodeAuthorizationURL_Jun;
            }
            set
            {
                Buy.nodeAuthorizationURL_Jun = value;
            }
        }

        public static BuyBankDirectConnectResult BankDirectConnect(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string pa7_cardNo, string pa8_cardPwd, string pa_MP)
        {
            return Buy.BankDirectConnect(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, pa7_cardNo, pa8_cardPwd, pa_MP, "JUNNET-NET");
        }

        public static BuyBankDirectConnectResult BankDirectConnect(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string pa7_cardNo, string pa8_cardPwd, string pa_MP, string pd_FrpId)
        {
            string aValue = "" + "BankDirectConnect" + p1_MerId + p2_Order + p3_Amt + p4_Cur + p5_Pid + p6_Pcat + p7_Pdesc + DES.Encrypt3DESJW(pa7_cardNo, keyValue) + DES.Encrypt3DESJW(pa8_cardPwd, keyValue) + pa_MP + pd_FrpId;
            string para = "" + "?p0_Cmd=BankDirectConnect&p1_MerId=" + p1_MerId + "&p2_Order=" + p2_Order + "&p3_Amt=" + p3_Amt + "&p4_Cur=" + p4_Cur + "&p5_Pid=" + HttpUtility.UrlEncode(p5_Pid, Encoding.GetEncoding("gb2312")) + "&p6_Pcat=" + HttpUtility.UrlEncode(p6_Pcat, Encoding.GetEncoding("gb2312")) + "&p7_Pdesc=" + HttpUtility.UrlEncode(p7_Pdesc, Encoding.GetEncoding("gb2312")) + "&pa7_cardNo=" + HttpUtility.UrlEncode(DES.Encrypt3DESJW(pa7_cardNo, keyValue), Encoding.GetEncoding("gb2312")) + "&pa8_cardPwd=" + HttpUtility.UrlEncode(DES.Encrypt3DESJW(pa8_cardPwd, keyValue), Encoding.GetEncoding("gb2312")) + "&pa_MP=" + HttpUtility.UrlEncode(pa_MP, Encoding.GetEncoding("gb2312")) + "&pd_FrpId=" + pd_FrpId + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string str = HttpUtils.SendRequest(Buy.nodeAuthorizationURL_Jun, para);
            return new BuyBankDirectConnectResult(FormatQueryString.GetQueryString("r0_Cmd", str, '\n'), FormatQueryString.GetQueryString("r1_Code", str, '\n'), FormatQueryString.GetQueryString("r2_TrxId", str, '\n'), FormatQueryString.GetQueryString("r3_Amt", str, '\n'), FormatQueryString.GetQueryString("r4_Cur", str, '\n'), FormatQueryString.GetQueryString("r6_Order", str, '\n'), FormatQueryString.GetQueryString("ro_BankOrderId", str, '\n'), FormatQueryString.GetQueryString("r8_MP", str, '\n'), FormatQueryString.GetQueryString("hmac", str, '\n'), Buy.nodeAuthorizationURL_Jun + para, str);
        }

        public static string CreateBuyForm(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p5_Pid, string p8_Url, string pa_MP, string pd_FrpId, string formId)
        {
            return Buy.CreateBuyForm(p1_MerId, keyValue, p2_Order, p3_Amt, "CNY", p5_Pid, "", "", p8_Url, "", pa_MP, pd_FrpId, "", "", "1", formId);
        }

        public static string CreateBuyForm(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId, string pr_NeedRespone, string formId)
        {
            return Buy.CreateBuyForm(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, "", "", pr_NeedRespone, formId);
        }

        public static string CreateBuyForm(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId, string pm_Period, string pn_Unit, string pr_NeedRespone, string formId)
        {
            string aValue = "" + "Buy" + p1_MerId + p2_Order + p3_Amt + p4_Cur + p5_Pid + p6_Pcat + p7_Pdesc + p8_Url + p9_SAF + pa_MP + pd_FrpId + pm_Period + pn_Unit + pr_NeedRespone;
            return "" + "<form name='" + formId + "' action='" + Buy.nodeAuthorizationURL + "' method='POST'>\r<input type='hidden' name='p0_Cmd' value='Buy'>\r<input type='hidden' name='p1_MerId' value='" + p1_MerId + "'>\r<input type='hidden' name='p2_Order' value='" + p2_Order + "'>\r<input type='hidden' name='p3_Amt' value='" + p3_Amt + "'>\r<input type='hidden' name='p4_Cur' value='" + p4_Cur + "'>\r<input type='hidden' name='p5_Pid' value='" + p5_Pid + "'>\r<input type='hidden' name='p6_Pcat' value='" + p6_Pcat + "'>\r<input type='hidden' name='p7_Pdesc' value='" + p7_Pdesc + "'>\r<input type='hidden' name='p8_Url' value='" + p8_Url + "'>\r<input type='hidden' name='p9_SAF' value='" + p9_SAF + "'>\r<input type='hidden' name='pa_MP' value='" + pa_MP + "'>\r<input type='hidden' name='pd_FrpId' value='" + pd_FrpId + "'>\r<input type='hidden' name='pm_Period' value='" + pm_Period + "'>\r<input type='hidden' name='pn_Unit' value='" + pn_Unit + "'>\r<input type='hidden' name='pr_NeedResponse' value='" + pr_NeedRespone + "'>\r<input type='hidden' name='hmac' value='" + Digest.HmacSign(aValue, keyValue) + "'>\r<input type='hidden' name='noLoadingPage' value='1'></form>";
        }

        public static string CreateBuyUrl(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p5_Pid, string p8_Url, string pa_MP, string pd_FrpId)
        {
            return Buy.CreateBuyUrl(p1_MerId, keyValue, p2_Order, p3_Amt, "CNY", p5_Pid, "", "", p8_Url, "", pa_MP, pd_FrpId, "", "", "1");
        }

        public static string CreateBuyUrl(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId, string pr_NeedRespone)
        {
            return Buy.CreateBuyUrl(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, "", "", pr_NeedRespone);
        }

        public static string CreateBuyUrl(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId, string pm_Period, string pn_Unit, string pr_NeedRespone)
        {
            string aValue = "" + "Buy" + p1_MerId + p2_Order + p3_Amt + p4_Cur + p5_Pid + p6_Pcat + p7_Pdesc + p8_Url + p9_SAF + pa_MP + pd_FrpId + pm_Period + pn_Unit + pr_NeedRespone;
            return "" + Buy.nodeAuthorizationURL + "?p0_Cmd=Buy&p1_MerId=" + p1_MerId + "&p2_Order=" + HttpUtility.UrlEncode(p2_Order, Encoding.GetEncoding("gb2312")) + "&p3_Amt=" + p3_Amt + "&p4_Cur=" + p4_Cur + "&p5_Pid=" + HttpUtility.UrlEncode(p5_Pid, Encoding.GetEncoding("gb2312")) + "&p6_Pcat=" + HttpUtility.UrlEncode(p6_Pcat, Encoding.GetEncoding("gb2312")) + "&p7_Pdesc=" + HttpUtility.UrlEncode(p7_Pdesc, Encoding.GetEncoding("gb2312")) + "&p8_Url=" + HttpUtility.UrlEncode(p8_Url, Encoding.GetEncoding("gb2312")) + "&p9_SAF=" + p9_SAF + "&pa_MP=" + HttpUtility.UrlEncode(pa_MP, Encoding.GetEncoding("gb2312")) + "&pd_FrpId=" + pd_FrpId + "&pm_Period=" + pm_Period + "&pn_Unit=" + pn_Unit + "&pr_NeedResponse=" + pr_NeedRespone + "&hmac=" + Digest.HmacSign(aValue, keyValue);
        }

        public static BuyMotoOrdResult MotoOrd(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p7_Pdesc, string p8_Url, string pa_MP, string pd_FrpId, string pe_BuyerTel, string pf_BuyerName, string pg_BuyerAddr, string pm_Period, string pn_CheckType, string pn_Unit, string pr_NeedResponse)
        {
            string aValue = "" + "MotoOrd" + p1_MerId + p2_Order + p3_Amt + p4_Cur + p5_Pid + p7_Pdesc + p8_Url + pa_MP + pd_FrpId + pe_BuyerTel + pf_BuyerName + pg_BuyerAddr + pm_Period + pn_CheckType + pn_Unit + pr_NeedResponse;
            string para = "" + "?p0_Cmd=MotoOrd&p1_MerId=" + p1_MerId + "&p2_Order=" + p2_Order + "&p3_Amt=" + p3_Amt + "&p4_Cur=" + p4_Cur + "&p5_Pid=" + HttpUtility.UrlEncode(p5_Pid, Encoding.GetEncoding("gb2312")) + "&p7_Pdesc=" + HttpUtility.UrlEncode(p7_Pdesc, Encoding.GetEncoding("gb2312")) + "&p8_Url=" + HttpUtility.UrlEncode(p8_Url, Encoding.GetEncoding("gb2312")) + "&pa_MP=" + HttpUtility.UrlEncode(pa_MP, Encoding.GetEncoding("gb2312")) + "&pd_FrpId=" + pd_FrpId + "&pe_BuyerTel=" + pe_BuyerTel + "&pf_BuyerName=" + HttpUtility.UrlEncode(pf_BuyerName, Encoding.GetEncoding("gb2312")) + "&pg_BuyerAddr=" + HttpUtility.UrlEncode(pg_BuyerAddr, Encoding.GetEncoding("gb2312")) + "&pm_Period=" + pm_Period + "&pn_CheckType=" + pn_CheckType + "&pn_Unit=" + pn_Unit + "&pr_NeedResponse=" + pr_NeedResponse + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string strUrl = HttpUtils.SendRequest(Buy.nodeAuthorizationURL, para);
            return new BuyMotoOrdResult(FormatQueryString.GetQueryString("r1_Code", strUrl, '\n'), FormatQueryString.GetQueryString("rd_MotoId", strUrl, '\n'), FormatQueryString.GetQueryString("r3_Amt", strUrl, '\n'), FormatQueryString.GetQueryString("r4_Cur", strUrl, '\n'), FormatQueryString.GetQueryString("r5_Pid", strUrl, '\n'), FormatQueryString.GetQueryString("r6_Order", strUrl, '\n'), FormatQueryString.GetQueryString("re_OrderIndex", strUrl, '\n'), FormatQueryString.GetQueryString("hmac", strUrl, '\n'));
        }

        public static BuyQueryOrdDetailResult QueryOrdDetail(string p1_MerId, string keyValue, string p2_Order)
        {
            string aValue = "" + "QueryOrdDetail" + p1_MerId + p2_Order;
            string para = "" + "?p0_Cmd=QueryOrdDetail&p1_MerId=" + p1_MerId + "&p2_Order=" + p2_Order + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string strUrl = HttpUtils.SendRequest(Buy.nodeAuthorizationURL, para);
            return new BuyQueryOrdDetailResult(FormatQueryString.GetQueryString("r0_Cmd", strUrl, '\n'), FormatQueryString.GetQueryString("r1_Code", strUrl, '\n'), FormatQueryString.GetQueryString("r2_TrxId", strUrl, '\n'), FormatQueryString.GetQueryString("r3_Amt", strUrl, '\n'), FormatQueryString.GetQueryString("r4_Cur", strUrl, '\n'), FormatQueryString.GetQueryString("r5_Pid", strUrl, '\n'), FormatQueryString.GetQueryString("r6_Order", strUrl, '\n'), FormatQueryString.GetQueryString("r8_MP", strUrl, '\n'), FormatQueryString.GetQueryString("rb_PayStatus", strUrl, '\n'), FormatQueryString.GetQueryString("rc_RefundCount", strUrl, '\n'), FormatQueryString.GetQueryString("rd_RefundAmt", strUrl, '\n'), FormatQueryString.GetQueryString("hmac", strUrl, '\n'));
        }

        public static BuyRefundOrdResult RefundOrd(string p1_MerId, string keyValue, string pb_TrxId, string p3_Amt, string p4_Cur, string p5_Desc)
        {
            string aValue = "" + "RefundOrd" + p1_MerId + pb_TrxId + p3_Amt + p4_Cur + p5_Desc;
            string para = "" + "?p0_Cmd=RefundOrd&p1_MerId=" + p1_MerId + "&pb_TrxId=" + pb_TrxId + "&p3_Amt=" + p3_Amt + "&p4_Cur=" + p4_Cur + "&p5_Desc=" + HttpUtility.UrlEncode(p5_Desc, Encoding.GetEncoding("gb2312")) + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string strUrl = HttpUtils.SendRequest(Buy.nodeAuthorizationURL, para);
            return new BuyRefundOrdResult(FormatQueryString.GetQueryString("r0_Cmd", strUrl, '\n'), FormatQueryString.GetQueryString("r1_Code", strUrl, '\n'), FormatQueryString.GetQueryString("r2_TrxId", strUrl, '\n'), FormatQueryString.GetQueryString("r3_Amt", strUrl, '\n'), FormatQueryString.GetQueryString("r4_Cur", strUrl, '\n'), FormatQueryString.GetQueryString("hmac", strUrl, '\n'));
        }

        public static bool VerifyCallback(string p1_MerId, string keyValue, string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string r5_Pid, string r6_Order, string r7_Uid, string r8_MP, string r9_BType, string hmac)
        {
            return Digest.HmacSign("" + p1_MerId + r0_Cmd + r1_Code + r2_TrxId + r3_Amt + r4_Cur + r5_Pid + r6_Order + r7_Uid + r8_MP + r9_BType, keyValue) == hmac;
        }

        public static BuyCallbackResult VerifyCallback(string p1_MerId, string keyValue, string r0_Cmd, string r1_Code, string r2_TrxId, string r3_Amt, string r4_Cur, string r5_Pid, string r6_Order, string r7_Uid, string r8_MP, string r9_BType, string rp_PayDate, string hmac)
        {
            if (Digest.HmacSign("" + p1_MerId + r0_Cmd + r1_Code + r2_TrxId + r3_Amt + r4_Cur + r5_Pid + r6_Order + r7_Uid + r8_MP + r9_BType, keyValue) == hmac)
                return new BuyCallbackResult(p1_MerId, r0_Cmd, r1_Code, r2_TrxId, r3_Amt, r4_Cur, r5_Pid, r6_Order, r7_Uid, r8_MP, r9_BType, rp_PayDate, hmac, "");
            else
                return new BuyCallbackResult(p1_MerId, r0_Cmd, r1_Code, r2_TrxId, r3_Amt, r4_Cur, r5_Pid, r6_Order, r7_Uid, r8_MP, r9_BType, rp_PayDate, hmac, "交易签名被篡改");
        }
    }
}
