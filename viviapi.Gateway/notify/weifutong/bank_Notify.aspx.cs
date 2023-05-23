namespace viviapi.gateway.Ret.weifutong
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.weifutong;

    public class Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

