using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Settled
{
    public class ManageTrade
    {
        internal const string SQL_TABLE = "V_ManageTrade";
        internal const string SQL_TABLE_FIELDS = "[id]\r\n      ,[userid]\r\n      ,[type]\r\n      ,[billType]\r\n      ,[billNo]\r\n      ,[tradeTime]\r\n      ,[Amt]\r\n      ,[Balance]\r\n      ,[Remark]\r\n      ,[relname]";

        public static Decimal GetManageIncome(int ManageId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
          new SqlParameter("@ManageId", SqlDbType.Int),
          new SqlParameter("@btime", SqlDbType.DateTime, 8),
          new SqlParameter("@etime", SqlDbType.DateTime, 8)
                };
                sqlParameterArray[0].Value = (object)ManageId;
                sqlParameterArray[1].Value = (object)sdate;
                sqlParameterArray[2].Value = (object)edate;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getManageIncome", sqlParameterArray);
                if (obj != DBNull.Value)
                    return Convert.ToDecimal(obj);
                return new Decimal(0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new Decimal(0);
            }
        }

        public static int Add(int manageId, int _type, int _billType, string billNo, DateTime tradeTime, Decimal Amt, string Remark)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[8]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@manageid", SqlDbType.Int, 4),
          new SqlParameter("@type", SqlDbType.TinyInt, 1),
          new SqlParameter("@billType", SqlDbType.TinyInt, 1),
          new SqlParameter("@billNo", SqlDbType.NVarChar, 50),
          new SqlParameter("@tradeTime", SqlDbType.DateTime),
          new SqlParameter("@Amt", SqlDbType.Decimal, 9),
          new SqlParameter("@Remark", SqlDbType.VarChar, 100)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)manageId;
                sqlParameterArray[2].Value = (object)_type;
                sqlParameterArray[3].Value = (object)_billType;
                sqlParameterArray[4].Value = (object)billNo;
                sqlParameterArray[5].Value = (object)tradeTime;
                sqlParameterArray[6].Value = (object)Amt;
                sqlParameterArray[7].Value = (object)Remark;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_managetrade_add", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static Decimal GetSettledAmt(int ManageId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
          new SqlParameter("@ManageId", SqlDbType.Int),
          new SqlParameter("@btime", SqlDbType.DateTime, 8),
          new SqlParameter("@etime", SqlDbType.DateTime, 8)
                };
                sqlParameterArray[0].Value = (object)ManageId;
                sqlParameterArray[1].Value = (object)sdate;
                sqlParameterArray[2].Value = (object)edate;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_Managetrade_get", sqlParameterArray);
                if (obj != DBNull.Value)
                    return Convert.ToDecimal(obj);
                return new Decimal(0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new Decimal(0);
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_ManageTrade";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "userid asc,id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = ManageTrade.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userid]\r\n      ,[type]\r\n      ,[billType]\r\n      ,[billNo]\r\n      ,[tradeTime]\r\n      ,[Amt]\r\n      ,[Balance]\r\n      ,[Remark]\r\n      ,[relname]", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                            stringBuilder.Append(" AND [tradeTime] >= @stime");
                            SqlParameter sqlParameter2 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter2.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter2);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [tradeTime] <= @etime");
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
