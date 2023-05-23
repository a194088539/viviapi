namespace viviapi.WebUI.Managements.User
{
    using System;
    using System.Text;
    using viviapi.BLL;
    using viviapi.BLL.Payment;
    using viviapi.Model;
    using viviapi.Model.Payment;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public partial class agentratelimit : ManagePageBase
    {

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            PayRate modelByUser = PayRateFactory.GetModelByUser(this.ItemInfoId);
            if (modelByUser != null)
            {
                decimal num = Convert.ToDecimal(modelByUser.p100) * 100M;
                this.txtp100.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p101) * 100M;
                this.txtp101.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p102) * 100M;
                this.txtp102.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p103) * 100M;
                this.txtp103.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p104) * 100M;
                this.txtp104.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p105) * 100M;
                this.txtp105.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p106) * 100M;
                this.txtp106.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p107) * 100M;
                this.txtp107.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p108) * 100M;
                this.txtp108.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p109) * 100M;
                this.txtp109.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p110) * 100M;
                this.txtp110.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p111) * 100M;
                this.txtp111.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p112) * 100M;
                this.txtp112.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p113) * 100M;
                this.txtp113.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p114) * 100M;
                this.txtp114.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p115) * 100M;
                this.txtp115.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p116) * 100M;
                this.txtp116.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p117) * 100M;
                this.txtp117.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p118) * 100M;
                this.txtp118.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p119) * 100M;
                this.txtp119.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p200) * 100M;
                this.txtp200.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p201) * 100M;
                this.txtp201.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p202) * 100M;
                this.txtp202.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p203) * 100M;
                this.txtp203.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p204) * 100M;
                this.txtp204.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p205) * 100M;
                this.txtp205.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p208) * 100M;
                this.txtp208.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p209) * 100M;
                this.txtp209.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p99) * 100M;
                this.txtp99.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p98) * 100M;
                this.txtp98.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p97) * 100M;
                this.txtp97.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p96) * 100M;
                this.txtp96.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p95) * 100M;
                this.txtp95.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p94) * 100M;
                this.txtp94.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p93) * 100M;
                this.txtp93.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p92) * 100M;
                this.txtp92.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p91) * 100M;
                this.txtp91.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p89) * 100M;
                this.txtp89.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p88) * 100M;
                this.txtp88.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p87) * 100M;
                this.txtp87.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p86) * 100M;
                this.txtp86.Text = num.ToString("0.00");
                this.txtp90.Text = (Convert.ToDecimal(modelByUser.p90) * 100M).ToString("0.00");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (!viviLib.Text.Validate.IsNumber(this.txtp99.Text))
            {
                msg = msg + "p99格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp98.Text))
            {
                msg = msg + "p98格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp97.Text))
            {
                msg = msg + "p97格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp96.Text))
            {
                msg = msg + "p96格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp95.Text))
            {
                msg = msg + "p95格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp94.Text))
            {
                msg = msg + "p94格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp93.Text))
            {
                msg = msg + "p93格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp92.Text))
            {
                msg = msg + "p92格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp91.Text))
            {
                msg = msg + "p91格式错误！\n";
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
            if (!viviLib.Text.Validate.IsNumber(this.txtp1001.Text))
            {
                msg = msg + "p1001格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1011.Text))
            {
                msg = msg + "p1011格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1021.Text))
            {
                msg = msg + "p1021格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1031.Text))
            {
                msg = msg + "p1031格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1041.Text))
            {
                msg = msg + "p1041格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1051.Text))
            {
                msg = msg + "p1051格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1061.Text))
            {
                msg = msg + "p1061格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1071.Text))
            {
                msg = msg + "p1071格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1081.Text))
            {
                msg = msg + "p1081格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1091.Text))
            {
                msg = msg + "p1091格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1101.Text))
            {
                msg = msg + "p1101格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1111.Text))
            {
                msg = msg + "p1111格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1121.Text))
            {
                msg = msg + "p1121格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1131.Text))
            {
                msg = msg + "p1131格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1141.Text))
            {
                msg = msg + "p1141格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1151.Text))
            {
                msg = msg + "p1151格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1161.Text))
            {
                msg = msg + "p1161格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1171.Text))
            {
                msg = msg + "p11711格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1181.Text))
            {
                msg = msg + "p1181格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp1191.Text))
            {
                msg = msg + "p1191格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2001.Text))
            {
                msg = msg + "p2001格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2011.Text))
            {
                msg = msg + "p2011格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2021.Text))
            {
                msg = msg + "p2021格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2031.Text))
            {
                msg = msg + "p2031格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2041.Text))
            {
                msg = msg + "p2041格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2051.Text))
            {
                msg = msg + "p2051格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2081.Text))
            {
                msg = msg + "p2081格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp2091.Text))
            {
                msg = msg + "p2091格式错误！\n";
            }
            if (!viviLib.Text.Validate.IsNumber(this.txtp901.Text))
            {
                msg = msg + "p901格式错误！\n";
            }
            if (msg != "")
            {
                base.AlertAndRedirect(msg);
            }
            else
            {
                decimal num = decimal.Parse(this.txtp100.Text) / 100M;
                decimal num2 = decimal.Parse(this.txtp101.Text) / 100M;
                decimal num3 = decimal.Parse(this.txtp102.Text) / 100M;
                decimal num4 = decimal.Parse(this.txtp103.Text) / 100M;
                decimal num5 = decimal.Parse(this.txtp104.Text) / 100M;
                decimal num6 = decimal.Parse(this.txtp105.Text) / 100M;
                decimal num7 = decimal.Parse(this.txtp106.Text) / 100M;
                decimal num8 = decimal.Parse(this.txtp107.Text) / 100M;
                decimal num9 = decimal.Parse(this.txtp108.Text) / 100M;
                decimal num10 = decimal.Parse(this.txtp109.Text) / 100M;
                decimal num11 = decimal.Parse(this.txtp110.Text) / 100M;
                decimal num12 = decimal.Parse(this.txtp111.Text) / 100M;
                decimal num13 = decimal.Parse(this.txtp112.Text) / 100M;
                decimal num14 = decimal.Parse(this.txtp113.Text) / 100M;
                decimal num15 = decimal.Parse(this.txtp114.Text) / 100M;
                decimal num16 = decimal.Parse(this.txtp115.Text) / 100M;
                decimal num17 = decimal.Parse(this.txtp116.Text) / 100M;
                decimal num18 = decimal.Parse(this.txtp117.Text) / 100M;
                decimal num19 = decimal.Parse(this.txtp118.Text) / 100M;
                decimal num20 = decimal.Parse(this.txtp119.Text) / 100M;
                decimal num21 = decimal.Parse(this.txtp200.Text) / 100M;
                decimal num22 = decimal.Parse(this.txtp201.Text) / 100M;
                decimal num23 = decimal.Parse(this.txtp202.Text) / 100M;
                decimal num24 = decimal.Parse(this.txtp203.Text) / 100M;
                decimal num25 = decimal.Parse(this.txtp204.Text) / 100M;
                decimal num26 = decimal.Parse(this.txtp205.Text) / 100M;
                decimal num27 = decimal.Parse(this.txtp208.Text) / 100M;
                decimal num28 = decimal.Parse(this.txtp209.Text) / 100M;
                decimal num29 = decimal.Parse(this.txtp90.Text) / 100M;
                decimal num30 = decimal.Parse(this.txtp1001.Text) / 100M;
                decimal num31 = decimal.Parse(this.txtp1011.Text) / 100M;
                decimal num32 = decimal.Parse(this.txtp1021.Text) / 100M;
                decimal num33 = decimal.Parse(this.txtp1031.Text) / 100M;
                decimal num34 = decimal.Parse(this.txtp1041.Text) / 100M;
                decimal num35 = decimal.Parse(this.txtp1051.Text) / 100M;
                decimal num36 = decimal.Parse(this.txtp1061.Text) / 100M;
                decimal num37 = decimal.Parse(this.txtp1071.Text) / 100M;
                decimal num38 = decimal.Parse(this.txtp1081.Text) / 100M;
                decimal num39 = decimal.Parse(this.txtp1091.Text) / 100M;
                decimal num40 = decimal.Parse(this.txtp1101.Text) / 100M;
                decimal num41 = decimal.Parse(this.txtp1111.Text) / 100M;
                decimal num42 = decimal.Parse(this.txtp1121.Text) / 100M;
                decimal num43 = decimal.Parse(this.txtp1131.Text) / 100M;
                decimal num44 = decimal.Parse(this.txtp1141.Text) / 100M;
                decimal num45 = decimal.Parse(this.txtp1151.Text) / 100M;
                decimal num46 = decimal.Parse(this.txtp1161.Text) / 100M;
                decimal num47 = decimal.Parse(this.txtp1171.Text) / 100M;
                decimal num48 = decimal.Parse(this.txtp1181.Text) / 100M;
                decimal num49 = decimal.Parse(this.txtp1191.Text) / 100M;
                decimal num50 = decimal.Parse(this.txtp2001.Text) / 100M;
                decimal num51 = decimal.Parse(this.txtp2011.Text) / 100M;
                decimal num52 = decimal.Parse(this.txtp2021.Text) / 100M;
                decimal num53 = decimal.Parse(this.txtp2031.Text) / 100M;
                decimal num54 = decimal.Parse(this.txtp2041.Text) / 100M;
                decimal num55 = decimal.Parse(this.txtp2051.Text) / 100M;
                decimal num56 = decimal.Parse(this.txtp2081.Text) / 100M;
                decimal num57 = decimal.Parse(this.txtp2091.Text) / 100M;
                decimal num58 = decimal.Parse(this.txtp901.Text) / 100M;
                decimal num59 = decimal.Parse(this.txtp99.Text) / 100M;
                decimal num60 = decimal.Parse(this.txtp98.Text) / 100M;
                decimal num61 = decimal.Parse(this.txtp97.Text) / 100M;
                decimal num62 = decimal.Parse(this.txtp96.Text) / 100M;
                decimal num63 = decimal.Parse(this.txtp95.Text) / 100M;
                decimal num64 = decimal.Parse(this.txtp94.Text) / 100M;
                decimal num65 = decimal.Parse(this.txtp93.Text) / 100M;
                decimal num66 = decimal.Parse(this.txtp92.Text) / 100M;
                decimal num67 = decimal.Parse(this.txtp91.Text) / 100M;
                decimal num68 = decimal.Parse(this.txtp89.Text) / 100M;
                decimal num69 = decimal.Parse(this.txtp88.Text) / 100M;
                decimal num70 = decimal.Parse(this.txtp87.Text) / 100M;
                decimal num71 = decimal.Parse(this.txtp86.Text) / 100M;
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0}:{1}:{2}", 100, num, num30);
                builder.AppendFormat("|{0}:{1}:{2}", 0x65, num2, num31);
                builder.AppendFormat("|{0}:{1}:{2}", 0x66, num3, num32);
                builder.AppendFormat("|{0}:{1}:{2}", 0x67, num4, num33);
                builder.AppendFormat("|{0}:{1}:{2}", 0x68, num5, num34);
                builder.AppendFormat("|{0}:{1}:{2}", 0x69, num6, num35);
                builder.AppendFormat("|{0}:{1}:{2}", 0x6a, num7, num36);
                builder.AppendFormat("|{0}:{1}:{2}", 0x6b, num8, num37);
                builder.AppendFormat("|{0}:{1}:{2}", 0x6c, num9, num38);
                builder.AppendFormat("|{0}:{1}:{2}", 0x6d, num10, num39);
                builder.AppendFormat("|{0}:{1}:{2}", 110, num11, num40);
                builder.AppendFormat("|{0}:{1}:{2}", 0x6f, num12, num41);
                builder.AppendFormat("|{0}:{1}:{2}", 0x70, num13, num42);
                builder.AppendFormat("|{0}:{1}:{2}", 0x71, num14, num43);
                builder.AppendFormat("|{0}:{1}:{2}", 0x72, num15, num44);
                builder.AppendFormat("|{0}:{1}:{2}", 0x73, num16, num45);
                builder.AppendFormat("|{0}:{1}:{2}", 0x74, num17, num46);
                builder.AppendFormat("|{0}:{1}:{2}", 0x75, num18, num47);
                builder.AppendFormat("|{0}:{1}:{2}", 0x76, num19, num48);
                builder.AppendFormat("|{0}:{1}:{2}", 0x77, num20, num49);
                builder.AppendFormat("|{0}:{1}:{2}", 200, num21, num50);
                builder.AppendFormat("|{0}:{1}:{2}", 0xc9, num22, num51);
                builder.AppendFormat("|{0}:{1}:{2}", 0xca, num23, num52);
                builder.AppendFormat("|{0}:{1}:{2}", 0xcb, num24, num53);
                builder.AppendFormat("|{0}:{1}:{2}", 0xcc, num25, num54);
                builder.AppendFormat("|{0}:{1}:{2}", 0xcd, num26, num55);
                builder.AppendFormat("|{0}:{1}:{2}", 0xd0, num27, num56);
                builder.AppendFormat("|{0}:{1}:{2}", 0xd1, num28, num57);
                builder.AppendFormat("|{0}:{1}:{2}", 90, num29, num58);
                builder.AppendFormat("|{0}:{1}:{2}", 99, num30, num59);
                builder.AppendFormat("|{0}:{1}:{2}", 98, num31, num60);
                builder.AppendFormat("|{0}:{1}:{2}", 97, num32, num61);
                builder.AppendFormat("|{0}:{1}:{2}", 96, num33, num62);
                builder.AppendFormat("|{0}:{1}:{2}", 95, num34, num63);
                builder.AppendFormat("|{0}:{1}:{2}", 94, num35, num64);
                builder.AppendFormat("|{0}:{1}:{2}", 93, num36, num65);
                builder.AppendFormat("|{0}:{1}:{2}", 92, num37, num66);
                builder.AppendFormat("|{0}:{1}:{2}", 91, num38, num67);
                builder.AppendFormat("|{0}:{1}:{2}", 89, num39, num68);
                builder.AppendFormat("|{0}:{1}:{2}", 88, num40, num69);
                builder.AppendFormat("|{0}:{1}:{2}", 87, num40, num70);
                builder.AppendFormat("|{0}:{1}:{2}", 86, num40, num71);
                if (WebInfoFactory.SetAgent_Payrate_Setconfig(builder.ToString()))
                {
                    base.AlertAndRedirect("设置成功");
                }
                else
                {
                    base.AlertAndRedirect("设置失败");
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
            string str = WebInfoFactory.GetAgent_Payrate_Setconfig();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { '|' });
                foreach (string str2 in strArray)
                {
                    decimal num2;
                    string[] strArray2 = str2.Split(new char[] { ':' });
                    if (strArray2[0] == "100")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp100.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1001.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "101")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp101.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1011.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "102")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp102.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1021.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "103")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp103.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1031.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "104")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp104.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1041.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "105")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp105.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1051.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "106")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp106.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1061.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "107")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp107.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1071.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "108")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp108.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1081.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "109")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp109.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1091.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "110")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp110.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1101.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "111")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp111.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1111.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "112")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp112.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1121.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "113")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp113.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1131.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "114")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp114.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1141.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "115")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp115.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1151.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "116")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp116.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1161.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "117")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp117.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1171.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "118")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp118.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1181.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "119")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp119.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1191.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "200")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp200.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2001.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "201")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp201.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2011.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "202")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp202.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2021.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "203")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp203.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2031.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "204")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp204.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2041.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "205")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp205.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2051.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "208")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp208.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2081.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "209")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp209.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2091.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "89")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp89.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp891.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "88")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp88.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp881.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "87")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp87.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp871.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "86")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp86.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp861.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "91")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp91.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp911.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "92")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp92.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp921.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "93")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp93.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp931.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "94")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp94.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp941.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "95")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp95.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp951.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "96")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp96.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp961.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "97")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp97.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp971.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "98")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp98.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp981.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "99")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp99.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp991.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "90")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp90.Text = num2.ToString("0.00");
                        this.txtp901.Text = (Convert.ToDecimal(strArray2[2]) * 100M).ToString("0.00");
                    }
                }
            }
        }

        public int ItemInfoId
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }
    }
}

