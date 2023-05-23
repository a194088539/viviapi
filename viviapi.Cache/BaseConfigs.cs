namespace viviapi.Cache
{
    using System.Configuration;

    public class BaseConfigs
    {
        private static string webPath = "";

        static BaseConfigs()
        {
            webPath = ConfigurationManager.AppSettings["WebPath"];
        }

        public static string GetPath
        {
            get
            {
                return webPath;
            }
        }
    }
}

