using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.User;
using viviapi.Cache;
using viviapi.ETAPI;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviLib.Web;

namespace viviapi.gateway
{
    public class BankSelect : Page
    {
        protected HtmlForm form1;
        protected HiddenField hftypeid;
        protected Literal litSysOrderId;
        protected Literal litUserOrderId;
        protected Literal litUserId;
        protected Literal litTypeViewName;
        protected Literal litTratAmt;
        protected HtmlTable tb_bank;
        protected HtmlTable tb_alipay;
        protected HtmlTable tb_tenpay;
        protected Button btnCmmit;
        private OrderBankInfo _orderInfo;

        public string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("sysorderid", "");
            }
        }

        public OrderBankInfo orderInfo
        {
            get
            {
                if (this._orderInfo == null && !string.IsNullOrEmpty(this.OrderId))
                {
                    this._orderInfo = WebCache.GetCacheService().RetrieveObject(this.OrderId) as OrderBankInfo;
                    if (this._orderInfo == null)
                        this._orderInfo = new OrderBank().GetModel(this.OrderId);
                }
                return this._orderInfo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            if (this._orderInfo == null)
            {
                this.Response.Write("参数有误！");
                this.Response.End();
            }
            else
            {
                int typeId = this.orderInfo.typeId;
                this.hftypeid.Value = typeId.ToString();
                if (typeId == 102)
                {
                    this.hftypeid.Value = "967";
                    this.tb_bank.Style.Add("display", "");
                }
                if (typeId == 100)
                {
                    this.hftypeid.Value = "993";
                    this.tb_tenpay.Style.Add("display", "");
                }
                if (typeId == 101)
                {
                    this.hftypeid.Value = "992";
                    this.tb_alipay.Style.Add("display", "");
                }
                ChannelTypeInfo cacheModel = ChannelType.GetCacheModel(typeId);
                if (cacheModel != null)
                    this.litTypeViewName.Text = cacheModel.modetypename;
                this.litSysOrderId.Text = this.orderInfo.orderid;
                this.litUserId.Text = this.orderInfo.userid.ToString();
                this.litTratAmt.Text = Decimal.Round(this.orderInfo.refervalue, 2).ToString();
            }
        }

        protected void btnCmmit_Click(object sender, EventArgs e)
        {
            string error = string.Empty;
            string chanelNo = this.Request.Form["pd_FrpId"];
            if (string.IsNullOrEmpty(chanelNo))
            {
                WebUtility.AlertAndRedirect((Page)this, "请选择银行");
            }
            else
            {
                ChannelInfo model = Channel.GetModel(chanelNo, this.orderInfo.userid, false);
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
                    OrderBank orderBank = new OrderBank();
                    UserFactory.GetCacheUserBaseInfo(this.orderInfo.userid);
                    SellFactory.OnlineBankPay(suppid, this.orderInfo.orderid, this.orderInfo.refervalue, this.orderInfo.paymodeId);
                }
            }
        }
    }
}
