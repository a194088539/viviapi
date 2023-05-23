namespace viviapi.WebUI.business.User
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class PromUserList : BusinessPageBase
    {
        protected Button btn_allgetmoney;
        protected Button btnCashTo;
        protected Button btnDelete;
        protected Button btnSearch;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected TextBox KeyWordBox;
        protected AspNetPager Pager1;
        protected Repeater rptUsers;
        protected DropDownList SeachType;
        protected HtmlInputHidden selectedUsers;
        protected DropDownList StatusList;
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

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
                    base.AlertAndRedirect("操作成功", "PromUserList.aspx");
                }
                else
                {
                    base.AlertAndRedirect("操作失败");
                }
            }
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userType", 2));
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            }
            string str = this.KeyWordBox.Text.Trim();
            if (!string.IsNullOrEmpty(this.SeachType.SelectedValue) && !string.IsNullOrEmpty(str))
            {
                if (this.SeachType.SelectedValue.ToLower() == "userid")
                {
                    int result = 0;
                    int.TryParse(str, out result);
                    searchParams.Add(new SearchParam("id", result));
                }
                else if (this.SeachType.SelectedValue == "UserName")
                {
                    searchParams.Add(new SearchParam("userName", str));
                }
            }
            string orderby = this.orderBy + " " + this.orderByType;
            DataSet set = UserFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            DataTable table = set.Tables[1];
            DataTable list = WebInfoFactory.GetList(string.Empty);
            table.Columns.Add("Mystr");
            table.Columns.Add("agstr");
            table.Columns.Add("prourl");
            foreach (DataRow row in table.Rows)
            {
                foreach (DataRow row2 in list.Rows)
                {
                    if (int.Parse(row["ID"].ToString()) == int.Parse(row2["ID"].ToString()))
                    {
                        row["prourl"] = row2["Domain"].ToString();
                    }
                }
            }
            this.rptUsers.DataSource = table;
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
            }
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string s = DataBinder.Eval(e.Item.DataItem, "userType").ToString();
                string str2 = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string str3 = DataBinder.Eval(e.Item.DataItem, "userLevel").ToString();
                string str4 = DataBinder.Eval(e.Item.DataItem, "id").ToString();
                Label label = (Label)e.Item.FindControl("lblUserType");
                label.Text = Enum.GetName(typeof(UserTypeEnum), int.Parse(s));
                Label label2 = (Label)e.Item.FindControl("lblUserStat");
                label2.Text = Enum.GetName(typeof(UserStatusEnum), int.Parse(str2));
                Label label3 = (Label)e.Item.FindControl("lblUserLevel");
                label3.Text = Enum.GetName(typeof(UserLevelEnum), int.Parse(str3));
                string str5 = string.Empty;
                switch (str2)
                {
                    case "1":
                        str5 = string.Format("<a onclick=\"return confirm('你确定要通过该用户吗？')\" href=\"?cmd=ok&ID={0}\" style=\"color:Green;\">通过</a> <a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>", str4);
                        break;

                    case "2":
                        str5 = string.Format("<a onclick=\"return confirm('你确定要锁定该用户吗？')\" href=\"?cmd=del&ID={0}\" style=\"color:red;\">锁定</a>", str4);
                        break;

                    case "4":
                        str5 = string.Format("<a onclick=\"return confirm('你确定要恢复该用户吗？')\" href=\"?cmd=ok&ID={0}\">恢复</a>", str4);
                        break;
                }
                Label label4 = (Label)e.Item.FindControl("labcmd");
                label4.Text = str5;
                str5 = string.Empty;
                if (s == "1")
                {
                    str5 = string.Format(" <a onclick=\"return confirm('你确定要将该用户设为代理吗？')\" href=\"?cmd=pok&ID={0}\" style=\"color:red;\">设为代理</a>", str4);
                }
                else if (s == "2")
                {
                    str5 = string.Format("<a onclick=\"return confirm('你确定要取消该用户的代理权限吗？')\" href=\"?cmd=pdel&ID={0}\" style=\"color:red;\">取消代理权限</a>", str4);
                }
                Label label5 = (Label)e.Item.FindControl("labagcmd");
                label5.Text = str5;
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

