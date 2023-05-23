namespace viviapi.WebUI.Managements
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class SettledSearch : ManagePageBase
    {
        protected Button btnSearch;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected TextBox KeyWords;
        protected AspNetPager Pager1;
        protected Repeater rptList;
        protected DropDownList SeachType;
        protected DropDownList StatusList;
        protected TextBox StimeBox;
        protected string TotalMoney = "0.00元";

        private void BindData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.StatusList.SelectedValue))
            {
                searchParams.Add(new SearchParam("status", int.Parse(this.StatusList.SelectedValue)));
            }
            string s = this.KeyWords.Text.Trim();
            int result = 0;
            int.TryParse(s, out result);
            string selectedValue = this.SeachType.SelectedValue;
            if (result > 0)
            {
                if (string.IsNullOrEmpty(selectedValue))
                {
                    searchParams.Add(new SearchParam("all", result));
                }
                else if (selectedValue.ToLower() == "id")
                {
                    searchParams.Add(new SearchParam("id", result));
                }
                else if (selectedValue.ToLower() == "userid")
                {
                    searchParams.Add(new SearchParam("userid", result));
                }
            }
            else if (!string.IsNullOrEmpty(s) && (string.IsNullOrEmpty(selectedValue) || (selectedValue.ToLower() == "username")))
            {
                searchParams.Add(new SearchParam("username", s));
            }
            DateTime minValue = DateTime.MinValue;
            DateTime.TryParse(this.StimeBox.Text.Trim(), out minValue);
            if (minValue != DateTime.MinValue)
            {
                searchParams.Add(new SearchParam("begindate", minValue));
            }
            DateTime.TryParse(this.EtimeBox.Text.Trim(), out minValue);
            if (minValue != DateTime.MinValue)
            {
                searchParams.Add(new SearchParam("enddate", minValue.AddDays(1.0)));
            }
            DataSet set = SettledFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            DataTable table = set.Tables[1];
            if (table != null)
            {
                table.Columns.Add("StatusText");
                foreach (DataRow row in table.Rows)
                {
                    switch (((SettledStatus)row["Status"]))
                    {
                        case SettledStatus.审核中:
                            row["StatusText"] = "<font color='#66CC00'>审核中</font>";
                            break;

                        case SettledStatus.支付中:
                            row["StatusText"] = "<a href=\"PayMoneyInfo.aspx?ID=" + row["ID"].ToString() + "\">进行支付</a>";
                            break;

                        case SettledStatus.无效:
                            row["StatusText"] = "<font color='red'>无效申请</font>";
                            break;

                        case SettledStatus.已支付:
                            row["StatusText"] = "<font color='blue'>已支付</font>";
                            break;
                    }
                }
            }
            this.rptList.DataSource = table;
            this.rptList.DataBind();
            this.TotalMoney = Convert.ToString(set.Tables[2].Rows[0][0]);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StatusList.Items.Add(new ListItem("—状态—", ""));
                foreach (int num in Enum.GetValues(typeof(SettledStatus)))
                {
                    this.StatusList.Items.Add(new ListItem(Enum.GetName(typeof(SettledStatus), num), num.ToString()));
                }
                this.StatusList.SelectedValue = 8.ToString();
                if (this.action == "paylistbyid")
                {
                    this.SeachType.SelectedValue = "Userid";
                    this.KeyWords.Text = this.userId.ToString();
                }
                this.BindData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public string action
        {
            get
            {
                return WebBase.GetQueryStringString("action", "");
            }
        }

        public int userId
        {
            get
            {
                return WebBase.GetQueryStringInt32("userid", 0);
            }
        }
    }
}

