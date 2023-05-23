using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using viviapi.Model;

namespace viviapi.BLL
{
    public class WebSiteFactory
    {
        public static bool AddSite(WebSiteInfo _websiteinfo)
        {
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "WebSite_Add", DataBase.MakeInParam("@SiteName", SqlDbType.VarChar, 100, (object)_websiteinfo.SiteName), DataBase.MakeInParam("@Domain", SqlDbType.VarChar, 100, (object)_websiteinfo.Domain), DataBase.MakeInParam("@Description", SqlDbType.VarChar, 500, (object)_websiteinfo.Description), DataBase.MakeInParam("@SiteType", SqlDbType.Int, 4, (object)_websiteinfo.SiteType), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)_websiteinfo.AddTime), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, (object)_websiteinfo.Status), DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)_websiteinfo.Uid)) > 0;
        }

        public static List<WebSiteInfo> GetListArray(int uid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ID,SiteName,Domain,Description,SiteType,AddTime,Status,Uid ");
            stringBuilder.Append(" FROM WebSite WHERE Uid=" + (object)uid);
            List<WebSiteInfo> list = new List<WebSiteInfo>();
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.Text, stringBuilder.ToString()))
            {
                while (dataReader.Read())
                    list.Add(WebSiteFactory.ReaderBind(dataReader));
            }
            return list;
        }

        public static WebSiteInfo GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object) id)
            };
            WebSiteInfo webSiteInfo = (WebSiteInfo)null;
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.StoredProcedure, "UP_WebSite_GetModel", sqlParameterArray))
            {
                if (dataReader.Read())
                    webSiteInfo = WebSiteFactory.ReaderBind(dataReader);
            }
            return webSiteInfo;
        }

        public static DataTable GetMySiteList(int uid)
        {
            return DataBase.ExecuteDataset(CommandType.Text, "SELECT * FROM [WebSite] WHERE [Uid]=" + (object)uid).Tables[0];
        }

        public static WebSiteInfo GetWebSiteInfoByDomain(string domain)
        {
            WebSiteInfo webSiteInfo = new WebSiteInfo();
            if (HttpContext.Current.Cache[domain] != null)
                return (WebSiteInfo)HttpContext.Current.Cache.Get(domain);
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM [WebSite] WHERE Status=" + (object)1 + " AND [Domain]='" + domain + "'");
            if (sqlDataReader.Read())
            {
                webSiteInfo.ID = (int)sqlDataReader["ID"];
                webSiteInfo.SiteName = sqlDataReader["SiteName"].ToString();
                webSiteInfo.Domain = sqlDataReader["Domain"].ToString();
                webSiteInfo.Description = sqlDataReader["Description"].ToString();
                webSiteInfo.SiteType = (int)sqlDataReader["SiteType"];
                webSiteInfo.AddTime = DateTime.Parse(sqlDataReader["AddTime"].ToString());
                webSiteInfo.Status = (int)sqlDataReader["Status"];
                webSiteInfo.Uid = (int)sqlDataReader["Uid"];
            }
            sqlDataReader.Close();
            sqlDataReader.Dispose();
            HttpContext.Current.Cache.Insert(domain, (object)webSiteInfo);
            return webSiteInfo;
        }

        public static WebSiteInfo GetWebSiteInfoById(int id)
        {
            WebSiteInfo webSiteInfo = new WebSiteInfo();
            if (HttpContext.Current.Cache["websiteinfo" + id.ToString()] != null)
                return (WebSiteInfo)HttpContext.Current.Cache.Get("websiteinfo" + id.ToString());
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM [WebSite] WHERE [ID]=" + (object)id);
            if (sqlDataReader.Read())
            {
                webSiteInfo.ID = (int)sqlDataReader["ID"];
                webSiteInfo.SiteName = sqlDataReader["SiteName"].ToString();
                webSiteInfo.Domain = sqlDataReader["Domain"].ToString();
                webSiteInfo.Description = sqlDataReader["Description"].ToString();
                webSiteInfo.SiteType = (int)sqlDataReader["SiteType"];
                webSiteInfo.AddTime = DateTime.Parse(sqlDataReader["AddTime"].ToString());
                webSiteInfo.Status = (int)sqlDataReader["Status"];
                webSiteInfo.Uid = (int)sqlDataReader["Uid"];
            }
            sqlDataReader.Close();
            HttpContext.Current.Cache.Insert("websiteinfo" + id.ToString(), (object)webSiteInfo);
            return webSiteInfo;
        }

        public static WebSiteInfo ReaderBind(SqlDataReader dataReader)
        {
            WebSiteInfo webSiteInfo = new WebSiteInfo();
            object obj1 = dataReader["ID"];
            if (obj1 != null && obj1 != DBNull.Value)
                webSiteInfo.ID = (int)obj1;
            webSiteInfo.SiteName = dataReader["SiteName"].ToString();
            webSiteInfo.Domain = dataReader["Domain"].ToString();
            webSiteInfo.Description = dataReader["Description"].ToString();
            object obj2 = dataReader["SiteType"];
            if (obj2 != null && obj2 != DBNull.Value)
                webSiteInfo.SiteType = (int)obj2;
            object obj3 = dataReader["AddTime"];
            if (obj3 != null && obj3 != DBNull.Value)
                webSiteInfo.AddTime = (DateTime)obj3;
            object obj4 = dataReader["Status"];
            if (obj4 != null && obj4 != DBNull.Value)
                webSiteInfo.Status = (int)obj4;
            object obj5 = dataReader["Uid"];
            if (obj5 != null && obj5 != DBNull.Value)
                webSiteInfo.Uid = (int)obj5;
            return webSiteInfo;
        }

        public static bool UpdateWebSite(WebSiteInfo _websiteinfo)
        {
            HttpContext.Current.Cache.Remove("websiteinfo" + _websiteinfo.ID.ToString());
            HttpContext.Current.Cache.Remove(_websiteinfo.Domain);
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "WebSite_Update", DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object)_websiteinfo.ID), DataBase.MakeInParam("@SiteName", SqlDbType.VarChar, 100, (object)_websiteinfo.SiteName), DataBase.MakeInParam("@Domain", SqlDbType.VarChar, 100, (object)_websiteinfo.Domain), DataBase.MakeInParam("@Description", SqlDbType.VarChar, 500, (object)_websiteinfo.Description), DataBase.MakeInParam("@SiteType", SqlDbType.Int, 4, (object)_websiteinfo.SiteType), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)_websiteinfo.AddTime), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, (object)_websiteinfo.Status), DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)_websiteinfo.Uid)) > 0;
        }
    }
}
