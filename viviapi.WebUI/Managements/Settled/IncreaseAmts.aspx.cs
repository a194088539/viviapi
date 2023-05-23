namespace viviapi.WebUI.Managements.Settled
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Settled;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class IncreaseAmts : ManagePageBase
    {
        protected Button btnAdd;
        protected Button btnSearch;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptTrades;
        protected HtmlInputHidden selectedUsers;
        protected TextBox StimeBox;
        protected TextBox txtuserId;
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("IncreaseAmtEdit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (base.currentManage.isSuperAdmin <= 0)
            {
            }
            string s = this.txtuserId.Text.Trim();
            int result = 0;
            if (int.TryParse(s, out result))
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
            DataSet set = IncreaseAmt.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptTrades.DataSource = set.Tables[1];
            this.rptTrades.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
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

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("proid", 0);
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

