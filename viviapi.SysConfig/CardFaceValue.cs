using viviLib.Configuration;

namespace viviapi.SysConfig
{
    public class CardFaceValue
    {
        private static readonly string _group = "cardfacevalue";

        public static string SettingGroup
        {
            get
            {
                return CardFaceValue._group;
            }
        }

        public static string S0001
        {
            get
            {
                return ConfigHelper.GetConfig(CardFaceValue.SettingGroup, "0001");
            }
        }

        public static string S0001ZJ
        {
            get
            {
                return ConfigHelper.GetConfig(CardFaceValue.SettingGroup, "0001ZJ");
            }
        }

        public static string S0001FJ
        {
            get
            {
                return ConfigHelper.GetConfig(CardFaceValue.SettingGroup, "0001FJ");
            }
        }

        public static string S0001GD
        {
            get
            {
                return ConfigHelper.GetConfig(CardFaceValue.SettingGroup, "0001ZJ");
            }
        }

        public static string S0001LN
        {
            get
            {
                return ConfigHelper.GetConfig(CardFaceValue.SettingGroup, "0001LN");
            }
        }
    }
}
