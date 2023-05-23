using System;
using System.IO;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
    public sealed class RuntimeSetting
    {
        private static readonly string _group = "runtimeSettings";

        public static string HostPort
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "HostPort");
            }
        }

        public static string HostName
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "HostName");
            }
        }

        public static string SMSUser
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SMSUser");
            }
        }

        public static string JXTURL
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "JXTURL");
            }
        }

        public static string SMUID
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SMUID");
            }
        }

        public static string SMPWD
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SMPWD");
            }
        }

        public static string SMSPassword
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SMSPassword");
            }
        }

        public static string SettingGroup
        {
            get
            {
                return RuntimeSetting._group;
            }
        }

        public static string Paycompletpage
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "Paycompletpage");
            }
        }

        public static string firstpage
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "firstpage");
            }
        }

        public static string CSSDomain
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "CSSDomain");
            }
        }

        public static string SqlDataUser
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SqlDataUser");
            }
        }

        public static string SystemName
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SystemName");
            }
        }

        public static string[] UserAgent
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "UserAgent").Split('|');
            }
        }

        public static string WebSiteKeywords
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "WebSiteKeywords");
            }
        }

        public static string ConnectString
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "ConnectString");
            }
        }

        public static string WebDAL
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "WebDAL");
            }
        }

        public static string OrdersDAL
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "OrdersDAL");
            }
        }

        public static string OrderStrategyAssembly
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "OrderStrategyAssembly");
            }
        }

        public static string OrderStrategyClass
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "OrderStrategyClass");
            }
        }

        public static string OrderCardStrategyAssembly
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "OrderCardStrategyAssembly");
            }
        }

        public static string OrderCardStrategyClass
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "OrderCardStrategyClass");
            }
        }

        public static string OrderSmsStrategyAssembly
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "OrderSmsStrategyAssembly");
            }
        }

        public static string OrderSmsStrategyClass
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "OrderSmsStrategyClass");
            }
        }

        public static string SiteDomain
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "Sitedomain");
            }
        }

        public static string Paydomain
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "Paydomain");
            }
        }

        public static int ServerId
        {
            get
            {
                try
                {
                    return int.Parse(ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "ServerId"));
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static int xiaoka_time_interval
        {
            get
            {
                try
                {
                    return int.Parse(ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "xiaoka_time_interval"));
                }
                catch
                {
                    return 1;
                }
            }
        }

        public static double DeductSafetyTime
        {
            get
            {
                try
                {
                    return double.Parse(ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "DeductSafetyTime"));
                }
                catch
                {
                    return 0.0;
                }
            }
        }

        public static int MaxDayToCashTimes
        {
            get
            {
                try
                {
                    return int.Parse(ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "MaxDayToCashTimes"));
                }
                catch
                {
                    return 1;
                }
            }
        }

        public static string SMSSN
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SMSSN");
            }
        }

        public static string SMSKEY
        {
            get
            {
                return ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "SMSKEY");
            }
        }

        public static string ManagePagePath
        {
            get
            {
                string config = ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "ManagePagePath");
                if (config == string.Empty)
                    return "Console";
                return config;
            }
        }

        public static string UrlManagerConfigPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configurations\\urlmanagerconfiguration.config");
            }
        }

        public static bool CheckUrlReferrer
        {
            get
            {
                string config = ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "CheckUrlReferrer");
                if (string.IsNullOrEmpty(config))
                    return true;
                return config == "1";
            }
        }

        public static bool CheckUserOrderNo
        {
            get
            {
                string config = ConfigHelper.GetConfig(RuntimeSetting.SettingGroup, "CheckUserOrderNo");
                if (string.IsNullOrEmpty(config))
                    return false;
                return config == "1";
            }
        }

        private RuntimeSetting()
        {
        }
    }
}
