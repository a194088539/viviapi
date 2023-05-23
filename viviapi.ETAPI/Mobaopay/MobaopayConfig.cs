using System.Collections.Generic;

namespace viviapi.ETAPI.Mobaopay
{
    public class MobaopayConfig
    {
        private static string mbp_key = "";
        private static string mobaopay_gateway = "";
        private static string mobaopay_api_version = "";
        private static string mobaopay_apiname_pay = "";
        private static string mobaopay_apiname_realpay = "";
        private static string mobaopay_apiname_query = "";
        private static string mobaopay_apiname_refund = "";
        private static string platform_id = "";
        private static string merchant_acc = "";
        private static string merchant_notify_url = "";
        private static Dictionary<string, string> bankCodeDict = new Dictionary<string, string>();

        public static string Mbp_key
        {
            get
            {
                return MobaopayConfig.mbp_key;
            }
            set
            {
                MobaopayConfig.mbp_key = value;
            }
        }

        public static string Mobaopay_gateway
        {
            get
            {
                return MobaopayConfig.mobaopay_gateway;
            }
            set
            {
                MobaopayConfig.mobaopay_gateway = value;
            }
        }

        public static string Mobaopay_api_version
        {
            get
            {
                return MobaopayConfig.mobaopay_api_version;
            }
            set
            {
                MobaopayConfig.mobaopay_api_version = value;
            }
        }

        public static string Mobaopay_apiname_pay
        {
            get
            {
                return MobaopayConfig.mobaopay_apiname_pay;
            }
            set
            {
                MobaopayConfig.mobaopay_apiname_pay = value;
            }
        }

        public static string Mobaopay_apiname_realpay
        {
            get
            {
                return MobaopayConfig.mobaopay_apiname_realpay;
            }
        }

        public static string Mobaopay_apiname_query
        {
            get
            {
                return MobaopayConfig.mobaopay_apiname_query;
            }
            set
            {
                MobaopayConfig.mobaopay_apiname_query = value;
            }
        }

        public static string Mobaopay_apiname_refund
        {
            get
            {
                return MobaopayConfig.mobaopay_apiname_refund;
            }
            set
            {
                MobaopayConfig.mobaopay_apiname_refund = value;
            }
        }

        public static string Platform_id
        {
            get
            {
                return MobaopayConfig.platform_id;
            }
            set
            {
                MobaopayConfig.platform_id = value;
            }
        }

        public static string Merchant_acc
        {
            get
            {
                return MobaopayConfig.merchant_acc;
            }
            set
            {
                MobaopayConfig.merchant_acc = value;
            }
        }

        public static string Merchant_notify_url
        {
            get
            {
                return MobaopayConfig.merchant_notify_url;
            }
            set
            {
                MobaopayConfig.merchant_notify_url = value;
            }
        }

        static MobaopayConfig()
        {
            MobaopayConfig.mbp_key = "22c41d776c24deddca95b1709a88f04b";
            MobaopayConfig.mobaopay_gateway = "https://trade.mobaopay.uat/cgi-bin/netpayment/pay_gate.cgi";
            MobaopayConfig.merchant_notify_url = "http://192.168.31.234/MBPExampleNet/Callback.aspx";
            MobaopayConfig.platform_id = "MerchTest";
            MobaopayConfig.merchant_acc = "210001110100250";
            MobaopayConfig.bankCodeDict.Add("ICBC", "ICBC");
            MobaopayConfig.bankCodeDict.Add("ABC", "ABC");
            MobaopayConfig.bankCodeDict.Add("BOC", "BOC");
            MobaopayConfig.bankCodeDict.Add("CCB", "CCB");
            MobaopayConfig.bankCodeDict.Add("COMM", "COMM");
            MobaopayConfig.bankCodeDict.Add("CMB", "CMB");
            MobaopayConfig.bankCodeDict.Add("SPDB", "SPDB");
            MobaopayConfig.bankCodeDict.Add("CIB", "CIB");
            MobaopayConfig.bankCodeDict.Add("CMBC", "CMBC");
            MobaopayConfig.bankCodeDict.Add("CGB", "CGB");
            MobaopayConfig.bankCodeDict.Add("CNCB", "CNCB");
            MobaopayConfig.bankCodeDict.Add("CEB", "CEB");
            MobaopayConfig.bankCodeDict.Add("HXB", "HXB");
            MobaopayConfig.bankCodeDict.Add("PSBC", "PSBC");
            MobaopayConfig.bankCodeDict.Add("PAB", "PAB");
            MobaopayConfig.mobaopay_api_version = "1.0.0.0";
            MobaopayConfig.mobaopay_apiname_pay = "WEB_PAY_B2C";
            MobaopayConfig.mobaopay_apiname_realpay = "CUST_REAL_PAY";
            MobaopayConfig.mobaopay_apiname_query = "MOBO_TRAN_QUERY";
            MobaopayConfig.mobaopay_apiname_refund = "MOBO_TRAN_RETURN";
        }
    }
}
