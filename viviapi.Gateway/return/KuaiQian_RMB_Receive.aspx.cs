namespace viviapi.Gateway.Return
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.KuaiQian;

    public class KuaiQian_RMB_Receive : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new RMB().Return(true, false);
        }
    }
}

