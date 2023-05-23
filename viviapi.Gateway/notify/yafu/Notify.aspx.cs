namespace viviapi.gateway.Ret.yafu
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.YaFu;

    public class Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}

