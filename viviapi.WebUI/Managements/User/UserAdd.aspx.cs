namespace viviapi.WebUI.Managements.User
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Payment;
    using viviapi.BLL.Settled;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.Payment;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Security;
    using viviLib.Web;

    public class UserAdd : ManagePageBase
    {
        public UserInfo _ItemInfo = null;
        protected Button btnAdd;
        protected CheckBox cb_isdebug;
        protected CheckBox cb_isEmailPass;
        protected CheckBox cb_isPhonePass;
        protected CheckBox cb_isRealNamePass;
        protected CheckBox cb_UrlNoRefPayUrl;
        protected DropDownList ddlmange;
        protected DropDownList ddlmemvip;
        protected DropDownList ddlpromvip;
        protected DropDownList ddlStatus;
        protected DropDownList ddlTocashScheme;
        protected HtmlForm form1;
        protected RadioButtonList rbl_settledmode;
        protected RadioButtonList rblsettlemode;
        protected RadioButtonList rbluserType;
        protected RadioButtonList rbuserclass;
        protected TextBox txtaccount;
        protected TextBox txtanswer;
        protected TextBox txtapiAcct;
        protected TextBox txtapikey;
        protected TextBox txtbankAddress;
        protected TextBox txtbankCity;
        protected TextBox txtbankProvince;
        protected TextBox txtCPSDrate;
        protected TextBox txtCVSNrate;
        protected TextBox txtdesc;
        protected TextBox txtemail;
        protected TextBox txtfullname;
        protected TextBox txtGetPromSuperior;
        protected TextBox txtidCard;
        protected TextBox txtpassword;
        protected TextBox txtpassword2;
        protected TextBox txtpayeeBank;
        protected TextBox txtpayeeName;
        protected TextBox txtqq;
        protected TextBox txtquestion;
        protected TextBox txtsiteName;
        protected TextBox txtsiteUrl;
        protected TextBox txttel;
        protected TextBox txtuserName;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void InitForm()
        {
            if (!base.isSuperAdmin)
            {
                int? manageId = this.model.manageId;
                int id = base.currentManage.id;
                if ((manageId.GetValueOrDefault() != id) || !manageId.HasValue)
                {
                    base.Response.Write("Sorry,No authority!");
                    base.Response.End();
                }
            }
            this.ddlmemvip.Style.Add("display", "none");
            this.ddlpromvip.Style.Add("display", "none");
            foreach (int num in Enum.GetValues(typeof(UserStatusEnum)))
            {
                this.ddlStatus.Items.Add(new ListItem(Enum.GetName(typeof(UserStatusEnum), num), num.ToString()));
            }
            DataTable levName = PayRateFactory.GetLevName(RateTypeEnum.Member);
            this.ddlmemvip.Items.Add("--商户等级--");
            foreach (DataRow row in levName.Rows)
            {
                this.ddlmemvip.Items.Add(new ListItem(row["levName"].ToString(), row["userLevel"].ToString()));
            }
            levName = PayRateFactory.GetLevName(RateTypeEnum.Agent);
            this.ddlpromvip.Items.Add("--代理等级--");
            foreach (DataRow row in levName.Rows)
            {
                this.ddlpromvip.Items.Add(new ListItem(row["levName"].ToString(), row["userLevel"].ToString()));
            }
            this.ddlmange.Items.Add(new ListItem("--请选择管理员--", ""));
            levName = ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow row in levName.Rows)
            {
                this.ddlmange.Items.Add(new ListItem(row["username"].ToString(), row["id"].ToString()));
            }
            this.ddlTocashScheme.Items.Add(new ListItem("--默认--", ""));
            levName = TocashScheme.GetList(string.Empty).Tables[0];
            foreach (DataRow row in levName.Rows)
            {
                this.ddlTocashScheme.Items.Add(new ListItem(row["schemename"].ToString(), row["id"].ToString()));
            }
        }

        private UsersUpdateLog newUpdateLog(string f, string n, string o)
        {
            return new UsersUpdateLog { userid = this.model.ID, Addtime = DateTime.Now, field = f, newvalue = n, oldValue = o, Editor = ManageFactory.CurrentManage.username, OIp = ServerVariables.TrueIP };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.InitForm();
                this.ShowInfo();
                this.rbluserType.Style.Add("display", "none");
            }
        }

        private void Save()
        {
            List<UsersUpdateLog> changeList = new List<UsersUpdateLog>();
            string text = this.txtuserName.Text;
            string str2 = this.txtpassword.Text;
            int result = 0;
            int.TryParse(this.txtCPSDrate.Text, out result);
            int num2 = 0;
            int.TryParse(this.txtCVSNrate.Text, out num2);
            string n = this.txtemail.Text;
            string str4 = this.txtqq.Text;
            string str5 = this.txttel.Text;
            string str6 = this.txtidCard.Text;
            int num3 = 0;
            int.TryParse(this.rblsettlemode.SelectedValue, out num3);
            string str7 = this.txtpayeeName.Text;
            string str8 = this.txtpayeeBank.Text;
            string str9 = this.txtbankProvince.Text;
            string str10 = this.txtbankCity.Text;
            string str11 = this.txtbankAddress.Text;
            int num4 = int.Parse(this.ddlStatus.SelectedValue);
            this.model.classid = int.Parse(this.rbuserclass.SelectedValue);
            string str12 = this.txtaccount.Text;
            string str13 = this.txtsiteName.Text;
            string str14 = this.txtsiteUrl.Text;
            UserTypeEnum enum2 = (UserTypeEnum)int.Parse(this.rbluserType.SelectedValue);
            int num5 = 0;
            if (enum2 == UserTypeEnum.会员)
            {
                num5 = int.Parse(this.ddlmemvip.SelectedValue);
            }
            else if (enum2 == UserTypeEnum.代理)
            {
                num5 = int.Parse(this.ddlpromvip.SelectedValue);
            }
            string str15 = this.txtapikey.Text;
            this.model.UserName = text;
            this.model.APIAccount = int.Parse(this.txtapiAcct.Text);
            this.model.Settles = Convert.ToInt32(this.rbl_settledmode.SelectedValue);
            if (!string.IsNullOrEmpty(str2))
            {
                str2 = Cryptography.MD5(str2);
                if (this.isUpdate && (str2 != this.model.Password))
                {
                    changeList.Add(this.newUpdateLog("password", str2, this.model.Password));
                }
                this.model.Password = str2;
            }
            if (!string.IsNullOrEmpty(this.txtpassword2.Text.Trim()))
            {
                string str16 = Cryptography.MD5(this.txtpassword2.Text.Trim());
                this.model.Password2 = str16;
            }
            if (this.isUpdate && (result != this.model.CPSDrate))
            {
                changeList.Add(this.newUpdateLog("CPSDrate", result.ToString(), this.model.CPSDrate.ToString()));
            }
            this.model.CPSDrate = result;
            if (this.isUpdate && (num2 != this.model.CVSNrate))
            {
                changeList.Add(this.newUpdateLog("CVSNrate", num2.ToString(), this.model.CVSNrate.ToString()));
            }
            this.model.CVSNrate = num2;
            if (this.isUpdate && (n != this.model.Email))
            {
                changeList.Add(this.newUpdateLog("Email", n, this.model.Email));
            }
            this.model.Email = n;
            if (this.isUpdate && (str4 != this.model.QQ))
            {
                changeList.Add(this.newUpdateLog("QQ", str4, this.model.QQ));
            }
            this.model.QQ = str4;
            if (this.isUpdate && (str5 != this.model.Tel))
            {
                changeList.Add(this.newUpdateLog("tel", str5, this.model.Tel));
            }
            this.model.Tel = str5;
            if (this.isUpdate && (str6 != this.model.IdCard))
            {
                changeList.Add(this.newUpdateLog("idCard", str6, this.model.IdCard));
            }
            this.model.IdCard = str6;
            if (this.isUpdate && (num3 != this.model.PMode))
            {
                changeList.Add(this.newUpdateLog("pmode", num3.ToString(), this.model.PMode.ToString()));
            }
            this.model.PMode = num3;
            if (this.isUpdate && (str12 != this.model.Account))
            {
                changeList.Add(this.newUpdateLog("account", str12, this.model.Account));
            }
            this.model.Account = str12;
            if (this.isUpdate && (str7 != this.model.PayeeName))
            {
                changeList.Add(this.newUpdateLog("payeeName", str7, this.model.PayeeName));
            }
            this.model.PayeeName = str7;
            if (this.isUpdate && (str8 != this.model.PayeeBank))
            {
                changeList.Add(this.newUpdateLog("payeeBank", str8, this.model.PayeeBank));
            }
            this.model.PayeeBank = str8;
            if (this.isUpdate && (str9 != this.model.BankProvince))
            {
                changeList.Add(this.newUpdateLog("BankProvince", str9, this.model.BankProvince));
            }
            this.model.BankProvince = str9;
            if (this.isUpdate && (str10 != this.model.BankCity))
            {
                changeList.Add(this.newUpdateLog("BankCity", str10, this.model.BankCity));
            }
            this.model.BankCity = str10;
            if (this.isUpdate && (str11 != this.model.BankAddress))
            {
                changeList.Add(this.newUpdateLog("bankAddress", str11, this.model.BankAddress));
            }
            this.model.BankAddress = str11;
            if (this.isUpdate && (num4 != this.model.Status))
            {
                changeList.Add(this.newUpdateLog("status", num4.ToString(), this.model.Status.ToString()));
            }
            this.model.Status = num4;
            if (this.isUpdate && (str13 != this.model.SiteName))
            {
                changeList.Add(this.newUpdateLog("SiteName", str13, this.model.SiteName));
            }
            this.model.SiteName = str13;
            if (this.isUpdate && (str14 != this.model.SiteUrl))
            {
                changeList.Add(this.newUpdateLog("siteUrl", str14, this.model.SiteUrl));
            }
            this.model.SiteUrl = str14;
            if (this.isUpdate && (enum2 != this.model.UserType))
            {
                changeList.Add(this.newUpdateLog("userType", enum2.ToString(), ((int)this.model.UserType).ToString()));
            }
            this.model.UserType = enum2;
            if (this.isUpdate && (UserLevelEnum)num2 != this.model.UserLevel)
            {
                changeList.Add(this.newUpdateLog("userLevel", num5.ToString(), ((int)this.model.UserLevel).ToString()));
            }
            this.model.UserLevel = (UserLevelEnum)num5;
            int num6 = 0;
            if (!string.IsNullOrEmpty(this.ddlTocashScheme.SelectedValue))
            {
                num6 = int.Parse(this.ddlTocashScheme.SelectedValue);
            }
            if (this.isUpdate && (num6 != this.model.MaxDayToCashTimes))
            {
                changeList.Add(this.newUpdateLog("MaxDayToCashTimes", num6.ToString(), this.model.MaxDayToCashTimes.ToString()));
            }
            this.model.MaxDayToCashTimes = num6;
            if (this.isUpdate && (str15 != this.model.APIKey))
            {
                changeList.Add(this.newUpdateLog("APIKey", str15, this.model.APIKey));
            }
            this.model.APIKey = str15;
            this.model.Desc = this.txtdesc.Text;
            this.model.IsRealNamePass = this.cb_isRealNamePass.Checked ? 1 : 0;
            this.model.IsEmailPass = this.cb_isEmailPass.Checked ? 1 : 0;
            this.model.IsPhonePass = this.cb_isPhonePass.Checked ? 1 : 0;
            this.model.isdebug = this.cb_isdebug.Checked ? 1 : 0;
            if (!string.IsNullOrEmpty(this.ddlmange.SelectedValue))
            {
                this.model.manageId = new int?(int.Parse(this.ddlmange.SelectedValue));
            }
            if (!this.isUpdate)
            {
                if (UserFactory.Add(this.model) > 0)
                {
                    base.AlertAndRedirect("保存成功！", "UserList.aspx");
                }
                else
                {
                    base.AlertAndRedirect("保存失败！");
                }
            }
            else if (UserFactory.Update(this.model, changeList))
            {
                base.AlertAndRedirect("更新成功！", "UserList.aspx");
            }
            else
            {
                base.AlertAndRedirect("更新失败！");
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Merchant))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            if (this.isUpdate && (this.model != null))
            {
                UserInfo promSuperior = UserFactory.GetPromSuperior(this.model.ID);
                if ((promSuperior != null) && (promSuperior.ID > 0))
                {
                    this.txtGetPromSuperior.Text = promSuperior.UserName;
                }
                else
                {
                    this.txtGetPromSuperior.Text = "无代理";
                }
                this.rbuserclass.SelectedValue = this.model.classid.ToString();
                this.txtuserName.Text = this.model.UserName;
                this.txtfullname.Text = this.model.full_name;
                this.txtCPSDrate.Text = this.model.CPSDrate.ToString();
                this.txtCVSNrate.Text = this.model.CVSNrate.ToString();
                this.txtemail.Text = this.model.Email;
                this.txtqq.Text = this.model.QQ;
                this.txttel.Text = this.model.Tel;
                this.txtidCard.Text = this.model.IdCard;
                this.rblsettlemode.SelectedValue = this.model.PMode.ToString();
                this.ddlTocashScheme.SelectedValue = this.model.MaxDayToCashTimes.ToString();
                this.txtaccount.Text = this.model.Account;
                this.txtpayeeName.Text = this.model.PayeeName;
                this.txtpayeeBank.Text = this.model.PayeeBank;
                this.txtbankProvince.Text = this.model.BankProvince;
                this.txtbankCity.Text = this.model.BankCity;
                this.txtbankAddress.Text = this.model.BankAddress;
                this.ddlStatus.SelectedValue = this.model.Status.ToString();
                this.txtsiteName.Text = this.model.SiteName;
                this.txtsiteUrl.Text = this.model.SiteUrl;
                this.rbluserType.SelectedValue = ((int)this.model.UserType).ToString();
                this.txtapiAcct.Text = this.model.APIAccount.ToString();
                if (this.model.UserType == UserTypeEnum.会员)
                {
                    this.ddlmemvip.SelectedValue = ((int)this.model.UserLevel).ToString();
                }
                if (this.model.UserType == UserTypeEnum.代理)
                {
                    this.ddlpromvip.SelectedValue = ((int)this.model.UserLevel).ToString();
                }
                int num = 0;
                if (!string.IsNullOrEmpty(this.ddlTocashScheme.SelectedValue))
                {
                    num = int.Parse(this.ddlTocashScheme.SelectedValue);
                }
                this.txtapikey.Text = this.model.APIKey;
                this.txtdesc.Text = this.model.Desc;
                this.ddlmange.SelectedValue = this.model.manageId.ToString();
                this.txtquestion.Text = this.model.question;
                this.txtanswer.Text = this.model.answer;
                this.cb_isRealNamePass.Checked = this.model.IsRealNamePass == 1;
                this.cb_isEmailPass.Checked = this.model.IsEmailPass == 1;
                this.cb_isPhonePass.Checked = this.model.IsPhonePass == 1;
                this.rbl_settledmode.SelectedValue = this.model.Settles.ToString();
                this.cb_isdebug.Checked = this.model.isdebug == 1;
            }
        }

        public bool isUpdate
        {
            get
            {
                return (this.ItemInfoId > 0);
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

