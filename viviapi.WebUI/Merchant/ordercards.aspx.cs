namespace viviapi.WebUI.Merchant
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

    public class ordercards : UserPageBase
    {
        protected Button b_search;
        protected HtmlSelect channelId;
        protected DropDownList ddlNotifyStatus;
        protected HtmlInputText edate;
        protected HtmlInputText okey;
        protected string pageordersucctotal = "0";
        protected string pageordertotal = "0";
        protected AspNetPager Pager1;
        protected string pagerealvalue = "0";
        protected string pagerefervalue = "0";
        protected Repeater rptOrders;
        protected HtmlInputText sdate;
        protected HtmlSelect select_field;
        protected HtmlSelect Success;
        protected string totalordertotal = "0";
        protected string totalrealvalue = "0";
        protected string totalrefervalue = "0";
        protected string totalsuccordertotal = "0";

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
            this.sdate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            this.edate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            this.channelId.Value = "0";
            this.okey.Value = string.Empty;
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            int result = 0;
            if ((!string.IsNullOrEmpty(this.channelId.Value) && int.TryParse(this.channelId.Value, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("typeId", result));
            }
            if (int.TryParse(this.select_field.Value, out result))
            {
                string str2 = this.okey.Value.Trim();
                if (!string.IsNullOrEmpty(str2))
                {
                    switch (result)
                    {
                        case 1:
                            searchParams.Add(new SearchParam("userorder", str2));
                            goto Label_0128;

                        case 3:
                            searchParams.Add(new SearchParam("orderid", str2));
                            break;

                        case 2:
                            searchParams.Add(new SearchParam("cardno", str2));
                            break;
                    }
                }
            }
        Label_0128:
            if ((!string.IsNullOrEmpty(this.Success.Value) && int.TryParse(this.Success.Value, out result)) && (result > 0))
            {
                if (result != 4)
                {
                    searchParams.Add(new SearchParam("status", result));
                }
                else
                {
                    searchParams.Add(new SearchParam("statusallfail", result));
                }
            }
            if ((!string.IsNullOrEmpty(this.ddlNotifyStatus.SelectedValue) && int.TryParse(this.ddlNotifyStatus.SelectedValue, out result)) && (result > 0))
            {
                searchParams.Add(new SearchParam("notifystat", result));
            }
            DateTime minValue = DateTime.MinValue;
            if (DateTime.TryParse(this.builderdate(this.sdate.Value, "00", "00", "00"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("stime", minValue.ToString()));
            }
            if (DateTime.TryParse(this.builderdate(this.edate.Value, "23", "59", "59"), out minValue) && (minValue > DateTime.MinValue))
            {
                searchParams.Add(new SearchParam("etime", minValue.ToString()));
            }
            string orderby = string.Empty;
            DataSet set = new OrderCard().PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptOrders.DataSource = set.Tables[1];
            this.rptOrders.DataBind();
            DataTable table = set.Tables[2];
            if ((table != null) && (table.Rows.Count > 0))
            {
                try
                {
                    this.totalrefervalue = Convert.ToDecimal(table.Rows[0]["refervalue"]).ToString("f0");
                    this.totalrealvalue = Convert.ToDecimal(table.Rows[0]["realvalue"]).ToString("f0");
                    this.totalordertotal = Convert.ToDecimal(table.Rows[0]["ordtotal"]).ToString("f0");
                    this.totalsuccordertotal = Convert.ToDecimal(table.Rows[0]["succordtotal"]).ToString("f0");
                }
                catch
                {
                }
            }
            table = set.Tables[1];
            try
            {
                this.pagerefervalue = Convert.ToDecimal(table.Compute("sum(refervalue)", "")).ToString("f0");
                this.pagerealvalue = Convert.ToDecimal(table.Compute("sum(realvalue)", "")).ToString("f0");
                this.pageordertotal = table.Rows.Count.ToString();
                DataRow[] rowArray = table.Select("status=2");
                this.pageordersucctotal = rowArray.Length.ToString();
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
                    case "8":
                        literal.Text = string.Format("<a href=\"javascript:replenish('{0}');\">&laquo; 补单</a>", str2);
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

