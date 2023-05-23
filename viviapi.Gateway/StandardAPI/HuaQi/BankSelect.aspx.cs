using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.Order;
using viviapi.BLL.User;
using viviapi.Cache;
using viviapi.ETAPI;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviLib.Web;

namespace viviapi.gateway.StandardAPI.HuaQi
{
    public class BankSelect : Page
    {
        private OrderBankInfo _orderinfo = (OrderBankInfo)null;
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected HiddenField hftypeid;
        protected HtmlGenericControl lblorderid;
        protected HtmlGenericControl lblsysorderid;
        protected HtmlGenericControl lblordermoney;
        protected HtmlGenericControl UL1;
        protected HtmlGenericControl UL2;
        protected HtmlGenericControl UL3;
        protected ImageButton ibtnnext;

        public string orderId
        {
            get
            {
                return WebBase.GetQueryStringString("sysorderid", "");
            }
        }

        public OrderBankInfo orderinfo
        {
            get
            {
                if (this._orderinfo == null && !string.IsNullOrEmpty(this.orderId))
                {
                    this._orderinfo = WebCache.GetCacheService().RetrieveObject(this.orderId) as OrderBankInfo;
                    if (this._orderinfo == null)
                        this._orderinfo = new OrderBank().GetModel(this.orderId);
                }
                return this._orderinfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            if (this.orderinfo == null)
            {
                this.Response.Write("参数有误！");
                this.Response.End();
            }
            else
            {
                this.lblorderid.InnerText = this.orderinfo.userorder;
                this.lblsysorderid.InnerText = this.orderId;
                this.lblordermoney.InnerText = this.orderinfo.refervalue.ToString("f2");
                int typeId = this.orderinfo.typeId;
                this.hftypeid.Value = typeId.ToString();
                if (typeId == 102)
                {
                    this.hftypeid.Value = "967";
                    this.UL1.Visible = true;
                }
                if (typeId == 100)
                {
                    this.hftypeid.Value = "993";
                    this.UL3.Visible = true;
                }
                if (typeId == 101)
                {
                    this.hftypeid.Value = "992";
                    this.UL2.Visible = true;
                }
            }
        }

        protected void ibtnnext_Click(object sender, ImageClickEventArgs e)
        {
            string error = string.Empty;
            string str = this.Request.Form["pd_FrpId"];
            if (string.IsNullOrEmpty(str))
            {
                WebUtility.AlertAndRedirect((Page)this, "请选择银行");
            }
            else
            {
                ChannelInfo model = Channel.GetModel(str, this.orderinfo.userid, true);
                if (model == null)
                    error = "error:1067:银行编号不存在!";
                else if (model.isOpen.Value != 1)
                    error = "error:1068:通道维护中!";
                if (!string.IsNullOrEmpty(error))
                {
                    WebUtility.ShowErrorMsg(error);
                }
                else
                {
                    int suppid = model.supplier.Value;
                    Dal.Update(this.orderId, str, suppid);
                    OrderBank orderBank = new OrderBank();
                    UserFactory.GetCacheUserBaseInfo(this.orderinfo.userid);
                    SellFactory.OnlineBankPay(suppid, this.orderinfo.orderid, this.orderinfo.refervalue, str);
                }
            }
        }
    }
}
