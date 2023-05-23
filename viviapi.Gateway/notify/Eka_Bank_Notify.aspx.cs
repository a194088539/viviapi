using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class Eka_Bank_Notify : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new EkaBank().Notify();
        }
    }
}
