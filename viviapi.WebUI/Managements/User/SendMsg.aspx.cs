namespace viviapi.WebUI.Managements.User
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;

    public class SendMsg : ManagePageBase
    {
        private IMSG _item = null;
        protected Button bt_sub;
        protected HiddenField content;
        protected HtmlForm form1;
        protected Label lblMsg;
        protected HiddenField NewsID;
        protected TextBox tb_title;
        protected TextBox txtMsgTo;

        protected void bt_sub_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string str in this.GMID.Split(new char[] { ',' }))
                {
                    int result = 0;
                    if (int.TryParse(str, out result))
                    {
                        this._item = new IMSG();
                        this.Item.msg_from = 0;
                        this.Item.msg_to = new int?(int.Parse(str));
                        this.Item.msg_addtime = new DateTime?(DateTime.Now);
                        this.Item.msg_content = this.content.Value;
                        this.Item.msg_title = this.tb_title.Text;
                        this.Item.msg_fromname = "管理员";
                        IMSGFactory.Add(this._item);
                    }
                }
                this.lblMsg.Text = "发送成功";
            }
            catch (Exception)
            {
                this.lblMsg.Text = "发送失败";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtMsgTo.Text = this.GMID;
            }
        }

        public string GMID
        {
            get
            {
                return base.Request.QueryString["uid"];
            }
        }

        public string GMName
        {
            get
            {
                return base.Request.QueryString["UserName"];
            }
        }

        public IMSG Item
        {
            get
            {
                return this._item;
            }
        }
    }
}

