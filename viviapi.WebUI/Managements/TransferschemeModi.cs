namespace viviapi.WebUI.Managements
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Text;

    public class TransferschemeModi : ManagePageBase
    {
        public viviapi.Model.Settled.transferscheme _ItemInfo = null;
        protected Button btnAdd;
        protected HtmlForm form1;
        protected RadioButtonList rblisdefault;
        protected viviapi.BLL.Settled.transferscheme tsBLL = new viviapi.BLL.Settled.transferscheme();
        protected TextBox txtchargeleastofeach;
        protected TextBox txtchargemostofeach;
        protected TextBox txtchargerate;
        protected TextBox txtdailymaxamt;
        protected TextBox txtdailymaxtimes;
        protected TextBox txtmaxamtlimitofeach;
        protected TextBox txtminamtlimitofeach;
        protected TextBox txtmonthmaxamt;
        protected TextBox txtschemename;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtschemename.Text.Trim().Length == 0)
            {
                msg = msg + "方案名称不能为空！\n";
            }
            if (!this.isNum(this.txtmonthmaxamt.Text.Trim()))
            {
                msg = msg + "每月免费流量 格式错误\n";
            }
            if (!this.isNum(this.txtminamtlimitofeach.Text.Trim()))
            {
                msg = msg + "最低提现金额限制(每笔)格式错误！\n";
            }
            if (!this.isNum(this.txtmaxamtlimitofeach.Text.Trim()))
            {
                msg = msg + "最大提现金额限制(每笔)格式错误！\n";
            }
            if (!PageValidate.IsNumber(this.txtdailymaxtimes.Text.Trim()))
            {
                msg = msg + "每天最多可提现次数格式错误！\n";
            }
            if (!this.isNum(this.txtdailymaxamt.Text.Trim()))
            {
                msg = msg + "每天最多可限额格式错误！\n";
            }
            if (!this.isNum(this.txtchargerate.Text.Trim()))
            {
                msg = msg + "提现手续费格式错误！\n";
            }
            if (!this.isNum(this.txtchargeleastofeach.Text.Trim()))
            {
                msg = msg + "提现手续费最少每笔格式错误！\n";
            }
            if (!this.isNum(this.txtchargemostofeach.Text.Trim()))
            {
                msg = msg + "提现手续费最高每笔格式错误！\n";
            }
            if (msg != "")
            {
                base.AlertAndRedirect(msg);
            }
            else
            {
                string str2 = this.txtschemename.Text.Trim();
                decimal num = decimal.Parse(this.txtminamtlimitofeach.Text.Trim());
                decimal num2 = decimal.Parse(this.txtmaxamtlimitofeach.Text.Trim());
                int num3 = int.Parse(this.txtdailymaxtimes.Text.Trim());
                decimal num4 = decimal.Parse(this.txtdailymaxamt.Text.Trim());
                decimal num5 = decimal.Parse(this.txtchargerate.Text.Trim());
                decimal num6 = decimal.Parse(this.txtchargeleastofeach.Text.Trim());
                decimal num7 = decimal.Parse(this.txtchargemostofeach.Text.Trim());
                int num8 = int.Parse(this.rblisdefault.SelectedValue);
                this.model.schemename = str2;
                this.model.minamtlimitofeach = num;
                this.model.maxamtlimitofeach = num2;
                this.model.monthmaxamt = decimal.Parse(this.txtmonthmaxamt.Text);
                this.model.monthmaxtimes = 0;
                this.model.dailymaxtimes = num3;
                this.model.dailymaxamt = num4;
                this.model.chargerate = num5;
                this.model.chargeleastofeach = num6;
                this.model.chargemostofeach = num7;
                this.model.isdefault = num8;
                bool flag = false;
                if (this.isUpdate)
                {
                    if (this.tsBLL.Update(this.model))
                    {
                        flag = true;
                    }
                }
                else if (this.tsBLL.Add(this.model) > 0)
                {
                    flag = true;
                }
                if (flag)
                {
                    base.AlertAndRedirect("操作成功");
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
                this.txtmonthmaxamt.Text = this.model.monthmaxamt.ToString();
            }
        }

        private bool isNum(string input)
        {
            return (PageValidate.IsNumber(input) || PageValidate.IsDecimal(input));
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

        public bool isUpdate
        {
            get
            {
                return (this.model.id > 0);
            }
        }

        public viviapi.Model.Settled.transferscheme model
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    this._ItemInfo = this.tsBLL.GetModel(1);
                }
                if (this._ItemInfo == null)
                {
                    this._ItemInfo = new viviapi.Model.Settled.transferscheme();
                }
                return this._ItemInfo;
            }
        }
    }
}

