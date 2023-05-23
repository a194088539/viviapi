using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Cache;
using viviapi.Model.User;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
    public class userHost
    {
        internal const string SQL_TABLE = "V_userhost";
        internal const string SQL_FIELDS = "[id],[userid],[siteip],[sitetype],[hostName],[hostUrl],[status],[desc],[username]";
        public const string CACHE_KEY = "USERHOST_{0}";

        public bool Exists(int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userhost_Exists", sqlParameterArray)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Exists(int userid, string host)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@hostName", SqlDbType.VarChar, 200)
                };
                sqlParameterArray[0].Value = (object)userid;
                sqlParameterArray[1].Value = (object)host;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userhost_Exists2", sqlParameterArray)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public int Add(userHostInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[8]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@siteip", SqlDbType.VarChar, 50),
          new SqlParameter("@sitetype", SqlDbType.TinyInt, 1),
          new SqlParameter("@hostName", SqlDbType.VarChar, 200),
          new SqlParameter("@hostUrl", SqlDbType.VarChar, (int) byte.MaxValue),
          new SqlParameter("@desc", SqlDbType.VarChar, (int) byte.MaxValue),
          new SqlParameter("@status", SqlDbType.TinyInt)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.siteip;
                sqlParameterArray[3].Value = (object)model.sitetype;
                sqlParameterArray[4].Value = (object)model.hostName;
                sqlParameterArray[5].Value = (object)model.hostUrl;
                sqlParameterArray[6].Value = (object)model.desc;
                sqlParameterArray[7].Value = (object)1;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_add", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(userHostInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[8]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@siteip", SqlDbType.VarChar, 50),
          new SqlParameter("@sitetype", SqlDbType.TinyInt, 1),
          new SqlParameter("@hostName", SqlDbType.VarChar, 200),
          new SqlParameter("@hostUrl", SqlDbType.VarChar, (int) byte.MaxValue),
          new SqlParameter("@desc", SqlDbType.VarChar, (int) byte.MaxValue),
          new SqlParameter("@status", SqlDbType.TinyInt)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.siteip;
                sqlParameterArray[3].Value = (object)model.sitetype;
                sqlParameterArray[4].Value = (object)model.hostName;
                sqlParameterArray[5].Value = (object)model.hostUrl;
                sqlParameterArray[6].Value = (object)model.desc;
                sqlParameterArray[7].Value = (object)model.status;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_update", sqlParameterArray) > 0;
                if (flag)
                    this.ClearCache(model.id);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool ChangeStatus(int id, int status)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt)
                };
                sqlParameterArray[0].Value = (object)id;
                sqlParameterArray[1].Value = (object)status;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_ChangeStatus", sqlParameterArray) > 0;
                if (flag)
                    this.ClearCache(id);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userhost_Delete", sqlParameterArray) > 0;
                if (flag)
                    this.ClearCache(id);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public userHostInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return userHost.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userhost_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (userHostInfo)null;
            }
        }

        public userHostInfo GetCacheModel(int id)
        {
            userHostInfo userHostInfo1 = new userHostInfo();
            string str = string.Format("USERHOST_{0}", (object)id);
            userHostInfo userHostInfo2 = (userHostInfo)WebCache.GetCacheService().RetrieveObject(str);
            if (userHostInfo2 == null)
            {
                IDictionary<string, object> parameters = (IDictionary<string, object>)new Dictionary<string, object>();
                parameters.Add("id", (object)id);
                DataBase.AddSqlDependency(str, "V_userhost", "[id],[userid],[siteip],[sitetype],[hostName],[hostUrl],[status],[desc],[username]", "[id]=@id", parameters);
                userHostInfo2 = this.GetModel(id);
                WebCache.GetCacheService().AddObject(str, (object)userHostInfo2);
            }
            return userHostInfo2;
        }

        public static userHostInfo GetModelFromDs(DataSet ds)
        {
            userHostInfo userHostInfo = new userHostInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (userHostInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                userHostInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                userHostInfo.userid = new int?(int.Parse(ds.Tables[0].Rows[0]["userid"].ToString()));
            userHostInfo.siteip = ds.Tables[0].Rows[0]["siteip"].ToString();
            if (ds.Tables[0].Rows[0]["sitetype"].ToString() != "")
                userHostInfo.sitetype = new int?(int.Parse(ds.Tables[0].Rows[0]["sitetype"].ToString()));
            userHostInfo.hostName = ds.Tables[0].Rows[0]["hostName"].ToString();
            userHostInfo.hostUrl = ds.Tables[0].Rows[0]["hostUrl"].ToString();
            userHostInfo.desc = ds.Tables[0].Rows[0]["desc"].ToString();
            userHostInfo.status = !(ds.Tables[0].Rows[0]["status"].ToString() != "") ? userHostStatus.未知 : (userHostStatus)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            return userHostInfo;
        }

        public DataTable GetList(int userId)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int)
                };
                sqlParameterArray[0].Value = (object)userId;
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userhost_GetList", sqlParameterArray).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_userhost";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "userid desc,id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = userHost.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id],[userid],[siteip],[sitetype],[hostName],[hostUrl],[status],[desc],[username]", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return dataSet;
            }
        }

        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder stringBuilder = new StringBuilder(" 1 = 1");
            if (param != null && param.Count > 0)
            {
                for (int index = 0; index < param.Count; ++index)
                {
                    SearchParam searchParam = param[index];
                    switch (searchParam.ParamKey.Trim().ToLower())
                    {
                        case "userid":
                            stringBuilder.Append(" AND [userid] = @userid");
                            SqlParameter sqlParameter1 = new SqlParameter("@userid", SqlDbType.Int);
                            sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter1);
                            break;
                        case "username":
                            stringBuilder.Append(" AND [userName] like @UserName");
                            SqlParameter sqlParameter2 = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                            sqlParameter2.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 20) + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "status":
                            stringBuilder.Append(" AND [status] = @status");
                            SqlParameter sqlParameter3 = new SqlParameter("@status", SqlDbType.TinyInt);
                            sqlParameter3.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [addtime] >= @stime");
                            SqlParameter sqlParameter4 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter4.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter4);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [addtime] <= @etime");
                            SqlParameter sqlParameter5 = new SqlParameter("@etime", SqlDbType.DateTime);
                            sqlParameter5.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter5);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }

        internal void ClearCache(int id)
        {
            WebCache.GetCacheService().RemoveObject(string.Format("USERHOST_{0}", (object)id));
        }
    }
}
