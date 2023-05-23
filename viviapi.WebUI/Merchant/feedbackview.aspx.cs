using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviapi.WebUI.LongBao.merchant
{
    public class feedbackview : UserPageBase
    {
        private feedback bll = new feedback();
        private feedbackInfo _item = (feedbackInfo)null;
        protected HtmlForm form1;
        protected Literal lit_typeid;
        protected Literal lit_title;
        protected Literal lit_cont;
        protected Literal lit_clientip;
        protected Literal lit_reply;

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
                if (this._item == null && this.itemid > 0)
                    this._item = this.bll.GetModel(this.itemid, this.UserId);
                return this._item;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            this.InitForm();
        }

        private void InitForm()
        {
            if (this.ItemInfo == null)
                return;
            this.lit_typeid.Text = Enum.GetName(typeof(feedbacktype), (object)this.ItemInfo.typeid);
            this.lit_title.Text = this.ItemInfo.title;
            this.lit_cont.Text = this.ItemInfo.cont;
            this.lit_clientip.Text = this.ItemInfo.clientip;
            this.lit_reply.Text = this.ItemInfo.reply;
        }
    }
}
