namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.Model.User;

    public class Base : MasterPage
    {
        protected ContentPlaceHolder ContentPlaceHolder1;
        protected HtmlForm form1;
        protected ContentPlaceHolder head;
        protected HyperLink hlinkLastIp;
        protected Literal litUserName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && (this.currentUser != null))
            {
                this.hlinkLastIp.NavigateUrl = string.Format("http://www.ip138.com/ips138.asp?ip={0}&action=2", this.currentUser.LastLoginIp);
                this.hlinkLastIp.Text = "登录IP:" + this.currentUser.LastLoginIp;
                this.litUserName.Text = this.currentUser.UserName;
            }
        }

        public UserInfo currentUser
        {
            get
            {
                return UserFactory.CurrentMember;
            }
        }
    }
}

