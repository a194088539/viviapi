namespace viviapi.gateway.tools
{
    using System.Web;

    public class SyncLocalCache : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
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

