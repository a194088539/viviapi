namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.Model;
    using viviapi.WebComponents.Web;
    using viviLib.Web;

    public class ChannelList : ManagePageBase
    {
        protected Button btnAdd;
        protected Button btnSearch;
        protected DropDownList ddlType;
        protected HtmlForm Form1;
        protected GridView GVChannel;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ChannelEdit.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["selecttype"] = this.ddlType.SelectedValue;
            this.LoadData();
        }

        protected void GVChannel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "del")
                {
                    viviapi.BLL.Channel.Channel.Delete(Convert.ToInt32(e.CommandArgument));
                    base.AlertAndRedirect("删除成功");
                }
            }
            catch (Exception exception)
            {
                base.AlertAndRedirect("Error:" + exception.Message);
            }
        }

        protected void GVChannel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = e.Row.DataItem as DataRowView;
                Literal literal = e.Row.FindControl("litopen") as Literal;
                string str = "Unknown";
                if (dataItem["isOpen"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dataItem["isOpen"]) == 0)
                    {
                        str = "wrong";
                    }
                    else
                    {
                        str = "right";
                    }
                }
                literal.Text = string.Format("<img  src='../style/images/{0}.png' />", str);
            }
        }

        private void LoadData()
        {
            int result = 0;
            if (!string.IsNullOrEmpty(this.ddlType.SelectedValue))
            {
                int.TryParse(this.ddlType.SelectedValue, out result);
            }
            this.GVChannel.DataSource = viviapi.BLL.Channel.Channel.GetList(result);
            this.GVChannel.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageFactory.CheckSecondPwd();
            this.setPower();
            if (!base.IsPostBack)
            {
                this.ddlType.Items.Add(new ListItem("---全部类别---", ""));
                DataTable table = ChannelType.GetList(null).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    this.ddlType.Items.Add(new ListItem(row["modetypename"].ToString(), row["typeId"].ToString()));
                }
                if (!string.IsNullOrEmpty(this.ptypeId))
                {
                    this.ddlType.SelectedValue = this.ptypeId;
                }
                this.LoadData();
            }
        }

        private void setPower()
        {
            if (!ManageFactory.CheckCurrentPermission(false, ManageRole.Interfaces))
            {
                base.Response.Write("Sorry,No authority!");
                base.Response.End();
            }
        }

        public string ptypeId
        {
            get
            {
                return WebBase.GetQueryStringString("typeId", "");
            }
        }
    }
}

