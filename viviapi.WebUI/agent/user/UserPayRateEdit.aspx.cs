namespace viviapi.web.Agent.User
{
    using System;
    using System.Text;
    using viviapi.BLL;
    using viviapi.BLL.Payment;
    using viviapi.BLL.User;
    using viviapi.Model;
    using viviapi.Model.Payment;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public partial class UserPayRateEdit : AgentPageBase
    {
        public usersettingInfo _model = null;

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            PayRate modelByUser = PayRateFactory.GetModelByUser(this.ItemInfoId);
            if (modelByUser != null)
            {
                //支付宝APP
                decimal num = Convert.ToDecimal(modelByUser.p95) * 100M;
                this.txtp95.Text = num.ToString("0.00");
                //微信APP
                num = Convert.ToDecimal(modelByUser.p94) * 100M;
                this.txtp94.Text = num.ToString("0.00");
                //网银APP
                num = Convert.ToDecimal(modelByUser.p96) * 100M;
                this.txtp96.Text = num.ToString("0.00");

                num = Convert.ToDecimal(modelByUser.p98) * 100M;
                this.txtp98.Text = num.ToString("0.00");
                //微信
                num = Convert.ToDecimal(modelByUser.p99) * 100M;
                this.txtp99.Text = num.ToString("0.00");
                //财付通
                num = Convert.ToDecimal(modelByUser.p100) * 100M;
                this.txtp100.Text = num.ToString("0.00");
                //支付宝
                num = Convert.ToDecimal(modelByUser.p101) * 100M;
                this.txtp101.Text = num.ToString("0.00");
                //网银
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
                num = Convert.ToDecimal(modelByUser.p210) * 100M;
                this.txtp210.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p91) * 100M;
                this.txtp91.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p92) * 100M;
                this.txtp92.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p93) * 100M;
                this.txtp93.Text = num.ToString("0.00");
                num = Convert.ToDecimal(modelByUser.p97) * 100M;
                this.txtp97.Text = num.ToString("0.00");
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
            if (msg != "")
            {
                base.AlertAndRedirect(msg);
            }
            else if (!this.CheckOK())
            {
                base.AlertAndRedirect("费率不在指定的范围内,请重新设置");
            }
            else
            {
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
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0}:{1}", 95, num);
                builder.AppendFormat("|{0}:{1}", 94, num2);
                builder.AppendFormat("|{0}:{1}", 96, num3);
                builder.AppendFormat("|{0}:{1}", 0x62, num4);
                builder.AppendFormat("|{0}:{1}", 0x63, num5);
                builder.AppendFormat("|{0}:{1}", 100, num6);
                builder.AppendFormat("|{0}:{1}", 0x65, num7);
                builder.AppendFormat("|{0}:{1}", 0x66, num8);
                builder.AppendFormat("|{0}:{1}", 0x67, num9);
                builder.AppendFormat("|{0}:{1}", 0x68, num10);
                builder.AppendFormat("|{0}:{1}", 0x69, num11);
                builder.AppendFormat("|{0}:{1}", 0x6a, num12);
                builder.AppendFormat("|{0}:{1}", 0x6b, num13);
                builder.AppendFormat("|{0}:{1}", 0x6c, num14);
                builder.AppendFormat("|{0}:{1}", 0x6d, num15);
                builder.AppendFormat("|{0}:{1}", 110, num16);
                builder.AppendFormat("|{0}:{1}", 0x6f, num17);
                builder.AppendFormat("|{0}:{1}", 0x70, num18);
                builder.AppendFormat("|{0}:{1}", 0x71, num19);
                builder.AppendFormat("|{0}:{1}", 0x72, num20);
                builder.AppendFormat("|{0}:{1}", 0x73, num21);
                builder.AppendFormat("|{0}:{1}", 0x74, num22);
                builder.AppendFormat("|{0}:{1}", 0x75, num23);
                builder.AppendFormat("|{0}:{1}", 0x76, num24);
                builder.AppendFormat("|{0}:{1}", 0x77, num25);
                builder.AppendFormat("|{0}:{1}", 200, num26);
                builder.AppendFormat("|{0}:{1}", 0xc9, num27);
                builder.AppendFormat("|{0}:{1}", 0xca, num28);
                builder.AppendFormat("|{0}:{1}", 0xcb, num29);
                builder.AppendFormat("|{0}:{1}", 0xcc, num30);
                builder.AppendFormat("|{0}:{1}", 0xcd, num31);
                builder.AppendFormat("|{0}:{1}", 0xd0, num32);
                builder.AppendFormat("|{0}:{1}", 0xd1, num33);
                builder.AppendFormat("|{0}:{1}", 210, num34);
                builder.AppendFormat("|{0}:{1}", 90, num35);
                builder.AppendFormat("|{0}:{1}", 91, num36);
                builder.AppendFormat("|{0}:{1}", 92, num37);
                builder.AppendFormat("|{0}:{1}", 93, num38);
                builder.AppendFormat("|{0}:{1}", 97, num39);
                builder.AppendFormat("|{0}:{1}", 89, num40);
                builder.AppendFormat("|{0}:{1}", 88, num41);
                builder.AppendFormat("|{0}:{1}", 87, num42);
                builder.AppendFormat("|{0}:{1}", 86, num43);

                usersetting usersetting = new usersetting();
                this.model.payrate = builder.ToString();
                this.model.special = this.ckb_isopen.Checked ? 1 : 0;

                if (usersetting.Insert(this.model))
                {
                    base.AlertAndRedirect("设置成功");
                }
                else
                {
                    base.AlertAndRedirect("设置失败");
                }
            }
        }

        private bool CheckOK()
        {
            //95:0.9:0.98|96:0.9:0.98|94:0.9:0.98|99:0.9:0.98|100:0.9:0.98|101:0.9:0.98|102:0.8:0.98|103:0.7:0.8|104:0.7:0.8|105:0.7:0.8|106:0.7:0.8|107:0.7:0.8|108:0.7:0.8|109:0.7:0.8|110:0.7:0.8|111:0.7:0.8|112:0.7:0.8|113:0.7:0.8|114:0.7:0.8|115:0.7:0.8|116:0.7:0.8|117:0.7:0.8|118:0.7:0.8|119:0.7:0.8|200:0.7:0.8|201:0.7:0.8|202:0.7:0.8|203:0.7:0.8|204:0.7:0.8|205:0.7:0.8|208:0.7:0.8|209:0.7:0.8|90:0.7:0.8|91:0.7:0.8|92:0.7:0.8|93:0.7:0.8|97:0.7:0.8|89:0.7:0.8|88:0.7:0.8|87:0.7:0.8|86:0.7:0.8
            string str = WebInfoFactory.GetAgent_Payrate_Setconfig();
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            decimal num = 0M;
            decimal num2 = 0M;
            decimal num3 = 0M;
            string[] strArray = str.Split(new char[] { '|' });
            foreach (string str2 in strArray)
            {
                string[] strArray2 = str2.Split(new char[] { ':' });
                if (strArray2[0] == "95")
                {
                    num3 = Convert.ToDecimal(this.txtp95.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                if (strArray2[0] == "94")
                {
                    num3 = Convert.ToDecimal(this.txtp94.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                if (strArray2[0] == "96")
                {
                    num3 = Convert.ToDecimal(this.txtp96.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                if (strArray2[0] == "99")
                {
                    num3 = Convert.ToDecimal(this.txtp99.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }

                else if (strArray2[0] == "100")
                {
                    num3 = Convert.ToDecimal(this.txtp100.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "101")
                {
                    num3 = Convert.ToDecimal(this.txtp101.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "102")
                {
                    num3 = Convert.ToDecimal(this.txtp102.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "103")
                {
                    num3 = Convert.ToDecimal(this.txtp103.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "104")
                {
                    num3 = Convert.ToDecimal(this.txtp104.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "105")
                {
                    num3 = Convert.ToDecimal(this.txtp105.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "106")
                {
                    num3 = Convert.ToDecimal(this.txtp106.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "107")
                {
                    num3 = Convert.ToDecimal(this.txtp107.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "108")
                {
                    num3 = Convert.ToDecimal(this.txtp108.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "109")
                {
                    num3 = Convert.ToDecimal(this.txtp109.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "110")
                {
                    num3 = Convert.ToDecimal(this.txtp110.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "111")
                {
                    num3 = Convert.ToDecimal(this.txtp111.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "112")
                {
                    num3 = Convert.ToDecimal(this.txtp112.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "113")
                {
                    num3 = Convert.ToDecimal(this.txtp113.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "114")
                {
                    num3 = Convert.ToDecimal(this.txtp114.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "115")
                {
                    num3 = Convert.ToDecimal(this.txtp115.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "116")
                {
                    num3 = Convert.ToDecimal(this.txtp116.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "117")
                {
                    num3 = Convert.ToDecimal(this.txtp117.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "118")
                {
                    num3 = Convert.ToDecimal(this.txtp118.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "119")
                {
                    num3 = Convert.ToDecimal(this.txtp119.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "200")
                {
                    num3 = Convert.ToDecimal(this.txtp200.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "201")
                {
                    num3 = Convert.ToDecimal(this.txtp201.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "202")
                {
                    num3 = Convert.ToDecimal(this.txtp202.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "203")
                {
                    num3 = Convert.ToDecimal(this.txtp203.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "204")
                {
                    num3 = Convert.ToDecimal(this.txtp204.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "205")
                {
                    num3 = Convert.ToDecimal(this.txtp205.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "208")
                {
                    num3 = Convert.ToDecimal(this.txtp208.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "209")
                {
                    num3 = Convert.ToDecimal(this.txtp209.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "89")
                {
                    num3 = Convert.ToDecimal(this.txtp89.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "88")
                {
                    num3 = Convert.ToDecimal(this.txtp88.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "87")
                {
                    num3 = Convert.ToDecimal(this.txtp87.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "86")
                {
                    num3 = Convert.ToDecimal(this.txtp86.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "90")
                {
                    num3 = Convert.ToDecimal(this.txtp90.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }

                else if (strArray2[0] == "91")
                {
                    num3 = Convert.ToDecimal(this.txtp91.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "92")
                {
                    num3 = Convert.ToDecimal(this.txtp92.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "93")
                {
                    num3 = Convert.ToDecimal(this.txtp93.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
                else if (strArray2[0] == "97")
                {
                    num3 = Convert.ToDecimal(this.txtp97.Text.Trim());
                    num = Convert.ToDecimal(strArray2[1]) * 100M;
                    num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                    if ((num3 < num) || (num3 > num2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (string.IsNullOrEmpty(WebInfoFactory.GetAgent_Payrate_Setconfig()))
                {
                    base.Response.Write("未配置费率范围，不能操作");
                    base.Response.End();
                }
                this.ShowConfig();
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

        private void ShowConfig()
        {
            string str = WebInfoFactory.GetAgent_Payrate_Setconfig();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { '|' });
                foreach (string str2 in strArray)
                {
                    decimal num2;
                    string[] strArray2 = str2.Split(new char[] { ':' });
                    if (strArray2[0] == "89")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp890.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp891.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "88")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp880.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp881.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "87")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp870.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp871.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "86")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp860.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp861.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "91")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp910.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp911.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "92")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp920.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp921.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "93")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp930.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp931.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "97")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp970.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp971.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "95")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp950.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp951.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "94")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp940.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp941.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "96")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp960.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp961.Text = num2.ToString("0.00");
                    }
                    if (strArray2[0] == "99")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp0990.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp0991.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "100")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1000.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1001.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "101")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1010.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1011.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "102")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp01020.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp01021.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "103")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1030.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1031.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "104")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1040.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1041.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "105")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1050.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1051.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "106")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1060.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1061.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "107")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1070.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1071.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "108")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1080.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1081.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "109")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1090.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1091.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "110")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1100.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1101.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "111")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1110.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1111.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "112")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1120.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1121.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "113")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1130.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1131.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "114")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1140.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1141.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "115")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1150.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1151.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "116")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1160.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1161.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "117")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1170.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1171.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "118")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1180.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1181.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "119")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp1190.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp1191.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "200")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2000.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2001.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "201")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2010.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2011.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "202")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2020.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2021.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "203")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2030.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2031.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "204")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2040.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2041.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "205")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2050.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2051.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "208")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2080.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2081.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "209")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp2090.Text = num2.ToString("0.00");
                        num2 = Convert.ToDecimal(strArray2[2]) * 100M;
                        this.txtp2091.Text = num2.ToString("0.00");
                    }
                    else if (strArray2[0] == "90")
                    {
                        num2 = Convert.ToDecimal(strArray2[1]) * 100M;
                        this.txtp900.Text = num2.ToString("0.00");
                        this.txtp901.Text = (Convert.ToDecimal(strArray2[2]) * 100M).ToString("0.00");
                    }
                }
            }
        }

        private void ShowInfo()
        {
            this.lblUserId.Text = this.ItemInfoId.ToString();
            if (this.model != null)
            {
                this.ckb_isopen.Checked = this.model.special > 0;
                string payrate = this.model.payrate;
                if (!string.IsNullOrEmpty(payrate))
                {
                    string[] strArray = payrate.Split(new char[] { '|' });
                    foreach (string str2 in strArray)
                    {
                        decimal num3;
                        string[] strArray2 = str2.Split(new char[] { ':' });
                        if (strArray2[0] == "95")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp95.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "94")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp94.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "96")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp96.Text = num3.ToString("0.00");
                        }
                        if (strArray2[0] == "98")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp98.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "99")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp99.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "100")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp100.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "101")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp101.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "102")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp102.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "103")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp103.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "104")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp104.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "105")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp105.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "106")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp106.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "107")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp107.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "108")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp108.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "109")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp109.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "110")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp110.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "111")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp111.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "112")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp112.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "113")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp113.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "114")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp114.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "115")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp115.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "116")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp116.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "117")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp117.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "118")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp118.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "119")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp119.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "200")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp200.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "201")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp201.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "202")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp202.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "203")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp203.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "204")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp204.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "205")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp205.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "208")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp208.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "209")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp209.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "210")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp210.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "89")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp89.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "88")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp88.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "87")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp87.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "86")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp86.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "91")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp91.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "92")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp92.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "93")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp93.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "97")
                        {
                            num3 = Convert.ToDecimal(strArray2[1]) * 100M;
                            this.txtp97.Text = num3.ToString("0.00");
                        }
                        else if (strArray2[0] == "90")
                        {
                            this.txtp90.Text = (Convert.ToDecimal(strArray2[1]) * 100M).ToString("0.00");
                        }
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

        public usersettingInfo model
        {
            get
            {
                if (this._model == null)
                {
                    this._model = new usersetting().GetModel(this.ItemInfoId);
                }
                return this._model;
            }
        }
    }
}

