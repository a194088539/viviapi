namespace viviapi.WebUI.business.User
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.BLL.Payment;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class UserList : BusinessPageBase
    {
        protected Button btn_allgetmoney;
        protected Button btn_Msg;
        protected Button btnAdd;
        protected Button btnCashTo;
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList ddlisSpecialPayRate;
        protected DropDownList ddlpayrate;
        protected DropDownList ddlSpecial;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptUsers;
        protected HtmlInputHidden selectedUsers;
        protected DropDownList StatusList;
        protected TextBox txtagent;
        protected TextBox txtfullname;
        protected TextBox txtMail;
        protected TextBox txtQQ;
        protected TextBox txtTel;
        protected TextBox txtuserId;
        protected TextBox txtuserName;
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

        protected void btn_Msg_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["chkItem"];
            if (string.IsNullOrEmpty(str))
            {
                base.AlertAndRedirect("选择商户");
            }
            else
            {
                base.Response.Redirect("SendMsg.aspx?uid=" + str);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UserAdd.aspx", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = base.Request.Form["chkItem"];
                foreach (string str2 in str.Split(new char[] { ',' }))
                {
                    UserFactory.Del(int.Parse(str2));
                }
            }
            catch
            {
            }
            this.LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void DoCmd()
        {
            if (!string.IsNullOrEmpty(this.cmd) && (this.UserID > 0))
            {
                List<UsersUpdateLog> changeList = new List<UsersUpdateLog>();
                UserInfo model = UserFactory.GetModel(this.UserID);
                if (this.cmd == "ok")
                {
                    changeList.Add(this.newUpdateLog("Status", model.Status.ToString(), "2", "审核"));
                    model.Status = 2;
                }
                else if (this.cmd == "del")
                {
                    changeList.Add(this.newUpdateLog("Status", model.Status.ToString(), "4", "锁定"));
                    model.Status = 4;
                }
                else if (this.cmd == "pok")
                {
                    changeList.Add(this.newUpdateLog("UserType", ((int)model.UserType).ToString(), "2", "设为代理"));
                    changeList.Add(this.newUpdateLog("UserLevel", ((int)model.UserLevel).ToString(), "1", "设为代理"));
                    model.UserType = UserTypeEnum.代理;
                    model.UserLevel = UserLevelEnum.普通代理;
                }
                else if (this.cmd == "pdel")
                {
                    changeList.Add(this.newUpdateLog("UserType", ((int)model.UserType).ToString(), "1", "取消代理"));
                    changeList.Add(this.newUpdateLog("UserLevel", ((int)model.UserLevel).ToString(), "100", "取消代理"));
                    model.UserType = UserTypeEnum.会员;
                    model.UserLevel = UserLevelEnum.初级代理;
                }
                if (UserFactory.Update(model, changeList))
                {
                    base.AlertAndRedirect("操作成功", "UserList.aspx");
                }
                else
                {
                    base.AlertAndRedirect("操作失败");
                }
            }
        }

        protected string getpassview(object obj)
        {
            if ((obj == null) || (obj == DBNull.Value))
            {
                return string.Empty;
            }
            if (Convert.ToInt32(obj) > 0)
            {
                return "√";
            }
            return "\x00d7";
        }

        public string isSpecialChannel(object userid)
        {
            if (ChannelTypeUsers.Exists(Convert.ToInt32(userid)) == 1)
            {
                return "(独)";
            }
            return string.Empty;
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!base.isSuperAdmin)
            {
                searchParams.Add(new SearchParam("manageId", base.ManageId));
            }
            else if (this.manageid > 0)
            {
                searchParams.Add(new SearchParam("manageId", this.manageid));
            }
            if (this.proid > 0)
            {
                searchParams.Add(new SearchParam("proid", this.proid));
            }
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlisSpecialPayRate.SelectedValue))
            {
                searchParams.Add(new SearchParam("special", int.Parse(this.ddlisSpecialPayRate.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.txtuserName.Text.Trim()))
            {
                searchParams.Add(new SearchParam("userName", this.txtuserName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtuserId.Text.Trim()))
            {
                int result = 0;
                if (int.TryParse(this.txtuserId.Text.Trim(), out result))
                {
                    searchParams.Add(new SearchParam("id", result));
                }
            }
            if (!string.IsNullOrEmpty(this.ddlpayrate.SelectedValue))
            {
                searchParams.Add(new SearchParam("userlevel", int.Parse(this.ddlpayrate.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.txtQQ.Text.Trim()))
            {
                searchParams.Add(new SearchParam("qq", this.txtQQ.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtMail.Text.Trim()))
            {
                searchParams.Add(new SearchParam("email", this.txtMail.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtTel.Text.Trim()))
            {
                searchParams.Add(new SearchParam("tel", this.txtTel.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtfullname.Text.Trim()))
            {
                searchParams.Add(new SearchParam("full_name", this.txtfullname.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.ddlSpecial.SelectedValue))
            {
                searchParams.Add(new SearchParam("specialchannel", this.ddlSpecial.SelectedValue));
            }
            string orderby = this.orderBy + " " + this.orderByType;
            DataSet set = UserFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = set.Tables[1];
            this.rptUsers.DataBind();
        }

        private UsersUpdateLog newUpdateLog(string f, string n, string o, string desc)
        {
            UsersUpdateLog log = new UsersUpdateLog();
            log.userid = this.UserID;
            log.Addtime = DateTime.Now;
            log.field = f;
            log.newvalue = n;
            log.oldValue = o;
            log.Editor = ManageFactory.CurrentManage.username;
            log.OIp = ServerVariables.TrueIP;
            log.Desc = desc;
            return log;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            this.DoCmd();
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.UserStatus))
                {
                    this.StatusList.SelectedValue = this.UserStatus;
                }
                DataTable userlevData = PayRateFactory.GetUserlevData();
                foreach (DataRow row in userlevData.Rows)
                {
                    this.ddlpayrate.Items.Add(new ListItem(row["levName"].ToString(), row["userLevel"].ToString()));
                }
                if (this.proid > 0)
                {
                    this.txtagent.Text = this.proid.ToString();
                }
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                string orderBy = this.orderBy;
                if ((orderBy != null) && (orderBy == "balance"))
                {
                    HyperLink link = (HyperLink)e.Item.FindControl("hlinkOrderby");
                    if (this.orderByType == "asc")
                    {
                        link.Text = "余额↓";
                        link.NavigateUrl = "?orderby=balance&type=desc";
                    }
                    else
                    {
                        link.Text = "余额↑";
                        link.NavigateUrl = "?orderby=balance&type=asc";
                    }
                }
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string s = DataBinder.Eval(e.Item.DataItem, "userType").ToString();
                string str2 = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string str3 = DataBinder.Eval(e.Item.DataItem, "levName").ToString();
                string str4 = DataBinder.Eval(e.Item.DataItem, "settles").ToString();
                string str5 = DataBinder.Eval(e.Item.DataItem, "manageId").ToString();
                Label label = (Label)e.Item.FindControl("lblUserType");
                label.Text = Enum.GetName(typeof(UserTypeEnum), int.Parse(s));
                Label label2 = (Label)e.Item.FindControl("lblUserStat");
                label2.Text = Enum.GetName(typeof(UserStatusEnum), int.Parse(str2));
                Label label3 = (Label)e.Item.FindControl("lblUserLevel");
                label3.Text = str3;
                Label label4 = (Label)e.Item.FindControl("lblUserSettle");
                label4.Text = "T+" + str4;
                string str6 = DataBinder.Eval(e.Item.DataItem, "id").ToString();
                string relname = string.Empty;
                switch (str2)
                {
                    case "1":
                        relname = string.Format("<a onclick=\"return confirm('你确定要通过该用户吗？')\" href=\"?cmd=ok&ID={0}\" style=\"color:Green;\">通过</a> <a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>", str6);
                        break;

                    case "2":
                        relname = string.Format("<a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>", str6);
                        break;

                    case "4":
                        relname = string.Format("<a onclick=\"return confirm('你确定要恢复该用户吗？')\" href=\"?cmd=ok&ID={0}\">恢复</a>", str6);
                        break;
                }
                Label label5 = (Label)e.Item.FindControl("labcmd");
                label5.Text = relname;
                relname = string.Empty;
                if (!string.IsNullOrEmpty(str5))
                {
                    Manage model = ManageFactory.GetModel(int.Parse(str5));
                    if (model != null)
                    {
                        relname = model.relname;
                    }
                }
                Label label6 = (Label)e.Item.FindControl("labagcmd");
                label6.Text = relname;
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

        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public int manageid
        {
            get
            {
                return WebBase.GetQueryStringInt32("manageid", 0);
            }
        }

        public string orderBy
        {
            get
            {
                return WebBase.GetQueryStringString("orderby", "balance");
            }
        }

        public string orderByType
        {
            get
            {
                return WebBase.GetQueryStringString("type", "asc");
            }
        }

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("agentid", 0);
            }
        }

        public int UserID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public string UserStatus
        {
            get
            {
                return WebBase.GetQueryStringString("UserStatus", "");
            }
        }
    }
}

