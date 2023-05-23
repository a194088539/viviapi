using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using viviapi.BLL.Sys;
using viviapi.Model.Sys;
using viviLib.ExceptionHandling;
using viviLib.Logging;
using viviLib.Web;
//using CommonLibrary.CommonSecurity;
namespace viviapi.ETAPI.XinFuBao
{
    public class DaiFu : ETAPIBase
    {
        private static int suppId = 10004;
        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/xfb/df_Notify.aspx";
            }
        }
        public DaiFu(int _suppId)
            : base(_suppId)
        {
            suppId = _suppId;
        }

        //public DaiFu()
        //    : base(DaiFu.suppId)
        //{

        //}
        public string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        public string GetBankCode(string bankcode)
        {
            //102	中国工商银行
            //103	中国农业银行
            //104	中国银行
            //105	中国建设银行
            //203	中国农业发展银行
            //301	交通银行
            //403	中国邮政储蓄银行
            //302	中信银行
            //303	中国光大银行
            //304	华夏银行
            //305	中国民生银行
            //306	广东发展银行
            //307	深圳发展银行
            //308	招商银行
            //309	兴业银行
            //310	上海浦东发展银行
            //313	城市商业银行
            //314	农村商业银行
            //315	恒丰银行
            //317	农村合作银行
            //318	渤海银行
            //783	平安银行
            //786	青岛国际银行
            //001	中国人民银行

            switch (bankcode)
            {
                case "cmb"://
                    return "308";

                case "icbc"://
                    return "102";

                case "abc"://
                    return "103";

                case "ccb"://
                    return "105";

                case "boc"://
                    return "104";

                case "spdb"://
                    return "310";

                case "cmbc"://
                    return "301";

                case "comm"://
                    return "305";

                case "gbd"://
                    return "306";

                case "citic"://
                    return "302";

                case "hxb"://
                    return "304";

                case "cib"://
                    return "309";

                case "ceb":
                    return "303";//

                case "pabc":
                    return "783";//

                case "postgc"://
                    return "403";
                default:
                    return "no";
            }
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
            //=================================
            string sybmiturl = _suppInfo.distributionUrl;
            if (string.IsNullOrEmpty(_suppInfo.distributionUrl))
                sybmiturl = "http://online.atrustpay.com/payment/WithdrawApply.do";

            string versionId = "1.0";
            string orderAmount = (amount * 100).ToString("f0");
            string orderDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string currency = "RMB";
            string transType = "008";
            string asynNotifyUrl = notifyUrl;
            //string synNotifyUrl = returnUrl;
            string signType = "MD5";
            string merId = suppAccount;
            string prdOrdNo = trade_no;
            string receivableType = "D00";
            string isCompay = "0";
            string phoneNo = "15555555555";//必填
            string customerName = bank_account_name;//必填
            string cerdType = "";//证件类型 可填
            string cerdId = "445202199002108316";//证件号必填
            string accBankNo = "";//开户行号。可填
            string accBankName = "";
            string acctNo = bank_account_no;
            string rcvBranchName = "";//联行名，可填
            string rcvBranchCode = GetBankCode(bank_code);//联行号，必填
            string outaccounttype = "2";//手续费在发款人扣除
            string note = "";

            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            waitSign.Add("versionId", versionId);
            waitSign.Add("orderAmount", orderAmount);
            waitSign.Add("orderDate", orderDate);
            waitSign.Add("currency", currency);
            waitSign.Add("transType", transType);
            waitSign.Add("asynNotifyUrl", asynNotifyUrl);

            waitSign.Add("signType", signType);
            waitSign.Add("merId", merId);
            waitSign.Add("prdOrdNo", prdOrdNo);

            waitSign.Add("receivableType", receivableType);

            waitSign.Add("isCompay", isCompay);
            waitSign.Add("phoneNo", phoneNo);

            waitSign.Add("customerName", customerName);
            waitSign.Add("cerdType", cerdType);
            waitSign.Add("cerdId", cerdId);
            waitSign.Add("accBankNo", accBankNo);
            waitSign.Add("accBankName", accBankName);
            waitSign.Add("acctNo", acctNo);
            waitSign.Add("rcvBranchName", rcvBranchName);
            waitSign.Add("rcvBranchCode", rcvBranchCode);
            waitSign.Add("outaccounttype", outaccounttype);
            waitSign.Add("note", note);

            string signdata = "";
            string postdata = "";
            foreach (var key in waitSign.Keys)
            {
                if (waitSign[key].Length > 0)
                {
                    signdata += key + "=" + waitSign[key] + "&";
                    postdata += key + "=" + waitSign[key] + "&";
                }
            }
            signdata = signdata + "key=" + suppKey;
            string signed = UserMd5(signdata).ToUpper();
            postdata += "signData=" + signed;

            LogHelper.Write(DateTime.Now.ToString() + ":信付宝代付Post数据---" + postdata);

            string result = WebClientHelper.GetString(sybmiturl, postdata, "POST", Encoding.GetEncoding("UTF-8"), 100000);
            LogHelper.Write(DateTime.Now.ToString() + "：信付宝代付返回---" + result);
            //{"signData":"75876A5E944935FEA2130B81AD8A548B","code":"1","signType":"MD5","retCode":"1","serviceName":"提现申请","prdOrdNo":"186820170802143125","retMsg":"提现提交成功","desc":""}
            model.errorinfo = result;
            debuglog.Insert(model);

            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                ReturnMessage msg = js.Deserialize<ReturnMessage>(result);
                if (msg.retCode == "1")
                    return "SUCCESS";
                else
                    return "df_error：状态码:" + msg.retCode + ";错误信息描述：" + msg.retMsg;
            }
            catch
            {
                return result;
            }
        }

        public class ReturnMessage
        {
            public string prdOrdNo { get; set; }
            public string signType { get; set; }
            public string signData { get; set; }
            public string retCode { get; set; }
            public string retMsg { get; set; }
        }

        public void Notify()
        {
            LogHelper.Write("开始处理信付宝代付异步通知");
            NameValueCollection formlist = HttpContext.Current.Request.Form;
            NameValueCollection querylist = HttpContext.Current.Request.QueryString;
            string signdata = "";
            string signData = "";
            SortedDictionary<string, string> list = new SortedDictionary<string, string>();

            foreach (string key in formlist.Keys)
            {

                if (key == "signData")
                    signData = formlist[key];
                else
                {
                    list.Add(key, formlist[key]);
                    //signdata += key + "=" + formlist[key] + "&";
                }
            }
            foreach (string key in querylist.Keys)
            {
                //list.Add(key, querylist[key]);
                if (key == "signData")
                    signData = querylist[key];
                else
                {
                    list.Add(key, querylist[key]);
                    //signdata += key + "=" + querylist[key] + "&";
                }
            }
            foreach (string key in list.Keys)
            {
                signdata += key + "=" + list[key] + "&";
            }

            signdata = signdata + "key=" + suppKey;
            string signed = UserMd5(signdata).ToUpper();

            LogHelper.Write(DateTime.Now.ToString() + ":信付宝代付异步返回数据---" + signdata + "&signed=" + signed + "&signData=" + signData);
            //asynNotifyUrl=https://pay.0cloud.net/notify/xfb/df_Notify.aspx&merId=100519123&orderAmount=null&orderStatus=01&payId=892961607867437056&payTime=20170803122000&prdOrdNo=194020170803121331&signType=MD5&transType=008&versionId=1.0&key=mIyGKmCK0p5x&signed=8626E27C10D169B97CF0A2FDFB9BA95D
            //if (signed.ToUpper().Equals(signData.ToUpper()))
            //{
            //    LogHelper.Write(DateTime.Now.ToString() + ":信付宝代付异步返回验签成功---");

            //执行操作方法
            if (list["orderStatus"].Equals("01"))
            {
                bool is_cancel = false;
                int status = 3;

                Model.distribution model = new viviapi.BLL.distribution().GetModelList("trade_no")[0];
                Withdraw.Complete(DaiFu.suppId, list["prdOrdNo"], is_cancel, status, model.amount.ToString("f2"), list["payId"], "信付宝代付成功").ToString();

                HttpContext.Current.Response.Write("SUCCESS");
            }
            //}

        }

        /// <summary>
        ///代付
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string DoTrans(viviapi.Model.distribution info)
        {
            try
            {
                return this.Req2(info.trade_no, info.bankCode, info.bankBranch, info.bankAccountName, info.bankAccount, info.amount - info.charges, string.Empty);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return "df_error" + ex.Message;
            }

        }
    }
}
