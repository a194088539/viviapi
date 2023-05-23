namespace viviapi.WebUI.Userlogin.safety
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class modiphone : UserPageBase
    {
        protected Button btnSave;
        protected HtmlForm form1;
        protected HtmlInputHidden IsPhonePass;
        protected Literal litphone;
        protected HtmlInputText phonecode;
        protected HtmlInputText yphone;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string parmValue = this.GetParmValue("action");
            string str3 = this.GetParmValue("yphone");
            string str4 = this.GetParmValue("phone");
            string str5 = this.GetParmValue("getdate");
            string str6 = this.GetParmValue("phonecode");
            string objId = "PHONE_VALID_" + str4;
            string str8 = (string)WebCache.GetCacheService().RetrieveObject(objId);
            if (base.currentUser.IsPhonePass == 0)
            {
                if (string.IsNullOrEmpty(str4))
                {
                    str = "请输入手机号码!";
                }
                else if (string.IsNullOrEmpty(str4))
                {
                    str = "请输入手机认验码!";
                }
                else if (str6 != str8)
                {
                    str = "手机验证码不正确!";
                }
                else
                {
                    base.currentUser.IsPhonePass = 1;
                    base.currentUser.Tel = str4;
                }
            }
            else if (parmValue == "modiphone")
            {
                if (string.IsNullOrEmpty(str3))
                {
                    str = "请输入原手机号码!";
                }
                else if (str3 != base.currentUser.Tel)
                {
                    str = "原手机号码输入错误!";
                }
                else if (string.IsNullOrEmpty(str4))
                {
                    str = "请输入新手机号码!";
                }
                else if (str3 == str4)
                {
                    str = "原手机号码和新手机号码一样!";
                }
                else if (string.IsNullOrEmpty(str6))
                {
                    str = "请输入手机认验码!";
                }
                else if (str6 != str8)
                {
                    str = "手机验证码不正确!";
                }
                else
                {
                    base.currentUser.Tel = str4;
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                if (UserFactory.Update(base.currentUser, null))
                {
                    str = "操作成功";
                }
                else
                {
                    base.currentUser.IsPhonePass = 0;
                    base.currentUser.Tel = string.Empty;
                    str = "修改失败";
                }
            }
            base.AlertAndRedirect(str);
        }

        private string GetParmValue(string caption)
        {
            return WebBase.GetFormString(caption, "");
        }

        private void InitForm()
        {
            this.IsPhonePass.Value = base.currentUser.IsPhonePass.ToString();
            if (base.currentUser.IsPhonePass == 0)
            {
                this.litphone.Text = "<input name=\"phone\" type=\"text\" id=\"phone\" value=\"\" maxlength=\"11\" class=\"txt_02\" size=\"50\"/><input name=\"getdate\" value=\"new\" type=\"hidden\" />";
                this.litphone.Text = this.litphone.Text + "<em class=\"txtr b_m_l\">* <a href=\"javascript:;\" id=\"sendmsg\">发送验证码</a></em>    ";
            }
            else
            {
                this.litphone.Text = string.Format("<em id=\"phoneinput\">{0} <a href=\"javascript:;\"> 修改</a></em>", base.UserViewTel);
                this.litphone.Text = this.litphone.Text + "<span id=\"phoneputbox\"><input name=\"phone\" type=\"text\" id=\"phone\" value=\"\" maxlength=\"11\" class=\"txt_01\" size=\"50\"/><em class=\"txtr b_m_l b_m_r\">*  <a href=\"javascript:;\" id=\"sendmsg\">发送验证码</a></em><em id=\"phoneinput_close\"><a href=\"javascript:;\"> 取消</a></em></span>";
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

