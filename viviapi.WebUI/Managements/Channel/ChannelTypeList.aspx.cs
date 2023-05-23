namespace viviapi.WebUI.Managements
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.BLL.Channel;
    using viviapi.Model;
    using viviapi.Model.Channel;
    using viviapi.WebComponents.Web;

    public class ChannelTypeList : ManagePageBase
    {
        protected Button btnAdd;
        protected HtmlForm Form1;
        protected GridView GVChannel;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("SupplierEdit.aspx", true);
        }

        protected void GVChannel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataItem = e.Row.DataItem as DataRowView;
                Literal literal = e.Row.FindControl("ltType") as Literal;
                Literal literal2 = e.Row.FindControl("ltrunmode") as Literal;
                literal.Text = Enum.GetName(typeof(ChannelClassEnum), dataItem["classid"]);
                if (dataItem["runmode"].ToString() == "1")
                {
                    literal2.Text = "<span style='color:red'>轮询</span>";
                }
                else
                {
                    literal2.Text = "单独";
                }
                int num = int.Parse(dataItem["isOpen"].ToString());
                Literal literal3 = e.Row.FindControl("litOpen") as Literal;
                switch (num)
                {
                    case 1:
                        literal3.Text = "<span style='color:red'>全部关闭</span>";
                        break;

                    case 2:
                        literal3.Text = "<span style='color:green'>全部开启</span>";
                        break;

                    case 4:
                        literal3.Text = "<span style='color:red'>按配置(默认关闭)</span>";
                        break;

                    case 8:
                        literal3.Text = "<span style='color:green'>按配置(默认开启)</span>";
                        break;
                }
                RadioButtonList list = e.Row.FindControl("rblrelease") as RadioButtonList;
                list.SelectedValue = dataItem["release"].ToString();
                list.Enabled = false;
            }
        }

        private void LoadData()
        {
            this.GVChannel.DataSource = ChannelType.GetList(null);
            this.GVChannel.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
            ManageFactory.CheckSecondPwd();
            if (!base.IsPostBack)
            {
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
    }
}

