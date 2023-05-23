using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.Return
{
    public class Bank15173_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}