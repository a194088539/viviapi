namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Settled;
    using viviapi.BLL.Withdraw;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.Model.Settled;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.TimeControl;

    public class applycost : UserPageBase
    {
        private TocashSchemeInfo _scheme = null;
        protected Button btnpost;
        private channelwithdraw chnlBLL = new channelwithdraw();
        protected decimal enableAmount = 0M;
        protected Label lblMessage;
        protected Literal litBalance;
        protected Literal litbankdetentionAmt;
        protected Literal litbankdetentiondays;
        protected Literal litcarddetentionAmt;
        protected Literal litcarddetentiondays;
        protected Literal litotherdetentionAmt;
        protected Literal litotherdetentiondays;
        protected HtmlInputText txtApplyMoney;
        protected HtmlInputText txtBalance;
        protected TextBox txtcashpwd;
        protected HtmlInputText txtUserName;

        protected void btnpost_Click(object sender, EventArgs e)
        {
            string closecashReason = "";
            if (!SysConfig.isopenCash)
            {
                closecashReason = SysConfig.closecashReason;
            }
            else
            {
                string str2 = this.txtApplyMoney.Value.Trim();
                string str3 = this.txtcashpwd.Text.Trim();
                decimal result = 0M;
                decimal num2 = (base.balance - base.unpayment) - base.Freeze;
                decimal num3 = 0M;
                if (this.scheme.bankdetentiondays > 0)
                {
                    num3 += Trade.GetNdaysIncome(1, base.UserId, this.scheme.bankdetentiondays);
                }
                if (this.scheme.carddetentiondays > 0)
                {
                    num3 += Trade.GetNdaysIncome(2, base.UserId, this.scheme.carddetentiondays);
                }
                if (this.scheme.otherdetentiondays > 0)
                {
                    num3 += Trade.GetNdaysIncome(3, base.UserId, this.scheme.otherdetentiondays);
                }
                this.enableAmount = ((base.balance - base.unpayment) - base.Freeze) - num3;
                if (this.enableAmount < 0M)
                {
                    this.enableAmount = 0M;
                }
                if (this.enableAmount <= 0M)
                {
                    closecashReason = "可提现金额不足！";
                }
                else if (string.IsNullOrEmpty(str2))
                {
                    closecashReason = "请输入您要提现的金额";
                }
                else if (!decimal.TryParse(str2, out result))
                {
                    closecashReason = "请输入您正确的金额";
                }
                else if (string.IsNullOrEmpty(str3))
                {
                    closecashReason = "请输入您的提现密码";
                }
                else if (Cryptography.MD5(str3) != base.currentUser.Password2)
                {
                    closecashReason = "提现密码不正确";
                }
                else if (result > num2)
                {
                    closecashReason = "余额不足,请修改提现金额";
                }
                else if (result < this.scheme.minamtlimitofeach)
                {
                    closecashReason = "您的提现金额小于最低提现金额限制.";
                }
                else if (result > this.scheme.maxamtlimitofeach)
                {
                    closecashReason = "您的提现金额大于最大提现金额限制.";
                }
                else if (SettledFactory.GetUserDaySettledTimes(base.UserId, FormatConvertor.DateTimeToDateString(DateTime.Now)) >= this.scheme.dailymaxtimes)
                {
                    closecashReason = "您今天的提现次数已达到最多限制，请明天再试。";
                }
                else
                {
                    decimal userDaySettledAmt = SettledFactory.GetUserDaySettledAmt(base.UserId, FormatConvertor.DateTimeToDateString(DateTime.Now));
                    if ((userDaySettledAmt + result) >= this.scheme.dailymaxamt)
                    {
                        closecashReason = string.Format("您今天的提现将超过最大限额，你最多可以提现{0:f2}", this.scheme.dailymaxamt - userDaySettledAmt);
                    }
                }
                if (string.IsNullOrEmpty(closecashReason))
                {
                    SettledInfo model = new SettledInfo();
                    model.addtime = DateTime.Now;
                    model.amount = result;
                    model.charges = 0;
                    model.paytime = DateTime.Now;
                    model.status = SettledStatus.审核中;
                    model.tax = 0;
                    model.userid = base.UserId;
                    if (base.currentUser.Settles == 0)
                    {
                        model.AppType = AppTypeEnum.t0;
                    }
                    else
                    {
                        model.AppType = AppTypeEnum.t1;
                    }
                    model.PayeeBank = base.currentUser.PayeeBank;
                    if (base.currentUser.PMode == 2)
                    {
                        model.PayeeBank = "0002";
                    }
                    else if (base.currentUser.PMode == 3)
                    {
                        model.PayeeBank = "0003";
                    }
                    model.Payeeaddress = base.currentUser.BankAddress;
                    model.payeeName = base.currentUser.full_name;
                    model.Account = base.currentUser.Account;
                    model.Paytype = 1;
                    model.charges = new decimal?(this.scheme.chargerate * result);
                    decimal? charges = model.charges;
                    decimal chargeleastofeach = this.scheme.chargeleastofeach;
                    if ((charges.GetValueOrDefault() < chargeleastofeach) && charges.HasValue)
                    {
                        model.charges = new decimal?(this.scheme.chargeleastofeach);
                    }
                    else
                    {
                        charges = model.charges;
                        chargeleastofeach = this.scheme.chargemostofeach;
                        if ((charges.GetValueOrDefault() > chargeleastofeach) && charges.HasValue)
                        {
                            model.charges = new decimal?(this.scheme.chargemostofeach);
                        }
                    }
                    if (DateTime.Now.Hour > 0x10)
                    {
                        model.required = DateTime.Now.AddDays(1.0);
                    }
                    else
                    {
                        model.required = DateTime.Now;
                    }
                    if (this.scheme.vaiInterface > 0)
                    {
                        model.suppid = this.chnlBLL.GetSupplier(base.currentUser.PayeeBank);
                    }
                    if ((model.suppid > 0) && (this.scheme.tranRequiredAudit == 0))
                    {
                        model.status = SettledStatus.付款接口支付中;
                    }
                    int num6 = SettledFactory.Apply(model);
                    model.id = num6;
                    if (num6 > 0)
                    {
                        if (model.status == SettledStatus.付款接口支付中)
                        {
                            Withdraw.InitDistribution(model);
                        }
                    }
                    else
                    {
                        closecashReason = "提现失败";
                    }
                }
            }
            if (!string.IsNullOrEmpty(closecashReason))
            {
                base.AlertAndRedirect(closecashReason);
            }
            else
            {
                base.AlertAndRedirect("提现成功", "costlog.aspx");
            }
        }

        private void InitForm()
        {
            bool flag = true;
            string closecashReason = string.Empty;
            if (!SysConfig.isopenCash)
            {
                closecashReason = SysConfig.closecashReason;
            }
            else if (string.IsNullOrEmpty(base.currentUser.Password2))
            {
                flag = false;
                closecashReason = "您尚未设置提现密码,暂时无法提现。";
            }
            else if (this.scheme == null)
            {
                flag = false;
                closecashReason = "未设置提现方案，请联系客服人员!";
            }
            if (!flag)
            {
                this.btnpost.Enabled = false;
                this.lblMessage.Text = closecashReason;
            }
            this.txtUserName.Value = base.currentUser.UserName;
            this.txtBalance.Value = base.currentUser.Balance.ToString("f2");
            StringBuilder builder = new StringBuilder();
            if (base.unpayment > 0M)
            {
                builder.AppendFormat("提现中{0:f2},", base.unpayment);
            }
            if (base.Freeze > 0M)
            {
                builder.AppendFormat("冻结{0:f2},", base.unpayment);
            }
            decimal num = 0M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            if (this.scheme != null)
            {
                this.litbankdetentiondays.Text = this.scheme.bankdetentiondays.ToString();
                if (this.scheme.bankdetentiondays > 0)
                {
                    num2 = Trade.GetNdaysIncome(1, base.UserId, this.scheme.bankdetentiondays);
                    num += num2;
                }
                this.litcarddetentiondays.Text = this.scheme.carddetentiondays.ToString();
                this.litbankdetentionAmt.Text = num2.ToString("f2");
                if (this.scheme.carddetentiondays > 0)
                {
                    num3 = Trade.GetNdaysIncome(2, base.UserId, this.scheme.carddetentiondays);
                    num += num3;
                }
                this.litcarddetentiondays.Text = this.scheme.carddetentiondays.ToString();
                this.litcarddetentionAmt.Text = num3.ToString("f2");
                if (this.scheme.otherdetentiondays > 0)
                {
                    num4 = Trade.GetNdaysIncome(3, base.UserId, this.scheme.otherdetentiondays);
                    num += num4;
                }
                this.litotherdetentiondays.Text = this.scheme.otherdetentiondays.ToString();
                this.litotherdetentionAmt.Text = num4.ToString("f2");
                if (num > 0M)
                {
                    builder.AppendFormat("总扣押{0:f2},", num);
                }
                if (builder.Length > 0)
                {
                    this.litBalance.Text = string.Format("其中({0})", builder.ToString());
                }
            }
            this.enableAmount = ((base.balance - base.unpayment) - base.Freeze) - num;
            if (this.enableAmount < 0M)
            {
                this.enableAmount = 0M;
            }
            this.txtApplyMoney.Value = this.enableAmount.ToString("f2");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtApplyMoney.Attributes["onkeyup"] = @"this.value=this.value.replace(/[^\d\.]+?/g,'')";
                this.txtUserName.Attributes["readonly"] = "true";
                this.txtBalance.Attributes["readonly"] = "true";
                this.InitForm();
            }
        }

        protected TocashSchemeInfo scheme
        {
            get
            {
                if (this._scheme == null)
                {
                    this._scheme = TocashScheme.GetModelByUser(1, base.UserId);
                }
                return this._scheme;
            }
        }
    }
}

