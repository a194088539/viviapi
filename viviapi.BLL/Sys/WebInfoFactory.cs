using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using viviapi.BLL.Sys;
using viviapi.Cache;
using viviapi.Model;
using viviLib.ExceptionHandling;

namespace viviapi.BLL
{
    public class WebInfoFactory
    {
        public static string WEBINFO_DOMAIN_CACHEKEY = Constant.Cache_Mark + "WEBINFOCONFIG_{0}";
        internal static string SQL_TABLE = "webinfo";
        internal static string SQL_TABLE_FIELD = "[id],[templateID],[name],[domain],[jsqq],[kfqq],[phone],[footer],[code],[logopath],[payurl]";

        public static WebInfo CurrentWebInfo
        {
            get
            {
                return WebInfoFactory.GetCacheWebInfoByDomain(HttpContext.Current.Request.Url.Host);
            }
        }

        public static void Add(WebInfo model)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("insert into webinfo(");
                stringBuilder.Append("[id],[templateID],[name],[domain],[jsqq],[kfqq],[phone],[footer],[code],[logopath],[payurl])");
                stringBuilder.Append(" values (");
                stringBuilder.Append("@id,@templateID,@name,@domain,@jsqq,@kfqq,@phone,@footer,@code,@logopath,@payurl)");
                SqlParameter[] sqlParameterArray = new SqlParameter[11]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@templateID", SqlDbType.VarChar, 50),
          new SqlParameter("@name", SqlDbType.VarChar, 50),
          new SqlParameter("@domain", SqlDbType.VarChar, 50),
          new SqlParameter("@jsqq", SqlDbType.VarChar, 50),
          new SqlParameter("@kfqq", SqlDbType.VarChar, 50),
          new SqlParameter("@phone", SqlDbType.VarChar, 50),
          new SqlParameter("@footer", SqlDbType.VarChar, 50),
          new SqlParameter("@code", SqlDbType.VarChar, 50),
          new SqlParameter("@logopath", SqlDbType.VarChar, 50),
          new SqlParameter("@payurl", SqlDbType.VarChar, 50)
                };
                sqlParameterArray[0].Value = (object)model.ID;
                sqlParameterArray[1].Value = (object)model.TemplateId;
                sqlParameterArray[2].Value = (object)model.Name;
                sqlParameterArray[3].Value = (object)model.Domain;
                sqlParameterArray[4].Value = (object)model.Jsqq;
                sqlParameterArray[5].Value = (object)model.Kfqq;
                sqlParameterArray[6].Value = (object)model.Phone;
                sqlParameterArray[7].Value = (object)model.Footer;
                sqlParameterArray[8].Value = (object)model.Code;
                sqlParameterArray[9].Value = (object)model.LogoPath;
                sqlParameterArray[9].Value = (object)model.PayUrl;
                DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public static bool Update(WebInfo _webinfo)
        {
            try
            {
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_webinfo_Update", DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object)_webinfo.ID), DataBase.MakeInParam("@templateID", SqlDbType.VarChar, 20, (object)_webinfo.TemplateId), DataBase.MakeInParam("@name", SqlDbType.VarChar, 100, (object)_webinfo.Name), DataBase.MakeInParam("@domain", SqlDbType.VarChar, 500, (object)_webinfo.Domain), DataBase.MakeInParam("@jsqq", SqlDbType.VarChar, 300, (object)_webinfo.Jsqq), DataBase.MakeInParam("@kfqq", SqlDbType.VarChar, 300, (object)_webinfo.Kfqq), DataBase.MakeInParam("@phone", SqlDbType.VarChar, 50, (object)_webinfo.Phone), DataBase.MakeInParam("@footer", SqlDbType.VarChar, 500, (object)_webinfo.Footer), DataBase.MakeInParam("@code", SqlDbType.VarChar, 500, (object)_webinfo.Code), DataBase.MakeInParam("@logopath", SqlDbType.VarChar, 500, (object)_webinfo.LogoPath), DataBase.MakeInParam("@payurl", SqlDbType.VarChar, 500, (object)_webinfo.PayUrl), DataBase.MakeInParam("@apibankname", SqlDbType.VarChar, 100, (object)_webinfo.apibankname), DataBase.MakeInParam("@apibankversion", SqlDbType.VarChar, 20, (object)_webinfo.apibankversion), DataBase.MakeInParam("@apicardname", SqlDbType.VarChar, 100, (object)_webinfo.apicardname), DataBase.MakeInParam("@apicardversion", SqlDbType.VarChar, 20, (object)_webinfo.apicardversion)) > 0;
                if (flag)
                    WebInfoFactory.ClearCache(HttpContext.Current.Request.Url.Host);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static WebInfo GetWebInfoById(int id)
        {
            try
            {
                WebInfo webInfo = new WebInfo();
                return WebInfoFactory.GetObjectFromDR(DataBase.ExecuteReader(CommandType.Text, "SELECT [id]\r\n      ,[templateID]\r\n      ,[name]\r\n      ,[domain]\r\n      ,[jsqq]\r\n      ,[kfqq]\r\n      ,[phone]\r\n      ,[footer]\r\n      ,[code]\r\n      ,[logopath]\r\n      ,[payurl]\r\n      ,[apibankname]\r\n      ,[apibankversion]\r\n      ,[apicardname]\r\n      ,[apicardversion]\r\n  FROM [webinfo] WHERE [id]=@id", new SqlParameter[1]
                {
          DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object) id)
                }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (WebInfo)null;
            }
        }

        public static WebInfo GetCacheWebInfoByDomain(string domain)
        {
            string str = string.Format(WebInfoFactory.WEBINFO_DOMAIN_CACHEKEY, (object)domain);
            WebInfo webInfo1 = new WebInfo();
            WebInfo webInfo2 = (WebInfo)WebCache.GetCacheService().RetrieveObject(str);
            if (webInfo2 == null)
            {
                IDictionary<string, object> parameters = (IDictionary<string, object>)new Dictionary<string, object>();
                parameters.Add("domain", (object)domain);
                DataBase.AddSqlDependency(str, WebInfoFactory.SQL_TABLE, WebInfoFactory.SQL_TABLE_FIELD, "[domain]=@domain", parameters);
                webInfo2 = WebInfoFactory.GetWebInfoByDomain(domain);
                if (webInfo2 == null)
                    return (WebInfo)null;
                WebCache.GetCacheService().AddObject(str, (object)webInfo2);
            }
            return webInfo2;
        }

        public static WebInfo GetWebInfoByDomain(string domain)
        {
            try
            {
                return WebInfoFactory.GetObjectFromDR(DataBase.ExecuteReader(CommandType.Text, "DECLARE @TempID int\r\nSELECT @TempID = [id] FROM [webinfo] WHERE [domain]=@domain\r\nIF(@TempID IS NULL)\r\nSELECT @TempID = 1\r\n\r\nSELECT [id]\r\n      ,[templateID]\r\n      ,[name]\r\n      ,[domain]\r\n      ,[jsqq]\r\n      ,[kfqq]\r\n      ,[phone]\r\n      ,[footer]\r\n      ,[code]\r\n      ,[logopath]\r\n      ,[payurl] ,[apibankname]\r\n      ,[apibankversion]\r\n      ,[apicardname]\r\n      ,[apicardversion]\r\nFROM [dbo].[webinfo] WHERE [id] = @TempID ", new SqlParameter[1]
                {
          DataBase.MakeInParam("@domain", SqlDbType.VarChar, 50, (object) domain)
                }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (WebInfo)null;
            }
        }

        public static WebInfo GetWebInfoByDomain(string domain, int id)
        {
            try
            {
                return WebInfoFactory.GetObjectFromDR(DataBase.ExecuteReader(CommandType.Text, "DECLARE @TempID int\r\nSELECT @TempID = [id] FROM [webinfo] WHERE [domain]=@domain\r\nIF(@TempID IS NULL)\r\nSELECT @TempID = @ID\r\n\r\nSELECT [id]\r\n      ,[templateID]\r\n      ,[name]\r\n      ,[domain]\r\n      ,[jsqq]\r\n      ,[kfqq]\r\n      ,[phone]\r\n      ,[footer]\r\n      ,[code]\r\n      ,[logopath]\r\n      ,[payurl] ,[apibankname]\r\n      ,[apibankversion]\r\n      ,[apicardname]\r\n      ,[apicardversion]\r\nFROM [dbo].[webinfo] WHERE [id] = @TempID ", new SqlParameter[2]
                {
          DataBase.MakeInParam("@domain", SqlDbType.VarChar, 50, (object) domain),
          DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object) id)
                }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (WebInfo)null;
            }
        }

        private static WebInfo GetObjectFromDR(SqlDataReader dr)
        {
            if (dr == null)
                return (WebInfo)null;
            WebInfo webInfo = new WebInfo();
            if (dr.Read())
            {
                webInfo.ID = (int)dr["id"];
                webInfo.TemplateId = dr["templateID"].ToString();
                webInfo.Name = dr["name"].ToString();
                webInfo.Domain = dr["domain"].ToString();
                webInfo.Jsqq = dr["jsqq"].ToString();
                webInfo.Kfqq = dr["kfqq"].ToString();
                webInfo.Phone = dr["phone"].ToString();
                webInfo.Footer = dr["footer"].ToString();
                webInfo.Code = dr["code"].ToString();
                webInfo.LogoPath = dr["logopath"].ToString();
                webInfo.PayUrl = dr["payurl"].ToString();
                webInfo.apibankname = dr["apibankname"].ToString();
                webInfo.apibankversion = dr["apibankversion"].ToString();
                webInfo.apicardname = dr["apicardname"].ToString();
                webInfo.apicardversion = dr["apicardversion"].ToString();
            }
            dr.Close();
            return webInfo;
        }

        public static string GetAgent_Payrate_Setconfig()
        {
            return Convert.ToString(DataBase.ExecuteScalar(CommandType.Text, "select top 1 isnull(agentpayratesetconfig,'') from webinfo where id = 1"));
        }

        public static bool SetAgent_Payrate_Setconfig(string config)
        {
            return DataBase.ExecuteNonQuery(CommandType.Text, "update webinfo set agentpayratesetconfig=@agentpayratesetconfig where id = 1", new SqlParameter[1]
            {
        DataBase.MakeInParam("@agentpayratesetconfig", SqlDbType.VarChar, 4000, (object) config)
            }) > 0;
        }

        public static DataTable GetList(string where)
        {
            try
            {
                return DataBase.ExecuteDataset(CommandType.Text, "SELECT [id]\r\n      ,[templateID]\r\n      ,[name]\r\n      ,[domain]\r\n      ,[jsqq]\r\n      ,[kfqq]\r\n      ,[phone]\r\n      ,[footer]\r\n      ,[code]\r\n      ,[logopath]\r\n      ,[payurl] ,[apibankname]\r\n      ,[apibankversion]\r\n      ,[apicardname]\r\n      ,[apicardversion]\r\n  FROM [webinfo] ").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        private static void ClearCache(string domain)
        {
            WebCache.GetCacheService().RemoveObject(string.Format(WebInfoFactory.WEBINFO_DOMAIN_CACHEKEY, (object)domain));
        }
    }
}
