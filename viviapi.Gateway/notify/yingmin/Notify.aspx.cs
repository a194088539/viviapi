namespace viviapi.gateway.Ret.yingmin
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.Yingmin;

    public class Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

