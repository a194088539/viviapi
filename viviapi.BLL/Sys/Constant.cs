using System.Configuration;

namespace viviapi.BLL.Sys
{
    public sealed class Constant
    {
        public static readonly string ParameterEncryptionKey = "{3D666036-C28E-4e61-9F00-75CAB1829459}-{325BB0C1-1A4B-4072-BCEF-4784B5E089BC}-{DE4EB920-7850-4b2d-AA32-6DC76364B12A}";
        public static readonly string MailParameterEncryptionKey = "{D56D4ECF-65DF-4343-BB9C-D9816E4588C7}";

        public static string Cache_Mark
        {
            get
            {
                return ConfigurationManager.AppSettings["Cache_Mark"];
            }
        }
    }
}
