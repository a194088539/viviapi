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

    public class orderreport7 : ManagePageBase
    {
        protected Button btn_Search;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected Repeater rep_report;
        protected TextBox StimeBox;
        protected string TotalProfit = "0.00";

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
            DataSet set = Dal.BusinessStat7(minValue, result.AddDays(1.0));
            if (set != null)
            {
                try
                {
                    this.TotalProfit = Convert.ToDecimal(set.Tables[0].Compute("sum(amt)", "")).ToString("f2");
                }
                catch
                {
                }
            }
            this.rep_report.DataSource = set;
            this.rep_report.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
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
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Orders))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

