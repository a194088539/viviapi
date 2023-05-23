using System;
using System.Web.UI;
using viviapi.ETAPI.Youka;

namespace viviapi.gateway.notify.Youka
{
    public class Bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
