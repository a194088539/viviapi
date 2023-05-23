namespace viviapi.gateway
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.Channel;
    using viviapi.Model.Channel;
    using viviLib.Web;

    public class PayResult : Page
    {
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Literal LitcardNo;
        protected Literal litCardPwd;
        protected Literal litMoney;
        protected Literal litStatus;
        protected Literal litSysOrderId;
        protected Literal litTypeId;
        protected Literal litUserId;
        protected Literal litUserOrderId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.litSysOrderId.Text = this.OrderId;
                this.litUserId.Text = this.UserId.ToString();
                ChannelTypeInfo cacheModel = ChannelType.GetCacheModel(this.typeId);
                if (cacheModel != null)
                {
                    this.litTypeId.Text = cacheModel.modetypename;
                    if (!string.IsNullOrEmpty(this.payMode))
                    {
                        ChannelInfo info2 = viviapi.BLL.Channel.Channel.GetCacheModel(this.payMode);
                        if (info2 != null)
                        {
                            this.litTypeId.Text = this.litTypeId.Text + "[" + info2.modeName + "]";
                        }
                    }
                }
                this.litUserOrderId.Text = this.UserOrderId;
                this.LitcardNo.Text = this.CardNo;
                this.litCardPwd.Text = this.CardPwd;
                this.litMoney.Text = decimal.Round(this.FaceValue, 2).ToString();
                this.litStatus.Text = ((this.Status == 2) || (this.Status == 8)) ? "支付成功" : "支付失败";
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

        public decimal FaceValue
        {
            get
            {
                return WebBase.GetQueryStringDecimal("v", 0M);
            }
        }

        public string Mac
        {
            get
            {
                return WebBase.GetQueryStringString("k", "");
            }
        }

        public string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("o", "");
            }
        }

        public string payMode
        {
            get
            {
                return WebBase.GetQueryStringString("c", string.Empty);
            }
        }

        public string Result
        {
            get
            {
                return WebBase.GetQueryStringString("e", "");
            }
        }

        public int Status
        {
            get
            {
                return WebBase.GetQueryStringInt32("s", 0);
            }
        }

        public int typeId
        {
            get
            {
                return WebBase.GetQueryStringInt32("t", 0);
            }
        }

        public int UserId
        {
            get
            {
                return WebBase.GetQueryStringInt32("u", 0);
            }
        }

        public string UserOrderId
        {
            get
            {
                return WebBase.GetQueryStringString("uo", "");
            }
        }
    }
}

