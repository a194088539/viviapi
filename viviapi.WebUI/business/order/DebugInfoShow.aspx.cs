namespace viviapi.WebUI.business.Order
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Sys;
    using viviapi.Model.Sys;
    using viviapi.WebComponents.Web;

    public class Console_Order_DebugInfoShow : BusinessPageBase
    {
        protected HtmlForm form1;
        protected Label lbladdtime;
        protected Label lblbugtype;
        protected Label lbldetail;
        protected Label lblerrorcode;
        protected Label lblerrorinfo;
        protected Label lblid;
        protected Label lblurl;
        protected Label lbluserid;
        protected Label lbluserorder;
        public string strid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != "")))
            {
                this.strid = base.Request.Params["id"];
                int id = Convert.ToInt32(this.strid);
                this.ShowInfo(id);
            }
        }

        private void ShowInfo(int id)
        {
            debuginfo model = debuglog.GetModel(id);
            this.lblid.Text = model.id.ToString();
            this.lblbugtype.Text = Enum.GetName(typeof(debugtypeenum), model.bugtype);
            this.lbluserid.Text = model.userid.ToString();
            this.lbluserorder.Text = model.userorder;
            this.lblurl.Text = model.url;
            this.lblerrorcode.Text = model.errorcode;
            this.lblerrorinfo.Text = model.errorinfo;
            this.lbldetail.Text = model.detail;
            this.lbladdtime.Text = model.addtime.Value.ToString("yyyy-MM-dd HH:ss:mm");
        }
    }
}

