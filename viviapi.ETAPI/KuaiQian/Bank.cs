namespace viviapi.ETAPI.KuaiQian
{
    public class Bank
    {
        public static string GetBankCode(string paymodeId)
        {
            string str = string.Empty;
            switch (paymodeId)
            {
                case "970":
                    str = "CMB";
                    break;
                case "967":
                    str = "ICBC";
                    break;
                case "964":
                    str = "ABC";
                    break;
                case "965":
                    str = "CCB";
                    break;
                case "963":
                    str = "BOC";
                    break;
                case "977":
                    str = "SPDB";
                    break;
                case "981":
                    str = "BCOM";
                    break;
                case "980":
                    str = "CMBC";
                    break;
                case "974":
                    str = "SDB";
                    break;
                case "985":
                    str = "GDB";
                    break;
                case "962":
                    str = "CITIC";
                    break;
                case "982":
                    str = "HXB";
                    break;
                case "972":
                    str = "CIB";
                    break;
                case "984":
                    str = "GZRCC";
                    break;
                case "995":
                    str = "GZCB";
                    break;
                case "996":
                    str = "CUPS";
                    break;
                case "976":
                    str = "SRCB";
                    break;
                case "989":
                    str = "BOB";
                    break;
                case "988":
                    str = "CBHB";
                    break;
                case "990":
                    str = "BJRCB";
                    break;
                case "979":
                    str = "NJCB";
                    break;
                case "986":
                    str = "CEB";
                    break;
                case "987":
                    str = "BEA";
                    break;
                case "997":
                    str = "NBCB";
                    break;
                case "983":
                    str = "HZB";
                    break;
                case "978":
                    str = "PAB";
                    break;
                case "998":
                    str = "HSB";
                    break;
                case "968":
                    str = "CZB";
                    break;
                case "975":
                    str = "SHB";
                    break;
                case "971":
                    str = "PSBC";
                    break;
                case "999":
                    str = "UPOP";
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
                case "中国工商银行":
                    return "ICBC";
                case "中国建设银行":
                    return "CCB";
                case "中国农业银行":
                    return "ABC";
                case "中国银行_上海":
                    return "BOC_SH";
                case "中国银行_广州":
                    return "BOC_GZ";
                case "上海浦东发展银行":
                    return "SPDB";
                case "交通银行":
                    return "BCOM";
                case "中国民生银行":
                    return "CMBC";
                case "深圳发展银行":
                    return "SDB";
                case "广东发展银行":
                    return "GDB";
                case "中信银行":
                    return "CITIC";
                case "华夏银行":
                    return "HXB";
                case "兴业银行":
                    return "CIB";
                case "广州市农村信用合作社":
                    return "GZRCC";
                case "上海农村商业银行":
                    return "SHRCC";
                case "中国邮政储蓄":
                    return "CPSRB";
                case "中国光大银行":
                    return "CEB";
                case "北京银行":
                    return "BOB";
                case "渤海银行":
                    return "CBHB";
                case "北京农村商业银行":
                    return "BJRCB";
                case "中国银联":
                    return "CNPY";
                case "招商银行":
                    return "CMB";
                case "中国银行":
                    return "BOC";
                case "邮政储蓄":
                    return "PSBC";
                case "南京银行":
                    return "NJCB";
                case "上海农商银行":
                    return "SRCB";
                case "广州市商业银行":
                    return "GZRCC";
                case "浙商银行":
                    return "CZB";
                default:
                    return "所有银行";
            }
        }

        public enum BankTypeEnum
        {
            所有银行,
            中国工商银行,
            中国建设银行,
            中国农业银行,
            中国银行_上海,
            中国银行_广州,
            上海浦东发展银行,
            交通银行,
            中国民生银行,
            深圳发展银行,
            广东发展银行,
            中信银行,
            华夏银行,
            兴业银行,
            广州市农村信用合作社,
            广州市商业银行,
            上海农村商业银行,
            中国邮政储蓄,
            中国光大银行,
            北京银行,
            渤海银行,
            北京农村商业银行,
            中国银联,
            招商银行,
            中国银行,
            邮政储蓄,
            南京银行,
            上海农商银行,
        }
    }
}
