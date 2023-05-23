namespace viviapi.gateway
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using viviapi.BLL;
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

    public class paybank : Page
    {
        public decimal MaxChargeAMT = TransactionSetting.MaxTranATM;
        public decimal MinTranAMT = TransactionSetting.MinTranATM;

        private bool CheckUrlReferrer(int uid)
        {
            return true;
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

        private bool isClientIpOk()
        {
            return (((this.clientIp == null) || (this.clientIp.Length == 0)) || viviLib.Text.Validate.IsIPSect(this.clientIp));
        }

        private bool isNotifyUrlOk()
        {
            if ((this.notifyurl == null) || (this.notifyurl.Length == 0))
            {
                return false;
            }
            bool flag = viviLib.Text.Validate.IsUrl(this.notifyurl);
            if (flag)
            {
                return (!this.notifyurl.Contains("?") && !this.notifyurl.Contains("&"));
            }
            return flag;
        }

        private bool isReturnUrlOk()
        {
            if ((this.returnurl == null) || (this.returnurl.Length == 0))
            {
                return true;
            }
            bool flag = viviLib.Text.Validate.IsUrl(this.returnurl);
            if (flag)
            {
                return (!this.returnurl.Contains("?") && !this.returnurl.Contains("&"));
            }
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(this.userid))
            {
                str = "error:1001 商户ID（parter）不能空！";
            }
            else if (string.IsNullOrEmpty(this.bankid))
            {
                str = "error:1002 银行类型（type）不能空！";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                str = "error:1003 订单金额（value）不能空！";
            }
            else if (string.IsNullOrEmpty(this.orderid))
            {
                str = "error:1004 商户订单号（orderid）不能空！";
            }
            else if (string.IsNullOrEmpty(this.notifyurl))
            {
                str = "error:1005 下行异步通知地址（callbackurl）不能空！";
            }
            else if (string.IsNullOrEmpty(this.sign))
            {
                str = "error:1006 MD5签名（sign）不能空！";
            }
            else if (this.userid.Length > 5)
            {
                str = "error:1020 商户ID（parter）长度超过5位！";
            }
            else if (this.bankid.Length > 4)
            {
                str = "error:1021 银行类型（type）长度超过4位！";
            }
            else if (this.orderid.Length > 30)
            {
                str = "error:1022 商户订单号（orderid）长度超过30位！";
            }
            else if (this.money.Length > 8)
            {
                str = "error:1023 订单金额（value）长度超过最长限制！";
            }
            else if (this.notifyurl.Length > 0xff)
            {
                str = "error:1024 下行异步通知地址（callbackurl）长度超过255位！";
            }
            else if (this.returnurl.Length > 0xff)
            {
                str = "error:1025 下行同步通知地址（hrefbackurl）长度超过255位！";
            }
            else if (this.clientIp.Length > 20)
            {
                str = "error:1026 支付用户IP（payerIp）长度超过20位！";
            }
            else if (this.attach.Length > 0xff)
            {
                str = "error:1027 备注消息（attach）长度超过255位！";
            }
            else if (this.sign.Length != 0x20)
            {
                str = "error:1028 签名（sign）长度不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.userid))
            {
                str = "error:1040 商户ID（parter）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.bankid))
            {
                str = "error:1041 银行类型（type）格式不正确！";
            }
            else if (!this.isNotifyUrlOk())
            {
                str = "error:1043 下行异步通知地址（callbackurl）格式不正确！";
            }
            else if (!this.isReturnUrlOk())
            {
                str = "error:1044 下行同步通知地址（hrefbackurl）格式不正确！";
            }
            else if (!this.isClientIpOk())
            {
                str = "error:1045 支付用户IP（payerIp）格式不正确！";
            }
            if (!string.IsNullOrEmpty(str))
            {
                viviapi.gateway.WebUtility.ShowErrorMsg(str);
                return;
            }
            UserInfo userInfo = null;
            decimal result = 0M;
            int uid = int.Parse(this.userid);
            if (!this.CheckUrlReferrer(uid))
            {
                string host = string.Empty;
                if (base.Request.UrlReferrer != null)
                {
                    host = base.Request.UrlReferrer.Host;
                }
                str = string.Format("error:1070 来路地址不合法！{0}", host);
            }
            else if (!decimal.TryParse(this.money, out result))
            {
                str = "error:1060 订单金额（value）有误！";
            }
            else if (result < this.MinTranAMT)
            {
                str = "error:1061 订单金额（value）小于最小允许交易额！";
            }
            else if (result > this.MaxChargeAMT)
            {
                str = string.Format("error:1062 订单金额（value）{0:f2}大于最大允许交易额{1:f2}！", result, this.MaxChargeAMT);
            }
            else
            {
                bool checkUserOrderNo = RuntimeSetting.CheckUserOrderNo;
                int num3 = Dal.BankOrder_DataCheck(uid, checkUserOrderNo, this.orderid, out userInfo);
                switch (num3)
                {
                    case 1:
                        str = "error:1064 商户编号不存在";
                        goto Label_042C;

                    case 2:
                        str = "error:1065 商户状态不正常";
                        goto Label_042C;

                    case 3:
                        str = "error:1069 商户订单号重复";
                        break;
                }
                if (num3 == 3)
                {
                    str = "error:1069 商户订单号重复";
                }
                else if (!viviapi.gateway.WebUtility.BankMD5Check(this.userid, this.bankid, this.money, this.orderid, this.notifyurl, userInfo.APIKey, this.sign))
                {
                    str = "error:1066 签名错误!";
                }
            }
        Label_042C:
            if (!string.IsNullOrEmpty(str))
            {
                viviapi.gateway.WebUtility.ShowErrorMsg(str);
            }
            else
            {
                int typeId = 0;
                int suppid = 0;
                ChannelInfo info2 = viviapi.BLL.Channel.Channel.GetModel(this.bankid, uid, true);
                if (info2 == null)
                {
                    str = "error:1067:银行编号不存在!";
                }
                else if (info2.isOpen.Value != 1)
                {
                    str = "error:1068:通道维护中!";
                }
                if (!string.IsNullOrEmpty(str))
                {
                    if (SysConfig.debuglog && (userInfo.isdebug == 1))
                    {
                        debuginfo model = new debuginfo();
                        model.addtime = new DateTime?(DateTime.Now);
                        model.bugtype = debugtypeenum.网银订单;
                        model.detail = string.Empty;
                        model.errorcode = str;
                        model.errorinfo = str;
                        model.userid = new int?(userInfo.ID);
                        if (base.Request.RawUrl != null)
                        {
                            model.url = base.Request.RawUrl.ToString();
                        }
                        else
                        {
                            model.url = string.Empty;
                        }
                        debuglog.Insert(model);
                    }
                    viviapi.gateway.WebUtility.ShowErrorMsg(str);
                }
                else
                {
                    typeId = info2.typeId;
                    suppid = info2.supplier.Value;
                    OrderBank bank = new OrderBank();
                    OrderBankInfo o = new OrderBankInfo();
                    o.orderid = bank.GenerateUniqueOrderId(typeId);
                    o.addtime = DateTime.Now;
                    o.attach = this.attach;
                    o.notifycontext = string.Empty;
                    o.notifycount = 0;
                    o.notifystat = 0;
                    o.notifyurl = this.notifyurl;
                    o.clientip = ServerVariables.TrueIP;
                    o.completetime = new DateTime?(DateTime.Now);
                    o.returnurl = this.returnurl;
                    o.ordertype = 1;
                    o.typeId = typeId;
                    o.paymodeId = this.bankid;
                    o.supplierId = suppid;
                    o.supplierOrder = string.Empty;
                    o.userid = uid;
                    o.userorder = this.orderid;
                    o.refervalue = result;
                    if (base.Request.UrlReferrer != null)
                    {
                        o.referUrl = base.Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        o.referUrl = string.Empty;
                    }
                    o.server = new int?(RuntimeSetting.ServerId);
                    o.manageId = userInfo.manageId;
                    if (!o.manageId.HasValue || (o.manageId.Value <= 0))
                    {
                        if (this.agentId > 0)
                        {
                            if (UserFactory.chkAgent(this.agentId))
                            {
                                o.agentId = this.agentId;
                            }
                        }
                        else
                        {
                            o.agentId = UserFactory.GetPromID(uid);
                        }
                    }
                    o.version = this.version;
                    WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
                    bank.Insert(o);
                    SellFactory.OnlineBankPay(suppid, o.orderid, o.refervalue, o.paymodeId);
                }
            }
        }

        public int agentId
        {
            get
            {
                int result = 0;
                try
                {
                    string parmValue = this.GetParmValue("agent");
                    if (!string.IsNullOrEmpty(parmValue))
                    {
                        int.TryParse(parmValue, out result);
                        return result;
                    }
                    parmValue = this.GetParmValue("hashcode");
                    if (!string.IsNullOrEmpty(parmValue))
                    {
                        result = Convert.ToInt32(parmValue, 0x10);
                    }
                }
                catch
                {
                }
                return result;
            }
        }

        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("attach", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string bankid
        {
            get
            {
                return this.GetParmValue("type");
            }
        }

        public string clientIp
        {
            get
            {
                return this.GetParmValue("payerIp");
            }
        }

        public string money
        {
            get
            {
                return this.GetParmValue("value");
            }
        }

        public string notifyurl
        {
            get
            {
                return this.GetParmValue("callbackurl");
            }
        }

        public string orderid
        {
            get
            {
                return this.GetParmValue("orderid");
            }
        }

        public string returnurl
        {
            get
            {
                return this.GetParmValue("hrefbackurl");
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("sign");
            }
        }

        public string userid
        {
            get
            {
                return this.GetParmValue("parter");
            }
        }

        public string version
        {
            get
            {
                return SystemApiHelper.vbmyapi20;
            }
        }
    }
}

