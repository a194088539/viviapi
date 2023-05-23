namespace viviapi.WebUI.Managements.Order
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Order;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class usersOrderIncomes : ManagePageBase
    {
        protected Button btn_Search;
        protected DropDownList ddlChannelType;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected Repeater gv_data;
        protected AspNetPager Pager1;
        protected TextBox StimeBox;
        protected TextBox txtuserid;
        protected TextBox txtvaluefrom;
        protected TextBox txtvalueto;

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            int result = 0;
            if (!string.IsNullOrEmpty(this.txtuserid.Text.Trim()) && int.TryParse(this.txtuserid.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("userid", result));
            }
            if ((!string.IsNullOrEmpty(this.ddlChannelType.SelectedValue) && int.TryParse(this.ddlChannelType.SelectedValue, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("typeid", result));
            }
            DateTime minValue = DateTime.MinValue;
            if ((!string.IsNullOrEmpty(this.StimeBox.Text.Trim()) && DateTime.TryParse(this.StimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString("yyyy-MM-dd")));
            }
            if ((!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && DateTime.TryParse(this.EtimeBox.Text.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString("yyyy-MM-dd")));
            }
            decimal num2 = 0M;
            if (!string.IsNullOrEmpty(this.txtvaluefrom.Text.Trim()) && decimal.TryParse(this.txtvaluefrom.Text.Trim(), out num2))
            {
                searchParams.Add(new SearchParam("fvaluefrom", num2));
            }
            if (!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && decimal.TryParse(this.EtimeBox.Text.Trim(), out num2))
            {
                searchParams.Add(new SearchParam("fvalueto", num2));
            }
            string orderby = string.Empty;
            OrderBank bank = new OrderBank();
            DataSet set = Dal.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.gv_data.DataSource = set.Tables[1];
            this.gv_data.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.AddDays(-30.0).ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Report))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

