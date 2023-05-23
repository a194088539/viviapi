using System.Web;

namespace viviapi.ETAPI.AlipayWap
{
    public class Config
    {
        public static string partner = "";
        public static string seller_id = Config.partner;
        public static string key = "";
        public static string notify_url = "http://商户网关地址/alipay.wap.create.direct.pay.by.user-CSHARP-UTF-8/notify_url.aspx";
        public static string return_url = "http://商户网关地址/alipay.wap.create.direct.pay.by.user-CSHARP-UTF-8/return_url.aspx";
        public static string sign_type = "MD5";
        public static string log_path = ((object)HttpRuntime.AppDomainAppPath).ToString() + "log\\";
        public static string input_charset = "utf-8";
        public static string payment_type = "1";
        public static string service = "alipay.wap.create.direct.pay.by.user";
    }
}
