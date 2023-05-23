using System;
using System.Web.UI;
using viviapi.ETAPI.Youqi;

namespace viviapi.gateway.notify.youqi
{
    public class Bank_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}