using System;
using System.IO;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
    public sealed class MongoDBSetting
    {
        private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configurations\\memcached.config");
        private static readonly string _group = "MongoDB";

        public static string SettingGroup
        {
            get
            {
                return MongoDBSetting._group;
            }
        }

        public static string Connstring
        {
            get
            {
                return ConfigHelper.GetConfig(MongoDBSetting.SettingGroup, "connStr");
            }
        }

        public static string DefaultDB
        {
            get
            {
                return ConfigHelper.GetConfig(MongoDBSetting.SettingGroup, "defaultdb");
            }
        }

        public static string CollectionName
        {
            get
            {
                return ConfigHelper.GetConfig(MongoDBSetting.SettingGroup, "collectionName");
            }
        }

        public static string WebSiteDescription
        {
            get
            {
                return ConfigHelper.GetConfig(MongoDBSetting.SettingGroup, "WebSiteDescription");
            }
        }

        public static string SiteDomain
        {
            get
            {
                return ConfigHelper.GetConfig(MongoDBSetting.SettingGroup, "Sitedomain");
            }
        }

        private MongoDBSetting()
        {
        }

        public static string GetConfig(string group, string key)
        {
            return ConfigHelper.GetConfig(MongoDBSetting._filePath, group, key);
        }
    }
}
