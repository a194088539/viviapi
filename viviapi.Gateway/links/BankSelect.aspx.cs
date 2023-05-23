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
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.Web;

namespace viviapi.gateway.links
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

        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("u", 0);
            }
        }

        public int typeId
        {
            get
            {
                return WebBase.GetQueryStringInt32("t", 0);
            }
        }

        public string Mac
        {
            get
            {
                return WebBase.GetQueryStringString("k", "");
            }
        }

        public int OrderType
        {
            get
            {
                return WebBase.GetQueryStringInt32("c", 2);
            }
        }

        public string TraAmt
        {
            get
            {
                return WebBase.GetQueryStringString("v", "");
            }
        }

        public string OrderId
        {
            get
            {
                return (string)this.ViewState["sysOrderId"];
            }
            set
            {
                this.ViewState["sysOrderId"] = (object)value;
            }
        }

        public string UrlRef
        {
            get
            {
                return WebBase.GetQueryStringString("r", "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            string s = string.Empty;
            string[] strArray1 = new string[5];
            string[] strArray2 = strArray1;
            int index1 = 0;
            int num = this.UserId;
            string str1 = num.ToString();
            strArray2[index1] = str1;
            string[] strArray3 = strArray1;
            int index2 = 1;
            num = this.typeId;
            string str2 = num.ToString();
            strArray3[index2] = str2;
            string[] strArray4 = strArray1;
            int index3 = 2;
            num = this.OrderType;
            string str3 = num.ToString();
            strArray4[index3] = str3;
            strArray1[3] = this.UrlRef;
            strArray1[4] = this.TraAmt;
            if (!WebUtility.CheckKey(string.Concat(strArray1), this.Mac))
                s = "ERROR 000001 参数错误";
            if (!string.IsNullOrEmpty(s))
            {
                this.Response.Write(s);
                this.Response.End();
            }
            else
            {
                HiddenField hiddenField = this.hftypeid;
                num = this.typeId;
                string str4 = num.ToString();
                hiddenField.Value = str4;
                if (this.typeId == 102)
                {
                    this.hftypeid.Value = "967";
                    this.tb_bank.Style.Add("display", "");
                }
                if (this.typeId == 100)
                {
                    this.hftypeid.Value = "993";
                    this.tb_tenpay.Style.Add("display", "");
                }
                if (this.typeId == 101)
                {
                    this.hftypeid.Value = "992";
                    this.tb_alipay.Style.Add("display", "");
                }
                ChannelTypeInfo cacheModel = ChannelType.GetCacheModel(this.typeId);
                if (cacheModel != null)
                    this.litTypeViewName.Text = cacheModel.modetypename;
                this.OrderId = new OrderBank().GenerateUniqueOrderId(this.typeId);
                this.litSysOrderId.Text = this.OrderId;
                Literal literal = this.litUserId;
                num = this.UserId;
                string str5 = num.ToString();
                literal.Text = str5;
                this.litTratAmt.Text = Decimal.Round(Decimal.Parse(this.TraAmt), 2).ToString();
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
                ChannelInfo model = Channel.GetModel(chanelNo, this.UserId, false);
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
                    UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(this.UserId);
                    OrderBankInfo order = new OrderBankInfo();
                    order.orderid = this.OrderId;
                    order.addtime = DateTime.Now;
                    order.attach = "";
                    order.notifycontext = string.Empty;
                    order.notifycount = 0;
                    order.notifystat = 0;
                    order.notifyurl = string.Empty;
                    order.clientip = ServerVariables.TrueIP;
                    order.completetime = new DateTime?(DateTime.Now);
                    order.returnurl = string.Empty;
                    order.ordertype = this.OrderType;
                    order.typeId = this.typeId;
                    order.paymodeId = chanelNo;
                    order.supplierId = suppid;
                    order.supplierOrder = string.Empty;
                    order.clientip = ServerVariables.TrueIP;
                    order.userid = this.UserId;
                    order.userorder = string.Empty;
                    order.refervalue = Decimal.Parse(this.TraAmt);
                    order.referUrl = this.UrlRef;
                    order.server = new int?(RuntimeSetting.ServerId);
                    order.manageId = cacheUserBaseInfo.manageId;
                    int? manageId = order.manageId;
                    int num;
                    if (manageId.HasValue)
                    {
                        manageId = order.manageId;
                        num = manageId.Value > 0 ? 1 : 0;
                    }
                    else
                        num = 0;
                    if (num == 0)
                        order.agentId = UserFactory.GetPromID(this.UserId);
                    WebCache.GetCacheService().AddObject(order.orderid, (object)order, TransactionSetting.ExpiresTime);
                    orderBank.Insert(order);
                    SellFactory.OnlineBankPay(suppid, order.orderid, order.refervalue, order.paymodeId);
                }
            }
        }
    }
}
