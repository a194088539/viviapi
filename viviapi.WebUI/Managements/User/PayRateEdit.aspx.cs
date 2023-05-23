namespace viviapi.WebUI.Managements.User
{
    using System;
    using viviapi.BLL;
    using viviapi.BLL.Payment;
    using viviapi.Model;
    using viviapi.Model.Payment;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public partial class PayRateEdit : ManagePageBase
    {
        public PayRate _model = null;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtlevName.Text.Trim().Length == 0)
            {
                msg = msg + "levName不能为空！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp89.Text))
            {
                msg = msg + "p89格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp88.Text))
            {
                msg = msg + "p88格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp87.Text))
            {
                msg = msg + "p87格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp86.Text))
            {
                msg = msg + "p86格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp91.Text))
            {
                msg = msg + "p91格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp92.Text))
            {
                msg = msg + "p92格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp93.Text))
            {
                msg = msg + "p93格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp97.Text))
            {
                msg = msg + "p97格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp95.Text))
            {
                msg = msg + "p95格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp94.Text))
            {
                msg = msg + "p94格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp96.Text))
            {
                msg = msg + "p96格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp98.Text))
            {
                msg = msg + "p98格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp99.Text))
            {
                msg = msg + "p99格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp100.Text))
            {
                msg = msg + "p100格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp101.Text))
            {
                msg = msg + "p101格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp102.Text))
            {
                msg = msg + "p102格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp103.Text))
            {
                msg = msg + "p103格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp104.Text))
            {
                msg = msg + "p104格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp105.Text))
            {
                msg = msg + "p105格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp106.Text))
            {
                msg = msg + "p106格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp107.Text))
            {
                msg = msg + "p107格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp108.Text))
            {
                msg = msg + "p108格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp109.Text))
            {
                msg = msg + "p109格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp110.Text))
            {
                msg = msg + "p110格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp111.Text))
            {
                msg = msg + "p111格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp112.Text))
            {
                msg = msg + "p112格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp113.Text))
            {
                msg = msg + "p113格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp114.Text))
            {
                msg = msg + "p114格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp115.Text))
            {
                msg = msg + "p115格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp116.Text))
            {
                msg = msg + "p116格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp117.Text))
            {
                msg = msg + "p117格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp118.Text))
            {
                msg = msg + "p118格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp119.Text))
            {
                msg = msg + "p119格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp200.Text))
            {
                msg = msg + "p200格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp201.Text))
            {
                msg = msg + "p201格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp202.Text))
            {
                msg = msg + "p202格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp203.Text))
            {
                msg = msg + "p203格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp204.Text))
            {
                msg = msg + "p204格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp205.Text))
            {
                msg = msg + "p205格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp208.Text))
            {
                msg = msg + "p208格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp209.Text))
            {
                msg = msg + "p209格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp90.Text))
            {
                msg = msg + "p90格式错误！\n";
            }
            if (msg != "")
            {
                base.AlertAndRedirect(msg);
            }
            else
            {
                string text = this.txtlevName.Text;
                decimal num = decimal.Parse(this.txtp95.Text) / 100M;
                decimal num2 = decimal.Parse(this.txtp94.Text) / 100M;
                decimal num3 = decimal.Parse(this.txtp96.Text) / 100M;
                decimal num4 = decimal.Parse(this.txtp98.Text) / 100M;
                decimal num5 = decimal.Parse(this.txtp99.Text) / 100M;
                decimal num6 = decimal.Parse(this.txtp100.Text) / 100M;
                decimal num7 = decimal.Parse(this.txtp101.Text) / 100M;
                decimal num8 = decimal.Parse(this.txtp102.Text) / 100M;
                decimal num9 = decimal.Parse(this.txtp103.Text) / 100M;
                decimal num10 = decimal.Parse(this.txtp104.Text) / 100M;
                decimal num11 = decimal.Parse(this.txtp105.Text) / 100M;
                decimal num12 = decimal.Parse(this.txtp106.Text) / 100M;
                decimal num13 = decimal.Parse(this.txtp107.Text) / 100M;
                decimal num14 = decimal.Parse(this.txtp108.Text) / 100M;
                decimal num15 = decimal.Parse(this.txtp109.Text) / 100M;
                decimal num16 = decimal.Parse(this.txtp110.Text) / 100M;
                decimal num17 = decimal.Parse(this.txtp111.Text) / 100M;
                decimal num18 = decimal.Parse(this.txtp112.Text) / 100M;
                decimal num19 = decimal.Parse(this.txtp113.Text) / 100M;
                decimal num20 = decimal.Parse(this.txtp114.Text) / 100M;
                decimal num21 = decimal.Parse(this.txtp115.Text) / 100M;
                decimal num22 = decimal.Parse(this.txtp116.Text) / 100M;
                decimal num23 = decimal.Parse(this.txtp117.Text) / 100M;
                decimal num24 = decimal.Parse(this.txtp118.Text) / 100M;
                decimal num25 = decimal.Parse(this.txtp119.Text) / 100M;
                decimal num26 = decimal.Parse(this.txtp200.Text) / 100M;
                decimal num27 = decimal.Parse(this.txtp201.Text) / 100M;
                decimal num28 = decimal.Parse(this.txtp202.Text) / 100M;
                decimal num29 = decimal.Parse(this.txtp203.Text) / 100M;
                decimal num30 = decimal.Parse(this.txtp204.Text) / 100M;
                decimal num31 = decimal.Parse(this.txtp205.Text) / 100M;
                decimal num32 = decimal.Parse(this.txtp208.Text) / 100M;
                decimal num33 = decimal.Parse(this.txtp209.Text) / 100M;
                decimal num34 = decimal.Parse(this.txtp210.Text) / 100M;
                decimal num35 = decimal.Parse(this.txtp90.Text) / 100M;
                decimal num36 = decimal.Parse(this.txtp91.Text) / 100M;
                decimal num37 = decimal.Parse(this.txtp92.Text) / 100M;
                decimal num38 = decimal.Parse(this.txtp93.Text) / 100M;
                decimal num39 = decimal.Parse(this.txtp97.Text) / 100M;
                decimal num40 = decimal.Parse(this.txtp89.Text) / 100M;
                decimal num41 = decimal.Parse(this.txtp88.Text) / 100M;
                decimal num42 = decimal.Parse(this.txtp87.Text) / 100M;
                decimal num43 = decimal.Parse(this.txtp86.Text) / 100M;
                this.model.levName = text;
                if (!this.isUpdate)
                {
                    this.model.rateType = (RateTypeEnum)int.Parse(this.rbl_type.SelectedValue);
                }
                this.model.p95 = num;
                this.model.p94 = num2;
                this.model.p96 = num3;
                this.model.p98 = num4;
                this.model.p99 = num5;
                this.model.p100 = num6;
                this.model.p101 = num7;
                this.model.p102 = num8;
                this.model.p103 = num9;
                this.model.p104 = num10;
                this.model.p105 = num11;
                this.model.p106 = num12;
                this.model.p107 = num13;
                this.model.p108 = num14;
                this.model.p109 = num15;
                this.model.p110 = num16;
                this.model.p111 = num17;
                this.model.p112 = num18;
                this.model.p113 = num19;
                this.model.p114 = num20;
                this.model.p115 = num21;
                this.model.p116 = num22;
                this.model.p117 = num23;
                this.model.p118 = num24;
                this.model.p119 = num25;
                this.model.p200 = num26;
                this.model.p201 = num27;
                this.model.p202 = num28;
                this.model.p203 = num29;
                this.model.p204 = num30;
                this.model.p205 = num31;
                this.model.p208 = num32;
                this.model.p209 = num33;
                this.model.p210 = num34;
                this.model.p90 = num35;
                this.model.p91 = num36;
                this.model.p92 = num37;
                this.model.p93 = num38;
                this.model.p97 = num39;
                this.model.p89 = num40;
                this.model.p88 = num41;
                this.model.p87 = num42;
                this.model.p86 = num43;
                if (this.isUpdate)
                {
                    if (PayRateFactory.Update(this.model))
                    {
                        base.AlertAndRedirect("修改成功", "PayRate.aspx");
                    }
                    else
                    {
                        base.AlertAndRedirect("修改失败");
                    }
                }
                else if (PayRateFactory.Add(this.model) > 0)
                {
                    base.AlertAndRedirect("新增成功", "PayRate.aspx");
                }
                else
                {
                    base.AlertAndRedirect("新增失败");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ShowInfo();
                this.rbl_type.Enabled = !this.isUpdate;
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Interfaces))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            if (this.isUpdate && (this.model != null))
            {
                this.txtlevName.Text = this.model.levName;
                this.rbl_type.SelectedValue = ((int)this.model.rateType).ToString();
                decimal num2 = Convert.ToDecimal(this.model.p95) * 100M;
                this.txtp95.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p89) * 100M;
                this.txtp89.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p88) * 100M;
                this.txtp88.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p87) * 100M;
                this.txtp87.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p86) * 100M;
                this.txtp86.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p91) * 100M;
                this.txtp91.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p92) * 100M;
                this.txtp92.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p93) * 100M;
                this.txtp93.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p97) * 100M;
                this.txtp97.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p94) * 100M;
                this.txtp94.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p96) * 100M;
                this.txtp96.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p98) * 100M;
                this.txtp98.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p99) * 100M;
                this.txtp99.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p100) * 100M;
                this.txtp100.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p101) * 100M;
                this.txtp101.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p102) * 100M;
                this.txtp102.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p103) * 100M;
                this.txtp103.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p104) * 100M;
                this.txtp104.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p105) * 100M;
                this.txtp105.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p106) * 100M;
                this.txtp106.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p107) * 100M;
                this.txtp107.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p108) * 100M;
                this.txtp108.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p109) * 100M;
                this.txtp109.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p110) * 100M;
                this.txtp110.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p111) * 100M;
                this.txtp111.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p112) * 100M;
                this.txtp112.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p113) * 100M;
                this.txtp113.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p114) * 100M;
                this.txtp114.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p115) * 100M;
                this.txtp115.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p116) * 100M;
                this.txtp116.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p117) * 100M;
                this.txtp117.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p118) * 100M;
                this.txtp118.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p119) * 100M;
                this.txtp119.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p200) * 100M;
                this.txtp200.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p201) * 100M;
                this.txtp201.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p202) * 100M;
                this.txtp202.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p203) * 100M;
                this.txtp203.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p204) * 100M;
                this.txtp204.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p205) * 100M;
                this.txtp205.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p208) * 100M;
                this.txtp208.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p209) * 100M;
                this.txtp209.Text = num2.ToString("0.00");
                num2 = Convert.ToDecimal(this.model.p210) * 100M;
                this.txtp210.Text = num2.ToString("0.00");
                this.txtp90.Text = (Convert.ToDecimal(this.model.p90) * 100M).ToString("0.00");
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

        public PayRate model
        {
            get
            {
                if (this._model == null)
                {
                    if (this.isUpdate)
                    {
                        this._model = PayRateFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._model = new PayRate();
                    }
                }
                return this._model;
            }
        }
    }
}

