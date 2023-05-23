namespace viviapi.WebUI.Merchant
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.basedata;
    using viviapi.BLL.User;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;

    public class costinfo : UserPageBase
    {
        public viviapi.Model.User.userspaybank _model = null;
        protected string BankAddress = string.Empty;
        protected Button btnSave;
        protected HtmlGenericControl callinfo;
        protected HtmlSelect ddlbankName;
        protected DropDownList ddlcity;
        protected DropDownList ddlprovince;
        protected string full_name = string.Empty;
        protected HiddenField hfaction;
        protected Literal lit_username;
        protected Literal litBankAddress;
        protected Literal litPayeeBank;
        protected Literal litPayeeName;
        protected Literal litpmode;
        protected Literal litProvince;
        protected Literal litUserViewBankAccout;
        protected string PayeeBank = string.Empty;
        protected string PayeeName = string.Empty;
        protected viviapi.BLL.User.userspaybank pbankBLL = new viviapi.BLL.User.userspaybank();
        protected RadioButton rb_accoutType0;
        protected RadioButton rb_accoutType1;
        protected RadioButton rb_alipay;
        protected RadioButton rb_bank;
        protected RadioButton rb_tenpay;
        protected HtmlTableRow tr_accoutType;
        protected HtmlTableRow tr_address;
        protected HtmlTableRow tr_bankselect;
        protected HtmlTableRow tr_oldcard;
        protected HtmlTableRow tr_province;
        protected HtmlInputText txtaccount;
        protected HtmlInputText txtbankAddress;
        protected HtmlInputText txtoldaccount;
        protected HtmlInputText txtreaccount;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = "";
            int num = 1;
            if (this.rb_alipay.Checked)
            {
                num = 2;
            }
            else if (this.rb_tenpay.Checked)
            {
                num = 3;
            }
            string str2 = this.txtoldaccount.Value.Trim();
            string text = this.ddlbankName.Items[this.ddlbankName.SelectedIndex].Text;
            string str4 = this.txtbankAddress.Value;
            string str5 = this.txtaccount.Value;
            string str6 = this.txtreaccount.Value;
            if ((this.model == null) && !string.IsNullOrEmpty(this.model.account))
            {
                if (string.IsNullOrEmpty(str2))
                {
                    str = "当前银行卡号不能为空";
                }
                else if (str2 != this.model.account)
                {
                    str = "当前银行卡号不正确";
                }
                else if (this.txtoldaccount.Value != this.model.account)
                {
                    str = "原账号输入不正确。";
                }
            }
            if (num == 1)
            {
                if (string.IsNullOrEmpty(this.ddlprovince.SelectedValue))
                {
                    str = "请选择省份";
                }
                else if (string.IsNullOrEmpty(this.ddlcity.SelectedValue))
                {
                    str = "请选择城市";
                }
                else if (string.IsNullOrEmpty(text))
                {
                    str = "开户银行不能为空";
                }
                else if (string.IsNullOrEmpty(str4))
                {
                    str = "支行名称不能为空";
                }
            }
            if (string.IsNullOrEmpty(str5))
            {
                str = "个人银行帐号";
            }
            if (str5 != str6)
            {
                str = "个人银行帐号与确认个人银行帐号必须一致";
            }
            if (string.IsNullOrEmpty(str))
            {
                int num2 = 0;
                if (this.rb_accoutType1.Checked)
                {
                    num2 = 1;
                }
                UserPayBankAppInfo model = new UserPayBankAppInfo();
                model.userid = base.currentUser.ID;
                model.accoutType = num2;
                model.pmode = num;
                model.account = str5;
                model.payeeName = base.currentUser.full_name;
                model.payeeBank = string.Empty;
                model.bankProvince = string.Empty;
                model.bankCity = string.Empty;
                model.bankAddress = string.Empty;
                model.BankCode = string.Empty;
                model.provinceCode = string.Empty;
                model.cityCode = string.Empty;
                switch (num)
                {
                    case 1:
                        model.payeeBank = this.ddlbankName.Items[this.ddlbankName.SelectedIndex].Text;
                        model.bankProvince = this.ddlprovince.Items[this.ddlprovince.SelectedIndex].Text;
                        model.bankCity = this.ddlcity.Items[this.ddlcity.SelectedIndex].Text;
                        model.bankAddress = str4;
                        model.BankCode = this.ddlbankName.Value;
                        model.provinceCode = this.ddlprovince.SelectedValue;
                        model.cityCode = this.ddlcity.SelectedValue;
                        break;

                    case 2:
                        model.payeeBank = "0002";
                        break;

                    case 3:
                        model.payeeBank = "0003";
                        break;
                }
                model.status = AcctChangeEnum.待审核;
                model.AddTime = new DateTime?(DateTime.Now);
                model.SureTime = new DateTime?(DateTime.Now);
                model.SureUser = 0;
                if (UserPayBankApp.Add(model) > 0)
                {
                    str = "结算账户添加成功，请联系商务审核!";
                }
                else
                {
                    str = "操作失败";
                }
            }
            this.callinfo.InnerText = str;
        }

        private void controls()
        {
            this.tr_bankselect.Visible = this.rb_bank.Checked;
            this.tr_province.Visible = this.rb_bank.Checked;
            this.tr_address.Visible = this.rb_bank.Checked;
            this.tr_accoutType.Visible = this.rb_bank.Checked;
        }

        protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadCity(string.Empty);
        }

        private void InitForm()
        {
            this.tr_oldcard.Visible = false;
            DataSet list = base_province.GetList("");
            this.ddlprovince.Items.Clear();
            this.ddlprovince.Items.Add(new ListItem("--省份--", ""));
            foreach (DataRow row in list.Tables[0].Rows)
            {
                this.ddlprovince.Items.Add(new ListItem(row["ProvinceName"].ToString(), row["ProvinceID"].ToString()));
            }
            if (base.currentUser != null)
            {
                this.lit_username.Text = base.currentUser.full_name;
                if (this.model != null)
                {
                    if (!string.IsNullOrEmpty(this.model.account))
                    {
                        this.tr_oldcard.Visible = true;
                    }
                    this.rb_bank.Checked = this.model.pmode == 1;
                    this.rb_alipay.Checked = this.model.pmode == 2;
                    this.rb_tenpay.Checked = this.model.pmode == 3;
                    this.rb_accoutType0.Checked = this.model.accoutType == 0;
                    this.rb_accoutType1.Checked = this.model.accoutType == 1;
                    this.ddlprovince.SelectedValue = this.model.provinceCode;
                    this.LoadCity(this.model.cityCode);
                    this.ddlbankName.Value = this.model.BankCode;
                    this.txtbankAddress.Value = this.model.bankAddress;
                    this.litpmode.Text = this.model.pmodeName;
                    this.litPayeeBank.Text = SettledFactory.GetSettleBankName(this.model.payeeBank);
                    this.litUserViewBankAccout.Text = base.UserViewBankAccout;
                    this.litPayeeName.Text = this.model.payeeName;
                    this.litBankAddress.Text = this.model.bankAddress;
                    if (this.model.pmode == 1)
                    {
                        this.litProvince.Text = string.Format("开户省市：{0}{1}<br />", this.model.bankProvince, this.model.bankCity);
                    }
                }
            }
            this.controls();
        }

        private void LoadCity(string inivalue)
        {
            string selectedValue = this.ddlprovince.SelectedValue;
            if (!string.IsNullOrEmpty(selectedValue))
            {
                DataSet list = base_city.GetList("ProvinceID=" + selectedValue);
                this.ddlcity.Items.Clear();
                this.ddlcity.Items.Add(new ListItem("--市区--", ""));
                foreach (DataRow row in list.Tables[0].Rows)
                {
                    ListItem item = new ListItem(row["CityName"].ToString(), row["CityID"].ToString());
                    if (inivalue == row["CityID"].ToString())
                    {
                        item.Selected = true;
                    }
                    this.ddlcity.Items.Add(item);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
                if (UserPayBankApp.Exists3(base.UserId))
                {
                    this.callinfo.InnerText = "您提交的资料还未审核，请联系商务审核";
                    this.btnSave.Enabled = false;
                }
            }
        }

        protected void rb_alipay_CheckedChanged(object sender, EventArgs e)
        {
            this.controls();
        }

        protected void rb_bank_CheckedChanged(object sender, EventArgs e)
        {
            this.controls();
        }

        protected void rb_tenpay_CheckedChanged(object sender, EventArgs e)
        {
            this.controls();
        }

        public viviapi.Model.User.userspaybank model
        {
            get
            {
                if ((this._model == null) && (base.currentUser != null))
                {
                    this._model = this.pbankBLL.GetModel(base.currentUser.ID);
                }
                return this._model;
            }
        }
    }
}

