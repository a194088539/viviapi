namespace viviapi.WebUI.Userlogin.settlement
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class transferlist : UserPageBase
    {
        protected Button b_search;
        protected HtmlInputText edate;
        protected HtmlForm form1;
        protected HtmlInputText okey;
        protected AspNetPager Pager1;
        protected Repeater rptrecharges;
        protected HtmlInputText sdate;
        protected viviapi.BLL.Settled.transfer tranBLL = new viviapi.BLL.Settled.transfer();

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", new object[] { date, hour, m, s });
        }

        private void InitForm()
        {
            this.sdate.Value = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            DateTime minValue = DateTime.MinValue;
            if ((!string.IsNullOrEmpty(this.sdate.Value.Trim()) && DateTime.TryParse(this.sdate.Value.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("starttime", minValue));
            }
            if ((!string.IsNullOrEmpty(this.edate.Value.Trim()) && DateTime.TryParse(this.edate.Value.Trim(), out minValue)) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("endtime", minValue.AddDays(1.0)));
            }
            DataSet set = this.tranBLL.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptrecharges.DataSource = set.Tables[1];
            this.rptrecharges.DataBind();
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

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Footer) && (this.rptrecharges.Items.Count == 0))
            {
                Literal literal = (Literal)e.Item.FindControl("litfoot");
                literal.Text = " <tfoot>\r\n                        <tr>\r\n                            <td colspan=\"10\" class=\"nomsg\">\r\n                                －_－^..暂无记录\r\n                            </td>\r\n                        </tr>\r\n                     </tfoot>     ";
            }
        }
    }
}

