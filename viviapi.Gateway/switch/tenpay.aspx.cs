using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.gateway._switch
{
    public class tenpay : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(100);
            if (cacheModel == null)
                return;
            this.lit_script.Text = "<form name=\"frm1\" id=\"frm1\" method=\"post\" action=\"" + cacheModel.postBankUrl + "\">" + "<input type=\"hidden\" name=\"partner\" value=\"" + this.Request.Form["partner"] + "\" />" + "<input type=\"hidden\" name=\"out_trade_no\" value=\"" + this.Request.Form["out_trade_no"] + "\" />" + "<input type=\"hidden\" name=\"total_fee\" value=\"" + this.Request.Form["total_fee"] + "\" />" + "<input type=\"hidden\" name=\"return_url\" value=\"" + this.Request.Form["return_url"] + "\" />" + "<input type=\"hidden\" name=\"notify_url\" value=\"" + this.Request.Form["notify_url"] + "\" />" + "<input type=\"hidden\" name=\"body\" value=\"" + this.Request.Form["body"] + "\" />" + "<input type=\"hidden\" name=\"bank_type\" value=\"" + this.Request.Form["bank_type"] + "\" />" + "<input type=\"hidden\" name=\"spbill_create_ip\" value=\"" + this.Request.Form["spbill_create_ip"] + "\" />" + "<input type=\"hidden\" name=\"fee_type\" value=\"" + this.Request.Form["fee_type"] + "\" />" + "<input type=\"hidden\" name=\"subject\" value=\"" + this.Request.Form["subject"] + "\" />" + "<input type=\"hidden\" name=\"sign_type\" value=\"" + this.Request.Form["sign_type"] + "\" />" + "<input type=\"hidden\" name=\"service_version\" value=\"" + this.Request.Form["service_version"] + "\" />" + "<input type=\"hidden\" name=\"input_charset\" value=\"" + this.Request.Form["input_charset"] + "\" />" + "<input type=\"hidden\" name=\"sign_key_index\" value=\"" + this.Request.Form["sign_key_index"] + "\" />" + "<input type=\"hidden\" name=\"ProductDesc\" value=\"" + this.Request.Form["ProductDesc"] + "\" />" + "<input type=\"hidden\" name=\"attach\" value=\"" + this.Request.Form["attach"] + "\" />" + "<input type=\"hidden\" name=\"product_fee\" value=\"" + this.Request.Form["product_fee"] + "\" />" + "<input type=\"hidden\" name=\"transport_fee\" value=\"" + this.Request.Form["transport_fee"] + "\" />" + "<input type=\"hidden\" name=\"time_start\" value=\"" + this.Request.Form["time_start"] + "\" />" + "<input type=\"hidden\" name=\"time_expire\" value=\"" + this.Request.Form["time_expire"] + "\" />" + "<input type=\"hidden\" name=\"buyer_id\" value=\"" + this.Request.Form["buyer_id"] + "\" />" + "<input type=\"hidden\" name=\"goods_tag\" value=\"" + this.Request.Form["goods_tag"] + "\" />" + "<input type=\"hidden\" name=\"trade_mode\" value=\"" + this.Request.Form["trade_mode"] + "\" />" + "<input type=\"hidden\" name=\"transport_desc\" value=\"" + this.Request.Form["transport_desc"] + "\" />" + "<input type=\"hidden\" name=\"trans_type\" value=\"" + this.Request.Form["trans_type"] + "\" />" + "<input type=\"hidden\" name=\"agentid\" value=\"" + this.Request.Form["agentid"] + "\" />" + "<input type=\"hidden\" name=\"agent_type\" value=\"" + this.Request.Form["agent_type"] + "\" />" + "<input type=\"hidden\" name=\"seller_id\" value=\"" + this.Request.Form["seller_id"] + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + this.Request.Form["sign"] + "\" />" + "</form>" + "<script>document.forms[0].submit();</script>";
        }
    }
}
