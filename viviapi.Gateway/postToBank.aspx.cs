using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL.Sys;
using viviapi.ETAPI;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.gateway
{
    public class postToBank : Page
    {
        protected HtmlForm form1;
        protected HyperLink linkPayUrl;

        public string bankcode
        {
            get
            {
                return WebBase.GetQueryStringString("bankcode", string.Empty);
            }
        }

        public int suppId
        {
            get
            {
                return WebBase.GetQueryStringInt32("suppId", 0);
            }
        }

        public Decimal orderAmt
        {
            get
            {
                return WebBase.GetQueryStringDecimal("orderAmt", new Decimal(0));
            }
        }

        public string orderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }

        public string sign
        {
            get
            {
                return WebBase.GetQueryStringString("sign", "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Cryptography.MD5(this.suppId.ToString() + this.orderAmt.ToString() + this.orderid + Constant.ParameterEncryptionKey) == this.sign)
            {
                string str = string.Empty;
                if (this.suppId == 70)
                    str = new YZCHRMB().PayBank(this.orderid, this.orderAmt, this.bankcode, false);
                else if (this.suppId == 90)
                    str = new LianXinRMB().PayBank(this.orderid, this.orderAmt, this.bankcode, false);
                else if (this.suppId == 101)
                    str = new AliPay().GetPayUrl(this.orderid, Convert.ToDouble(this.orderAmt));
                if (string.IsNullOrEmpty(str))
                    return;
                this.linkPayUrl.NavigateUrl = str;
                this.ClientScript.RegisterStartupScript(this.GetType(), "commit", string.Format("\r\n<SCRIPT LANGUAGE='javascript'><!--\r\nlocation.href='{0}';\r\n//--></SCRIPT>", (object)str));
            }
            else
                this.Response.Write("参数错误");
        }
    }
}
