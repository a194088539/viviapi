namespace viviapi.Gateway._switch
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;

    public class ips70pay : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(10023);
            if (cacheModel != null)
            {
                string req = base.Request.Form["pGateWayReq"];
                string str2 = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + cacheModel.postBankUrl + "\">" + "<input type=\"hidden\" name=\"pGateWayReq\" value=\"" + req + "\" />" + "</form>" + "<script type=\"text/javascript\" language=\"javascript\">setTimeout(\"document.getElementById('frm1').submit();\",100);</script>";
                this.lit_script.Text = str2;
            }
        }
    }
}

