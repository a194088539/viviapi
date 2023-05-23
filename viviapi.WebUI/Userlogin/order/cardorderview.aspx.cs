namespace viviapi.WebUI.Userlogin.order
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.BLL.User;
    using viviapi.Model.Order;
    using viviapi.WebComponents.Web;
    using viviLib.TimeControl;
    using viviLib.Web;

    public class cardorderview : UserPageBase
    {
        protected HtmlForm form1;
        protected Label lbladdtime;
        protected Label lblattach;
        protected Label lblcardno;
        protected Label lblcardpwd;
        protected Label lblcompletetime;
        protected Label lblmessage;
        protected Label lblnotifycontext;
        protected Label lblnotifycount;
        protected Label lblnotifystat;
        protected Label lblorderid;
        protected Label lblordertype;
        protected Label lblpayAmt;
        protected Label lblpayerip;
        protected Label lblpaymodeId;
        protected Label lblpayRate;
        protected Label lblrealvalue;
        protected Label lblrefervalue;
        protected Label lblstatus;
        protected Label lbltypeId;
        protected Label lbluserorder;
        protected Label lblversion;
        protected string notifyurl = string.Empty;
        protected string referUrl = string.Empty;

        private string getChannelName(string code)
        {
            try
            {
                return viviapi.BLL.Channel.Channel.GetModelByCode(code).modeName;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string getChannelTypeName(int id)
        {
            try
            {
                return ChannelType.GetModelByTypeId(id).modetypename;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string getuserName(int uid)
        {
            try
            {
                return UserFactory.GetCacheModel(uid).UserName;
            }
            catch
            {
                return string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo(this.orderid);
            }
        }

        private void ShowInfo(string orderid)
        {
            if (!string.IsNullOrEmpty(orderid))
            {
                OrderCardInfo model = new OrderCard().GetModel(orderid);
                if (model != null)
                {
                    this.lblorderid.Text = model.orderid;
                    this.lblordertype.Text = model.ordertype.ToString();
                    this.lbltypeId.Text = this.getChannelTypeName(model.typeId);
                    this.lblpaymodeId.Text = this.getChannelName(model.paymodeId);
                    this.lbluserorder.Text = model.userorder;
                    this.lblrefervalue.Text = model.refervalue.ToString("f2");
                    if (model.realvalue.HasValue)
                    {
                        this.lblrealvalue.Text = model.realvalue.Value.ToString("f2");
                    }
                    this.referUrl = model.referUrl;
                    this.notifyurl = model.notifyurl;
                    if (!string.IsNullOrEmpty(model.againNotifyUrl))
                    {
                        this.notifyurl = model.againNotifyUrl;
                    }
                    this.lblnotifycount.Text = model.notifycount.ToString();
                    this.lblnotifystat.Text = Enum.GetName(typeof(OrderNofityStatusEnum), model.notifystat);
                    this.lblnotifycontext.Text = base.Server.HtmlEncode(model.notifycontext);
                    this.lblattach.Text = model.attach;
                    this.lblpayerip.Text = model.payerip;
                    this.lbladdtime.Text = FormatConvertor.DateTimeToTimeString(model.addtime);
                    this.lblstatus.Text = Enum.GetName(typeof(OrderStatusEnum), model.status);
                    if (model.completetime.HasValue)
                    {
                        this.lblcompletetime.Text = FormatConvertor.DateTimeToTimeString(model.completetime.Value);
                    }
                    this.lblpayRate.Text = model.payRate.ToString("p2");
                    this.lblpayAmt.Text = model.payAmt.ToString("f2");
                    this.lblcardno.Text = model.cardNo;
                    this.lblcardpwd.Text = model.cardPwd;
                    this.lblmessage.Text = model.userViewMsg;
                    string versionName = SystemApiHelper.GetVersionName(model.version);
                    if (string.IsNullOrEmpty(versionName) && (WebInfoFactory.CurrentWebInfo != null))
                    {
                        versionName = WebInfoFactory.CurrentWebInfo.apicardname + "[" + WebInfoFactory.CurrentWebInfo.apicardversion + "]";
                    }
                    this.lblversion.Text = versionName;
                }
            }
        }

        public string orderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }
    }
}

