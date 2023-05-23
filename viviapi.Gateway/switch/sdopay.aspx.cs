using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.gateway._switch
{
    public class sdopay : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(900);
            if (cacheModel == null)
                return;
            this.lit_script.Text = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + cacheModel.postBankUrl + "\">" + "<input type=\"hidden\" name=\"Version\" value=\"" + this.Request.Form["Version"] + "\" />" + "<input type=\"hidden\" name=\"Amount\" value=\"" + this.Request.Form["Amount"] + "\" />" + "<input type=\"hidden\" name=\"OrderNo\" value=\"" + this.Request.Form["OrderNo"] + "\" />" + "<input type=\"hidden\" name=\"MerchantNo\" value=\"" + this.Request.Form["MerchantNo"] + "\" />" + "<input type=\"hidden\" name=\"MerchantUserId\" value=\"" + this.Request.Form["MerchantUserId"] + "\" />" + "<input type=\"hidden\" name=\"PayChannel\" value=\"" + this.Request.Form["PayChannel"] + "\" />" + "<input type=\"hidden\" name=\"PostBackUrl\" value=\"" + this.Request.Form["PostBackUrl"] + "\" />" + "<input type=\"hidden\" name=\"NotifyUrl\" value=\"" + this.Request.Form["NotifyUrl"] + "\" />" + "<input type=\"hidden\" name=\"BackUrl\" value=\"" + this.Request.Form["BackUrl"] + "\" />" + "<input type=\"hidden\" name=\"OrderTime\" value=\"" + this.Request.Form["OrderTime"] + "\" />" + "<input type=\"hidden\" name=\"CurrencyType\" value=\"" + this.Request.Form["CurrencyType"] + "\" />" + "<input type=\"hidden\" name=\"NotifyUrlType\" value=\"" + this.Request.Form["NotifyUrlType"] + "\" />" + "<input type=\"hidden\" name=\"SignType\" value=\"" + this.Request.Form["SignType"] + "\" />" + "<input type=\"hidden\" name=\"ProductNo\" value=\"" + this.Request.Form["ProductNo"] + "\" />" + "<input type=\"hidden\" name=\"ProductDesc\" value=\"" + this.Request.Form["ProductDesc"] + "\" />" + "<input type=\"hidden\" name=\"Remark1\" value=\"" + this.Request.Form["Remark1"] + "\" />" + "<input type=\"hidden\" name=\"Remark2\" value=\"" + this.Request.Form["Remark2"] + "\" />" + "<input type=\"hidden\" name=\"BankCode\" value=\"" + this.Request.Form["BankCode"] + "\" />" + "<input type=\"hidden\" name=\"DefaultChannel\" value=\"" + this.Request.Form["DefaultChannel"] + "\" />" + "<input type=\"hidden\" name=\"ExterInvokeIp\" value=\"" + this.Request.Form["ExterInvokeIp"] + "\" />" + "<input type=\"hidden\" name=\"CharSet\" value=\"" + this.Request.Form["CharSet"] + "\" />" + "<input type=\"hidden\" name=\"MAC\" value=\"" + this.Request.Form["MAC"] + "\" />" + "</form>" + "<script>document.forms[0].submit();</script>";
        }
    }
}
