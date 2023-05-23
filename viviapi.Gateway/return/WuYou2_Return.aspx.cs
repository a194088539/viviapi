using System;
using System.Web.UI;
using viviapi.ETAPI.tongyi;

namespace viviapi.gateway.Return
{
    public class WuYou2_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new WuYou2API().ReturnBank();
        }
    }
}