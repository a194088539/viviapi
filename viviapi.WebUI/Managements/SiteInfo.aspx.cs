namespace viviapi.WebUI.Managements
{
    using System;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib;

    public partial class SiteInfo : ManagePageBase
    {
        private WebInfo _objectInfo = null;

        protected void Bind()
        {
            this.jsqqpanle.InnerHtml = "";
            this.kefu.InnerHtml = "";
            this.txtDomain.Text = this.ObjectInfo.Domain;
            this.txtPayUrl.Text = this.ObjectInfo.PayUrl;
            this.txtFooter.Text = this.ObjectInfo.Footer;
            this.txtName.Text = this.ObjectInfo.Name;
            this.txtPhone.Text = this.ObjectInfo.Phone;
            this.hdTemplate.Value = this.ObjectInfo.TemplateId.ToString();
            this.txtCode.Text = this.ObjectInfo.Code;
            this.txtJSQQ.Text = this.ObjectInfo.Jsqq;
            this.txtKFQQ.Text = this.ObjectInfo.Kfqq;
            this.txtapibankname.Text = this.ObjectInfo.apibankname;
            this.txtapibankversion.Text = this.ObjectInfo.apibankversion;
            this.txtapicardname.Text = this.ObjectInfo.apicardname;
            this.txtapicardversion.Text = this.ObjectInfo.apicardversion;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            this.ObjectInfo.Domain = this.txtDomain.Text.ToLower();
            this.ObjectInfo.PayUrl = this.txtPayUrl.Text.ToLower();
            this.ObjectInfo.Footer = this.txtFooter.Text;
            this.ObjectInfo.Name = this.txtName.Text;
            this.ObjectInfo.Phone = this.txtPhone.Text;
            this.ObjectInfo.Jsqq = this.txtJSQQ.Text;
            this.ObjectInfo.Kfqq = this.txtKFQQ.Text;
            this.ObjectInfo.Code = this.txtCode.Text;
            this.ObjectInfo.apibankname = this.txtapibankname.Text.Trim();
            this.ObjectInfo.apibankversion = this.txtapibankversion.Text.Trim();
            this.ObjectInfo.apicardname = this.txtapicardname.Text.Trim();
            this.ObjectInfo.apicardversion = this.txtapicardversion.Text.Trim();
            if (this.txtName.Text == "")
            {
                base.AlertAndRedirect("网站名称不能为空!");
            }
            else if (this.txtDomain.Text == "")
            {
                base.AlertAndRedirect("域名不能为空!");
            }
            else if (this.txtPhone.Text == "")
            {
                base.AlertAndRedirect("联系电话不能为空!");
            }
            else if (WebInfoFactory.Update(this.ObjectInfo))
            {
                SysConfig.Update(1, this.ddlstatus.SelectedValue);
                SysConfig.Update(2, this.ddlopen.SelectedValue);
                SysConfig.Update(3, this.rbl_mobilval.SelectedValue);
                int result = 0;
                int.TryParse(this.txtMobilMaxSendTimes.Text.Trim(), out result);
                SysConfig.Update(4, result.ToString());
                SysConfig.Update(0x25, this.txtRefCount.Text.Trim());
                SysConfig.Update(0x26, this.rbl_NoRef.SelectedValue);
                SysConfig.Update(0x27, this.txtuserloginMsgForlock.Text.Trim());
                SysConfig.Update(40, this.txtUserloginMsgForUnCheck.Text);
                SysConfig.Update(0x29, this.txtUserloginMsgForCheckfail.Text.Trim());
                SysConfig.Update(0x2a, this.rblOpenDeduct.SelectedValue);
                SysConfig.Update(0x2b, this.txtDefaultCPSDrate.Text);
                SysConfig.Update(0x2c, this.rbl_isopenCash.SelectedValue);
                SysConfig.Update(0x2d, this.txtclosecashReason.Text);
                SysConfig.Update(0x2e, this.rbl_settledmode.SelectedValue);
                SysConfig.Update(0x2f, this.rbl_ActivationByEmail.SelectedValue);
                SysConfig.Update(0x34, this.rbl_debuglog.SelectedValue);
                SysConfig.Update(0x39, this.txtTitleSuffix.Text);
                SysConfig.Update(0x3a, this.txtWebSiteKey.Text);
                SysConfig.Update(0x3b, this.txtWebSitedescription.Text);
                SysConfig.Update(0x45, this.rbl_isUserloginByEmail.SelectedValue);
                SysConfig.Update(0x48, this.TextPhone.Text);
                SysConfig.Update(0x47, this.RadioButtonPhone.SelectedValue);
                SysConfig.Update(0x4b, this.RadioButtonshouji.SelectedValue);
                SysConfig.Update(0x4c, this.RadioButtonemail.SelectedValue);
                SysConfig.Update(0x4d, this.txtbank.Text);
                SysConfig.Update(0x4e, this.txtweixin.Text);
                SysConfig.Update(0x4f, this.txtali.Text);
                base.AlertAndRedirect("更新成功!", "siteinfo.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.Bind();
                this.ddlstatus.SelectedValue = SysConfig.IsAudit ? "1" : "0";
                this.ddlopen.SelectedValue = SysConfig.IsOpenRegistration ? "1" : "0";
                this.rbl_mobilval.SelectedValue = SysConfig.IsPhoneVerification ? "1" : "0";
                if (SysConfig.IsPhoneVerification)
                {
                    this.txtMobilMaxSendTimes.Text = SysConfig.MaxInformationNumber.ToString();
                }
                else
                {
                    this.txtMobilMaxSendTimes.Text = string.Empty;
                }
                this.txtRefCount.Text = SysConfig.LaiLuCount.ToString();
                if (SysConfig.IsOpenNoLaiLu)
                {
                    this.rbl_NoRef.SelectedValue = "1";
                }
                else
                {
                    this.rbl_NoRef.SelectedValue = "0";
                }
                this.txtuserloginMsgForlock.Text = SysConfig.UserloginMsgForlock;
                this.txtUserloginMsgForUnCheck.Text = SysConfig.UserloginMsgForUnCheck;
                this.txtUserloginMsgForCheckfail.Text = SysConfig.UserloginMsgForCheckfail;
                this.rblOpenDeduct.SelectedValue = SysConfig.isOpenDeduct ? "1" : "0";
                this.txtDefaultCPSDrate.Text = SysConfig.DefaultCPSDrate.ToString();
                this.rbl_isopenCash.SelectedValue = SysConfig.isopenCash ? "1" : "0";
                this.txtclosecashReason.Text = SysConfig.closecashReason;
                this.rbl_ActivationByEmail.SelectedValue = SysConfig.RegistrationActivationByEmail ? "1" : "0";
                this.rbl_settledmode.SelectedValue = SysConfig.DefaultSettledMode.ToString();
                this.rbl_debuglog.SelectedValue = SysConfig.debuglog ? "1" : "0";
                this.TextPhone.Text = SysConfig.textPhone.ToString();
                this.RadioButtonPhone.SelectedValue = SysConfig.radioButtonPhone ? "1" : "0";
                this.RadioButtonshouji.SelectedValue = SysConfig.radioButtonshouji ? "1" : "0";
                this.RadioButtonemail.SelectedValue = SysConfig.radioButtonemail ? "1" : "0";
                this.txtTitleSuffix.Text = SysConfig.WebSiteTitleSuffix;
                this.txtWebSiteKey.Text = SysConfig.WebSiteKey;
                this.txtWebSitedescription.Text = SysConfig.WebSitedescription;
                this.rbl_isUserloginByEmail.SelectedValue = SysConfig.isUserloginByEmail;
                this.txtbank.Text = SysConfig.banklimit.ToString();
                this.txtweixin.Text = SysConfig.weixinlimit.ToString();
                this.txtali.Text = SysConfig.alilimit.ToString();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.System))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public WebInfo ObjectInfo
        {
            get
            {
                if (this._objectInfo == null)
                {
                    this._objectInfo = WebInfoFactory.GetWebInfoByDomain(XRequest.GetHost());
                }
                return this._objectInfo;
            }
        }
    }
}

