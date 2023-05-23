namespace viviapi.Gateway
{
    using System;
    using System.Text;
    using System.Web;
    using viviapi.BLL;
    using viviapi.BLL.Api;
    using viviapi.BLL.Channel;
    using viviapi.BLL.Order;
    using viviapi.BLL.Sys;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.Model.Order;
    using viviapi.Model.Sys;
    using viviapi.Model.User;
    using viviapi.SysConfig;
    using viviLib.Text;
    using viviLib.Web;

    public class cardreceive : IHttpHandler
    {
        public UserInfo _userInfo = null;
        protected OrderCard OrderBLL = new OrderCard();

        private int CheckMoney()
        {
            decimal _result = 0M;
            if (!decimal.TryParse(this.money, out _result))
            {
                return 0;
            }
            if (_result <= 0M)
            {
                return 0;
            }
            return 3;
        }

        private string ConvertErrorCode(SupplierCode supp, string errcode)
        {
            string syscode = "99";
            if (supp == SupplierCode.OfCard)
            {
                if (errcode == "2001")
                {
                    return "1";
                }
                if (errcode == "2002")
                {
                    return "2";
                }
                if (errcode == "2005")
                {
                    return "5";
                }
                if (errcode == "2009")
                {
                    return "19";
                }
                if (errcode == "2010")
                {
                    return "10";
                }
                if (errcode == "2012")
                {
                    return "12";
                }
                if (errcode == "2016")
                {
                    syscode = "16";
                }
                return syscode;
            }
            if (supp == SupplierCode.HuiYuan)
            {
                syscode = "99";
                if (errcode == "0")
                {
                    return "1";
                }
                if (errcode == "9")
                {
                    return "16";
                }
                if (errcode == "10")
                {
                    return "18";
                }
                if (errcode == "98")
                {
                    syscode = "19";
                }
            }
            return syscode;
        }

        private string ConvertParm(string errcode)
        {
            switch (errcode)
            {
                case "1049":
                case "1063":
                    return "2";

                case "1062":
                    return "3";

                case "1069":
                    return "4";

                case "1070":
                    return "5";

                case "1071":
                    return "6";

                case "1060":
                case "1061":
                    return "8";

                case "1064":
                    return "9";

                case "1082":
                case "1083":
                case "1084":
                    return "10";
            }
            return "7";
        }

        private string GetChannelInfo(int _value, out int _supplierId, out decimal _supprate)
        {
            _supplierId = 0;
            _supprate = 0M;
            string _errorcode = string.Empty;
            ChannelInfo chanelInfo = viviapi.BLL.Channel.Channel.GetModel(this.channelTypeId.ToString("0000") + _value.ToString(), this.userid, true);
            if (chanelInfo == null)
            {
                return "1063";
            }
            if (chanelInfo.isOpen != 1)
            {
                return "1064";
            }
            if (chanelInfo.supplier.Value <= 0)
            {
                return "1065";
            }
            _supplierId = chanelInfo.supplier.Value;
            _supprate = chanelInfo.supprate;
            return _errorcode;
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string infoMsg = ErrorCode;
            switch (ErrorCode)
            {
                case "1001":
                    return "卡类型（type）不能空！";

                case "1002":
                    return "商户ID（parter）不能空！";

                case "1003":
                    return "卡号（cardno）不能空！";

                case "1004":
                    return "密码（cardpwd）不能空！";

                case "1005":
                    return "订单金额（value）不能空！";

                case "1006":
                    return "卡能使用的地理范围（restrict）不能空！";

                case "1007":
                    return "商户订单号（orderid）不能空！";

                case "1008":
                    return "异步通知地址（callbackurl）不能空！";

                case "1009":
                    return "MD5签名（sign）不能空！";

                case "1010":
                    return "总金额（totalvalue）不能空！";

                case "1020":
                    return "卡类型（type）长度超过最长限制！";

                case "1021":
                    return "商户ID（parter）长度超过最长限制！";

                case "1022":
                    return "卡号（cardno）长度超过最长限制！";

                case "1023":
                    return "密码（cardpwd）长度超过最长限制！";

                case "1024":
                    return "订单金额（value）长度超过最长限制！";

                case "1025":
                    return "卡能使用的地理范围（restrict）长度超过最长限制！";

                case "1026":
                    return "商户订单号（orderid）长度超过最长限制！";

                case "1027":
                    return "异步通知地址（callbackurl）长度超过最长限制！";

                case "1028":
                    return "备注(attach)消息长度超过最长限制！";

                case "1029":
                    return "签名（sign）长度不正确！";

                case "1030":
                    return "卡号列表（cardno）长度与密码（cardpwd）列表长度长度不一致！";

                case "1031":
                    return "卡号列表（cardno）长度与金额（value）列表长度长度不一致！";

                case "1032":
                    return "卡号列表（cardno）长度与限制（restrict）列表长度长度不一致！";

                case "1041":
                    return "卡类型（type）格式不正确！";

                case "1042":
                    return "商户ID（parter）格式不正确！";

                case "1043":
                    return "卡号（cardno）格式不正确！";

                case "1044":
                    return "卡密（cardpwd）格式不正确！";

                case "1045":
                    return "卡面值（value）格式不正确！";

                case "1046":
                    return "异步通知地址（callbackurl）格式不正确！";

                case "1047":
                    return "卡面值（totalvalue）格式不正确！";

                case "1048":
                    return "单卡总金额(money)与总金额（totalvalue）价值不一致！";

                case "1049":
                    return "系统目前不支持此卡类型（type）！";

                case "1060":
                    return "非法商户ID（parter）！";

                case "1061":
                    return "商户（parter）状态不正常!";

                case "1062":
                    return "签名错误！";

                case "1063":
                    return "不支持此卡类别!";

                case "1064":
                    return "通道维护中!";

                case "1068":
                    return "商户订单号重复!";

                case "1069":
                    return "合作伙伴提交了相同的卡密信息时返回 ，并且该笔卡密信息龙宝还在处理中!";

                case "1070":
                    return "该卡密有被使用或者正在被他人使用的记录!";

                case "1071":
                    return "合作伙伴发送了相同的订单号!";

                case "1080":
                    return "卡参数有误! 请查看详细";

                case "1081":
                    return "系统故障";

                case "1082":
                    return "卡余额不足";

                case "1083":
                    return "卡内余额小于提交值";

                case "2001":
                    return "数据接收成功!";
            }
            return infoMsg;
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

        private void InitOrder(string _orderid, int _status, byte makeup, string _opstate, string _msg, int _suppId, string _returncode)
        {
            string _chanelno = this.channelTypeId.ToString("0000") + this.cardfacevalue;
            if (!string.IsNullOrEmpty(_returncode) && _returncode.EndsWith("|"))
            {
                _returncode = _returncode.Substring(0, _returncode.Length - 1);
            }
            OrderCardInfo order = new OrderCardInfo();
            order.ordertype = 1;
            order.orderid = _orderid;
            order.userid = this.userid;
            order.userorder = this.userorderid;
            order.typeId = this.sysChannelTypeId;
            order.cardType = this.channelTypeId;
            order.cardNo = this.cardno;
            order.cardPwd = this.cardpass;
            order.paymodeId = _chanelno;
            order.refervalue = this.cardfacevalue;
            order.faceValue = 0M;
            order.attach = this.attach;
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                order.referUrl = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            else
            {
                order.referUrl = string.Empty;
            }
            order.clientip = ServerVariables.TrueIP;
            order.addtime = DateTime.Now;
            order.completetime = new DateTime?(DateTime.Now);
            order.notifycontext = string.Empty;
            order.notifycount = 0;
            order.notifystat = 0;
            order.notifyurl = this.notifyurl;
            order.payRate = 0M;
            order.supplierId = _suppId;
            order.supplierOrder = string.Empty;
            order.server = new int?(RuntimeSetting.ServerId);
            order.manageId = this.userInfo.manageId;
            if (!order.manageId.HasValue || (order.manageId.Value <= 0))
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
                    order.agentId = UserFactory.GetPromID(this.userid);
                }
            }
            order.cardnum = 1;
            order.resultcode = _returncode;
            order.ismulticard = 0;
            order.status = _status;
            order.ovalue = string.Empty;
            order.opstate = _opstate;
            order.msg = _msg;
            order.userViewMsg = _msg;
            order.errtype = _returncode;
            order.Desc = string.Empty;
            order.version = this.version;
            order.withhold_type = 0;
            order.makeup = makeup;
            WebCache.GetCacheService().AddObject(order.orderid, order, TransactionSetting.ExpiresTime);
            this.OrderBLL.Insert(order);
        }

        private bool IsNotifyUrlOk()
        {
            if ((this.notifyurl == null) || (this.notifyurl.Length == 0))
            {
                return false;
            }
            bool isUrl = Validate.IsUrl(this.notifyurl);
            if (isUrl)
            {
                return (!this.notifyurl.Contains("?") && !this.notifyurl.Contains("&"));
            }
            return isUrl;
        }

        private bool IsValid(string _value)
        {
            return Validate.QuickValidate("^[0-9A-Za-z]{6,30}", _value);
        }

        public void ProcessRequest(HttpContext context)
        {
            byte method = 0;
            byte makeup = 0;
            string errorcode = string.Empty;
            string sysorderid = string.Empty;
            string supperrinfo = string.Empty;
            int supplierid = 0;
            decimal supplierrate = 0M;
            string opstate = string.Empty;
            StringBuilder _errorMsg = new StringBuilder();
            if (string.IsNullOrEmpty(this.cardtype))
            {
                errorcode = "1001";
            }
            else if (string.IsNullOrEmpty(this.parter))
            {
                errorcode = "1002";
            }
            else if (string.IsNullOrEmpty(this.cardno))
            {
                errorcode = "1003";
            }
            else if (string.IsNullOrEmpty(this.cardpass))
            {
                errorcode = "1004";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                errorcode = "1005";
            }
            else if (string.IsNullOrEmpty(this.restrict))
            {
                errorcode = "1006";
            }
            else if (string.IsNullOrEmpty(this.userorderid))
            {
                errorcode = "1007";
            }
            else if (string.IsNullOrEmpty(this.notifyurl))
            {
                errorcode = "1008";
            }
            else if (string.IsNullOrEmpty(this.sign))
            {
                errorcode = "1009";
            }
            else if (this.cardtype.Length > 2)
            {
                errorcode = "1020";
            }
            else if (this.parter.Length > 8)
            {
                errorcode = "1021";
            }
            else if (this.cardno.Length > 0x3e8)
            {
                errorcode = "1022";
            }
            else if (this.cardpass.Length > 0x3e8)
            {
                errorcode = "1023";
            }
            else if (this.money.Length > 500)
            {
                errorcode = "1024";
            }
            else if (this.restrict.Length > 200)
            {
                errorcode = "1025";
            }
            else if (this.userorderid.Length > 30)
            {
                errorcode = "1026";
            }
            else if (this.notifyurl.Length > 200)
            {
                errorcode = "1027";
            }
            else if (this.attach.Length > 500)
            {
                errorcode = "1028";
            }
            else if (this.sign.Length != 0x20)
            {
                errorcode = "1029";
            }
            else if (!Validate.IsNumeric(this.cardtype))
            {
                errorcode = "1041";
            }
            else if (this.sysChannelTypeId < 100)
            {
                errorcode = "1049";
            }
            else if (!Validate.IsNumeric(this.parter))
            {
                errorcode = "1042";
            }
            else
            {
                int checkMoneyResult = this.CheckMoney();
                switch (checkMoneyResult)
                {
                    case 0:
                        errorcode = "1045";
                        goto Label_0398;

                    case 1:
                        errorcode = "1047";
                        goto Label_0398;

                    case 2:
                        errorcode = "1048";
                        break;
                }
                if (checkMoneyResult == 2)
                {
                    errorcode = "1048";
                }
                else if (!this.IsNotifyUrlOk())
                {
                    errorcode = "1046";
                }
            }
        Label_0398:
            if (string.IsNullOrEmpty(errorcode))
            {
                if (this.userid <= 0)
                {
                    errorcode = "1021";
                }
                else
                {
                    string key = this.userInfo.APIKey;
                    if (this.userInfo.ID < 0)
                    {
                        errorcode = "1060";
                    }
                    else if (this.userInfo.Status != 2)
                    {
                        errorcode = "1061";
                    }
                    else if (!SellCard20.SignVerification(this.cardtype, this.parter, this.cardno, this.cardpass, this.money, this.userorderid, this.restrict, this.notifyurl, key, this.sign))
                    {
                        errorcode = "1062";
                    }
                }
            }
            if (string.IsNullOrEmpty(errorcode))
            {
                sysorderid = this.OrderBLL.GenerateUniqueOrderId(this.sysChannelTypeId);
                if (!this.IsValid(this.cardno))
                {
                    errorcode = "1043";
                }
                else if (!this.IsValid(this.cardpass))
                {
                    errorcode = "1043";
                }
                else if (this.cardfacevalue == 0)
                {
                    errorcode = "1045";
                }
                else
                {
                    errorcode = this.GetChannelInfo(this.cardfacevalue, out supplierid, out supplierrate);
                }
            }
            if (string.IsNullOrEmpty(errorcode))
            {
                CheckCardResult _result = Dal.CheckCardRepeat(this.userid, this.userorderid, this.sysChannelTypeId, this.cardno, this.cardpass);
                if (_result == null)
                {
                    errorcode = "1081";
                }
                else if (_result.isRepeat == 4)
                {
                    errorcode = "1069";
                }
                else if (_result.isRepeat == 5)
                {
                    errorcode = "1070";
                }
                else if (_result.isRepeat == 6)
                {
                    errorcode = "1071";
                }
                else if (_result.isRepeat == 1)
                {
                    method = 1;
                }
            }
            if (!string.IsNullOrEmpty(errorcode))
            {
                if (SysConfig.debuglog && (((this.userid > 0) || (this.userInfo != null)) || (this.userInfo.isdebug == 1)))
                {
                    debuginfo _debugInfo = new debuginfo();
                    _debugInfo.userid = new int?(this.userid);
                    _debugInfo.addtime = new DateTime?(DateTime.Now);
                    _debugInfo.bugtype = debugtypeenum.卡类订单;
                    _debugInfo.detail = string.Empty;
                    _debugInfo.errorcode = errorcode;
                    _debugInfo.errorinfo = this.GetErrorInfo(errorcode);
                    _debugInfo.userorder = this.userorderid;
                    if (context.Request.RawUrl != null)
                    {
                        _debugInfo.url = context.Request.RawUrl;
                    }
                    else
                    {
                        _debugInfo.url = string.Empty;
                    }
                    _debugInfo.detail = this.GetErrorInfo(errorcode);
                    debuglog.Insert(_debugInfo);
                }
                opstate = this.ConvertParm(errorcode);
            }
            else if (method > 0)
            {
                int status = 4;
                SupplierCode supp = SupplierCode.System;
                if (method == 1)
                {
                    if (supplierid > 0)
                    {
                        string supporderid;
                        string suppererrcode;
                        supp = (SupplierCode)supplierid;
                        if (SellFactory.SellCard(supp, sysorderid, this.sysChannelTypeId, this.cardno, this.cardpass, string.Empty, this.cardfacevalue, out supporderid, out suppererrcode, out supperrinfo) == "0")
                        {
                            status = 2;
                        }
                        opstate = SellCard20.ConvertSynchronousErrorCode(supp, suppererrcode);
                    }
                    else
                    {
                        opstate = "15";
                    }
                }
                this.InitOrder(sysorderid, status, makeup, opstate, supperrinfo, supplierid, supperrinfo);
            }
            else
            {
                opstate = "15";
            }
            string retcode = string.Format("opstate={0}", opstate);
            context.Response.ContentType = "text/plain";
            context.Response.Write(retcode);
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
                        return _agentId;
                    }
                    parms = this.GetParmValue("hashcode");
                    if (!string.IsNullOrEmpty(parms))
                    {
                        _agentId = Convert.ToInt32(parms, 0x10);
                    }
                }
                catch
                {
                }
                return _agentId;
            }
        }

        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("attach"), Encoding.GetEncoding("GB2312"));
            }
        }

        public int cardfacevalue
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this.money);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public string cardno
        {
            get
            {
                return this.GetParmValue("cardno");
            }
        }

        public string cardpass
        {
            get
            {
                return this.GetParmValue("cardpwd");
            }
        }

        public string cardtype
        {
            get
            {
                return this.GetParmValue("type");
            }
        }

        public int channelTypeId
        {
            get
            {
                int _typeid = 0;
                if (!string.IsNullOrEmpty(this.cardtype) && int.TryParse(this.cardtype, out _typeid))
                {
                    if (_typeid != 2)
                    {
                        return _typeid;
                    }
                    if (ChannelType.IsShengFuTong(this.cardno))
                    {
                        return 0x1c;
                    }
                }
                return _typeid;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
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

        public string parter
        {
            get
            {
                return this.GetParmValue("parter");
            }
        }

        public string restrict
        {
            get
            {
                return this.GetParmValue("restrict");
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("sign");
            }
        }

        public int sysChannelTypeId
        {
            get
            {
                if (this.channelTypeId > 0)
                {
                    return ChannelType.GetSysTypeId(this.channelTypeId, this.cardno);
                }
                return 0;
            }
        }

        public string totalvalue
        {
            get
            {
                return this.GetParmValue("totalvalue");
            }
        }

        public int userid
        {
            get
            {
                int _userid = 0;
                if (!string.IsNullOrEmpty(this.parter) && int.TryParse(this.parter, out _userid))
                {
                    return _userid;
                }
                return _userid;
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

        public string userorderid
        {
            get
            {
                return this.GetParmValue("orderid");
            }
        }

        public string version
        {
            get
            {
                return SystemApiHelper.vcmyapi20;
            }
        }
    }
}

