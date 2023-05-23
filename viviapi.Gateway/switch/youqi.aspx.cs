using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.gateway._switch
{
    public class youqi : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(10013);
            if (cacheModel == null)
                return;
            this.lit_script.Text = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + cacheModel.postBankUrl + "\">" + "<input type=\"hidden\" name=\"version\" value=\"" + this.Request.Form["version"] + "\" />" + "<input type=\"hidden\" name=\"customerid\" value=\"" + this.Request.Form["customerid"] + "\" />" + "<input type=\"hidden\" name=\"sdorderno\" value=\"" + this.Request.Form["sdorderno"] + "\" />" + "<input type=\"hidden\" name=\"total_fee\" value=\"" + this.Request.Form["total_fee"] + "\" />" + "<input type=\"hidden\" name=\"paytype\" value=\"" + this.Request.Form["paytype"] + "\" />" + "<input type=\"hidden\" name=\"notifyurl\" value=\"" + this.Request.Form["notifyurl"] + "\" />" + "<input type=\"hidden\" name=\"returnurl\" value=\"" + this.Request.Form["returnurl"] + "\" />" + "<input type=\"hidden\" name=\"remark\" value=\"" + this.Request.Form["remark"] + "\" />" + "<input type=\"hidden\" name=\"bankcode\" value=\"" + this.Request.Form["bankcode"] + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + this.Request.Form["sign"] + "\" />" + "<input type=\"hidden\" name=\"get_code\" value=\"" + this.Request.Form["get_code"] + "\" />" + "</form>" + "<script>document.forms[0].submit();</script>";
        }
    }
}
