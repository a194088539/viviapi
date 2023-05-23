using System;
using System.Web.UI;
using viviapi.ETAPI.Youka;

namespace viviapi.gateway.Ret.Youka
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}
