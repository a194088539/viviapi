using System;
using System.Web.UI;

namespace viviapi.gateway.Return
{
    public class YZCHBank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new viviapi.ETAPI.YZCHRMB().ReturnBank();
        }
    }
}
