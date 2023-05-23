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
    using viviapi.Model;
    using viviapi.Model.Settled;
    using viviapi.WebComponents.Web;
    using viviLib.Data;
    using viviLib.Web;
    using Wuqi.Webdiyer;

    public class UnFreeze : ManagePageBase
    {
        protected Button btnSearch;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected AspNetPager Pager1;
        protected Repeater rptData;
        protected HtmlInputHidden selectedUsers;
        protected TextBox txtuserId;
        protected string wzfmoney = string.Empty;
        protected string yzfmoney = string.Empty;

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            List<SearchParam> searchParams = new List<SearchParam>();
            string str = this.txtuserId.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                int result = 0;
                int.TryParse(str, out result);
                searchParams.Add(new SearchParam("userid", result));
            }
            string orderby = this.orderBy + " " + this.orderByType;
            DataSet set = UsersAmtFreeze.PageSearch(searchParams, this.Pager1.PageSize, this.Pager1.CurrentPageIndex, orderby);
            this.Pager1.RecordCount = Convert.ToInt32(set.Tables[0].Rows[0][0]);
            this.rptData.DataSource = set.Tables[1];
            this.rptData.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            if (!base.IsPostBack)
            {
                this.LoadData();
            }
        }

        protected void Pager1_PageChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                AmtunFreezeMode mode = AmtunFreezeMode.未处理;
                if (e.CommandName == "unfreeze1")
                {
                    mode = AmtunFreezeMode.解冻到余额;
                }
                else if (e.CommandName == "unfreeze2")
                {
                    mode = AmtunFreezeMode.解冻并扣除;
                }
                if (mode != AmtunFreezeMode.未处理)
                {
                    if (UsersAmtFreeze.unFreeze(int.Parse(e.CommandArgument.ToString()), mode))
                    {
                        base.AlertAndRedirect("操作成功");
                    }
                    else
                    {
                        base.AlertAndRedirect("操作失败");
                    }
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect(exception.Message);
            }
        }

        protected void rptUsersItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string str = DataBinder.Eval(e.Item.DataItem, "status").ToString();
                Button button = (Button)e.Item.FindControl("btn_unfreeze1");
                Button button2 = (Button)e.Item.FindControl("btn_unfreeze2");
                if (str == "1")
                {
                    button.Visible = true;
                    button2.Visible = true;
                }
                else
                {
                    button.Visible = false;
                    button2.Visible = false;
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
                return WebBase.GetQueryStringString("orderby", "addtime");
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

