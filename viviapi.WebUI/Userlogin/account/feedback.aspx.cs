namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class feedback : UserPageBase
    {
        protected Button b_save;
        protected HtmlSelect ddltypeid;
        protected HtmlForm form1;
        protected HtmlTextArea txtcontent;
        protected HtmlInputText txttitle;

        protected void b_save_Click(object sender, EventArgs e)
        {
            string s = this.ddltypeid.Value;
            string str2 = this.txttitle.Value;
            string str3 = this.txtcontent.Value;
            int result = 0;
            if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
            {
                base.AlertAndRedirect("请输入您的建议或意见");
            }
            else
            {
                int.TryParse(s, out result);
                feedbackInfo model = new feedbackInfo();
                model.addtime = DateTime.Now;
                model.cont = str3;
                model.reply = string.Empty;
                model.replyer = 0;
                model.replytime = new DateTime?(DateTime.Now);
                model.status = feedbackstatus.等待回复;
                model.title = str2;
                model.userid = base.UserId;
                model.typeid = (feedbacktype)result;
                model.clientip = ServerVariables.TrueIP;
                viviapi.BLL.feedback feedback = new viviapi.BLL.feedback();
                if (feedback.Add(model) > 0)
                {
                    string script = "<SCRIPT LANGUAGE='javascript'>alert('发送成功');window.parent.location.href='feedbacks.aspx';</SCRIPT>";
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "AlertAndRedirect", script);
                }
                else
                {
                    base.AlertAndRedirect("发送失败");
                }
            }
        }

        private void InitForm()
        {
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            if (base.Server.GetLastError() is HttpRequestValidationException)
            {
                base.Response.Write("<script language=javascript>alert('字符串含有非法字符！')</script>");
                base.Server.ClearError();
                base.Response.Write("<script language=javascript>window.location.href='feedback.aspx';</script>");
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

