namespace viviapi.Gateway.notify
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.KuaiQian;

    public class KuaiQian_RMB_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new RMB().Return(false, true);
        }
    }
}

