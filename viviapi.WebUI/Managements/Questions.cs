namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.WebComponents.Web;

    public class Questions : ManagePageBase
    {
        protected Button btnAdd;
        protected HtmlForm form1;
        protected GridView GridView1;
        protected HtmlHead Head1;

        private void BindView()
        {
            DataSet list = new Question().GetList(string.Empty);
            this.GridView1.DataSource = list;
            this.GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("QuestionEdit.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.BindView();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.System))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

