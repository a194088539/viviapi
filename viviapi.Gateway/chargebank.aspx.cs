using System;
using System.Text;
using System.Web;
using System.Web.UI;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.Order;
using viviapi.BLL.Sys;
using viviapi.BLL.User;
using viviapi.Cache;
using viviapi.ETAPI;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.Sys;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.Web;

namespace viviapi.gateway
{
    public class chargebank : Page
    {
        public decimal MaxChargeAMT = TransactionSetting.MaxTranATM;

        public decimal MinTranAMT = TransactionSetting.MinTranATM;

        public string userid
        {
            get
            {
                return this.GetParmValue("parter");
            }
        }

        public string bankid
        {
            get
            {
                return this.GetParmValue("type");
            }
        }

        public string money
        {
            get
            {
                return this.GetParmValue("value");
            }
        }

        public string orderid
        {
            get
            {
                return this.GetParmValue("orderid");
            }
        }

        public string notifyurl
        {
            get
            {
                return this.GetParmValue("callbackurl");
            }
        }

        public string returnurl
        {
            get
            {
                return this.GetParmValue("hrefbackurl");
            }
        }

        public string clientIp
        {
            get
            {
                return this.GetParmValue("payerIp");
            }
        }

        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("attach", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string version
        {
            get
            {
                return SystemApiHelper.vbmyapi10;
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("sign");
            }
        }

        public int agentId
        {
            get
            {
                int _agentId = 0;
                try
                {
                    string parms = this.GetParmValue("agent");
                    if (!string.IsNullOrEmpty(parms))
                    {
                        int.TryParse(parms, out _agentId);
                    }
                    else
                    {
                        parms = this.GetParmValue("hashcode");
                        if (!string.IsNullOrEmpty(parms))
                        {
                            _agentId = Convert.ToInt32(parms, 16);
                        }
                    }
                }
                catch
                {
                }
                return _agentId;
            }
        }

        public string GetParmValue(string parmName)
        {
            string _parmValue = WebBase.GetQueryStringString(parmName, "");
            if (string.IsNullOrEmpty(_parmValue))
            {
                _parmValue = WebBase.GetFormString(parmName, "");
            }
            return _parmValue;
        }

        private bool isReturnUrlOk()
        {
            bool result;
            if (this.returnurl == null || this.returnurl.Length == 0)
            {
                result = true;
            }
            else
            {
                bool isUrl = viviLib.Text.Validate.IsUrl(this.returnurl);
                if (isUrl)
                {
                    result = (!this.returnurl.Contains("?") && !this.returnurl.Contains("&"));
                }
                else
                {
                    result = isUrl;
                }
            }
            return result;
        }

        private bool isNotifyUrlOk()
        {
            bool result;
            if (this.notifyurl == null || this.notifyurl.Length == 0)
            {
                result = false;
            }
            else
            {
                bool isUrl = viviLib.Text.Validate.IsUrl(this.notifyurl);
                if (isUrl)
                {
                    result = (!this.notifyurl.Contains("?") && !this.notifyurl.Contains("&"));
                }
                else
                {
                    result = isUrl;
                }
            }
            return result;
        }

        private bool isClientIpOk()
        {
            return this.clientIp == null || this.clientIp.Length == 0 || viviLib.Text.Validate.IsIPSect(this.clientIp);
        }

        private bool CheckUrlReferrer(int uid)
        {
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(this.userid))
            {
                error = "error:1001 商户ID（parter）不能空！";
            }
            else if (string.IsNullOrEmpty(this.bankid))
            {
                error = "error:1002 银行类型（type）不能空！";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                error = "error:1003 订单金额（value）不能空！";
            }
            else if (string.IsNullOrEmpty(this.orderid))
            {
                error = "error:1004 商户订单号（orderid）不能空！";
            }
            else if (string.IsNullOrEmpty(this.notifyurl))
            {
                error = "error:1005 下行异步通知地址（callbackurl）不能空！";
            }
            else if (string.IsNullOrEmpty(this.sign))
            {
                error = "error:1006 MD5签名（sign）不能空！";
            }
            else if (this.userid.Length > 5)
            {
                error = "error:1020 商户ID（parter）长度超过5位！";
            }
            else if (this.bankid.Length > 4)
            {
                error = "error:1021 银行类型（type）长度超过4位！";
            }
            else if (this.orderid.Length > 30)
            {
                error = "error:1022 商户订单号（orderid）长度超过30位！";
            }
            else if (this.money.Length > 8)
            {
                error = "error:1023 订单金额（value）长度超过最长限制！";
            }
            else if (this.notifyurl.Length > 255)
            {
                error = "error:1024 下行异步通知地址（callbackurl）长度超过255位！";
            }
            else if (this.returnurl.Length > 255)
            {
                error = "error:1025 下行同步通知地址（hrefbackurl）长度超过255位！";
            }
            else if (this.clientIp.Length > 20)
            {
                error = "error:1026 支付用户IP（payerIp）长度超过20位！";
            }
            else if (this.attach.Length > 255)
            {
                error = "error:1027 备注消息（attach）长度超过255位！";
            }
            else if (this.sign.Length != 32)
            {
                error = "error:1028 签名（sign）长度不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.userid))
            {
                error = "error:1040 商户ID（parter）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.bankid))
            {
                error = "error:1041 银行类型（type）格式不正确！";
            }
            else if (!this.isNotifyUrlOk())
            {
                error = "error:1043 下行异步通知地址（callbackurl）格式不正确！";
            }
            else if (!this.isReturnUrlOk())
            {
                error = "error:1044 下行同步通知地址（hrefbackurl）格式不正确！";
            }
            else if (!this.isClientIpOk())
            {
                error = "error:1045 支付用户IP（payerIp）格式不正确！";
            }
            if (!string.IsNullOrEmpty(error))
            {
                WebUtility.ShowErrorMsg(error);
            }
            else
            {
                UserInfo userInfo = null;
                decimal tranAmt = 0m;
                int userId = int.Parse(this.userid);
                if (!this.CheckUrlReferrer(userId))
                {
                    string Host = string.Empty;
                    if (base.Request.UrlReferrer != null)
                    {
                        Host = base.Request.UrlReferrer.Host;
                    }
                    error = string.Format("error:1070 来路地址不合法！{0}", Host);
                }
                else if (!decimal.TryParse(this.money, out tranAmt))
                {
                    error = "error:1060 订单金额（value）有误！";
                }
                else if (tranAmt < this.MinTranAMT)
                {
                    error = "error:1061 订单金额（value）小于最小允许交易额！";
                }
                else if (tranAmt > this.MaxChargeAMT)
                {
                    error = string.Format("error:1062 订单金额（value）{0:f2}大于最大允许交易额{1:f2}！", tranAmt, this.MaxChargeAMT);
                }
                else
                {
                    bool ischeckuserorder = RuntimeSetting.CheckUserOrderNo;
                    int checkResult = Dal.BankOrder_DataCheck(userId, ischeckuserorder, this.orderid, out userInfo);
                    if (checkResult == 1)
                    {
                        error = "error:1064 商户编号不存在";
                    }
                    else if (checkResult == 2)
                    {
                        error = "error:1065 商户状态不正常";
                    }
                    else if (checkResult == 3)
                    {
                        error = "error:1069 商户订单号重复";
                    }
                    else if (!WebUtility.BankMD5Check(this.userid, this.bankid, this.money, this.orderid, this.notifyurl, userInfo.APIKey, this.sign))
                    {
                        error = "error:1066 签名错误!";
                    }
                }
                if (!string.IsNullOrEmpty(error))
                {
                    WebUtility.ShowErrorMsg(error);
                }
                else
                {
                    ChannelInfo channelInfo = Channel.GetModel(this.bankid, userId, true);
                    if (channelInfo == null)
                    {
                        error = "error:1067:银行编号不存在!";
                    }
                    else if (channelInfo.isOpen.Value != 1)
                    {
                        error = "error:1068:通道维护中!";
                    }
                    if (!string.IsNullOrEmpty(error))
                    {
                        if (viviapi.BLL.SysConfig.debuglog)
                        {
                            if (userInfo.isdebug == 1)
                            {
                                debuginfo _debugInfo = new debuginfo();
                                _debugInfo.addtime = new DateTime?(DateTime.Now);
                                _debugInfo.bugtype = debugtypeenum.网银订单;
                                _debugInfo.detail = string.Empty;
                                _debugInfo.errorcode = error;
                                _debugInfo.errorinfo = error;
                                _debugInfo.userid = new int?(userInfo.ID);
                                if (base.Request.RawUrl != null)
                                {
                                    _debugInfo.url = base.Request.RawUrl.ToString();
                                }
                                else
                                {
                                    _debugInfo.url = string.Empty;
                                }
                                debuglog.Insert(_debugInfo);
                            }
                        }
                        WebUtility.ShowErrorMsg(error);
                    }
                    else
                    {
                        int typeId = channelInfo.typeId;
                        int supplierId = channelInfo.supplier.Value;
                        OrderBank newOrder = new OrderBank();
                        OrderBankInfo order = new OrderBankInfo();
                        order.orderid = newOrder.GenerateUniqueOrderId(typeId);
                        order.addtime = DateTime.Now;
                        order.attach = this.attach;
                        order.notifycontext = string.Empty;
                        order.notifycount = 0;
                        order.notifystat = 0;
                        order.notifyurl = this.notifyurl;
                        order.clientip = ServerVariables.TrueIP;
                        order.completetime = new DateTime?(DateTime.Now);
                        order.returnurl = this.returnurl;
                        order.ordertype = 1;
                        order.typeId = typeId;
                        order.paymodeId = this.bankid;
                        order.supplierId = supplierId;
                        order.supplierOrder = string.Empty;
                        order.userid = userId;
                        order.userorder = this.orderid;
                        order.refervalue = tranAmt;
                        if (base.Request.UrlReferrer != null)
                        {
                            order.referUrl = base.Request.UrlReferrer.ToString();
                        }
                        else
                        {
                            order.referUrl = string.Empty;
                        }
                        order.server = new int?(RuntimeSetting.ServerId);
                        order.manageId = userInfo.manageId;
                        if (!order.manageId.HasValue || order.manageId.Value <= 0)
                        {
                            if (this.agentId > 0)
                            {
                                if (UserFactory.chkAgent(this.agentId))
                                {
                                    order.agentId = this.agentId;
                                }
                            }
                            else
                            {
                                order.agentId = UserFactory.GetPromID(userId);
                            }
                        }
                        order.version = this.version;
                        WebCache.GetCacheService().AddObject(order.orderid, order, TransactionSetting.ExpiresTime);
                        newOrder.Insert(order);
                        SellFactory.OnlineBankPay(supplierId, order.orderid, order.refervalue, order.paymodeId);
                    }
                }
            }
        }
    }
}
