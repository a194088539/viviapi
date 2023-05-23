namespace viviapi.WebUI.Userlogin.behalf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Withdraw;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class importitems : UserPageBase
    {
        protected Button b_search;
        protected string cancel_qty_str = "0";
        protected DropDownList ddlaudit_status;
        protected DropDownList ddlpayment_status;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected string qty_str = "0";
        protected string qty1_amt_str = "0.00";
        protected string qty1_str = "0";
        protected string qty2_amt_str = "0.00";
        protected string qty2_str = "0";
        protected string qty3_amt_str = "0.00";
        protected string qty3_str = "0";
        protected string qty4_amt_str = "0.00";
        protected string qty4_str = "0";
        protected Repeater rptrecharges;
        protected settledAgent stlAgtBLL = new settledAgent();
        protected TextBox txtamtfrom;
        protected TextBox txtamtto;
        protected TextBox txtLotNo;

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
            if (!string.IsNullOrEmpty(this.lotno))
            {
                this.txtLotNo.Text = this.lotno;
            }
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            searchParams.Add(new SearchParam("mode", 2));
            if (!string.IsNullOrEmpty(this.txtLotNo.Text.Trim()))
            {
                searchParams.Add(new SearchParam("lotno", this.txtLotNo.Text.Trim()));
            }
            decimal result = 0M;
            if (!string.IsNullOrEmpty(this.txtamtfrom.Text.Trim()) && decimal.TryParse(this.txtamtfrom.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("amount_from", result));
            }
            if (!string.IsNullOrEmpty(this.txtamtto.Text.Trim()) && decimal.TryParse(this.txtamtto.Text.Trim(), out result))
            {
                searchParams.Add(new SearchParam("txtamtto", result));
            }
            if (!string.IsNullOrEmpty(this.ddlaudit_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("audit_status", int.Parse(this.ddlaudit_status.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlpayment_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("payment_status", int.Parse(this.ddlpayment_status.SelectedValue)));
            }
            DataSet set = this.stlAgtBLL.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 2);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptrecharges.DataSource = set.Tables[1];
            this.rptrecharges.DataBind();
            try
            {
                DataTable table = set.Tables[2];
                DataRow row = table.Rows[0];
                this.qty_str = string.Format("{0}", row["qty"]);
                this.cancel_qty_str = string.Format("{0}", row["cancel_qty"]);
                this.qty1_str = string.Format("{0}", row["qty1"]);
                this.qty1_amt_str = string.Format("{0:f2}", row["amt1"]);
                this.qty2_str = string.Format("{0}", row["qty2"]);
                this.qty2_amt_str = string.Format("{0:f2}", row["amt2"]);
                this.qty3_str = string.Format("{0}", row["qty3"]);
                this.qty3_amt_str = string.Format("{0:f2}", row["amt3"]);
                this.qty4_str = string.Format("{0}", row["qty4"]);
                this.qty4_amt_str = string.Format("{0:f2}", row["amt4"]);
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
            if ((e.Item.ItemType == ListItemType.Footer) && (this.rptrecharges.Items.Count == 0))
            {
                Literal literal = (Literal)e.Item.FindControl("litfoot");
                literal.Text = " <tfoot>\r\n                        <tr>\r\n                            <td colspan=\"10\" class=\"nomsg\">\r\n                                －_－^..暂无记录\r\n                            </td>\r\n                        </tr>\r\n                     </tfoot>     ";
            }
        }

        public string lotno
        {
            get
            {
                return WebBase.GetQueryStringString("lotno", "");
            }
        }
    }
}

