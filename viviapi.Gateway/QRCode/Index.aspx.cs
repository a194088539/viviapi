namespace viviapi.gateway.QRCode
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using viviapi.BLL.Sys;
    using viviapi.ETAPI.WxPayAPI;
    using viviLib.Security;
    using viviLib.Web;

    public partial class Index : Page
    {
        protected HtmlLink linkPaymentDialogCss1;
        protected HtmlLink linkWebCss1;
        protected HtmlLink linkWeixinCss1;
        protected string url = "";
        protected string wxorderid = "0";

        protected void Page_Init(object sender, EventArgs e)
        {
            if (bankcode == "992")
            {
                Label1.Text = "支付宝";
            }
            if (bankcode == "1004")
            {
                Label1.Text = "微信";
            }
            if (bankcode == "1005")
            {
                Label1.Text = "手机网银";
            }
            if (bankcode == "1006")
            {
                Label1.Text = "支付宝";
            }
            if (bankcode == "1007")
            {
                Label1.Text = "微信";
            }
            if (bankcode == "2005")
            {
                Label1.Text = "微信";
            }
            if (bankcode == "1008")
            {
                Label1.Text = "QQ钱包";
            }
            if (bankcode == "1009")
            {
                Label1.Text = "QQ钱包";
            }
            if (bankcode == "2001")
            {
                Label1.Text = "京东钱包";
            }
            if (bankcode == "2002")
            {
                Label1.Text = "京东钱包";
            }
            if (bankcode == "2003")
            {
                Label1.Text = "银联钱包";
            }
            if (bankcode == "2006")
            {
                Label1.Text = "百度钱包";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Cryptography.MD5(this.suppId.ToString() + this.orderAmt.ToString() + this.orderid + bankcode + payurl + Constant.ParameterEncryptionKey) == this.sign)
            {
                //if (suppId == 10012 || suppId == 6002)
                //{
                //Label1.Text = "";
                this.wxorderid = this.orderid;
                Log.Info(base.GetType().ToString(), "page load");
                this.shijian.Text = string.Format("{0:yyyy-MM-dd:HH:mm:ss}", DateTime.Now);
                string orderAmt = decimal.Round(this.orderAmt * 100M, 0).ToString();
                this.LabelAmt.Text = this.orderAmt.ToString();
                this.Labelno.Text = this.orderid;
                this.Image2.ImageUrl = payurl;
                //}
                //else
                //{
                //    this.wxorderid = this.orderid;
                //    Log.Info(base.GetType().ToString(), "page load");
                //    NativePay pay = new NativePay();
                //    this.shijian.Text = string.Format("{0:yyyy-MM-dd:HH:mm:ss}", DateTime.Now);
                //    string orderAmt = decimal.Round(this.orderAmt * 100M, 0).ToString();
                //    this.LabelAmt.Text = this.orderAmt.ToString();
                //    this.Labelno.Text = this.orderid;

                //    string payUrl = pay.GetPayUrl(this.orderid, orderAmt);
                //    this.Image2.ImageUrl = "MakeQRCode.aspx?data=" + HttpUtility.UrlEncode(payUrl);
                //}
            }
        }
        //}

        public string bankcode
        {
            get
            {
                return WebBase.GetQueryStringString("bankcode", string.Empty);
            }
        }

        public decimal orderAmt
        {
            get
            {
                return WebBase.GetQueryStringDecimal("orderAmt", 0M);
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

        public int suppId
        {
            get
            {
                return WebBase.GetQueryStringInt32("suppId", 0);
            }
        }
        public string payurl
        {
            get
            {
                return WebBase.GetQueryStringString("payurl", "");
            }
        }
    }
}

