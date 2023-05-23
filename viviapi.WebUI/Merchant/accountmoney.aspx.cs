namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web.UI.HtmlControls;
    using viviapi.WebComponents.Web;

    public class accountmoney : UserPageBase
    {
        protected HtmlInputText txtBalance;
        protected HtmlInputText txtFreezeAmt;
        protected HtmlInputText txtuserid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtuserid.Attributes["readonly"] = "true";
                this.txtBalance.Attributes["readonly"] = "true";
                this.txtuserid.Value = base.UserId.ToString();
                this.txtBalance.Value = ((base.balance - base.unpayment) - base.Freeze).ToString("f2");
                this.txtFreezeAmt.Value = base.Freeze.ToString("f2");
            }
        }
    }
}

