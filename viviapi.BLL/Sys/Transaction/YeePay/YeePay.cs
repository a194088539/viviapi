namespace viviapi.BLL.Sys.Transaction.YeePay
{
    public class Helper
    {
        public static bool CheckSign(string merchantId, string p2_Order, string p3_Amt, string p4_Cur, string p5_Pid, string p6_Pcat, string p7_Pdesc, string p8_Url, string p9_SAF, string pa_MP, string pd_FrpId, string pr_NeedRespone, string keyValue, string sign)
        {
            string str = Digest.HmacSign("" + "Buy" + merchantId + p2_Order + p3_Amt + p4_Cur + p5_Pid + p6_Pcat + p7_Pdesc + p8_Url + p9_SAF + pa_MP + pd_FrpId + pr_NeedRespone, keyValue);
            return sign == str;
        }
    }
}
