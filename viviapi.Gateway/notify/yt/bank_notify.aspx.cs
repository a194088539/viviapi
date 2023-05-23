using System;
using System.Web.UI;
using viviapi.ETAPI.Yt;

namespace viviapi.gateway.notify.yt
{
    public class Bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}