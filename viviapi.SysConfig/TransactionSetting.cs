using System;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
    public sealed class TransactionSetting
    {
        private static readonly string _group = "TransactionSettings";

        public static string SettingGroup
        {
            get
            {
                return TransactionSetting._group;
            }
        }

        public static Decimal MinTranATM
        {
            get
            {
                Decimal result = new Decimal(2, 0, 0, false, (byte)2);
                Decimal.TryParse(ConfigHelper.GetConfig(TransactionSetting.SettingGroup, "mintransactionamount"), out result);
                return result;
            }
        }

        public static Decimal MaxTranATM
        {
            get
            {
                Decimal result = new Decimal(5000);
                Decimal.TryParse(ConfigHelper.GetConfig(TransactionSetting.SettingGroup, "maxtransactionamount"), out result);
                return result;
            }
        }

        public static int ExpiresTime
        {
            get
            {
                int result = 300;
                int.TryParse(ConfigHelper.GetConfig(TransactionSetting.SettingGroup, "orderCacheExpiresTime"), out result);
                return result;
            }
        }
    }
}
