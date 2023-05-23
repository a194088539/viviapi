namespace viviapi.WebUI.payresult
{
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using viviapi.BLL;
    using viviapi.BLL.User;
    using viviapi.WebComponents.Web;
    using viviLib.ExceptionHandling;
    using viviLib.Security;

    public class recharge_notify : UserPageBase
    {
        protected HtmlForm form1;
        private static int suppId = 700;

        protected void Page_Load(object sender, EventArgs e)
        {
            string aPIKey = UserFactory.GetModel(base.currentUser.ID).APIKey;
            HttpRequest request = HttpContext.Current.Request;
            string orderId = request.QueryString["orderid"];
            string str3 = request.QueryString["opstate"];
            string s = request.QueryString["ovalue"];
            string str5 = request.QueryString["sign"];
            string supplierOrderId = request.QueryString["ekaorderid"];
            string str7 = request.QueryString["ekatime"];
            string str9 = Cryptography.MD5(string.Format("orderid={0}&opstate={1}&ovalue={2}{3}", new object[] { orderId, str3, s, aPIKey }));
            try
            {
                if (str9 == str5)
                {
                    string opstate = "-1";
                    int status = 4;
                    if (str3.ToLower() == "0")
                    {
                        opstate = "0";
                        status = 2;
                    }
                    new OrderBank().DoBankComplete(suppId, orderId, supplierOrderId, status, opstate, string.Empty, decimal.Parse(s), 0M, false, true);
                    HttpContext.Current.Response.Write("opstate=0");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }
    }
}

