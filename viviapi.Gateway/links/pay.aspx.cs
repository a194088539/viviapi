namespace viviapi.gateway.links
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.Model.Order;
    using viviapi.Model.User;
    using viviapi.SysConfig;
    using viviLib.Web;

    public class pay : Page
    {
        protected Button btnCmmit;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HiddenField hftypeid;
        protected Repeater rptChannlType;
        protected TextBox txtAmt;
        protected TextBox txtCardId;
        protected TextBox txtCardPass;
        protected TextBox txtUserId;

        protected void btnCmmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAmt.Text))
            {
                viviapi.gateway.WebUtility.AlertAndRedirect(this, "请输入金额");
            }
            else
            {
                string url = string.Empty;
                string s = base.Request.Form["rdtypeId"];
                int result = 0;
                if (int.TryParse(s, out result))
                {
                    if ((((result == 100) || (result == 0x65)) || ((result == 0x66) || (result == 0x63))) || (result == 0x62))
                    {
                        decimal num2 = 0M;
                        if (!(decimal.TryParse(this.txtAmt.Text, out num2) && (num2 > 0M)))
                        {
                            viviapi.gateway.WebUtility.AlertAndRedirect(this, "请输入正确金额值");
                        }
                        else
                        {
                            url = string.Format("BankSelect.aspx?u={0}&v={1}&t={2}&r={3}&c=2&k={4}", new object[] { this.UserId, this.txtAmt.Text, s, HttpUtility.UrlEncode(this.UrlRef), viviapi.gateway.WebUtility.GetKey(this.UserId.ToString() + s + "2" + this.UrlRef + this.txtAmt.Text) });
                            base.Response.Redirect(url);
                        }
                    }
                    else
                    {
                        int num3 = 0;
                        string orderid = string.Empty;
                        if (!int.TryParse(this.txtAmt.Text, out num3))
                        {
                            viviapi.gateway.WebUtility.AlertAndRedirect(this, "请输入正确面值");
                        }
                        else if (string.IsNullOrEmpty(this.txtCardId.Text))
                        {
                            viviapi.gateway.WebUtility.AlertAndRedirect(this, "请输入卡号");
                        }
                        else if (string.IsNullOrEmpty(this.txtCardPass.Text))
                        {
                            viviapi.gateway.WebUtility.AlertAndRedirect(this, "请输入卡密");
                        }
                        else
                        {
                            string str4 = string.Empty;
                            int num4 = 0;
                            ChannelInfo info = viviapi.BLL.Channel.Channel.GetModel(result, num3, this.UserId, true);
                            if (info == null)
                            {
                                str4 = "1063";
                            }
                            else if (info.isOpen != 1)
                            {
                                str4 = "1064";
                            }
                            else if (info.supplier.Value <= 0)
                            {
                                str4 = "1065";
                            }
                            else
                            {
                                num4 = info.supplier.Value;
                            }
                            if (!string.IsNullOrEmpty(str4))
                            {
                                viviapi.gateway.WebUtility.AlertAndRedirect(this, this.GetErrorInfo(str4));
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(str4) && (num4 > 0))
                                {
                                    UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(this.UserId);
                                    OrderCard card = new OrderCard();
                                    OrderCardInfo o = new OrderCardInfo();
                                    o.orderid = card.GenerateUniqueOrderId(result);
                                    orderid = o.orderid;
                                    o.addtime = DateTime.Now;
                                    o.attach = "";
                                    o.notifycontext = string.Empty;
                                    o.notifycount = 0;
                                    o.notifystat = 0;
                                    o.notifyurl = string.Empty;
                                    o.clientip = ServerVariables.TrueIP;
                                    o.completetime = new DateTime?(DateTime.Now);
                                    o.ordertype = 2;
                                    o.typeId = result;
                                    o.paymodeId = info.code;
                                    o.payRate = 0M;
                                    o.supplierId = num4;
                                    o.supplierOrder = string.Empty;
                                    o.userid = this.UserId;
                                    o.userorder = o.orderid;
                                    o.refervalue = num3;
                                    o.referUrl = this.UrlRef;
                                    o.cardNo = this.txtCardId.Text;
                                    o.cardPwd = this.txtCardPass.Text;
                                    o.server = new int?(RuntimeSetting.ServerId);
                                    o.resultcode = string.Empty;
                                    o.opstate = string.Empty;
                                    o.ovalue = string.Empty;
                                    o.manageId = cacheUserBaseInfo.manageId;
                                    if (!(o.manageId.HasValue && (o.manageId.Value > 0)))
                                    {
                                        o.agentId = UserFactory.GetPromID(this.UserId);
                                    }
                                    WebCache.GetCacheService().AddObject(o.orderid, o, TransactionSetting.ExpiresTime);
                                    card.Insert(o);
                                    string errormsg = string.Empty;
                                    string supporderid = string.Empty;
                                    string supperrorcode = string.Empty;
                                    SupplierCode supp = (SupplierCode)num4;
                                    str4 = SellFactory.SellCard(supp, orderid, result, o.cardNo, o.cardPwd, string.Empty, num3, out supporderid, out supperrorcode, out errormsg);
                                }
                                url = string.Format("PayResult.aspx?o={0}&u={1}&t={2}&c={3}&p={4}&v={5}&e={6}&k={7}", new object[] { orderid, this.UserId, s, this.txtCardId.Text.Trim(), this.txtCardPass.Text.Trim(), this.txtAmt.Text.Trim(), str4, viviapi.gateway.WebUtility.GetKey(orderid + this.UserId.ToString() + s + this.txtCardId.Text.Trim() + this.txtCardPass.Text.Trim() + this.txtAmt.Text.Trim()) });
                                base.Response.Redirect(url);
                            }
                        }
                    }
                }
                else
                {
                    viviapi.gateway.WebUtility.AlertAndRedirect(this, "请选择付款方式");
                }
            }
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
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

                case "96":
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

                case "2001":
                    return "数据接收成功!";
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.UrlRef = string.Empty;
                if (base.Request.UrlReferrer != null)
                {
                    this.UrlRef = base.Request.UrlReferrer.ToString();
                }
                string str = string.Empty;
                if (!SysConfig.IsOpenNoLaiLu)
                {
                    str = "ERROR 000001 不开放支付";
                }
                else if (!viviapi.gateway.WebUtility.CheckKey(this.UserId.ToString(), this.Mac))
                {
                    str = "ERROR 000001 参数错误";
                }
                else
                {
                    UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(this.UserId);
                    if (cacheUserBaseInfo == null)
                    {
                        str = "ERROR 000002 参数错误 商户非法";
                    }
                    else if (cacheUserBaseInfo.Status != 2)
                    {
                        str = "ERROR 000003 参数错误 商户状态不正常";
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    base.Response.Write(str);
                    base.Response.End();
                }
                this.txtUserId.Text = this.UserId.ToString();
                this.txtUserId.Enabled = false;
                this.txtAmt.Attributes["onkeypress"] = "if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}";
                DataTable table = ChannelType.GetCacheList().Copy();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int typeId = Convert.ToInt32(table.Rows[i]["typeId"]);
                    bool enable = false;
                    ChannelType.GetModel(typeId, this.UserId, out enable);
                    if (!(enable && (typeId != 90)))
                    {
                        table.Rows[i].Delete();
                    }
                }
                table.AcceptChanges();
                this.rptChannlType.DataSource = table;
                this.rptChannlType.DataBind();
            }
        }

        public string Mac
        {
            get
            {
                return WebBase.GetQueryStringString("k", "");
            }
        }

        public string UrlRef
        {
            get
            {
                return (string)this.ViewState["UrlRef"];
            }
            set
            {
                this.ViewState["UrlRef"] = value;
            }
        }

        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("u", 0);
            }
        }
    }
}

