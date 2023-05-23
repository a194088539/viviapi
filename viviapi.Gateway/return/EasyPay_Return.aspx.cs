using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using viviapi.ETAPI;

namespace viviapi.gateway.Return
{
    public class EasyPay_Return : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            new EasyPay().ReturnBank();
        }
    }
}
