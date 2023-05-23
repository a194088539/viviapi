namespace viviapi.Gateway.Return
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.KuaiQian;

    public class KuaiQian_RMB_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new RMB().Return(false, false);
        }
    }
}

