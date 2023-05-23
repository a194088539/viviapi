using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using viviapi.BLL.Sys;
using viviapi.Model.Sys;
using viviLib.ExceptionHandling;
using viviLib.Logging;
using viviLib.Web;

namespace viviapi.ETAPI.TongLian
{
    public class daifu : ETAPIBase
    {
        private static int suppId = 10002;


        public daifu()
            : base(daifu.suppId)
        {
        }
        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/tonglian/dfNotify.aspx";
            }
        }
        public class ReturnMessage
        {
            public string settleStatus { get; set; }
            public string respCode { get; set; }
            public string respDesc { get; set; }
        }
        public class ChaXunReturnMessage
        {
            public string settleMax { get; set; }
            public string accAmount { get; set; }
        }

        public string GetBankCode(string bankcode)
        {
            switch (bankcode)
            {
                case "cmb"://
                    return "308100005310";

                case "icbc"://
                    return "102100000353";

                case "abc"://
                    return "103100004425";

                case "ccb"://
                    return "105100004067";

                case "boc"://
                    return "104100004265";

                case "spdb"://
                    return "310100000010";

                case "cmbc"://
                    return "301100000187";

                case "comm"://
                    return "305100001008";

                case "gbd"://
                    return "306100004505";

                case "citic"://
                    return "302100011325";

                case "hxb"://
                    return "306100005071";

                case "cib"://
                    return "309100001194";

                case "ceb":
                    return "303100000330";//

                case "pabc":
                    return "307100003019";//

                case "postgc"://
                    return "403100000359";
                default:
                    return "no";
            }
        }
        public string GetBankType(string bankcode)
        {
            switch (bankcode)
            {
                case "cmb"://
                    return "CMB";

                case "icbc"://
                    return "ICBC";

                case "abc"://
                    return "ABC";

                case "ccb"://
                    return "CCB";

                case "boc"://boc
                    return "BOC";

                case "spdb"://
                    return "SPDB";

                case "comm"://
                    return "BCOM";

                case "cmbc"://
                    return "CMBC";

                case "gbd"://
                    return "GDB";

                case "citic"://
                    return "CITIC";

                case "hxb"://
                    return "HXB";

                case "cib"://
                    return "CIB";

                case "ceb":
                    return "CEB";//

                case "pabc":
                    return "PABC";//

                case "postgc"://
                    return "PSBC";
                default:
                    return "no";
            }
        }
        public static string UserMd5(string str, string encoding)
        {

            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.GetEncoding(encoding).GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }
        public class NotifyMessage
        {
            public string merchNo { get; set; }
            public string settleNo { get; set; }
            public string settleId { get; set; }
            public string settleAmount { get; set; }
            public string settleStatus { get; set; }
            public string notifyTime { get; set; }
            public string sign { get; set; }
        }
        public void Notify()
        {
            LogHelper.Write("通联异步调用成功");
            try
            {
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                string jsonData = "";
                using (var reader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    jsonData = reader.ReadToEnd();
                }
                LogHelper.Write(DateTime.Now.ToString() + "通联代付通知异步参数" + jsonData);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                NotifyMessage resp = ser.Deserialize<NotifyMessage>(jsonData);
                int status = 3;
                //执行操作方法
                if (resp.settleStatus.Equals("2"))
                {
                    Withdraw.Complete(daifu.suppId, resp.settleNo, false, status, (Convert.ToDecimal(resp.settleAmount) / 100m).ToString("f2"), resp.settleId, "接口通知代付成功").ToString();
                }

            }
            catch (Exception ex)
            {
                LogHelper.Write(ex.Message);
            }

        }
        public string GetYuE()
        {
            string sybmiturl = "http://47.94.208.216:8080/app/accSingleMax.do";

            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();

            waitSign.Add("merchNo", suppAccount);
            waitSign.Add("settleType", "D0");
            string signdata = "merchNo=" + suppAccount + "&settleType=D0" + suppKey;

            string sign = UserMd5(signdata, "UTF-8");
            waitSign.Add("sign", sign);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string x = ser.Serialize(waitSign);
            LogHelper.Write(DateTime.Now.ToString() + ":通联余额查询Post数据---" + x);
            string result = WebClientHelper.GetString(sybmiturl, x, "POST", Encoding.GetEncoding("UTF-8"), 100000);
            LogHelper.Write(DateTime.Now.ToString() + ":通联代付余额查询返回---" + result);
            JavaScriptSerializer js = new JavaScriptSerializer();
            //try
            //{
            ChaXunReturnMessage msg = js.Deserialize<ChaXunReturnMessage>(result);
            return msg.settleMax;
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
            string sybmiturl = "http://47.94.208.216:8080/app/doWithdrawSettle.do";

            SortedDictionary<string, string> waitSign = new SortedDictionary<string, string>();
            //参数
            string accIdentity = "371102198908166510";
            string mobile = "15553637711";//13800138000
            string bankno = GetBankCode(bank_code);
            string bankType = GetBankType(bank_code);
            waitSign.Add("merchNo", suppAccount);
            waitSign.Add("settleNo", trade_no);
            waitSign.Add("settleAmount", (amount * 100).ToString("f0"));
            waitSign.Add("notifyUrl", notifyUrl);
            waitSign.Add("accIdentity", accIdentity);
            waitSign.Add("accType", "1");
            waitSign.Add("accNo", bank_account_no);
            waitSign.Add("accName", bank_account_name);
            waitSign.Add("accPhone", mobile);
            waitSign.Add("unionCode", bankno);
            waitSign.Add("bankCode", bankType);

            string signdata = "";
            foreach (var key in waitSign.Keys)
            {
                signdata += key + "=" + waitSign[key] + "&";
            }
            signdata = signdata.Substring(0, signdata.Length - 1) + suppKey;
            string signed = UserMd5(signdata, "UTF-8");
            waitSign.Add("sign", signed);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string x = js.Serialize(waitSign);


            LogHelper.Write(DateTime.Now.ToString() + ":通联代付Post数据---" + x);
            string result = WebClientHelper.GetString(sybmiturl, x, "POST", Encoding.GetEncoding("UTF-8"), 100000);
            LogHelper.Write(DateTime.Now.ToString() + ":通联代付返回---" + result);
            model.errorinfo = result;
            debuglog.Insert(model);

            JavaScriptSerializer resp = new JavaScriptSerializer();
            try
            {
                ReturnMessage msg = resp.Deserialize<ReturnMessage>(result);
                if (msg.settleStatus == "1")
                    return "SUCCESS";
                else
                    return "df_error：状态码:" + msg.respCode + ";错误信息描述：" + msg.respDesc;
            }
            catch
            {
                return result;
            }
        }

        public string encode(string val)
        {
            return HttpUtility.UrlDecode(val, Encoding.GetEncoding("UTF-8"));
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
