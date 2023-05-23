using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;
using viviapi.SysConfig;

namespace viviLib.WebComponents.UrlManager
{
    public class HandlerBase : IHttpHandler, IRequiresSessionState
    {
        private static List<string> _writingStaticFilePathes = new List<string>();
        protected UrlManagerConfig Config;
        protected string SimplePath;
        protected string RealPath;
        protected string FilePath;
        protected string PathInfo;
        protected string QueryString;
        protected string Url;

        public virtual bool IsReusable
        {
            get
            {
                return true;
            }
        }

        protected string Error404Url
        {
            get
            {
                return "/Error404Url.aspx";
            }
        }

        public static List<string> WritingStaticFilePathes
        {
            get
            {
                return HandlerBase._writingStaticFilePathes;
            }
        }

        public HandlerBase()
        {
        }

        public HandlerBase(HttpContext context, UrlManagerConfig config, string simplePath, string realPath, string filePath, string pathInfo, string queryString, string url)
          : this()
        {
            this.Config = config;
            this.SimplePath = context.Request.ApplicationPath == "/" ? simplePath : context.Request.ApplicationPath + simplePath;
            this.RealPath = context.Request.ApplicationPath == "/" ? realPath : context.Request.ApplicationPath + realPath;
            this.FilePath = context.Request.ApplicationPath == "/" ? filePath : context.Request.ApplicationPath + filePath;
            this.PathInfo = pathInfo;
            this.QueryString = queryString;
            this.Url = url;
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            if (this.Config != null)
            {
                switch (this.Config.Action)
                {
                    case "none":
                        this.SimplePath = context.Request.Path;
                        this.RealPath = context.Request.Path;
                        this.FilePath = context.Request.Path;
                        this.PathInfo = context.Request.PathInfo;
                        this.QueryString = context.Request.QueryString.ToString();
                        this.Url = context.Request.Url.ToString();
                        break;
                    case "redirect":
                        context.Response.Redirect(this.Url, true);
                        break;
                }
            }
            else
            {
                this.Config = new UrlManagerConfig();
                this.Config.Action = "none";
                this.SimplePath = context.Request.Path;
                this.RealPath = context.Request.Path;
                this.FilePath = context.Request.Path;
                this.PathInfo = context.Request.PathInfo;
                this.QueryString = context.Request.QueryString.ToString();
                this.Url = context.Request.Url.ToString();
            }
        }

        protected void SetStaticFilter(HttpContext context, string physicalPath)
        {
            string directoryName = Path.GetDirectoryName(physicalPath);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            context.Response.Filter = (Stream)new Filter(context.Response.Filter, physicalPath);
        }
    }
}
