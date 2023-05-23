using System.IO;
using System.Web;
using viviapi.SysConfig;

namespace viviLib.WebComponents.UrlManager
{
    public class UrlManagerFactory : IHttpHandlerFactory
    {
        public void ReleaseHandler(IHttpHandler handler)
        {
        }

        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            UrlManagerConfig config = (UrlManagerConfig)null;
            string simplePath = string.Empty;
            string realPath = string.Empty;
            string filePath = string.Empty;
            string pathInfo = string.Empty;
            string queryString = string.Empty;
            string url1 = string.Empty;
            UrlManagerConfig.IsMatch(context, ref config, ref simplePath, ref realPath, ref filePath, ref pathInfo, ref queryString, ref url1);
            if (config != null)
            {
                if (Path.GetExtension(context.Request.Path) == ".aspx")
                    return (IHttpHandler)new PageHandler(context, config, simplePath, realPath, filePath, pathInfo, queryString, url1);
                return (IHttpHandler)new StaticFileHandler(context, config, simplePath, realPath, filePath, pathInfo, queryString, url1);
            }
            if (Path.GetExtension(context.Request.Path) == ".aspx")
                return (IHttpHandler)new PageHandler();
            return (IHttpHandler)new StaticFileHandler();
        }
    }
}
