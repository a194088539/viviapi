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

    public class orderreport5 : ManagePageBase
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
            string s = this.txtuserid.Text.Trim();
            int result = 0;
            if (int.TryParse(s, out result))
            {
            }
            int typeid = 0;
            if (!string.IsNullOrEmpty(this.ddlChannelType.SelectedValue))
            {
                typeid = int.Parse(this.ddlChannelType.SelectedValue);
            }
            string text = this.StimeBox.Text;
            string edt = this.EtimeBox.Text;
            DataSet set = Dal.AgentStat4(result, typeid, text, edt, this.Pager1.PageSize, this.Pager1.CurrentPageIndex - 1, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.gv_data.DataSource = set.Tables[1];
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
                this.LoadData();
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

