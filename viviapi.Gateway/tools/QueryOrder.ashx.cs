namespace viviapi.Gateway.tools
{
    using System.Collections.Generic;
    using System.Data;
    using System.Web;
    using viviapi.BLL;
    using viviLib;
    using viviLib.Data;
    using viviLib.Security;

    public class QueryOrder : IHttpHandler
    {
        private string status = "0";
        private string url = "";

        public void ProcessRequest(HttpContext context)
        {
            string paramValue = XRequest.GetString("wxorderid");
            List<SearchParam> searchParams = new List<SearchParam>();
            searchParams.Add(new SearchParam("orderid", paramValue));
            string orderby = string.Empty;
            OrderBank bank = new OrderBank();
            DataTable table = bank.PageSearch(searchParams, 0x2710, 1, orderby).Tables[1];
            if ((table == null) || (table.Rows.Count <= 0))
            {
                this.status = "ERR2";
            }
            else if (table.Rows[0]["status"].ToString() == "2")
            {
                this.status = "1";
                this.url = table.Rows[0]["againNotifyUrl"].ToString();
            }
            AntiXss.JavaScriptEncode(this.url);
            context.Response.ContentType = "text/plain";
            context.Response.Write(this.status);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

