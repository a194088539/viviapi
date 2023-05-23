using System;
using System.Web;
using System.Web.UI;
using viviapi.Model.User;
using viviLib;

namespace viviapi.BLL
{
    public class PUserBasepage : Page
    {
        private int _userid = 0;
        private UserStatusEnum _userstatus = UserStatusEnum.待审核;

        public int UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public UserStatusEnum UserStatus
        {
            get
            {
                return this._userstatus;
            }
            set
            {
                this._userstatus = value;
            }
        }

        public void CheckLogin()
        {
            if (this.IsLogin())
                return;
            string str = string.Empty;
            str = HttpContext.Current.Request.Url.ToString().ToLower().Contains("Logout.aspx") ? "index.html" : XRequest.GetUrl();
            HttpContext.Current.Response.Write("<script>window.location='/Pman/Login.aspx';</script>");
            HttpContext.Current.Response.End();
        }

        public bool IsLogin()
        {
            bool flag = false;
            if (HttpContext.Current.Session["UserId"] != null && HttpContext.Current.Session["UserStatus"] != null && int.Parse(HttpContext.Current.Session["group"].ToString()) == 2)
                flag = true;
            return flag;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.IsLogin())
            {
                this._userid = int.Parse(HttpContext.Current.Session["UserId"].ToString());
                this._userstatus = (UserStatusEnum)int.Parse(HttpContext.Current.Session["UserStatus"].ToString());
            }
            else
                this.CheckLogin();
        }
    }
}
