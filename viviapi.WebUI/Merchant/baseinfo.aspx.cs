namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web.UI.HtmlControls;
    using viviapi.BLL.Payment;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;

    public class baseinfo : UserPageBase
    {
        protected HtmlInputText txtemail;
        protected HtmlInputText txtLinkMan;
        protected HtmlInputText txtqq;
        protected HtmlInputText txtsitename;
        protected HtmlInputText txtsiteUrl;
        protected HtmlInputText txtStatus;
        protected HtmlInputText txtTel;
        protected HtmlInputText txtuserid;
        protected HtmlInputText txtuserlev;
        protected HtmlInputText txtusername;

        private void InitForm()
        {
            this.txtuserid.Attributes["readonly"] = "true";
            this.txtusername.Attributes["readonly"] = "true";
            this.txtuserlev.Attributes["readonly"] = "true";
            this.txtemail.Attributes["readonly"] = "true";
            this.txtsitename.Attributes["readonly"] = "true";
            this.txtsiteUrl.Attributes["readonly"] = "true";
            this.txtTel.Attributes["readonly"] = "true";
            this.txtLinkMan.Attributes["readonly"] = "true";
            this.txtStatus.Attributes["readonly"] = "true";
            this.txtqq.Attributes["readonly"] = "true";
            this.txtuserid.Value = base.currentUser.ID.ToString();
            this.txtusername.Value = base.currentUser.UserName;
            this.txtuserlev.Value = PayRateFactory.GetUserLevName(base.currentUser.ID);
            this.txtemail.Value = base.UserViewEmail;
            this.txtsitename.Value = base.currentUser.SiteName;
            this.txtsiteUrl.Value = base.currentUser.SiteUrl;
            this.txtTel.Value = base.currentUser.Tel;
            this.txtLinkMan.Value = base.currentUser.LinkMan;
            this.txtStatus.Value = Enum.GetName(typeof(UserStatusEnum), base.currentUser.Status);
            this.txtqq.Value = base.currentUser.QQ;
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

