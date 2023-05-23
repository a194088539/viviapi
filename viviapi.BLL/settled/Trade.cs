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
    public class Trade
    {
        internal const string SQL_TABLE = "V_Trade";
        internal const string SQL_TABLE_FIELDS = "[id]\r\n      ,[userid]\r\n      ,[type]\r\n      ,[billType]\r\n      ,[billNo]\r\n      ,[tradeTime]\r\n      ,[Amt]\r\n      ,[Balance]\r\n      ,[Remark]\r\n      ,[username]";

        public static Decimal GetUserIncome(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
          new SqlParameter("@userid", SqlDbType.Int),
          new SqlParameter("@btime", SqlDbType.VarChar, 10),
          new SqlParameter("@etime", SqlDbType.VarChar, 10)
                };
                sqlParameterArray[0].Value = (object)userId;
                sqlParameterArray[1].Value = (object)sdate.ToString("yyyy-MM-dd");
                sqlParameterArray[2].Value = (object)edate.ToString("yyyy-MM-dd");
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getuserIncome", sqlParameterArray);
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

        public static Decimal GetUserIncome(int classid, int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
                {
          new SqlParameter("@userid", SqlDbType.Int),
          new SqlParameter("@classid", SqlDbType.TinyInt),
          new SqlParameter("@btime", SqlDbType.DateTime, 8),
          new SqlParameter("@etime", SqlDbType.DateTime, 8)
                };
                sqlParameterArray[0].Value = (object)userId;
                sqlParameterArray[1].Value = (object)classid;
                sqlParameterArray[2].Value = (object)sdate;
                sqlParameterArray[3].Value = (object)edate;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getuserIncomex", sqlParameterArray);
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

        public static Decimal GetUserIncome2(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
          new SqlParameter("@userid", SqlDbType.Int),
          new SqlParameter("@btime", SqlDbType.DateTime, 8),
          new SqlParameter("@etime", SqlDbType.DateTime, 8)
                };
                sqlParameterArray[0].Value = (object)userId;
                sqlParameterArray[1].Value = (object)sdate;
                sqlParameterArray[2].Value = (object)edate;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_trade_getuserIncome2", sqlParameterArray);
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

        public static Decimal GetUserOrderAmt(int userId, DateTime sdate, DateTime edate)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
          new SqlParameter("@userid", SqlDbType.Int),
          new SqlParameter("@btime", SqlDbType.VarChar, 10),
          new SqlParameter("@etime", SqlDbType.VarChar, 10)
                };
                sqlParameterArray[0].Value = (object)userId;
                sqlParameterArray[1].Value = (object)sdate.ToString("yyyy-MM-dd");
                sqlParameterArray[2].Value = (object)edate.ToString("yyyy-MM-dd");
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_order_getuserOrdAmt", sqlParameterArray);
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
                string tables = "V_Trade";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "userid asc,id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = Trade.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userid]\r\n      ,[type]\r\n      ,[billType]\r\n      ,[billNo]\r\n      ,[tradeTime]\r\n      ,[Amt]\r\n      ,[Balance]\r\n      ,[Remark]\r\n      ,[username]", tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect \r\n sum(case when [type] = 1 then Amt else 0 end) income\r\n,sum(case when [billType] = 2 then Amt else 0 end) agentincome\r\n,sum(case when [type] = 0 then 0-Amt else 0 end) expenditure from v_trade where " + wheres, paramList.ToArray());
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
                        case "billtype":
                            stringBuilder.Append(" AND [billtype] = @billtype");
                            SqlParameter sqlParameter4 = new SqlParameter("@billtype", SqlDbType.Int);
                            sqlParameter4.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter4);
                            break;
                        case "supplier":
                            stringBuilder.Append(" AND exists(select 0 from ordercard with(nolock) where v_trade.billNo = ordercard.orderid and ordercard.supplierID = @supplier)");
                            SqlParameter sqlParameter5 = new SqlParameter("@supplier", SqlDbType.Int);
                            sqlParameter5.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter5);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public static Decimal GetNdaysIncome(int classid, int userid, int days)
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime sdate = today.AddDays((double)(-days + 1));
                today = DateTime.Today;
                DateTime edate = today.AddDays(1.0);
                return Trade.GetUserIncome(classid, userid, sdate, edate);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new Decimal(0);
            }
        }
    }
}
