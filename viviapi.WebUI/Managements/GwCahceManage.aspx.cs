using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Sys;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.SysConfig;
using viviapi.WebComponents.Web;
using viviLib;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.WebUI.LongBao.console
{
    public class GwCahceManage : ManagePageBase
    {
        private WebInfo _objectInfo = (WebInfo)null;
        protected HtmlForm form1;
        protected TextBox txtGwUrl;
        protected DropDownList ddlcachetype;
        protected DropDownList ddlsubcache;
        protected Button BtnRemove;

        public WebInfo ObjectInfo
        {
            get
            {
                if (this._objectInfo == null)
                    this._objectInfo = WebInfoFactory.GetWebInfoByDomain(XRequest.GetHost());
                return this._objectInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack || this.ObjectInfo == null)
                return;
            this.txtGwUrl.Text = this.ObjectInfo.PayUrl;
        }

        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtGwUrl.Text.Trim()))
                return;
            string str1 = Constant.Cache_Mark + this.ddlcachetype.SelectedValue;
            if (this.ddlcachetype.SelectedValue.ToUpper() == "USER_")
                str1 = Constant.Cache_Mark + this.ddlsubcache.SelectedValue;
            string authCode = MemCachedConfig.AuthCode;
            string str2 = Cryptography.MD5(str1 + authCode);
            string url = this.txtGwUrl.Text.Trim() + string.Format("/tools/SyncLocalCache.ashx?cacheKey={0}&passKey={1}", (object)str1, (object)str2);
            string str3 = string.Empty;
            string msg;
            try
            {
                msg = WebClientHelper.GetString(url, (NameValueCollection)null, "get", Encoding.GetEncoding("gbk"));
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            this.AlertAndRedirect(msg);
        }

        protected void ddlcachetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(this.ddlcachetype.SelectedValue.ToUpper() == "USER_"))
                return;
            this.ddlsubcache.Items.Clear();
            foreach (int num in UserFactory.GetUsers("[status] = 2"))
            {
                string str = string.Format("USER_{0}", (object)num);
                this.ddlsubcache.Items.Add(new ListItem(num.ToString(), str));
            }
        }
    }
}
