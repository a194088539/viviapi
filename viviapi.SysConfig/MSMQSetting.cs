using viviLib.Configuration;

namespace viviapi.SysConfig
{
    public sealed class MSMQSetting
    {
        private static readonly string _group = "MSMQ";

        public static string SettingGroup
        {
            get
            {
                return MSMQSetting._group;
            }
        }

        public static string OrderMessaging
        {
            get
            {
                return ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "OrderMessaging");
            }
        }

        public static string BankOrderPath
        {
            get
            {
                return ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "BankOrderPath");
            }
        }

        public static string BankNotifyPath
        {
            get
            {
                return ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "BankNotifyPath");
            }
        }

        public static string CardOrderPath
        {
            get
            {
                return ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "CardOrderPath");
            }
        }

        public static string CardNotifyPath
        {
            get
            {
                return ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "CardNotifyPath");
            }
        }

        public static string SmsOrderPath
        {
            get
            {
                return ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "SmsOrderPath");
            }
        }

        public static string SmsNotifyPath
        {
            get
            {
                return ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "SmsNotifyPath");
            }
        }

        public static int TransactionTimeout
        {
            get
            {
                int result = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "TransactionTimeout"), out result))
                    result = 30;
                return result;
            }
        }

        public static int QueueTimeout
        {
            get
            {
                int result = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "QueueTimeout"), out result))
                    result = 20;
                return result;
            }
        }

        public static int BatchSize
        {
            get
            {
                int result = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "BatchSize"), out result))
                    result = 10;
                return result;
            }
        }

        public static int ThreadCount
        {
            get
            {
                int result = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "ThreadCount"), out result))
                    result = 2;
                return result;
            }
        }

        public static int NotifyTransactionTimeout
        {
            get
            {
                int result = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyTransactionTimeout"), out result))
                    result = 30;
                return result;
            }
        }

        public static int NotifyQueueTimeout
        {
            get
            {
                int result = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyQueueTimeout"), out result))
                    result = 20;
                return result;
            }
        }

        public static int NotifyBatchSize
        {
            get
            {
                int result = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyBatchSize"), out result))
                    result = 10;
                return result;
            }
        }

        public static int NotifyThreadCount
        {
            get
            {
                int result = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyThreadCount"), out result))
                    result = 2;
                return result;
            }
        }

        public static int TransactionTimeout_CardOrder
        {
            get
            {
                int result = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "TransactionTimeout_CardOrder"), out result))
                    result = 30;
                return result;
            }
        }

        public static int QueueTimeout_CardOrder
        {
            get
            {
                int result = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "QueueTimeout_CardOrder"), out result))
                    result = 20;
                return result;
            }
        }

        public static int BatchSize_CardOrder
        {
            get
            {
                int result = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "BatchSize_CardOrder"), out result))
                    result = 10;
                return result;
            }
        }

        public static int ThreadCount_CardOrder
        {
            get
            {
                int result = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "ThreadCount_CardOrder"), out result))
                    result = 2;
                return result;
            }
        }

        public static int NotifyTransactionTimeout_Card
        {
            get
            {
                int result = 30;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyTransactionTimeout_Card"), out result))
                    result = 30;
                return result;
            }
        }

        public static int NotifyQueueTimeout_Card
        {
            get
            {
                int result = 20;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyQueueTimeout_Card"), out result))
                    result = 20;
                return result;
            }
        }

        public static int NotifyBatchSize_Card
        {
            get
            {
                int result = 10;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyBatchSize_Card"), out result))
                    result = 10;
                return result;
            }
        }

        public static int NotifyThreadCount_Card
        {
            get
            {
                int result = 2;
                if (!int.TryParse(ConfigHelper.GetConfig(MSMQSetting.SettingGroup, "NotifyThreadCount_Card"), out result))
                    result = 2;
                return result;
            }
        }

        private MSMQSetting()
        {
        }
    }
}
