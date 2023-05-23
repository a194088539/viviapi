namespace viviapi.WebUI.business
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Order;
    using viviapi.WebComponents.Web;

    public class orderreport2 : BusinessPageBase
    {
        protected Button btn_Search;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected Repeater rep_report;
        protected TextBox StimeBox;
        protected string TotalCommission = "0.00";
        protected string TotalPayAmtATM = "0.00";
        protected string TotalRealvalue = "0.00";
        protected string TotalSupplierAmt = "0.00";

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            DateTime minValue = DateTime.MinValue;
            if (!string.IsNullOrEmpty(this.StimeBox.Text.Trim()) && DateTime.TryParse(this.StimeBox.Text.Trim(), out minValue))
            {
            }
            DateTime result = DateTime.MinValue;
            if (!string.IsNullOrEmpty(this.EtimeBox.Text.Trim()) && DateTime.TryParse(this.EtimeBox.Text.Trim(), out result))
            {
            }
            DataTable table = Dal.StatForBusiness(base.ManageId, minValue, result);
            this.rep_report.DataSource = table;
            this.rep_report.DataBind();
            if (table != null)
            {
                try
                {
                    this.TotalRealvalue = Convert.ToDecimal(table.Compute("sum(realvalue)", "")).ToString("f2");
                    this.TotalPayAmtATM = Convert.ToDecimal(table.Compute("sum(payAmt)", "")).ToString("f2");
                    this.TotalCommission = Convert.ToDecimal(table.Compute("sum(commission)", "")).ToString("f2");
                }
                catch
                {
                }
            }
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

        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
            }
        }

        private void setPower()
        {
        }
    }
}

