using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;
using viviapi.Model;

namespace viviapi.BLL
{
    public class templateFactory
    {
        public static List<template> AllTemplate()
        {
            List<template> list1 = HttpRuntime.Cache.Get("TemplateConfiguration") as List<template>;
            if (list1 == null)
            {
                List<template> list2 = new List<template>();
                foreach (string dir in Directory.GetFiles(HttpContext.Current.Server.MapPath("/Template/"), "*.xml", SearchOption.AllDirectories))
                    list2.Add(templateFactory.Get(dir));
                list1 = list2;
                HttpRuntime.Cache.Insert("TemplateConfiguration", (object)list1);
            }
            return list1;
        }

        public static template Get(string dir)
        {
            template template = new template();
            foreach (XmlElement xmlElement in templateFactory.GetConfig(dir).SelectSingleNode("about").ChildNodes)
            {
                template.ID = xmlElement.GetAttribute("ID");
                template.Name = xmlElement.GetAttribute("name");
                template.Author = xmlElement.GetAttribute("author");
                template.Createdate = xmlElement.GetAttribute("createdate");
                template.IsAgent = xmlElement.GetAttribute("isAgent");
                template.Copyright = xmlElement.GetAttribute("copyright");
                template.Photo = template.ID + "/about.jpg";
                template.Bigphoto = template.ID + "/bigabout.jpg";
            }
            return template;
        }

        public static XmlDocument GetConfig(string dir)
        {
            string str = dir;
            if (!File.Exists(str))
                throw new FileNotFoundException(str);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(str);
            return xmlDocument;
        }
    }
}
