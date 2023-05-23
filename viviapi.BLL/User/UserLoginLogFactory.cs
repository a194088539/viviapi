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
    public class UserLoginLogFactory
    {
        internal const string SQL_TABLE = "V_usersLoginLog";
        internal const string FIELD_NEWS = "[id]\r\n      ,[type]\r\n      ,[userID]\r\n      ,[lastIP]\r\n      ,[address]\r\n      ,[remark]\r\n      ,[lastTime]\r\n      ,[sessionId]\r\n      ,[userName],[payeeName]";

        public static int Add(UserLoginLog logEntity)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("insert into usersLoginLog(");
                stringBuilder.Append("type,userID,lastIP,address,remark,lastTime)");
                stringBuilder.Append(" values (");
                stringBuilder.Append("@type,@userID,@lastIP,@address,@remark,@lastTime)");
                SqlParameter[] sqlParameterArray = new SqlParameter[6]
                {
          new SqlParameter("@type", (object) logEntity.type),
          new SqlParameter("@userID", (object) logEntity.userID),
          new SqlParameter("@lastIP", (object) logEntity.lastIP),
          new SqlParameter("@address", (object) logEntity.address),
          new SqlParameter("@remark", (object) logEntity.remark),
          new SqlParameter("@lastTime", (object) logEntity.lastTime)
                };
                return DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Del(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersLoginLog_del", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_usersLoginLog";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "lastTime desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = UserLoginLogFactory.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[type]\r\n      ,[userID]\r\n      ,[lastIP]\r\n      ,[address]\r\n      ,[remark]\r\n      ,[lastTime]\r\n      ,[sessionId]\r\n      ,[userName],[payeeName]", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                            stringBuilder.Append(" AND [userName] like @userName");
                            SqlParameter sqlParameter2 = new SqlParameter("@userName", SqlDbType.VarChar, 20);
                            sqlParameter2.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 100) + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "starttime":
                            stringBuilder.Append(" AND [lastTime] > @starttime");
                            SqlParameter sqlParameter3 = new SqlParameter("@starttime", SqlDbType.DateTime);
                            sqlParameter3.Value = (object)Convert.ToDateTime(searchParam.ParamValue);
                            paramList.Add(sqlParameter3);
                            break;
                        case "endtime":
                            stringBuilder.Append(" AND [lastTime] < @endtime");
                            SqlParameter sqlParameter4 = new SqlParameter("@endtime", SqlDbType.DateTime);
                            sqlParameter4.Value = (object)Convert.ToDateTime(searchParam.ParamValue);
                            paramList.Add(sqlParameter4);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
