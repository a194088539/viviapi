namespace viviapi.WebUI.Managements
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib;

    public class chargessetting : ManagePageBase
    {
        private WebInfo _objectInfo = null;
        protected Button btn_Update;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected TextBox txtCashTimes1;
        protected TextBox txtCashTimes2;
        protected TextBox txtCashTimes3;
        protected TextBox txtCharges1;
        protected TextBox txtCharges2;
        protected TextBox txtCharges3;
        protected TextBox txtMaximum1;
        protected TextBox txtMaximum2;
        protected TextBox txtMaximum3;
        protected TextBox txtMinimum1;
        protected TextBox txtMinimum2;
        protected TextBox txtMinimum3;

        protected void Bind()
        {
            this.txtMinimum1.Text = SysConfig.MinGetMoney.ToString("f2");
            this.txtMaximum1.Text = SysConfig.MaxGetMoney.ToString("f2");
            this.txtCashTimes1.Text = SysConfig.CashTimesEveryDay.ToString();
            this.txtMinimum2.Text = SysConfig.MinGetMoney1.ToString("f2");
            this.txtMaximum2.Text = SysConfig.MaxGetMoney1.ToString("f2");
            this.txtCashTimes2.Text = SysConfig.CashTimesEveryDay1.ToString();
            this.txtCharges2.Text = SysConfig.Charges1.ToString("f4");
            this.txtMinimum3.Text = SysConfig.MinGetMoney2.ToString("f2");
            this.txtMaximum3.Text = SysConfig.MaxGetMoney2.ToString("f2");
            this.txtCashTimes3.Text = SysConfig.CashTimesEveryDay2.ToString();
            this.txtCharges3.Text = SysConfig.Charges2.ToString("f4");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal result = 0M;
                int num2 = 0;
                if (decimal.TryParse(this.txtMinimum1.Text.Trim(), out result))
                {
                    SysConfig.Update(0x19, this.txtMinimum1.Text.Trim());
                }
                if (decimal.TryParse(this.txtMaximum1.Text.Trim(), out result))
                {
                    SysConfig.Update(0x1a, this.txtMaximum1.Text.Trim());
                }
                if (int.TryParse(this.txtCashTimes1.Text.Trim(), out num2))
                {
                    SysConfig.Update(0x1b, num2.ToString());
                }
                if (decimal.TryParse(this.txtCharges1.Text.Trim(), out result))
                {
                    SysConfig.Update(0x1c, this.txtCharges1.Text.Trim());
                }
                if (decimal.TryParse(this.txtMinimum2.Text.Trim(), out result))
                {
                    SysConfig.Update(0x1d, this.txtMinimum2.Text.Trim());
                }
                if (decimal.TryParse(this.txtMaximum2.Text.Trim(), out result))
                {
                    SysConfig.Update(30, this.txtMaximum2.Text.Trim());
                }
                if (int.TryParse(this.txtCashTimes2.Text.Trim(), out num2))
                {
                    SysConfig.Update(0x1f, num2.ToString());
                }
                if (decimal.TryParse(this.txtCharges2.Text.Trim(), out result))
                {
                    SysConfig.Update(0x20, this.txtCharges2.Text.Trim());
                }
                if (decimal.TryParse(this.txtMinimum3.Text.Trim(), out result))
                {
                    SysConfig.Update(0x21, this.txtMinimum3.Text.Trim());
                }
                if (decimal.TryParse(this.txtMaximum3.Text.Trim(), out result))
                {
                    SysConfig.Update(0x22, this.txtMaximum3.Text.Trim());
                }
                if (int.TryParse(this.txtCashTimes3.Text.Trim(), out num2))
                {
                    SysConfig.Update(0x23, num2.ToString());
                }
                if (decimal.TryParse(this.txtCharges3.Text.Trim(), out result))
                {
                    SysConfig.Update(0x24, this.txtCharges3.Text.Trim());
                }
                base.AlertAndRedirect("设置成功");
            }
            catch
            {
                base.AlertAndRedirect("设置失败");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.Bind();
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

