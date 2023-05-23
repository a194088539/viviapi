using com.yeepay.cmbn;
using com.yeepay.Utils;
using System;
using System.Text;
using System.Web;
using viviapi.BLL;
using viviLib.ExceptionHandling;

namespace viviapi.ETAPI.YeePay
{
    public class Card : ETAPIBase
    {
        private static int suppId = 102;
        private string p2_Order;
        private string p3_Amt;
        private string p4_verifyAmt;
        private string p5_Pid;
        private string p6_Pcat;
        private string p7_Pdesc;
        private string p8_Url;
        private string pa8_cardNo;
        private string pa9_cardPwd;
        private string pa_MP;
        private string pa7_cardAmt;
        private string pd_FrpId;
        private string pr_NeedResponse;
        private string pz_userId;
        private string pz1_userRegTime;

        public Card()
          : base(Card.suppId)
        {
        }

        public string CardTypeNo(int code)
        {
            switch (code)
            {
                case 103:
                    return "SZX";
                case 104:
                    return "SNDACARD";
                case 105:
                    return "ZHENGTU";
                case 106:
                    return "JUNNET";
                case 107:
                    return "QQCARD";
                case 108:
                    return "UNICOM";
                case 109:
                    return "JIUYOU";
                case 110:
                    return "NETEASE";
                case 111:
                    return "WANMEI";
                case 112:
                    return "SOHU";
                case 113:
                    return "TELECOM";
                case 115:
                    return "GUANGYU";
                case 117:
                    return "ZONGYOU";
                case 118:
                    return "TIANXIA";
                case 119:
                    return "TIANHONG";
                default:
                    return string.Empty;
            }
        }

        public string GetPayUrl(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errmsg)
        {
            errmsg = string.Empty;
            string suppAccount = this.suppAccount;
            string suppKey = this.suppKey;
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            this.p2_Order = _orderid;
            this.p3_Amt = Decimal.Parse(cardvalue.ToString()).ToString("0.00");
            this.p4_verifyAmt = "false";
            this.p5_Pid = "product";
            this.p6_Pcat = "producttype";
            this.p7_Pdesc = "productdesc";
            this.p8_Url = this.SiteDomain + "/notify/YeePay_Card_Return.aspx";
            this.pa_MP = "";
            this.pa7_cardAmt = Decimal.Parse(cardvalue.ToString()).ToString("0.00");
            this.pa8_cardNo = _cardno;
            this.pa9_cardPwd = _cardpwd;
            this.pd_FrpId = this.CardTypeNo(_typeId);
            this.pr_NeedResponse = "1";
            this.pz_userId = suppAccount;
            this.pz1_userRegTime = "";
            try
            {
                SZXResult szxResult = SZX.AnnulCard(this.suppKey, this.suppAccount, this.p2_Order, this.p3_Amt, this.p4_verifyAmt, this.p5_Pid, this.p6_Pcat, this.p7_Pdesc, this.p8_Url, this.pa_MP, this.pa7_cardAmt, this.pa8_cardNo, this.pa9_cardPwd, this.pd_FrpId, this.pr_NeedResponse, this.pz_userId, this.pz1_userRegTime);
                if (szxResult.R1_Code == "1")
                    return "0";
                if (szxResult.R1_Code == "11")
                {
                    errmsg = "订单号重复";
                    return "-1";
                }
                else if (szxResult.R1_Code == "7")
                {
                    errmsg = "卡密无效";
                    return "-1";
                }
                else if (szxResult.R1_Code == "61")
                {
                    errmsg = "卡密无效";
                    return "-1";
                }
                else
                {
                    errmsg = "未知错误，错误编码：" + szxResult.R1_Code;
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return "-1";
            }
        }

        public void Return(HttpContext context)
        {
            SZX.logURL(context.Request.RawUrl);
            SZXCallbackResult szxCallbackResult = SZX.VerifyCallback(this.suppKey, FormatQueryString.GetQueryString("r0_Cmd"), FormatQueryString.GetQueryString("r1_Code"), FormatQueryString.GetQueryString("p1_MerId"), FormatQueryString.GetQueryString("p2_Order"), FormatQueryString.GetQueryString("p3_Amt"), FormatQueryString.GetQueryString("p4_FrpId"), FormatQueryString.GetQueryString("p5_CardNo"), FormatQueryString.GetQueryString("p6_confirmAmount"), FormatQueryString.GetQueryString("p7_realAmount"), FormatQueryString.GetQueryString("p8_cardStatus"), FormatQueryString.GetQueryString("p9_MP"), FormatQueryString.GetQueryString("pb_BalanceAmt"), FormatQueryString.GetQueryString("pc_BalanceAct"), FormatQueryString.GetQueryString("hmac"));
            if (string.IsNullOrEmpty(szxCallbackResult.ErrMsg))
            {
                string opstate = "-1";
                int status = szxCallbackResult.R1_Code == "1" ? 2 : 4;
                if (status == 2)
                    opstate = "0";
                new OrderCard().ReceiveSuppResult(Card.suppId, szxCallbackResult.P2_Order, szxCallbackResult.P5_CardNo, status, opstate, this.GetMsgInfo(szxCallbackResult.P8_cardStatus), Decimal.Parse(szxCallbackResult.P3_Amt), new Decimal(0), szxCallbackResult.P8_cardStatus);
                HttpContext.Current.Response.Write("SUCCESS");
            }
            else
            {
                context.Response.Write("交易签名无效!");
                context.Response.Write("<BR>YeePay-HMAC:" + szxCallbackResult.Hmac);
                context.Response.Write("<BR>LocalHost:" + szxCallbackResult.ErrMsg);
            }
        }

        public string GetMsgInfo(string cardstatus)
        {
            switch (cardstatus)
            {
                case "0":
                    return "支付成功";
                case "1":
                    return "支付成功";
                case "7":
                    return "卡号卡密或卡面额不符合规则";
                case "1002":
                    return "本张卡密您提交过于频繁，请您稍后再试";
                case "1003":
                    return "不支持的卡类型（比如电信地方卡）";
                case "1004":
                    return "密码错误或充值卡无效";
                case "1006":
                    return "充值卡无效";
                case "1007":
                    return "卡内余额不足";
                case "1008":
                    return "余额卡过期（有效期1个月）";
                case "1010":
                    return "此卡正在处理中";
                case "10000":
                    return "未知错误";
                case "2005":
                    return "此卡已使用";
                case "2006":
                    return "卡密在系统处理中";
                case "2007":
                    return "该卡为假卡";
                case "2008":
                    return "该卡种正在维护";
                case "2009":
                    return "浙江省移动维护";
                case "2010":
                    return "江苏省移动维护";
                case "2011":
                    return "福建省移动维护";
                case "2012":
                    return "辽宁省移动维护";
                case "2013":
                    return "该卡已被锁定";
                case "2014":
                    return "系统繁忙，请稍后再试";
                default:
                    return cardstatus;
            }
        }
    }
}
