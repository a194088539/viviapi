namespace viviapi.WebUI.Userlogin.behalf
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Settled;
    using viviapi.ETAPI;
    using viviapi.Model.Settled;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class agentdists : UserPageBase
    {
        private TocashSchemeInfo _scheme = null;
        protected Button b_search;
        protected DropDownList ddlaudit_status;
        protected DropDownList ddlbankCode;
        protected DropDownList ddlpayment_status;
        protected HtmlForm form1;
        protected AspNetPager Pager1;
        protected Repeater rptDetails;
        protected viviapi.BLL.Withdraw.settledAgent stlAgtBLL = new viviapi.BLL.Withdraw.settledAgent();
        protected TextBox txtAccount;
        protected TextBox txtbankAccountName;
        protected HtmlInputText txtout_trade_no;
        protected HtmlInputText txttrade_no;

        private string Affirm(string trade_no, byte status)
        {
            string str = string.Empty;
            int num = this.stlAgtBLL.Affirm(trade_no, status, ServerVariables.TrueIP);
            if (num == 0)
            {
                str = "操作成功";
            }
            else if (num == 1)
            {
                str = "不存在此单";
            }
            else if (num == 2)
            {
                str = "此单已处理，不可重复操作";
            }
            else if (num == 3)
            {
                str = "输入不正确";
            }
            else if (num == 4)
            {
                str = "系统出错";
            }
            else
            {
                str = "未知错误";
            }
            if ((status == 2) && (num == 0))
            {
                if ((this.scheme.tranRequiredAudit != 0) || (this.scheme.vaiInterface != 1))
                {
                    return str;
                }
                viviapi.Model.Withdraw.settledAgent model = this.stlAgtBLL.GetModel(trade_no);
                if (!((model.suppid <= 0) || model.is_cancel))
                {
                    Withdraw.InitDistribution2(model);
                }
            }
            return str;
        }

        protected void b_search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void InitForm()
        {
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("userid", base.UserId));
            searchParams.Add(new SearchParam("mode", 1));
            if (!string.IsNullOrEmpty(this.txttrade_no.Value.Trim()))
            {
                searchParams.Add(new SearchParam("trade_no", this.txttrade_no.Value.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtout_trade_no.Value.Trim()))
            {
                searchParams.Add(new SearchParam("out_trade_no", this.txtout_trade_no.Value.Trim()));
            }
            if (!string.IsNullOrEmpty(this.ddlbankCode.SelectedValue))
            {
                searchParams.Add(new SearchParam("bankCode", this.ddlbankCode.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtbankAccountName.Text))
            {
                searchParams.Add(new SearchParam("bankAccountName", this.txtbankAccountName.Text));
            }
            if (!string.IsNullOrEmpty(this.ddlaudit_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("audit_status", int.Parse(this.ddlaudit_status.SelectedValue)));
            }
            if (!string.IsNullOrEmpty(this.ddlpayment_status.SelectedValue))
            {
                searchParams.Add(new SearchParam("payment_status", int.Parse(this.ddlpayment_status.SelectedValue)));
            }
            DataSet set = this.stlAgtBLL.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty, 0);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            DataTable table = set.Tables[1];
            this.rptDetails.DataSource = table;
            this.rptDetails.DataBind();
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

        protected void rptDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string msg = string.Empty;
                string str2 = e.CommandArgument.ToString();
                if (e.CommandName == "sure")
                {
                    msg = this.Affirm(str2, 2);
                }
                else if (e.CommandName == "cancel")
                {
                    msg = this.Affirm(str2, 3);
                }
                base.AlertAndRedirect(msg, "agentdists.aspx");
            }
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) && (DataBinder.Eval(e.Item.DataItem, "issure").ToString() == "1"))
            {
                Button button = e.Item.FindControl("btnSure") as Button;
                Button button2 = e.Item.FindControl("btnCancel") as Button;
                button.Visible = true;
                button2.Visible = true;
            }
        }

        protected TocashSchemeInfo scheme
        {
            get
            {
                if (this._scheme == null)
                {
                    this._scheme = TocashScheme.GetModelByUser(2, base.UserId);
                }
                return this._scheme;
            }
        }
    }
}

