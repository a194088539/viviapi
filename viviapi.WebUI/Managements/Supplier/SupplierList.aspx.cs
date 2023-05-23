namespace viviapi.WebUI.Managements
{
    using System;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;

    public partial class SupplierList : ManagePageBase
    {

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("SupplierEdit.aspx", true);
        }

        private void LoadData()
        {
            string where = "release = 1";
            this.GVSupplier.DataSource = SupplierFactory.GetList(where);
            this.GVSupplier.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
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

