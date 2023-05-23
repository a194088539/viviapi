namespace viviapi.gateway.Ret.TongLian2
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.TongLian2;

    public class bankNotify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

