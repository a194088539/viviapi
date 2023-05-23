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

    public class CardOrderShow : ManagePageBase
    {
        protected HtmlForm form1;
        protected Label lbladdtime;
        protected Label lblattach;
        protected Label lblcardfaceval;
        protected Label lblcardno;
        protected Label lblcardNum;
        protected Label lblcardpwd;
        protected Label lblclientip;
        protected Label lblcompletetime;
        protected Label lblid;
        protected Label lblmakeup;
        protected Label lblnotifycontext;
        protected Label lblnotifycount;
        protected Label lblnotifystat;
        protected Label lblnotifyurl;
        protected Label lblorderid;
        protected Label lblordertype;
        protected Label lblpayAmt;
        protected Label lblpayerip;
        protected Label lblpaymodeId;
        protected Label lblpayRate;
        protected Label lblprofits;
        protected Label lblpromAmt;
        protected Label lblpromRate;
        protected Label lblrealvalue;
        protected Label lblreferUrl;
        protected Label lblrefervalue;
        protected Label lblreturnurl;
        protected Label lblserver;
        protected Label lblsmsg;
        protected Label lblstatus;
        protected Label lblsupplierAmt;
        protected Label lblsupplierId;
        protected Label lblsupplierOrder;
        protected Label lblsupplierRate;
        protected Label lbltypeId;
        protected Label lbluserid;
        protected Label lbluserorder;
        protected Label lblversion;
        protected Label lblwithhold;
        protected Literal litNotify;
        protected Label litreback;

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

        public string Getmakeup(byte makeup)
        {
            if ((makeup != 0) && (makeup == 1))
            {
                return "系统补充";
            }
            return "";
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

        public string GetWithholdType(byte withhold)
        {
            if (withhold == 0)
            {
                return "正常";
            }
            if (withhold == 1)
            {
                return "大提小";
            }
            if (withhold == 2)
            {
                return "小提大";
            }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo(this.Id);
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
                OrderCardInfo model = new OrderCard().GetModel(this.Id);
                if (model != null)
                {
                    this.lblid.Text = this.Id.ToString();
                    this.lblorderid.Text = model.orderid;
                    this.lblordertype.Text = model.ordertype.ToString();
                    this.lblsmsg.Text = model.msg;
                    this.lbluserid.Text = model.userid.ToString() + " (" + this.getuserName(model.userid) + ")";
                    this.lbltypeId.Text = this.getChannelTypeName(model.typeId);
                    this.lblpaymodeId.Text = this.getChannelName(model.paymodeId);
                    this.lbluserorder.Text = model.userorder;
                    this.lblrefervalue.Text = model.refervalue.ToString("f2");
                    if (model.realvalue.HasValue)
                    {
                        this.lblrealvalue.Text = model.realvalue.Value.ToString("f2");
                    }
                    this.lblcardfaceval.Text = model.faceValue.ToString("f2");
                    this.lblnotifyurl.Text = model.notifyurl;
                    if (!string.IsNullOrEmpty(model.againNotifyUrl))
                    {
                        this.litNotify.Text = string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", model.againNotifyUrl);
                    }
                    this.lblnotifycount.Text = model.notifycount.ToString();
                    this.lblnotifystat.Text = Enum.GetName(typeof(OrderNofityStatusEnum), model.notifystat);
                    this.lblnotifycontext.Text = model.notifycontext;
                    this.lblreturnurl.Text = model.returnurl;
                    this.lblattach.Text = model.attach;
                    this.lblpayerip.Text = model.payerip;
                    this.lblclientip.Text = model.clientip;
                    this.lblreferUrl.Text = model.referUrl;
                    this.lbladdtime.Text = FormatConvertor.DateTimeToTimeString(model.addtime);
                    this.lblsupplierId.Text = WebUtility.GetsupplierName(model.supplierId);
                    this.lblsupplierOrder.Text = model.supplierOrder;
                    this.lblstatus.Text = Enum.GetName(typeof(OrderStatusEnum), model.status);
                    if (model.completetime.HasValue)
                    {
                        this.lblcompletetime.Text = FormatConvertor.DateTimeToTimeString(model.completetime.Value);
                    }
                    this.lblpayRate.Text = model.payRate.ToString("p2");
                    this.lblsupplierRate.Text = model.supplierRate.ToString("p2");
                    this.lblpromRate.Text = model.promRate.ToString("p2");
                    this.lblpayAmt.Text = model.payAmt.ToString("f2");
                    this.lblpromAmt.Text = model.promAmt.ToString("f2");
                    this.lblsupplierAmt.Text = model.supplierAmt.ToString("f2");
                    this.lblprofits.Text = model.profits.ToString("f2");
                    this.lblserver.Text = model.server.ToString();
                    this.lblcardno.Text = model.cardNo;
                    this.lblcardpwd.Text = model.cardPwd;
                    this.lblwithhold.Text = this.GetWithholdType(model.withhold_type);
                    this.lblmakeup.Text = this.Getmakeup(model.makeup);
                    string versionName = SystemApiHelper.GetVersionName(model.version);
                    if (string.IsNullOrEmpty(versionName))
                    {
                        versionName = "龙宝标准接口";
                    }
                    if (model.version == SystemApiHelper.vcmyapi20)
                    {
                        versionName = "龙宝标准接口v2.0";
                    }
                    this.lblversion.Text = versionName;
                    if (model.ismulticard == 1)
                    {
                        this.lblversion.Text = this.lblversion.Text + "多卡";
                    }
                    else
                    {
                        this.lblversion.Text = this.lblversion.Text + "单卡";
                    }
                    this.lblcardNum.Text = model.cardnum.ToString();
                    this.litreback.Text = string.Format("opstate:{0} ovalue:{1} ", model.opstate, model.ovalue);
                }
            }
        }

        public long Id
        {
            get
            {
                return WebBase.GetQueryStringInt64("id", 0L);
            }
        }
    }
}

