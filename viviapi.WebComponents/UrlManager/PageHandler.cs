using System;
using System.IO;
using System.Web;
using System.Web.UI;
using viviapi.SysConfig;

namespace viviLib.WebComponents.UrlManager
{
    public class PageHandler : HandlerBase
    {
        public PageHandler()
        {
        }

        public PageHandler(HttpContext context, UrlManagerConfig config, string simplePath, string realPath, string filePath, string pathInfo, string queryString, string url)
          : base(context, config, simplePath, realPath, filePath, pathInfo, queryString, url)
        {
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            string str1 = context.Server.MapPath(this.RealPath);
            if (File.Exists(str1))
            {
                switch (this.Config.Action)
                {
                    case "none":
                        PageParser.GetCompiledPageInstance(this.RealPath, str1, context).ProcessRequest(context);
                        break;
                    case "rewrite":
                        context.RewritePath(context.Request.Path, this.PathInfo, this.QueryString);
                        PageParser.GetCompiledPageInstance(this.RealPath, str1, context).ProcessRequest(context);
                        break;
                    case "cache_static":
                    case "cache_static_news":
                    case "cache_static_pub":
                        string str2 = context.Server.MapPath(this.FilePath);
                        FileInfo fileInfo1 = new FileInfo(str2);
                        FileInfo fileInfo2 = new FileInfo(str1);
                        bool flag = false;
                        if (!fileInfo1.Exists || this.Config.TimeSpan.Ticks > 0L && fileInfo1.LastWriteTime.Add(this.Config.TimeSpan) < DateTime.Now || fileInfo2.LastWriteTime > fileInfo1.LastWriteTime)
                            flag = true;
                        if (flag)
                        {
                            context.RewritePath(context.Request.Path, this.PathInfo, this.QueryString);
                            switch (this.Config.Action)
                            {
                                default:
                                    PageParser.GetCompiledPageInstance(this.RealPath, str1, context).ProcessRequest(context);
                                    this.SetStaticFilter(context, str2);
                                    return;
                            }
                        }
                        else
                        {
                            context.Response.WriteFile(str2);
                            context.Response.End();
                            break;
                        }
                }
            }
            else
                context.Response.Redirect(this.Error404Url, true);
        }
    }
}
