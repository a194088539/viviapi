namespace viviapi.WebUI.Managements.Order
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Order;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using Wuqi.Webdiyer;

    public class orderreport6 : ManagePageBase
    {
        protected Button btn_Search;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rep_report;
        protected TextBox StimeBox;
        protected string TotalPayAmtATM = "0.00";
        protected string TotalProfit = "0.00";
        protected string TotalRealvalue = "0.00";
        protected string TotalSupplierAmt = "0.00";

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            string orderby = "promAmt DESC";
            if (this.ViewState["Sort"] != null)
            {
                orderby = this.ViewState["Sort"].ToString();
            }
            DateTime minValue = DateTime.MinValue;
            if (!string.IsNullOrEmpty(this.StimeBox.Text.Trim()) && DateTime.TryParse(this.StimeBox.Text.Trim(), out minValue))
            {
            }
            DateTime result = DateTime.MinValue;
            if (!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && DateTime.TryParse(this.EtimeBox.Text.Trim(), out result))
            {
            }
            DataSet set = Dal.BusinessStat4(minValue, result, this.Pager1.CurrentPageIndex - 1, this.Pager1.PageSize, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0]["C"]);
            this.rep_report.DataSource = set.Tables[1];
            this.rep_report.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Today.AddDays(1.0).ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rep_report_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                LinkButton button = (LinkButton)e.Item.FindControl("iBtn" + e.CommandName.Trim());
                if (this.ViewState[e.CommandName.Trim()] == null)
                {
                    this.ViewState[e.CommandName.Trim()] = "DESC";
                    button.Text = button.Text + "▼";
                }
                else if (this.ViewState[e.CommandName.Trim()].ToString().Trim() == "DESC")
                {
                    this.ViewState[e.CommandName.Trim()] = "ASC";
                    if (button.Text.IndexOf("▼") != -1)
                    {
                        button.Text = button.Text.Trim().Replace("▼", "▲");
                    }
                    else
                    {
                        button.Text = button.Text + "▲";
                    }
                }
                else
                {
                    this.ViewState[e.CommandName.Trim()] = "DESC";
                    if (button.Text.IndexOf("▲") != -1)
                    {
                        button.Text = button.Text.Replace("▲", "▼");
                    }
                    else
                    {
                        button.Text = button.Text + "▼";
                    }
                }
                this.ViewState["text"] = button.Text;
                this.ViewState["id"] = e.CommandName.Trim();
                this.ViewState["Sort"] = e.CommandName.ToString().Trim() + " " + this.ViewState[e.CommandName.Trim()].ToString().Trim();
                this.LoadData();
            }
        }

        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Header) && (this.ViewState["id"] != null))
            {
                LinkButton button = (LinkButton)e.Item.FindControl("iBtn" + this.ViewState["id"].ToString().Trim());
                button.Text = this.ViewState["text"].ToString();
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Orders))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

