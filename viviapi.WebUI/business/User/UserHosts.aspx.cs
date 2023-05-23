namespace viviapi.WebUI.business.User
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Sys;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Security;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class UserHosts : BusinessPageBase
    {
        protected Button btnClose;
        protected Button btnDelete;
        protected Button btnOpen;
        protected Button btnSearch;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptIamges;
        protected HtmlInputHidden selectedUsers;
        protected DropDownList StatusList;
        protected TextBox txtUserId;
        protected TextBox txtUserName;

        protected void btnClose_Click(object sender, EventArgs e)
        {
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = base.Request.Form["chkItem"];
                foreach (string str2 in str.Split(new char[] { ',' }))
                {
                    new userHost().Delete(int.Parse(str2));
                }
            }
            catch
            {
            }
            this.LoadData();
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void DoCmd()
        {
            if (!string.IsNullOrEmpty(this.cmd) && (this.ItemID > 0))
            {
                int status = 1;
                if (this.cmd == "close")
                {
                    status = 2;
                }
                userHost host = new userHost();
                if (host.ChangeStatus(this.ItemID, status))
                {
                    base.AlertAndRedirect("操作成功", "UserHosts.aspx");
                }
                else
                {
                    base.AlertAndRedirect("操作失败");
                }
            }
        }

        protected string GetPaymentUrl(object id)
        {
            if ((id == null) || (id == DBNull.Value))
            {
                return string.Empty;
            }
            return string.Format(WebInfoFactory.CurrentWebInfo.PayUrl + "links/CheckPay.aspx?h={0}&k={1}", id, Cryptography.MD5(id.ToString() + Constant.ParameterEncryptionKey));
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            }
            int result = 0;
            if (int.TryParse(this.txtUserId.Text, out result) && (result > 0))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if (!string.IsNullOrEmpty(this.txtUserName.Text))
            {
                searchParams.Add(new SearchParam("userName", this.txtUserName.Text));
            }
            string orderby = string.Empty;
            DataSet set = userHost.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptIamges.DataSource = set.Tables[1];
            this.rptIamges.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            this.DoCmd();
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.ItemStatus))
                {
                    this.StatusList.SelectedValue = this.ItemStatus;
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
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string str = DataBinder.Eval(e.Item.DataItem, "id").ToString();
                string str2 = DataBinder.Eval(e.Item.DataItem, "userid").ToString();
                string str3 = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string str4 = string.Empty;
                switch (str3)
                {
                    case "1":
                        str4 = string.Format("<a onclick=\"return confirm('关闭？')\" href=\"?cmd=close&ID={0}&userid={1}\" style=\"color:red;\">关闭</a>", str, str2);
                        break;

                    case "2":
                        str4 = string.Format("<a onclick=\"return confirm('开启?')\" href=\"?cmd=open&ID={0}&userid={1}\" style=\"color:Green;\">开启</a>", str, str2);
                        break;
                }
                Label label = (Label)e.Item.FindControl("labagcmd");
                label.Text = str4;
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

        public int ItemID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public string ItemStatus
        {
            get
            {
                return WebBase.GetQueryStringString("s", "");
            }
        }
    }
}

