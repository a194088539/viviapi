namespace viviapi.gateway
{
    using System;
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

    public class ipayment : Page
    {
        public UserInfo _userInfo = null;
        public decimal MaxChargeAMT = TransactionSetting.MaxTranATM;
        public decimal MinTranAMT = TransactionSetting.MinTranATM;

        public static string ConverBankCode(string bankcode)
        {
            switch (bankcode)
            {
                case "00042":
                    return "970";

                case "00004":
                    return "967";

                case "00017":
                    return "964";

                case "00012":
                    return "965";

                case "00083":
                    return "963";

                case "00032":
                    return "977";

                case "00005":
                    return "981";

                case "00013":
                    return "980";

                case "00023":
                    return "974";

                case "00052":
                    return "985";

                case "00092":
                    return "962";

                case "00041":
                    return "982";

                case "00016":
                    return "972";

                case "00050":
                    return "989";

                case "00057":
                    return "986";

                case "00087":
                    return "978";

                case "00084":
                    return "975";

                case "0005":
                    return "971";

                case "984":
                    return "00011";

                case "00056":
                    return "990";

                case "00055":
                    return "979";

                case "00081":
                    return "983";

                case "00086":
                    return "968";
            }
            return "967";
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

        private bool isNotifyUrlOk()
        {
            if ((this.FailUrl == null) || (this.FailUrl.Length == 0))
            {
                return false;
            }
            return viviLib.Text.Validate.IsUrl(this.FailUrl);
        }

        private bool isReturnUrlOk()
        {
            return (((this.Merchanturl == null) || (this.Merchanturl.Length == 0)) || viviLib.Text.Validate.IsUrl(this.Merchanturl));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(this.Mer_code))
            {
                str = "error:1001 商户ID（Mer_code）不能空！";
            }
            else if (string.IsNullOrEmpty(this.Bankco))
            {
                str = "error:1002 银行编号（Bankco）不能空！";
            }
            else if (string.IsNullOrEmpty(this.Amount))
            {
                str = "error:1003 订单金额（Amount）不能空！";
            }
            else if (string.IsNullOrEmpty(this.Billno))
            {
                str = "error:1004 商户订单号（Billno）不能空！";
            }
            else if (string.IsNullOrEmpty(this.ServerUrl))
            {
                str = "error:1005 商户接收后台返回储值结果的地址（ServerUrl）不能空！";
            }
            else if (string.IsNullOrEmpty(this.SignMD5))
            {
                str = "error:1006 签名数据（sign）不能空！";
            }
            else if (string.IsNullOrEmpty(this.OrderEncodeType))
            {
                str = "error:1006 订单加密方式（OrderEncodeType）不能空！";
            }
            else if (string.IsNullOrEmpty(this.RetEncodeType))
            {
                str = "error:1006 交易返回接口加密方式（RetEncodeType）不能空！";
            }
            else if (string.IsNullOrEmpty(this.Currency_Type))
            {
                str = "error:1006 交易币种（Currency_Type）不能空！";
            }
            else if (string.IsNullOrEmpty(this.Gateway_Type))
            {
                str = "error:1006 交易类型（Gateway_Type）不能空！";
            }
            else if (this.Mer_code.Length > 10)
            {
                str = "error:1020 商户ID（Mer_code）长度超过5位！";
            }
            else if (this.Bankco.Length > 5)
            {
                str = "error:1021 银行编号（Bankco）长度超过4位！";
            }
            else if (this.Billno.Length > 30)
            {
                str = "error:1022 商户订单号（Billno）长度超过30位！";
            }
            else if (this.Amount.Length > 8)
            {
                str = "error:1023 订单金额（Amount）长度超过最长限制！";
            }
            else if (this.ServerUrl.Length > 0xff)
            {
                str = "error:1024 商户接收后台返回储值结果的地址（url）长度超过255位！";
            }
            else if (this.Merchanturl.Length > 0xff)
            {
                str = "error:1025 下行同步通知地址（aurl）长度超过255位！";
            }
            else if (this.Attach.Length > 0xff)
            {
                str = "error:1027 备注消息（attach）长度超过255位！";
            }
            else if (this.SignMD5.Length != 0x20)
            {
                str = "error:1028 签名（sign）长度不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.Mer_code))
            {
                str = "error:1040 商户ID（Mer_code）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.Bankco))
            {
                str = "error:1041 银行编号（bankid）格式不正确！";
            }
            else if (!this.isNotifyUrlOk())
            {
                str = "error:1043 商户接收后台返回储值结果的地址（url）格式不正确！";
            }
            else if (!this.isReturnUrlOk())
            {
                str = "error:1044 下行同步通知地址（aurl）格式不正确！";
            }
            if (!string.IsNullOrEmpty(str))
            {
                WebUtility.ShowErrorMsg(str);
                return;
            }
            UserInfo userInfo = null;
            decimal result = 0M;
            int num2 = int.Parse(this.Mer_code);
            if (!decimal.TryParse(this.Amount, out result))
            {
                str = "error:1060 订单金额（Amount）有误！";
            }
            else if (result < this.MinTranAMT)
            {
                str = "error:1061 订单金额（Amount）小于最小允许交易额！";
            }
            else if (result > this.MaxChargeAMT)
            {
                str = string.Format("error:1062 订单金额（Amount）{0:f2}大于最大允许交易额{1:f2}！", result, this.MaxChargeAMT);
            }
            else
            {
                bool checkUserOrderNo = RuntimeSetting.CheckUserOrderNo;
                int num3 = Dal.BankOrder_DataCheck(num2, checkUserOrderNo, this.Billno, out userInfo);
                userInfo = UserFactory.GetCacheUserBaseInfo(num2);
                switch (num3)
                {
                    case 1:
                        str = "error:1064 商户编号不存在";
                        goto Label_0426;

                    case 2:
                        str = "error:1065 商户状态不正常";
                        goto Label_0426;

                    case 3:
                        str = "error:1069 商户订单号重复";
                        break;
                }
                if (num3 == 3)
                {
                    str = "error:1069 商户订单号重复";
                }
                else if (!WebUtility.IPSBankMD5Check(this.Billno, this.Currency_Type, this.Amount, this.Date, this.OrderEncodeType, userInfo.APIKey, this.SignMD5))
                {
                    str = "error:1066 签名错误!";
                }
            }
        Label_0426:
            if (!string.IsNullOrEmpty(str))
            {
                WebUtility.ShowErrorMsg(str);
            }
            else
            {
                int typeId = 0;
                int suppid = 0;
                ChannelInfo info2 = viviapi.BLL.Channel.Channel.GetModel(this.sysBankcode, num2, true);
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
                        model.detail = "接口:" + SystemApiHelper.GetVersionName(this.version);
                        model.errorcode = str;
                        model.errorinfo = str;
                        model.userid = new int?(num2);
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
                    WebUtility.ShowErrorMsg(str);
                }
                else
                {
                    typeId = info2.typeId;
                    suppid = info2.supplier.Value;
                    OrderBank bank = new OrderBank();
                    OrderBankInfo o = new OrderBankInfo();
                    o.orderid = bank.GenerateUniqueOrderId(typeId);
                    o.addtime = DateTime.Now;
                    o.attach = this.Attach;
                    o.notifycontext = string.Empty;
                    o.notifycount = 0;
                    o.notifystat = 0;
                    o.notifyurl = this.ServerUrl;
                    o.clientip = ServerVariables.TrueIP;
                    o.completetime = new DateTime?(DateTime.Now);
                    o.returnurl = this.Merchanturl;
                    o.ordertype = 1;
                    o.typeId = typeId;
                    o.paymodeId = this.sysBankcode;
                    o.supplierId = suppid;
                    o.supplierOrder = string.Empty;
                    o.userid = num2;
                    o.userorder = this.Billno;
                    o.refervalue = result;
                    o.version = this.version;
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
                    if (!(o.manageId.HasValue && (o.manageId.Value > 0)))
                    {
                        o.agentId = UserFactory.GetPromID(num2);
                    }
                    WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
                    bank.Insert(o);
                    SellFactory.OnlineBankPay(suppid, o.orderid, o.refervalue, o.paymodeId);
                }
            }
        }

        public string Amount
        {
            get
            {
                return this.GetParmValue("Amount");
            }
        }

        public string Attach
        {
            get
            {
                return this.GetParmValue("Attach");
            }
        }

        public string Bankco
        {
            get
            {
                return this.GetParmValue("Bankco");
            }
        }

        public string Billno
        {
            get
            {
                return this.GetParmValue("Billno");
            }
        }

        public string Currency_Type
        {
            get
            {
                return this.GetParmValue("Currency_Type");
            }
        }

        public string Date
        {
            get
            {
                return this.GetParmValue("Date");
            }
        }

        public string DispAmount
        {
            get
            {
                return this.GetParmValue("DispAmount");
            }
        }

        public string DoCredit
        {
            get
            {
                return this.GetParmValue("DoCredit");
            }
        }

        public string ErrorUrl
        {
            get
            {
                return this.GetParmValue("ErrorUrl");
            }
        }

        public string FailUrl
        {
            get
            {
                return this.GetParmValue("FailUrl");
            }
        }

        public string Gateway_Type
        {
            get
            {
                return this.GetParmValue("Gateway_Type");
            }
        }

        public string Lang
        {
            get
            {
                return this.GetParmValue("Lang");
            }
        }

        public string Mer_code
        {
            get
            {
                return this.GetParmValue("Mer_code");
            }
        }

        public string Merchanturl
        {
            get
            {
                return this.GetParmValue("Merchanturl");
            }
        }

        public string OrderEncodeType
        {
            get
            {
                return this.GetParmValue("OrderEncodeType");
            }
        }

        public string RetEncodeType
        {
            get
            {
                return this.GetParmValue("RetEncodeType");
            }
        }

        public string RetType
        {
            get
            {
                return this.GetParmValue("RetType");
            }
        }

        public string ServerUrl
        {
            get
            {
                return this.GetParmValue("ServerUrl");
            }
        }

        public string SignMD5
        {
            get
            {
                return this.GetParmValue("SignMD5");
            }
        }

        public string sysBankcode
        {
            get
            {
                return ConverBankCode(this.Bankco);
            }
        }

        public int userid
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrEmpty(this.Mer_code) && int.TryParse(this.Mer_code, out result))
                {
                    return result;
                }
                return result;
            }
        }

        public UserInfo userInfo
        {
            get
            {
                if ((this.userid > 0) && (this._userInfo == null))
                {
                    this._userInfo = UserFactory.GetCacheUserBaseInfo(this.userid);
                }
                if (this._userInfo == null)
                {
                    this._userInfo = new UserInfo();
                }
                return this._userInfo;
            }
        }

        public string version
        {
            get
            {
                return SystemApiHelper.IPS20;
            }
        }
    }
}

