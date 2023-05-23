using System;
using System.Web.UI;

namespace viviapi.gateway.notify.Huida
{

    public class Bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new viviapi.ETAPI.Huida.pay().Notify();
        }
    }
}


