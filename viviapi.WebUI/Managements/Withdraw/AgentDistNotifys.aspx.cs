namespace viviapi.WebUI.Managements.Withdraw
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Withdraw;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using Wuqi.Webdiyer;

    public class AgentDistNotifys : ManagePageBase
    {
        protected Button btnSearch;
        protected DropDownList ddlnotifystatus;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected settledAgentNotify notifyBLL = new settledAgentNotify();
        protected AspNetPager Pager1;
        protected Repeater rptList;
        protected string totalMoney = "0.00?";
        protected TextBox txtEtimeBox;
        protected TextBox txtout_trade_no;
        protected TextBox txtStimeBox;
        protected TextBox txttrade_no;
        protected TextBox txtUserId;

        private void BindData()
        {
            List<SearchParam> listParam = new List<SearchParam>();
            int tempId = 0;
            if (!string.IsNullOrEmpty(this.txtUserId.Text.Trim()) && int.TryParse(this.txtUserId.Text.Trim(), out tempId))
            {
                listParam.Add(new SearchParam("userid", tempId));
            }
            if (!string.IsNullOrEmpty(this.txttrade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("trade_no", this.txttrade_no.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtout_trade_no.Text.Trim()))
            {
                listParam.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.ddlnotifystatus.SelectedValue))
            {
                listParam.Add(new SearchParam("tranapi", int.Parse(this.ddlnotifystatus.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlnotifystatus.SelectedValue))
            {
                listParam.Add(new SearchParam("notifystatus", int.Parse(this.ddlnotifystatus.SelectedValue)));
            }
            DataSet pageData = this.notifyBLL.PageSearch(listParam, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(pageData.Tables[0].Rows[0][0]);
            DataTable data = pageData.Tables[1];
            this.rptList.DataSource = data;
            this.rptList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.txtStimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.txtEtimeBox.Attributes.Add("onFocus", "WdatePicker()");
                this.txtStimeBox.Text = DateTime.Now.ToString("yyyy-MM-01");
                this.txtEtimeBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.BindData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

