namespace viviapi.WebUI.Managements
{
    using System;
    using viviapi.WebComponents.Web;

    public class Top : ManagePageBase
    {
        protected string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.username = base.currentManage.username;
            }
        }

        private void setPower()
        {
        }
    }
}

