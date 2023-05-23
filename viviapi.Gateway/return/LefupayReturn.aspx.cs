using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.Return
{
    public class LefupayReturn : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Lefupayment().ReturnBank();
        }
    }
}
