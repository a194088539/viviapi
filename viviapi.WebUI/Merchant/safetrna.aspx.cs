namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Tools;
    using viviapi.WebComponents.Web;
    using viviLib.Text;

    public class safetrna : UserPageBase
    {
        protected Button btnSave;
        protected Label lblMessage;
        protected HtmlTableRow tr_pernumber;
        protected HtmlTableRow tr_repernumber;
        protected HtmlInputText txtpername;
        protected HtmlInputText txtpernumber;
        protected HtmlInputText txtrpernumber;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string input = this.txtpernumber.Value;
            string str3 = this.txtpername.Value;
            if (string.IsNullOrEmpty(str3))
            {
                str = "请输入真实姓名";
            }
            string regularString = Regular.GetRegularString(RegularType.ChineseIDCard, 0x12);
            if (!Regex.IsMatch(input, regularString))
            {
                str = "请输入正确的身份证号码 ";
            }
            else
            {
                string str5;
                string str6;
                IdcardInfo info = new IdcardInfo();
                info.code = input;
                if (viviapi.BLL.basedata.identitycard.GetBirthdayAndSex(input, out str5, out str6))
                {
                    info.birthday = str5;
                    info.gender = str6;
                    viviapi.Model.basedata.identitycard model = viviapi.BLL.basedata.identitycard.GetModel(input.Substring(0, 6));
                    if (model == null)
                    {
                        info = null;
                    }
                    else
                    {
                        info.location = model.DQ;
                    }
                }
                else
                {
                    info = null;
                }
                if (info == null)
                {
                    str = "无效身份信息。请重新输入。";
                }
                else
                {
                    info.fullname = str3;
                    if (string.IsNullOrEmpty(str))
                    {
                        str = "true";
                        this.Session["IDCard_" + base.currentUser.ID.ToString()] = info;
                    }
                }
            }
            if (str.Equals("true"))
            {
                base.Response.Redirect("safetrna1.aspx");
            }
            else
            {
                base.AlertAndRedirect(str);
            }
        }

        private void InitForm()
        {
            this.txtpername.Value = base.currentUser.full_name;
            if (base.currentUser.IsRealNamePass == 1)
            {
                this.lblMessage.Visible = true;
                this.btnSave.Enabled = false;
                this.btnSave.Visible = false;
                this.txtpernumber.Value = base.UserViewIdCard;
                this.txtpername.Attributes["readonly"] = "true";
                this.txtpernumber.Attributes["readonly"] = "true";
                this.tr_repernumber.Visible = false;
                this.lblMessage.Text = "您已通过实名认证。";
            }
            else
            {
                this.tr_repernumber.Visible = true;
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

