namespace viviapi.ETAPI.YeePay
{
    public class Bank
    {
        public static string GetBankCode(string paymodeId)
        {
            string str = "";
            switch (paymodeId)
            {
                case "970":
                    str = "CMBCHINA-NET-B2C";
                    break;
                case "967":
                    str = "ICBC-NET-B2C";
                    break;
                case "964":
                    str = "ABC-NET-B2C";
                    break;
                case "965":
                    str = "CCB-NET-B2C";
                    break;
                case "963":
                    str = "BOC-NET-B2C";
                    break;
                case "981":
                    str = "BOCO-NET-B2C";
                    break;
                case "980":
                    str = "CMBC-NET-B2C";
                    break;
                case "974":
                    str = "SDB-NET-B2C";
                    break;
                case "985":
                    str = "GDB-NET-B2C";
                    break;
                case "962":
                    str = "ECITIC-NET-B2C";
                    break;
                case "982":
                    str = "HXB-NET-B2C";
                    break;
                case "972":
                    str = "CIB-NET-B2C";
                    break;
                case "971":
                    str = "POST-NET-B2C";
                    break;
                case "989":
                    str = "BCCB-NET-B2C";
                    break;
                case "988":
                    str = "CBHB-NET-B2C";
                    break;
                case "990":
                    str = "BJRCB-NET-B2C";
                    break;
                case "979":
                    str = "NJCB-NET-B2C";
                    break;
                case "986":
                    str = "CEB-NET-B2C";
                    break;
                case "987":
                    str = "HKBEA-NET-B2C";
                    break;
                case "997":
                    str = "NBCB-NET-B2C";
                    break;
                case "978":
                    str = "PINGANBANK-NET";
                    break;
                case "968":
                    str = "CZ-NET-B2C";
                    break;
                case "975":
                    str = "SHB-NET-B2C";
                    break;
                case "977":
                    str = "SPDB-NET-B2C";
                    break;
            }
            return str;
        }

        public static string GetBankCode(Bank.BankTypeEnum _banktype)
        {
            switch (((object)_banktype).ToString())
            {
                case "所有银行":
                    return "";
                case "易宝会员支付":
                    return "1000000-NET";
                case "中国农业银行":
                    return "ABC-NET";
                case "北京银行":
                    return "BCCB-NET";
                case "交通银行":
                    return "BOCO-NET";
                case "建设银行":
                    return "CCB-NET";
                case "兴业银行":
                    return "CIB-NET";
                case "招商银行":
                    return "CMBCHINA-NET";
                case "中国民生银行总行":
                    return "CMBC-NET";
                case "光大银行":
                    return "CEB-NET";
                case "中国银行":
                    return "BOC-NET";
                case "中信银行":
                    return "ECITIC-NET";
                case "中国工商银行":
                    return "ICBC-NET";
                case "上海浦东发展银行":
                    return "SPDB-NET";
                case "深圳发展银行":
                    return "SDB-NET";
                case "广东发展银行":
                    return "GDB-NET";
                case "中国邮政":
                    return "POST-NET";
                case "北京农村商业银行":
                    return "BJRCB-NET";
                case "华夏银行":
                    return "HXB-NET";
                case "广州市农信社":
                    return "GNXS-NET";
                case "广州市商业银行":
                    return "GZCB-NET";
                case "顺德农信社":
                    return "SDE-NET";
                case "海农村商业银行":
                    return "SHRCB-NET";
                case "骏网一卡通":
                    return "JUNNET-NET";
                case "联华OK卡":
                    return "LIANHUAOKCARD-NET";
                case "电信聚信卡":
                    return "SHTEL-NET";
                case "盛大卡":
                    return "SNDACARD-NET";
                case "神州行标准版网关":
                    return "SZX-NET";
                case "征途卡":
                    return "ZHENGTU-NET";
                default:
                    return "";
            }
        }

        public enum BankTypeEnum
        {
            所有银行,
            易宝会员支付,
            中国农业银行,
            北京银行,
            交通银行,
            建设银行,
            兴业银行,
            招商银行,
            中国民生银行总行,
            光大银行,
            中国银行,
            中信银行,
            中国工商银行,
            上海浦东发展银行,
            深圳发展银行,
            广东发展银行,
            中国邮政,
            北京农村商业银行,
            华夏银行,
            广州市农信社,
            广州市商业银行,
            顺德农信社,
            上海农村商业银行,
            骏网一卡通,
            联华OK卡,
            电信聚信卡,
            盛大卡,
            神州行标准版网关,
            征途卡,
        }
    }
}
