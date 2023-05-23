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
using viviapi.ETAPI.KuaiQian;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.Web;

namespace viviapi.gateway.links
{
    public class checkPay : Page
    {
        private userHostInfo _hostInfo = (userHostInfo)null;
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected HiddenField hftypeid;
        protected TextBox txtUserId;
        protected Repeater rptChannlType;
        protected TextBox txtCardId;
        protected TextBox txtCardPass;
        protected TextBox txtAmt;
        protected Button btnCmmit;

        public int HostId
        {
            get
            {
                return WebBase.GetQueryStringInt32("h", 0);
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
                this.ViewState["UrlRef"] = (object)value;
            }
        }

        public userHostInfo hostInfo
        {
            get
            {
                if (this._hostInfo == null && this.HostId > 0)
                    this._hostInfo = new userHost().GetCacheModel(this.HostId);
                return this._hostInfo;
            }
        }

        public int UserId
        {
            get
            {
                if (this.hostInfo == null)
                    return 0;
                return this.hostInfo.userid.Value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            this.UrlRef = string.Empty;
            if (this.Request.UrlReferrer != (Uri)null)
                this.UrlRef = this.Request.UrlReferrer.ToString();
            string s = string.Empty;
            int num = this.HostId;
            if (!WebUtility.CheckKey(num.ToString(), this.Mac))
                s = "ERROR 000001 参数错误";
            else if (this.hostInfo == null)
                s = "ERROR 00002 参数错误";
            else if (this.hostInfo.status == userHostStatus.已关闭)
                s = "ERROR 00003 链接已关闭";
            else if (string.IsNullOrEmpty(this.UrlRef))
                s = "ERROR 00004 来源为空";
            else if (this.Request.UrlReferrer.Host.ToLower() != this.hostInfo.hostUrl.ToLower())
                s = "ERROR 00003 来源错误";
            if (!string.IsNullOrEmpty(s))
            {
                this.Response.Write(s);
                this.Response.End();
            }
            TextBox textBox = this.txtUserId;
            num = this.UserId;
            string str = num.ToString();
            textBox.Text = str;
            this.txtUserId.Enabled = false;
            this.txtAmt.Attributes["onkeypress"] = "if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}";
            DataTable dataTable = ChannelType.GetCacheList().Copy();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
                int typeId = Convert.ToInt32(dataTable.Rows[index]["typeId"]);
                bool enable = false;
                ChannelType.GetModel(typeId, this.UserId, out enable);
                if (!enable || typeId == 90)
                    dataTable.Rows[index].Delete();
            }
            dataTable.AcceptChanges();
            this.rptChannlType.DataSource = (object)dataTable;
            this.rptChannlType.DataBind();
        }

        private string GetErrorInfo(string ErrorCode)
        {
            string str = ErrorCode;
            switch (ErrorCode)
            {
                case "1001":
                    str = "卡类型（type）不能空！";
                    break;
                case "1002":
                    str = "商户ID（parter）不能空！";
                    break;
                case "1003":
                    str = "卡号（cardno）不能空！";
                    break;
                case "1004":
                    str = "密码（cardpwd）不能空！";
                    break;
                case "1005":
                    str = "订单金额（value）不能空！";
                    break;
                case "1006":
                    str = "卡能使用的地理范围（restrict）不能空！";
                    break;
                case "1007":
                    str = "商户订单号（orderid）不能空！";
                    break;
                case "1008":
                    str = "异步通知地址（callbackurl）不能空！";
                    break;
                case "1009":
                    str = "MD5签名（sign）不能空！";
                    break;
                case "96":
                    str = "卡类型（type）长度超过最长限制！";
                    break;
                case "1021":
                    str = "商户ID（parter）长度超过最长限制！";
                    break;
                case "1022":
                    str = "卡号（cardno）长度超过最长限制！";
                    break;
                case "1023":
                    str = "密码（cardpwd）长度超过最长限制！";
                    break;
                case "1024":
                    str = "订单金额（value）长度超过最长限制！";
                    break;
                case "1025":
                    str = "卡能使用的地理范围（restrict）长度超过最长限制！";
                    break;
                case "1026":
                    str = "商户订单号（orderid）长度超过最长限制！";
                    break;
                case "1027":
                    str = "异步通知地址（callbackurl）长度超过最长限制！";
                    break;
                case "1028":
                    str = "备注(attach)消息长度超过最长限制！";
                    break;
                case "1029":
                    str = "签名（sign）长度不正确！";
                    break;
                case "1041":
                    str = "卡类型（type）格式不正确！";
                    break;
                case "1042":
                    str = "商户ID（parter）格式不正确！";
                    break;
                case "1043":
                    str = "卡号（cardno）格式不正确！";
                    break;
                case "1044":
                    str = "卡密（cardpwd）格式不正确！";
                    break;
                case "1045":
                    str = "卡面值（value）格式不正确！";
                    break;
                case "1046":
                    str = "异步通知地址（callbackurl）格式不正确！";
                    break;
                case "1060":
                    str = "非法商户ID（parter）！";
                    break;
                case "1061":
                    str = "商户（parter）状态不正常!";
                    break;
                case "1062":
                    str = "签名错误！";
                    break;
                case "1063":
                    str = "不支持此卡类别!";
                    break;
                case "1064":
                    str = "通道维护中!";
                    break;
                case "1068":
                    str = "商户订单号重复!";
                    break;
                case "2001":
                    str = "数据接收成功!";
                    break;
            }
            return str;
        }

        protected void btnCmmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAmt.Text))
            {
                WebUtility.AlertAndRedirect((Page)this, "请输入金额");
            }
            else
            {
                string str1 = string.Empty;
                string s = this.Request.Form["rdtypeId"];
                int result1 = 0;
                if (int.TryParse(s, out result1))
                {
                    if (result1 == 100 || result1 == 101 || result1 == 102)
                    {
                        Decimal result2 = new Decimal(0);
                        if (!Decimal.TryParse(this.txtAmt.Text, out result2) || result2 <= new Decimal(0))
                            WebUtility.AlertAndRedirect((Page)this, "请输入正确金额值");
                        else
                            this.Response.Redirect(string.Format("BankSelect.aspx?u={0}&v={1}&t={2}&r={3}&c=4&k={4}", (object)this.UserId, (object)this.txtAmt.Text, (object)s, (object)HttpUtility.UrlEncode(this.UrlRef), (object)WebUtility.GetKey(this.UserId.ToString() + s + "4" + this.UrlRef + this.txtAmt.Text)));
                    }
                    else
                    {
                        int result2 = 0;
                        string _orderid = string.Empty;
                        if (!int.TryParse(this.txtAmt.Text, out result2))
                            WebUtility.AlertAndRedirect((Page)this, "请输入正确面值");
                        else if (string.IsNullOrEmpty(this.txtCardId.Text))
                            WebUtility.AlertAndRedirect((Page)this, "请输入卡号");
                        else if (string.IsNullOrEmpty(this.txtCardPass.Text))
                        {
                            WebUtility.AlertAndRedirect((Page)this, "请输入卡密");
                        }
                        else
                        {
                            string ErrorCode = string.Empty;
                            int num = 0;
                            ChannelInfo model = Channel.GetModel(result1, result2, this.UserId, false);
                            if (model == null)
                            {
                                ErrorCode = "1063";
                            }
                            else
                            {
                                int? nullable = model.isOpen;
                                if ((nullable.GetValueOrDefault() != 1 ? 1 : (!nullable.HasValue ? 1 : 0)) != 0)
                                {
                                    ErrorCode = "1064";
                                }
                                else
                                {
                                    nullable = model.supplier;
                                    if (nullable.Value <= 0)
                                    {
                                        ErrorCode = "1065";
                                    }
                                    else
                                    {
                                        nullable = model.supplier;
                                        num = nullable.Value;
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(ErrorCode))
                            {
                                WebUtility.AlertAndRedirect((Page)this, this.GetErrorInfo(ErrorCode));
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(ErrorCode) && num > 0)
                                {
                                    UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(this.UserId);
                                    OrderCard orderCard = new OrderCard();
                                    OrderCardInfo order = new OrderCardInfo();
                                    order.orderid = orderCard.GenerateUniqueOrderId(result1);
                                    _orderid = order.orderid;
                                    order.addtime = DateTime.Now;
                                    order.attach = "";
                                    order.notifycontext = string.Empty;
                                    order.notifycount = 0;
                                    order.notifystat = 0;
                                    order.notifyurl = string.Empty;
                                    order.clientip = ServerVariables.TrueIP;
                                    order.completetime = new DateTime?(DateTime.Now);
                                    order.ordertype = 4;
                                    order.typeId = result1;
                                    order.paymodeId = model.code;
                                    order.payRate = new Decimal(0);
                                    order.supplierId = num;
                                    order.supplierOrder = string.Empty;
                                    order.userid = this.UserId;
                                    order.userorder = order.orderid;
                                    order.refervalue = (Decimal)result2;
                                    order.referUrl = this.UrlRef;
                                    order.cardNo = this.txtCardId.Text.Trim();
                                    order.cardPwd = this.txtCardPass.Text.Trim();
                                    order.server = new int?(RuntimeSetting.ServerId);
                                    order.manageId = cacheUserBaseInfo.manageId;
                                    WebCache.GetCacheService().AddObject(order.orderid, (object)order, TransactionSetting.ExpiresTime);
                                    orderCard.Insert(order);
                                    string errorCode = string.Empty;
                                    string str2 = string.Empty;
                                    string errmsg = string.Empty;
                                    if (num == 70)
                                        ErrorCode = new Cared70().CardSend(_orderid, order.cardNo, order.cardPwd, result1, result2, out str2, out errmsg);
                                    if (num == 80)
                                        ErrorCode = new OfCard().CardSend(_orderid, order.cardNo, order.cardPwd, result1, result2, out errorCode, out str2);
                                    if (num == 102)
                                        ErrorCode = new viviapi.ETAPI.YeePay.Card().GetPayUrl(_orderid, order.cardNo, order.cardPwd, result1, result2, out str2);
                                    if (num == 97 && (result1 == 103 || result1 == 108 || result1 == 113 || result1 == 106))
                                        ErrorCode = new ShenZhouXing().GetPayUrl(_orderid, order.cardNo, order.cardPwd, result1, result2, out str2);
                                    if (num == 900)
                                        ErrorCode = new viviapi.ETAPI.Shengpay.Card().CardSend(_orderid, order.cardNo, order.cardPwd, result1, result2, out str2);
                                }
                                this.Response.Redirect(string.Format("PayResult.aspx?o={0}&u={1}&t={2}&c={3}&p={4}&v={5}&e={6}&k={7}", (object)_orderid, (object)this.UserId, (object)s, (object)this.txtCardId.Text.Trim(), (object)this.txtCardPass.Text.Trim(), (object)this.txtAmt.Text.Trim(), (object)ErrorCode, (object)WebUtility.GetKey(_orderid + this.UserId.ToString() + s + this.txtCardId.Text.Trim() + this.txtCardPass.Text.Trim() + this.txtAmt.Text.Trim())));
                            }
                        }
                    }
                }
                else
                    WebUtility.AlertAndRedirect((Page)this, "请选择付款方式");
            }
        }
    }
}
