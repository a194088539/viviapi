namespace viviapi.gateway
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using viviapi.BLL;
    using viviapi.BLL.Sys;
    using viviapi.BLL.User;
    using viviapi.ETAPI;
    using viviapi.Model.Sys;
    using viviLib.ExceptionHandling;
    using viviLib.Logging;
    using viviLib.Text;
    using viviLib.Web;

    public class agentDistribution : Page
    {
        public static string current_service = "longbao_agent_distribution";
        private viviapi.BLL.Withdraw.settledAgent setAntBLL = new viviapi.BLL.Withdraw.settledAgent();

        public string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        public static string BuildParamString(string[] s)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (i == (s.Length - 1))
                {
                    builder.Append(s[i]);
                }
                else
                {
                    builder.Append(s[i] + "&");
                }
            }
            return builder.ToString();
        }

        public bool CheckAgentTime()
        {
            DateTime minValue = DateTime.MinValue;
            try
            {
                minValue = DateTime.ParseExact(this.agent_time, "yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            if (minValue == DateTime.MinValue)
            {
                return false;
            }
            return (minValue.ToString("yyyyMMddHHmmss") == this.agent_time);
        }

        public string CheckParm(out DataRow row)
        {
            row = null;
            if (string.IsNullOrEmpty(this.partner))
            {
                return "E1001001";
            }
            if ((this.partner.Length < 4) || (this.partner.Length > 10))
            {
                return "E1001002";
            }
            if (!viviLib.Text.Validate.IsNumber(this.partner))
            {
                return "E1001003";
            }
            if (string.IsNullOrEmpty(this.sign))
            {
                return "E1002001";
            }
            string userApiKey = UserFactory.GetUserApiKey(this.userid);
            if (string.IsNullOrEmpty(userApiKey))
            {
                return "E1017001";
            }
            switch (this.ChkSign(userApiKey))
            {
                case -1:
                    return "E1002001";

                case 0:
                    return "E1002002";
            }
            if (string.IsNullOrEmpty(this.input_charset))
            {
                return "E100901";
            }
            if (!this.Isinput_charsetOk())
            {
                return "E100902";
            }
            if (string.IsNullOrEmpty(this.sign_type))
            {
                return "E1004001";
            }
            if (this.sign_type != "MD5")
            {
                return "E1004002";
            }
            if (string.IsNullOrEmpty(this.return_url))
            {
                return "E1006001";
            }
            if (this.return_url.Length > 0x100)
            {
                return "E1006002";
            }
            if (string.IsNullOrEmpty(this.service))
            {
                return "E1008001";
            }
            if (this.service != current_service)
            {
                return "E1008002";
            }
            if (string.IsNullOrEmpty(this.agent_time))
            {
                return "E1009001";
            }
            if (!this.CheckAgentTime())
            {
                return "E1009002";
            }
            if (string.IsNullOrEmpty(this.out_trade_no))
            {
                return "E1010001";
            }
            if (this.out_trade_no.Length > 0x40)
            {
                return "E1010002";
            }
            if (string.IsNullOrEmpty(this.bank_name))
            {
                return "E1011001";
            }
            if (!this.IsBankNameOK())
            {
                return "E1011002";
            }
            if (this.bank_site_name.Length > 80)
            {
                return "E1012001";
            }
            if (string.IsNullOrEmpty(this.bank_account_name))
            {
                return "E101901";
            }
            if (this.bank_account_name.Length > 0x20)
            {
                return "E101902";
            }
            if (!viviLib.Text.Validate.isChinese(this.bank_account_name))
            {
                return "E101903";
            }
            if (string.IsNullOrEmpty(this.amount_str))
            {
                return "E1014001";
            }
            if (!(PageValidate.IsDecimal(this.amount_str) || PageValidate.IsNumber(this.amount_str)))
            {
                return "E1014002";
            }
            if (this.amount == 0M)
            {
                return "E1014003";
            }
            if (this.remark.Length > 80)
            {
                return "E1015001";
            }
            switch (this.setAntBLL.ChkParms(this.userid, this.sysbankCode, this.amount, out row))
            {
                case 1:
                    return "E1017001";

                case 2:
                    return "E1018001";

                case 3:
                    return "E1016001";

                case 4:
                    return "E1021001";

                case 5:
                    return "E1014005";

                case 6:
                    return "E1014005";

                case 7:
                    return "E1014005";
            }
            return string.Empty;
        }

        private int ChkSign(string apiKey)
        {
            try
            {
                string[] r = new string[] { "service=" + this.service, "input_charset=" + this.input_charset, "partner=" + this.partner, "sign_type=" + this.sign_type, "return_url=" + this.return_url, "out_trade_no=" + this.out_trade_no, "bank_name=" + this.bank_name, "bank_site_name=" + this.bank_site_name, "bank_account_name=" + this.bank_account_name, "bank_account_no=" + this.bank_account_no, "amount_str=" + this.amount_str, "remark=" + this.remark, "agent_time=" + this.agent_time };
                string str = BuildParamString(this.BubbleSort(r));
                string str2 = md5(this.input_charset, str + apiKey).ToLower();
                LogHelper.Write(str + apiKey);
                LogHelper.Write(str2);
                return ((str2 == this.sign) ? 1 : 0);
            }
            catch
            {
                return -1;
            }
        }

        public string GetBankCode(string bankName)
        {
            string str = string.Empty;
            switch (bankName)
            {
                case "支付宝":
                    return "0002";

                case "财付通":
                    return "0003";

                case "工商银行":
                    return "1002";

                case "农业银行":
                    return "1005";

                case "建设银行":
                    return "1003";

                case "中国银行":
                    return "1026";

                case "招商银行":
                    return "1001";

                case "民生银行":
                    return "1006";

                case "交通银行":
                    return "96";

                case "华夏银行":
                    return "1025";

                case "兴业银行":
                    return "1009";

                case "广发银行":
                    return "1027";

                case "浦发银行":
                    return "1004";

                case "光大银行":
                    return "1022";

                case "中信银行":
                    return "1021";

                case "平安银行":
                    return "1010";

                case "邮政储蓄银行":
                    return "1066";
            }
            return str;
        }

        public string GetParmValue(string parmName)
        {
            string queryStringString = WebBase.GetQueryStringString(parmName, "");
            if (string.IsNullOrEmpty(queryStringString))
            {
                queryStringString = WebBase.GetFormString(parmName, "");
            }
            return queryStringString;
        }

        public string GetParmValue2(string parmName)
        {
            string str = base.Request[parmName];
            if (this.Isinput_charsetOk())
            {
                str = HttpUtility.ParseQueryString(base.Request.Url.Query, Encoding.GetEncoding(this.input_charset))[parmName];
            }
            return str;
        }

        protected string GetQueryString(string sKey, Encoding e)
        {
            string input = base.Server.UrlDecode(HttpUtility.UrlDecode(base.Request.ServerVariables["QUERY_STRING "], e));
            Match match = new Regex(sKey + "=([^&$]*?)(&|$) ").Match(input);
            if (match.Success)
            {
                return match.Result("$1 ");
            }
            return string.Empty;
        }

        private bool IsBankNameOK()
        {
            string[] strArray = new string[] {
                "工商银行", "农业银行", "建设银行", "交通银行", "中国银行", "招商银行", "邮政储蓄银行", "民生银行", "华夏银行", "兴业银行", "广发银行", "浦发银行", "光大银行", "中信银行", "平安银行", "财付通",
                "支付宝"
             };
            foreach (string str in strArray)
            {
                if (this.bank_name == str)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Isinput_charsetOk()
        {
            string[] strArray = new string[] { "UTF-8", "GB2312", "GBK" };
            foreach (string str in strArray)
            {
                if (this.input_charset.ToUpper() == str)
                {
                    return true;
                }
            }
            return false;
        }

        public static string md5(string input_charset, string plainText)
        {
            MD5 md = new MD5CryptoServiceProvider();
            return viviapi.ETAPI.Ebatong.CommonHelper.ToHexStr(md.ComputeHash(Encoding.GetEncoding(input_charset).GetBytes(plainText)));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "F";
            string str2 = string.Empty;
            try
            {
                DataRow row = null;
                str2 = this.CheckParm(out row);
                if (string.IsNullOrEmpty(str2))
                {
                    if (row != null)
                    {
                        byte num = Convert.ToByte(row["tranRequiredAudit"]);
                        viviapi.Model.Withdraw.settledAgent model = new viviapi.Model.Withdraw.settledAgent();
                        model.amount = this.amount;
                        model.bankAccount = this.bank_account_no;
                        model.bankAccountName = this.bank_account_name;
                        model.bankBranch = this.bank_site_name;
                        model.bankName = this.bank_name;
                        model.bankCode = this.GetBankCode(this.bank_name);
                        model.charge = Convert.ToDecimal(row["charge"]);
                        model.mode = 1;
                        model.out_trade_no = this.out_trade_no;
                        model.remark = this.remark;
                        model.return_url = this.return_url;
                        model.service = this.service;
                        model.input_charset = this.input_charset;
                        model.sign_type = this.sign_type;
                        model.userid = this.userid;
                        model.trade_no = this.setAntBLL.GenerateTradeNo(this.userid, 1);
                        if (Convert.ToInt32(row["tranApi"]) > 0)
                        {
                            model.suppid = Convert.ToInt32(row["suppId"]);
                        }
                        model.issure = 1;
                        model.sureclientip = ServerVariables.TrueIP;
                        if (Convert.ToByte(row["isRequireAgentDistAudit"]) == 0)
                        {
                            model.issure = 2;
                        }
                        int num2 = this.setAntBLL.Add(model);
                        if (num2 <= 0)
                        {
                            str2 = "E1022001";
                        }
                        else
                        {
                            model.id = num2;
                            if ((model.issure == 2) && ((model.suppid > 0) && (num == 0)))
                            {
                                if (this.setAntBLL.Audit(model.trade_no, 2, 0, "system") == 0)
                                {
                                    Withdraw.InitDistribution2(model);
                                }
                                else
                                {
                                    str2 = "E1022002";
                                }
                            }
                            str = "T";
                        }
                    }
                    else
                    {
                        str2 = "E1021001";
                    }
                }
            }
            catch
            {
                str2 = "E1021002";
            }
            if (!string.IsNullOrEmpty(str2) && SysConfig.debuglog)
            {
                debuginfo debuginfo = new debuginfo();
                debuginfo.addtime = new DateTime?(DateTime.Now);
                debuginfo.bugtype = debugtypeenum.对私代发;
                debuginfo.detail = string.Empty;
                debuginfo.errorcode = str2;
                debuginfo.errorinfo = str2;
                debuginfo.userid = new int?(this.userid);
                if (base.Request.RawUrl != null)
                {
                    debuginfo.url = base.Request.RawUrl.ToString();
                }
                else
                {
                    debuginfo.url = string.Empty;
                }
                debuglog.Insert(debuginfo);
            }
            base.Response.Write(string.Format("{0}|{1}|{2}", this.out_trade_no, str2, str));
        }

        public string agent_time
        {
            get
            {
                return this.GetParmValue("agent_time");
            }
        }

        public decimal amount
        {
            get
            {
                decimal result = 0M;
                if (!string.IsNullOrEmpty(this.amount_str))
                {
                    decimal.TryParse(this.amount_str, out result);
                }
                return result;
            }
        }

        public string amount_str
        {
            get
            {
                return this.GetParmValue("amount_str");
            }
        }

        public string bank_account_name
        {
            get
            {
                return this.GetParmValue2("bank_account_name");
            }
        }

        public string bank_account_no
        {
            get
            {
                return this.GetParmValue("bank_account_no");
            }
        }

        public string bank_name
        {
            get
            {
                return this.GetParmValue2("bank_name");
            }
        }

        public string bank_site_name
        {
            get
            {
                return this.GetParmValue2("bank_site_name");
            }
        }

        public string input_charset
        {
            get
            {
                return this.GetParmValue("input_charset");
            }
        }

        public string out_trade_no
        {
            get
            {
                return this.GetParmValue("out_trade_no");
            }
        }

        public string partner
        {
            get
            {
                return this.GetParmValue("partner");
            }
        }

        public string remark
        {
            get
            {
                string parmValue = this.GetParmValue("remark");
                if (!string.IsNullOrEmpty(parmValue))
                {
                    parmValue = HttpUtility.UrlDecode(parmValue);
                }
                return parmValue;
            }
        }

        public string return_url
        {
            get
            {
                return this.GetParmValue("return_url");
            }
        }

        public string service
        {
            get
            {
                return this.GetParmValue("service");
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("sign");
            }
        }

        public string sign_type
        {
            get
            {
                return this.GetParmValue("sign_type");
            }
        }

        public string sysbankCode
        {
            get
            {
                return this.GetBankCode(this.bank_name);
            }
        }

        public int userid
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrEmpty(this.partner))
                {
                    int.TryParse(this.partner, out result);
                }
                return result;
            }
        }
    }
}

