using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.User;
using viviapi.Cache;
using viviapi.ETAPI;
using viviapi.ETAPI.KuaiQian;
using viviapi.Model.Channel;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviapi.SysConfig;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.gateway.links
{
    public class payment : Page
    {
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected TextBox txtUserId;
        protected Repeater rptChannlType;
        protected TextBox txtCardId;
        protected TextBox txtCardPass;
        protected TextBox txtAmt;
        protected Button btnCmmit;

        public int userId
        {
            get
            {
                return WebBase.GetQueryStringInt32("u", 1024);
            }
        }

        public string Ref
        {
            get
            {
                return WebBase.GetQueryStringString("r", "");
            }
        }

        public string Mac
        {
            get
            {
                return WebBase.GetQueryStringString("mac", "");
            }
        }

        private string key
        {
            get
            {
                return "{A1F66B31-2632-4db2-A601-4251557FCEB2}";
            }
        }

        public string CheckKey()
        {
            return Cryptography.MD5(this.key + this.userId.ToString());
        }

        public string GetKey(string str)
        {
            return Cryptography.MD5(this.key + str);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            if (this.CheckKey() != this.Mac)
            {
                this.Response.Write("参数错误");
                this.Response.End();
            }
            this.txtUserId.Text = this.userId.ToString();
            this.txtUserId.Enabled = false;
            this.txtAmt.Attributes["onkeypress"] = "if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}";
            DataTable dataTable = ChannelType.GetCacheList().Copy();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
                int typeId = Convert.ToInt32(dataTable.Rows[index]["typeId"]);
                bool enable = false;
                ChannelType.GetModel(typeId, this.userId, out enable);
                if (!enable)
                    dataTable.Rows[index].Delete();
            }
            dataTable.AcceptChanges();
            this.rptChannlType.DataSource = (object)dataTable;
            this.rptChannlType.DataBind();
        }

        protected void btnCmmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtAmt.Text))
            {
                WebUtility.AlertAndRedirect((Page)this, "请输入金额");
            }
            else
            {
                string str1 = string.Empty;
                string s = this.Request.Form["rdtypeId"];
                int result1 = 0;
                if (int.TryParse(s, out result1))
                    return;
                if (result1 == 100 || result1 == 101 || result1 == 102)
                {
                    Decimal result2 = new Decimal(0);
                    if (!Decimal.TryParse(this.txtAmt.Text, out result2))
                        WebUtility.AlertAndRedirect((Page)this, "请输入正确金额值");
                    else
                        this.Response.Redirect(string.Format("BankSelect.aspx?u={0}&v={1}&mac={2}", (object)this.userId, (object)this.txtAmt.Text, (object)this.GetKey(this.userId.ToString() + this.txtAmt.Text)));
                }
                else
                {
                    int result2 = 0;
                    string _orderid = string.Empty;
                    if (!int.TryParse(this.txtAmt.Text, out result2))
                        WebUtility.AlertAndRedirect((Page)this, "请输入正确面值");
                    else if (string.IsNullOrEmpty(this.txtCardId.Text))
                        WebUtility.AlertAndRedirect((Page)this, "请输入卡号");
                    else if (string.IsNullOrEmpty(this.txtCardPass.Text))
                    {
                        WebUtility.AlertAndRedirect((Page)this, "请输入卡密");
                    }
                    else
                    {
                        string str2 = string.Empty;
                        int num = 0;
                        string chanelNo = result1.ToString("0000") + this.txtAmt.Text;
                        ChannelInfo model = Channel.GetModel(chanelNo, this.userId, false);
                        if (model == null)
                        {
                            str2 = "1063";
                        }
                        else
                        {
                            int? nullable = model.isOpen;
                            if ((nullable.GetValueOrDefault() != 1 ? 1 : (!nullable.HasValue ? 1 : 0)) != 0)
                            {
                                str2 = "1064";
                            }
                            else
                            {
                                nullable = model.supplier;
                                if (nullable.Value <= 0)
                                {
                                    str2 = "1065";
                                }
                                else
                                {
                                    nullable = model.supplier;
                                    num = nullable.Value;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(str2) && num > 0)
                        {
                            UserInfo cacheUserBaseInfo = UserFactory.GetCacheUserBaseInfo(this.userId);
                            OrderCard orderCard = new OrderCard();
                            OrderCardInfo order = new OrderCardInfo();
                            order.orderid = orderCard.GenerateUniqueOrderId(result1);
                            _orderid = order.orderid;
                            order.addtime = DateTime.Now;
                            order.attach = "";
                            order.notifycontext = string.Empty;
                            order.notifycount = 0;
                            order.notifystat = 0;
                            order.notifyurl = string.Empty;
                            order.clientip = ServerVariables.TrueIP;
                            order.completetime = new DateTime?(DateTime.Now);
                            order.ordertype = 1;
                            order.typeId = result1;
                            order.paymodeId = chanelNo;
                            order.payRate = new Decimal(0);
                            order.supplierId = num;
                            order.supplierOrder = string.Empty;
                            order.userid = this.userId;
                            order.userorder = order.orderid;
                            order.refervalue = (Decimal)result2;
                            order.referUrl = this.Ref;
                            order.cardNo = this.txtCardId.Text;
                            order.cardPwd = this.txtCardPass.Text;
                            order.server = new int?(RuntimeSetting.ServerId);
                            order.manageId = cacheUserBaseInfo.manageId;
                            WebCache.GetCacheService().AddObject(order.orderid, (object)order, TransactionSetting.ExpiresTime);
                            orderCard.Insert(order);
                            string errorCode = string.Empty;
                            string str3 = string.Empty;
                            string errmsg = string.Empty;
                            if (num == 70)
                                str2 = new Cared70().CardSend(_orderid, order.cardNo, order.cardPwd, result1, result2, out str3, out errmsg);
                            if (num == 80)
                                str2 = new OfCard().CardSend(_orderid, order.cardNo, order.cardPwd, result1, result2, out errorCode, out str3);
                            if (num == 102)
                                str2 = new viviapi.ETAPI.YeePay.Card().GetPayUrl(_orderid, order.cardNo, order.cardPwd, result1, result2, out str3);
                            if (num == 990 && (result1 == 103 || result1 == 108 || result1 == 113 || result1 == 106))
                                str2 = new ShenZhouXing().GetPayUrl(_orderid, order.cardNo, order.cardPwd, result1, result2, out str3);
                            if (num == 900)
                                str2 = new viviapi.ETAPI.Shengpay.Card().CardSend(_orderid, order.cardNo, order.cardPwd, result1, result2, out str3);
                        }
                        str1 = string.Format("CardPayResult.aspx?o={1}&u={2}&t={3}&c={4}&p={5}v={6}r={7}&mac={8}", (object)_orderid, (object)this.userId, (object)s, (object)this.txtUserId.Text.Trim(), (object)this.txtCardPass.Text.Trim(), (object)this.txtAmt.Text.Trim(), (object)str2);
                    }
                }
            }
        }

        protected void rptChannlType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                ;
        }
    }
}
