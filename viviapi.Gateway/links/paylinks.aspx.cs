using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using viviapi.BLL.User;
using viviapi.Model.User;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.gateway.links
{
    public class paylinks : Page
    {
        private string SecurityKey = "{5D46E961-1A43-4d5e-97DC-902F3FAFD068}";
        private string key = "{A1F66B31-2632-4db2-A601-4251557FCEB2}";
        protected HtmlForm form1;

        public int HostId
        {
            get
            {
                return WebBase.GetQueryStringInt32("h", 0);
            }
        }

        public bool isHostPay
        {
            get
            {
                return this.HostId > 0;
            }
        }

        public string Mac
        {
            get
            {
                return WebBase.GetQueryStringString("mac", string.Empty);
            }
        }

        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("u", 0);
            }
        }

        public string CheckKey(int id)
        {
            return Cryptography.MD5(this.SecurityKey + id.ToString());
        }

        public string GetKey(int id)
        {
            return Cryptography.MD5(this.key + id.ToString());
        }

        private string CheckHostPay(out int _userId)
        {
            _userId = 0;
            string str = string.Empty;
            if (this.CheckKey(this.HostId).ToLower() != this.Mac.ToLower())
            {
                str = "ERROR 00000 非法参数";
            }
            else
            {
                userHostInfo cacheModel = new userHost().GetCacheModel(this.HostId);
                if (cacheModel == null)
                    str = "ERROR 00001 参数错误";
                else if (cacheModel.status == userHostStatus.已开启)
                    str = "ERROR 00007 链接已关闭";
                else if (this.Request.UrlReferrer == (Uri)null)
                    str = "ERROR 00002 来源为空";
                else if (this.Request.UrlReferrer.Host.ToLower() != cacheModel.hostUrl.ToLower())
                    str = "ERROR 00003 来源错误";
                else
                    _userId = cacheModel.userid.Value;
            }
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            string s1 = string.Empty;
            string str = string.Empty;
            if (this.Request.UrlReferrer != (Uri)null)
                str = this.Request.UrlReferrer.ToString();
            int _userId = 0;
            if (this.isHostPay)
                s1 = this.CheckHostPay(out _userId);
            else
                _userId = this.UserId;
            if (!string.IsNullOrEmpty(s1))
            {
                this.Response.Write(s1);
                this.Response.End();
            }
            string s2;
            if (_userId <= 0)
            {
                s2 = "ERROR 00004 参数错误商户编号";
            }
            else
            {
                UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(_userId);
                if (cacheUserBaseInfo != null)
                {
                    if (cacheUserBaseInfo.Status == 2)
                        ;
                }
                s2 = "ERROR 00006 商户状态不正常";
            }
            if (!string.IsNullOrEmpty(s2))
            {
                this.Response.Write(s2);
                this.Response.End();
            }
            this.Response.Redirect("payment.aspx?u=" + _userId.ToString() + "&r=" + HttpUtility.UrlEncode(str) + "&mac=" + this.GetKey(_userId));
        }
    }
}
