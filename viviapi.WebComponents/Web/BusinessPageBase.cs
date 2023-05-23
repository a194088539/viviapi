using System;
using System.Web;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.WebComponents.Web
{
    public class BusinessPageBase : PageBase
    {
        public DateTime sDate = Convert.ToDateTime("2012-01-01");
        public DateTime eDate = Convert.ToDateTime("2112-08-01");

        public bool isSuperAdmin
        {
            get
            {
                return this.currentManage.isSuperAdmin > 0;
            }
        }

        public Manage currentManage
        {
            get
            {
                return ManageFactory.CurrentManage;
            }
        }

        public int ManageId
        {
            get
            {
                return this.currentManage.id;
            }
        }

        public bool IsLogin
        {
            get
            {
                return this.currentManage != null;
            }
        }

        public void checkLogin()
        {
            if (DateTime.Now < this.sDate && DateTime.Now > this.eDate)
            {
                HttpContext.Current.Response.Write(string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert('试用过期请联系管理员');top.location.href=\"{0}\";\r\n//--></SCRIPT>", (object)"/Business/Login.aspx"));
                HttpContext.Current.Response.End();
            }
            if (this.IsLogin)
                return;
            string s = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\ntop.location.href=\"{0}\";\r\n//--></SCRIPT>", (object)"/Business/Login.aspx");
            if (HttpContext.Current.Request.RawUrl.ToLower().IndexOf("agent") > 0)
                s = string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\ntop.location.href=\"{0}\";\r\n//--></SCRIPT>", (object)"/agent/Login.aspx");
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.checkLogin();
        }
    }
}
