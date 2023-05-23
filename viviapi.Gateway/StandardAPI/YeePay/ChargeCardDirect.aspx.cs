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
    using viviLib.Logging;
    using viviLib.Web;

    public class ChargeCardDirect : Page
    {
        private ChannelTypeInfo _typeInfo = null;
        public UserInfo _userInfo = null;
        private string[] cardStatus;
        private bool checkok = false;
        private OrderCard OrderBLL = new OrderCard();
        private string[] sellflag;
        private string[] supperrinfolist;
        private int[] supplierlist;
        private string[] supporderidlist;
        private string sysOrderId = string.Empty;

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
            decimal num;
            string str = "";
            string[] strArray = this.pa7_cardAmt.Split(new char[] { ',' });
            string[] strArray2 = this.pa8_cardNo.Split(new char[] { ',' });
            string[] strArray3 = this.pa9_cardPwd.Split(new char[] { ',' });
            if (((strArray.Length == strArray2.Length) && (strArray.Length == strArray3.Length)) && (strArray.Length > 1))
            {
                str = string.Empty;
                if (strArray.Length > 10)
                {
                    str = "5";
                }
                else
                {
                    foreach (string str2 in strArray)
                    {
                        num = 0M;
                        if (!decimal.TryParse(str2, out num))
                        {
                            str = "8001";
                        }
                    }
                    if (string.IsNullOrEmpty(str))
                    {
                        foreach (string str2 in strArray2)
                        {
                            if (string.IsNullOrEmpty(str2))
                            {
                                str = "8002";
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(str))
                    {
                        foreach (string str2 in strArray3)
                        {
                            if (string.IsNullOrEmpty(str2))
                            {
                                str = "8002";
                            }
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                num = 0M;
                if (!decimal.TryParse(this.p3_Amt, out num))
                {
                    str = "66";
                }
            }
            return str;
        }

        private bool CheckSign()
        {
            if (this.userInfo == null)
            {
                return false;
            }
            string aPIKey = this.userInfo.APIKey;
            string str2 = "";
            return (Digest.HmacSign(((((((((str2 + "ChargeCardDirect") + this.p1_MerId + this.p2_Order) + this.p3_Amt + this.p4_verifyAmt) + this.p5_Pid + this.p6_Pcat) + this.p7_Pdesc + this.p8_Url) + this.pa_MP + this.pa7_cardAmt) + this.pa8_cardNo + this.pa9_cardPwd) + this.pd_FrpId + this.pr_NeedResponse) + this.pz_userId + this.pz1_userRegTime, aPIKey) == this.hmac);
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
            ChannelInfo info = viviapi.BLL.Channel.Channel.GetModel(SystemApiHelper.CodeMapping(this.ChannelTypeId).ToString("0000") + decimal.Round(Convert.ToDecimal(_value), 0).ToString(), this.userid, true);
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

        public string GetErrorMsg(string[] list)
        {
            string str = string.Empty;
            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0)
                {
                    str = str + list[i];
                }
                else
                {
                    str = str + "," + list[i];
                }
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
            if (this.cardnum == 1)
            {
                str = str + decimal.Round(Convert.ToDecimal(this.p3_Amt), 0).ToString();
            }
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
            o.cardNo = this.pa8_cardNo;
            o.cardType = SystemApiHelper.CodeMapping(this.ChannelTypeId);
            o.cardPwd = this.pa9_cardPwd;
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
            o.cardnum = this.cardnum;
            o.resultcode = _returncode;
            o.ismulticard = (this.cardnum > 1) ? 1 : 0;
            o.status = _status;
            o.ovalue = string.Empty;
            o.opstate = _opstate;
            o.msg = _msg;
            o.Desc = string.Empty;
            o.version = this.version;
            o.cus_subject = this.p5_Pid;
            o.cus_field1 = this.p6_Pcat;
            o.cus_description = this.p7_Pdesc;
            o.cus_field2 = this.pz_userId;
            o.cus_field3 = this.pr_NeedResponse;
            o.cus_field4 = this.p4_verifyAmt;
            o.cus_field5 = this.pz1_userRegTime;
            o.manageId = this.userInfo.manageId;
            if (!(o.manageId.HasValue && (o.manageId.Value > 0)))
            {
                o.agentId = UserFactory.GetPromID(this.userid);
            }
            WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
            this.OrderBLL.Insert(o);
        }

        private void InitOrderItem(string _pOrderid, int serial, int status, string _cardNo, string _cardPwd, int _suppid, decimal _supprate, string _suppNo, decimal _cardvalue, string _opstate, string _msg)
        {
            CardItemInfo o = new CardItemInfo();
            o.userid = this.userid;
            o.porderid = _pOrderid;
            o.serial = serial;
            o.cardtype = this.ChannelTypeId;
            o.cardno = _cardNo;
            o.cardpwd = _cardPwd;
            o.refervalue = new decimal?(_cardvalue);
            o.status = status;
            o.suppid = _suppid;
            o.supplierOrder = _suppNo;
            o.opstate = _opstate;
            o.msg = _msg;
            o.addtime = DateTime.Now;
            o.supplierOrder = string.Empty;
            o.realvalue = 0M;
            o.completetime = new DateTime?(DateTime.Now);
            o.supplierrate = _supprate;
            o.promrate = 0M;
            o.commission = 0M;
            WebCache.GetCacheService().AddObject(o.porderid + serial.ToString(), o, TransactionSetting.ExpiresTime);
            this.OrderBLL.InsertItem(o);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int num3;
            string str = "1";
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
                this.sysOrderId = this.OrderBLL.GenerateUniqueOrderId(this.ChannelTypeId);
                string[] strArray = this.pa7_cardAmt.Split(new char[] { ',' });
                string[] strArray2 = this.pa8_cardNo.Split(new char[] { ',' });
                string[] strArray3 = this.pa9_cardPwd.Split(new char[] { ',' });
                this.cardStatus = new string[this.cardnum];
                this.supplierlist = new int[this.cardnum];
                this.supporderidlist = new string[this.cardnum];
                this.supperrinfolist = new string[this.cardnum];
                this.sellflag = new string[this.cardnum];
                int num = 0;
                decimal num2 = 0M;
                string str2 = string.Empty;
                string str3 = string.Empty;
                for (num3 = 0; num3 < this.cardnum; num3++)
                {
                    this.cardStatus[num3] = "1006";
                    this.supplierlist[num3] = 0;
                    this.sellflag[num3] = "N";
                    this.supporderidlist[num3] = string.Empty;
                    this.supperrinfolist[num3] = string.Empty;
                    string str6 = string.Empty;
                    if (!ChannelType.CheckCardFormat(this.ChannelTypeId, strArray2[num3], strArray3[num3], 0))
                    {
                        this.cardStatus[num3] = "7";
                        str6 = "卡格式不正确";
                        this.supperrinfolist[num3] = str6;
                    }
                    else
                    {
                        decimal result = 0M;
                        if (decimal.TryParse(strArray[num3], out result))
                        {
                            string str7 = this.GetChannelInfo(strArray[num3], out num, out num2);
                            this.supplierlist[num3] = num;
                            if (!string.IsNullOrEmpty(str7))
                            {
                                this.cardStatus[num3] = "1003";
                                str6 = "不支持的卡类型";
                                this.supperrinfolist[num3] = str6;
                            }
                            else if (num <= 0)
                            {
                                this.cardStatus[num3] = "10000";
                                str6 = "未设置接口商";
                                this.supperrinfolist[num3] = str6;
                            }
                            else
                            {
                                this.cardStatus[num3] = "0";
                            }
                        }
                        else
                        {
                            this.cardStatus[num3] = "1003";
                            str6 = "不支持的卡类型";
                            this.supperrinfolist[num3] = str6;
                        }
                    }
                    if (this.cardnum > 1)
                    {
                        this.InitOrderItem(this.sysOrderId, num3, (this.cardStatus[num3] == "0") ? 1 : 4, strArray2[num3], strArray3[num3], num, num2, string.Empty, Convert.ToDecimal(strArray[num3]), this.cardStatus[num3], str6);
                    }
                    if ((this.cardnum == 1) || ((this.cardnum > 1) && (num3 == (this.cardnum - 1))))
                    {
                        this.InitOrder(this.sysOrderId, ((this.cardnum > 1) || (this.cardStatus[num3] == "0")) ? 1 : 4, (this.cardnum > 1) ? str : this.cardStatus[num3], this.GetErrorMsg(this.supperrinfolist), num, str3.ToString());
                    }
                }
                for (num3 = 0; num3 < this.cardnum; num3++)
                {
                    if (this.cardStatus[num3] == "0")
                    {
                        this.sellflag[num3] = "Y";
                        str2 = string.Empty;
                        string str8 = string.Empty;
                        this.cardStatus[num3] = this.Sell(this.supplierlist[num3], this.sysOrderId, num3, strArray2[num3], strArray3[num3], this.ChannelTypeId, strArray[num3], out str8, out str2);
                        this.supporderidlist[num3] = str8;
                        this.supperrinfolist[num3] = str2;
                        if (this.cardStatus[num3] == "0")
                        {
                            this.checkok = true;
                        }
                        else
                        {
                            this.cardStatus[num3] = "1006";
                        }
                    }
                }
            }
            string str9 = string.Empty;
            string str10 = "ChargeCardDirect" + str + this.p2_Order + str9;
            LogHelper.Write(str10);
            string aKey = string.Empty;
            if (this.userInfo != null)
            {
                aKey = this.userInfo.APIKey;
            }
            string str12 = Digest.HmacSign(str10, aKey);
            LogHelper.Write(aKey);
            LogHelper.Write(str12);
            string s = string.Format("r0_Cmd={1}{0}r1_Code={2}{0}r6_Order={3}{0}rq_ReturnMsg={4}{0}hmac={5}", new object[] { '\n', "ChargeCardDirect", str, this.p2_Order, str9, str12 });
            base.Response.Write(s);
            if (str == "1")
            {
                if (this.cardnum > 1)
                {
                    for (num3 = 0; num3 < this.cardnum; num3++)
                    {
                        if ((this.cardStatus[num3] != "0") && (this.sellflag[num3] == "Y"))
                        {
                            string orderId = this.sysOrderId + num3.ToString();
                            this.OrderBLL.ReceiveSuppResult(this.supplierlist[num3], orderId, this.supporderidlist[num3], 4, this.cardStatus[num3], this.supperrinfolist[num3], 0M, 0M, string.Empty);
                        }
                    }
                }
                if (!this.checkok)
                {
                    this.OrderBLL.ReceiveSuppResult(this.supplierlist[0], this.sysOrderId, this.GetErrorMsg(this.supporderidlist), 4, this.GetErrorMsg(this.cardStatus), this.GetErrorMsg(this.supperrinfolist), 0M, 0M, string.Empty);
                }
            }
            base.Response.End();
        }

        private string Sell(int _suppId, string _sysorderid, int _serial, string _cardno, string _cardpwd, int _typeid, string _cardvalue, out string _supporderid, out string _errmsg)
        {
            _supporderid = string.Empty;
            _errmsg = string.Empty;
            string supperrorcode = string.Empty;
            if (this.cardnum > 1)
            {
                _sysorderid = _sysorderid + _serial.ToString();
            }
            SupplierCode supp = (SupplierCode)_suppId;
            return SellFactory.SellCard(supp, _sysorderid, _typeid, _cardno, _cardpwd, string.Empty, Convert.ToInt32(decimal.Round(Convert.ToDecimal(_cardvalue), 0)), out _supporderid, out supperrorcode, out _errmsg);
        }

        public int cardnum
        {
            get
            {
                int length = 0;
                try
                {
                    if (!string.IsNullOrEmpty(this.pa8_cardNo))
                    {
                        length = this.pa8_cardNo.Split(new char[] { ',' }).Length;
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
                return length;
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

        public string p4_verifyAmt
        {
            get
            {
                return this.GetParmValue("p4_verifyAmt");
            }
        }

        public string p5_Pid
        {
            get
            {
                return this.GetParmValue("p5_Pid");
            }
        }

        public string p6_Pcat
        {
            get
            {
                return this.GetParmValue("p6_Pcat");
            }
        }

        public string p7_Pdesc
        {
            get
            {
                return this.GetParmValue("p7_Pdesc");
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

        public string pa7_cardAmt
        {
            get
            {
                return this.GetParmValue("pa7_cardAmt");
            }
        }

        public string pa8_cardNo
        {
            get
            {
                return this.GetParmValue("pa8_cardNo");
            }
        }

        public string pa9_cardPwd
        {
            get
            {
                return this.GetParmValue("pa9_cardPwd");
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

        public string pz_userId
        {
            get
            {
                return this.GetParmValue("pz_userId");
            }
        }

        public string pz1_userRegTime
        {
            get
            {
                return this.GetParmValue("pz1_userRegTime");
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
                return SystemApiHelper.vcYee10;
            }
        }
    }
}

