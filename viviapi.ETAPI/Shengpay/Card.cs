namespace viviapi.ETAPI.Shengpay
{
    using System;
    using System.Web;
    using viviapi.BLL;
    using viviapi.Cache;
    using viviapi.ETAPI;
    using viviapi.ETAPI.ServiceReference1;
    using viviapi.Model.Order;
    using viviapi.SysConfig;
    using viviLib.ExceptionHandling;
    using viviLib.Security;

    public class Card : ETAPIBase
    {
        public string notify_url;
        private static int suppId = 900;

        public Card()
            : base(suppId)
        {
            this.notify_url = RuntimeSetting.SiteDomain + "/notify/sheng_card_notity.aspx";
        }

        public string CardSend(string _orderid, string _cardno, string _cardpwd, int _typeId, int cardvalue, out string errmsg)
        {
            errmsg = string.Empty;
            string str = "-1";
            PayGateRequest request = new PayGateRequest();
            request.Version = "3.0";
            request.Amount = cardvalue.ToString("0.00");
            request.OrderNo = _orderid;
            request.MerchantNo = base._suppInfo.puserid1;
            request.MerchantUserId = string.Empty;
            request.NotifyUrl = this.notify_url;
            request.OrderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            request.CurrencyType = "RMB";
            request.NotifyUrlType = "http";
            request.SignType = "2";
            request.ProductNo = string.Empty;
            request.ProductDesc = string.Empty;
            request.Remark1 = _cardno;
            request.Remark2 = string.Empty;
            request.CharSet = "GBK";
            SDCardInfo[] infoArray = new SDCardInfo[1];
            SDCardInfo info = new SDCardInfo();
            info.CardNO = _cardno;
            info.CardPassword = _cardpwd;
            infoArray[0] = info;
            request.Cards = infoArray;
            string str3 = Cryptography.MD5((request.Version + request.Amount + request.OrderNo + request.MerchantNo + request.MerchantUserId + request.NotifyUrl + request.OrderTime + request.CurrencyType + request.NotifyUrlType + request.SignType + request.ProductNo + request.ProductDesc + request.Remark1 + request.Remark2 + request.CharSet + _cardno + _cardpwd) + base._suppInfo.puserkey1).ToUpper();
            request.Mac = str3;
            PayGateResponse response = new ApiPaySoapClient().Pay(request);
            if (response == null)
            {
                return str;
            }
            SDCardResult[] cards = response.Cards;
            if (response.Code == 0)
            {
                return "0";
            }
            errmsg = response.Code.ToString() + "(" + this.getErrMsgInfo(response.Code.ToString()) + ")";
            return "-1";
        }

        public string getErrMsgInfo(string code)
        {
            string str = string.Empty;
            switch (code)
            {
                case "0":
                    return "成功";

                case "-99999":
                    return "未知错误";

                case "-1":
                    return "系统异常";

                case "04":
                    return "您输入的卡号已被封，请联系您购卡的经销商！";

                case "03":
                    return "该卡已被使用";

                case "05":
                    return "卡被异常封停";

                case "06":
                    return "卡被永久封停";

                case "-200":
                    return "部分卡无效";

                case "-201":
                    return "正常卡";

                case "-203":
                    return "已使用的卡";

                case "-220":
                    return "不存在的卡，或已经过期的卡";

                case "-204":
                    return "卡被封了";

                case "-205":
                    return "卡被异常封停";

                case "-206":
                    return "卡被永久封停";

                case "-207":
                    return "卡还没生效";

                case "-208":
                    return "卡号或密码错误";

                case "-210":
                    return "卡余额不足";

                case "-211":
                    return "提供卡过多";

                case "-212":
                    return "您输入的卡号不能充值该游戏，请确认后重新输入";

                case "-217":
                    return "更新支付、充值状态失败";

                case "-299":
                    return "扣卡异常";

                case "-250":
                    return "该卡不能支付该商户";

                case "-300":
                    return "请求参数非法";

                case "-301":
                    return "更新订单状态失败";

                case "-1011":
                    return "该卡不是有效卡";

                case "-1004":
                    return "该卡号不存在或者余额为0";

                case "-1006":
                    return "卡的余额不足";

                case "-1201":
                    return "出现异常";

                case "-1202":
                    return "已进行过充值";

                case "-12101":
                    return "充值游戏不支持绿卡";

                case "-10214":
                    return "已进行过充值";
            }
            return str;
        }

        public void Notify()
        {
            string opstate = "0";
            string str2 = HttpContext.Current.Request.Form["Amount"];
            string s = HttpContext.Current.Request.Form["PayAmount"];
            string objId = HttpContext.Current.Request.Form["OrderNo"];
            string supplierOrderId = HttpContext.Current.Request.Form["serialno"];
            string errtype = HttpContext.Current.Request.Form["Status"];
            string str7 = HttpContext.Current.Request.Form["MerchantNo"];
            string str8 = HttpContext.Current.Request.Form["PayChannel"];
            string str9 = HttpContext.Current.Request.Form["Discount"];
            string str10 = HttpContext.Current.Request.Form["SignType"];
            string str11 = HttpContext.Current.Request.Form["PayTime"];
            string str12 = HttpContext.Current.Request.Form["CurrencyType"];
            string str13 = HttpContext.Current.Request.Form["ProductNo"];
            string str14 = HttpContext.Current.Request.Form["ProductDesc"];
            string str15 = HttpContext.Current.Request.Form["TransTime"];
            string str16 = HttpContext.Current.Request.Form["Remark1"];
            string str17 = HttpContext.Current.Request.Form["Remark2"];
            string str18 = HttpContext.Current.Request.Form["ExInfo"];
            string str19 = HttpContext.Current.Request.Form["MAC"];
            if (Cryptography.MD5((str2 + "|" + s + "|" + objId + "|" + supplierOrderId + "|" + errtype + "|" + str7 + "|" + str8 + "|" + str9 + "|" + str10 + "|" + str11 + "|" + str12 + "|" + str13 + "|" + str14 + "|" + str16 + "|" + str17 + "|" + str18) + "|" + base._suppInfo.puserkey1).ToUpper().ToUpper() == str19.ToUpper())
            {
                try
                {
                    int status = 4;
                    OrderCard card = new OrderCard();
                    OrderCardInfo model = WebCache.GetCacheService().RetrieveObject(objId) as OrderCardInfo;
                    if (model == null)
                    {
                        model = card.GetModel(objId);
                    }
                    status = (errtype == "01") ? 2 : 4;
                    if (status == 2)
                    {
                        opstate = "0";
                    }
                    string msg = errtype;
                    string userviewmsg = msg;
                    card.DoCardComplete(suppId, objId, supplierOrderId, status, opstate, msg, userviewmsg, decimal.Parse(s), 0M, errtype, 1);
                    HttpContext.Current.Response.Write("OK");
                    HttpContext.Current.Response.End();
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
        }
    }
}

