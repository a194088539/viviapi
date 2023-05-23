using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model;

namespace viviapi.BLL
{
    public class CPS
    {
        public static int Add(FillMoneyInfo fillmoneyinfo)
        {
            SqlParameter sqlParameter = DataBase.MakeOutParam("@ID", SqlDbType.Int, 4);
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "FillMoney_ADD", sqlParameter, DataBase.MakeInParam("@UserId", SqlDbType.Int, 4, (object)fillmoneyinfo.UserId), DataBase.MakeInParam("@Money", SqlDbType.Money, 8, (object)fillmoneyinfo.Money), DataBase.MakeInParam("@Status", SqlDbType.TinyInt, 1, (object)fillmoneyinfo.Status), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 4, (object)fillmoneyinfo.AddTime)) == 1)
                return (int)sqlParameter.Value;
            return 0;
        }

        public static DataTable GetList(int uid, int cid, int pageindex, out int total, out double countmoney, DateTime stime, DateTime etime)
        {
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("total", SqlDbType.Int, 4);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Money, 8);
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "CPS_GetList", sqlParameter1, sqlParameter2, DataBase.MakeInParam("@cid", SqlDbType.Int, 4, (object)cid), DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object)0), DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)uid), DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime), DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime), DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex), DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)40)).Tables[0];
            total = (int)sqlParameter1.Value;
            countmoney = double.Parse(sqlParameter2.Value.ToString());
            return dataTable;
        }

        public static DataTable GetList(int uid, int cid, int status, int pageindex, out int total, out double countmoney, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("total", SqlDbType.Int, 4);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Money, 8);
            list.Add(sqlParameter1);
            list.Add(sqlParameter2);
            if (uid != 0)
                list.Add(DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)uid));
            if (cid != 0)
                list.Add(DataBase.MakeInParam("@Cid", SqlDbType.Int, 4, (object)cid));
            if (status != 999)
                list.Add(DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)40));
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "CPS_GetList", list.ToArray()).Tables[0];
            total = (int)sqlParameter1.Value;
            countmoney = double.Parse(sqlParameter2.Value.ToString());
            return dataTable;
        }

        public static DataTable GetPayList(int status, int pageindex, out int total, out double countmoney, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("total", SqlDbType.Int, 4);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Money, 8);
            if (status != 999)
                list.Add(DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(sqlParameter1);
            list.Add(sqlParameter2);
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)40));
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Pay_GetList", list.ToArray()).Tables[0];
            total = (int)sqlParameter1.Value;
            countmoney = double.Parse(sqlParameter2.Value.ToString());
            return dataTable;
        }

        public static DataTable GetPayList(int uid, int status, int pageindex, out int total, out double countmoney, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("total", SqlDbType.Int, 4);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Money, 8);
            if (status != 999)
                list.Add(DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(sqlParameter1);
            list.Add(sqlParameter2);
            list.Add(DataBase.MakeInParam("@uid", SqlDbType.Int, 4, (object)uid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)40));
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "Pay_GetList", list.ToArray()).Tables[0];
            total = (int)sqlParameter1.Value;
            countmoney = double.Parse(sqlParameter2.Value.ToString());
            return dataTable;
        }

        public static int GetUserPayTimes(int userid)
        {
            return int.Parse(DataBase.ExecuteScalarToStr(CommandType.Text, "SELECT ISNULL(COUNT(*),0) FROM [RegUser] WHERE UserId=" + (object)userid));
        }
    }
}
