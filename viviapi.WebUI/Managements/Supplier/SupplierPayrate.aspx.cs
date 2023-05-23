namespace viviapi.WebUI.Managements
{
    using System;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Text;
    using viviLib.Web;

    public partial class SupplierPayrate : ManagePageBase
    {
        public SupplierPayRateInfo _ItemInfo = null;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if ((this.SupplierId == 98) && !PageValidate.IsDecimal(this.txtp98.Text))
            {
                msg = msg + "支付宝扫码费率格式错误！\n";
            }
            if ((this.SupplierId == 99) && !PageValidate.IsDecimal(this.txtp99.Text))
            {
                msg = msg + "微信扫码费率格式错误！\n";
            }
            if (this.SupplierId == 100)
            {
                if (!PageValidate.IsDecimal(this.txtp100.Text))
                {
                    msg = msg + "财付通费率格式错误！\n";
                }
            }
            else if ((this.SupplierId == 101) && !PageValidate.IsDecimal(this.txtp101.Text))
            {
                msg = msg + "支付宝费率格式错误！\n";
            }
            if ((this.SupplierId == 95) && !PageValidate.IsDecimal(this.txtp95.Text))
            {
                msg = msg + "支付宝APP费率格式错误！\n";
            }
            if ((this.SupplierId == 94) && !PageValidate.IsDecimal(this.txtp94.Text))
            {
                msg = msg + "微信APP费率格式错误！\n";
            }
            if ((this.SupplierId == 96) && !PageValidate.IsDecimal(this.txtp96.Text))
            {
                msg = msg + "网银APP费率格式错误！\n";
            }
            if ((this.SupplierId == 91) && !PageValidate.IsDecimal(this.txtp91.Text))
            {
                msg = msg + "银联钱包费率格式错误！\n";
            }
            if ((this.SupplierId == 92) && !PageValidate.IsDecimal(this.txtp92.Text))
            {
                msg = msg + "京东APP费率格式错误！\n";
            }
            if ((this.SupplierId == 93) && !PageValidate.IsDecimal(this.txtp93.Text))
            {
                msg = msg + "QQAPP费率格式错误！\n";
            }
            if ((this.SupplierId == 97) && !PageValidate.IsDecimal(this.txtp97.Text))
            {
                msg = msg + "京东费率格式错误！\n";
            }
            if ((this.SupplierId == 89) && !PageValidate.IsDecimal(this.txtp89.Text))
            {
                msg = msg + "微信H5费率格式错误！\n";
            }
            if ((this.SupplierId == 88) && !PageValidate.IsDecimal(this.txtp88.Text))
            {
                msg = msg + "百度钱包费率格式错误！\n";
            }
            if ((this.SupplierId == 87) && !PageValidate.IsDecimal(this.txtp87.Text))
            {
                msg = msg + "支付宝H5费率格式错误！\n";
            }
            if ((this.SupplierId == 86) && !PageValidate.IsDecimal(this.txtp86.Text))
            {
                msg = msg + "QQ钱包H5费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp102.Text))
            {
                msg = msg + "网上银行费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp103.Text))
            {
                msg = msg + "神州行充值卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp200.Text))
            {
                msg = msg + "神州行浙江卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp201.Text))
            {
                msg = msg + "神州行江苏卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp202.Text))
            {
                msg = msg + "神州行辽宁卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp203.Text))
            {
                msg = msg + "神州行福建卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp104.Text))
            {
                msg = msg + "盛大一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp105.Text))
            {
                msg = msg + "征途支付卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp106.Text))
            {
                msg = msg + "骏网一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp107.Text))
            {
                msg = msg + "腾讯Q币卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp108.Text))
            {
                msg = msg + "联通充值卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp109.Text))
            {
                msg = msg + "久游一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp110.Text))
            {
                msg = msg + "网易一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp111.Text))
            {
                msg = msg + "完美一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp112.Text))
            {
                msg = msg + "搜狐一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp113.Text))
            {
                msg = msg + "电信充值卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp114.Text))
            {
                msg = msg + "声讯卡费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp115.Text))
            {
                msg = msg + "光宇一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp116.Text))
            {
                msg = msg + "金山一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp117.Text))
            {
                msg = msg + "纵游一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp118.Text))
            {
                msg = msg + "天下一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp119.Text))
            {
                msg = msg + "天宏一卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp204.Text))
            {
                msg = msg + "魔兽卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp205.Text))
            {
                msg = msg + "联华卡通费率格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp208.Text))
            {
                msg = msg + "p208格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp209.Text))
            {
                msg = msg + "p209格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp210.Text))
            {
                msg = msg + "p210格式错误！\n";
            }
            if (!PageValidate.IsDecimal(this.txtp90.Text))
            {
                msg = msg + "短信费率格式错误！\n";
            }
            if (msg != "")
            {
                base.AlertAndRedirect(this, msg);
            }
            else
            {
                int num = int.Parse(this.hfsupplierid.Value);
                decimal num2 = decimal.Parse(this.txtp95.Text) / 100M;
                decimal num3 = decimal.Parse(this.txtp94.Text) / 100M;
                decimal num4 = decimal.Parse(this.txtp96.Text) / 100M;
                decimal num5 = decimal.Parse(this.txtp98.Text) / 100M;
                decimal num6 = decimal.Parse(this.txtp99.Text) / 100M;
                decimal num7 = decimal.Parse(this.txtp100.Text) / 100M;
                decimal num8 = decimal.Parse(this.txtp101.Text) / 100M;
                decimal num9 = decimal.Parse(this.txtp102.Text) / 100M;
                decimal num10 = decimal.Parse(this.txtp103.Text) / 100M;
                decimal num11 = decimal.Parse(this.txtp104.Text) / 100M;
                decimal num12 = decimal.Parse(this.txtp105.Text) / 100M;
                decimal num13 = decimal.Parse(this.txtp106.Text) / 100M;
                decimal num14 = decimal.Parse(this.txtp107.Text) / 100M;
                decimal num15 = decimal.Parse(this.txtp108.Text) / 100M;
                decimal num16 = decimal.Parse(this.txtp109.Text) / 100M;
                decimal num17 = decimal.Parse(this.txtp110.Text) / 100M;
                decimal num18 = decimal.Parse(this.txtp111.Text) / 100M;
                decimal num19 = decimal.Parse(this.txtp112.Text) / 100M;
                decimal num20 = decimal.Parse(this.txtp113.Text) / 100M;
                decimal num21 = decimal.Parse(this.txtp114.Text) / 100M;
                decimal num22 = decimal.Parse(this.txtp115.Text) / 100M;
                decimal num23 = decimal.Parse(this.txtp116.Text) / 100M;
                decimal num24 = decimal.Parse(this.txtp117.Text) / 100M;
                decimal num25 = decimal.Parse(this.txtp118.Text) / 100M;
                decimal num26 = decimal.Parse(this.txtp119.Text) / 100M;
                decimal num27 = decimal.Parse(this.txtp90.Text) / 100M;
                decimal num28 = decimal.Parse(this.txtp200.Text) / 100M;
                decimal num29 = decimal.Parse(this.txtp201.Text) / 100M;
                decimal num30 = decimal.Parse(this.txtp202.Text) / 100M;
                decimal num31 = decimal.Parse(this.txtp203.Text) / 100M;
                decimal num32 = decimal.Parse(this.txtp204.Text) / 100M;
                decimal num33 = decimal.Parse(this.txtp205.Text) / 100M;
                decimal num34 = decimal.Parse(this.txtp208.Text) / 100M;
                decimal num35 = decimal.Parse(this.txtp209.Text) / 100M;
                decimal num36 = decimal.Parse(this.txtp210.Text) / 100M;
                decimal num37 = decimal.Parse(this.txtp91.Text) / 100M;
                decimal num38 = decimal.Parse(this.txtp92.Text) / 100M;
                decimal num39 = decimal.Parse(this.txtp93.Text) / 100M;
                decimal num40 = decimal.Parse(this.txtp97.Text) / 100M;
                decimal num41 = decimal.Parse(this.txtp89.Text) / 100M;
                decimal num42 = decimal.Parse(this.txtp88.Text) / 100M;
                decimal num43 = decimal.Parse(this.txtp87.Text) / 100M;
                decimal num44 = decimal.Parse(this.txtp86.Text) / 100M;
                this.ItemInfo.supplierid = this.SupplierId;
                this.ItemInfo.p95 = num2;
                this.ItemInfo.p94 = num3;
                this.ItemInfo.p96 = num4;
                this.ItemInfo.p98 = num5;
                this.ItemInfo.p99 = num6;
                this.ItemInfo.p100 = num7;
                this.ItemInfo.p101 = num8;
                this.ItemInfo.p102 = num9;
                this.ItemInfo.p103 = num10;
                this.ItemInfo.p104 = num11;
                this.ItemInfo.p105 = num12;
                this.ItemInfo.p106 = num13;
                this.ItemInfo.p107 = num14;
                this.ItemInfo.p108 = num15;
                this.ItemInfo.p109 = num16;
                this.ItemInfo.p110 = num17;
                this.ItemInfo.p111 = num18;
                this.ItemInfo.p112 = num19;
                this.ItemInfo.p113 = num20;
                this.ItemInfo.p114 = num21;
                this.ItemInfo.p115 = num22;
                this.ItemInfo.p116 = num23;
                this.ItemInfo.p117 = num24;
                this.ItemInfo.p118 = num25;
                this.ItemInfo.p119 = num26;
                this.ItemInfo.p90 = num27;
                this.ItemInfo.p200 = num28;
                this.ItemInfo.p201 = num29;
                this.ItemInfo.p202 = num30;
                this.ItemInfo.p203 = num31;
                this.ItemInfo.p204 = num32;
                this.ItemInfo.p205 = num33;
                this.ItemInfo.p208 = num34;
                this.ItemInfo.p209 = num35;
                this.ItemInfo.p210 = num36;
                this.ItemInfo.p91 = num37;
                this.ItemInfo.p92 = num38;
                this.ItemInfo.p93 = num39;
                this.ItemInfo.p97 = num40;
                this.ItemInfo.p89 = num41;
                this.ItemInfo.p88 = num42;
                this.ItemInfo.p87 = num43;
                this.ItemInfo.p86 = num44;
                if (SupplierPayRateFactory.Add(this.ItemInfo) > 0)
                {
                    base.AlertAndRedirect("保存成功！", "SupplierList.aspx");
                }
                else
                {
                    base.AlertAndRedirect("保存失败！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ShowInfo();
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
            this.hfsupplierid.Value = this.SupplierId.ToString();
            this.lblName.Text = this.SupplierName;
            if (this.ItemInfo != null)
            {
                this.txtp95.Text = this.ItemInfo.p95.ToString("p4").Replace("%", "");
                this.txtp94.Text = this.ItemInfo.p94.ToString("p4").Replace("%", "");
                this.txtp96.Text = this.ItemInfo.p96.ToString("p4").Replace("%", "");
                this.txtp98.Text = this.ItemInfo.p98.ToString("p4").Replace("%", "");
                this.txtp99.Text = this.ItemInfo.p99.ToString("p4").Replace("%", "");
                this.txtp100.Text = this.ItemInfo.p100.ToString("p4").Replace("%", "");
                this.txtp101.Text = this.ItemInfo.p101.ToString("p4").Replace("%", "");
                this.txtp102.Text = this.ItemInfo.p102.ToString("p4").Replace("%", "");
                this.txtp103.Text = this.ItemInfo.p103.ToString("p4").Replace("%", "");
                this.txtp104.Text = this.ItemInfo.p104.ToString("p4").Replace("%", "");
                this.txtp105.Text = this.ItemInfo.p105.ToString("p4").Replace("%", "");
                this.txtp106.Text = this.ItemInfo.p106.ToString("p4").Replace("%", "");
                this.txtp107.Text = this.ItemInfo.p107.ToString("p4").Replace("%", "");
                this.txtp108.Text = this.ItemInfo.p108.ToString("p4").Replace("%", "");
                this.txtp109.Text = this.ItemInfo.p109.ToString("p4").Replace("%", "");
                this.txtp110.Text = this.ItemInfo.p110.ToString("p4").Replace("%", "");
                this.txtp111.Text = this.ItemInfo.p111.ToString("p4").Replace("%", "");
                this.txtp112.Text = this.ItemInfo.p112.ToString("p4").Replace("%", "");
                this.txtp113.Text = this.ItemInfo.p113.ToString("p4").Replace("%", "");
                this.txtp114.Text = this.ItemInfo.p114.ToString("p4").Replace("%", "");
                this.txtp115.Text = this.ItemInfo.p115.ToString("p4").Replace("%", "");
                this.txtp116.Text = this.ItemInfo.p116.ToString("p4").Replace("%", "");
                this.txtp117.Text = this.ItemInfo.p117.ToString("p4").Replace("%", "");
                this.txtp118.Text = this.ItemInfo.p118.ToString("p4").Replace("%", "");
                this.txtp119.Text = this.ItemInfo.p119.ToString("p4").Replace("%", "");
                this.txtp90.Text = this.ItemInfo.p90.ToString("p4").Replace("%", "");
                this.txtp200.Text = this.ItemInfo.p200.ToString("p4").Replace("%", "");
                this.txtp201.Text = this.ItemInfo.p201.ToString("p4").Replace("%", "");
                this.txtp202.Text = this.ItemInfo.p202.ToString("p4").Replace("%", "");
                this.txtp203.Text = this.ItemInfo.p203.ToString("p4").Replace("%", "");
                this.txtp204.Text = this.ItemInfo.p204.ToString("p4").Replace("%", "");
                this.txtp205.Text = this.ItemInfo.p205.ToString("p4").Replace("%", "");
                this.txtp208.Text = this.ItemInfo.p208.ToString("p4").Replace("%", "");
                this.txtp209.Text = this.ItemInfo.p209.ToString("p4").Replace("%", "");
                this.txtp210.Text = this.ItemInfo.p210.ToString("p4").Replace("%", "");
                this.txtp91.Text = this.ItemInfo.p91.ToString("p4").Replace("%", "");
                this.txtp92.Text = this.ItemInfo.p92.ToString("p4").Replace("%", "");
                this.txtp93.Text = this.ItemInfo.p93.ToString("p4").Replace("%", "");
                this.txtp97.Text = this.ItemInfo.p97.ToString("p4").Replace("%", "");
                this.txtp89.Text = this.ItemInfo.p89.ToString("p4").Replace("%", "");
                this.txtp88.Text = this.ItemInfo.p88.ToString("p4").Replace("%", "");
                this.txtp87.Text = this.ItemInfo.p87.ToString("p4").Replace("%", "");
                this.txtp86.Text = this.ItemInfo.p86.ToString("p4").Replace("%", "");
            }
        }

        public SupplierPayRateInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.SupplierId > 0)
                    {
                        this._ItemInfo = SupplierPayRateFactory.GetModel(this.SupplierId);
                    }
                    if (this._ItemInfo == null)
                    {
                        this._ItemInfo = new SupplierPayRateInfo();
                    }
                }
                return this._ItemInfo;
            }
        }

        public int SupplierId
        {
            get
            {
                return WebBase.GetQueryStringInt32("supid", 0);
            }
        }

        public string SupplierName
        {
            get
            {
                return WebBase.GetQueryStringString("n", string.Empty);
            }
        }
    }
}

