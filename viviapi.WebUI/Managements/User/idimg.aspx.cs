using System;
using System.Web.UI;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviLib.Web;

public class Console_User_idimg : Page
{
    public usersIdImageInfo _ItemInfo = (usersIdImageInfo)null;

    public int ItemInfoId
    {
        get
        {
            return WebBase.GetQueryStringInt32("id", 0);
        }
    }

    public string show
    {
        get
        {
            return WebBase.GetQueryStringString("show", "");
        }
    }

    public usersIdImageInfo ItemInfo
    {
        get
        {
            if (this._ItemInfo == null)
                this._ItemInfo = this.ItemInfoId <= 0 ? new usersIdImageInfo() : new usersIdImage().Get(this.ItemInfoId);
            return this._ItemInfo;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.show == "on" && this.ItemInfo != null)
        {
            this.Response.Clear();
            this.Response.ContentType = this.ItemInfo.ptype;
            this.Response.OutputStream.Write(this.ItemInfo.image_on, 0, this.ItemInfo.filesize.Value);
            this.Response.End();
        }
        if (!(this.show == "down") || this.ItemInfo == null)
            return;
        this.Response.Clear();
        this.Response.ContentType = this.ItemInfo.ptype;
        this.Response.OutputStream.Write(this.ItemInfo.image_down, 0, this.ItemInfo.filesize1.Value);
        this.Response.End();
    }
}
