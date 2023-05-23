namespace viviapi.WebUI.Userlogin.recharg
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Web;

    public class recharge : UserPageBase
    {
        public UserInfo _ItemInfo = null;
        protected Button btnsubmit;
        protected HtmlGenericControl callinfo;
        protected HtmlForm form1;
        protected TextBox txtactualMoney;
        protected TextBox txtRechargeMoney;

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string str = this.txtRechargeMoney.Text.Trim();
            decimal result = 0M;
            if (string.IsNullOrEmpty(str))
            {
                this.callinfo.InnerText = "请输入充值金额";
            }
            else if (!decimal.TryParse(str.Replace(",", ""), out result))
            {
                this.callinfo.InnerText = "充值金额格式不正确。";
            }
            else if (UserFactory.GetModel(800) == null)
            {
                this.callinfo.InnerText = "系统故障，请系统管理员。";
            }
            else
            {
                string str2 = UserFactory.CurrentMember.ID.ToString();
                string payno = new Random().Next(100, 0x3e7).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
                string str4 = base.Request.Form["bank_list"];
                if (this.InitData(payno, str4, result) > 0)
                {
                    string str5 = string.Concat(new object[] { "http://", base.Request.Url.Host, ":", base.Request.Url.Port, "/payresult/recharge_notify.aspx" });
                    string str6 = string.Concat(new object[] { "http://", base.Request.Url.Host, ":", base.Request.Url.Port, "/Userlogin/recharg/recharge_return.aspx" });
                    if (string.IsNullOrEmpty(str4))
                    {
                        str4 = "967";
                    }
                    string str7 = result.ToString();
                    string str8 = UserFactory.CurrentMember.APIKey.ToString();
                    string str9 = string.Format("parter={0}&type={1}&value={2}&orderid={3}&callbackurl={4}", new object[] { str2, str4, str7, payno, str5 });
                    string str10 = Cryptography.MD5(str9 + str8).ToLower();
                    string url = string.Format("{0}chargebank.aspx?{1}&hrefbackurl={2}&sign={3}&attach=system_recharge", new object[] { base.webInfo.PayUrl, str9, str6, str10 });
                    base.Response.Redirect(url);
                }
                else
                {
                    this.callinfo.InnerText = "系统故障，请系统管理员。";
                }
            }
        }

        private int InitData(string payno, string bank_list, decimal rechargeAmt)
        {
            int num = 0x66;
            if (bank_list == "992")
            {
                num = 0x65;
            }
            else if (bank_list == "993")
            {
                num = 100;
            }
            viviapi.Model.APP.apprecharge model = new viviapi.Model.APP.apprecharge();
            model.id = 0;
            model.orderid = payno;
            model.paytype = num;
            model.suppid = 1;
            model.processstatus = 1;
            model.processtime = new DateTime?(DateTime.Now);
            model.realPayAmt = 0;
            model.rechargeAmt = rechargeAmt;
            model.rechtype = 1;
            model.remark = string.Empty;
            model.smsnotification = false;
            model.status = 1;
            model.account = base.currentUser.UserName;
            model.userid = base.UserId;
            model.field1 = bank_list;
            model.processstatus = 0;
            viviapi.BLL.APP.apprecharge apprecharge2 = new viviapi.BLL.APP.apprecharge();
            return apprecharge2.Add(model);
        }

        private void InitForm()
        {
            this.txtactualMoney.Attributes.Add("readonly", "true");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                base.Form.Target = "_blank";
                this.InitForm();
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public UserInfo model
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = UserFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new UserInfo();
                    }
                }
                return this._ItemInfo;
            }
        }
    }
}

