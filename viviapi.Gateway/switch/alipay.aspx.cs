using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.gateway._switch
{
    public class alipay : Page
    {
        protected Literal lit_script;

        protected void Page_Load(object sender, EventArgs e)
        {
            SupplierInfo cacheModel = SupplierFactory.GetCacheModel(101);
            if (cacheModel == null)
                return;
            this.lit_script.Text = "<form name=\"alipaysubmit\" id=\"alipaysubmit\" method=\"get\" action=\"" + cacheModel.postBankUrl + "\">" + "<input type=\"hidden\" name=\"partner\" value=\"" + this.Request.QueryString["partner"] + "\" />" + "<input type=\"hidden\" name=\"service\" value=\"" + this.Request.QueryString["service"] + "\" />" + "<input type=\"hidden\" name=\"payment_type\" value=\"" + this.Request.QueryString["payment_type"] + "\" />" + "<input type=\"hidden\" name=\"notify_url\" value=\"" + this.Request.QueryString["notify_url"] + "\" />" + "<input type=\"hidden\" name=\"return_url\" value=\"" + this.Request.QueryString["return_url"] + "\" />" + "<input type=\"hidden\" name=\"seller_email\" value=\"" + this.Request.QueryString["seller_email"] + "\" />" + "<input type=\"hidden\" name=\"out_trade_no\" value=\"" + this.Request.QueryString["out_trade_no"] + "\" />" + "<input type=\"hidden\" name=\"subject\" value=\"" + this.Request.QueryString["subject"] + "\" />" + "<input type=\"hidden\" name=\"total_fee\" value=\"" + this.Request.QueryString["total_fee"] + "\" />" + "<input type=\"hidden\" name=\"body\" value=\"" + this.Request.QueryString["body"] + "\" />" + "<input type=\"hidden\" name=\"paymethod\" value=\"" + this.Request.QueryString["paymethod"] + "\" />" + "<input type=\"hidden\" name=\"defaultbank\" value=\"" + this.Request.QueryString["defaultbank"] + "\" />" + "<input type=\"hidden\" name=\"show_url\" value=\"" + this.Request.QueryString["show_url"] + "\" />" + "<input type=\"hidden\" name=\"sign\" value=\"" + this.Request.QueryString["sign"] + "\" />" + "<input type=\"hidden\" name=\"sign_type\" value=\"" + this.Request.QueryString["sign_type"] + "\" />" + "</form>" + "<script>document.forms[0].submit();</script>";
        }

        public string GetParaVal(string paraName)
        {
            return this.Request.QueryString["paraName"];
        }
    }
}
