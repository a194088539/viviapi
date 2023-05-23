namespace viviapi.WebUI.Managements
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class SMSLogList : ManagePageBase
    {
        protected Button btn_Search;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater repSms;
        protected HtmlInputHidden selectedUsers;
        protected TextBox txtMobile;

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void Docmd()
        {
            if (((this.logid > 0) && !string.IsNullOrEmpty(this.cmd)) && ((this.cmd == "open") || (this.cmd == "close")))
            {
                bool state = this.cmd == "open";
                PhoneValidFactory.PhoneSetting(this.logid, state);
            }
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            if (!string.IsNullOrEmpty(this.txtMobile.Text))
            {
                searchParams.Add(new SearchParam("Mobile", this.txtMobile.Text.Trim()));
            }
            DataSet set = PhoneValidFactory.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, string.Empty);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.detailData = set.Tables[2];
            this.repSms.DataSource = set.Tables[1];
            this.repSms.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            this.Docmd();
            if (!base.IsPostBack)
            {
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void repSms_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string str = DataBinder.Eval(e.Item.DataItem, "phone").ToString();
                bool flag = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "enable"));
                string str2 = DataBinder.Eval(e.Item.DataItem, "ID").ToString();
                Repeater repeater = (Repeater)e.Item.FindControl("rptDetail");
                Literal literal = (Literal)e.Item.FindControl("litcmd");
                if (repeater != null)
                {
                    DataRow[] rowArray = this.detailData.Select("phone=" + str);
                    repeater.DataSource = rowArray;
                    repeater.DataBind();
                }
                if (flag)
                {
                    literal.Text = "<a onclick=\"return confirm('你确定要关闭发送短信功能吗?')\" href=\"?cmd=close&ID=" + str2 + "\" style=\"color:Green;\">关闭</a>";
                }
                else
                {
                    literal.Text = "<a onclick=\"return confirm('你确定要开启发送短信功能吗?')\" href=\"?cmd=open&ID=" + str2 + "\" style=\"color:Green;\">开启</a>";
                }
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Merchant))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public string cmd
        {
            get
            {
                return WebBase.GetQueryStringString("cmd", string.Empty);
            }
        }

        public DataTable detailData
        {
            get
            {
                return (this.ViewState["detailData"] as DataTable);
            }
            set
            {
                this.ViewState["detailData"] = value;
            }
        }

        public int logid
        {
            get
            {
                return WebBase.GetQueryStringInt32("ID", 0);
            }
        }
    }
}

