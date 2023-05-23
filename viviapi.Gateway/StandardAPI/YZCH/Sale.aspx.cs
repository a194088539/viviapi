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
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.Model.Order;
    using viviapi.Model.Sys;
    using viviapi.Model.User;
    using viviapi.SysConfig;
    using viviLib.Web;

    public class Sale : Page
    {
        public UserInfo _userInfo = null;
        private OrderCard OrderBLL = new OrderCard();

        private int checkMoney()
        {
            decimal result = 0M;
            if (!decimal.TryParse(this.money, out result))
            {
                return 0;
            }
            if (result <= 0M)
            {
                return 0;
            }
            return 3;
        }

        private string ConvertParm(string errcode)
        {
            switch (errcode)
            {
                case "":
                case "0":
                    return "1";

                case "1062":
                    return "6";

                case "1060":
                case "1002":
                case "1021":
                case "1042":
                    return "7";

                case "1003":
                case "1004":
                case "1005":
                case "1022":
                case "1023":
                case "1080":
                case "1082":
                case "1083":
                case "1084":
                    return "11";

                case "1064":
                case "1065":
                    return "12";
            }
            return "12";
        }

        private string GetChannelInfo(out int _supplierId, out decimal _supprate)
        {
            _supplierId = 0;
            _supprate = 0M;
            string str = string.Empty;
            ChannelInfo info = viviapi.BLL.Channel.Channel.GetModel(this.sysChannelNo, this.userid, true);
            if (info == null)
            {
                return "1063";
            }
            if (info.isOpen != 1)
            {
                return "1064";
            }
            if (info.supplier.Value <= 0)
            {
                return "1065";
            }
            _supplierId = info.supplier.Value;
            _supprate = info.supprate;
            return str;
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "1001":
                    return "卡类型（typeid）不能空！";

                case "1002":
                    return "商户ID（userid）不能空！";

                case "1003":
                    return "卡号（cardno）不能空！";

                case "1004":
                    return "密码（cardpwd）不能空！";

                case "1005":
                    return "订单金额(卡面值)（money）不能空！";

                case "1006":
                    return "产品代码（productid）不能空！";

                case "1007":
                    return "商户订单号（orderid）不能空！";

                case "1008":
                    return "商户接收售卡结果数据的地址（url）不能空！";

                case "1009":
                    return "MD5签名（sign）不能空！";

                case "1010":
                    return "总金额（totalvalue）不能空！";

                case "1020":
                    return "卡类型（typeid）长度超过最长限制！";

                case "1021":
                    return "商户ID（userid）长度超过最长限制！";

                case "1022":
                    return "卡号（cardno）长度超过最长限制！";

                case "1023":
                    return "密码（cardpwd）长度超过最长限制！";

                case "1024":
                    return "订单金额（money）长度超过最长限制！";

                case "1025":
                    return "产品ID（productid）长度超过最长限制！";

                case "1026":
                    return "商户订单号（orderid）长度超过最长限制！";

                case "1027":
                    return "商户接收售卡结果数据的地址（url）长度超过最长限制！";

                case "1028":
                    return "备注（ext）消息长度超过最长限制！";

                case "1029":
                    return "签名（sign）长度不正确！";

                case "1030":
                    return "卡号列表（cardno）长度与密码（cardpwd）列表长度长度不一致！";

                case "1031":
                    return "卡号列表（cardno）长度与金额（value）列表长度长度不一致！";

                case "1032":
                    return "卡号列表（cardno）长度与限制（restrict）列表长度长度不一致！";

                case "1041":
                    return "卡类型（typeid）格式不正确！";

                case "1042":
                    return "商户ID（userid）格式不正确！";

                case "1043":
                    return "卡号（cardno）格式不正确！";

                case "1044":
                    return "卡密（cardpwd）格式不正确！";

                case "1045":
                    return "卡面值（money）格式不正确！";

                case "1046":
                    return "商户接收售卡结果数据的地址（url）格式不正确！";

                case "1047":
                    return "产品ID（productid）格式不正确！";

                case "1048":
                    return "单卡总金额(money)与总金额（totalvalue）价值不一致！";

                case "1049":
                    return "系统目前不支持此卡类型（typeid）！";

                case "1060":
                    return "非法商户ID（userid）！";

                case "1061":
                    return "商户（userid）状态不正常!";

                case "1062":
                    return "签名错误！";

                case "1063":
                    return "不支持此卡类别!";

                case "1064":
                    return "通道维护中!";

                case "1068":
                    return "商户订单号重复!";

                case "1080":
                    return "发送到接口商消卡报错";

                case "2001":
                    return "数据接收成功!";
            }
            return str;
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

        private void InitOrder(string _orderid, int _status, byte makeup, string _opstate, string _msg, int _suppId, string _returncode)
        {
            string sysChannelNo = this.sysChannelNo;
            OrderCardInfo o = new OrderCardInfo();
            o.ordertype = 1;
            o.orderid = _orderid;
            o.userid = this.userid;
            o.userorder = this.userorderid;
            o.typeId = this.sysChannelTypeId;
            o.cardNo = this.cardno;
            o.cardType = SystemApiHelper.CodeMapping(this.sysChannelTypeId);
            o.cardPwd = this.cardpass;
            o.paymodeId = sysChannelNo;
            o.refervalue = this.CardFaceValue;
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
            o.userViewMsg = _msg;
            o.withhold_type = 0;
            o.Desc = string.Empty;
            o.version = this.version;
            o.makeup = makeup;
            o.manageId = this.userInfo.manageId;
            if (!(o.manageId.HasValue && (o.manageId.Value > 0)))
            {
                o.agentId = UserFactory.GetPromID(this.userid);
            }
            WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
            this.OrderBLL.Insert(o);
        }

        private bool isNotifyUrlOk()
        {
            if ((this.notifyurl == null) || (this.notifyurl.Length == 0))
            {
                return false;
            }
            return viviLib.Text.Validate.IsUrl(this.notifyurl);
        }

        private bool isvalid(string _value)
        {
            return viviLib.Text.Validate.QuickValidate("^[0-9A-Za-z]{6,30}", _value);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            byte num = 0;
            byte makeup = 0;
            string str = string.Empty;
            string orderid = string.Empty;
            int num3 = 0;
            if (string.IsNullOrEmpty(this.cardtype))
            {
                str = "1001";
            }
            else if (string.IsNullOrEmpty(this.parter))
            {
                str = "1002";
            }
            else if (string.IsNullOrEmpty(this.cardno))
            {
                str = "1003";
            }
            else if (string.IsNullOrEmpty(this.cardpass))
            {
                str = "1004";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                str = "1005";
            }
            else if (string.IsNullOrEmpty(this.productid))
            {
                str = "1006";
            }
            else if (string.IsNullOrEmpty(this.userorderid))
            {
                str = "1007";
            }
            else if (string.IsNullOrEmpty(this.notifyurl))
            {
                str = "1008";
            }
            else if (string.IsNullOrEmpty(this.sign))
            {
                str = "1009";
            }
            else if (this.cardtype.Length > 2)
            {
                str = "1020";
            }
            else if (this.parter.Length > 10)
            {
                str = "1021";
            }
            else if (this.cardno.Length > 30)
            {
                str = "1022";
            }
            else if (this.cardpass.Length > 30)
            {
                str = "1023";
            }
            else if (this.money.Length > 10)
            {
                str = "1024";
            }
            else if (this.productid.Length > 200)
            {
                str = "1025";
            }
            else if (this.userorderid.Length > 30)
            {
                str = "1026";
            }
            else if (this.notifyurl.Length > 200)
            {
                str = "1027";
            }
            else if (this.attach.Length > 500)
            {
                str = "1028";
            }
            else if (this.sign.Length != 0x20)
            {
                str = "1029";
            }
            else if (!viviLib.Text.Validate.IsLetterOrNumber(this.cardtype))
            {
                str = "1041";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.parter))
            {
                str = "1042";
            }
            else if (!this.isvalid(this.cardno))
            {
                str = "1043";
            }
            else if (!this.isvalid(this.cardpass))
            {
                str = "1043";
            }
            else if (!viviLib.Text.Validate.IsLetterOrNumber(this.productid))
            {
                str = "1044";
            }
            else if (this.sysChannelTypeId < 100)
            {
                str = "1047";
            }
            else if (this.checkMoney() == 0)
            {
                str = "1045";
            }
            else if (!this.isNotifyUrlOk())
            {
                str = "1046";
            }
            if (string.IsNullOrEmpty(str))
            {
                if (this.userid <= 0)
                {
                    str = "1021";
                }
                else
                {
                    string aPIKey = this.userInfo.APIKey;
                    if (this.userInfo.ID < 0)
                    {
                        str = "1060";
                    }
                    else if (this.userInfo.Status != 2)
                    {
                        str = "1061";
                    }
                    else if (!SystemApiHelper.CardReceiveVerify(this.version, this.sign, new object[] { this.userid, this.userorderid, this.cardtype, this.productid, this.cardno, this.cardpass, this.money, this.notifyurl, aPIKey }))
                    {
                        str = "1062";
                    }
                }
            }
            decimal num5 = 0M;
            if (string.IsNullOrEmpty(str))
            {
                orderid = this.OrderBLL.GenerateUniqueOrderId(this.sysChannelTypeId);
                num3 = 0;
                string channelInfo = this.GetChannelInfo(out num3, out num5);
                if (!string.IsNullOrEmpty(channelInfo))
                {
                    str = channelInfo;
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                CheckCardResult result = Dal.CheckCardRepeat(this.userid, this.userorderid, this.sysChannelTypeId, this.cardno, this.cardpass);
                if (result == null)
                {
                    str = "1081";
                }
                else if (result.isRepeat == 4)
                {
                    str = "1069";
                }
                else if (result.isRepeat == 5)
                {
                    str = "1070";
                }
                else if (result.isRepeat == 6)
                {
                    str = "1071";
                }
                else if (result.isRepeat == 1)
                {
                    num = 1;
                }
            }
            string errormsg = string.Empty;
            if (string.IsNullOrEmpty(str))
            {
                string supperrorcode = string.Empty;
                string supporderid = string.Empty;
                if (num > 0)
                {
                    int num6 = 4;
                    string str8 = string.Empty;
                    if (num3 > 0)
                    {
                        SupplierCode supp = (SupplierCode)num3;
                        str8 = SellFactory.SellCard(supp, orderid, this.sysChannelTypeId, this.cardno, this.cardpass, string.Empty, this.CardFaceValue, out supporderid, out supperrorcode, out errormsg);
                        if (str8 == "0")
                        {
                            str = "0";
                            num6 = 1;
                        }
                        else
                        {
                            num6 = 4;
                            str = "1080";
                        }
                    }
                    else
                    {
                        str = "1080";
                    }
                    this.InitOrder(orderid, num6, makeup, str8, errormsg, num3, str8);
                }
            }
            if ((!string.IsNullOrEmpty(str) && SysConfig.debuglog) && (((this.userid > 0) || (this.userInfo != null)) || (this.userInfo.isdebug == 1)))
            {
                debuginfo model = new debuginfo();
                model.userid = new int?(this.userid);
                model.addtime = new DateTime?(DateTime.Now);
                model.bugtype = debugtypeenum.卡类订单;
                model.detail = "版本号：" + SystemApiHelper.GetVersionName(this.version) + " " + errormsg;
                model.errorcode = str;
                model.errorinfo = this.GetErrorInfo(str);
                model.userorder = this.userorderid;
                if (base.Request.RawUrl != null)
                {
                    model.url = base.Request.RawUrl;
                }
                else
                {
                    model.url = string.Empty;
                }
                debuglog.Insert(model);
            }
            string str9 = this.ConvertParm(str);
            string str10 = SystemApiHelper.CreateSynchronousNotifySign(this.version, new object[] { str9, this.userorderid, this.userInfo.APIKey });
            string s = string.Format("returncode={0}&returnorderid={1}&sign={2}", str9, this.userorderid, str10);
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            base.Response.Write(s);
        }

        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("ext"), Encoding.GetEncoding("GB2312"));
            }
        }

        public int CardFaceValue
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrEmpty(this.money) && int.TryParse(this.money, out result))
                {
                    return result;
                }
                return result;
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
                return this.GetParmValue("typeid");
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

        public string parter
        {
            get
            {
                return this.GetParmValue("userid");
            }
        }

        public string productid
        {
            get
            {
                return this.GetParmValue("productid");
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("sign");
            }
        }

        public string sysChannelNo
        {
            get
            {
                return (SystemApiHelper.CodeMapping(this.sysChannelTypeId).ToString("0000") + this.money);
            }
        }

        public int sysChannelTypeId
        {
            get
            {
                int num = 0;
                try
                {
                    num = Convert.ToInt32(SystemApiHelper.ConverCardCode(this.version, this.cardtype, this.cardno));
                }
                catch
                {
                }
                return num;
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
                return SystemApiHelper.vc7010;
            }
        }
    }
}

