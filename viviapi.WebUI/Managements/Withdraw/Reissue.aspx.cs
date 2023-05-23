namespace viviapi.WebUI.Managements.Withdraw
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.ETAPI;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class Reissue : ManagePageBase
    {
        private viviapi.Model.distribution _model = null;
        protected Button btnAdd;
        protected DropDownList ddlstatus;
        protected DropDownList ddlSupplier;
        protected HtmlForm form1;
        protected TextBox txtamount;
        protected TextBox txterror_message;
        protected TextBox txtout_trade_no;
        protected TextBox txttrade_no;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.model != null)
            {
                bool flag = false;
                int num = int.Parse(this.ddlstatus.SelectedValue);
                if (((num == 1) || (num == 2)) || (num == 4))
                {
                    flag = true;
                }
                switch (Withdraw.Complete(int.Parse(this.ddlSupplier.SelectedValue), this.txttrade_no.Text.Trim(), flag, int.Parse(this.ddlstatus.SelectedValue), this.txtamount.Text.Trim(), this.txttrade_no.Text.Trim(), this.txterror_message.Text.Trim()))
                {
                    case 0:
                        msg = "处理成功";
                        goto Label_012F;

                    case 1:
                        msg = "无效单";
                        goto Label_012F;

                    case 2:
                        msg = "无效接口商";
                        goto Label_012F;

                    case 3:
                        msg = "状态无效";
                        break;

                    case 0x63:
                        msg = "系统出错";
                        break;
                }
            }
        Label_012F:
            base.AlertAndRedirect(msg);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            if (!base.IsPostBack)
            {
                DataTable table = SupplierFactory.GetList("isdistribution=1").Tables[0];
                this.ddlSupplier.Items.Add(new ListItem("--付款接口--", ""));
                this.ddlSupplier.Items.Add(new ListItem("不走接口", "0"));
                foreach (DataRow row in table.Rows)
                {
                    this.ddlSupplier.Items.Add(new ListItem(row["name"].ToString(), row["code"].ToString()));
                }
                if (this.model != null)
                {
                    this.txttrade_no.Attributes["readonly"] = "true";
                    this.txttrade_no.Text = this.model.trade_no;
                    this.ddlstatus.SelectedValue = this.model.status.ToString();
                    this.txtamount.Text = this.model.amount.ToString();
                    this.ddlSupplier.SelectedValue = this.model.suppid.ToString();
                }
            }
        }

        protected int id
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public viviapi.Model.distribution model
        {
            get
            {
                if ((this._model == null) && (this.id > 0))
                {
                    this._model = viviapi.BLL.distribution.GetModel(this.id);
                }
                return this._model;
            }
        }
    }
}

