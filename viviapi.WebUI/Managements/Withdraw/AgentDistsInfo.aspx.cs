namespace viviapi.WebUI.Managements.Withdraw
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.ETAPI;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class AgentDistsInfo : ManagePageBase
    {
        private viviapi.Model.Withdraw.settledAgent _model = null;
        private viviapi.BLL.Withdraw.settledAgent bll = new viviapi.BLL.Withdraw.settledAgent();
        protected Button btn_cancel;
        protected Button btnAudits;
        protected Button btnpayfail;
        protected Button btnpaysuccess;
        protected Button btnRefuse;
        protected Button btnreNotify;
        protected HtmlForm form1;
        protected Label Label1;
        protected Label lbladdTime;
        protected Label lblamount;
        protected Label lblaudit_status;
        protected Label lblbankAccount;
        protected Label lblbankAccountName;
        protected Label lblbankBranch;
        protected Label lblbankCode;
        protected Label lblbankName;
        protected Label lblcallbackText;
        protected Label lblcharge;
        protected Label lblid;
        protected Label lblis_cancel;
        protected Label lbllotno;
        protected Label lblmode;
        protected Label lblnotifystatus;
        protected Label lblnotifyTimes;
        protected Label lblout_trade_no;
        protected Label lblpayment_status;
        protected Label lblprocessingTime;
        protected Label lblremark;
        protected Label lblreturn_url;
        protected Label lblserial;
        protected Label lblservice;
        protected Label lblsign_type;
        protected Label lbltrade_no;
        protected Label lbltranApi;
        protected Label lbluserid;

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string msg = this.bll.doCancel(this.model.trade_no);
            base.AlertAndRedirect(msg);
        }

        protected void btnAudits_Click(object sender, EventArgs e)
        {
            string msg = this.bll.doAudit(this.model.trade_no, base.currentManage.id, base.currentManage.username);
            if ((msg == "审核成功") && ((this._model != null) && (this.model.suppid > 0)))
            {
                Withdraw.InitDistribution2(this.model);
            }
            base.AlertAndRedirect(msg);
        }

        protected void btnpayfail_Click(object sender, EventArgs e)
        {
            string msg = this.bll.PayFail(this.model.trade_no);
            base.AlertAndRedirect(msg);
        }

        protected void btnpaysuccess_Click(object sender, EventArgs e)
        {
            string msg = this.bll.PaySuccess(this.model.trade_no);
            base.AlertAndRedirect(msg);
        }

        protected void btnRefuse_Click(object sender, EventArgs e)
        {
            string msg = this.bll.doRefuse(this.model.trade_no, base.currentManage.id, base.currentManage.username);
            base.AlertAndRedirect(msg);
        }

        protected void btnreNotify_Click(object sender, EventArgs e)
        {
            this.bll.DoNotify(this.model.trade_no);
            base.AlertAndRedirect("已提交");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Orders))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            if (this.model != null)
            {
                this.lblid.Text = this.model.id.ToString();
                this.lblmode.Text = this.bll.GetModeText(this.model.mode);
                this.lbltrade_no.Text = this.model.trade_no;
                this.lblout_trade_no.Text = this.model.out_trade_no;
                this.lblservice.Text = this.model.service;
                this.lbluserid.Text = this.model.userid.ToString();
                this.lblsign_type.Text = this.model.sign_type;
                this.lblreturn_url.Text = this.model.return_url;
                this.lblbankCode.Text = this.model.bankCode;
                this.lblbankName.Text = this.model.bankName;
                this.lblbankBranch.Text = this.model.bankBranch;
                this.lblbankAccountName.Text = this.model.bankAccountName;
                this.lblbankAccount.Text = this.model.bankAccount;
                this.lblamount.Text = this.model.amount.ToString();
                this.lblcharge.Text = this.model.charge.ToString();
                this.lbladdTime.Text = this.model.addTime.ToString();
                this.lblprocessingTime.Text = this.model.processingTime.ToString();
                this.lblaudit_status.Text = this.bll.GetAuditStatusText(this.model.audit_status);
                this.lblpayment_status.Text = this.bll.GetPaymentStatusText(this.model.payment_status);
                this.lblis_cancel.Text = this.model.is_cancel ? "是" : "否";
                this.lblremark.Text = this.model.remark;
                this.lbltranApi.Text = this.model.suppid.ToString();
                this.lblnotifyTimes.Text = this.model.notifyTimes.ToString();
                this.lblnotifystatus.Text = this.model.notifystatus.ToString();
                this.lblcallbackText.Text = this.model.callbackText;
                this.lbllotno.Text = this.model.lotno;
                this.lblserial.Text = this.model.serial.ToString();
                if (!this.model.is_cancel)
                {
                    this.btn_cancel.Visible = (this.model.issure == 1) && (this.model.audit_status == 1);
                    this.btnAudits.Visible = (this.model.issure == 1) && (this.model.audit_status == 1);
                    this.btnRefuse.Visible = (this.model.issure == 1) && (this.model.audit_status == 1);
                    if (this.model.audit_status == 2)
                    {
                        this.btnpaysuccess.Visible = this.model.payment_status == 1;
                        this.btnpayfail.Visible = this.model.payment_status == 1;
                    }
                }
            }
        }

        public int id
        {
            get
            {
                return WebBase.GetQueryStringInt32("id", 0);
            }
        }

        public viviapi.Model.Withdraw.settledAgent model
        {
            get
            {
                if ((this._model == null) && (this.id > 0))
                {
                    this._model = this.bll.GetModel(this.id);
                }
                return this._model;
            }
        }
    }
}

