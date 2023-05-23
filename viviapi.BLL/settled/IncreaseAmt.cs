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
    public sealed class IncreaseAmt
    {
        internal const string SQL_TABLE = "V_increaseAmt";
        internal const string SQL_TABLE_FIELDS = "[id]\r\n      ,[userId]\r\n      ,[increaseAmt]\r\n      ,[addtime]\r\n      ,[mangeId]\r\n      ,[mangeName]\r\n      ,[status]\r\n      ,[desc],[username],[optype]";

        public static int Add(IncreaseAmtInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[9]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@increaseAmt", SqlDbType.Decimal, 9),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@mangeId", SqlDbType.Int, 4),
          new SqlParameter("@mangeName", SqlDbType.NVarChar, 50),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@desc", SqlDbType.NVarChar, 100),
          new SqlParameter("@optype", SqlDbType.TinyInt)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.userId;
                sqlParameterArray[2].Value = (object)model.increaseAmt;
                sqlParameterArray[3].Value = (object)model.addtime;
                sqlParameterArray[4].Value = (object)model.mangeId;
                sqlParameterArray[5].Value = (object)model.mangeName;
                sqlParameterArray[6].Value = (object)model.status;
                sqlParameterArray[7].Value = (object)model.desc;
                sqlParameterArray[8].Value = (object)model.optype;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_increaseAmt_Insert", sqlParameterArray) > 0)
                    return (int)sqlParameterArray[0].Value;
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static IncreaseAmtInfo GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            IncreaseAmtInfo increaseAmtInfo = new IncreaseAmtInfo();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Proc_increaseAmt_GetModel", sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return (IncreaseAmtInfo)null;
            if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
                increaseAmtInfo.id = int.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
            if (dataSet.Tables[0].Rows[0]["userId"].ToString() != "")
                increaseAmtInfo.userId = new int?(int.Parse(dataSet.Tables[0].Rows[0]["userId"].ToString()));
            if (dataSet.Tables[0].Rows[0]["increaseAmt"].ToString() != "")
                increaseAmtInfo.increaseAmt = new Decimal?(Decimal.Parse(dataSet.Tables[0].Rows[0]["increaseAmt"].ToString()));
            if (dataSet.Tables[0].Rows[0]["addtime"].ToString() != "")
                increaseAmtInfo.addtime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["addtime"].ToString()));
            if (dataSet.Tables[0].Rows[0]["mangeId"].ToString() != "")
                increaseAmtInfo.mangeId = new int?(int.Parse(dataSet.Tables[0].Rows[0]["mangeId"].ToString()));
            increaseAmtInfo.mangeName = dataSet.Tables[0].Rows[0]["mangeName"].ToString();
            if (dataSet.Tables[0].Rows[0]["status"].ToString() != "")
                increaseAmtInfo.status = new int?(int.Parse(dataSet.Tables[0].Rows[0]["status"].ToString()));
            increaseAmtInfo.desc = dataSet.Tables[0].Rows[0]["desc"].ToString();
            return increaseAmtInfo;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_increaseAmt";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "userid asc,id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = IncreaseAmt.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userId]\r\n      ,[increaseAmt]\r\n      ,[addtime]\r\n      ,[mangeId]\r\n      ,[mangeName]\r\n      ,[status]\r\n      ,[desc],[username],[optype]", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
