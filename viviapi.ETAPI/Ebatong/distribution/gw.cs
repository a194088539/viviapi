using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using viviapi.BLL.Sys;
using viviapi.Model.Sys;
using viviLib.ExceptionHandling;
using viviLib.Logging;
using viviLib.Web;

namespace viviapi.ETAPI.Ebatong.distribution
{
    public class gw : ETAPIBase
    {
        private static int suppId = 10030;

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/ebatong/distribution_notify.aspx";
            }
        }

        public gw()
          : base(gw.suppId)
        {
        }

        public string Req2(string trade_no, string bank_code, string bank_site_name, string bank_account_name, string bank_account_no, Decimal amount, string remark)
        {
            debuginfo model = new debuginfo();
            model.addtime = new DateTime?(DateTime.Now);
            model.bugtype = debugtypeenum.对私代发接口;
            model.detail = string.Empty;
            model.errorcode = "";
            model.errorinfo = "";
            model.userid = new int?(0);
            string bankName = this.GetBankName(bank_code);
            string val1 = "ebatong_agent_distribution";
            string str1 = "UTF-8";
            string puserid5 = this._suppInfo.puserid5;
            string val2 = "MD5";
            string notifyUrl = this.notifyUrl;
            string val3 = trade_no;
            string val4 = amount.ToString("f2");
            string val5 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string trueIp = ServerVariables.TrueIP;
            string str2 = CommonHelper.BuildParamString(CommonHelper.BubbleSort(new string[13]
            {
        "service=" + val1,
        "input_charset=" + str1,
        "partner=" + puserid5,
        "sign_type=" + val2,
        "return_url=" + notifyUrl,
        "out_trade_no=" + val3,
        "bank_name=" + bankName,
        "bank_site_name=" + bank_site_name,
        "bank_account_name=" + bank_account_name,
        "bank_account_no=" + bank_account_no,
        "amount_str=" + val4,
        "remark=" + remark,
        "agent_time=" + val5
            }));
            string str3 = CommonHelper.md5(str1, str2 + this._suppInfo.puserkey5).ToLower();
            string val6 = HttpUtility.UrlDecode(notifyUrl);
            StringBuilder stringBuilder = new StringBuilder();
            NameValueCollection list = new NameValueCollection();
            list.Add("service", this.encode(val1));
            list.Add("input_charset", this.encode(str1));
            list.Add("partner", this.encode(puserid5));
            list.Add("sign_type", this.encode(val2));
            list.Add("return_url", this.encode(val6));
            list.Add("out_trade_no", this.encode(trade_no));
            list.Add("bank_name", this.encode(bankName));
            list.Add("bank_site_name", this.encode(bank_site_name));
            list.Add("bank_account_name", this.encode(bank_account_name));
            list.Add("bank_account_no", this.encode(bank_account_no));
            list.Add("amount_str", this.encode(val4));
            list.Add("remark", this.encode(remark));
            list.Add("agent_time", this.encode(val5));
            list.Add("sign", str3);
            stringBuilder.AppendFormat("?service={0}", (object)this.encode(val1));
            stringBuilder.AppendFormat("&input_charset={0}", (object)this.encode(str1));
            stringBuilder.AppendFormat("&partner={0}", (object)this.encode(puserid5));
            stringBuilder.AppendFormat("&sign_type={0}", (object)this.encode(val2));
            stringBuilder.AppendFormat("&return_url={0}", (object)this.encode(val6));
            stringBuilder.AppendFormat("&out_trade_no={0}", (object)this.encode(val3));
            stringBuilder.AppendFormat("&bank_name={0}", (object)this.encode(bankName));
            stringBuilder.AppendFormat("&bank_site_name={0}", (object)this.encode(bank_site_name));
            stringBuilder.AppendFormat("&bank_account_name={0}", (object)this.encode(bank_account_name));
            stringBuilder.AppendFormat("&bank_account_no={0}", (object)this.encode(bank_account_no));
            stringBuilder.AppendFormat("&amount_str={0}", (object)this.encode(val4));
            stringBuilder.AppendFormat("&remark={0}", (object)this.encode(remark));
            stringBuilder.AppendFormat("&agent_time={0}", (object)this.encode(val5));
            stringBuilder.AppendFormat("&sign={0}", (object)str3);
            string distributionUrl = this._suppInfo.distributionUrl;
            model.url = distributionUrl + ((object)stringBuilder).ToString();
            string @string = WebClientHelper.GetString(distributionUrl, list, "POST", Encoding.UTF8);
            model.errorinfo = @string;
            debuglog.Insert(model);
            return @string;
        }

        public string encode(string val)
        {
            return HttpUtility.UrlDecode(val, Encoding.UTF8);
        }

        public bool DoTrans(viviapi.Model.distribution info)
        {
            try
            {
                return this.Req2(info.trade_no, info.bankCode, info.bankBranch, info.bankAccountName, info.bankAccount, info.amount, string.Empty).Split('|')[2] == "T";
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return false;
        }

        public void Notify()
        {
            try
            {
                NameValueCollection queryString = HttpContext.Current.Request.QueryString;
                debuglog.Insert(new debuginfo()
                {
                    addtime = new DateTime?(DateTime.Now),
                    bugtype = debugtypeenum.对私代发接口,
                    detail = string.Empty,
                    errorcode = "",
                    errorinfo = "",
                    userid = new int?(0),
                    url = HttpContext.Current.Request.RawUrl.ToLower()
                });
                string str1 = queryString["service"];
                string str2 = queryString["sign"];
                string str3 = queryString["notify_id"];
                LogHelper.Write(str3);
                string str4 = queryString["notify_time"];
                string message = queryString["error_message"];
                string amount = queryString["amount_str"];
                string[] allKeys = queryString.AllKeys;
                string[] strArray = CommonHelper.BubbleSort(allKeys);
                string str5 = "";
                for (int index = 0; index < allKeys.Length; ++index)
                {
                    if (!(strArray[index] == "sign"))
                        str5 = str5 + strArray[index] + "=" + queryString[strArray[index]] + "&";
                }
                int length = str5.Length;
                string str6 = CommonHelper.md5(Config.Input_charset, str5.Remove(length - 1, 1) + this._suppInfo.puserkey5).ToLower();
                string str7 = string.Empty;
                LogHelper.Write(str6);
                LogHelper.Write(str2);
                if (!str6.Equals(str2))
                    return;
                string trade_no = queryString["out_trade_no"];
                int status = int.Parse(queryString["trade_status"]);
                bool is_cancel = false;
                if (status == 1 || status == 2 || status == 4)
                    is_cancel = true;
                LogHelper.Write(Withdraw.Complete(gw.suppId, trade_no, is_cancel, status, amount, str3, message).ToString());
                HttpContext.Current.Response.Write(str3);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        protected string GetBankName(string bankCode)
        {
            string str = bankCode;
            switch (bankCode)
            {
                case "1002":
                    str = "工商银行";
                    break;
                case "1005":
                    str = "农业银行";
                    break;
                case "1003":
                    str = "建设银行";
                    break;
                case "1026":
                    str = "中国银行";
                    break;
                case "1001":
                    str = "招商银行";
                    break;
                case "1006":
                    str = "民生银行";
                    break;
                case "1020":
                    str = "交通银行";
                    break;
                case "1025":
                    str = "华夏银行";
                    break;
                case "1009":
                    str = "兴业银行";
                    break;
                case "1027":
                    str = "广发银行";
                    break;
                case "1004":
                    str = "浦发银行";
                    break;
                case "1022":
                    str = "光大银行";
                    break;
                case "1021":
                    str = "中信银行";
                    break;
                case "1010":
                    str = "平安银行";
                    break;
                case "1066":
                    str = "邮政储蓄银行";
                    break;
            }
            return str;
        }
    }
}
