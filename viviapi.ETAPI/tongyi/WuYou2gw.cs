using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviapi.Model;
using viviapi.Model.Sys;
using viviapi.SysConfig;
using viviLib.ExceptionHandling;
using viviLib.Security;

namespace viviapi.ETAPI.tongyi
{
    public class WuYou2gw : ETAPIBase
    {
        private static int suppId = 10041;
        private string noticestr = RuntimeSetting.SiteDomain + "/notify/WuYou2_AgentNotify.aspx";
        private PayHttpClient pay = new PayHttpClient();

        static WuYou2gw()
        {
        }

        public WuYou2gw()
          : base(WuYou2gw.suppId)
        {
        }

        public WuYou2gw(int _suppId)
                : base(_suppId)
        {
            suppId = _suppId;
        }

        public string settlementOrder(string trade_no, string bank_name, string bank_code, string bank_site_name, string ext2, string ext3, string bank_account_name, string bank_account_no, Decimal amount, int suppid, string remark)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(suppid);
            debuginfo debuginfo = new debuginfo()
            {
                addtime = new DateTime?(DateTime.Now),
                bugtype = debugtypeenum.对私代发接口,
                detail = string.Empty,
                errorcode = "",
                errorinfo = "",
                userid = new int?(0)
            };
            string str1 = "http://order.ganningdz.com/api/gatewaypaid";
            string str2 = "";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("mchId", cacheModel.puserid);
            dictionary.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            string str3 = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(Util.DictionaryToJson(new Dictionary<string, string>()
      {
        {
          "outOrderId",
          trade_no
        },
        {
          "payType",
          "unionpay.gatewaypay"
        },
        {
          "fundType",
          "T1"
        },
        {
          "amount",
          (amount * 100).ToString("f0")
        },
        {
          "acctName",
          bank_account_name
        },
        {
          "acctNo",
          bank_account_no
        },
        {
          "bankEnglishName",
          this.GetBankCode(bank_code)
        },
        {
          "bankBranchName",
          this.GetBankName(bank_code)
        },
        {
          "bankBranchCode",
          this.GetBranchCode(bank_code)
        },
        {
          "notify_url",
          this.noticestr
        }
      })));
            dictionary.Add("data", str3);
            string str4 = Cryptography.MD5(Util.CreateLinkString2(dictionary) + "&key=" + cacheModel.puserkey, "UTF-8").ToUpper();
            dictionary.Add("signature", str4);
            this.pay.setReqContent(new Dictionary<string, string>()
      {
        {
          "url",
          str1
        },
        {
          "data",
          Util.DictionaryToJson(dictionary)
        }
      });
            if (this.pay.call1())
                str2 = this.pay.getResContent();
            else
                Log.WriteLog(this.GetType().ToString(), "收单错误: ", this.pay.getErrInfo());
            return str2;
        }

        public bool DoTrans(viviapi.Model.distribution info)
        {
            bool flag = false;
            try
            {
                WuYou2gw.Resultinfo resultinfo = (WuYou2gw.Resultinfo)JsonConvert.DeserializeObject(this.settlementOrder(info.trade_no, info.bankName, info.bankCode, info.bankBranch, info.ext2, info.ext3, info.bankAccountName, info.bankAccount, info.amount, info.suppid, string.Empty), typeof(WuYou2gw.Resultinfo));
                if (resultinfo.status == "SUCCESS")
                {
                    if (resultinfo.resultCode == "200")
                    {
                        string tradeNo = info.trade_no;
                        int status = 3;
                        bool is_cancel = false;
                        Log.WriteLog(this.GetType().ToString(), "代付同步通知状态", Withdraw.Complete(WuYou2gw.suppId, tradeNo, is_cancel, status, info.amount.ToString(), resultinfo.message, resultinfo.message).ToString());
                        flag = true;
                    }
                    else
                    {
                        Withdraw.Complete(WuYou2gw.suppId, info.trade_no, true, 1, info.amount.ToString(), info.trade_no, resultinfo.message);
                        Log.WriteLog(this.GetType().ToString(), "代付失败", resultinfo.message);
                    }
                }
                else
                {
                    Withdraw.Complete(WuYou2gw.suppId, info.trade_no, true, 1, info.amount.ToString(), info.trade_no, resultinfo.message);
                    Log.WriteLog(this.GetType().ToString(), "代付失败", resultinfo.message);
                }
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return false;
        }

        public void Notify()
        {
            Stream inputStream = HttpContext.Current.Request.InputStream;
            StreamReader streamReader = new StreamReader(HttpContext.Current.Request.InputStream);
            string str1 = streamReader.ReadToEnd();
            Log.WriteLog(this.GetType().ToString(), "代付返回CONTENT: ", str1);
            using (streamReader)
            {
                Dictionary<string, string> dicArray = Util.JsonToDictionary1(str1);
                string str2 = dicArray["signature"];
                dicArray.Remove("signature");
                SupplierInfo cacheModel = SupplierFactory.GetCacheModel(WuYou2gw.suppId);
                if (Cryptography.MD5(Util.CreateLinkString3(dicArray) + "&key=" + cacheModel.puserkey, "UTF-8").ToUpper() == str2)
                {
                    if (dicArray["status"] == "SUCCESS")
                    {
                        int status = 3;
                        bool is_cancel = false;
                        Log.WriteLog(this.GetType().ToString(), "代付异步步通知状态", Withdraw.Complete(WuYou2gw.suppId, dicArray["outOrderId"], is_cancel, status, (Decimal.Parse(dicArray["amount"]) / new Decimal(100)).ToString(), dicArray["outOrderId"], dicArray["status"]).ToString());
                    }
                    else
                    {
                        Withdraw.Complete(WuYou2gw.suppId, dicArray["outOrderId"], true, 1, "0", dicArray["outOrderId"], dicArray["status"]);
                        Log.WriteLog(this.GetType().ToString(), "代付失败", dicArray["status"]);
                    }
                }
                else
                    Log.WriteLog(this.GetType().ToString(), "验签失败 ", str1);
            }
            HttpContext.Current.Response.Write("SUCCESS");
            HttpContext.Current.Response.End();
        }

        protected string GetBankCode(string bankCode)
        {
            string str = bankCode;
            switch (bankCode)
            {
                case "1002":
                    str = "ICBC";
                    break;
                case "1005":
                    str = "ABC";
                    break;
                case "1003":
                    str = "CCB";
                    break;
                case "1026":
                    str = "BOC";
                    break;
                case "1001":
                    str = "CMB";
                    break;
                case "1006":
                    str = "CMSB";
                    break;
                case "1020":
                    str = "BCOM";
                    break;
                case "1025":
                    str = "HXBJ";
                    break;
                case "1009":
                    str = "IBCN";
                    break;
                case "1027":
                    str = "CGBC";
                    break;
                case "1004":
                    str = "SPDB";
                    break;
                case "1022":
                    str = "CEB";
                    break;
                case "1021":
                    str = "CTIB";
                    break;
                case "1010":
                    str = "SZPA";
                    break;
                case "1066":
                    str = "PSBC";
                    break;
            }
            return str;
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

        protected string GetBranchCode(string bankCode)
        {
            string str = bankCode;
            switch (bankCode)
            {
                case "1002":
                    str = "102100099996";
                    break;
                case "1005":
                    str = "103100000018";
                    break;
                case "1003":
                    str = "105100000017";
                    break;
                case "1026":
                    str = "104100000004";
                    break;
                case "1001":
                    str = "308584000013";
                    break;
                case "1006":
                    str = "305100000013";
                    break;
                case "1020":
                    str = "301290000007";
                    break;
                case "1025":
                    str = "304100040000";
                    break;
                case "1009":
                    str = "309391000011";
                    break;
                case "1027":
                    str = "306581000003";
                    break;
                case "1004":
                    str = "310290000013";
                    break;
                case "1022":
                    str = "303100010077";
                    break;
                case "1021":
                    str = "302100011000";
                    break;
                case "1010":
                    str = "307584008005";
                    break;
                case "1066":
                    str = "403100000004";
                    break;
            }
            return str;
        }

        public class Resultinfo
        {
            public string status;
            public string data;
            public string message;
            public string resultCode;
            public string timestamp;
            public string signature;
        }
    }
}

