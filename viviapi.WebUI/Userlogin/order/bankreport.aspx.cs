namespace viviapi.WebUI.Userlogin.order
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class bankreport : UserPageBase
    {
        protected Button b_search;
        protected HtmlForm form1;
        protected HtmlInputText okey;
        protected AspNetPager Pager1;
        protected Repeater rptOrders;
        protected HtmlSelect select_field;
        protected HtmlSelect Success;

        protected void b_search_Click(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.LoadData();
            }
        }

        public string builderdate(string date, string hour, string m, string s)
        {
            return string.Format("{0} {1}:{2}:{3}", new object[] { date, hour, m, s });
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            searchParams.Add(new SearchParam("ordertype", "<>", 8));
            int result = 0;
            if (int.TryParse(this.select_field.Value, out result))
            {
                string str2 = this.okey.Value.Trim();
                if (!string.IsNullOrEmpty(str2))
                {
                    if (result == 1)
                    {
                        searchParams.Add(new SearchParam("userorder", str2));
                    }
                    else if (result == 2)
                    {
                        searchParams.Add(new SearchParam("cardNo", str2));
                    }
                }
            }
            if ((!string.IsNullOrEmpty(this.Success.Value) && int.TryParse(this.Success.Value, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("status", result));
            }
            string orderby = string.Empty;
            DataSet set = new OrderBank().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = set.Tables[1];
            this.rptOrders.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                Literal literal = (Literal)e.Item.FindControl("litdo");
                string str = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                string str2 = DataBinder.Eval(e.Item.DataItem, "orderid").ToString();
                switch (str)
                {
                    case "2":
                    case "4":
                        literal.Text = string.Format("<a href=\"javascript:switchstate('{0}');\">&laquo; 查看</a>", DataBinder.Eval(e.Item.DataItem, "againnotifyurl"));
                        break;
                }
            }
            if ((e.Item.ItemType == ListItemType.Footer) && (this.rptOrders.Items.Count == 0))
            {
                Literal literal2 = (Literal)e.Item.FindControl("litfoot");
                literal2.Text = " <tfoot>\r\n                        <tr>\r\n                            <td colspan=\"10\" class=\"nomsg\">\r\n                                －_－^..暂无记录\r\n                            </td>\r\n                        </tr>\r\n                     </tfoot>     ";
            }
        }
    }
}

