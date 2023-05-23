namespace viviapi.Gateway._switch
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;

    public class dinpay : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(10015);
            if (cacheModel != null)
            {
                string str2 = ((((((((((((((((((((((("<form name=\"pay\" id=\"pay\" method=\"post\" action=\"" + (cacheModel.postBankUrl + "?input_charset=UTF-8") + "\">\n") + "<input type=\"hidden\" name=\"sign\" value=\"" + base.Request["sign"] + "\" />\n") + "<input type=\"hidden\" name=\"merchant_code\" value=\"" + base.Request["merchant_code"] + "\" />\n") + "<input type=\"hidden\" name=\"bank_code\" value=\"" + base.Request["bank_code"] + "\" />\n") + "<input type=\"hidden\" name=\"order_no\" value=\"" + base.Request["order_no"] + "\" />\n") + "<input type=\"hidden\" name=\"order_amount\" value=\"" + base.Request["order_amount"] + "\" />\n") + "<input type=\"hidden\" name=\"service_type\" value=\"" + base.Request["service_type"] + "\" />\n") + "<input type=\"hidden\" name=\"notify_url\" value=\"" + base.Request["notify_url"] + "\" />\n") + "<input type=\"hidden\" name=\"interface_version\" value=\"" + base.Request["interface_version"] + "\" />\n") + "<input type=\"hidden\" name=\"sign_type\" value=\"" + base.Request["sign_type"] + "\" />\n") + "<input type=\"hidden\" name=\"order_time\" value=\"" + base.Request["order_time"] + "\" />\n") + "<input type=\"hidden\" name=\"product_name\" value=\"" + base.Request["product_name"] + "\" />\n") + "<input type=\"hidden\" name=\"client_ip_check\" value=\"" + base.Request["client_ip_check"] + "\" />\n") + "<input type=\"hidden\" name=\"client_ip\" value=\"" + base.Request["client_ip"] + "\" />\n") + "<input type=\"hidden\" name=\"extend_param\" value=\"" + base.Request["extend_param"] + "\" />\n") + "<input type=\"hidden\" name=\"extra_return_param\" value=\"" + base.Request["extra_return_param"] + "\" />\n") + "<input type=\"hidden\" name=\"product_code\" value=\"" + base.Request["product_code"] + "\" />\n") + "<input type=\"hidden\" name=\"product_desc\" value=\"" + base.Request["product_desc"] + "\" />\n") + "<input type=\"hidden\" name=\"product_num\" value=\"" + base.Request["product_num"] + "\" />\n") + "<input type=\"hidden\" name=\"return_url\" value=\"" + base.Request["return_url"] + "\" />\n") + "<input type=\"hidden\" name=\"show_url\" value=\"" + base.Request["show_url"] + "\" />\n") + "<input type=\"hidden\" name=\"redo_flag\" value=\"" + base.Request["redo_flag"] + "\" />\n") + "<input type=\"hidden\" name=\"pay_type\" value=\"" + base.Request["pay_type"] + "\" />\n") + "</form>" + "<script>setTimeout(\"document.getElementById('pay').submit();\",100);</script>";
                this.lit_script.Text = str2;
            }
        }
    }
}

