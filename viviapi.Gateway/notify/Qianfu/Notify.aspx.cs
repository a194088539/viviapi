namespace viviapi.gateway.Ret.Qianfu
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Qianfu;

    public class Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

