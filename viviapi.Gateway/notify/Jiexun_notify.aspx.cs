using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class Jiexun_notify : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            new Jiexun().Notify();
        }
    }
}
