namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;

    public class apiinfo : UserPageBase
    {
        protected Button btnModiKey;
        protected Button btnSave;
        protected HtmlInputText txtapikey;
        protected HtmlInputText txtReturnUrl;
        protected HtmlInputText txtuserid;

        protected void btnModiKey_Click(object sender, EventArgs e)
        {
            base.currentUser.APIKey = Guid.NewGuid().ToString("N").ToLower();
            UserFactory.Update(base.currentUser, null);
            base.AlertAndRedirect("操作成功", "apiinfo.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            base.currentUser.smsNotifyUrl = this.txtReturnUrl.Value.Trim();
            if (UserFactory.Update(base.currentUser, null))
            {
                base.AlertAndRedirect("更新成功");
            }
            else
            {
                base.AlertAndRedirect("更新失败");
            }
        }

        private void InitForm()
        {
            this.txtuserid.Attributes["readonly"] = "true";
            this.txtapikey.Attributes["readonly"] = "true";
            this.txtuserid.Value = base.currentUser.ID.ToString();
            this.txtapikey.Value = base.currentUser.APIKey;
            this.txtReturnUrl.Value = base.currentUser.smsNotifyUrl;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }
    }
}

