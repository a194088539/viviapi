namespace viviapi.gateway.Ret.TongLian
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.TongLian;

    public class Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Code().Notify();
        }
    }
}

