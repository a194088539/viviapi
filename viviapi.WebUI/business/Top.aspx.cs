namespace viviapi.WebUI.Business
{
    using System;
    using viviapi.WebComponents.Web;

    public class Top : BusinessPageBase
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

