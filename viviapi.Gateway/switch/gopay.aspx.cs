namespace viviapi.Gateway._switch
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;

    public class gopay : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(10021);
            if (cacheModel != null)
            {
                string s = ((((((((((((((((((("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + cacheModel.postBankUrl + "\">") + "<input type=\"hidden\" name=\"Mer_code\" value=\"" + base.Request.Form["Mer_code"] + "\" />") + "<input type=\"hidden\" name=\"Billno\" value=\"" + base.Request.Form["Billno"] + "\" />") + "<input type=\"hidden\" name=\"Amount\" value=\"" + base.Request.Form["Amount"] + "\" />") + "<input type=\"hidden\" name=\"Date\" value=\"" + base.Request.Form["Date"] + "\" />") + "<input type=\"hidden\" name=\"Currency_Type\" value=\"" + base.Request.Form["Currency_Type"] + "\" />") + "<input type=\"hidden\" name=\"Gateway_Type\" value=\"" + base.Request.Form["Gateway_Type"] + "\" />") + "<input type=\"hidden\" name=\"Merchanturl\" value=\"" + base.Request.Form["Merchanturl"] + "\" />") + "<input type=\"hidden\" name=\"FailUrl\" value=\"" + base.Request.Form["FailUrl"] + "\" />") + "<input type=\"hidden\" name=\"ErrorUrl\" value=\"" + base.Request.Form["ErrorUrl"] + "\" />") + "<input type=\"hidden\" name=\"Attach\" value=\"" + base.Request.Form["Attach"] + "\" />") + "<input type=\"hidden\" name=\"DispAmount\" value=\"" + base.Request.Form["DispAmount"] + "\" />") + "<input type=\"hidden\" name=\"OrderEncodeType\" value=\"" + base.Request.Form["OrderEncodeType"] + "\" />") + "<input type=\"hidden\" name=\"RetEncodeType\" value=\"" + base.Request.Form["RetEncodeType"] + "\" />") + "<input type=\"hidden\" name=\"ServerUrl\" value=\"" + base.Request.Form["ServerUrl"] + "\" />") + "<input type=\"hidden\" name=\"SignMD5\" value=\"" + base.Request.Form["SignMD5"] + "\" />") + "<input type=\"hidden\" name=\"DoCredit\" value=\"" + base.Request.Form["DoCredit"] + "\" />") + "<input type=\"hidden\" name=\"Bankco\" value=\"" + base.Request.Form["Bankco"] + "\" />") + "<input type=\"hidden\" name=\"RetType\" value=\"" + base.Request.Form["RetType"] + "\" />") + "</form>" + "<script>document.forms[0].submit();</script>";
                base.Response.Write(s);
            }
        }
    }
}

