namespace viviapi.gateway.Ret.XinFuBao
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.XinFuBao;

    public class Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Code().Notify();
        }
    }
}

