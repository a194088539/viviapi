using System;
using System.IO;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
    public class MemCachedConfig
    {
        private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configurations\\memcached.config");
        private static readonly string _group = "MemCachedConfig";

        public static string SettingGroup
        {
            get
            {
                return MemCachedConfig._group;
            }
        }

        public static bool ApplyMemCached
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(MemCachedConfig.GetConfig("ApplyMemCached"));
                }
                catch
                {
                    return false;
                }
            }
        }

        public static string ServerList
        {
            get
            {
                return MemCachedConfig.GetConfig("ServerList");
            }
        }

        public static string Weights
        {
            get
            {
                return MemCachedConfig.GetConfig("Weights");
            }
        }

        public static string PoolName
        {
            get
            {
                return MemCachedConfig.GetConfig("PoolName");
            }
        }

        public static int IntConnections
        {
            get
            {
                int num = Convert.ToInt32(MemCachedConfig.GetConfig("IntConnections"));
                return num > 0 ? num : 3;
            }
        }

        public static int MinConnections
        {
            get
            {
                int num = Convert.ToInt32(MemCachedConfig.GetConfig("MinConnections"));
                return num > 0 ? num : 3;
            }
        }

        public static int MaxConnections
        {
            get
            {
                int num = Convert.ToInt32(MemCachedConfig.GetConfig("MaxConnections"));
                return num > 0 ? num : 5;
            }
        }

        public static int SocketConnectTimeout
        {
            get
            {
                int num = Convert.ToInt32(MemCachedConfig.GetConfig("SocketConnectTimeout"));
                return num > 1000 ? num : 1000;
            }
        }

        public static int SocketTimeout
        {
            get
            {
                return Convert.ToInt32(MemCachedConfig.GetConfig("SocketTimeout")) > 1000 ? MemCachedConfig.MaintenanceSleep : 3000;
            }
        }

        public static int MaintenanceSleep
        {
            get
            {
                int num = Convert.ToInt32(MemCachedConfig.GetConfig("MaintenanceSleep"));
                return num > 0 ? num : 30;
            }
        }

        public static bool FailOver
        {
            get
            {
                return Convert.ToBoolean(MemCachedConfig.GetConfig("FailOver"));
            }
        }

        public static bool Nagle
        {
            get
            {
                return Convert.ToBoolean(MemCachedConfig.GetConfig("Nagle"));
            }
        }

        public string HashingAlgorithm
        {
            get
            {
                return MemCachedConfig.GetConfig("HashingAlgorithm");
            }
        }

        public static int LocalCacheTime
        {
            get
            {
                return Convert.ToInt32(MemCachedConfig.GetConfig("LocalCacheTime"));
            }
        }

        public static int MemCacheTime
        {
            get
            {
                return Convert.ToInt32(MemCachedConfig.GetConfig("MemCacheTime"));
            }
        }

        public static bool RecordeLog
        {
            get
            {
                return Convert.ToBoolean(MemCachedConfig.GetConfig("RecordeLog"));
            }
        }

        public static string SyncCacheUrl
        {
            get
            {
                return MemCachedConfig.GetConfig("SyncCacheUrl");
            }
        }

        public static string AuthCode
        {
            get
            {
                return MemCachedConfig.GetConfig("AuthCode");
            }
        }

        public static bool ApplyBase64
        {
            get
            {
                return Convert.ToBoolean(MemCachedConfig.GetConfig("ApplyBase64"));
            }
        }

        public static string GetConfig(string key)
        {
            return ConfigHelper.GetConfig(MemCachedConfig._filePath, MemCachedConfig._group, key);
        }
    }
}
