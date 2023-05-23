namespace com.yeepay
{
    public abstract class BuyOld : FormatQueryString
    {
        private static string nodeAuthorizationURL = "https://www.yeepay.com/app-merchant-proxy/node";

        public static string NodeAuthorizationURL
        {
            get
            {
                return BuyOld.nodeAuthorizationURL;
            }
            set
            {
                BuyOld.nodeAuthorizationURL = value;
            }
        }

        public static string CreateForm(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId, string formId)
        {
            return Buy.CreateBuyForm(p1_MerId, keyValue, p2_Order, p3_Amt, "CNY", p5_Pid, "", "", p8_Url, p9_SAF, pa_MP, pd_FrpId, "", "", "1", formId);
        }

        public static string CreateUrl(string p1_MerId, string keyValue, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId)
        {
            return Buy.CreateBuyUrl(p1_MerId, keyValue, p2_Order, p3_Amt, "CNY", p5_Pid, "", "", p8_Url, p9_SAF, pa_MP, pd_FrpId, "", "", "1");
        }

        public static QueryOrdResult QueryOrder(string p1_MerId, string keyValue, string p2_Order)
        {
            string aValue = "" + "QueryOrdDetail" + p1_MerId + p2_Order;
            string para = "" + "?p0_Cmd=QueryOrdDetail&p1_MerId=" + p1_MerId + "&p2_Order=" + p2_Order + "&hmac=" + Digest.HmacSign(aValue, keyValue);
            string strUrl = HttpUtils.SendRequest(BuyOld.nodeAuthorizationURL, para);
            FormatQueryString.GetQueryString("r0_Cmd", strUrl, '\n');
            string queryString1 = FormatQueryString.GetQueryString("r1_Code", strUrl, '\n');
            string queryString2 = FormatQueryString.GetQueryString("r2_TrxId", strUrl, '\n');
            string queryString3 = FormatQueryString.GetQueryString("r3_Amt", strUrl, '\n');
            FormatQueryString.GetQueryString("r4_Cur", strUrl, '\n');
            string queryString4 = FormatQueryString.GetQueryString("r5_Pid", strUrl, '\n');
            string queryString5 = FormatQueryString.GetQueryString("r6_Order", strUrl, '\n');
            string queryString6 = FormatQueryString.GetQueryString("r8_MP", strUrl, '\n');
            string queryString7 = FormatQueryString.GetQueryString("rb_PayStatus", strUrl, '\n');
            string queryString8 = FormatQueryString.GetQueryString("rc_RefundCount", strUrl, '\n');
            FormatQueryString.GetQueryString("rd_RefundAmt", strUrl, '\n');
            FormatQueryString.GetQueryString("hmac", strUrl, '\n');
            return new QueryOrdResult(queryString1, queryString2, queryString3, queryString4, queryString5, queryString7, queryString6, queryString3, queryString8);
        }
    }
}
