using System;
using System.Web.UI;
using viviapi.ETAPI.Longbao;

namespace viviapi.gateway.Ret.Longbao
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}
