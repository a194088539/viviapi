using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.WxPayAPI
{
    public class WxPayConfig
    {
        private SupplierInfo suppInfo = SupplierFactory.GetCacheModel(99);
        public const string APPID = "wx2428e34e0e7dc6ef";
        public const string MCHID = "1233410002";
        public const string KEY = "e10adc3849ba56abbe56e056f20f883e";
        public const string APPSECRET = "51c56b886b5be869567dd389b3e5d1d6";
        public const string SSLCERT_PATH = "cert/apiclient_cert.p12";
        public const string SSLCERT_PASSWORD = "1233410002";
        public const string NOTIFY_URL = "http://paysdk.weixin.qq.com/example/ResultNotifyPage.aspx";
        public const string IP = "8.8.8.8";
        public const string PROXY_URL = "http://0.0.0.0:0";
        public const int REPORT_LEVENL = 0;
        public const int LOG_LEVENL = 0;
    }
}
