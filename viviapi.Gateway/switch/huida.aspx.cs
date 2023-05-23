namespace viviapi.Gateway._switch
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;

    public class huida : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(10009);
            if (cacheModel != null)
            {
                string str2 = ((((((((((("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + cacheModel.postBankUrl + "\">\n") + "<input type=\"hidden\" name=\"apiName\" value=\"" + base.Request["apiName"] + "\" />") + "<input type=\"hidden\" name=\"apiVersion\" value=\"" + base.Request["apiVersion"] + "\" />") + "<input type=\"hidden\" name=\"platformID\" value=\"" + base.Request["platformID"] + "\" />") + "<input type=\"hidden\" name=\"merchNo\" value=\"" + base.Request["merchNo"] + "\" />") + "<input type=\"hidden\" name=\"orderNo\" value=\"" + base.Request["orderNo"] + "\" />") + "<input type=\"hidden\" name=\"tradeDate\" value=\"" + base.Request["tradeDate"] + "\" />") + "<input type=\"hidden\" name=\"amt\" value=\"" + base.Request["amt"] + "\" />") + "<input type=\"hidden\" name=\"merchUrl\" value=\"" + base.Request["merchUrl"] + "\" />") + "<input type=\"hidden\" name=\"merchParam\" value=\"" + base.Request["merchParam"] + "\" />") + "<input type=\"hidden\" name=\"bankCode\" value=\"" + base.Request["bankCode"] + "\" />") + "<input type=\"hidden\" name=\"tradeSummary\" value=\"" + base.Request["tradeSummary"] + "\" />";
                if (base.Request["bankCode"] == "MOBOACC")
                {
                    str2 = str2 + "<input type=\"hidden\" name=\"choosePayType\" value=\"" + base.Request["choosePayType"] + "\" />";
                }
                str2 = (str2 + "<input type=\"hidden\" name=\"signMsg\" value=\"" + base.Request["signMsg"] + "\" />") + "</form>" + "<script>document.forms[0].submit();</script>";
                this.lit_script.Text = str2;
            }
        }
    }
}

