namespace viviapi.WebUI.LongBao.console.news
{
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.TimeControl;
    using viviLib.Web;

    public class WebForm1 : ManagePageBase
    {
        public NewsInfo _ItemInfo = null;
        protected Button BtnSubmit;
        protected CheckBox cb_bold;
        protected CheckBox cb_pop;
        protected CheckBox cb_red;
        protected CheckBox cb_top;
        protected DropDownList ddl_type;
        protected HtmlForm form1;
        protected HiddenField hfcontent;
        protected string newscontent = string.Empty;
        protected RadioButtonList rbColor;
        protected RadioButtonList rbl_Release;
        protected TextBox txtColorCode;
        protected HtmlInputText txtDate;
        protected TextBox txtTitle;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            bool success = false;
            int newstype = int.Parse(this.ddl_type.SelectedValue);
            string newstitle = this.txtTitle.Text;
            DateTime addTime = DateTime.Parse(this.txtDate.Value);
            string newscontent = this.hfcontent.Value;
            int IsRed = this.cb_red.Checked ? 1 : 0;
            int IsTop = this.cb_top.Checked ? 1 : 0;
            int IsPop = this.cb_pop.Checked ? 1 : 0;
            int Isbold = this.cb_bold.Checked ? 1 : 0;
            string color = this.txtColorCode.Text.Trim();
            this.ItemInfo.newstype = newstype;
            this.ItemInfo.newstitle = newstitle;
            this.ItemInfo.addTime = addTime;
            this.ItemInfo.newscontent = newscontent;
            this.ItemInfo.IsRed = IsRed;
            this.ItemInfo.IsTop = IsTop;
            this.ItemInfo.IsPop = IsPop;
            this.ItemInfo.Isbold = Isbold;
            this.ItemInfo.Color = color;
            this.ItemInfo.release = this.rbl_Release.SelectedValue == "1";
            if (this.isUpdate)
            {
                success = NewsFactory.Update(this.ItemInfo);
            }
            else
            {
                success = NewsFactory.Add(this.ItemInfo) > 0;
            }
            if (success)
            {
                base.AlertAndRedirect("保存成功!", "NewsList.aspx");
            }
            else
            {
                base.AlertAndRedirect("保存失败");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.News))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        private void ShowInfo()
        {
            this.txtDate.Attributes.Add("onFocus", "WdatePicker()");
            foreach (int num in Enum.GetValues(typeof(NewsType)))
            {
                this.ddl_type.Items.Add(new ListItem(Enum.GetName(typeof(NewsType), num), num.ToString()));
            }
            if (this.isUpdate && (this.ItemInfo != null))
            {
                this.ddl_type.SelectedValue = this.ItemInfo.newstype.ToString();
                this.txtTitle.Text = this.ItemInfo.newstitle;
                this.txtDate.Value = FormatConvertor.DateTimeToDateString(this.ItemInfo.addTime);
                this.hfcontent.Value = HttpUtility.HtmlEncode(this.ItemInfo.newscontent);
                this.cb_red.Checked = this.ItemInfo.IsRed == 1;
                this.cb_top.Checked = this.ItemInfo.IsTop == 1;
                this.cb_pop.Checked = this.ItemInfo.IsPop == 1;
                this.cb_bold.Checked = this.ItemInfo.Isbold == 1;
                this.rbl_Release.SelectedValue = this.ItemInfo.release ? "1" : "0";
                this.txtColorCode.Text = this.ItemInfo.Color;
                this.newscontent = HttpUtility.HtmlDecode(this.ItemInfo.newscontent);
            }
            else
            {
                this.txtDate.Value = FormatConvertor.DateTimeToDateString(DateTime.Now);
            }
        }

        public bool isUpdate
        {
            get
            {
                return (this.ItemInfoId > 0);
            }
        }

        public NewsInfo ItemInfo
        {
            get
            {
                if (this._ItemInfo == null)
                {
                    if (this.ItemInfoId > 0)
                    {
                        this._ItemInfo = NewsFactory.GetModel(this.ItemInfoId);
                    }
                    else
                    {
                        this._ItemInfo = new NewsInfo();
                    }
                }
                return this._ItemInfo;
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

