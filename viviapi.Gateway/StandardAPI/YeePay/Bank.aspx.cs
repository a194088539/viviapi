namespace viviapi.gateway.StandardAPI.YeePay
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
    using viviLib.Security;
    using viviLib.Web;

    public class Bank : Page
    {
        public decimal MaxChargeAMT = TransactionSetting.MaxTranATM;
        public decimal MinTranAMT = TransactionSetting.MinTranATM;

        public static string ConverBankCode(string bankcode)
        {
            switch (bankcode)
            {
                case "CMBCHINA-NET-B2C":
                    return "970";

                case "ICBC-NET-B2C":
                    return "967";

                case "ABC-NET-B2C":
                    return "964";

                case "CCB-NET-B2C":
                    return "965";

                case "BOC-NET-B2C":
                    return "963";

                case "SPDB-NET-B2C":
                    return "977";

                case "BOCO-NET-B2C":
                    return "981";

                case "CMBC-NET-B2C":
                    return "980";

                case "SDB-NET-B2C":
                    return "974";

                case "GDB-NET-B2C":
                    return "985";

                case "ECITIC-NET-B2C":
                    return "962";

                case "HXB-NET-B2C":
                    return "982";

                case "CIB-NET-B2C":
                    return "972";

                case "BCCB-NET-B2C":
                    return "989";

                case "CEB-NET-B2C":
                    return "986";

                case "PINGANBANK-NET":
                    return "978";

                case "SHB-NET-B2C":
                    return "975";

                case "POST-NET-B2C":
                    return "971";

                case "CBHB-NET-B2C":
                    return "CBHB";

                case "BJRCB-NET-B2C":
                    return "990";

                case "NJCB-NET-B2C":
                    return "979";

                case "HKBEA-NET-B2C":
                    return "987";

                case "NBCB-NET-B2C ":
                    return "998";

                case "HZBANK-NET-B2C":
                    return "983";

                case "CZ-NET-B2C":
                    return "968";

                case "CMBCHINA-NET":
                    return "970";

                case "ICBC-NET":
                    return "967";

                case "ABC-NET":
                    return "964";

                case "CCB-NET":
                    return "965";

                case "BOC-NET":
                    return "963";

                case "SPDB-NET":
                    return "977";

                case "BOCO-NET":
                    return "981";

                case "CMBC-NET":
                    return "980";

                case "SDB-NET":
                    return "974";

                case "GDB-NET":
                    return "985";

                case "ECITIC-NET":
                    return "962";

                case "HXB-NET":
                    return "982";

                case "CIB-NET":
                    return "972";

                case "BCCB-NET":
                    return "989";

                case "CEB-NET":
                    return "986";

                case "SHB-NET":
                    return "975";

                case "POST-NET":
                    return "971";

                case "CBHB-NET":
                    return "CBHB";

                case "BJRCB-NET":
                    return "990";

                case "NJCB-NET":
                    return "979";

                case "HKBEA-NET":
                    return "987";

                case "NBCB-NET ":
                    return "998";

                case "HZBANK-NET":
                    return "983";

                case "CZ-NET":
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
                str = "error:1001 商户ID（p1_MerId）不能空！";
            }
            else if (string.IsNullOrEmpty(this.money))
            {
                str = "error:1003 订单金额（p3_Amt）不能空！";
            }
            else if (string.IsNullOrEmpty(this.orderid))
            {
                str = "error:1004 商户订单号（p2_Order）不能空！";
            }
            else if (string.IsNullOrEmpty(this.notifyurl))
            {
                str = "error:1005 下行异步通知地址（p8_Url）不能空！";
            }
            else if (string.IsNullOrEmpty(this.sign))
            {
                str = "error:1006 MD5签名（hmac）不能空！";
            }
            else if (this.userid.Length > 11)
            {
                str = "error:1020 商户ID（p1_MerId）长度超过11位！";
            }
            else if (this.bankid.Length > 50)
            {
                str = "error:1021 支付通道编码（pd_FrpId）长度超过50位！";
            }
            else if (this.orderid.Length > 50)
            {
                str = "error:1022 商户订单号（p2_Order）长度超过50位！";
            }
            else if (this.money.Length > 20)
            {
                str = "error:1023 订单金额（ p3_Amt）长度超过最长限制！";
            }
            else if (this.notifyurl.Length > 0xff)
            {
                str = "error:1024 下行异步通知地址（p8_Url）长度超过255位！";
            }
            else if (this.returnurl.Length > 2)
            {
                str = "error:1025  p9_SAF（ p9_SAF）长度超过255位！";
            }
            else if (this.attach.Length > 0xff)
            {
                str = "error:1027 商户扩展信息（pa_MP）长度超过255位！";
            }
            else if (this.sign.Length != 0x20)
            {
                str = "error:1028 签名数据（hmac）长度不正确！";
            }
            else if (!viviLib.Text.Validate.IsNumeric(this.userid))
            {
                str = "error:1040 商户编号（p1_MerId）格式不正确！";
            }
            else if (!this.isNotifyUrlOk())
            {
                str = "error:1043  商户接收支付成功数据的地址（p8_Url）格式不正确！";
            }
            if (!string.IsNullOrEmpty(str))
            {
                WebUtility.ShowErrorMsg(str);
                return;
            }
            UserInfo userInfo = null;
            decimal result = 0M;
            int num2 = 0;
            if (!int.TryParse(this.userid, out num2))
            {
                str = "error:1064 商户编号不存在！";
            }
            else if (!decimal.TryParse(this.money, out result))
            {
                str = "error:1060 支付金额（p3_Amt）有误！";
            }
            else if (result < this.MinTranAMT)
            {
                str = "error:1061 订单金额（p3_Amt）小于最小允许交易额！";
            }
            else if (result > this.MaxChargeAMT)
            {
                str = string.Format("error:1062 订单金额（p3_Amt）{0:f2}大于最大允许交易额{1:f2}！", result, this.MaxChargeAMT);
            }
            else
            {
                bool checkUserOrderNo = RuntimeSetting.CheckUserOrderNo;
                int num3 = Dal.BankOrder_DataCheck(num2, checkUserOrderNo, this.orderid, out userInfo);
                switch (num3)
                {
                    case 1:
                        str = "error:1064 商户编号不存在";
                        goto Label_0387;

                    case 2:
                        str = "error:1065 商户状态不正常";
                        goto Label_0387;

                    case 3:
                        str = "error:1069 商户订单号重复";
                        break;
                }
                if (num3 == 3)
                {
                    str = "error:1069 商户订单号重复";
                }
                else if (!viviapi.BLL.Sys.Transaction.YeePay.Helper.CheckSign(this.userid, this.orderid, this.money, this.p4_Cur, this.p5_Pid, this.p6_Pcat, this.p7_Pdesc, this.notifyurl, this.returnurl, this.attach, this.bankid, this.pr_NeedResponse, userInfo.APIKey, this.sign))
                {
                    str = "error:1066 签名错误!";
                }
            }
        Label_0387:
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
                    o.returnurl = this.notifyurl;
                    o.ordertype = 1;
                    o.typeId = typeId;
                    o.paymodeId = this.sysBankcode;
                    o.supplierId = suppid;
                    o.supplierOrder = string.Empty;
                    o.userid = num2;
                    o.userorder = this.orderid;
                    o.refervalue = result;
                    o.cus_subject = this.p5_Pid;
                    o.cus_field1 = this.p6_Pcat;
                    o.cus_description = this.p7_Pdesc;
                    o.cus_field2 = this.p4_Cur;
                    o.cus_field3 = this.pr_NeedResponse;
                    o.cus_field4 = this.returnurl;
                    if (base.Request.UrlReferrer != null)
                    {
                        o.referUrl = base.Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        o.referUrl = string.Empty;
                    }
                    o.server = new int?(RuntimeSetting.ServerId);
                    o.version = this.version;
                    o.manageId = userInfo.manageId;
                    if (!(o.manageId.HasValue && (o.manageId.Value > 0)))
                    {
                        o.agentId = UserFactory.GetPromID(num2);
                    }
                    WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
                    bank.Insert(o);
                    if (string.IsNullOrEmpty(this.bankid))
                    {
                        string url = string.Empty;
                        string strToEncrypt = string.Concat(new object[] { this.userid.ToString(), suppid.ToString(), this.orderid, o.orderid, o.ordertype, o.returnurl, o.refervalue.ToString(), Constant.ParameterEncryptionKey });
                        url = string.Format("/links/Bankpay.aspx?userid={0}&typeId={1}&TraAmt={2}&OrderId={3}&sign={4}&OrderType={5}&UrlRef={6}&Userorder={7}", new object[] { this.userid, suppid, o.refervalue, o.orderid, Cryptography.MD5(strToEncrypt), o.ordertype, o.returnurl, this.orderid });
                        base.Response.Redirect(url);
                    }
                    else
                    {
                        SellFactory.OnlineBankPay(suppid, o.orderid, o.refervalue, o.paymodeId);
                    }
                }
            }
        }

        public string attach
        {
            get
            {
                return HttpUtility.UrlDecode(WebBase.GetQueryStringString("pa_MP", ""), Encoding.GetEncoding("GB2312"));
            }
        }

        public string bankid
        {
            get
            {
                return this.GetParmValue("pd_FrpId");
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
                return this.GetParmValue("p3_Amt");
            }
        }

        public string notifyurl
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("p8_Url"), Encoding.GetEncoding("gb2312"));
            }
        }

        public string orderid
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("p2_Order"), Encoding.GetEncoding("gb2312"));
            }
        }

        public string p0_Cmd
        {
            get
            {
                return this.GetParmValue("p0_Cmd");
            }
        }

        public string p4_Cur
        {
            get
            {
                return this.GetParmValue("p4_Cur");
            }
        }

        public string p5_Pid
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("p5_Pid"), Encoding.GetEncoding("gb2312"));
            }
        }

        public string p6_Pcat
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("p6_Pcat"), Encoding.GetEncoding("gb2312"));
            }
        }

        public string p7_Pdesc
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("p7_Pdesc"), Encoding.GetEncoding("gb2312"));
            }
        }

        public string pr_NeedResponse
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("pr_NeedResponse"), Encoding.GetEncoding("gb2312"));
            }
        }

        public string returnurl
        {
            get
            {
                return HttpUtility.UrlDecode(this.GetParmValue("p9_SAF"), Encoding.GetEncoding("gb2312"));
            }
        }

        public string sign
        {
            get
            {
                return this.GetParmValue("hmac");
            }
        }

        public string sysBankcode
        {
            get
            {
                return ConverBankCode(this.bankid);
            }
        }

        public string userid
        {
            get
            {
                return this.GetParmValue("p1_MerId");
            }
        }

        public string version
        {
            get
            {
                return SystemApiHelper.vYee10;
            }
        }
    }
}

