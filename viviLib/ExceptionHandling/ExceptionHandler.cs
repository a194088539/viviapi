using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using viviLib.Configuration;
using viviLib.Logging;
using viviLib.Web;

namespace viviLib.ExceptionHandling
{
    public sealed class ExceptionHandler
    {
        private static readonly string[] IgnoredProperties = new string[5]
        {
      "Source",
      "Message",
      "HelpLink",
      "InnerException",
      "StackTrace"
        };

        private ExceptionHandler()
        {
        }

        private static string GetFieldInfo(FieldInfo field, object fieldValue)
        {
            return string.Format("{0} : {1}", (object)field.Name, fieldValue);
        }

        private static string GetPropertyInfo(PropertyInfo propertyInfo, object propertyValue)
        {
            return string.Format("{0} : {1}", (object)propertyInfo.Name, propertyValue);
        }

        private static string GetReflectionInfo(Exception e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Type type = e.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.CanRead && Array.IndexOf<string>(ExceptionHandler.IgnoredProperties, propertyInfo.Name) == -1)
                {
                    object propertyValue = propertyInfo.GetValue((object)e, (object[])null);
                    stringBuilder.Append(ExceptionHandler.GetPropertyInfo(propertyInfo, propertyValue));
                    stringBuilder.Append("\r\n");
                }
            }
            foreach (FieldInfo field in fields)
            {
                object fieldValue = field.GetValue((object)e);
                stringBuilder.Append(ExceptionHandler.GetFieldInfo(field, fieldValue));
                stringBuilder.Append("\r\n");
            }
            return stringBuilder.ToString();
        }

        public static void HandleException(Exception ex)
        {
            if (ex == null || !LogSetting.ExceptionLogEnabled)
                return;
            try
            {
                if (WebBase.Context == null || File.Exists(WebBase.Server.MapPath(WebBase.Request.Path)) || (!(ex is HttpException) || ex.InnerException == null) || !(ex.InnerException is FileNotFoundException))
                {
                    LogHelper.Write(LogSetting.ExceptionLogFilePath(DateTime.Today), string.Format("Path              = {0}\r\nTime              = {1}\r\nClientIP          = {2}\r\nType              = {3}\r\nMessage           = {4}\r\nSource            = {5}\r\nHelpLink          = {6}\r\nReflectionInfo    = {7}\r\nStackTrace        = {8}", WebBase.Context != null ? (object)WebBase.Request.RawUrl : (object)string.Empty, (object)string.Format("{0:yyyy-MM-dd HH:mm:ss}", (object)DateTime.Now), WebBase.Context != null ? (object)ServerVariables.TrueIP : (object)string.Empty, (object)ex.GetType().AssemblyQualifiedName, (object)ex.Message, (object)ex.Source, (object)ex.HelpLink, (object)ExceptionHandler.GetReflectionInfo(ex).Replace("\n", "\n" + new string(' ', 24)), (object)ex.StackTrace.Replace("\n", "\n" + new string(' ', 24))));
                    if (ex.InnerException != null)
                        ExceptionHandler.HandleException(ex.InnerException);
                }
            }
            catch
            {
            }
        }
    }
}
