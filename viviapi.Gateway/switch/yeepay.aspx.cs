namespace viviapi.Gateway._switch
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;

    public class yeepay : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(10025);
            if (cacheModel != null)
            {
                string str2 = (((((((((((((((((("<form name='payform' action='" + cacheModel.postBankUrl + "' method='POST'>") + "<input type='hidden' name='p0_Cmd' value='Buy'>") + "<input type='hidden' name='p1_MerId' value='" + base.Request.Form["p1_MerId"] + "'>") + "<input type='hidden' name='p2_Order' value='" + base.Request.Form["p2_Order"] + "'>") + "<input type='hidden' name='p3_Amt' value='" + base.Request.Form["p3_Amt"] + "'>") + "<input type='hidden' name='p4_Cur' value='" + base.Request.Form["p4_Cur"] + "'>") + "<input type='hidden' name='p5_Pid' value='" + base.Request.Form["p5_Pid"] + "'>") + "<input type='hidden' name='p6_Pcat' value='" + base.Request.Form["p6_Pcat"] + "'>") + "<input type='hidden' name='p7_Pdesc' value='" + base.Request.Form["p7_Pdesc"] + "'>") + "<input type='hidden' name='p8_Url' value='" + base.Request.Form["p8_Url"] + "'>") + "<input type='hidden' name='p9_SAF' value='" + base.Request.Form["p9_SAF"] + "'>") + "<input type='hidden' name='pa_MP' value='" + base.Request.Form["pa_MP"] + "'>") + "<input type='hidden' name='pd_FrpId' value='" + base.Request.Form["pd_FrpId"] + "'>") + "<input type='hidden' name='pm_Period' value='" + base.Request.Form["pm_Period"] + "'>") + "<input type='hidden' name='pn_Unit' value='" + base.Request.Form["pn_Unit"] + "'>") + "<input type='hidden' name='pr_NeedResponse' value='1'>") + "<input type='hidden' name='hmac' value='" + base.Request.Form["hmac"] + "'>\r") + "<input type='hidden' name='noLoadingPage' value='1'>") + "</form>" + "<script>document.forms[0].submit();</script>";
                this.lit_script.Text = str2;
            }
        }
    }
}

