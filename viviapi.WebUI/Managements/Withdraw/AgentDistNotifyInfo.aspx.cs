namespace viviapi.WebUI.Managements.Withdraw
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.WebComponents.Web;

    public class AgentDistNotifyInfo : ManagePageBase
    {
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Label lbladdTime;
        protected Label lblext1;
        protected Label lblext2;
        protected Label lblext3;
        protected Label lblid;
        protected Label lblnotify_id;
        protected Label lblnotifystatus;
        protected Label lblnotifyurl;
        protected Label lblout_trade_no;
        protected Label lblremark;
        protected Label lblresText;
        protected Label lblresTime;
        protected Label lbltrade_no;
        protected Label lbluserid;
        public string strid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != "")))
            {
                this.strid = base.Request.Params["id"];
                int id = Convert.ToInt32(this.strid);
                this.ShowInfo(id);
            }
        }

        private void ShowInfo(int id)
        {
            viviapi.BLL.Withdraw.settledAgentNotify notify = new viviapi.BLL.Withdraw.settledAgentNotify();
            viviapi.Model.Withdraw.settledAgentNotify model = notify.GetModel(id);
            this.lblid.Text = model.id.ToString();
            this.lblnotify_id.Text = model.notify_id;
            this.lbluserid.Text = model.userid.ToString();
            this.lbltrade_no.Text = model.trade_no;
            this.lblout_trade_no.Text = model.out_trade_no;
            this.lblnotifystatus.Text = notify.GetNotifyStatusText(model.notifystatus);
            this.lblnotifyurl.Text = model.notifyurl;
            this.lblresText.Text = base.Server.HtmlDecode(model.resText);
            this.lbladdTime.Text = model.addTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblresTime.Text = model.resTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblext1.Text = model.ext1;
            this.lblext2.Text = model.ext2;
            this.lblext3.Text = model.ext3;
            this.lblremark.Text = model.remark;
        }
    }
}

