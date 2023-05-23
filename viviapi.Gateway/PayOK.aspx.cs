namespace viviapi.Gateway
{
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Cache;
    using viviapi.Model.Order;
    using viviapi.SysConfig;
    using viviLib.Web;

    public partial class PayOK : Page
    {
        public string completetime = "";
        public HtmlForm form1;
        public Label Labelcss;
        public string LabelJG = "";
        public string Labelvalue = "";
        public string txtorderid = "";
        public string url = "";
        public void BankOrderReturn(OrderBankInfo orderinfo)
        {
            string s = SystemApiHelper.NewBankNoticeUrl(orderinfo, false);
            if (orderinfo.version == "vyb1.00")
            {
                base.Response.Write(s);
            }
            else
            {
                base.Response.Redirect(s, false);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderBank bank = new OrderBank();
            OrderBankInfo orderModel = null;
            if (!string.IsNullOrEmpty(this.orderid))
            {
                orderModel = WebCache.GetCacheService().RetrieveObject(orderid) as OrderBankInfo;
                if (orderModel == null)
                {
                    orderModel = new OrderBank().GetModel(orderid);
                }
            }

            if (orderModel != null)
            {
                if (orderModel.status == 2)
                {
                    orderModel.opstate = "0";
                    if (orderModel.ordertype == 1)
                    {

                        if (!string.IsNullOrEmpty(orderModel.returnurl))
                        {

                            BankOrderReturn(orderModel);
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.AppendFormat("o={0}", orderModel.orderid);
                            builder.AppendFormat("&uo={0}", orderModel.userorder);
                            builder.AppendFormat("&c={0}", orderModel.paymodeId);
                            builder.AppendFormat("&t={0}", orderModel.typeId);
                            builder.AppendFormat("&v={0:f2}", orderModel.realvalue.ToString());
                            builder.AppendFormat("&e={0}", orderModel.msg);
                            builder.AppendFormat("&u={0}", orderModel.userid);
                            builder.AppendFormat("&s={0}", orderModel.status);
                            Response.Redirect(RuntimeSetting.SiteDomain + "/PayResult.aspx?" + builder.ToString(), false);
                        }
                    }
                    if ((orderModel.ordertype == 2) || (orderModel.ordertype == 4))
                    {
                        this.url = "#";
                    }
                }
                else
                {
                    this.LabelJG = "支付失败";
                }
            }
            else
            {
                this.LabelJG = "参数错误";
                this.Labelcss.Text = "<span class=\"icon-big-error mt5\"></span>";
            }
        }

        public string orderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }
    }
}

