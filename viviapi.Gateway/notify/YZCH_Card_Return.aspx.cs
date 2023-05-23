using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.notify
{
    public class YZCH_Card_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Cared70().CardNotify();
        }
    }
}
