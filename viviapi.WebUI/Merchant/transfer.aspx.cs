namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Web;

    public class transfer : UserPageBase
    {
        protected viviapi.Model.Settled.transferscheme _scheme = null;
        private usersettingInfo _setting = null;
        protected Button btnSave;
        protected HtmlGenericControl callinfo;
        protected decimal enableAmount = 0M;
        protected HiddenField HiddenField1;
        protected viviapi.BLL.Settled.transferscheme schemeBLL = new viviapi.BLL.Settled.transferscheme();
        protected usersetting setbll = new usersetting();
        protected HiddenField touserid;
        protected viviapi.BLL.Settled.transfer tranBLL = new viviapi.BLL.Settled.transfer();
        protected TextBox txtremark;
        protected TextBox txttocashpwd;
        protected TextBox txtToUser;
        protected TextBox txtTransferMoney;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            string text = this.txttocashpwd.Text;
            if (string.IsNullOrEmpty(text))
            {
                str = "请输入您的提现密码";
            }
            else if (Cryptography.MD5(text) != base.currentUser.Password2)
            {
                str = "提现密码不正确";
            }
            else if ((this.setting == null) || (this.setting.istransfer == 0))
            {
                str = "你暂未开通转账权限";
            }
            else if (this.scheme == null)
            {
                str = "规则未设置，请联系管理员。";
            }
            else
            {
                string str3 = this.txtTransferMoney.Text;
                decimal result = 0M;
                int num2 = 0;
                if (!int.TryParse(this.touserid.Value, out num2))
                {
                    str = "请输入付款账号.";
                }
                if (num2 == base.UserId)
                {
                    str = "";
                }
                else if (!UserFactory.Exists(num2))
                {
                    str = "此账号不存在。";
                }
                else if (string.IsNullOrEmpty(str3))
                {
                    str = "请输入付款金额。";
                }
                else if (!decimal.TryParse(str3.Replace(",", ""), out result))
                {
                    str = "请输入正确的付款金额。";
                }
                else
                {
                    if (base.SettlesMode == 1)
                    {
                        this.enableAmount = ((base.balance - base.unpayment) - base.Freeze) - base.TodayIncome;
                        if (this.enableAmount < 0M)
                        {
                            this.enableAmount = 0M;
                        }
                    }
                    else
                    {
                        this.enableAmount = (base.balance - base.unpayment) - base.Freeze;
                    }
                    decimal num5 = this.schemeBLL.GetUserMonthTotalAmt(base.UserId) - this.scheme.monthmaxamt;
                    if (num5 < 0M)
                    {
                        num5 = 0M;
                    }
                    decimal chargeleastofeach = 0M;
                    if (num5 > 0M)
                    {
                        chargeleastofeach = (this.scheme.chargerate * num5) / 100M;
                        if (chargeleastofeach < this.scheme.chargeleastofeach)
                        {
                            chargeleastofeach = this.scheme.chargeleastofeach;
                        }
                        else if (chargeleastofeach > this.scheme.chargemostofeach)
                        {
                            chargeleastofeach = this.scheme.chargemostofeach;
                        }
                    }
                    if ((result + chargeleastofeach) > this.enableAmount)
                    {
                        str = "余额不足。";
                    }
                    else
                    {
                        viviapi.Model.Settled.transfer model = new viviapi.Model.Settled.transfer();
                        model.addtime = DateTime.Now;
                        model.amt = result;
                        model.charge = chargeleastofeach;
                        model.month = new int?(DateTime.Now.Month);
                        model.year = new int?(DateTime.Now.Year);
                        model.userid = base.currentUser.ID;
                        model.touserid = num2;
                        model.updatetime = new DateTime?(DateTime.Now);
                        model.remark = this.txtremark.Text;
                        model.status = 2;
                        if (this.tranBLL.Add(model) > 0)
                        {
                            str = "转账成功";
                        }
                        else
                        {
                            str = "转账失败。";
                        }
                    }
                }
            }
            this.callinfo.InnerText = str;
        }

        private string GetParmValue(string caption)
        {
            return WebBase.GetFormString(caption, "");
        }

        private void InitForm()
        {
            if (string.IsNullOrEmpty(base.currentUser.Password2))
            {
                this.callinfo.InnerHtml = "您尚未设置提现密码,暂时无法提现。<a href=\"anquan_tixianmima.aspx\">点击设置</a>";
                this.btnSave.Enabled = false;
            }
            else if ((this.setting == null) || (this.setting.istransfer == 0))
            {
                this.btnSave.Enabled = false;
                this.callinfo.InnerText = "你暂未开通转账权限。";
            }
            else if (this.scheme == null)
            {
                this.btnSave.Enabled = false;
                this.callinfo.InnerText = "规则未设置，请联系管理员。";
            }
            if (base.SettlesMode == 1)
            {
                this.enableAmount = ((base.balance - base.unpayment) - base.Freeze) - base.TodayIncome;
                if (this.enableAmount < 0M)
                {
                    this.enableAmount = 0M;
                }
            }
            else
            {
                this.enableAmount = (base.balance - base.unpayment) - base.Freeze;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }

        protected viviapi.Model.Settled.transferscheme scheme
        {
            get
            {
                if (this._scheme == null)
                {
                    this._scheme = this.schemeBLL.GetModel(1);
                }
                return this._scheme;
            }
        }

        public usersettingInfo setting
        {
            get
            {
                this._setting = this.setbll.GetModel(base.UserId);
                if (this._setting == null)
                {
                    this._setting = new usersettingInfo();
                }
                return this._setting;
            }
        }
    }
}

