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
    public class UsersAmt
    {
        internal const string SQL_TABLE = "usersAmt";
        internal const string SQL_TABLE_FIELD = "[id]\r\n      ,[userId]\r\n      ,[Integral]\r\n      ,[Freeze]\r\n      ,[balance]\r\n      ,[payment]\r\n      ,[unpayment],[Freeze],(ISNULL([balance],0)-ISNULL([unpayment],0)-ISNULL([Freeze],0)) as enableAmt";

        public static UsersAmtInfo GetModel(int userId)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@userId", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)userId;
            UsersAmtInfo usersAmtInfo = new UsersAmtInfo();
            return UsersAmt.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersAmt_GetModel", sqlParameterArray));
        }

        public static UsersAmtInfo GetModelFromDs(DataSet ds)
        {
            UsersAmtInfo usersAmtInfo = new UsersAmtInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (UsersAmtInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                usersAmtInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                usersAmtInfo.userId = int.Parse(ds.Tables[0].Rows[0]["userId"].ToString());
            if (ds.Tables[0].Rows[0]["Integral"].ToString() != "")
                usersAmtInfo.Integral = new int?(int.Parse(ds.Tables[0].Rows[0]["Integral"].ToString()));
            if (ds.Tables[0].Rows[0]["balance"].ToString() != "")
                usersAmtInfo.balance = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["balance"].ToString()));
            if (ds.Tables[0].Rows[0]["payment"].ToString() != "")
                usersAmtInfo.payment = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["payment"].ToString()));
            if (ds.Tables[0].Rows[0]["unpayment"].ToString() != "")
                usersAmtInfo.unpayment = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["unpayment"].ToString()));
            if (ds.Tables[0].Rows[0]["Freeze"].ToString() != "")
                usersAmtInfo.Freeze = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["Freeze"].ToString()));
            if (ds.Tables[0].Rows[0]["enableAmt"].ToString() != "")
                usersAmtInfo.enableAmt = Decimal.Parse(ds.Tables[0].Rows[0]["enableAmt"].ToString());
            return usersAmtInfo;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "usersAmt";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = UsersAmt.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userId]\r\n      ,[Integral]\r\n      ,[Freeze]\r\n      ,[balance]\r\n      ,[payment]\r\n      ,[unpayment],[Freeze],(ISNULL([balance],0)-ISNULL([unpayment],0)-ISNULL([Freeze],0)) as enableAmt", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                    if (searchParam.CmpOperator == "=")
                    {
                        switch (searchParam.ParamKey.Trim().ToLower())
                        {
                            case "userid":
                                stringBuilder.Append(" AND [userid] = @userid");
                                SqlParameter sqlParameter1 = new SqlParameter("@userid", SqlDbType.Int);
                                sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter1);
                                continue;
                            default:
                                continue;
                        }
                    }
                    else
                    {
                        switch (searchParam.ParamKey.Trim().ToLower())
                        {
                            case "balance":
                                stringBuilder.AppendFormat(" AND [balance] {0} @balance", (object)searchParam.CmpOperator);
                                SqlParameter sqlParameter2 = new SqlParameter("@balance", SqlDbType.Decimal);
                                sqlParameter2.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter2);
                                break;
                        }
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
