using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.User;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
    public class UserAccessTime
    {
        internal const string SQL_TABLE = "V_userhost";
        internal const string SQL_FIELDS = "[id],[userid],[siteip],[sitetype],[hostName],[hostUrl],[status],[desc],[username]";

        public static bool Add(UserAccessTimeInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@lastAccesstime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)model.userid;
                sqlParameterArray[1].Value = (object)model.lastAccesstime;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usertime_add", sqlParameterArray) > 0;
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
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usertime_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static UserAccessTimeInfo GetModel(int userId)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userId;
                return UserAccessTime.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usertime_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new UserAccessTimeInfo();
            }
        }

        public static UserAccessTimeInfo GetModelFromDs(DataSet ds)
        {
            UserAccessTimeInfo userAccessTimeInfo = new UserAccessTimeInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (UserAccessTimeInfo)null;
            if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                userAccessTimeInfo.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
            if (ds.Tables[0].Rows[0]["lastAccesstime"].ToString() != "")
                userAccessTimeInfo.lastAccesstime = DateTime.Parse(ds.Tables[0].Rows[0]["lastAccesstime"].ToString());
            return userAccessTimeInfo;
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
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usertime_GetList", sqlParameterArray).Tables[0];
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
                string wheres = UserAccessTime.BuilderWhere(searchParams, paramList);
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
    }
}
