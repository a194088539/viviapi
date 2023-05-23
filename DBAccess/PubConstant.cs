namespace DBAccess
{
    using System.Configuration;
    using viviapi.SysConfig;
    using viviLib.Security;

    public class PubConstant
    {
        public static string GetConnectionString(string configName)
        {
            string strToDecrypt = ConfigurationManager.AppSettings[configName];
            string str2 = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (str2 == "true")
            {
                strToDecrypt = Cryptography.RijndaelDecrypt(strToDecrypt);
            }
            return strToDecrypt;
        }

        public static string ConnectionString
        {
            get
            {
                return Cryptography.RijndaelDecrypt(RuntimeSetting.ConnectString);
            }
        }
    }
}

