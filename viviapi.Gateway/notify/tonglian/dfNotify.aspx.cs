namespace viviapi.gateway.Ret.TongLian
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.TongLian;

    public class dfNotify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new daifu().Notify();
        }
    }
}

