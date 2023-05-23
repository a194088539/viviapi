using System;
using System.Web.UI;
using viviapi.ETAPI;

namespace viviapi.gateway.Return
{
    public class ten_return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new TenPayRMB().Return(this.Context);
        }
    }
}
