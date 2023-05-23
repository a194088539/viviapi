namespace viviapi.WebUI.Userlogin.safety
{
    using System;
    using System.Web.UI.HtmlControls;
    using viviapi.WebComponents.Web;

    public class index : UserPageBase
    {
        protected HtmlForm form1;
        public string getnid = "";
        public string getnm = "";

        private void InitForm()
        {
            this.getnid = base.currentUser.ID.ToString();
            this.getnm = base.currentUser.UserName;
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

