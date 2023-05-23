using System;
using System.Web.UI;
using viviapi.ETAPI._91KA;

namespace viviapi.gateway._return._91ka
{
    public class Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}
