namespace viviapi.WebUI.Userlogin.account
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.basedata;
    using viviapi.BLL.Settled;
    using viviapi.BLL.User;
    using viviapi.Cache;
    using viviapi.Model.Settled;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Web;

    public class costinfo : UserPageBase
    {
        public viviapi.Model.User.userspaybank _model = null;
        private TocashSchemeInfo _scheme = null;
        protected string BankAddress = string.Empty;
        protected Button btnSave;
        protected HtmlGenericControl callinfo;
        protected HtmlSelect ddlbankName;
        protected DropDownList ddlcity;
        protected DropDownList ddlprovince;
        protected HtmlForm form1;
        protected string full_name = string.Empty;
        protected HiddenField hfaction;
        protected Literal lit_username;
        protected Literal litBankAddress;
        protected Literal litbankdetentiondays;
        protected Literal litcarddetentiondays;
        protected Literal litotherdetentiondays;
        protected Literal litPayeeBank;
        protected Literal litPayeeName;
        protected Literal litpmode;
        protected Literal litProvince;
        protected Literal litUserViewBankAccout;
        protected string PayeeBank = string.Empty;
        protected string PayeeName = string.Empty;
        protected viviapi.BLL.User.userspaybank pbankBLL = new viviapi.BLL.User.userspaybank();
        protected HtmlGenericControl qryhkh2;
        protected RadioButton rb_accoutType0;
        protected RadioButton rb_accoutType1;
        protected RadioButton rb_alipay;
        protected RadioButton rb_bank;
        protected RadioButton rb_tenpay;
        //protected TextBox TextEmail;
        protected HtmlTableRow tr_accoutType;
        protected HtmlTableRow tr_address;
        protected HtmlTableRow tr_bankselect;
        protected HtmlTableRow tr_oldcard;
        protected HtmlTableRow tr_province;
        protected HtmlInputText txtaccount;
        protected HtmlInputText txtbankAddress;
        protected TextBox txtcashpass;
        protected HtmlInputText txtoldaccount;
        protected HtmlInputText txtreaccount;
        protected HtmlGenericControl yhkh1;
        protected HtmlGenericControl yhkh2;

        private void bank()
        {
        }

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
            string parmValue = this.GetParmValue("phone");
            string objId = "PHONE_VALID_" + parmValue;
            string str4 = (string)WebCache.GetCacheService().RetrieveObject(objId);
            string str5 = this.txtoldaccount.Value.Trim();
            string text = this.ddlbankName.Items[this.ddlbankName.SelectedIndex].Text;
            string str7 = this.txtbankAddress.Value;
            string str8 = this.txtaccount.Value;
            string str9 = this.txtreaccount.Value;
            string str10 = this.txtcashpass.Text.Trim();
            // string str11 = this.TextEmail.Text.Trim();
            if (string.IsNullOrEmpty(str10))
            {
                str = "请输入您的提现密码";
            }
            else if (Cryptography.MD5(str10) != base.currentUser.Password2)
            {
                str = "提现密码不正确";
            }
            //else if (string.IsNullOrEmpty(str11))
            //{
            //str = "邮箱验证码不能为空";
            //}
            else //if (str11 != str4)
            {
                //str = "邮箱验证码不正确!";
                //}
                if ((this.model == null) && !string.IsNullOrEmpty(this.model.account))
                {
                    if (string.IsNullOrEmpty(str5))
                    {
                        str = "当前银行卡号不能为空";
                    }
                    else if (str5 != this.model.account)
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
                    else if (string.IsNullOrEmpty(str7))
                    {
                        str = "支行名称不能为空";
                    }
                }
                if (!string.IsNullOrEmpty(this.model.account))
                {
                    if (this.model.pmode == 1)
                    {
                        if (string.IsNullOrEmpty(str5))
                        {
                            str = "当前银行卡号不能为空";
                        }
                        else if (str5 != this.model.account)
                        {
                            str = "当前银行卡号不正确";
                        }
                        else if (this.txtoldaccount.Value != this.model.account)
                        {
                            str = "原账号输入不正确。";
                        }
                    }
                    else if (this.model.pmode == 2)
                    {
                        if (string.IsNullOrEmpty(str5))
                        {
                            str = "原支付宝账户不能为空";
                        }
                        else if (this.txtoldaccount.Value != this.model.account)
                        {
                            str = "原支付宝账户不正确";
                        }
                    }
                    else if (this.model.pmode == 3)
                    {
                        if (string.IsNullOrEmpty(str5))
                        {
                            str = "原财付通账户不能为空";
                        }
                        else if (this.txtoldaccount.Value != this.model.account)
                        {
                            str = "原财付通账户不正确";
                        }
                    }
                }
                if (string.IsNullOrEmpty(str8))
                {
                    str = "个人银行帐号不能为空";
                }
                if (str8 != str9)
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
                    model.account = str8;
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
                            model.bankAddress = str7;
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
        }

        private void controls()
        {
            this.tr_bankselect.Visible = this.rb_bank.Checked;
            this.tr_province.Visible = this.rb_bank.Checked;
            this.tr_address.Visible = this.rb_bank.Checked;
            this.tr_accoutType.Visible = this.rb_bank.Checked;
            if (this.rb_bank.Checked)
            {
                if (this.model.pmode == 1)
                {
                    this.yhkh1.InnerText = "原银行卡号 :";
                    this.yhkh2.InnerText = "新银行卡号 ;";
                    this.qryhkh2.InnerText = "确认新银行卡号 :";
                }
                else if (this.model.pmode == 2)
                {
                    this.yhkh1.InnerText = "原支付宝账号 :";
                    this.yhkh2.InnerText = "新银行卡号 ;";
                    this.qryhkh2.InnerText = "确认新银行卡号 :";
                }
                else if (this.model.pmode == 3)
                {
                    this.yhkh1.InnerText = "原财付通 :";
                    this.yhkh2.InnerText = "新银行卡号 ;";
                    this.qryhkh2.InnerText = "确认新银行卡号 :";
                }
            }
            else if (this.rb_alipay.Checked)
            {
                if (this.model.pmode == 1)
                {
                    this.yhkh1.InnerText = "原银行卡号 :";
                    this.yhkh2.InnerText = "新支付宝账号 ;";
                    this.qryhkh2.InnerText = "确认新支付宝账号 :";
                }
                else if (this.model.pmode == 2)
                {
                    this.yhkh1.InnerText = "原支付宝 :";
                    this.yhkh2.InnerText = "新支付宝账号 ;";
                    this.qryhkh2.InnerText = "确认支付宝账号 :";
                }
                else if (this.model.pmode == 3)
                {
                    this.yhkh1.InnerText = "原财付通 :";
                    this.yhkh2.InnerText = "新支付宝账号 ;";
                    this.qryhkh2.InnerText = "确认支付宝账号 :";
                }
            }
            else if (this.rb_tenpay.Checked)
            {
                if (this.model.pmode == 1)
                {
                    this.yhkh1.InnerText = "原银行卡号 :";
                    this.yhkh2.InnerText = "新财付通账号 ;";
                    this.qryhkh2.InnerText = "确认新财付通账号 :";
                }
                else if (this.model.pmode == 2)
                {
                    this.yhkh1.InnerText = "原支付宝 :";
                    this.yhkh2.InnerText = "新财付通账号 ;";
                    this.qryhkh2.InnerText = "确认财付通账号 :";
                }
                else if (this.model.pmode == 3)
                {
                    this.yhkh1.InnerText = "原财付通 :";
                    this.yhkh2.InnerText = "新财付通账号 ;";
                    this.qryhkh2.InnerText = "确认财付通账号 :";
                }
            }
        }

        protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadCity(string.Empty);
        }

        private string GetParmValue(string caption)
        {
            return WebBase.GetFormString(caption, "");
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
                this.litbankdetentiondays.Text = this.scheme.bankdetentiondays.ToString();
                this.litcarddetentiondays.Text = this.scheme.carddetentiondays.ToString();
                this.litotherdetentiondays.Text = this.scheme.otherdetentiondays.ToString();
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

