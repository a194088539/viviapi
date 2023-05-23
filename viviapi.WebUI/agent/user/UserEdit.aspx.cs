using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using viviapi.BLL;
using viviapi.BLL.Payment;
using viviapi.BLL.Settled;
using viviapi.BLL.Sys;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.Payment;
using viviapi.Model.User;
using viviapi.WebComponents.Web;
using viviLib.Security;
using viviLib.Web;

namespace viviapi.WebUI.agent.User
{
    public class UserEdits : AgentPageBase
    {
        public UserInfo _ItemInfo = null;

        protected HtmlForm form1;

        protected Label lblid;

        protected TextBox txtuserName;

        protected RadioButtonList rbuserclass;

        protected TextBox txtfullname;

        protected TextBox txtpassword;

        protected TextBox txtpassword2;

        protected RadioButtonList rbluserType;

        protected DropDownList ddlmemvip;

        protected DropDownList ddlpromvip;

        protected DropDownList ddlmange;

        protected TextBox txtGetPromSuperior;

        protected TextBox txtCPSDrate;

        protected TextBox txtCVSNrate;

        protected TextBox txtemail;

        protected TextBox txtqq;

        protected TextBox txttel;

        protected TextBox txtidCard;

        protected RadioButtonList rblsettlemode;

        protected TextBox txtpayeeBank;

        protected TextBox txtbankProvince;

        protected TextBox txtbankCity;

        protected TextBox txtbankAddress;

        protected TextBox txtpayeeName;

        protected TextBox txtaccount;

        protected DropDownList ddlStatus;

        protected DropDownList ddlTocashScheme;

        protected Label lblIntegral;

        protected Label lblregTime;

        protected Label lbllastLoginIp;

        protected Label lbllastLoginTime;

        protected Label lblbalance;

        protected TextBox txtsiteName;

        protected TextBox txtapiAcct;

        protected TextBox txtquestion;

        protected TextBox txtanswer;

        protected TextBox txtUrlNoRefPayUrl;

        protected TextBox txtsmsNotifyUrl;

        protected CheckBox cb_UrlNoRefPayUrl;

        protected CheckBox cb_isdebug;

        protected RadioButtonList rbl_settledmode;

        protected TextBox txtsiteUrl;

        protected TextBox txtapikey;

        protected CheckBox cb_isRealNamePass;

        protected CheckBox cb_isEmailPass;

        protected CheckBox cb_isPhonePass;

        protected TextBox txtdesc;

        protected Button btnAdd;

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public bool isUpdate
        {
            get
            {
                return this.ItemInfoId > 0;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.InitForm();
                this.ShowInfo();
                this.rbluserType.Style.Add("display", "none");
            }
        }

        private void InitForm()
        {
            this.ddlmemvip.Style.Add("display", "none");
            this.ddlpromvip.Style.Add("display", "none");
            foreach (int num in Enum.GetValues(typeof(UserStatusEnum)))
            {
                this.ddlStatus.Items.Add(new ListItem(Enum.GetName(typeof(UserStatusEnum), num), num.ToString()));
            }
            DataTable dataTable = PayRateFactory.GetLevName(RateTypeEnum.Member);
            this.ddlmemvip.Items.Add("--商户等级--");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                this.ddlmemvip.Items.Add(new ListItem(dataRow["levName"].ToString(), dataRow["userLevel"].ToString()));
            }
            dataTable = PayRateFactory.GetLevName(RateTypeEnum.Agent);
            this.ddlpromvip.Items.Add("--代理等级--");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                this.ddlpromvip.Items.Add(new ListItem(dataRow["levName"].ToString(), dataRow["userLevel"].ToString()));
            }
            this.ddlmange.Items.Add(new ListItem("--请选择管理员--", ""));
            dataTable = ManageFactory.GetList(" status =1").Tables[0];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                this.ddlmange.Items.Add(new ListItem(dataRow["username"].ToString(), dataRow["id"].ToString()));
            }
            this.ddlTocashScheme.Items.Add(new ListItem("--默认--", ""));
            dataTable = TocashScheme.GetList(string.Empty).Tables[0];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                this.ddlTocashScheme.Items.Add(new ListItem(dataRow["schemename"].ToString(), dataRow["id"].ToString()));
            }
        }

        private void ShowInfo()
        {
            if (this.isUpdate && this.model != null)
            {
                UserInfo promSuperior = UserFactory.GetPromSuperior(this.model.ID);
                if (promSuperior != null && promSuperior.ID > 0)
                {
                    this.txtGetPromSuperior.Text = promSuperior.UserName;
                }
                else
                {
                    this.txtGetPromSuperior.Text = "无代理";
                }
                this.rbuserclass.SelectedValue = this.model.classid.ToString();
                this.lblid.Text = this.model.ID.ToString();
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
                this.lblregTime.Text = this.model.RegTime.ToString("yyyy-MM-dd HH:mm");
                this.lblbalance.Text = this.model.Balance.ToString("0.00");
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
                this.lblIntegral.Text = this.model.Integral.ToString();
                if (!string.IsNullOrEmpty(this.ddlTocashScheme.SelectedValue))
                {
                    int num = int.Parse(this.ddlTocashScheme.SelectedValue);
                }
                this.txtapikey.Text = this.model.APIKey;
                this.lbllastLoginIp.Text = this.model.LastLoginIp;
                this.lbllastLoginTime.Text = this.model.LastLoginTime.ToString("yyyy-MM-dd HH:mm");
                this.txtdesc.Text = this.model.Desc;
                this.ddlmange.SelectedValue = this.model.manageId.ToString();
                this.txtquestion.Text = this.model.question;
                this.txtanswer.Text = this.model.answer;
                this.cb_isRealNamePass.Checked = (this.model.IsRealNamePass == 1);
                this.cb_isEmailPass.Checked = (this.model.IsEmailPass == 1);
                this.cb_isPhonePass.Checked = (this.model.IsPhonePass == 1);
                this.txtUrlNoRefPayUrl.Text = WebInfoFactory.CurrentWebInfo.PayUrl + string.Format("links/pay.aspx?u={0}&k={1}", this.model.ID, Cryptography.MD5(this.model.ID.ToString() + Constant.ParameterEncryptionKey));
                this.txtsmsNotifyUrl.Text = this.model.smsNotifyUrl;
                this.rbl_settledmode.SelectedValue = this.model.Settles.ToString();
                this.cb_isdebug.Checked = (this.model.isdebug == 1);
            }
        }

        private void Save()
        {
            List<UsersUpdateLog> list = new List<UsersUpdateLog>();
            string text = this.txtuserName.Text;
            string text2 = this.txtpassword.Text;
            int num = 0;
            int.TryParse(this.txtCPSDrate.Text, out num);
            int num2 = 0;
            int.TryParse(this.txtCVSNrate.Text, out num2);
            string text3 = this.txtemail.Text;
            string text4 = this.txtqq.Text;
            string text5 = this.txttel.Text;
            string text6 = this.txtidCard.Text;
            int num3 = 0;
            int.TryParse(this.rblsettlemode.SelectedValue, out num3);
            string text7 = this.txtpayeeName.Text;
            string text8 = this.txtpayeeBank.Text;
            string text9 = this.txtbankProvince.Text;
            string text10 = this.txtbankCity.Text;
            string text11 = this.txtbankAddress.Text;
            int num4 = int.Parse(this.ddlStatus.SelectedValue);
            this.model.classid = int.Parse(this.rbuserclass.SelectedValue);
            string text12 = this.txtaccount.Text;
            string text13 = this.txtsiteName.Text;
            string text14 = this.txtsiteUrl.Text;
            UserTypeEnum userTypeEnum = (UserTypeEnum)int.Parse(this.rbluserType.SelectedValue);
            int num5 = 0;
            if (userTypeEnum == UserTypeEnum.会员)
            {
                num5 = int.Parse(this.ddlmemvip.SelectedValue);
            }
            else if (userTypeEnum == UserTypeEnum.代理)
            {
                num5 = int.Parse(this.ddlpromvip.SelectedValue);
            }
            string text15 = this.txtapikey.Text;
            this.model.UserName = text;
            this.model.APIAccount = (long)int.Parse(this.txtapiAcct.Text);
            this.model.Settles = Convert.ToInt32(this.rbl_settledmode.SelectedValue);
            this.model.smsNotifyUrl = this.txtsmsNotifyUrl.Text;
            if (!string.IsNullOrEmpty(text2))
            {
                text2 = Cryptography.MD5(text2);
                if (this.isUpdate && text2 != this.model.Password)
                {
                    list.Add(this.newUpdateLog("password", text2, this.model.Password));
                }
                this.model.Password = text2;
            }
            if (!string.IsNullOrEmpty(this.txtpassword2.Text.Trim()))
            {
                string password = Cryptography.MD5(this.txtpassword2.Text.Trim());
                this.model.Password2 = password;
            }
            if (this.isUpdate && num != this.model.CPSDrate)
            {
                list.Add(this.newUpdateLog("CPSDrate", num.ToString(), this.model.CPSDrate.ToString()));
            }
            this.model.CPSDrate = num;
            if (this.isUpdate && num2 != this.model.CVSNrate)
            {
                list.Add(this.newUpdateLog("CVSNrate", num2.ToString(), this.model.CVSNrate.ToString()));
            }
            this.model.CVSNrate = num2;
            if (this.isUpdate && text3 != this.model.Email)
            {
                list.Add(this.newUpdateLog("Email", text3, this.model.Email));
            }
            this.model.Email = text3;
            if (this.isUpdate && text4 != this.model.QQ)
            {
                list.Add(this.newUpdateLog("QQ", text4, this.model.QQ));
            }
            this.model.QQ = text4;
            if (this.isUpdate && text5 != this.model.Tel)
            {
                list.Add(this.newUpdateLog("tel", text5, this.model.Tel));
            }
            this.model.Tel = text5;
            if (this.isUpdate && text6 != this.model.IdCard)
            {
                list.Add(this.newUpdateLog("idCard", text6, this.model.IdCard));
            }
            this.model.IdCard = text6;
            if (this.isUpdate && num3 != this.model.PMode)
            {
                list.Add(this.newUpdateLog("pmode", num3.ToString(), this.model.PMode.ToString()));
            }
            this.model.PMode = num3;
            if (this.isUpdate && text12 != this.model.Account)
            {
                list.Add(this.newUpdateLog("account", text12, this.model.Account));
            }
            this.model.Account = text12;
            if (this.isUpdate && text7 != this.model.PayeeName)
            {
                list.Add(this.newUpdateLog("payeeName", text7, this.model.PayeeName));
            }
            this.model.PayeeName = text7;
            if (this.isUpdate && text8 != this.model.PayeeBank)
            {
                list.Add(this.newUpdateLog("payeeBank", text8, this.model.PayeeBank));
            }
            this.model.PayeeBank = text8;
            if (this.isUpdate && text9 != this.model.BankProvince)
            {
                list.Add(this.newUpdateLog("BankProvince", text9, this.model.BankProvince));
            }
            this.model.BankProvince = text9;
            if (this.isUpdate && text10 != this.model.BankCity)
            {
                list.Add(this.newUpdateLog("BankCity", text10, this.model.BankCity));
            }
            this.model.BankCity = text10;
            if (this.isUpdate && text11 != this.model.BankAddress)
            {
                list.Add(this.newUpdateLog("bankAddress", text11, this.model.BankAddress));
            }
            this.model.BankAddress = text11;
            if (this.isUpdate && num4 != this.model.Status)
            {
                list.Add(this.newUpdateLog("status", num4.ToString(), this.model.Status.ToString()));
            }
            this.model.Status = num4;
            if (this.isUpdate && text13 != this.model.SiteName)
            {
                list.Add(this.newUpdateLog("SiteName", text13, this.model.SiteName));
            }
            this.model.SiteName = text13;
            if (this.isUpdate && text14 != this.model.SiteUrl)
            {
                list.Add(this.newUpdateLog("siteUrl", text14, this.model.SiteUrl));
            }
            this.model.SiteUrl = text14;
            if (this.isUpdate && userTypeEnum != this.model.UserType)
            {
                list.Add(this.newUpdateLog("userType", userTypeEnum.ToString(), ((int)this.model.UserType).ToString()));
            }
            this.model.UserType = userTypeEnum;
            if (this.isUpdate && num5 != (int)this.model.UserLevel)
            {
                list.Add(this.newUpdateLog("userLevel", num5.ToString(), ((int)this.model.UserLevel).ToString()));
            }
            this.model.UserLevel = (UserLevelEnum)num5;
            int num6 = 0;
            if (!string.IsNullOrEmpty(this.ddlTocashScheme.SelectedValue))
            {
                num6 = int.Parse(this.ddlTocashScheme.SelectedValue);
            }
            if (this.isUpdate && num6 != this.model.MaxDayToCashTimes)
            {
                list.Add(this.newUpdateLog("MaxDayToCashTimes", num6.ToString(), this.model.MaxDayToCashTimes.ToString()));
            }
            this.model.MaxDayToCashTimes = num6;
            if (this.isUpdate && text15 != this.model.APIKey)
            {
                list.Add(this.newUpdateLog("APIKey", text15, this.model.APIKey));
            }
            this.model.APIKey = text15;
            this.model.Desc = this.txtdesc.Text;
            this.model.IsRealNamePass = (this.cb_isRealNamePass.Checked ? 1 : 0);
            this.model.IsEmailPass = (this.cb_isEmailPass.Checked ? 1 : 0);
            this.model.IsPhonePass = (this.cb_isPhonePass.Checked ? 1 : 0);
            this.model.isdebug = (this.cb_isdebug.Checked ? 1 : 0);
            if (!string.IsNullOrEmpty(this.ddlmange.SelectedValue))
            {
                this.model.manageId = new int?(int.Parse(this.ddlmange.SelectedValue));
            }
            if (!this.isUpdate)
            {
                int num7 = UserFactory.Add(this.model);
                if (num7 > 0)
                {
                    base.AlertAndRedirect("保存成功！", "UserList.aspx");
                }
                else
                {
                    base.AlertAndRedirect("保存失败！");
                }
            }
            else if (UserFactory.Update(this.model, list))
            {
                base.AlertAndRedirect("更新成功！", "UserList.aspx");
            }
            else
            {
                base.AlertAndRedirect("更新失败！");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Merchant))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private UsersUpdateLog newUpdateLog(string f, string n, string o)
        {
            return new UsersUpdateLog
            {
                userid = this.model.ID,
                Addtime = DateTime.Now,
                field = f,
                newvalue = n,
                oldValue = o,
                Editor = ManageFactory.CurrentManage.username,
                OIp = ServerVariables.TrueIP
            };
        }
    }
}
