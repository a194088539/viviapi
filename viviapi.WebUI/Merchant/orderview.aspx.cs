using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Channel;
using viviapi.BLL.User;
using viviapi.Model.Order;
using viviapi.WebComponents.Web;
using viviLib.TimeControl;
using viviLib.Web;

namespace viviapi.WebUI.LongBao.merchant
{
    public class orderview : UserPageBase
    {
        protected string referUrl = string.Empty;
        protected string notifyurl = string.Empty;
        protected HtmlForm form1;
        protected Label lblorderid;
        protected Label lblordertype;
        protected Label lbltypeId;
        protected Label lblpaymodeId;
        protected Label lbluserorder;
        protected Label lblrefervalue;
        protected Label lblnotifycount;
        protected Label lblnotifystat;
        protected Label lblversion;
        protected Label lblnotifycontext;
        protected Label lblattach;
        protected Label lblpayerip;
        protected Label lbladdtime;
        protected Label lblstatus;
        protected Label lblrealvalue;
        protected Label lblpayRate;
        protected Label lblpayAmt;
        protected Label lblcompletetime;

        public string orderid
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;
            this.ShowInfo(this.orderid);
        }

        private void ShowInfo(string orderid)
        {
            if (string.IsNullOrEmpty(orderid))
                return;
            OrderBankInfo model = new OrderBank().GetModel(orderid);
            if (model == null)
                return;
            this.lblorderid.Text = model.orderid;
            this.lblordertype.Text = model.ordertype.ToString();
            this.lbltypeId.Text = this.getChannelTypeName(model.typeId);
            this.lblpaymodeId.Text = this.getChannelName(model.paymodeId);
            this.lbluserorder.Text = model.userorder;
            this.lblrefervalue.Text = model.refervalue.ToString("f2");
            Decimal num;
            if (model.realvalue.HasValue)
            {
                Label label = this.lblrealvalue;
                num = model.realvalue.Value;
                string str = num.ToString("f2");
                label.Text = str;
            }
            this.notifyurl = model.notifyurl;
            if (!string.IsNullOrEmpty(model.againNotifyUrl))
                this.notifyurl = model.againNotifyUrl;
            this.lblnotifycount.Text = model.notifycount.ToString();
            this.lblnotifystat.Text = Enum.GetName(typeof(OrderNofityStatusEnum), (object)model.notifystat);
            this.lblnotifycontext.Text = this.Server.HtmlEncode(model.notifycontext);
            this.lblattach.Text = model.attach;
            this.lblpayerip.Text = model.payerip;
            this.referUrl = model.referUrl;
            this.lbladdtime.Text = FormatConvertor.DateTimeToTimeString(model.addtime);
            this.lblstatus.Text = Enum.GetName(typeof(OrderStatusEnum), (object)model.status);
            if (model.completetime.HasValue)
                this.lblcompletetime.Text = FormatConvertor.DateTimeToTimeString(model.completetime.Value);
            Label label1 = this.lblpayRate;
            num = model.payRate;
            string str1 = num.ToString("p2");
            label1.Text = str1;
            Label label2 = this.lblpayAmt;
            num = model.payAmt;
            string str2 = num.ToString("f2");
            label2.Text = str2;
            string str3 = SystemApiHelper.GetVersionName(model.version);
            if (string.IsNullOrEmpty(str3) && WebInfoFactory.CurrentWebInfo != null)
                str3 = WebInfoFactory.CurrentWebInfo.apibankname + "[" + WebInfoFactory.CurrentWebInfo.apibankversion + "]";
            this.lblversion.Text = str3;
        }

        private string getuserName(int uid)
        {
            try
            {
                return UserFactory.GetCacheModel(uid).UserName;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string getChannelTypeName(int id)
        {
            try
            {
                return ChannelType.GetModelByTypeId(id).modetypename;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string getChannelName(string code)
        {
            try
            {
                return Channel.GetModelByCode(code).modeName;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
