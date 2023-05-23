namespace viviapi.WebUI.Userlogin.settlement
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Order;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class incomestat : UserPageBase
    {
        protected Button b_search;
        protected DropDownList ddlChannelType;
        protected HtmlInputText edate;
        protected HtmlForm form1;
        protected string pageordercount = "0";
        protected AspNetPager Pager1;
        protected string pagesumpay = "0";
        protected Repeater rptOrders;
        protected HtmlInputText sdate;
        protected string totalordercount = "0";
        protected string totalsumpay = "0";

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void InitForm()
        {
            this.sdate.Value = DateTime.Today.AddDays(-1.0).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            int result = 0;
            if ((!string.IsNullOrEmpty(this.ddlChannelType.SelectedValue) && int.TryParse(this.ddlChannelType.SelectedValue, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("typeId", result));
            }
            DateTime minValue = DateTime.MinValue;
            if (DateTime.TryParse(this.sdate.Value, out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString("yyyy-MM-dd")));
            }
            if (DateTime.TryParse(this.edate.Value, out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString("yyyy-MM-dd")));
            }
            string orderby = string.Empty;
            DataSet set = Dal.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = set.Tables[1];
            this.rptOrders.DataBind();
            DataTable table = set.Tables[2];
            if ((table != null) && (table.Rows.Count > 0))
            {
                try
                {
                    this.totalordercount = Convert.ToDecimal(table.Rows[0]["s_num"]).ToString("f0");
                    this.totalsumpay = Convert.ToDecimal(table.Rows[0]["sumpay"]).ToString("f0");
                }
                catch
                {
                }
            }
            table = set.Tables[1];
            try
            {
                this.pageordercount = Convert.ToDecimal(table.Compute("sum(s_num)", "")).ToString("f0");
                this.pagesumpay = Convert.ToDecimal(table.Compute("sum(sumpay)", "")).ToString("f0");
            }
            catch
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }
    }
}

