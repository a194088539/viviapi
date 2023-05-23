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

    public class UserIdImgLists : BusinessPageBase
    {
        protected Button btnCashTo;
        protected Button btnDelete;
        protected Button btnSearch;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptIamges;
        protected HtmlInputHidden selectedUsers;
        protected DropDownList StatusList;
        protected TextBox StimeBox;
        protected TextBox txtUserId;
        protected TextBox txtUserName;

        protected void btnCashTo_Click(object sender, EventArgs e)
        {
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["chkItem"];
            foreach (string str2 in str.Split(new char[] { ',' }))
            {
                new usersIdImage().Delete(int.Parse(str2));
            }
            this.LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void DoCmd()
        {
            if ((!string.IsNullOrEmpty(this.cmd) && (this.ItemID > 0)) && (this.UserId > 0))
            {
                usersIdImageInfo model = new usersIdImageInfo();
                model.id = this.ItemID;
                if (this.cmd == "ok")
                {
                    model.status = IdImageStatus.审核成功;
                }
                if (this.cmd == "fail")
                {
                    model.status = IdImageStatus.审核失败;
                }
                model.why = string.Empty;
                model.checktime = new DateTime?(DateTime.Now);
                model.admin = new int?(base.ManageId);
                model.userId = new int?(this.UserId);
                usersIdImage image = new usersIdImage();
                if (image.Check(model))
                {
                    base.AlertAndRedirect("操作成功", "UserIdImgList.aspx?s=1");
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
            DateTime minValue = DateTime.MinValue;
            if ((!string.IsNullOrEmpty(this.StimeBox.Text.Trim()) && DateTime.TryParse(this.StimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", this.StimeBox.Text.Trim()));
            }
            if ((!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && DateTime.TryParse(this.EtimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.AddDays(1.0)));
            }
            string orderby = string.Empty;
            DataSet set = usersIdImage.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
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
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                if (!string.IsNullOrEmpty(this.ItemStatus))
                {
                    this.StatusList.SelectedValue = this.ItemStatus;
                }
                this.LoadData();
            }
        }

        protected void Pager1_PageChanging(object src, PageChangingEventArgs e)
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
                if (str3 == "1")
                {
                    str4 = string.Format("<a onclick=\"return confirm('审核成功?')\" href=\"?cmd=ok&ID={0}&userid={1}\" style=\"color:Green;\">通过</a> <a onclick=\"return confirm('审核失败？')\" href=\"?cmd=fail&ID={0}&userid={1}\" style=\"color:red;\">失败</a>", str, str2);
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
                return WebBase.GetQueryStringInt32("proid", 0);
            }
        }

        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }
    }
}

