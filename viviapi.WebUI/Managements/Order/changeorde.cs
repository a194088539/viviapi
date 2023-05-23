namespace viviapi.WebUI.Managements.Order
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Order;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.Model.Order;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class changeorde : ManagePageBase
    {
        private OrderCardInfo _model = null;
        protected OrderCard bll = new OrderCard();
        protected Button btnSave;
        protected Button btnSend;
        protected DropDownList ddlSupp;
        protected HtmlForm form1;
        protected RadioButtonList rblOrdClass;
        protected RegularExpressionValidator rev_amt;
        protected RequiredFieldValidator rfv_amt;
        protected RequiredFieldValidator rfv_order;
        protected TextBox txtOrder;
        protected TextBox txtOrderAmt;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.SaveInfo();
            if (string.IsNullOrEmpty(str))
            {
                str = "操作成功";
            }
            base.AlertAndRedirect(str);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string str = this.SaveInfo();
            if (string.IsNullOrEmpty(str))
            {
                int num = int.Parse(this.ddlSupp.SelectedValue);
                if (this.model != null)
                {
                    string supporderid = string.Empty;
                    string errormsg = string.Empty;
                    string supperrorcode = string.Empty;
                    decimal d = decimal.Parse(this.txtOrderAmt.Text.Trim());
                    SupplierCode supp = (SupplierCode)num;
                    if (SellFactory.SellCard(supp, this.OrderId, this.model.typeId, this.model.cardNo, this.model.cardPwd, string.Empty, Convert.ToInt32(decimal.Round(d, 0)), out supporderid, out supperrorcode, out errormsg) != "0")
                    {
                        str = "提交失败";
                    }
                    else
                    {
                        str = "提交成功";
                    }
                }
                else
                {
                    str = "操作失败";
                }
            }
            base.AlertAndRedirect(str, "CardOrderList.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            if (!base.IsPostBack)
            {
                this.txtOrder.Text = this.OrderId;
                DataTable table = SupplierFactory.GetList(string.Empty).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    this.ddlSupp.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                if (this.model != null)
                {
                    this.txtOrderAmt.Text = this.model.refervalue.ToString("f2");
                    this.ddlSupp.SelectedValue = this.model.supplierId.ToString();
                }
            }
        }

        private string SaveInfo()
        {
            decimal result = 0M;
            string str = string.Empty;
            if (string.IsNullOrEmpty(this.OrderId))
            {
                str = "订单号不能为空";
            }
            else if (string.IsNullOrEmpty(this.ddlSupp.SelectedValue))
            {
                str = "请选择接口商";
            }
            else if (!decimal.TryParse(this.txtOrderAmt.Text.Trim(), out result))
            {
                str = "金额不能为0";
            }
            if (string.IsNullOrEmpty(str) && !Dal.UpdateCardOrderStatus(this.OrderId, int.Parse(this.ddlSupp.SelectedValue), result))
            {
                str = "保存失败";
            }
            return str;
        }

        protected OrderCardInfo model
        {
            get
            {
                if (!(string.IsNullOrEmpty(this.OrderId) || (this._model != null)))
                {
                    this._model = this.bll.GetModel(this.OrderId);
                }
                return this._model;
            }
        }

        protected string OrderId
        {
            get
            {
                return WebBase.GetQueryStringString("orderid", "");
            }
        }
    }
}

