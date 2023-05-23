namespace viviapi.WebUI.Userlogin.order
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using viviapi.BLL;
    using viviapi.WebComponents.Web;
    using viviLib.Data;

    public class excha : UserPageBase
    {
        protected Button btnSubmit;
        protected DropDownList ddlcardType;
        protected HtmlForm form1;
        protected Repeater rptorders;
        protected TextBox txtCards;

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<SearchParam> searchParams = new List<SearchParam>();
                searchParams.Add(new SearchParam("userid", base.UserId));
                OrderCard card = new OrderCard();
                DataTable table = card.PageSearch(searchParams, 8, 1, string.Empty).Tables[1];
                this.rptorders.DataSource = table;
                this.rptorders.DataBind();
            }
        }
    }
}

