namespace viviapi.WebUI.agent
{
    using System;
    using viviapi.WebComponents.Web;

    public class Top : AgentPageBase
    {
        protected string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.username = base.currentUser.UserName;
            }
        }
    }
}

