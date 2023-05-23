using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using uniondemo.com.allinpay.syb;
using viviapi.BLL;

namespace viviapi.ETAPI.TongLian2
{
    public class Code : ETAPIBase
    {
        string codekey = "43df939f1e7f5c6909b3f4b63f893994";
        string codesuppAccount = "990440153996000";
        string codeappid = "00000000";
        private static int suppId = 10003;

        internal string notifyUrl
        {
            get
            {
                return this.SiteDomain + "/notify/tonglian2/Notify.aspx";
            }
        }

        public Code()
            : base(Code.suppId)
        {
        }

        public string PayBank(string orderid, Decimal orderAmt, string bankcode)
        {
            string paytype = "W01";
            if (bankcode == "1004" || bankcode == "1007")
                paytype = "W01";
            else if (bankcode == "992" || bankcode == "1006")
                paytype = "A01";
            else if (bankcode == "1008" || bankcode == "1009")
                paytype = "Q01";

            Dictionary<String, String> paramDic = buildBasicParam();
            paramDic.Add("trxamt", (orderAmt * 100).ToString("f0"));
            paramDic.Add("reqsn", orderid);
            paramDic.Add("paytype", paytype);
            paramDic.Add("body", orderid);
            paramDic.Add("remark", orderid);
            paramDic.Add("acct", "");
            paramDic.Add("authcode", "");
            paramDic.Add("notify_url", notifyUrl);
            paramDic.Add("limit_pay", "");
            paramDic.Add("sign", AppUtil.signParam(paramDic, codekey));

            String rsp = HttpUtil.CreatePostHttpResponse("https://vsp.allinpay.com/apiweb/unitorder/pay", paramDic, Encoding.UTF8);
            Dictionary<String, String> rspDic = (Dictionary<String, String>)JsonConvert.DeserializeObject(rsp, typeof(Dictionary<String, String>));
            if ("SUCCESS".Equals(rspDic["retcode"]))//验签
            {
                //String signRsp = rspDic["sign"];
                //rspDic.Remove("sign");
                //String sign = AppUtil.signParam(rspDic, "43df939f1e7f5c6909b3f4b63f893994");
                return rspDic["payinfo"];
            }
            else
            {
                return "error:" + rspDic["retmsg"];
            }
        }
        private Dictionary<String, String> buildBasicParam()
        {
            Dictionary<String, String> paramDic = new Dictionary<String, String>();
            paramDic.Add("cusid", codesuppAccount);
            paramDic.Add("appid", codeappid);
            paramDic.Add("version", "11");
            paramDic.Add("randomstr", DateTime.Now.ToFileTime().ToString());
            return paramDic;

        }
        public static bool validSign(Dictionary<String, String> param, String appkey)
        {
            String signRsp = param["sign"];
            param.Remove("sign");
            String sign = AppUtil.signParam(param, "");
            param.Add("key", appkey);
            String blankStr = AppUtil.BuildParamStr(param);
            return AppUtil.MD5Encrypt(blankStr).ToLower().Equals(signRsp.ToLower());

        }
        public void Notify()
        {
            Dictionary<String, String> reqParams = new Dictionary<String, String>();
            for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
            {
                if (!reqParams.ContainsKey(HttpContext.Current.Request.Form.Keys[i]))
                {
                    reqParams.Add(HttpContext.Current.Request.Form.Keys[i], HttpContext.Current.Request.Form[i].ToString());
                }
            }

            if (reqParams.ContainsKey("sign"))//如果不包含sign,则不进行处理
            {
                string msg = "支付失败";// + this.GetErrorInfo(result, resultDesc)
                string opstate = "-1";
                int status1 = 4;
                if (reqParams["trxstatus"] == "0000")
                {
                    //if (validSign(reqParams, codekey))//验签成功
                    //{
                    msg = "支付成功";
                    opstate = "0";
                    status1 = 2;
                    //LogHelper.Write("通联异步开始处理数据：signdata" + signdata);
                    new OrderBank().DoBankComplete(Code.suppId, reqParams["outtrxid"], reqParams["trxid"], status1, opstate, msg, Decimal.Parse(reqParams["trxamt"]) / 100m, new Decimal(0), true, false);
                    HttpContext.Current.Response.Write("success");
                    //}
                }
            }
        }
    }
}
