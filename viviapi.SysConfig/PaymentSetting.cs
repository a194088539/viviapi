using System;
using viviLib.Configuration;

namespace viviapi.SysConfig
{
    public sealed class PaymentSetting
    {
        private static readonly string _group = "paymentSettings";

        public static string SettingGroup
        {
            get
            {
                return PaymentSetting._group;
            }
        }

        public static bool showjubao
        {
            get
            {
                try
                {
                    string config = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "showjubao");
                    if (!string.IsNullOrEmpty(config))
                        return Convert.ToInt32(config) > 0;
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static string jumpUrl
        {
            get
            {
                try
                {
                    return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "jumpUrl");
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }

        public static string alipay_body
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "alipay_body");
            }
        }

        public static string weixin_body
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "weixin_body");
            }
        }

        public static string yeepay_pid
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "yeepay_pid");
            }
        }

        public static string ebatong_body
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "ebatong_body");
            }
        }

        public static string ebatong_url
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "ebatong_url");
            }
        }

        public static string ebatong_subject
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "ebatong_subject");
            }
        }

        public static string yeepay_pcat
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "yeepay_pcat");
            }
        }

        public static string yeepay_pdesc
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "yeepay_pdesc");
            }
        }

        public static string tftpay_MerLicences
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "tftpay_MerLicences");
            }
        }

        public static string tftpay_TBLicences
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "tftpay_TBLicences");
            }
        }

        public static string tftpay_PostAdd
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "tftpay_PostAdd");
            }
        }

        public static string tftpay_MerBusType
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "tftpay_MerBusType");
            }
        }

        public static string mengsmsarrCom
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "mengsmsarrCom");
            }
        }

        public static string shenzhoufucertificate
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "shenzhoufucertificate");
            }
        }

        public static string alipay_subject
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "alipay_subject");
            }
        }

        public static string weixin_subject
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "weixin_subject");
            }
        }

        public static string yisheng_buyer_realname
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "yisheng_buyer_realname");
            }
        }

        public static string KuaiQian_prikey_path
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "KuaiQian_prikey_path");
            }
        }

        public static string KuaiQian_pubkey_path
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "KuaiQian_pubkey_path");
            }
        }

        public static string Gopay_userType
        {
            get
            {
                string str = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "Gopay_userType");
                if (string.IsNullOrEmpty(str))
                    str = "1";
                return str;
            }
        }

        public static string switch_yeepay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "switch_yeepay_form_url");
            }
        }

        public static string switch_sdopay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "switch_sdopay_form_url");
            }
        }

        public static string switch_alipay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "switch_alipay_form_url");
            }
        }

        public static string switch_tenpay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "switch_tenpay_form_url");
            }
        }

        public static string switch_ipspay_form_url
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "switch_ipspay_form_url");
            }
        }

        public static string DinpayPrivateKey
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DinpayPrivateKey");
            }
        }

        public static string DinpayPublicKey
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DinpayPublicKey");
            }
        }

        public static string QyfPayMD5Key
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayMD5Key");
            }
        }

        public static string QyfPayPublicKey
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayPublicKey");
            }
        }

        public static string QyfDaiPayPublicKey
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfDaiPayPublicKey");
            }
        }

        public static string QyfPrivateKey
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPrivateKey");
            }
        }

        public static string DlbpayPrivateKey
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DdbillPrivateKey");
            }
        }

        public static string DlbpayPublicKey
        {
            get
            {
                return ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "DdbillPublicKey");
            }
        }

        private PaymentSetting()
        {
        }
    }
}
