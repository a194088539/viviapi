namespace viviapi.WebUI.Managements.Withdraw
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Settled;
    using viviapi.Model;
    using viviapi.Model.Settled;
    using viviapi.WebComponents.Web;
    using viviLib.Text;
    using viviLib.Web;

    public class AgentDistsSchemeModi : ManagePageBase
    {
        public TocashSchemeInfo _ItemInfo = null;
        protected Button btnAdd;
        protected HtmlForm form1;
        protected RadioButtonList rblisdefault;
        protected RadioButtonList rbltranRequiredAudit;
        protected RadioButtonList rblVaiInterface;
        protected TextBox txtbankdetentiondays;
        protected TextBox txtcarddetentiondays;
        protected TextBox txtchargeleastofeach;
        protected TextBox txtchargemostofeach;
        protected TextBox txtchargerate;
        protected TextBox txtdailymaxamt;
        protected TextBox txtdailymaxtimes;
        protected TextBox txtmaxamtlimitofeach;
        protected TextBox txtminamtlimitofeach;
        protected TextBox txtotherdetentiondays;
        protected TextBox txtschemename;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtschemename.Text.Trim().Length == 0)
            {
                msg = msg + "方案名称不能为空！\n";
            }
            if (!this.isNum(this.txtminamtlimitofeach.Text))
            {
                msg = msg + "最低提现金额限制(每笔)格式错误！\n";
            }
            if (!this.isNum(this.txtmaxamtlimitofeach.Text))
            {
                msg = msg + "最大提现金额限制(每笔)格式错误！\n";
            }
            if (!PageValidate.IsNumber(this.txtdailymaxtimes.Text))
            {
                msg = msg + "每天最多可提现次数格式错误！\n";
            }
            if (!this.isNum(this.txtdailymaxamt.Text))
            {
                msg = msg + "每天最多可限额格式错误！\n";
            }
            if (!this.isNum(this.txtchargerate.Text))
            {
                msg = msg + "提现手续费格式错误！\n";
            }
            if (!this.isNum(this.txtchargeleastofeach.Text))
            {
                msg = msg + "提现手续费最少每笔格式错误！\n";
            }
            if (!this.isNum(this.txtchargemostofeach.Text))
            {
                msg = msg + "提现手续费最高每笔格式错误！\n";
            }
            if (msg != "")
            {
                base.AlertAndRedirect(msg);
            }
            else
            {
                string text = this.txtschemename.Text;
                decimal num = decimal.Parse(this.txtminamtlimitofeach.Text);
                decimal num2 = decimal.Parse(this.txtmaxamtlimitofeach.Text);
                int num3 = int.Parse(this.txtdailymaxtimes.Text);
                decimal num4 = decimal.Parse(this.txtdailymaxamt.Text);
                decimal num5 = decimal.Parse(this.txtchargerate.Text);
                decimal num6 = decimal.Parse(this.txtchargeleastofeach.Text);
                decimal num7 = decimal.Parse(this.txtchargemostofeach.Text);
                int num8 = int.Parse(this.rblisdefault.SelectedValue);
                int num9 = int.Parse(this.rblVaiInterface.SelectedValue);
                int result = 0;
                int num11 = 0;
                int num12 = 0;
                int.TryParse(this.txtbankdetentiondays.Text.Trim(), out result);
                int.TryParse(this.txtcarddetentiondays.Text.Trim(), out num11);
                int.TryParse(this.txtotherdetentiondays.Text.Trim(), out num12);
                this.model.schemename = text;
                this.model.minamtlimitofeach = num;
                this.model.maxamtlimitofeach = num2;
                this.model.dailymaxtimes = num3;
                this.model.dailymaxamt = num4;
                this.model.chargerate = num5;
                this.model.chargeleastofeach = num6;
                this.model.chargemostofeach = num7;
                this.model.isdefault = num8;
                this.model.vaiInterface = num9;
                this.model.bankdetentiondays = result;
                this.model.carddetentiondays = num11;
                this.model.otherdetentiondays = num12;
                this.model.tranRequiredAudit = Convert.ToByte(this.rbltranRequiredAudit.SelectedValue);
                bool flag = false;
                if (this.isUpdate)
                {
                    if (TocashScheme.Update(this.model))
                    {
                        flag = true;
                    }
                }
                else
                {
                    this.model.type = 2;
                    if (TocashScheme.Add(this.model) > 0)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    base.AlertAndRedirect("操作成功", "AgentDistsSchemes.aspx");
                }
                else
                {
                    base.AlertAndRedirect("操作失败");
                }
            }
        }

        private void InitForm()
        {
            if (this.isUpdate)
            {
                this.txtschemename.Text = this.model.schemename;
                this.txtminamtlimitofeach.Text = this.model.minamtlimitofeach.ToString();
                this.txtmaxamtlimitofeach.Text = this.model.maxamtlimitofeach.ToString();
                this.txtdailymaxtimes.Text = this.model.dailymaxtimes.ToString();
                this.txtdailymaxamt.Text = this.model.dailymaxamt.ToString();
                this.txtchargerate.Text = this.model.chargerate.ToString();
                this.txtchargeleastofeach.Text = this.model.chargeleastofeach.ToString();
                this.txtchargemostofeach.Text = this.model.chargemostofeach.ToString();
                this.rblisdefault.SelectedValue = this.model.isdefault.ToString();
                this.rblVaiInterface.SelectedValue = this.model.vaiInterface.ToString();
                this.txtbankdetentiondays.Text = this.model.bankdetentiondays.ToString();
                this.txtcarddetentiondays.Text = this.model.carddetentiondays.ToString();
                this.txtotherdetentiondays.Text = this.model.otherdetentiondays.ToString();
                this.rbltranRequiredAudit.SelectedValue = this.model.tranRequiredAudit.ToString();
            }
        }

        private bool isNum(string _input)
        {
            return (PageValidate.IsNumber(_input) || PageValidate.IsDecimal(_input));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.InitForm();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Financial))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public string Action
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public bool isUpdate
        {
            get
            {
                return ((this.ItemInfoId > 0) && (this.Action == "edit"));
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public TocashSchemeInfo model
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.isUpdate)
                    {
                        this._ItemInfo = TocashScheme.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new TocashSchemeInfo();
                    }
                }
                return this._ItemInfo;
            }
        }
    }
}

