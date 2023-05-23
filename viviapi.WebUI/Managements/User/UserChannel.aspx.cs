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

namespace viviapi.WebUI.Managements.User
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
            if (!base.IsPostBack)
            {
                this.puser.Value = this.UserID.ToString();
                this.lblInfo.Text = "当前用户ID：" + this.UserID.ToString();
                this.LoadData();
            }
        }

        private void DoCmd()
        {
            if (this.typeId > 0 && !string.IsNullOrEmpty(this.cmd) && this.ajaxUserId > 0)
            {
                ChannelTypeUserInfo channelTypeUserInfo = new ChannelTypeUserInfo();
                channelTypeUserInfo.userId = this.ajaxUserId;
                channelTypeUserInfo.typeId = this.typeId;
                channelTypeUserInfo.sysIsOpen = new bool?(!(this.cmd == "close"));
                channelTypeUserInfo.addTime = new DateTime?(DateTime.Now);
                channelTypeUserInfo.userIsOpen = null;
                string s = "error";
                if (ChannelTypeUsers.Add(channelTypeUserInfo) > 0)
                {
                    s = "success";
                }
                base.Response.Write(s);
                base.Response.End();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Merchant))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void LoadData()
        {
            DataTable dataTable = ChannelType.GetList(new bool?(true)).Tables[0];
            if (!dataTable.Columns.Contains("type_status"))
            {
                dataTable.Columns.Add("type_status", typeof(string));
            }
            if (!dataTable.Columns.Contains("sys_setting"))
            {
                dataTable.Columns.Add("sys_setting", typeof(string));
            }
            if (!dataTable.Columns.Contains("user_setting"))
            {
                dataTable.Columns.Add("user_setting", typeof(string));
            }
            if (!dataTable.Columns.Contains("payrate"))
            {
                dataTable.Columns.Add("payrate", typeof(double));
            }
            if (!dataTable.Columns.Contains("suppid"))
            {
                dataTable.Columns.Add("suppid", typeof(int));
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                int typeId = int.Parse(dataRow["typeId"].ToString());
                bool flag = false;
                bool? flag2 = null;
                bool? flag3 = null;
                ChannelTypeUserInfo model = ChannelTypeUsers.GetModel(this.UserID, typeId);
                ChannelTypeInfo modelByTypeId = ChannelType.GetModelByTypeId(typeId);
                OpenEnum isOpen = modelByTypeId.isOpen;
                switch (isOpen)
                {
                    case OpenEnum.AllClose:
                    case OpenEnum.Close:
                        flag = false;
                        break;
                    case OpenEnum.AllOpen:
                        goto IL_199;
                    case (OpenEnum)3:
                        break;
                    default:
                        if (isOpen == OpenEnum.Open)
                        {
                            goto IL_199;
                        }
                        break;
                }
            IL_19D:
                dataRow["type_status"] = (flag ? "right" : "wrong");
                dataRow["sys_setting"] = "Unknown";
                dataRow["user_setting"] = "Unknown";
                dataRow["suppid"] = 0;
                if (model != null)
                {
                    if (model.sysIsOpen.HasValue)
                    {
                        dataRow["sys_setting"] = (model.sysIsOpen.Value ? "right" : "wrong");
                    }
                    if (model.userIsOpen.HasValue)
                    {
                        dataRow["user_setting"] = (model.userIsOpen.Value ? "right" : "wrong");
                    }
                    if (model.suppid.HasValue)
                    {
                        dataRow["suppid"] = model.suppid.Value;
                    }
                }
                dataRow["payrate"] = 100m * PayRateFactory.GetPayRate(RateTypeEnum.Member, (int)UserFactory.GetModel(this.UserID).UserLevel, Convert.ToInt32(dataRow["typeId"]));
                continue;
            IL_199:
                flag = true;
                goto IL_19D;
            }
            this.rpt_paymode.DataSource = dataTable;
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dataRowView = e.Item.DataItem as DataRowView;
                int typeId = int.Parse(dataRowView["typeId"].ToString());
                ChannelTypeUserInfo model = ChannelTypeUsers.GetModel(this.UserID, typeId);
                if (model != null)
                {
                    if (model.sysIsOpen.HasValue)
                    {
                        Button button = e.Item.FindControl("btn_open") as Button;
                        Button button2 = e.Item.FindControl("btn_close") as Button;
                        button.Enabled = !model.sysIsOpen.Value;
                        button2.Enabled = model.sysIsOpen.Value;
                    }
                }
                DropDownList dropDownList = e.Item.FindControl("ddlsupp") as DropDownList;
                if (dropDownList != null)
                {
                    int suppId = int.Parse(dataRowView["suppid"].ToString());
                    this.bind(dropDownList, suppId);
                }
            }
        }

        private void bind(DropDownList ddlctrl, int suppId)
        {
            string where = "";
            DataTable dataTable = SupplierFactory.GetList(where).Tables[0];
            ddlctrl.Items.Add(new ListItem("--默认--", "0"));
            foreach (DataRow dataRow in dataTable.Rows)
            {
                ddlctrl.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
            }
            ddlctrl.SelectedValue = suppId.ToString();
        }

        protected void rpt_paymode_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ChannelTypeUserInfo channelTypeUserInfo = new ChannelTypeUserInfo();
            channelTypeUserInfo.updateTime = new DateTime?(DateTime.Now);
            channelTypeUserInfo.typeId = int.Parse(e.CommandArgument.ToString());
            channelTypeUserInfo.updateTime = new DateTime?(DateTime.Now);
            channelTypeUserInfo.userId = this.UserID;
            channelTypeUserInfo.userIsOpen = null;
            if (e.CommandName == "open")
            {
                channelTypeUserInfo.sysIsOpen = new bool?(true);
            }
            else if (e.CommandName == "close")
            {
                channelTypeUserInfo.sysIsOpen = new bool?(false);
            }
            ChannelTypeUsers.Add(channelTypeUserInfo);
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
                        int typeId = Convert.ToInt32(hiddenField.Value);
                        DropDownList dropDownList = repeaterItem.FindControl("ddlsupp") as DropDownList;
                        if (dropDownList != null)
                        {
                            ChannelTypeUsers.AddSupp(new ChannelTypeUserInfo
                            {
                                updateTime = new DateTime?(DateTime.Now),
                                typeId = typeId,
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
