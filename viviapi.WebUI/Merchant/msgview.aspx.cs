﻿using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;
using viviapi.WebComponents.Web;
using viviLib.Web;

namespace viviapi.WebUI.LongBao.merchant
{
    public class msgview : UserPageBase
    {
        private IMSG _item = (IMSG)null;
        protected HtmlForm form1;
        protected Literal lit_title;
        protected Literal lit_addtime;
        protected Literal lit_content;

        public int msgId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public IMSG ItemInfo
        {
            get
            {
                if (this._item == null && this.msgId > 0)
                    this._item = IMSGFactory.GetModel(this.msgId, this.UserId);
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
            this.lit_title.Text = this.ItemInfo.msg_title;
            this.lit_addtime.Text = this.ItemInfo.msg_addtime.Value.ToString("yyyy-MM-dd");
            this.lit_content.Text = this.ItemInfo.msg_content;
        }
    }
}
