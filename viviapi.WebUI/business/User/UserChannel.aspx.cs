using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.Payment;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.Channel;
using viviapi.Model.Payment;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviapi.WebUI.business.User
{
    public class UserChannel : BusinessPageBase
    {
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected HtmlInputHidden selectedUsers;
        protected Button btnAllOpen;
        protected Button btnAllColse;
        protected Button btnReSet;
        protected Button btnSave;
        protected Label lblInfo;
        protected Repeater rpt_paymode;
        protected HiddenField puser;

        public int UserID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public int typeId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", string.Empty);
            }
        }

        public int ajaxUserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userId", 0);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            this.DoCmd();
            if (this.IsPostBack)
                return;
            HiddenField hiddenField = this.puser;
            int userId = this.UserID;
            string str1 = userId.ToString();
            hiddenField.Value = str1;
            Label label = this.lblInfo;
            string str2 = "当前用户ID：";
            userId = this.UserID;
            string str3 = userId.ToString();
            string str4 = str2 + str3;
            label.Text = str4;
            this.LoadData();
        }

        private void DoCmd()
        {
            if (this.typeId <= 0 || string.IsNullOrEmpty(this.cmd) || this.ajaxUserId <= 0)
                return;
            ChannelTypeUserInfo model = new ChannelTypeUserInfo();
            model.userId = this.ajaxUserId;
            model.typeId = this.typeId;
            model.sysIsOpen = new bool?(!(this.cmd == "close"));
            model.addTime = new DateTime?(DateTime.Now);
            model.userIsOpen = new bool?();
            string s = "error";
            if (ChannelTypeUsers.Add(model) > 0)
                s = "success";
            this.Response.Write(s);
            this.Response.End();
        }

        private void setPower()
        {
            if (ManageFactory.CheckCurrentPermission(false, ManageRole.Merchant))
                return;
            this.Response.Write("Sorry,No authority!");
            this.Response.End();
        }

        private void LoadData()
        {
            DataTable dataTable = ChannelType.GetList(new bool?(true)).Tables[0];
            if (!dataTable.Columns.Contains("type_status"))
                dataTable.Columns.Add("type_status", typeof(string));
            if (!dataTable.Columns.Contains("sys_setting"))
                dataTable.Columns.Add("sys_setting", typeof(string));
            if (!dataTable.Columns.Contains("user_setting"))
                dataTable.Columns.Add("user_setting", typeof(string));
            if (!dataTable.Columns.Contains("payrate"))
                dataTable.Columns.Add("payrate", typeof(double));
            if (!dataTable.Columns.Contains("suppid"))
                dataTable.Columns.Add("suppid", typeof(int));
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
            {
                int typeId = int.Parse(dataRow["typeId"].ToString());
                bool flag = false;
                bool? nullable1 = new bool?();
                bool? nullable2 = new bool?();
                ChannelTypeUserInfo model = ChannelTypeUsers.GetModel(this.UserID, typeId);
                switch (ChannelType.GetModelByTypeId(typeId).isOpen)
                {
                    case OpenEnum.AllClose:
                    case OpenEnum.Close:
                        flag = false;
                        break;
                    case OpenEnum.AllOpen:
                    case OpenEnum.Open:
                        flag = true;
                        break;
                }
                dataRow["type_status"] = flag ? (object)"right" : (object)"wrong";
                dataRow["sys_setting"] = (object)"Unknown";
                dataRow["user_setting"] = (object)"Unknown";
                dataRow["suppid"] = (object)0;
                if (model != null)
                {
                    if (model.sysIsOpen.HasValue)
                        dataRow["sys_setting"] = model.sysIsOpen.Value ? (object)"right" : (object)"wrong";
                    if (model.userIsOpen.HasValue)
                        dataRow["user_setting"] = model.userIsOpen.Value ? (object)"right" : (object)"wrong";
                    if (model.suppid.HasValue)
                        dataRow["suppid"] = (object)model.suppid.Value;
                }
                dataRow["payrate"] = (object)(new Decimal(100) * PayRateFactory.GetPayRate(RateTypeEnum.Member, (int)UserFactory.GetModel(this.UserID).UserLevel, Convert.ToInt32(dataRow["typeId"])));
            }
            this.rpt_paymode.DataSource = (object)dataTable;
            this.rpt_paymode.DataBind();
        }

        protected void btnAllOpen_Click(object sender, EventArgs e)
        {
            ChannelTypeUsers.Setting(this.UserID, 1);
            this.LoadData();
        }

        protected void btnAllColse_Click(object sender, EventArgs e)
        {
            ChannelTypeUsers.Setting(this.UserID, 0);
            this.LoadData();
        }

        protected void btnReSet_Click(object sender, EventArgs e)
        {
            ChannelTypeUsers.Setting(this.UserID, 3);
            this.LoadData();
        }

        protected void rpt_paymode_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;
            DataRowView dataRowView = e.Item.DataItem as DataRowView;
            int typeId = int.Parse(dataRowView["typeId"].ToString());
            ChannelTypeUserInfo model = ChannelTypeUsers.GetModel(this.UserID, typeId);
            if (model != null && model.sysIsOpen.HasValue)
            {
                Button button1 = e.Item.FindControl("btn_open") as Button;
                Button button2 = e.Item.FindControl("btn_close") as Button;
                Button button3 = button1;
                bool? sysIsOpen = model.sysIsOpen;
                int num1 = !sysIsOpen.Value ? 1 : 0;
                button3.Enabled = num1 != 0;
                Button button4 = button2;
                sysIsOpen = model.sysIsOpen;
                int num2 = sysIsOpen.Value ? 1 : 0;
                button4.Enabled = num2 != 0;
            }
            DropDownList ddlctrl = e.Item.FindControl("ddlsupp") as DropDownList;
            if (ddlctrl != null)
            {
                ddlctrl.Visible = typeId == 102;
                if (typeId == 102)
                {
                    int suppId = int.Parse(dataRowView["suppid"].ToString());
                    this.bind(ddlctrl, suppId);
                }
            }
        }

        private void bind(DropDownList ddlctrl, int suppId)
        {
            DataTable dataTable = SupplierFactory.GetList("isbank=1").Tables[0];
            ddlctrl.Items.Add(new ListItem("--默认--", "0"));
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                ddlctrl.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
            ddlctrl.SelectedValue = suppId.ToString();
        }

        protected void rpt_paymode_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ChannelTypeUserInfo model = new ChannelTypeUserInfo();
            model.updateTime = new DateTime?(DateTime.Now);
            model.typeId = int.Parse(e.CommandArgument.ToString());
            model.updateTime = new DateTime?(DateTime.Now);
            model.userId = this.UserID;
            model.userIsOpen = new bool?();
            if (e.CommandName == "open")
                model.sysIsOpen = new bool?(true);
            else if (e.CommandName == "close")
                model.sysIsOpen = new bool?(false);
            ChannelTypeUsers.Add(model);
            this.LoadData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem repeaterItem in this.rpt_paymode.Items)
            {
                if (repeaterItem.ItemType == ListItemType.Item || repeaterItem.ItemType == ListItemType.AlternatingItem)
                {
                    HiddenField hiddenField = repeaterItem.FindControl("hftypeId") as HiddenField;
                    if (hiddenField != null)
                    {
                        int num = Convert.ToInt32(hiddenField.Value);
                        if (num == 102)
                        {
                            DropDownList dropDownList = repeaterItem.FindControl("ddlsupp") as DropDownList;
                            if (dropDownList != null)
                                ChannelTypeUsers.AddSupp(new ChannelTypeUserInfo()
                                {
                                    updateTime = new DateTime?(DateTime.Now),
                                    typeId = num,
                                    userId = this.UserID,
                                    suppid = new int?(int.Parse(dropDownList.SelectedValue))
                                });
                        }
                    }
                }
            }
            this.LoadData();
        }
    }
}
