namespace viviapi.gateway.StandardAPI.HuaQi
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.BLL.Sys;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.ETAPI;
    using viviapi.gateway;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.Model.Order;
    using viviapi.Model.Sys;
    using viviapi.Model.User;
    using viviapi.SysConfig;
    using viviLib.Web;

    public class gateway : Page
    {
        public UserInfo _userInfo = null;
        private string[] cardChannels = new string[] { "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
        public decimal MaxChargeAMT = TransactionSetting.MaxTranATM;
        public decimal MinTranAMT = TransactionSetting.MinTranATM;

        private void ChargeBank()
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(this.parter))
            {
                str = "error:1001 商户ID（P_UserId）不能空！";
            }
            else if (string.IsNullOrEmpty(this.orderid))
            {
                str = "error:1004 商户订单号（P_OrderId）不能空！";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                str = "error:1003 充值金额（P_FaceValue）不能空！";
            }
            else if (string.IsNullOrEmpty(this.type))
            {
                str = "error:1002 充值类型（P_ChannelId）不能空！";
            }
            else if (string.IsNullOrEmpty(this.P_Price))
            {
                str = "error:1007 产品价格（P_Price）不能空！";
            }
            else if (string.IsNullOrEmpty(this.P_Quantity))
            {
                str = "error:1008 产品数量（P_Quantity）不能空！";
            }
            else if (string.IsNullOrEmpty(this.notifyurl))
            {
                str = "error:1005 充值状态通知地址（P_Result_URL）不能空！";
            }
            else if (string.IsNullOrEmpty(this.sign))
            {
                str = "error:1006 MD5签名（sign）不能空！";
            }
            else if (this.parter.Length > 10)
            {
                str = "error:1020 商户ID（P_UserId）长度超过10位！";
            }
            else if (this.orderid.Length > 0x20)
            {
                str = "error:1022 商户订单号（P_OrderId）长度超过32位！";
            }
            else if (this.money.Length > 8)
            {
                str = "error:1023 订单金额（P_FaceValue）长度超过最长限制！";
            }
            else if (this.notifyurl.Length > 0xff)
            {
                str = "error:1024 充值状态通知地址（P_Result_URL）长度超过255位！";
            }
            else if (this.returnurl.Length > 0xff)
            {
                str = "error:1025 充值后网页跳转地址（P_Notify_URL）长度超过255位！";
            }
            else if (this.attach.Length > 0xff)
            {
                str = "error:1027 用户附加信息（P_Notic）长度超过255位！";
            }
            else if (this.P_Description.Length > 0xff)
            {
                str = "error:1029 产品描述（P_Description）长度超过255位！";
            }
            else if (this.P_Subject.Length > 50)
            {
                str = "error:1029 产品描述（P_Subject）长度超过50位！";
            }
            else if (this.sign.Length != 0x20)
            {
                str = "error:1028 签名认证串（sign）长度不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.parter))
            {
                str = "error:1040 商户ID（P_UserId）格式不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.type))
            {
                str = "error:1041 充值类型（P_ChannelId）格式不正确！";
            }
            else if (!this.checkOrderMoney())
            {
                str = "error:1042 订单金额（P_FaceValue）格式不正确！";
            }
            else if (!this.checkPrice())
            {
                str = "error:1045 产品价格（P_Price）格式不正确！";
            }
            else if (!this.checkPQuantity())
            {
                str = "error:1046 产品数量（P_Quantity）格式不正确！";
            }
            else if (!this.isNotifyUrlOk())
            {
                str = "error:1043 充值状态通知地址（P_Result_URL）格式不正确！";
            }
            else if (!this.isReturnUrlOk())
            {
                str = "error:1044 充值后网页跳转地址（P_Notify_URL）格式不正确！";
            }
            else if (this.sysChannelId <= 0)
            {
                str = "error:1067 不存在此支付通道（P_ChannelId）！";
            }
            else if (this.tranAmt <= 0M)
            {
                str = "error:1060 订单金额（P_FaceValue）有误！";
            }
            else if (this.tranAmt < this.MinTranAMT)
            {
                str = "error:1061 订单金额（P_FaceValue）小于最小允许交易额！";
            }
            else if (this.tranAmt > this.MaxChargeAMT)
            {
                str = string.Format("error:1062 订单金额（P_FaceValue）{0:f2}大于最大允许交易额{1:f2}！", this.tranAmt, this.MaxChargeAMT);
            }
            else if (this.userInfo == null)
            {
                str = "error:1064 商户（P_UserId）不存在";
            }
            else if (this.userInfo.Status != 2)
            {
                str = "error:1065 商户（P_UserId）状态不正常";
            }
            else if (!SystemApiHelper.BankReceiveVerify(this.version, this.sign, new object[] { this.parter, this.orderid, this.cardno, this.cardpass, this.money, this.type, this.userInfo.APIKey }))
            {
                str = "error:1066 签名认证串（P_PostKey）错误!";
            }
            if (string.IsNullOrEmpty(str))
            {
                ChannelTypeInfo cacheModel = ChannelType.GetCacheModel(this.sysChannelId);
                if (cacheModel == null)
                {
                    str = "error:1068:不存在此支付通道（P_ChannelId）!";
                }
                else if (cacheModel.isOpen == OpenEnum.Close)
                {
                    str = "error:1069:通道（P_ChannelId）维护中!";
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                if (SysConfig.debuglog && (this.userInfo.isdebug == 1))
                {
                    debuginfo model = new debuginfo();
                    model.addtime = new DateTime?(DateTime.Now);
                    model.bugtype = debugtypeenum.网银订单;
                    model.detail = string.Empty;
                    model.errorcode = str;
                    model.errorinfo = str;
                    model.userid = new int?(this.userInfo.ID);
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
                OrderBank bank = new OrderBank();
                OrderBankInfo o = new OrderBankInfo();
                o.orderid = bank.GenerateUniqueOrderId(this.sysChannelId);
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
                o.typeId = this.sysChannelId;
                o.paymodeId = string.Empty;
                o.supplierId = 0;
                o.supplierOrder = string.Empty;
                o.userid = this.userid;
                o.userorder = this.orderid;
                o.refervalue = this.tranAmt;
                if (base.Request.UrlReferrer != null)
                {
                    o.referUrl = base.Request.UrlReferrer.ToString();
                }
                else
                {
                    o.referUrl = string.Empty;
                }
                o.server = new int?(RuntimeSetting.ServerId);
                o.manageId = this.userInfo.manageId;
                o.version = this.version;
                o.cus_subject = this.P_Subject;
                o.cus_price = this.P_Price;
                o.cus_quantity = this.P_Quantity;
                o.cus_description = this.P_Description;
                o.cus_field2 = this.type;
                WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
                bank.Insert(o);
                string url = "BankSelect.aspx?sysorderid=" + o.orderid;
                base.Response.Redirect(url, true);
            }
        }

        private void ChargeCard()
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(this.parter))
            {
                str = "101";
            }
            else if (string.IsNullOrEmpty(this.orderid))
            {
                str = "118";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                str = "120";
            }
            else if (string.IsNullOrEmpty(this.type))
            {
                str = "107";
            }
            else if (string.IsNullOrEmpty(this.P_Price))
            {
                str = "123";
            }
            else if (this.orderid.Length > 0x20)
            {
                str = "119";
            }
            else if (this.money.Length > 8)
            {
                str = "120";
            }
            else if (this.notifyurl.Length > 0xff)
            {
                str = "126";
            }
            else if (this.returnurl.Length > 0xff)
            {
                str = "125";
            }
            else if (this.attach.Length > 0xff)
            {
                str = "124";
            }
            else if (this.P_Description.Length > 0xff)
            {
                str = "122";
            }
            else if (this.P_Subject.Length > 50)
            {
                str = "121";
            }
            else if (this.sign.Length != 0x20)
            {
                str = "110";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.parter))
            {
                str = "109";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.type))
            {
                str = "112";
            }
            else if (!this.checkOrderMoney())
            {
                str = "120";
            }
            else if (this.sysChannelId <= 0)
            {
                str = "112";
            }
            else if (this.tranAmt <= 0M)
            {
                str = "120";
            }
            else if (this.userInfo == null)
            {
                str = "109";
            }
            else if (this.userInfo.Status != 2)
            {
                str = "109";
            }
            else if (!SystemApiHelper.BankReceiveVerify(this.version, this.sign, new object[] { this.parter, this.orderid, this.cardno, this.cardpass, this.money, this.type, this.userInfo.APIKey }))
            {
                str = "110";
            }
            int supplier = 0;
            if (string.IsNullOrEmpty(str))
            {
                ChannelTypeInfo cacheModel = ChannelType.GetCacheModel(this.sysChannelId);
                if (cacheModel == null)
                {
                    str = "112";
                }
                else if (cacheModel.isOpen == OpenEnum.Close)
                {
                    str = "112";
                }
                else
                {
                    supplier = cacheModel.supplier;
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                if (SysConfig.debuglog && (this.userInfo.isdebug == 1))
                {
                    debuginfo model = new debuginfo();
                    model.addtime = new DateTime?(DateTime.Now);
                    model.bugtype = debugtypeenum.网银订单;
                    model.detail = string.Empty;
                    model.errorcode = str;
                    model.errorinfo = str;
                    model.userid = new int?(this.userInfo.ID);
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
                OrderCard card = new OrderCard();
                string str2 = string.Empty;
                string str3 = string.Empty;
                string str4 = card.GenerateUniqueOrderId(this.sysChannelId);
                string str5 = this.Sell(supplier, str4, 0, this.cardno, this.cardpass, this.sysChannelId, this.money, out str2, out str3);
                this.InitOrder(str4, (str5 == "0") ? 1 : 4, string.Empty, string.Empty, supplier, string.Empty);
                if (str5 == "0")
                {
                    str = "0";
                }
                else
                {
                    str = "117";
                }
                base.Response.Write("errCode=" + str);
                base.Response.End();
            }
        }

        private bool checkOrderMoney()
        {
            decimal num = 0M;
            try
            {
                num = Convert.ToDecimal(this.money);
            }
            catch
            {
            }
            return (num > 0M);
        }

        private bool checkPQuantity()
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(this.P_Quantity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool checkPrice()
        {
            decimal num = 0M;
            try
            {
                num = Convert.ToDecimal(this.P_Price);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetCardType(int paytype)
        {
            int num = paytype;
            if (paytype == 0x67)
            {
                return 13;
            }
            if (paytype == 0x68)
            {
                return 2;
            }
            if (paytype == 0x69)
            {
                return 7;
            }
            if (paytype == 0x6a)
            {
                return 3;
            }
            if (paytype == 0x6b)
            {
                return 1;
            }
            if (paytype == 0x6c)
            {
                return 14;
            }
            if (paytype == 0x6d)
            {
                return 8;
            }
            if (paytype == 110)
            {
                return 9;
            }
            if (paytype == 0x6f)
            {
                return 5;
            }
            if (paytype == 0x70)
            {
                return 6;
            }
            if (paytype == 0x71)
            {
                return 12;
            }
            if (paytype == 0x76)
            {
                num = 0x15;
            }
            return num;
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

        private void InitOrder(string _orderid, int _status, string _opstate, string _msg, int _suppId, string _returncode)
        {
            string str = this.GetCardType(this.sysChannelId).ToString("0000");
            if (!string.IsNullOrEmpty(_returncode) && _returncode.EndsWith("|"))
            {
                _returncode = _returncode.Substring(0, _returncode.Length - 1);
            }
            OrderCardInfo o = new OrderCardInfo();
            o.ordertype = 1;
            o.orderid = _orderid;
            o.userid = this.userid;
            o.userorder = this.orderid;
            o.typeId = this.sysChannelId;
            o.cardType = SystemApiHelper.CodeMapping(this.sysChannelId);
            o.cardNo = this.cardno;
            o.cardPwd = this.cardpass;
            o.paymodeId = str;
            o.refervalue = Convert.ToDecimal(this.tranAmt);
            o.attach = this.attach;
            if (base.Request.UrlReferrer != null)
            {
                o.referUrl = base.Request.UrlReferrer.ToString();
            }
            else
            {
                o.referUrl = string.Empty;
            }
            o.clientip = ServerVariables.TrueIP;
            o.addtime = DateTime.Now;
            o.completetime = new DateTime?(DateTime.Now);
            o.notifycontext = string.Empty;
            o.notifycount = 0;
            o.notifystat = 0;
            o.notifyurl = this.notifyurl;
            o.payRate = 0M;
            o.supplierId = _suppId;
            o.supplierOrder = string.Empty;
            o.server = new int?(RuntimeSetting.ServerId);
            o.cardnum = 1;
            o.resultcode = _returncode;
            o.ismulticard = 0;
            o.status = _status;
            o.ovalue = string.Empty;
            o.opstate = _opstate;
            o.msg = _msg;
            o.Desc = string.Empty;
            if (base.Request.UrlReferrer != null)
            {
                o.referUrl = base.Request.UrlReferrer.ToString();
            }
            else
            {
                o.referUrl = string.Empty;
            }
            o.server = new int?(RuntimeSetting.ServerId);
            o.manageId = this.userInfo.manageId;
            o.version = this.version;
            o.cus_subject = this.P_Subject;
            o.cus_price = this.P_Price;
            o.cus_quantity = this.P_Quantity;
            o.cus_description = this.P_Description;
            o.cus_field2 = this.type;
            o.manageId = this.userInfo.manageId;
            if (!(o.manageId.HasValue && (o.manageId.Value > 0)))
            {
                o.agentId = UserFactory.GetPromID(this.userid);
            }
            WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
            new OrderCard().Insert(o);
        }

        public bool isChargeBank()
        {
            if (string.IsNullOrEmpty(this.type))
            {
                return false;
            }
            return (((this.type == "1") || (this.type == "2")) || (this.type == "3"));
        }

        public bool isChargeCard()
        {
            if (!string.IsNullOrEmpty(this.type))
            {
                foreach (string str in this.cardChannels)
                {
                    if (this.type == str)
                    {
                        return true;
                    }
                }
            }
            return false;
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
            if (this.isChargeBank())
            {
                this.ChargeBank();
            }
            else if (this.isChargeCard())
            {
                this.ChargeCard();
            }
            else
            {
                base.Response.Write("充值类型 P_ChannelId 不存在");
            }
        }

        private string Sell(int _suppId, string _sysorderid, int _serial, string _cardno, string _cardpwd, int _typeid, string _cardvalue, out string _supporderid, out string _errmsg)
        {
            _supporderid = string.Empty;
            _errmsg = string.Empty;
            string supperrorcode = string.Empty;
            decimal result = 0M;
            decimal.TryParse(_cardvalue, out result);
            SupplierCode supp = (SupplierCode)_suppId;
            return SellFactory.SellCard(supp, _sysorderid, _typeid, _cardno, _cardpwd, string.Empty, Convert.ToInt32(decimal.Round(result, 0)), out _supporderid, out supperrorcode, out _errmsg);
        }

        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Notic", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string cardno
        {
            get
            {
                return this.GetParmValue("P_CardId");
            }
        }

        public string cardpass
        {
            get
            {
                return this.GetParmValue("P_CardPass");
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
                return this.GetParmValue("P_FaceValue");
            }
        }

        public string notifyurl
        {
            get
            {
                return this.GetParmValue("P_Result_URL");
            }
        }

        public string orderid
        {
            get
            {
                return this.GetParmValue("P_OrderId");
            }
        }

        public string P_Description
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Description", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string P_Price
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Price", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string P_Quantity
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Quantity", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string P_Subject
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("P_Subject", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string parter
        {
            get
            {
                return this.GetParmValue("P_UserId");
            }
        }

        public string returnurl
        {
            get
            {
                return this.GetParmValue("P_Notify_URL");
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("P_PostKey");
            }
        }

        public int sysChannelId
        {
            get
            {
                return SystemApiHelper.ConvertChannelCode(this.version, this.type);
            }
        }

        public decimal tranAmt
        {
            get
            {
                decimal num = 0M;
                try
                {
                    num = Convert.ToDecimal(this.money);
                }
                catch
                {
                }
                return num;
            }
        }

        public string type
        {
            get
            {
                return this.GetParmValue("P_ChannelId");
            }
        }

        public int userid
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrEmpty(this.parter) && int.TryParse(this.parter, out result))
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
                return SystemApiHelper.vhq10;
            }
        }
    }
}

