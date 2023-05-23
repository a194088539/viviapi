using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using viviapi.ETAPI._91KA;

namespace viviapi.gateway.notify._91ka
{
    public class notify : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Notify();
        }
    }
}
