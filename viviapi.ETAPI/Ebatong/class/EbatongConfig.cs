using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI.Ebatong
{
    public class Config
    {
        private static string partner = "";
        private static string key = "";
        private static string input_charset = "";
        private static string sign_type = "";

        public static string Partner
        {
            get
            {
                return Config.partner;
            }
            set
            {
                Config.partner = value;
            }
        }

        public static string Key
        {
            get
            {
                return Config.key;
            }
            set
            {
                Config.key = value;
            }
        }

        public static string Input_charset
        {
            get
            {
                return Config.input_charset;
            }
        }

        public static string Sign_type
        {
            get
            {
                return Config.sign_type;
            }
        }

        static Config()
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(1004);
            Config.partner = cacheModel.puserid;
            Config.key = cacheModel.puserkey;
            Config.input_charset = "UTF-8";
            Config.sign_type = "MD5";
        }
    }
}
