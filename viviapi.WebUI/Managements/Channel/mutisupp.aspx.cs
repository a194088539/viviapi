namespace viviapi.WebUI.Managements.channel
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class mutisupp : ManagePageBase
    {
        public ChannelTypeInfo _ItemInfo = null;
        protected Button btnsave;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HiddenField hftypeid;
        protected Repeater rptsupp;
        protected TextBox txttypename;

        protected void btnsave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in this.rptsupp.Items)
            {
                if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                {
                    HtmlInputCheckBox box = item.FindControl("chkItem") as HtmlInputCheckBox;
                    HtmlInputCheckBox box2 = item.FindControl("chkisdefault") as HtmlInputCheckBox;
                    HiddenField field = item.FindControl("hfsuppid") as HiddenField;
                    TextBox box3 = item.FindControl("txtpayrate") as TextBox;
                    if (((box != null) && (field != null)) && (box3 != null))
                    {
                        decimal result = 0M;
                        decimal.TryParse(box3.Text.Trim(), out result);
                        ChannelSupplier model = new ChannelSupplier();
                        model.userid = 0;
                        model.typeid = this.ItemInfo.typeId;
                        model.suppid = int.Parse(field.Value);
                        model.payrate = result / 100M;
                        model.isopen = box.Checked;
                        model.isdefault = box2.Checked;
                        Channelsupplier.Insert(model);
                    }
                }
            }
            base.AlertAndRedirect("设置成功", "ChannelTypeList.aspx");
        }

        private void LoadData()
        {
            DataSet list = Channelsupplier.GetList(this.ItemInfo.typeId, 0);
            this.rptsupp.DataSource = list;
            this.rptsupp.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.txttypename.Text = this.ItemInfo.modetypename;
                this.hftypeid.Value = this.ItemInfo.typeId.ToString();
                this.LoadData();
            }
        }

        protected void rptsupp_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void rptsupp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                HtmlInputCheckBox box = e.Item.FindControl("chkItem") as HtmlInputCheckBox;
                bool flag = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "isopen"));
                box.Checked = flag;
                HtmlInputCheckBox box2 = e.Item.FindControl("chkisdefault") as HtmlInputCheckBox;
                bool flag2 = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "isdefault"));
                box2.Checked = flag2;
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public ChannelTypeInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = ChannelType.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new ChannelTypeInfo();
                    }
                }
                return this._ItemInfo;
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
    }
}

