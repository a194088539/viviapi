using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.Ret
{
    public class Eka_Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new EkaBank().ReturnBank();
        }
    }
}
