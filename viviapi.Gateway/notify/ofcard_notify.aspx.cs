using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class ofcard_notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new OfCard().Notify();
        }
    }
}
