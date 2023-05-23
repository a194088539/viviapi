namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class feedbackview : UserPageBase
    {
        private feedbackInfo _item = null;
        private viviapi.BLL.feedback bll = new viviapi.BLL.feedback();
        protected HtmlForm form1;
        protected Literal lit_clientip;
        protected Literal lit_cont;
        protected Literal lit_reply;
        protected Literal lit_title;
        protected Literal lit_typeid;

        private void InitForm()
        {
            if (this.ItemInfo != null)
            {
                this.lit_typeid.Text = Enum.GetName(typeof(feedbacktype), this.ItemInfo.typeid);
                this.lit_title.Text = this.ItemInfo.title;
                this.lit_cont.Text = this.ItemInfo.cont;
                this.lit_clientip.Text = this.ItemInfo.clientip;
                this.lit_reply.Text = this.ItemInfo.reply;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }

        public int itemid
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public feedbackInfo ItemInfo
        {
            get
            {
                if ((this._item == null) && (this.itemid > 0))
                {
                    this._item = this.bll.GetModel(this.itemid, base.UserId);
                }
                return this._item;
            }
        }
    }
}

