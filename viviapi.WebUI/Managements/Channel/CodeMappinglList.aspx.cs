namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.Model;
    using viviapi.WebComponents.Web;

    public class CodeMappinglList : ManagePageBase
    {
        protected Button btnAdd;
        protected DropDownList ddlSupp;
        protected HtmlForm Form1;
        protected GridView gvData;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("CodeMappingEdit.aspx");
        }

        private void LoadData()
        {
            string strWhere = "1=1";
            this.gvData.DataSource = CodeMappingFactory.GetList(strWhere);
            this.gvData.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ddlSupp.Items.Add(new ListItem("---选择接口商---", ""));
                DataTable table = SupplierFactory.GetList("isbank=1 and code<>100 and code <> 101").Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                this.LoadData();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Interfaces))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }
    }
}

