namespace viviapi.gateway.StandardAPI.YeePay
{
    using System;
    using System.Web.UI;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.BLL.Sys;
    using viviapi.BLL.Sys.Transaction.YeePay;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.Model.Order;
    using viviapi.Model.Sys;
    using viviapi.Model.User;
    using viviapi.SysConfig;
    using viviLib.ExceptionHandling;
    using viviLib.Web;

    public class AnnulCard : Page
    {
        private ChannelTypeInfo _typeInfo = null;
        public UserInfo _userInfo = null;
        private OrderCard OrderBLL = new OrderCard();

        public string ChannelNo(int faceValue)
        {
            string str = string.Empty;
            int num = SystemApiHelper.CodeMapping(this.ChannelTypeId);
            if (num > 0)
            {
                str = num.ToString("0000") + faceValue.ToString();
            }
            return str;
        }

        private string CheckCard()
        {
            string str = "";
            if (string.IsNullOrEmpty(this.pa7_cardNo) || string.IsNullOrEmpty(this.pa8_cardPwd))
            {
                return "7";
            }
            if (string.IsNullOrEmpty(this.cardNo) || string.IsNullOrEmpty(this.cardPwd))
            {
                return "7";
            }
            if (!ChannelType.CheckCardFormat(this.ChannelTypeId, this.cardNo, this.cardPwd, 0))
            {
                return "7";
            }
            if (this.facevalue <= 0M)
            {
                str = "66";
            }
            return str;
        }

        private bool CheckSign()
        {
            string str = "";
            return (Digest.HmacSign((((((str + "AnnulCard") + this.p1_MerId + this.p2_Order) + this.p3_Amt + this.p8_Url) + this.pa_MP + this.cardNo) + this.cardPwd + this.pd_FrpId) + this.pa0_Mode + this.pr_NeedResponse, this.ApiKey) == this.hmac);
        }

        public string ConverCardCode(string cardtype)
        {
            string str = string.Empty;
            switch (cardtype)
            {
                case "SZX":
                    return "103";

                case "SNDACARD":
                    return "104";

                case "ZHENGTU":
                    return "105";

                case "JUNNET":
                    return "106";

                case "QQCARD":
                    return "107";

                case "UNICOM":
                    return "108";

                case "JIUYOU":
                    return "109";

                case "NETEASE":
                    return "110";

                case "WANMEI":
                    return "111";

                case "SOHU":
                    return "112";

                case "TELECOM":
                    return "113";

                case "ZONGYOU":
                    return "117";

                case "TIANXIA":
                    return "118";

                case "TIANHONG":
                    return "119";
            }
            return str;
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

        private string GetChannelInfo(string _value, out int _supplierId, out decimal _supprate)
        {
            _supplierId = 0;
            _supprate = 0M;
            string str = string.Empty;
            try
            {
                decimal result = 0M;
                decimal.TryParse(_value, out result);
                ChannelInfo info = viviapi.BLL.Channel.Channel.GetModel(SystemApiHelper.CodeMapping(this.ChannelTypeId).ToString("0000") + decimal.Round(result, 0).ToString(), this.userid, true);
                if (info == null)
                {
                    return "1003";
                }
                if (info.isOpen != 1)
                {
                    return "1003";
                }
                if (info.supplier.Value <= 0)
                {
                    return "10000";
                }
                _supplierId = info.supplier.Value;
                _supprate = info.supprate;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return str;
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "-1":
                    return "签名较验失败或未知错误！";

                case "1":
                    return "提交成功！";

                case "2":
                    return "卡密成功处理过或者提交卡号过于频繁！";

                case "5":
                    return "卡数量过多，目前最多支持10张卡！";

                case "11":
                    return "订单号重复！";

                case "66":
                    return "支付金额有误！";

                case "95":
                    return "支付方式未开通";

                case "8001":
                    return "卡面额组填写错误！";

                case "8002":
                    return "卡号密码为空或者数量不相等（使用组合支付时）！";
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

        private void InitOrder(string _orderid, int _status, string _opstate, string _msg, int _suppId, string _returncode)
        {
            string str = this.GetCardType(this.ChannelTypeId).ToString("0000");
            if (!string.IsNullOrEmpty(_returncode) && _returncode.EndsWith("|"))
            {
                _returncode = _returncode.Substring(0, _returncode.Length - 1);
            }
            OrderCardInfo o = new OrderCardInfo();
            o.ordertype = 1;
            o.orderid = _orderid;
            o.userid = this.userid;
            o.userorder = this.p2_Order;
            o.typeId = this.ChannelTypeId;
            o.cardNo = this.cardNo;
            o.cardType = SystemApiHelper.CodeMapping(this.ChannelTypeId);
            o.cardPwd = this.cardPwd;
            o.paymodeId = str;
            o.refervalue = Convert.ToDecimal(this.p3_Amt);
            o.attach = this.pa_MP;
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
            o.notifyurl = this.p8_Url;
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
            o.version = this.version;
            o.cus_subject = string.Empty;
            o.cus_field1 = string.Empty;
            o.cus_description = string.Empty;
            o.cus_field2 = string.Empty;
            o.cus_field3 = this.pr_NeedResponse;
            o.cus_field4 = this.pa0_Mode;
            o.cus_field5 = string.Empty;
            o.manageId = this.userInfo.manageId;
            if (!(o.manageId.HasValue && (o.manageId.Value > 0)))
            {
                o.agentId = UserFactory.GetPromID(this.userid);
            }
            WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
            this.OrderBLL.Insert(o);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "1";
            int num = 0;
            decimal num2 = 0M;
            string opstate = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            if (((this.userid <= 0) || (this.userInfo == null)) || (this.userInfo.Status != 2))
            {
                str = "112";
            }
            else if (!(((this.ChannelTypeId != 0) && (this.typeInfo != null)) && this.ChannelTypeIdOpenStatus))
            {
                str = "95";
            }
            else
            {
                str = this.CheckCard();
                if (string.IsNullOrEmpty(str))
                {
                    str = this.GetChannelInfo(this.p3_Amt, out num, out num2);
                    if (string.IsNullOrEmpty(str))
                    {
                        if (!this.CheckSign())
                        {
                            str = "-1";
                        }
                        else
                        {
                            str = "1";
                        }
                    }
                }
            }
            if (str != "1")
            {
                if (SysConfig.debuglog && (((this.userid > 0) && (this.userInfo != null)) && (this.userInfo.isdebug == 1)))
                {
                    debuginfo model = new debuginfo();
                    model.userid = new int?(this.userid);
                    model.addtime = new DateTime?(DateTime.Now);
                    model.bugtype = debugtypeenum.卡类订单;
                    model.detail = "版本号：" + SystemApiHelper.GetVersionName(this.version);
                    model.errorcode = str;
                    model.errorinfo = this.GetErrorInfo(str);
                    model.userorder = this.p2_Order;
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
            }
            else
            {
                str5 = this.OrderBLL.GenerateUniqueOrderId(this.ChannelTypeId);
                this.InitOrder(str5, 1, str, string.Empty, num, string.Empty);
                opstate = this.Sell(num, str5, 0, this.cardNo, this.cardPwd, this.ChannelTypeId, this.p3_Amt, out str3, out str4);
            }
            string str6 = "AnnulCard";
            string str7 = str5;
            string str8 = this.p2_Order;
            string str9 = str;
            string str10 = Digest.HmacSign(str6 + str + str7 + str8 + str9, this.ApiKey);
            string s = string.Format("r0_Cmd={1}{0}r1_Code={2}{0}r2_TrxId={3}{0}r6_Order={4}{0}rq_ReturnMsg={5}{0}hmac={6}", new object[] { '\n', str6, str, str7, str8, str9, str10 });
            base.Response.Write(s);
            if ((str == "1") && (opstate != "0"))
            {
                this.OrderBLL.ReceiveSuppResult(num, str5, str3, 4, opstate, str4, 0M, 0M, str);
            }
            base.Response.End();
        }

        private string Sell(int _suppId, string _sysorderid, int _serial, string _cardno, string _cardpwd, int _typeid, string _cardvalue, out string _supporderid, out string _errmsg)
        {
            string supperrorcode = string.Empty;
            _supporderid = string.Empty;
            _errmsg = string.Empty;
            try
            {
                decimal result = 0M;
                decimal.TryParse(_cardvalue, out result);
                SupplierCode supp = (SupplierCode)_suppId;
                return SellFactory.SellCard(supp, _sysorderid, _typeid, _cardno, _cardpwd, string.Empty, Convert.ToInt32(decimal.Round(result, 0)), out _supporderid, out supperrorcode, out _errmsg);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string ApiKey
        {
            get
            {
                if (this.userInfo == null)
                {
                    return string.Empty;
                }
                return this.userInfo.APIKey;
            }
        }

        public string cardNo
        {
            get
            {
                try
                {
                    if (this.pa0_Mode == "3")
                    {
                        return DES.Decrypt3DES(this.pa7_cardNo, this.ApiKey);
                    }
                    return this.pa7_cardNo;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public string cardPwd
        {
            get
            {
                try
                {
                    if (this.pa0_Mode == "3")
                    {
                        return DES.Decrypt3DES(this.pa8_cardPwd, this.ApiKey);
                    }
                    return this.pa8_cardPwd;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public int ChannelTypeId
        {
            get
            {
                int num = 0;
                try
                {
                    string str = this.ConverCardCode(this.pd_FrpId);
                    if (string.IsNullOrEmpty(str))
                    {
                        return num;
                    }
                    return Convert.ToInt32(str);
                }
                catch
                {
                }
                return num;
            }
        }

        public bool ChannelTypeIdOpenStatus
        {
            get
            {
                if ((((this.ChannelTypeId <= 0) || (this.userid <= 0)) || (this.userInfo == null)) || (this.userInfo.Status != 2))
                {
                    return false;
                }
                return (ChannelType.GetChannelTypeStatus(this.ChannelTypeId, this.userid) == 1);
            }
        }

        private decimal facevalue
        {
            get
            {
                decimal result = 0M;
                if (!decimal.TryParse(this.p3_Amt, out result))
                {
                    return 0M;
                }
                return result;
            }
        }

        public string hmac
        {
            get
            {
                return this.GetParmValue("hmac");
            }
        }

        public string p0_Cmd
        {
            get
            {
                return this.GetParmValue("p0_Cmd");
            }
        }

        public string p1_MerId
        {
            get
            {
                return this.GetParmValue("p1_MerId");
            }
        }

        public string p2_Order
        {
            get
            {
                return this.GetParmValue("p2_Order");
            }
        }

        public string p3_Amt
        {
            get
            {
                return this.GetParmValue("p3_Amt");
            }
        }

        public string p8_Url
        {
            get
            {
                return this.GetParmValue("p8_Url");
            }
        }

        public string pa_MP
        {
            get
            {
                return this.GetParmValue("pa_MP");
            }
        }

        public string pa0_Mode
        {
            get
            {
                return this.GetParmValue("pa0_Mode");
            }
        }

        public string pa7_cardNo
        {
            get
            {
                return this.GetParmValue("pa7_cardNo");
            }
        }

        public string pa8_cardPwd
        {
            get
            {
                return this.GetParmValue("pa8_cardPwd");
            }
        }

        public string pd_FrpId
        {
            get
            {
                return this.GetParmValue("pd_FrpId");
            }
        }

        public string pr_NeedResponse
        {
            get
            {
                return this.GetParmValue("pr_NeedResponse");
            }
        }

        private ChannelTypeInfo typeInfo
        {
            get
            {
                if ((this.ChannelTypeId > 0) && (this._typeInfo == null))
                {
                    this._typeInfo = ChannelType.GetCacheModel(this.ChannelTypeId);
                }
                return this._typeInfo;
            }
        }

        public int userid
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrEmpty(this.p1_MerId) && int.TryParse(this.p1_MerId, out result))
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
                return this._userInfo;
            }
        }

        public string version
        {
            get
            {
                return SystemApiHelper.vcYee20;
            }
        }
    }
}

