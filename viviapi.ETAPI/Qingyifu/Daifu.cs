//using System.Threading.Tasks;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using testconsole;
using viviapi.SysConfig;
using viviLib.Configuration;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.Qingyifu
{
    public class DaiFu : ETAPIBase
    {
        private static int suppId = 10008;

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/qingyifu/bank_notify.aspx";
            }
        }
        public DaiFu(int _suppId)
            : base(_suppId)
        {
            suppId = _suppId;
        }

        string[,] bankcodeArray = new string[,]{
            {"BOC","中国银行","BOC"},
            {"ABC","农业银行","ABC"},
            {"ICBC","工商银行","ICBC"},
            {"CCB","建设银行","CCB"},
            {"BCM","交通银行","BOCO"},
            {"CMB","招商银行","CMB"},
            {"CEB","光大银行","CEB"},
            {"CMBC","民生银行","CMBC"},
            {"HXB","华夏银行","HXB"},
            {"CIB","兴业银行","CIB"},
            {"CNCB","中信银行","CTTIC"},
            {"SPDB","上海浦东发展银行","SPDB"},
            {"PSBC","中国邮政","PSBS"}
        };

        public string GetBankType(string bankcode)
        {
            switch (bankcode)
            {
                case "CMB":
                    return "CMB";

                case "ICBC":
                    return "ICBC";

                case "ABC":
                    return "ABC";

                case "CCB":
                    return "CCB";

                case "BOC":
                    return "BOC";

                case "SPDB":
                    return "SPDB";

                case "BCOM":
                    return "BCOM";

                case "CMBC":
                    return "CMBC";

                case "CITIC":
                    return "CITIC";

                case "HXB":
                    return "HXB";

                case "CIB":
                    return "CIB";

                case "CEB":
                    return "CEB";

                default:
                    return "no";
            }
        }

        public string Req2(string trade_no, string bank_code, string bank_site_name, string bank_account_name, string bank_account_no, Decimal amount, string remark)
        {
            try
            {
                string Entrustid = trade_no;
                string m_bankcode = "";
                string EntrusBankName = GetBankType(bank_code);
                for (var i = 0; i < bankcodeArray.GetLength(0); i++)
                {
                    if (EntrusBankName.Contains(bankcodeArray[i, 1]))
                    {
                        m_bankcode = bankcodeArray[i, 0];
                        break;
                    }
                }

                if (m_bankcode == "")
                {
                    return "银行不支持";
                }

                string userId = this.suppAccount;//商户ID

                Dictionary<String, String> paramdic = new Dictionary<String, String>();
                paramdic.Add("version", "V3.1.0.0");
                paramdic.Add("merNo", userId);
                paramdic.Add("orderNum", trade_no);
                paramdic.Add("amount", (amount * 100).ToString("f0"));
                paramdic.Add("bankCode", m_bankcode);
                paramdic.Add("bankAccountName", bank_account_name);//开户名
                paramdic.Add("bankAccountNo", bank_account_no);//银行卡号
                paramdic.Add("callBackUrl", notifyUrl);
                paramdic.Add("charset", "UTF-8");
                //排序
                paramdic = paramdic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);

                string sourceStr = "{";
                foreach (KeyValuePair<string, string> v in paramdic)
                {
                    sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
                }
                //{"key":"value"}md5key
                sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}" + ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayMD5Key"); //MD5秘钥

                paramdic.Add("sign", MD5Encrypt.MD5(sourceStr, false).ToUpper());

                paramdic = paramdic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);
                sourceStr = "{";
                foreach (KeyValuePair<string, string> v in paramdic)
                {
                    sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
                }
                //{"key":"value"..."sign":""}
                sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}";
                //Log.WriteTextLog("RuiFuDaiFuInfo", "代付提交md5加密后：" + sourceStr, DateTime.Now);
                string publicKey = ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfDaiPayPublicKey"); // 加密公匙;
                string cipher_data = "";
                publicKey = RSAEncodHelper.RSAPublicKeyJava2DotNet(publicKey);

                byte[] cdatabyte = RSAEncodHelper.RSAPublicKeySignByte(sourceStr, publicKey);
                cipher_data = Convert.ToBase64String(cdatabyte);

                string paramstr = "data=" + System.Web.HttpUtility.UrlEncode(cipher_data) + "&merchNo=" + userId + "&version=" + paramdic["version"];
                //Log.WriteTextLog("RuiFuDaiFuInfo", "请求参数：" + paramstr, DateTime.Now);
                //string p = HttpUtil.BuildQuery(paramdic, true);
                string d = paramstr;
                string strResult = HttpPost.SendPost("http://remit.qyfpay.com:90/api/remit.action", paramstr, 5000);


                return strResult;
            }
            catch (Exception ex)
            {
                //Log.WriteTextLog("RuiFuDaiFuInfo", "DFSubmit()==>" + ex.Message, DateTime.Now);
                return ex.Message;
            }
        }

        public string encode(string val)
        {
            return HttpUtility.UrlDecode(val, Encoding.GetEncoding("UTF-8"));
        }

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

        public string DFCallBackHandle(string strResult)
        {
            try
            {
                string userId = this.suppAccount;//商户ID
                /*{"merNo":"qyf201705200001","msg":"提交成功
                    ","orderNum":"201612120132343123uI","amount":"100","sign":"4A0FB7DDD59C4D1C1BBB52D8473B729B","stateCode":"00"}                    */
                //Log.WriteTextLog("RuiFuDaiFuInfo", "DFCallBackHandle()返回==>" + strResult, DateTime.Now);
                if (string.IsNullOrEmpty(strResult))
                    return "";
                if (strResult == "银行不支持") return "";
                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                LitJson.JsonData jd = JsonMapper.ToObject(strResult);
                if (jd == null)
                    return "";
                if ((string)jd["stateCode"] != "00")
                {
                    return string.Format("错误,响应码:{0},响应消息:{1}", (string)jd["stateCode"], (string)jd["msg"]);
                }
                paramsDic.Add("merNo", (string)jd["merNo"]);
                paramsDic.Add("stateCode", (string)jd["stateCode"]);
                paramsDic.Add("msg", (string)jd["msg"]);
                paramsDic.Add("orderNum", (string)jd["orderNum"]);
                paramsDic.Add("amount", (string)jd["amount"]);

                paramsDic = paramsDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);
                string sourceStr = "{";
                foreach (KeyValuePair<string, string> v in paramsDic)
                {
                    sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
                }
                sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}" + userId;
                if (MD5Encrypt.MD5(sourceStr, false).ToUpper().Equals((string)jd["sign"]) == false)
                {
                    return "验签失败!";
                }
                string result = string.Format("金额:{0},状态:{1},消息:{2}", (Convert.ToDecimal((string)jd["amount"])) / 100, (string)jd["stateCode"], (string)jd["msg"]);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Notify(string strResult)
        //public string SelectCallBackHandle(string strResult)
        {
            try
            {
                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                LitJson.JsonData jd = JsonMapper.ToObject(strResult);
                if (jd == null)
                    return;
                if ((string)jd["stateCode"] != "00")
                {
                    //return string.Format("错误,响应码:{0},响应消息:{1}", (string)jd["stateCode"], (string)jd["msg"]);
                }
                paramsDic.Add("merNo", (string)jd["merNo"]);
                paramsDic.Add("stateCode", (string)jd["stateCode"]);
                paramsDic.Add("orderNum", (string)jd["orderNum"]);
                paramsDic.Add("amount", (string)jd["amount"]);
                paramsDic.Add("msg", (string)jd["msg"]);
                paramsDic.Add("remitResult", (string)jd["remitResult"]);

                paramsDic = paramsDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, pp => pp.Value);
                string sourceStr = "{";
                foreach (KeyValuePair<string, string> v in paramsDic)
                {
                    sourceStr += string.Format("\"{0}\":\"{1}\",", v.Key, v.Value);
                }
                sourceStr = sourceStr.Substring(0, sourceStr.Length - 1) + "}" + ConfigHelper.GetConfig(PaymentSetting.SettingGroup, "QyfPayMD5Key"); //MD5秘钥
                if (MD5Encrypt.MD5(sourceStr, false).ToUpper().Equals((string)jd["sign"]) == false)
                {
                    HttpContext.Current.Response.Write("error");
                    HttpContext.Current.Response.End();
                    //return "验签失败!";
                }
                int status = 3;
                if ((string)jd["remitResult"] == "00")
                {
                    Withdraw.Complete(DaiFu.suppId, (string)jd["orderNum"], false, status, (Convert.ToDecimal((string)jd["amount"]) / 100m).ToString("f2"), (string)jd["stateCode"], "轻易付代付成功").ToString();
                    HttpContext.Current.Response.Write("0");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }


    }
}
