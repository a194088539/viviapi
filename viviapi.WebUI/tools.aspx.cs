namespace viviapi.WebUI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviLib.Security;

    public class Tools : Page
    {
        protected Button Button1;
        protected Button Button2;
        protected HtmlForm form1;
        protected TextBox TextBox1;
        protected TextBox TextBox2;

        protected void Button1_Click(object sender, EventArgs e)
        {
            string text = this.TextBox1.Text;
            this.TextBox2.Text = Cryptography.RijndaelEncrypt(text);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string text = this.TextBox1.Text;
            this.TextBox2.Text = Cryptography.RijndaelDecrypt(text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

