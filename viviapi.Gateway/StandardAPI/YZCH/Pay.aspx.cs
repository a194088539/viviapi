namespace viviapi.gateway.StandardAPI.YZCH
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
    using viviapi.gateway;
    using viviapi.Model.Channel;
    using viviapi.Model.Order;
    using viviapi.Model.Sys;
    using viviapi.Model.User;
    using viviapi.SysConfig;
    using viviLib.Web;

    public class Pay : Page
    {
        public decimal MaxChargeAMT = TransactionSetting.MaxTranATM;
        public decimal MinTranAMT = TransactionSetting.MinTranATM;

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
            if ((this.notifyurl == null) || (this.notifyurl.Length == 0))
            {
                return false;
            }
            return viviLib.Text.Validate.IsUrl(this.notifyurl);
        }

        private bool isReturnUrlOk()
        {
            return (((this.returnurl == null) || (this.returnurl.Length == 0)) || viviLib.Text.Validate.IsUrl(this.returnurl));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(this.userid))
            {
                str = "error:1001 商户ID（userid）不能空！";
            }
            else if (string.IsNullOrEmpty(this.bankid))
            {
                str = "error:1002 银行编号（bankid）不能空！";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                str = "error:1003 订单金额（money）不能空！";
            }
            else if (string.IsNullOrEmpty(this.orderid))
            {
                str = "error:1004 商户订单号（orderid）不能空！";
            }
            else if (string.IsNullOrEmpty(this.notifyurl))
            {
                str = "error:1005 商户接收后台返回储值结果的地址（url）不能空！";
            }
            else if (string.IsNullOrEmpty(this.sign))
            {
                str = "error:1006 签名数据（sign）不能空！";
            }
            else if (this.userid.Length > 10)
            {
                str = "error:1020 商户ID（userid）长度超过5位！";
            }
            else if (this.bankid.Length > 5)
            {
                str = "error:1021 银行编号（bankid）长度超过4位！";
            }
            else if (this.orderid.Length > 30)
            {
                str = "error:1022 商户订单号（orderid）长度超过30位！";
            }
            else if (this.money.Length > 8)
            {
                str = "error:1023 订单金额（money）长度超过最长限制！";
            }
            else if (this.notifyurl.Length > 0xff)
            {
                str = "error:1024 商户接收后台返回储值结果的地址（url）长度超过255位！";
            }
            else if (this.returnurl.Length > 0xff)
            {
                str = "error:1025 下行同步通知地址（aurl）长度超过255位！";
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
                str = "error:1040 商户ID（userid）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.bankid))
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
            int num2 = int.Parse(this.userid);
            if (!decimal.TryParse(this.money, out result))
            {
                str = "error:1060 订单金额（money）有误！";
            }
            else if (result < this.MinTranAMT)
            {
                str = "error:1061 订单金额（money）小于最小允许交易额！";
            }
            else if (result > this.MaxChargeAMT)
            {
                str = string.Format("error:1062 订单金额（money）{0:f2}大于最大允许交易额{1:f2}！", result, this.MaxChargeAMT);
            }
            else
            {
                bool checkUserOrderNo = RuntimeSetting.CheckUserOrderNo;
                int num3 = Dal.BankOrder_DataCheck(num2, checkUserOrderNo, this.orderid, out userInfo);
                switch (num3)
                {
                    case 1:
                        str = "error:1064 商户编号不存在";
                        goto Label_03BC;

                    case 2:
                        str = "error:1065 商户状态不正常";
                        goto Label_03BC;

                    case 3:
                        str = "error:1069 商户订单号重复";
                        break;
                }
                if (num3 == 3)
                {
                    str = "error:1069 商户订单号重复";
                }
                else if (!SystemApiHelper.BankReceiveVerify(this.version, this.sign, new object[] { num2, this.orderid.Trim(), this.bankid.Trim(), userInfo.APIKey }))
                {
                    str = "error:1066 签名错误!";
                }
            }
        Label_03BC:
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
                    o.paymodeId = this.sysBankcode;
                    o.supplierId = suppid;
                    o.supplierOrder = string.Empty;
                    o.userid = num2;
                    o.userorder = this.orderid;
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

        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("ext", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string bankid
        {
            get
            {
                return this.GetParmValue("bankid");
            }
        }

        public string money
        {
            get
            {
                return this.GetParmValue("money");
            }
        }

        public string notifyurl
        {
            get
            {
                return this.GetParmValue("url");
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
                return this.GetParmValue("aurl");
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("sign");
            }
        }

        public string sysBankcode
        {
            get
            {
                return SystemApiHelper.ConverBankCode(this.version, this.bankid);
            }
        }

        public string userid
        {
            get
            {
                return this.GetParmValue("userid");
            }
        }

        public string version
        {
            get
            {
                return SystemApiHelper.v7010;
            }
        }
    }
}

