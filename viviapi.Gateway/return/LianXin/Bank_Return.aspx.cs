using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.Ret.LianXin
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new LianXinRMB().ReturnBank();
        }
    }
}
