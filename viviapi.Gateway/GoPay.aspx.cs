using System;
using System.Web.UI;
using viviapi.BLL.Sys;
using viviapi.ETAPI;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.gateway
{
    public class GoPay : Page
    {
        public string SubForm = string.Empty;

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
                if (this.suppId == 70)
                    this.SubForm = new YZCHRMB().PayBank(this.orderid, this.orderAmt, this.bankcode, true);
                else if (this.suppId == 90)
                    this.SubForm = new LianXinRMB().PayBank(this.orderid, this.orderAmt, this.bankcode, true);
                else if (this.suppId == 100)
                    this.SubForm = new TenPayRMB().GetPayForm(this.orderid, this.orderAmt, this.bankcode, this.Context);
                else if (this.suppId == 101)
                    this.SubForm = new AliPay().GetPayForm(this.orderid, Convert.ToDouble(this.orderAmt), false);
                else if (this.suppId == 600)
                    this.SubForm = new IPS().GetPayForm(this.orderid, this.orderAmt, this.bankcode);
                else if (this.suppId == 900)
                    this.SubForm = new viviapi.ETAPI.Shengpay.Bank().PayBank(this.orderid, this.orderAmt, this.bankcode, true);
                else if (this.suppId == 1001)
                    this.SubForm = new EasyPay().PayBank(this.orderid, this.orderAmt, this.bankcode);
                else if (this.suppId == 1001)
                    this.SubForm = new Gopay().GetPayForm(this.orderid, this.orderAmt, this.bankcode);
                else if (this.suppId == 1002)
                    this.SubForm = new Hnapay().GetPayForm(this.orderid, this.orderAmt, this.bankcode);
                else if (this.suppId == 1003)
                {
                    this.SubForm = new viviapi.ETAPI.Baofoo.Bank().PayBank(this.orderid, this.orderAmt, this.bankcode, true);
                }
                else
                {
                    if (this.suppId != 60866)
                        return;
                    this.SubForm = new Bank60866().PayBank(this.orderid, this.orderAmt, this.bankcode, true);
                }
            }
            else
                this.Response.Write("参数错误");
        }
    }
}
