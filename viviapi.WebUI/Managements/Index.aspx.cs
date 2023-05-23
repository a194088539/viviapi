namespace viviapi.WebUI.Managements
{
    using System;
    using System.Web.UI.HtmlControls;
    using viviapi.WebComponents.Web;
    using viviLib.TimeControl;

    public class Index : ManagePageBase
    {
        protected HtmlForm form1;
        protected string loginip;
        protected string logintime;
        protected HtmlGenericControl paysouid;
        protected string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                try
                {
                    this.loginip = base.currentManage.lastLoginIp;
                    this.logintime = FormatConvertor.DateTimeToTimeString(base.currentManage.lastLoginTime.Value);
                    this.username = base.currentManage.username;
                    this.paysouid.InnerText = "欢迎使用第三方支付平台";
                }
                catch
                {
                }
            }
        }

        private void setPower()
        {
        }
    }
}

