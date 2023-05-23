namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;

    public class api : UserPageBase
    {
        protected Button btnModiKey;
        protected HtmlForm form1;
        public string getapikey = "";
        public string getuserid = "";

        protected void btnModiKey_Click(object sender, EventArgs e)
        {
            base.currentUser.APIKey = Guid.NewGuid().ToString("N").ToLower();
            UserFactory.Update(base.currentUser, null);
            base.AlertAndRedirect("操作成功", "api.aspx");
        }

        private void InitForm()
        {
            this.getuserid = base.currentUser.ID.ToString();
            this.getapikey = base.currentUser.APIKey;
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

