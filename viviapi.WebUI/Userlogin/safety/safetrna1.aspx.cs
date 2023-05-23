namespace viviapi.WebUI.Userlogin.safety
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Tools;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;

    public class safetrna1 : UserPageBase
    {
        protected Button btnSure;
        protected HtmlForm form1;
        protected HtmlInputText txtbirthday;
        protected HtmlInputText txtfullname;
        protected HtmlInputText txtIdcard;
        protected HtmlInputText txtlocation;
        protected HtmlInputText txtmale;

        protected void btnSure_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            IdcardInfo info = this.Session["IDCard_" + base.UserId.ToString()] as IdcardInfo;
            if (info != null)
            {
                base.currentUser.full_name = info.fullname;
                base.currentUser.IdCard = info.code;
                base.currentUser.addtress = info.location;
                base.currentUser.male = info.gender;
                base.currentUser.IsRealNamePass = 1;
                base.currentUser.PayeeBank = info.fullname;
                if (UserFactory.Update(base.currentUser, null))
                {
                    msg = "true";
                }
                else
                {
                    msg = "认证失败";
                }
            }
            if (msg.Equals("true"))
            {
                base.Response.Redirect("safetrna.aspx", true);
            }
            else
            {
                base.AlertAndRedirect(msg);
            }
        }

        private void InitForm()
        {
            this.txtfullname.Attributes["readonly"] = "true";
            this.txtmale.Attributes["readonly"] = "true";
            this.txtbirthday.Attributes["readonly"] = "true";
            this.txtlocation.Attributes["readonly"] = "true";
            this.txtIdcard.Attributes["readonly"] = "true";
            try
            {
                IdcardInfo info = this.Session["IDCard_" + base.UserId.ToString()] as IdcardInfo;
                if (info != null)
                {
                    this.txtfullname.Value = info.fullname;
                    this.txtIdcard.Value = info.code;
                    this.txtmale.Value = info.gender;
                    if (info.birthday.Length == 8)
                    {
                        this.txtbirthday.Value = info.birthday.Substring(0, 4) + "年" + info.birthday.Substring(4, 2) + "月" + info.birthday.Substring(6, 2) + "日";
                    }
                    else
                    {
                        this.txtbirthday.Value = info.birthday;
                    }
                    this.txtlocation.Value = info.location;
                }
            }
            catch
            {
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

