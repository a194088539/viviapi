namespace viviapi.WebUI.Managements.Order
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.Order;
    using viviapi.WebComponents.Web;
    using viviapi.WebUI;
    using viviLib.TimeControl;
    using viviLib.Web;

    public class SmsOrderShow : ManagePageBase
    {
        protected HtmlForm form1;
        protected Label lbladdtime;
        protected Label lblattach;
        protected Label lblclientip;
        protected Label lblcompletetime;
        protected Label lbldescnum;
        protected Label lblid;
        protected Label lblmod;
        protected Label lblnotifycontext;
        protected Label lblnotifycount;
        protected Label lblnotifystat;
        protected Label lblnotifyurl;
        protected Label lblorderid;
        protected Label lblordertype;
        protected Label lblpayAmt;
        protected Label lblpayerip;
        protected Label lblpayRate;
        protected Label lblprofits;
        protected Label lblpromAmt;
        protected Label lblpromRate;
        protected Label lblrealvalue;
        protected Label lblreferUrl;
        protected Label lblrefervalue;
        protected Label lblreturnurl;
        protected Label lblserver;
        protected Label lblstatus;
        protected Label lblsupplierAmt;
        protected Label lblsupplierId;
        protected Label lblsupplierOrder;
        protected Label lblsupplierRate;
        protected Label lbluserid;
        protected Label lbluserorder;
        protected Literal litNotify;

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
            this.setPower();
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo((long)this.Id);
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Orders))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo(long id)
        {
            if (this.Id > 0L)
            {
                OrderSmsInfo model = new OrderSms().GetModel(this.Id);
                if (model != null)
                {
                    if (base.currentManage.isSuperAdmin <= 0)
                    {
                    }
                    this.lblid.Text = this.Id.ToString();
                    this.lblorderid.Text = model.orderid;
                    this.lbluserid.Text = model.userid.ToString() + " (" + this.getuserName(model.userid) + ")";
                    this.lblmod.Text = model.mobile;
                    this.lbldescnum.Text = model.servicenum;
                    this.lblattach.Text = model.message;
                    this.lbluserorder.Text = model.userorder;
                    this.lblrefervalue.Text = model.fee.ToString("f2");
                    this.lblnotifyurl.Text = model.notifyurl;
                    if (!string.IsNullOrEmpty(model.againNotifyUrl))
                    {
                        this.litNotify.Text = string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", model.againNotifyUrl);
                    }
                    this.lblnotifycount.Text = model.notifycount.ToString();
                    this.lblnotifystat.Text = Enum.GetName(typeof(OrderNofityStatusEnum), model.notifystat);
                    this.lblnotifycontext.Text = base.Server.HtmlEncode(model.notifycontext);
                    this.lbladdtime.Text = FormatConvertor.DateTimeToTimeString(model.addtime);
                    this.lblsupplierId.Text = WebUtility.GetsupplierName(model.supplierId);
                    this.lblsupplierOrder.Text = model.linkid;
                    this.lblstatus.Text = Enum.GetName(typeof(OrderStatusEnum), model.status);
                    this.lblcompletetime.Text = FormatConvertor.DateTimeToTimeString(model.completetime);
                    this.lblpayRate.Text = model.payRate.ToString("p2");
                    this.lblsupplierRate.Text = model.supplierRate.ToString("p2");
                    this.lblpromRate.Text = model.promRate.ToString("p2");
                    this.lblpayAmt.Text = model.payAmt.ToString("f2");
                    this.lblpromAmt.Text = model.promAmt.ToString("f2");
                    this.lblsupplierAmt.Text = model.supplierAmt.ToString("f2");
                    this.lblprofits.Text = model.profits.ToString("f2");
                    this.lblserver.Text = model.server.ToString();
                }
            }
        }

        public int Id
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
    }
}

