using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;
using viviLib.Configuration;
using viviLib.ExceptionHandling;

namespace viviLib.ScheduledTask
{
    public class ScheduledTaskConfigurationSectionHandler : IConfigurationSectionHandler
    {
        protected static Cache webCache = HttpRuntime.Cache;

        public object Create(object parent, object configContext, XmlNode section)
        {
            return (object)ScheduledTaskConfigurationSectionHandler.Parse(section);
        }

        public static List<ScheduledTaskConfiguration> GetConfigs()
        {
            string filePath = ConfigHelper.FilePath;
            string xpath = string.Format("/configuration/scheduledTaskConfiguration");
            string key = filePath + "|" + xpath;
            if (ScheduledTaskConfigurationSectionHandler.webCache[key] != null)
            {
                List<ScheduledTaskConfiguration> list = ScheduledTaskConfigurationSectionHandler.webCache[key] as List<ScheduledTaskConfiguration>;
                if (list != null)
                    return list;
            }
            try
            {
                if (File.Exists(filePath))
                {
                    List<ScheduledTaskConfiguration> list = new List<ScheduledTaskConfiguration>();
                    XmlDocument xmlDocument = ConfigHelper.GetXmlDocument(filePath);
                    if (xmlDocument != null)
                    {
                        XmlNode section = xmlDocument.SelectSingleNode(xpath);
                        if (section != null)
                            list = ScheduledTaskConfigurationSectionHandler.Parse(section);
                    }
                    ScheduledTaskConfigurationSectionHandler.webCache.Add(key, (object)list, new CacheDependency(filePath), DateTime.Now.AddDays(10.0), TimeSpan.Zero, CacheItemPriority.Normal, (CacheItemRemovedCallback)null);
                    return list;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return new List<ScheduledTaskConfiguration>(0);
        }

        public static List<ScheduledTaskConfiguration> Parse(XmlNode section)
        {
            List<ScheduledTaskConfiguration> list = new List<ScheduledTaskConfiguration>();
            foreach (XmlNode xmlNode1 in section.ChildNodes)
            {
                if (xmlNode1.Name == "scheduledTask")
                {
                    ScheduledTaskConfiguration taskConfiguration = new ScheduledTaskConfiguration();
                    foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap)xmlNode1.Attributes)
                    {
                        string name = xmlAttribute.Name;
                        if (name != null)
                        {
                            string str = string.IsInterned(name);
                            if (str == "ScheduledTaskType")
                                taskConfiguration.ScheduledTaskType = xmlAttribute.Value;
                            else if (str == "ThreadSleepSecond")
                                taskConfiguration.ThreadSleepSecond = Convert.ToInt32(xmlAttribute.Value, 10);
                        }
                    }
                    foreach (XmlNode xmlNode2 in xmlNode1.ChildNodes)
                    {
                        if (xmlNode2.Name == "execute")
                        {
                            foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap)xmlNode2.Attributes)
                            {
                                if (xmlAttribute.Name == "type")
                                {
                                    taskConfiguration.Executes.Add(xmlAttribute.Value);
                                    break;
                                }
                            }
                        }
                    }
                    list.Add(taskConfiguration);
                }
            }
            return list;
        }
    }
}
