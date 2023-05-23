namespace viviapi.WebUI.Managements.User
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Settled;
    using viviapi.BLL.User;
    using viviapi.BLL.Withdraw;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.Model.Settled;
    using viviapi.Model.User;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class BankForUser : ManagePageBase
    {
        protected Button btnAllSettle;
        protected Button btnBatchSettle;
        protected Button btnSearch;
        private channelwithdraw chnlBLL = new channelwithdraw();
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptUsers;
        protected HtmlInputHidden selectedUsers;
        protected TextBox txtbalance;
        protected TextBox txtPassWord;
        protected TextBox txtuserId;
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

        protected void btnAllSettle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPassWord.Text))
            {
                base.AlertAndRedirect("请输入二级密码");
            }
            else if (!ManageFactory.SecPwdVaild(this.txtPassWord.Text.Trim()))
            {
                base.AlertAndRedirect("二级密码不正确");
            }
            else
            {
                decimal result = 0M;
                if (!string.IsNullOrEmpty(this.txtbalance.Text))
                {
                    decimal.TryParse(this.txtbalance.Text, out result);
                }
                if (SettledFactory.AutoSettled(result))
                {
                    base.AlertAndRedirect("结算成功", "SettledAudit.aspx");
                }
                else
                {
                    base.AlertAndRedirect("结算失败");
                }
            }
        }

        protected void btnBatchSettle_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            decimal num3 = 0M;
            foreach (RepeaterItem item in this.rptUsers.Items)
            {
                if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
                {
                    HtmlInputCheckBox box = item.FindControl("chkItem") as HtmlInputCheckBox;
                    if (!box.Checked)
                    {
                        continue;
                    }
                    HiddenField field = item.FindControl("hfuserid") as HiddenField;
                    num++;
                    int userId = 0;
                    decimal settleAmt = 0M;
                    TextBox box2 = item.FindControl("txtpayAmt") as TextBox;
                    try
                    {
                        userId = Convert.ToInt32(field.Value);
                        settleAmt = Convert.ToDecimal(box2.Text.Trim());
                    }
                    catch
                    {
                    }
                    if ((userId <= 0) || (settleAmt <= 0M))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(this.Settle(userId, settleAmt)))
                    {
                        num2++;
                        num3 += settleAmt;
                    }
                }
            }
            base.AlertAndRedirect(string.Format("总处理提现总条数{0} 其中成功条数{1} 成功金额{2:0.00}", num, num2, num3), "BankForUser.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected string GetParm(object userid, object balance, object Freeze, object unpayment)
        {
            try
            {
                decimal num;
                decimal num2;
                decimal num3;
                if (balance == DBNull.Value)
                {
                    num = 0M;
                }
                else
                {
                    num = Convert.ToDecimal(balance);
                }
                if (Freeze == DBNull.Value)
                {
                    num2 = 0M;
                }
                else
                {
                    num2 = Convert.ToDecimal(Freeze);
                }
                if (unpayment == DBNull.Value)
                {
                    num3 = 0M;
                }
                else
                {
                    num3 = Convert.ToDecimal(unpayment);
                }
                return string.Format("{0}${1}${2}${3}", new object[] { userid, num, num2, num3 });
            }
            catch
            {
                return string.Format("{0}${1}${2}${3}", new object[] { "0.00", "0.00", "0.00", "0.00" });
            }
        }

        private void LoadData()
        {
            decimal result = 0M;
            if (!string.IsNullOrEmpty(this.txtbalance.Text))
            {
                decimal.TryParse(this.txtbalance.Text, out result);
            }
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("enableAmt", ">", result));
            if (!base.isSuperAdmin)
            {
                searchParams.Add(new SearchParam("manageId", base.ManageId));
            }
            if (this.proid > 0)
            {
                searchParams.Add(new SearchParam("proid", this.proid));
            }
            string str = this.txtuserId.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                int num2 = 0;
                int.TryParse(str, out num2);
                searchParams.Add(new SearchParam("id", num2));
            }
            string orderby = this.orderBy + " " + this.orderByType;
            DataSet set = UserFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptUsers.DataSource = set.Tables[1];
            this.rptUsers.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.txtbalance.Attributes["onkeypress"] = "if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;";
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void Pager1_PageChanging(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Settled")
            {
                int userId = 0;
                decimal settleAmt = 0M;
                TextBox box = e.Item.FindControl("txtpayAmt") as TextBox;
                try
                {
                    userId = Convert.ToInt32(e.CommandArgument);
                    settleAmt = Convert.ToDecimal(box.Text.Trim());
                }
                catch
                {
                }
                if ((userId <= 0) || (settleAmt <= 0M))
                {
                    base.AlertAndRedirect("参数不正确!");
                }
                else
                {
                    string str = this.Settle(userId, settleAmt);
                    if (!string.IsNullOrEmpty(str))
                    {
                        base.AlertAndRedirect(str);
                    }
                    else
                    {
                        base.AlertAndRedirect("提现成功", "BankForUser.aspx");
                    }
                }
            }
        }

        protected void rptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                TextBox box = (TextBox)e.Item.FindControl("txtpayAmt");
                Literal literal = (Literal)e.Item.FindControl("litTodayIncome");
                box.Attributes["onkeypress"] = "if (event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;";
                box.Text = "0";
                int userId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "id"));
                TocashSchemeInfo modelByUser = TocashScheme.GetModelByUser(1, userId);
                if (modelByUser != null)
                {
                    decimal d = 0M;
                    decimal num3 = 0M;
                    decimal num4 = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "balance"));
                    decimal num5 = 0M;
                    if (DataBinder.Eval(e.Item.DataItem, "unpayment") != DBNull.Value)
                    {
                        num5 = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "unpayment"));
                    }
                    decimal num6 = 0M;
                    if (DataBinder.Eval(e.Item.DataItem, "Freeze") != DBNull.Value)
                    {
                        num6 = Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Freeze"));
                    }
                    if (modelByUser.bankdetentiondays > 0)
                    {
                        num3 += Trade.GetNdaysIncome(1, userId, modelByUser.bankdetentiondays);
                    }
                    if (modelByUser.carddetentiondays > 0)
                    {
                        num3 += Trade.GetNdaysIncome(2, userId, modelByUser.carddetentiondays);
                    }
                    if (modelByUser.otherdetentiondays > 0)
                    {
                        num3 += Trade.GetNdaysIncome(3, userId, modelByUser.otherdetentiondays);
                    }
                    literal.Text = decimal.Round(num3, 2).ToString();
                    d = ((num4 - num5) - num6) - num3;
                    if (d < 0M)
                    {
                        d = 0M;
                    }
                    box.Text = decimal.Round(d, 2).ToString();
                }
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

        private string Settle(int userId, decimal settleAmt)
        {
            string str = "";
            UserInfo model = UserFactory.GetModel(userId);
            if (model == null)
            {
                return "用户不存在";
            }
            if (settleAmt <= 0M)
            {
                return "请输入正确的金额";
            }
            if (settleAmt > model.enableAmt)
            {
                return "结算金额大于余额 操作有误";
            }
            SettledInfo info2 = new SettledInfo();
            info2.addtime = DateTime.Now;
            info2.amount = settleAmt;
            info2.paytime = DateTime.Now;
            info2.status = SettledStatus.审核中;
            info2.tax = 0;
            info2.userid = userId;
            info2.AppType = AppTypeEnum.t0;
            info2.settmode = SettledmodeEnum.系统自动结算;
            info2.PayeeBank = model.BankCode;
            if (model.PMode == 2)
            {
                info2.PayeeBank = "0002";
            }
            else if (model.PMode == 3)
            {
                info2.PayeeBank = "0003";
            }
            info2.Payeeaddress = model.BankAddress;
            info2.payeeName = model.PayeeName;
            info2.Account = model.Account;
            TocashSchemeInfo modelByUser = TocashScheme.GetModelByUser(1, userId);
            info2.charges = new decimal?(modelByUser.chargerate * settleAmt);
            decimal? charges = info2.charges;
            decimal chargeleastofeach = modelByUser.chargeleastofeach;
            if ((charges.GetValueOrDefault() < chargeleastofeach) && charges.HasValue)
            {
                info2.charges = new decimal?(modelByUser.chargeleastofeach);
            }
            else
            {
                charges = info2.charges;
                chargeleastofeach = modelByUser.chargemostofeach;
                if ((charges.GetValueOrDefault() > chargeleastofeach) && charges.HasValue)
                {
                    info2.charges = new decimal?(modelByUser.chargemostofeach);
                }
            }
            if (DateTime.Now.Hour > 0x10)
            {
                info2.required = DateTime.Now.AddDays(1.0);
            }
            else
            {
                info2.required = DateTime.Now;
            }
            if (modelByUser.vaiInterface > 0)
            {
                info2.suppid = this.chnlBLL.GetSupplier(info2.PayeeBank);
            }
            if ((modelByUser.vaiInterface > 0) && (modelByUser.tranRequiredAudit == 0))
            {
                info2.status = SettledStatus.付款接口支付中;
            }
            int num = SettledFactory.Apply(info2);
            info2.id = num;
            if (num > 0)
            {
                if (info2.status == SettledStatus.付款接口支付中)
                {
                    Withdraw.InitDistribution(info2);
                }
                return str;
            }
            return "提现失败";
        }

        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", "");
            }
        }

        public string orderBy
        {
            get
            {
                return WebBase.GetQueryStringString("orderby", "balance");
            }
        }

        public string orderByType
        {
            get
            {
                return WebBase.GetQueryStringString("type", "asc");
            }
        }

        public int proid
        {
            get
            {
                return WebBase.GetQueryStringInt32("proid", 0);
            }
        }

        public int UserID
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }

        public string UserStatus
        {
            get
            {
                return WebBase.GetQueryStringString("UserStatus", "");
            }
        }
    }
}

