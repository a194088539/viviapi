namespace viviapi.WebUI.business.Order
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class Console_Order_ResetOrder : BusinessPageBase
    {
        protected Button btnAdd;
        protected DropDownList ddlSupp;
        protected HtmlForm form1;
        protected RadioButtonList rblOrdClass;
        protected RegularExpressionValidator rev_amt;
        protected RequiredFieldValidator rfv_amt;
        protected RequiredFieldValidator rfv_order;
        protected TextBox txtOrder;
        protected TextBox txtOrderAmt;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!base.IsValid)
            {
                return;
            }
            bool flag = false;
            string selectedValue = this.ddlSupp.SelectedValue;
            decimal tranAMT = decimal.Parse(this.txtOrderAmt.Text.Trim());
            if (tranAMT <= 0M)
            {
                base.AlertAndRedirect("金额不能为0");
                return;
            }
            if (!string.IsNullOrEmpty(selectedValue))
            {
                switch (this.rblOrdClass.SelectedValue)
                {
                    case "1":
                        flag = new OrderBank().DoBankComplete(int.Parse(selectedValue), this.txtOrder.Text.Trim(), "ResetOrder", 2, "0", "", tranAMT, 0M, true, false, false);
                        goto Label_0148;

                    case "2":
                        flag = new OrderCard().RepairOrder(int.Parse(selectedValue), this.txtOrder.Text.Trim(), "ResetOrder", 2, "0", "", "", tranAMT, 0M, string.Empty, 1);
                        break;
                }
            }
        Label_0148:
            if (flag)
            {
                base.AlertAndRedirect("操作成功");
            }
            else
            {
                base.AlertAndRedirect("操作失败");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            if (!base.IsPostBack)
            {
                this.txtOrder.Text = this.OrderId;
                this.rblOrdClass.SelectedValue = this.oclass.ToString();
                DataTable table = SupplierFactory.GetList(string.Empty).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                this.ddlSupp.SelectedValue = this.supp.ToString();
                this.txtOrderAmt.Text = decimal.Round(this.amt).ToString("f2");
            }
        }

        protected decimal amt
        {
            get
            {
                return WebBase.GetQueryStringDecimal("amt", 0M);
            }
        }

        protected int oclass
        {
            get
            {
                return WebBase.GetQueryStringInt32("oclass", 0);
            }
        }

        protected string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }

        protected int supp
        {
            get
            {
                return WebBase.GetQueryStringInt32("supp", 0);
            }
        }
    }
}

