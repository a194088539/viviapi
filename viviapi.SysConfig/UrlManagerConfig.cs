using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Xml;
using viviLib.ExceptionHandling;

namespace viviapi.SysConfig
{
    [Serializable]
    public class UrlManagerConfig
    {
        protected static volatile Cache webCache = HttpRuntime.Cache;
        private string _host = string.Empty;
        private string _action = string.Empty;
        private string _pattern = string.Empty;
        private string _url = string.Empty;
        private string _realPath = string.Empty;
        private string _filePath = string.Empty;
        private string _queryString = string.Empty;
        private string _pathInfo = string.Empty;
        private TimeSpan _timeSpan = new TimeSpan(0L);

        public string Host
        {
            get
            {
                return this._host;
            }
            set
            {
                this._host = value;
            }
        }

        public string Action
        {
            get
            {
                return this._action;
            }
            set
            {
                this._action = value;
            }
        }

        public string Pattern
        {
            get
            {
                return this._pattern;
            }
            set
            {
                this._pattern = value;
            }
        }

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        public string RealPath
        {
            get
            {
                return this._realPath;
            }
            set
            {
                this._realPath = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this._filePath;
            }
            set
            {
                this._filePath = value;
            }
        }

        public string QueryString
        {
            get
            {
                return this._queryString;
            }
            set
            {
                this._queryString = value;
            }
        }

        public string PathInfo
        {
            get
            {
                return this._pathInfo;
            }
            set
            {
                this._pathInfo = value;
            }
        }

        public TimeSpan TimeSpan
        {
            get
            {
                return this._timeSpan;
            }
            set
            {
                this._timeSpan = value;
            }
        }

        public static List<UrlManagerConfig> GetListFromXmlDocument(XmlDocument doc, string host)
        {
            XmlNodeList xmlNodeList = doc.SelectNodes("configs/" + host + "/location");
            List<UrlManagerConfig> list = new List<UrlManagerConfig>(xmlNodeList.Count);
            foreach (XmlNode node in xmlNodeList)
                list.Add(UrlManagerConfig.GetFromXmlNode(node, host));
            return list;
        }

        public static UrlManagerConfig GetFromXmlNode(XmlNode node, string host)
        {
            UrlManagerConfig urlManagerConfig = new UrlManagerConfig();
            urlManagerConfig.Host = host;
            if (node.Attributes["action"] != null && node.Attributes["action"].Value != null && node.Attributes["action"].Value.Length > 0)
                urlManagerConfig.Action = node.Attributes["action"].Value;
            if (node.Attributes["pattern"] != null && node.Attributes["pattern"].Value != null && node.Attributes["pattern"].Value.Length > 0)
                urlManagerConfig.Pattern = node.Attributes["pattern"].Value;
            if (node.Attributes["url"] != null && node.Attributes["url"].Value != null && node.Attributes["url"].Value.Length > 0)
                urlManagerConfig.Url = node.Attributes["url"].Value;
            if (node.Attributes["realpath"] != null && node.Attributes["realpath"].Value != null && node.Attributes["realpath"].Value.Length > 0)
                urlManagerConfig.RealPath = node.Attributes["realpath"].Value;
            if (node.Attributes["filepath"] != null && node.Attributes["filepath"].Value != null && node.Attributes["filepath"].Value.Length > 0)
                urlManagerConfig.FilePath = node.Attributes["filepath"].Value;
            if (node.Attributes["pathinfo"] != null && node.Attributes["pathinfo"].Value != null && node.Attributes["pathinfo"].Value.Length > 0)
                urlManagerConfig.PathInfo = node.Attributes["pathinfo"].Value;
            if (node.Attributes["querystring"] != null && node.Attributes["querystring"].Value != null && node.Attributes["querystring"].Value.Length > 0)
                urlManagerConfig.QueryString = node.Attributes["querystring"].Value;
            if (node.Attributes["timespan"] != null && node.Attributes["timespan"].Value != null && node.Attributes["timespan"].Value.Length > 0)
                urlManagerConfig.TimeSpan = TimeSpan.Parse(node.Attributes["timespan"].Value);
            return urlManagerConfig;
        }

        public static List<UrlManagerConfig> GetConfigs(string host)
        {
            if (UrlManagerConfig.webCache.Get(host) != null)
                return UrlManagerConfig.webCache.Get(host) as List<UrlManagerConfig>;
            string managerConfigPath = RuntimeSetting.UrlManagerConfigPath;
            if (File.Exists(managerConfigPath))
            {
                try
                {
                    XmlTextReader xmlTextReader = new XmlTextReader(managerConfigPath);
                    int num = (int)xmlTextReader.MoveToContent();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlTextReader.ReadOuterXml());
                    List<UrlManagerConfig> listFromXmlDocument = UrlManagerConfig.GetListFromXmlDocument(doc, host);
                    UrlManagerConfig.webCache.Insert(host, (object)listFromXmlDocument, new CacheDependency(managerConfigPath));
                    xmlTextReader.Close();
                    return listFromXmlDocument;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
            return new List<UrlManagerConfig>(0);
        }

        public static bool IsMatch(HttpContext context, ref UrlManagerConfig config, ref string simplePath, ref string realPath, ref string filePath, ref string pathInfo, ref string queryString, ref string url)
        {
            string input = context.Request.RawUrl.Substring(context.Request.ApplicationPath == "/" ? context.Request.ApplicationPath.Length - 1 : context.Request.ApplicationPath.Length);
            simplePath = context.Request.Path.Substring(context.Request.ApplicationPath == "/" ? context.Request.ApplicationPath.Length - 1 : context.Request.ApplicationPath.Length);
            List<UrlManagerConfig> configs1 = UrlManagerConfig.GetConfigs("none");
            if (configs1 != null)
            {
                foreach (UrlManagerConfig urlManagerConfig in configs1)
                {
                    Match match = new Regex(urlManagerConfig.Pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(input);
                    if (match.Success)
                    {
                        config = urlManagerConfig;
                        string[] strArray = new string[match.Groups.Count];
                        for (int index = 0; index < match.Groups.Count; ++index)
                            strArray[index] = match.Groups[index].Value;
                        realPath = string.Format(urlManagerConfig.RealPath, (object[])strArray);
                        filePath = UrlManagerConfig.FormatFilePath(context, string.Format(urlManagerConfig.FilePath, (object[])strArray));
                        pathInfo = string.Format(urlManagerConfig.PathInfo, (object[])strArray);
                        queryString = string.Format(urlManagerConfig.QueryString, (object[])strArray);
                        url = string.Format(urlManagerConfig.Url, (object[])strArray);
                        return true;
                    }
                }
            }
            List<UrlManagerConfig> configs2 = UrlManagerConfig.GetConfigs(context.Request.Url.Host);
            if (configs2 != null)
            {
                foreach (UrlManagerConfig urlManagerConfig in configs2)
                {
                    Match match = new Regex(urlManagerConfig.Pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(input);
                    if (match.Success)
                    {
                        config = urlManagerConfig;
                        string[] strArray = new string[match.Groups.Count];
                        for (int index = 0; index < match.Groups.Count; ++index)
                            strArray[index] = match.Groups[index].Value;
                        realPath = string.Format(urlManagerConfig.RealPath, (object[])strArray);
                        filePath = UrlManagerConfig.FormatFilePath(context, string.Format(urlManagerConfig.FilePath, (object[])strArray));
                        pathInfo = string.Format(urlManagerConfig.PathInfo, (object[])strArray);
                        queryString = string.Format(urlManagerConfig.QueryString, (object[])strArray);
                        url = string.Format(urlManagerConfig.Url, (object[])strArray);
                        return true;
                    }
                }
            }
            return false;
        }

        internal static string FormatFilePath(HttpContext context, string filePath)
        {
            int num = filePath.LastIndexOf(".");
            if (num > 0)
                return filePath.Substring(0, num) + "." + context.Request.Url.Host + filePath.Substring(num);
            return filePath;
        }
    }
}
