namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Web.UI.HtmlControls;
    using viviapi.BLL.Payment;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;

    public class info : UserPageBase
    {
        protected HtmlForm form1;
        public string getemail = "";
        public string getidcard = "";
        public string gettel = "";
        protected HtmlInputText txtLinkMan;
        protected HtmlInputText txtqq;
        protected HtmlInputText txtsitename;
        protected HtmlInputText txtsiteUrl;
        protected HtmlInputText txtStatus;
        protected HtmlInputText txtuserid;
        protected HtmlInputText txtuserlev;
        protected HtmlInputText txtusername;

        private void InitForm()
        {
            this.txtuserid.Attributes["readonly"] = "true";
            this.txtusername.Attributes["readonly"] = "true";
            this.txtuserlev.Attributes["readonly"] = "true";
            this.txtsitename.Attributes["readonly"] = "true";
            this.txtsiteUrl.Attributes["readonly"] = "true";
            this.txtLinkMan.Attributes["readonly"] = "true";
            this.txtStatus.Attributes["readonly"] = "true";
            this.txtqq.Attributes["readonly"] = "true";
            this.txtuserid.Value = base.currentUser.ID.ToString();
            this.txtusername.Value = base.currentUser.UserName;
            this.txtuserlev.Value = PayRateFactory.GetUserLevName(base.currentUser.ID);
            this.txtsitename.Value = base.currentUser.SiteName;
            this.txtsiteUrl.Value = base.currentUser.SiteUrl;
            this.txtLinkMan.Value = base.currentUser.full_name;
            this.txtStatus.Value = Enum.GetName(typeof(UserStatusEnum), base.currentUser.Status);
            this.txtqq.Value = base.currentUser.QQ;
            if (base.currentUser.Email.Length > 2)
            {
                this.getemail = base.UserViewEmail;
            }
            else
            {
                this.getemail = "<span style='color:red;'>未通过邮箱验证</span>，<a href='/Userlogin/safety/modiemail.aspx' targert='mainframe' style='color:#6694ae;'>马上去验证</a>";
            }
            if (base.currentUser.Tel.Length > 4)
            {
                string tel = base.currentUser.Tel;
                this.gettel = tel.Substring(0, 3) + "********";
            }
            else
            {
                this.gettel = "<span style='color:red;'>未通过手机验证</span>，<a href='/Userlogin/safety/modiphone.aspx' targert='mainframe' style='color:#6694ae;'>马上去验证</a>";
            }
            if (base.currentUser.IdCard.Length > 4)
            {
                string idCard = base.currentUser.IdCard;
                this.getidcard = "**************" + idCard.Substring(idCard.Length - 4, 4) + "(<span style='color:#448b45;'>已通过实名认证</span>)";
            }
            else
            {
                this.getidcard = "(<span style='color:red;'>未通过实名认证</span>，<a href='/Userlogin/safety/safetrna.aspx' targert='mainframe' style='color:#6694ae;'>马上去认证</a>)";
            }
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

