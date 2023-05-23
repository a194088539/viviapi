using System;
using System.Web.UI;
using viviapi.ETAPI.haiou;

namespace viviapi.gateway.Ret.haiou
{
    public class Bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}