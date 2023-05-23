namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Web.UI.HtmlControls;
    using viviapi.WebComponents.Web;

    public class safety : UserPageBase
    {
        protected HtmlForm form1;
        public string getemail = "";
        public string getemailbtn = "";
        public string getidcard = "";
        public string getidcardbtn = "";
        public string getmb = "";
        public string getmbbtn = "";
        public string getphone = "";
        public string getphonebtn = "";
        public string gettxbtn = "";
        public string gettxpass = "";

        private void InitForm()
        {
            if (base.currentUser.Password2.Length > 1)
            {
                this.gettxpass = "<span class=\"label label-success\">保护中</span>";
                this.gettxbtn = "修改";
            }
            else
            {
                this.gettxpass = "<span class=\"label label-warning\">还未设置提现密码</span>";
                this.gettxbtn = "设置";
            }
            if (base.currentUser.answer.Length > 0)
            {
                this.getmb = "<span class=\"label label-success\">已设置</span>";
                this.getmbbtn = "修改";
            }
            else
            {
                this.getmb = "<span class=\"label label-warning\">还未设置密保问题</span>";
                this.getmbbtn = "设置";
            }
            if (base.currentUser.IsPhonePass == 0)
            {
                this.getphone = "<span class=\"label label-warning\">还未进行手机认证</span>";
                this.getphonebtn = "认证";
            }
            else
            {
                this.getphone = "<span class=\"label label-success\">已认证</span>";
                this.getphonebtn = "修改";
            }
            if (base.currentUser.IsEmailPass == 0)
            {
                this.getemail = "<span class=\"label label-warning\">还未进行邮箱认证</span>";
                this.getemailbtn = "认证";
            }
            else
            {
                this.getemail = "<span class=\"label label-success\">已认证</span>";
                this.getemailbtn = "修改";
            }
            if (base.currentUser.IsRealNamePass == 0)
            {
                this.getidcard = "<span class=\"label label-warning\">还未进行实名认证</span>";
                this.getidcardbtn = "认证";
            }
            else
            {
                this.getidcard = "<span class=\"label label-success\">已认证</span>";
                this.getidcardbtn = "修改";
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

