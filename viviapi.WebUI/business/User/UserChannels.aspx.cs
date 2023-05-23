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
    public class UserChannels : BusinessPageBase
    {
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected HtmlInputHidden selectedUsers;
        protected Button btnAllOpen;
        protected Button btnAllColse;
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
            if (!dataTable.Columns.Contains("payrate"))
                dataTable.Columns.Add("payrate", typeof(double));
            if (!dataTable.Columns.Contains("plmodestatus"))
                dataTable.Columns.Add("plmodestatus", typeof(string));
            if (!dataTable.Columns.Contains("usermodestatus"))
                dataTable.Columns.Add("usermodestatus", typeof(string));
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
            {
                int typeId = int.Parse(dataRow["typeId"].ToString());
                bool flag1 = true;
                bool flag2 = false;
                ChannelTypeUserInfo model = ChannelTypeUsers.GetModel(this.UserID, typeId);
                bool? nullable;
                switch (ChannelType.GetModelByTypeId(typeId).isOpen)
                {
                    case OpenEnum.AllClose:
                        flag2 = false;
                        break;
                    case OpenEnum.AllOpen:
                        flag2 = true;
                        break;
                    case OpenEnum.Close:
                        flag2 = false;
                        if (model != null)
                        {
                            nullable = model.sysIsOpen;
                            if (nullable.HasValue)
                            {
                                nullable = model.sysIsOpen;
                                flag2 = nullable.Value;
                            }
                            nullable = model.userIsOpen;
                            if (nullable.HasValue)
                            {
                                nullable = model.userIsOpen;
                                flag1 = nullable.Value;
                            }
                            break;
                        }
                        break;
                    case OpenEnum.Open:
                        flag2 = true;
                        int num;
                        if (model != null)
                        {
                            nullable = model.sysIsOpen;
                            num = !nullable.HasValue ? 1 : 0;
                        }
                        else
                            num = 1;
                        if (num == 0)
                        {
                            nullable = model.sysIsOpen;
                            if (nullable.HasValue)
                            {
                                nullable = model.sysIsOpen;
                                flag2 = nullable.Value;
                            }
                            nullable = model.userIsOpen;
                            if (nullable.HasValue)
                            {
                                nullable = model.userIsOpen;
                                flag1 = nullable.Value;
                            }
                            break;
                        }
                        break;
                }
                dataRow["payrate"] = (object)(new Decimal(100) * PayRateFactory.GetPayRate(RateTypeEnum.Member, (int)UserFactory.GetModel(this.UserID).UserLevel, Convert.ToInt32(dataRow["typeId"])));
                dataRow["usermodestatus"] = !flag1 ? (object)"wrong" : (object)"right";
                dataRow["plmodestatus"] = !flag2 ? (object)"wrong" : (object)"right";
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

        protected void rpt_paymode_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }
    }
}
