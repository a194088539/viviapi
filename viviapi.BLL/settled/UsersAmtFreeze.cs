using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.Settled;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Settled
{
    public class UsersAmtFreeze
    {
        internal const string SQL_TABLE = "v_usersAmtFreeze";
        internal const string SQL_TABLE_FIELDS = "[id]\r\n      ,[userid]\r\n      ,[freezeAmt]\r\n      ,[addtime]\r\n      ,[manageId]\r\n      ,[status]\r\n      ,[checktime]\r\n      ,[why]\r\n      ,[unfreezemode],username,full_name";

        public static bool Freeze(UsersAmtFreezeInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[8]
                {
          new SqlParameter("@result", SqlDbType.Bit),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@Freeze", SqlDbType.Decimal, 9),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@manageId", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@why", SqlDbType.VarChar, 50),
          new SqlParameter("@unfreezemode", SqlDbType.TinyInt, 1)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.freezeAmt;
                sqlParameterArray[3].Value = (object)model.addtime;
                sqlParameterArray[4].Value = (object)model.manageId;
                sqlParameterArray[5].Value = (object)model.status;
                sqlParameterArray[6].Value = (object)model.why;
                sqlParameterArray[7].Value = (object)model.unfreezemode;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersAmt_Freeze", sqlParameterArray) > 0)
                    return (bool)sqlParameterArray[0].Value;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool unFreeze(int id, AmtunFreezeMode mode)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
                {
          new SqlParameter("@result", SqlDbType.Bit),
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@checktime", SqlDbType.DateTime),
          new SqlParameter("@unfreezemode", SqlDbType.TinyInt, 1)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)id;
                sqlParameterArray[2].Value = (object)DateTime.Now;
                sqlParameterArray[3].Value = (object)mode;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersAmt_unFreeze", sqlParameterArray) > 0)
                    return (bool)sqlParameterArray[0].Value;
                return false;
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
                string tables = "v_usersAmtFreeze";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = UsersAmtFreeze.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userid]\r\n      ,[freezeAmt]\r\n      ,[addtime]\r\n      ,[manageId]\r\n      ,[status]\r\n      ,[checktime]\r\n      ,[why]\r\n      ,[unfreezemode],username,full_name", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                        case "stime":
                            stringBuilder.Append(" AND [addtime] >= @stime");
                            SqlParameter sqlParameter2 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter2.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter2);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [addtime] <= @etime");
                            SqlParameter sqlParameter3 = new SqlParameter("@etime", SqlDbType.DateTime);
                            sqlParameter3.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
