namespace viviapi.WebUI.Managements
{
    using System;
    using viviapi.WebComponents.Web;
    using viviapi.WebUI;

    public partial class APIConfig : ManagePageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                WebUtility.BindBankSupplierDDL(this.ddlbankurl);
                WebUtility.BindCardSupplierDLL(this.ddlisurl);
                WebUtility.BindCardSupplierDLL(this.ddlszx);
                WebUtility.BindCardSupplierDLL(this.ddlsd);
                WebUtility.BindCardSupplierDLL(this.ddlzt);
                WebUtility.BindCardSupplierDLL(this.ddljw);
                WebUtility.BindCardSupplierDLL(this.ddlqq);
                WebUtility.BindCardSupplierDLL(this.ddllt);
                WebUtility.BindCardSupplierDLL(this.ddljy);
                WebUtility.BindCardSupplierDLL(this.ddlwy);
                WebUtility.BindCardSupplierDLL(this.ddlwm);
                WebUtility.BindCardSupplierDLL(this.ddlsh);
                WebUtility.BindCardSupplierDLL(this.ddlonline);
                WebUtility.BindCardSupplierDLL(this.ddlking);
                WebUtility.BindCardSupplierDLL(this.ddlmoko);
                WebUtility.BindCardSupplierDLL(this.ddl5173);
                WebUtility.BindCardSupplierDLL(this.ddlrxk);
                WebUtility.BindCardSupplierDLL(this.ddldx);
                WebUtility.BindSMSSupplierDLL(this.ddlsms);
                WebUtility.BindSXSupplierDLL(this.ddlsxk);
            }
        }

        protected void purlok_Click(object sender, EventArgs e)
        {
        }
    }
}

