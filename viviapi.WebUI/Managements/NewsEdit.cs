namespace viviapi.WebUI.Managements
{
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.TimeControl;
    using viviLib.Web;

    public class NewsEdit : ManagePageBase
    {
        public NewsInfo _ItemInfo = null;
        protected CheckBox cb_bold;
        protected CheckBox cb_pop;
        protected CheckBox cb_red;
        protected CheckBox cb_top;
        protected HiddenField content;
        protected DropDownList ddl_type;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected RadioButtonList rbColor;
        protected RadioButtonList rbl_Release;
        protected TextBox txtColorCode;
        protected HtmlInputText txtDate;
        protected TextBox txtTitle;

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            bool flag = false;
            int num = int.Parse(this.ddl_type.SelectedValue);
            string text = this.txtTitle.Text;
            DateTime time = DateTime.Parse(this.txtDate.Value);
            string str2 = this.content.Value;
            int num2 = this.cb_red.Checked ? 1 : 0;
            int num3 = this.cb_top.Checked ? 1 : 0;
            int num4 = this.cb_pop.Checked ? 1 : 0;
            int num5 = this.cb_bold.Checked ? 1 : 0;
            string str3 = this.txtColorCode.Text.Trim();
            this.ItemInfo.newstype = num;
            this.ItemInfo.newstitle = text;
            this.ItemInfo.addTime = time;
            this.ItemInfo.newscontent = str2;
            this.ItemInfo.IsRed = num2;
            this.ItemInfo.IsTop = num3;
            this.ItemInfo.IsPop = num4;
            this.ItemInfo.Isbold = num5;
            this.ItemInfo.Color = str3;
            this.ItemInfo.release = this.rbl_Release.SelectedValue == "1";
            if (this.isUpdate)
            {
                flag = NewsFactory.Update(this.ItemInfo);
            }
            else
            {
                flag = NewsFactory.Add(this.ItemInfo) > 0;
            }
            if (flag)
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
                this.content.Value = this.ItemInfo.newscontent;
                this.cb_red.Checked = this.ItemInfo.IsRed == 1;
                this.cb_top.Checked = this.ItemInfo.IsTop == 1;
                this.cb_pop.Checked = this.ItemInfo.IsPop == 1;
                this.cb_bold.Checked = this.ItemInfo.Isbold == 1;
                this.rbl_Release.SelectedValue = this.ItemInfo.release ? "1" : "0";
                this.txtColorCode.Text = this.ItemInfo.Color;
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

