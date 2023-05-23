using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL.Channel;
using viviapi.Model.Channel;
using viviLib.Web;

namespace viviapi.gateway.links
{
    public class PayResult : Page
    {
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected Literal litSysOrderId;
        protected Literal litUserOrderId;
        protected Literal litUserId;
        protected Literal litTypeId;
        protected Literal LitcardNo;
        protected Literal litCardPwd;
        protected Literal litMoney;
        protected Literal litStatus;

        public string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("o", "");
            }
        }

        public int typeId
        {
            get
            {
                return WebBase.GetQueryStringInt32("t", 0);
            }
        }

        public string CardNo
        {
            get
            {
                return WebBase.GetQueryStringString("c", "");
            }
        }

        public string CardPwd
        {
            get
            {
                return WebBase.GetQueryStringString("p", "");
            }
        }

        public int FaceValue
        {
            get
            {
                return WebBase.GetQueryStringInt32("v", 0);
            }
        }

        public string Result
        {
            get
            {
                return WebBase.GetQueryStringString("e", "");
            }
        }

        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("u", 0);
            }
        }

        public string Mac
        {
            get
            {
                return WebBase.GetQueryStringString("k", "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
                return;
            string str1 = string.Empty;
            this.litSysOrderId.Text = this.OrderId;
            Literal literal1 = this.litUserId;
            int num = this.UserId;
            string str2 = num.ToString();
            literal1.Text = str2;
            ChannelTypeInfo cacheModel = ChannelType.GetCacheModel(this.typeId);
            if (cacheModel != null)
                this.litTypeId.Text = cacheModel.modetypename;
            this.LitcardNo.Text = this.CardNo;
            this.litCardPwd.Text = this.CardPwd;
            Literal literal2 = this.litMoney;
            num = this.FaceValue;
            string str3 = num.ToString();
            literal2.Text = str3;
            this.litStatus.Text = this.Result;
        }
    }
}
