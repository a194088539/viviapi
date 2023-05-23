namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using viviapi.BLL;
    using viviapi.BLL.Payment;
    using viviapi.Model;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public partial class PayRate : ManagePageBase
    {
        public viviapi.Model.Payment.PayRate _model = null;

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtlevName.Text.Trim().Length == 0)
            {
                msg = msg + @"levName不能为空！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp95.Text))
            {
                msg = msg + @"p95格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp94.Text))
            {
                msg = msg + @"p94格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp96.Text))
            {
                msg = msg + @"p96格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp98.Text))
            {
                msg = msg + @"p98格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp99.Text))
            {
                msg = msg + @"p99格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp100.Text))
            {
                msg = msg + @"p100格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp101.Text))
            {
                msg = msg + @"p101格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp102.Text))
            {
                msg = msg + @"p102格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp103.Text))
            {
                msg = msg + @"p103格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp104.Text))
            {
                msg = msg + @"p104格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp105.Text))
            {
                msg = msg + @"p105格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp106.Text))
            {
                msg = msg + @"p106格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp107.Text))
            {
                msg = msg + @"p107格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp108.Text))
            {
                msg = msg + @"p108格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp109.Text))
            {
                msg = msg + @"p109格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp110.Text))
            {
                msg = msg + @"p110格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp111.Text))
            {
                msg = msg + @"p111格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp112.Text))
            {
                msg = msg + @"p112格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp113.Text))
            {
                msg = msg + @"p113格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp115.Text))
            {
                msg = msg + @"p115格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp116.Text))
            {
                msg = msg + @"p116格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp117.Text))
            {
                msg = msg + @"p117格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp118.Text))
            {
                msg = msg + @"p118格式错误！\n";
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
                decimal num20 = decimal.Parse(this.txtp115.Text) / 100M;
                decimal num21 = decimal.Parse(this.txtp116.Text) / 100M;
                decimal num22 = decimal.Parse(this.txtp117.Text) / 100M;
                decimal num23 = decimal.Parse(this.txtp118.Text) / 100M;
                this.model.levName = text;
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
                this.model.p115 = num20;
                this.model.p116 = num21;
                this.model.p117 = num22;
                this.model.p118 = num23;
                if (PayRateFactory.Update(this.model))
                {
                    base.AlertAndRedirect("修改成功");
                }
                else
                {
                    base.AlertAndRedirect("修改失败");
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("PayRateEdit.aspx");
        }

        private void LoadData()
        {
            DataTable list = PayRateFactory.GetList(" 1=1  order by id asc");
            list.Columns.Add("usetypename");
            list.Columns.Add("MerName");
            foreach (DataRow row in list.Rows)
            {
                switch (Convert.ToInt32(row["rateType"]))
                {
                    case 1:
                        row["usetypename"] = "平台费率";
                        break;

                    case 2:
                        row["usetypename"] = "代理";
                        break;

                    case 3:
                        row["usetypename"] = "会员";
                        break;

                    case 4:
                        row["usetypename"] = "代理";
                        break;
                }
                row["MerName"] = Enum.GetName(typeof(UserLevelEnum), (int)row["userLevel"]);
            }
            this.repRate.DataSource = list;
            this.repRate.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
                this.LoadData();
                if (this.isUpdate)
                {
                    this.ShowInfo();
                }
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Merchant | ManageRole.Interfaces))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            this.txtlevName.Text = this.model.levName;
            this.txtp95.Text = (Convert.ToDecimal(this.model.p95) * 100M).ToString("0.00");
            this.txtp94.Text = (Convert.ToDecimal(this.model.p94) * 100M).ToString("0.00");
            this.txtp96.Text = (Convert.ToDecimal(this.model.p96) * 100M).ToString("0.00");
            this.txtp98.Text = (Convert.ToDecimal(this.model.p98) * 100M).ToString("0.00");
            this.txtp99.Text = (Convert.ToDecimal(this.model.p99) * 100M).ToString("0.00");
            this.txtp100.Text = (Convert.ToDecimal(this.model.p100) * 100M).ToString("0.00");
            this.txtp101.Text = (Convert.ToDecimal(this.model.p101) * 100M).ToString("0.00");
            this.txtp102.Text = (Convert.ToDecimal(this.model.p102) * 100M).ToString("0.00");
            this.txtp103.Text = (Convert.ToDecimal(this.model.p103) * 100M).ToString("0.00");
            this.txtp104.Text = (Convert.ToDecimal(this.model.p104) * 100M).ToString("0.00");
            this.txtp105.Text = (Convert.ToDecimal(this.model.p105) * 100M).ToString("0.00");
            this.txtp106.Text = (Convert.ToDecimal(this.model.p106) * 100M).ToString("0.00");
            this.txtp107.Text = (Convert.ToDecimal(this.model.p107) * 100M).ToString("0.00");
            this.txtp108.Text = (Convert.ToDecimal(this.model.p108) * 100M).ToString("0.00");
            this.txtp109.Text = (Convert.ToDecimal(this.model.p109) * 100M).ToString("0.00");
            this.txtp110.Text = (Convert.ToDecimal(this.model.p110) * 100M).ToString("0.00");
            this.txtp111.Text = (Convert.ToDecimal(this.model.p111) * 100M).ToString("0.00");
            this.txtp112.Text = (Convert.ToDecimal(this.model.p112) * 100M).ToString("0.00");
            this.txtp113.Text = (Convert.ToDecimal(this.model.p113) * 100M).ToString("0.00");
            this.txtp115.Text = (Convert.ToDecimal(this.model.p115) * 100M).ToString("0.00");
            this.txtp116.Text = (Convert.ToDecimal(this.model.p116) * 100M).ToString("0.00");
            this.txtp117.Text = (Convert.ToDecimal(this.model.p117) * 100M).ToString("0.00");
            this.txtp118.Text = (Convert.ToDecimal(this.model.p118) * 100M).ToString("0.00");
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

        public viviapi.Model.Payment.PayRate model
        {
            get
            {
                if ((this.ItemInfoId > 0) && (this._model == null))
                {
                    this._model = PayRateFactory.GetModel(this.ItemInfoId);
                }
                return this._model;
            }
        }
    }
}

