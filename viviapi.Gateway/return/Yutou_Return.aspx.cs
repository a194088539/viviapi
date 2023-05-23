using System;
using System.Web.UI;
using viviapi.ETAPI.Yutou;

namespace viviapi.gateway.Return
{
    public class Yutou_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().ReturnBank();
        }
    }
}