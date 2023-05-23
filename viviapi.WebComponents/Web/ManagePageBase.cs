using System;
using System.Web;
using viviapi.BLL;
using viviapi.Model;
using viviapi.SysConfig;

namespace viviapi.WebComponents.Web
{
    public class ManagePageBase : PageBase
    {
        public DateTime sDate = Convert.ToDateTime("2012-01-01");
        public DateTime eDate = Convert.ToDateTime("2115-01-10");
        private Manage _currentManage = (Manage)null;
        public string[] AllowHosts = new string[3]
        {
      "long-bao.com",
      "localhost",
      "127.0.0.1"
        };

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
                if (this._currentManage == null)
                    this._currentManage = ManageFactory.CurrentManage;
                return this._currentManage;
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

        public bool CheckHost
        {
            get
            {
                string host = HttpContext.Current.Request.Url.Host;
                foreach (string str in this.AllowHosts)
                {
                    if (host.ToLower().Contains(str))
                        return true;
                }
                return false;
            }
        }

        public void checkLogin()
        {
            string str = string.Format("/{0}/Login.aspx", (object)RuntimeSetting.ManagePagePath);
            if (DateTime.Now < this.sDate || DateTime.Now > this.eDate)
            {
                HttpContext.Current.Response.Write(string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nalert('试用过期请联系管理员');top.location.href=\"{0}\";\r\n//--></SCRIPT>", (object)str));
                HttpContext.Current.Response.End();
            }
            if (this.IsLogin)
                return;
            HttpContext.Current.Response.Write(string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\ntop.location.href=\"{0}\";\r\n//--></SCRIPT>", (object)str));
            HttpContext.Current.Response.End();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.checkLogin();
        }
    }
}
