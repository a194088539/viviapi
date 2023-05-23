namespace viviapi.WebUI.Managements
{
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.WebComponents;
    using viviapi.WebComponents.Web;

    public class EmailTest : ManagePageBase
    {
        protected Button btn_Test;
        protected HtmlForm form1;
        protected TextBox txtcontent;
        protected TextBox txtReceives;
        protected TextBox txtSubject;

        protected void btn_Test_Click(object sender, EventArgs e)
        {
            string text = this.txtReceives.Text;
            string str2 = this.txtSubject.Text;
            string body = this.txtcontent.Text;
            new EmailHelper(string.Empty, text, text + "测试邮件", body, true, Encoding.GetEncoding("gbk")).Send();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

