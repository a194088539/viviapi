using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Tools;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.Settled;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.ExceptionHandling;
using viviLib.TimeControl;
using viviLib.Web;

namespace viviapi.WebUI.Managements
{
    public class Pay : ManagePageBase
    {
        private SettledInfo _ItemInfo = (SettledInfo)null;
        private UserInfo _userInfo = (UserInfo)null;
        protected HtmlHead Head1;
        protected HtmlForm form1;
        protected Label UidLabel;
        protected Label UserNameLabel;
        protected Label MoneyLabel;
        protected Label PayeeNameLabel;
        protected Label PayeeaddressLabel;
        protected Label BankLabel;
        protected Label AccountLabel;
        protected Label UserStatusLabel;
        protected Label AddTimeLabel;
        protected Label lblPayeeName;
        protected Label lblBank;
        protected Label lblPayeeaddress;
        protected Label lblAccount;
        protected Label PayMoneyLabel;
        protected TextBox TaxBox;
        protected TextBox ChargesBox;
        protected DropDownList ddlSupplier;
        protected Button btnSave;
        protected Button btnSure;
        protected Label errLabel;

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public SettledInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                    this._ItemInfo = this.ItemInfoId <= 0 ? new SettledInfo() : SettledFactory.GetModel(this.ItemInfoId);
                return this._ItemInfo;
            }
        }

        public UserInfo userInfo
        {
            get
            {
                if (this._userInfo == null && this.ItemInfo != null)
                    this._userInfo = UserFactory.GetModel(this.ItemInfo.userid);
                return this._userInfo;
            }
        }

        public string action
        {
            get
            {
                string queryStringString = WebBase.GetQueryStringString("action", "");
                if (queryStringString == "")
                    return "pay";
                return queryStringString;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (this.IsPostBack)
                return;
            DataTable dataTable = SupplierFactory.GetList("isdistribution=1").Tables[0];
            this.ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                this.ddlSupplier.Items.Add(new ListItem(dataRow["name"].ToString(), dataRow["code"].ToString()));
            if (this.ItemInfoId > 0 && this.ItemInfo != null && this.userInfo != null)
            {
                this.PayMoneyLabel.Text = this.ItemInfo.amount.ToString("c2");
                this.AddTimeLabel.Text = FormatConvertor.DateTimeToTimeString(this.ItemInfo.addtime);
                this.lblPayeeName.Text = this.ItemInfo.payeeName;
                this.lblBank.Text = SettledFactory.GetSettleBankName(this.ItemInfo.PayeeBank);
                this.lblPayeeaddress.Text = this.ItemInfo.Payeeaddress;
                this.lblAccount.Text = this.ItemInfo.Account;
                Decimal balance;
                if (this.ItemInfo.charges.HasValue)
                {
                    TextBox textBox = this.ChargesBox;
                    balance = this.ItemInfo.charges.Value;
                    string str = balance.ToString("f2");
                    textBox.Text = str;
                }
                else
                {
                    TocashSchemeInfo modelByUser = viviapi.BLL.Settled.TocashScheme.GetModelByUser(1, this.ItemInfo.userid);
                    Decimal num = modelByUser.chargerate * this.ItemInfo.amount;
                    if (num < modelByUser.chargeleastofeach)
                        num = modelByUser.chargeleastofeach;
                    else if (num > modelByUser.chargemostofeach)
                        num = modelByUser.chargemostofeach;
                    this.ChargesBox.Text = num.ToString("f2");
                }
                DropDownList dropDownList = this.ddlSupplier;
                int num1 = this.ItemInfo.suppid;
                string str1 = num1.ToString();
                dropDownList.SelectedValue = str1;
                Label label1 = this.UidLabel;
                num1 = this.userInfo.ID;
                string str2 = num1.ToString();
                label1.Text = str2;
                this.UserNameLabel.Text = this.userInfo.UserName;
                Label label2 = this.MoneyLabel;
                balance = this.userInfo.Balance;
                string str3 = balance.ToString("c2");
                label2.Text = str3;
                this.PayeeNameLabel.Text = this.userInfo.PayeeName;
                this.PayeeaddressLabel.Text = this.userInfo.BankAddress;
                this.AccountLabel.Text = this.userInfo.Account;
                this.BankLabel.Text = this.userInfo.PayeeBank;
                this.UserStatusLabel.Text = Enum.GetName(typeof(UserStatusEnum), (object)this.userInfo.Status);
                if (this.ItemInfo.status != SettledStatus.支付中)
                {
                    this.TaxBox.Enabled = false;
                    this.TaxBox.ReadOnly = true;
                    this.ChargesBox.Enabled = false;
                    this.ChargesBox.ReadOnly = true;
                    this.btnSure.Text = "已支付";
                    this.btnSure.Enabled = false;
                }
                if (this.action == "pay")
                {
                    this.btnSure.Visible = true;
                    this.btnSave.Visible = false;
                }
                else if (this.action == "modi")
                {
                    this.btnSure.Visible = false;
                    this.btnSave.Visible = true;
                }
            }
        }

        private void setPower()
        {
            if (ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
                return;
            this.Response.Write("Sorry,No authority!");
            this.Response.End();
        }

        private string DoPay()
        {
            try
            {
                Decimal num1 = Decimal.Parse(this.TaxBox.Text.Trim());
                Decimal num2 = Decimal.Parse(this.ChargesBox.Text.Trim());
                this.ItemInfo.paytime = DateTime.Now;
                this.ItemInfo.tax = new Decimal?(num1);
                this.ItemInfo.charges = new Decimal?(num2);
                this.ItemInfo.status = SettledStatus.已支付;
                if (!string.IsNullOrEmpty(this.ddlSupplier.SelectedValue))
                    this.ItemInfo.suppid = int.Parse(this.ddlSupplier.SelectedValue);
                switch (SettledFactory.Pay(this.ItemInfo))
                {
                    case 99:
                        return "未知错误";
                    case 1:
                        return "状态不正确";
                    case 0:
                        string smsTempTocash = viviapi.BLL.SysConfig.sms_temp_tocash;
                        if (!string.IsNullOrEmpty(smsTempTocash) && !string.IsNullOrEmpty(this.userInfo.Tel))
                            SMS.SendJXTWithCheck(this.userInfo.Tel, smsTempTocash.Replace("{@username}", this.userInfo.UserName).Replace("{@settledmoney}", this.ItemInfo.amount.ToString("f2")).Replace("{@sitename}", WebInfoFactory.CurrentWebInfo.Name), "");
                        if (this.ItemInfo.suppid > 0)
                            viviapi.ETAPI.Withdraw.InitDistribution(this.ItemInfo);
                        return "";
                    default:
                        return "err";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal num1 = Decimal.Parse(this.TaxBox.Text.Trim());
                Decimal num2 = Decimal.Parse(this.ChargesBox.Text.Trim());
                this.ItemInfo.tax = new Decimal?(num1);
                this.ItemInfo.charges = new Decimal?(num2);
                this.ItemInfo.paytime = DateTime.Now;
                if (!string.IsNullOrEmpty(this.ddlSupplier.SelectedValue))
                    this.ItemInfo.suppid = int.Parse(this.ddlSupplier.SelectedValue);
                if (SettledFactory.Update(this.ItemInfo))
                    this.AlertAndRedirect("修改成功", "Pays.aspx");
                else
                    this.AlertAndRedirect("修改失败");
            }
            catch (Exception ex)
            {
                this.AlertAndRedirect(ex.Message);
            }
        }

        protected void btnSure_Click(object sender, EventArgs e)
        {
            this.AlertAndRedirect(this.DoPay(), "Pays.aspx");
        }
    }
}
