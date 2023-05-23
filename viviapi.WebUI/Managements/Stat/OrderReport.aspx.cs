namespace viviapi.WebUI.Managements.Order
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;

    public class OrderReport : ManagePageBase
    {
        protected Button btn_Search;
        protected TextBox EtimeBox;
        protected HtmlForm form1;
        protected Repeater gv_data;
        protected Label lbchumoney;
        protected Label lbmoney;
        protected TextBox StimeBox;

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            DateTime beginTime = DateTime.Parse(this.StimeBox.Text.Trim());
            DateTime endTime = DateTime.Parse(this.EtimeBox.Text.Trim());
            DataTable table = viviapi.BLL.Stat.OrderReport.ReportByUser(beginTime, endTime, 0);
            double num = 0.0;
            double num2 = 0.0;
            foreach (DataRow row in table.Rows)
            {
                num += Convert.ToDouble(row["totalAmt"].ToString());
                num2 += Convert.ToDouble(row["payAmt"].ToString());
            }
            this.lbmoney.Text = num.ToString("f2");
            this.lbchumoney.Text = num2.ToString("f2");
            this.gv_data.DataSource = table;
            this.gv_data.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.StimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.StimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.EtimeBox.Attributes.Add("onFocus", "WdatePicker()");
            }
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

