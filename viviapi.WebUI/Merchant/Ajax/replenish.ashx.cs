namespace viviapi.WebUI.Merchant.Ajax
{
    using System;
    using System.Web;
    using System.Web.SessionState;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviLib.Web;

    public class replenish : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = "";
            try
            {
                if (UserFactory.CurrentMember == null)
                {
                    s = "登录信息失效，请重新登录";
                }
                else if ((this.type == 0) || string.IsNullOrEmpty(this.orderid))
                {
                    s = "参数不正确";
                }
                else if (this.type == 1)
                {
                    s = new OrderBankNotify().SynchronousNotify(this.orderid);
                }
                else if (this.type == 2)
                {
                    s = new OrderCardNotify().SynchronousNotify(this.orderid);
                }
            }
            catch (Exception exception)
            {
                s = exception.Message;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string orderid
        {
            get
            {
                return WebBase.GetQueryStringString("order", "");
            }
        }

        public int type
        {
            get
            {
                return WebBase.GetQueryStringInt32("type", 0);
            }
        }
    }
}

