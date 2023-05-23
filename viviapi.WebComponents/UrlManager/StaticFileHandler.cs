using System.IO;
using System.Web;
using viviapi.SysConfig;

namespace viviLib.WebComponents.UrlManager
{
    public class StaticFileHandler : HandlerBase
    {
        public StaticFileHandler()
        {
        }

        public StaticFileHandler(HttpContext context, UrlManagerConfig config, string simplePath, string realPath, string filePath, string pathInfo, string queryString, string url)
          : base(context, config, simplePath, realPath, filePath, pathInfo, queryString, url)
        {
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            context.Response.Clear();
            string str = context.Server.MapPath(this.RealPath);
            if (File.Exists(str))
            {
                context.Response.WriteFile(str);
                context.Response.End();
            }
            else
                context.Response.Redirect(this.Error404Url, true);
        }
    }
}
