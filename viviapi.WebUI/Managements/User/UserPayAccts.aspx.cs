namespace viviapi.WebUI.Managements.User
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

    public class UserPayAccts : ManagePageBase
    {
        protected Button btnDelete;
        protected Button btnSearch;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptApps;
        protected HtmlInputHidden selectedUsers;
        protected DropDownList StatusList;
        protected TextBox StimeBox;
        protected TextBox txtUserId;

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string str = base.Request.Form["chkItem"];
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { ',' });
                foreach (string str2 in strArray)
                {
                    int result = 0;
                    if (int.TryParse(str2, out result))
                    {
                        UserPayBankApp.Delete(result);
                    }
                }
                base.AlertAndRedirect("操作成功", "UserPayAccts.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            }
            int result = 0;
            if (int.TryParse(this.txtUserId.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
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
            DataSet set = UserPayBankApp.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptApps.DataSource = set.Tables[1];
            this.rptApps.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                if (!string.IsNullOrEmpty(this.InfoStatus))
                {
                    this.StatusList.SelectedValue = this.InfoStatus;
                }
                this.LoadData();
            }
        }

        protected void Pager1_PageChanging(object src, PageChangingEventArgs e)
        {
            this.LoadData();
        }

        protected void rptApps_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserPayBankAppInfo model = UserPayBankApp.GetModel(int.Parse(e.CommandArgument.ToString()));
            if (e.CommandName == "pass")
            {
                model.status = AcctChangeEnum.审核成功;
            }
            else if (e.CommandName == "fail")
            {
                model.status = AcctChangeEnum.审核失败;
            }
            model.SureTime = new DateTime?(DateTime.Now);
            model.SureUser = new int?(base.currentManage.id);
            if (UserPayBankApp.Check(model))
            {
                base.AlertAndRedirect("操作成功", "UserPayAccts.aspx");
            }
            else
            {
                base.AlertAndRedirect("操作失败", "UserPayAccts.aspx");
            }
        }

        protected void rptAppsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string s = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                Label label = (Label)e.Item.FindControl("lblUserStat");
                Button button = (Button)e.Item.FindControl("btn_pass");
                Button button2 = (Button)e.Item.FindControl("btn_fail");
                label.Text = Enum.GetName(typeof(AcctChangeEnum), int.Parse(s));
                string str2 = DataBinder.Eval(e.Item.DataItem, "userid").ToString();
                if (s == "1")
                {
                    button.Attributes["onclick"] = "return confirm('确定要通过此审核吗')";
                    button.Enabled = true;
                    button2.Attributes["onclick"] = "return confirm('确定要不同意些申请吗')";
                    button2.Enabled = true;
                }
                else
                {
                    button.Enabled = false;
                    button2.Enabled = false;
                }
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

        public string InfoStatus
        {
            get
            {
                return WebBase.GetQueryStringString("status", "");
            }
        }
    }
}

