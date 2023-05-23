using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.User;
using viviLib.ExceptionHandling;

namespace viviapi.BLL
{
    public class PromotionUserFactory
    {
        public static int Insert(PromotionUserInfo promUser)
        {
            try
            {
                Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_promotionUser_insert", DataBase.MakeInParam("@RegId", SqlDbType.Int, 4, (object)promUser.RegId), DataBase.MakeInParam("@PID", SqlDbType.Int, 4, (object)promUser.PID), DataBase.MakeInParam("@Prices", SqlDbType.Money, 8, (object)promUser.Prices), DataBase.MakeInParam("@PTime", SqlDbType.DateTime, 8, (object)promUser.PromTime), DataBase.MakeInParam("@PStatus", SqlDbType.Int, 4, (object)promUser.PromStatus)));
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Delete(int RegId)
        {
            try
            {
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_promotionUser_delete", new SqlParameter[1]
                {
          DataBase.MakeInParam("@RegId", SqlDbType.Int, 4, (object) RegId)
                }) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static int GetUserNum(int userId)
        {
            try
            {
                string commandText = "select count(0) from PromotionUser(nolock) where PID=@PID";
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@PID", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userId;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.Text, commandText, sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static PromotionUserInfo GetModel(int regId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  top 1 PromId,RegId,PID,Prices from PromotionUser ");
            stringBuilder.Append(" where RegId=@RegId ");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@RegId", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)regId;
            PromotionUserInfo promotionUserInfo = new PromotionUserInfo();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return promotionUserInfo;
            if (dataSet.Tables[0].Rows[0]["PromId"].ToString() != "")
                promotionUserInfo.PromId = int.Parse(dataSet.Tables[0].Rows[0]["PromId"].ToString());
            if (dataSet.Tables[0].Rows[0]["RegId"].ToString() != "")
                promotionUserInfo.RegId = int.Parse(dataSet.Tables[0].Rows[0]["RegId"].ToString());
            if (dataSet.Tables[0].Rows[0]["PID"].ToString() != "")
                promotionUserInfo.PID = int.Parse(dataSet.Tables[0].Rows[0]["PID"].ToString());
            if (dataSet.Tables[0].Rows[0]["Prices"].ToString() != "")
                promotionUserInfo.Prices = Decimal.Parse(dataSet.Tables[0].Rows[0]["Prices"].ToString());
            return promotionUserInfo;
        }

        public static DataTable Get_Pro_MoneyList(int pid, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (pid > 0)
                list.Add(DataBase.MakeInParam("@pid", SqlDbType.Int, 4, (object)pid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Get_Pro_MoneyList", list.ToArray());
            DataTable dataTable = (DataTable)null;
            if (dataSet.Tables.Count != 0)
                dataTable = dataSet.Tables[0];
            return dataTable;
        }

        public static DataTable Get_Pro_PayList(int gmid, int pid, int _paytype, int _sid, int pageindex, int pagesize, DateTime stime, DateTime etime, int status, out int total, out double money, out double money2, out double money3)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (gmid > 0)
                list.Add(DataBase.MakeInParam("@gmid", SqlDbType.Int, 4, (object)gmid));
            if (_paytype > 0)
                list.Add(DataBase.MakeInParam("@paytype", SqlDbType.Int, 4, (object)_paytype));
            if (_sid > 0)
                list.Add(DataBase.MakeInParam("@sid", SqlDbType.Int, 4, (object)_sid));
            if (pid > 0)
                list.Add(DataBase.MakeInParam("@pid", SqlDbType.Int, 4, (object)pid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@Status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)pagesize));
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Decimal, 8);
            list.Add(sqlParameter1);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@totalmoney2", SqlDbType.Decimal, 8);
            list.Add(sqlParameter2);
            SqlParameter sqlParameter3 = DataBase.MakeOutParam("@totalmoney3", SqlDbType.Decimal, 8);
            list.Add(sqlParameter3);
            SqlParameter sqlParameter4 = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            list.Add(sqlParameter4);
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Get_Pro_PayList", list.ToArray());
            DataTable dataTable = (DataTable)null;
            money = 0.0;
            total = 0;
            money2 = 0.0;
            money3 = 0.0;
            if (dataSet.Tables.Count != 0)
            {
                dataTable = dataSet.Tables[0];
                money = double.Parse(sqlParameter1.Value.ToString());
                money2 = double.Parse(sqlParameter2.Value.ToString());
                money3 = double.Parse(sqlParameter3.Value.ToString());
                total = int.Parse(sqlParameter4.Value.ToString());
            }
            return dataTable;
        }

        public static DataTable Get_Pro_PayList(int gmid, int pid, int _paytype, int _sid, int pageindex, int pagesize, DateTime stime, DateTime etime, int status, out int total, out double money, out double money2, out double money3, out double money4)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (gmid > 0)
                list.Add(DataBase.MakeInParam("@gmid", SqlDbType.Int, 4, (object)gmid));
            if (_paytype > 0)
                list.Add(DataBase.MakeInParam("@paytype", SqlDbType.Int, 4, (object)_paytype));
            if (_sid > 0)
                list.Add(DataBase.MakeInParam("@sid", SqlDbType.Int, 4, (object)_sid));
            if (pid > 0)
                list.Add(DataBase.MakeInParam("@pid", SqlDbType.Int, 4, (object)pid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@Status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)pagesize));
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Decimal, 8);
            list.Add(sqlParameter1);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@totalmoney2", SqlDbType.Decimal, 8);
            list.Add(sqlParameter2);
            SqlParameter sqlParameter3 = DataBase.MakeOutParam("@totalmoney3", SqlDbType.Decimal, 8);
            list.Add(sqlParameter3);
            SqlParameter sqlParameter4 = DataBase.MakeOutParam("@totalmoney4", SqlDbType.Decimal, 8);
            list.Add(sqlParameter4);
            SqlParameter sqlParameter5 = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            list.Add(sqlParameter5);
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Get_Pro_PayList_pingtai", list.ToArray());
            DataTable dataTable = (DataTable)null;
            money = 0.0;
            total = 0;
            money2 = 0.0;
            money3 = 0.0;
            money4 = 0.0;
            if (dataSet.Tables.Count != 0)
            {
                dataTable = dataSet.Tables[0];
                money = double.Parse(sqlParameter1.Value.ToString());
                money2 = double.Parse(sqlParameter2.Value.ToString());
                money3 = double.Parse(sqlParameter3.Value.ToString());
                money4 = double.Parse(sqlParameter4.Value.ToString());
                total = int.Parse(sqlParameter5.Value.ToString());
            }
            return dataTable;
        }

        public static DataTable Get_Pro_PayList(int gmid, int pid, int _paytype, int _sid, string username, ulong okxrorderid, string outorderid, int pageindex, int pagesize, DateTime stime, DateTime etime, int status, out int total, out double money, out double money2, out double money3, out double money4)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (gmid > 0)
                list.Add(DataBase.MakeInParam("@gmid", SqlDbType.Int, 4, (object)gmid));
            if (_paytype > 0)
                list.Add(DataBase.MakeInParam("@paytype", SqlDbType.Int, 4, (object)_paytype));
            if (_sid > 0)
                list.Add(DataBase.MakeInParam("@sid", SqlDbType.Int, 4, (object)_sid));
            if (pid > 0)
                list.Add(DataBase.MakeInParam("@pid", SqlDbType.Int, 4, (object)pid));
            if (username != "")
                list.Add(DataBase.MakeInParam("@UserName", SqlDbType.VarChar, 50, (object)username));
            if ((long)okxrorderid != 0L)
                list.Add(DataBase.MakeInParam("@OkxrOrderId", SqlDbType.BigInt, 8, (object)okxrorderid));
            if (outorderid != "")
                list.Add(DataBase.MakeInParam("@OutOrderId", SqlDbType.VarChar, 50, (object)outorderid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@Status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)pagesize));
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Decimal, 8);
            list.Add(sqlParameter1);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@totalmoney2", SqlDbType.Decimal, 8);
            list.Add(sqlParameter2);
            SqlParameter sqlParameter3 = DataBase.MakeOutParam("@totalmoney3", SqlDbType.Decimal, 8);
            list.Add(sqlParameter3);
            SqlParameter sqlParameter4 = DataBase.MakeOutParam("@totalmoney4", SqlDbType.Decimal, 8);
            list.Add(sqlParameter4);
            SqlParameter sqlParameter5 = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            list.Add(sqlParameter5);
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Get_Pro_PayList_pingtai", list.ToArray());
            DataTable dataTable = (DataTable)null;
            money = 0.0;
            total = 0;
            money2 = 0.0;
            money3 = 0.0;
            money4 = 0.0;
            if (dataSet.Tables.Count != 0)
            {
                dataTable = dataSet.Tables[0];
                money = double.Parse(sqlParameter1.Value.ToString());
                money2 = double.Parse(sqlParameter2.Value.ToString());
                money3 = double.Parse(sqlParameter3.Value.ToString());
                money4 = double.Parse(sqlParameter4.Value.ToString());
                total = int.Parse(sqlParameter5.Value.ToString());
            }
            return dataTable;
        }

        public static DataTable Get_Pro_UserList(int userId, string username, int pid, int Status, int pageindex, out int total)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter sqlParameter = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            list.Add(sqlParameter);
            if (userId > 0)
                list.Add(DataBase.MakeInParam("@uid", SqlDbType.Int, 4, (object)userId));
            if (pid > 0)
                list.Add(DataBase.MakeInParam("@pid", SqlDbType.Int, 4, (object)pid));
            if (username != "")
                list.Add(DataBase.MakeInParam("@username", SqlDbType.VarChar, 50, (object)username));
            if (Status != 999)
                list.Add(DataBase.MakeInParam("@status", SqlDbType.Int, 4, (object)Status));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)20));
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Get_Pro_UserList", list.ToArray()).Tables[0];
            total = (int)sqlParameter.Value;
            return dataTable;
        }

        public static DataTable getparouserlistall(int pid)
        {
            return DataBase.ExecuteDataset(CommandType.Text, "select * from PromotionUser where PID = " + (object)pid).Tables[0];
        }
    }
}
