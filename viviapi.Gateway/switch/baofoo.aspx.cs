namespace viviapi.Gateway._switch
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;

    public class baofoo : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(10028);
            if (cacheModel != null)
            {
                string str2 = ((((((((((((((((("<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + cacheModel.postBankUrl + "\">\n") + "<input type=\"hidden\" name=\"MerchantID\" value=\"" + base.Request["MerchantID"] + "\" />") + "<input type=\"hidden\" name=\"PayID\" value=\"" + base.Request["PayID"] + "\" />") + "<input type=\"hidden\" name=\"TradeDate\" value=\"" + base.Request["TradeDate"] + "\" />") + "<input type=\"hidden\" name=\"TransID\" value=\"" + base.Request["TransID"] + "\" />") + "<input type=\"hidden\" name=\"OrderMoney\" value=\"" + base.Request["OrderMoney"] + "\" />") + "<input type=\"hidden\" name=\"ProductName\" value=\"" + base.Request["ProductName"] + "\" />") + "<input type=\"hidden\" name=\"Amount\" value=\"" + base.Request["Amount"] + "\" />") + "<input type=\"hidden\" name=\"ProductLogo\" value=\"" + base.Request["ProductLogo"] + "\" />") + "<input type=\"hidden\" name=\"Username\" value=\"" + base.Request["Username"] + "\" />") + "<input type=\"hidden\" name=\"Email\" value=\"" + base.Request["Email"] + "\" />") + "<input type=\"hidden\" name=\"Mobile\" value=\"" + base.Request["Mobile"] + "\" />") + "<input type=\"hidden\" name=\"AdditionalInfo\" value=\"" + base.Request["AdditionalInfo"] + "\" />") + "<input type=\"hidden\" name=\"Merchant_url\" value=\"" + base.Request["Merchant_url"] + "\" />") + "<input type=\"hidden\" name=\"Return_url\" value=\"" + base.Request["Return_url"] + "\" />") + "<input type=\"hidden\" name=\"Md5Sign\" value=\"" + base.Request["Md5Sign"] + "\" />") + "<input type=\"hidden\" name=\"NoticeType\" value=\"" + base.Request["NoticeType"] + "\" />") + "</form>" + "<script>document.forms[0].submit();</script>";
                this.lit_script.Text = str2;
            }
        }
    }
}

