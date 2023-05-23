namespace viviapi.WebUI.Business
{
    using System;
    using System.Web.UI.HtmlControls;
    using viviapi.WebComponents.Web;

    public class Left : BusinessPageBase
    {
        protected HtmlAnchor a1;
        protected HtmlAnchor a10;
        protected HtmlAnchor a2;
        protected HtmlAnchor a3;
        protected HtmlAnchor a4;
        protected HtmlAnchor a5;
        protected HtmlAnchor a6;
        protected HtmlAnchor a7;
        protected HtmlAnchor a8;
        protected HtmlAnchor a9;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setPower();
        }

        private void setPower()
        {
        }
    }
}

