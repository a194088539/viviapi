using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;
using viviapi.Model.Settled;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviapi.WebUI.Managements
{
    public class TocashScheme : ManagePageBase
    {
        public TocashSchemeInfo _ItemInfo = (TocashSchemeInfo)null;
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected Button btnAdd;
        protected GridView GridView1;

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public string Action
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public bool isUpdate
        {
            get
            {
                return this.ItemInfoId > 0 && this.Action == "edit";
            }
        }

        public bool isDel
        {
            get
            {
                return this.ItemInfoId > 0 && this.Action == "del";
            }
        }

        public TocashSchemeInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                    this._ItemInfo = !this.isUpdate ? new TocashSchemeInfo() : viviapi.BLL.Settled.TocashScheme.GetModel(this.ItemInfoId);
                return this._ItemInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            this.DoCmd();
            if (this.IsPostBack)
                return;
            this.BindView();
        }

        private void DoCmd()
        {
            if (!this.isDel)
                return;
            if (viviapi.BLL.Settled.TocashScheme.Delete(this.ItemInfoId))
                this.AlertAndRedirect("删除成功!", "TocashSchemes.aspx");
            else
                this.AlertAndRedirect("删除失败!", "TocashSchemes.aspx");
        }

        private void setPower()
        {
            if (ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
                return;
            this.Response.Write("Sorry,No authority!");
            this.Response.End();
        }

        private void BindView()
        {
            this.GridView1.DataSource = (object)viviapi.BLL.Settled.TocashScheme.GetList("type=1").Tables[0].DefaultView;
            this.GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.BindView();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("TocashSchemeModi.aspx");
        }
    }
}
