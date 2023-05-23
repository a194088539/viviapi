namespace viviapi.gateway.Ret.XinFuBao
{
    using System;
    using System.Web.UI;
    using viviapi.ETAPI.XinFuBao;

    public class bank_Return : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new Bank().Ret();
        }
    }
}

