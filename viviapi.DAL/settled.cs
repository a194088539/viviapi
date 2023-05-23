using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL
{
    public class settled
    {
        internal const string SQL_TABLE = "V_Settled";
        internal const string FIELDS = "[UserName]\r\n      ,[PayeeName]\r\n      ,[Account]\r\n      ,[id]\r\n      ,[userid]\r\n      ,[amount]\r\n      ,[status]\r\n      ,[addTime]\r\n      ,[tax]\r\n      ,ISNULL([charges],0) as charges,[PayTime],[userid],[PayeeBank],[apptype],[required],[settmode],[settles],[tranapi],Payeeaddress\r\n      ,[amount]-isnull([charges],0)-isnull([tax],0) realpay";

        public int Add(SettledInfo model)
        {
            try
            {
                SqlParameter sqlParameter = DataBase.MakeOutParam("@id", SqlDbType.Int, 4);
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settled_add", sqlParameter, DataBase.MakeInParam("@userid", SqlDbType.Int, 4, (object)model.userid), DataBase.MakeInParam("@amount", SqlDbType.Money, 8, (object)model.amount), DataBase.MakeInParam("@status", SqlDbType.Int, 4, (object)model.status), DataBase.MakeInParam("@addtime", SqlDbType.DateTime, 8, (object)model.addtime), DataBase.MakeInParam("@paytime", SqlDbType.DateTime, 8, (object)model.paytime), DataBase.MakeInParam("@tax", SqlDbType.Money, 8, (object)model.tax), DataBase.MakeInParam("@charges", SqlDbType.Money, 8, (object)model.charges), DataBase.MakeInParam("@settmode", SqlDbType.TinyInt, 1, (object)model.settmode)) == 1)
                    return (int)sqlParameter.Value;
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public int Apply(SettledInfo model)
        {
            try
            {
                SqlParameter sqlParameter = DataBase.MakeOutParam("@id", SqlDbType.Int, 4);
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settled_apply", sqlParameter, DataBase.MakeInParam("@userid", SqlDbType.Int, 4, (object)model.userid), DataBase.MakeInParam("@amount", SqlDbType.Decimal, 9, (object)model.amount), DataBase.MakeInParam("@addtime", SqlDbType.DateTime, 8, (object)model.addtime), DataBase.MakeInParam("@apptype", SqlDbType.Int, 4, (object)model.AppType), DataBase.MakeInParam("@required", SqlDbType.DateTime, 8, (object)model.addtime), DataBase.MakeInParam("@Paytype", SqlDbType.TinyInt, 1, (object)model.Paytype), DataBase.MakeInParam("@PayeeBank", SqlDbType.VarChar, 50, (object)model.PayeeBank), DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, (object)model.payeeName), DataBase.MakeInParam("@Account", SqlDbType.VarChar, 50, (object)model.Account), DataBase.MakeInParam("@BankAddress", SqlDbType.VarChar, 100, (object)model.Payeeaddress), DataBase.MakeInParam("@settmode", SqlDbType.TinyInt, 1, (object)model.settmode), DataBase.MakeInParam("@charges", SqlDbType.Decimal, 9, (object)model.charges), DataBase.MakeInParam("@tranapi", SqlDbType.Int, 4, (object)model.suppid)) > 0)
                    return (int)sqlParameter.Value;
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Cancel(int id)
        {
            try
            {
                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_settled_cancel", new SqlParameter[1]
        {
          DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object) id)
        }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Update(SettledInfo model)
        {
            try
            {
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settled_update", DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object)model.id), DataBase.MakeInParam("@userid", SqlDbType.Int, 4, (object)model.userid), DataBase.MakeInParam("@amount", SqlDbType.Money, 8, (object)model.amount), DataBase.MakeInParam("@status", SqlDbType.Int, 4, (object)model.status), DataBase.MakeInParam("@addtime", SqlDbType.DateTime, 8, (object)model.addtime), DataBase.MakeInParam("@paytime", SqlDbType.DateTime, 8, (object)model.paytime), DataBase.MakeInParam("@tax", SqlDbType.Money, 8, (object)model.tax), DataBase.MakeInParam("@charges", SqlDbType.Money, 8, (object)model.charges), DataBase.MakeInParam("@tranapi", SqlDbType.Int, 4, (object)model.suppid)) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public SettledInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
        {
          DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object) id)
        };
                SettledInfo settledInfo = (SettledInfo)null;
                using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_settled_GetModel", sqlParameterArray))
                {
                    if (dataReader.Read())
                        settledInfo = settled.ReaderBind(dataReader);
                }
                return settledInfo;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (SettledInfo)null;
            }
        }

        public bool Audit(int id, int status)
        {
            try
            {
                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_settled_Audit", DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object)id), DataBase.MakeInParam("@status", SqlDbType.Int, 4, (object)status), DataBase.MakeInParam("@paytime", SqlDbType.DateTime, 8, (object)DateTime.Now)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool BatchPass(string ids, string batchNo, out DataTable withdrawListByApi)
        {
            withdrawListByApi = (DataTable)null;
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          DataBase.MakeInParam("@ids", SqlDbType.NVarChar, 1000, (object) ids),
          DataBase.MakeInParam("@batchNo", SqlDbType.VarChar, 30, (object) batchNo),
          DataBase.MakeOutParam("@result", SqlDbType.Bit, 1)
        };
                DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settled_batchpass", sqlParameterArray);
                if (dataSet != null && dataSet.Tables.Count > 0)
                    withdrawListByApi = dataSet.Tables[0];
                return Convert.ToBoolean((object)sqlParameterArray[2]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool BatchSettle(string ids)
        {
            try
            {
                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_settled_batchsettle", new SqlParameter[1]
        {
          DataBase.MakeInParam("@ids", SqlDbType.NVarChar, 1000, (object) ids)
        }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool AllPass(string batchNo)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
        {
          DataBase.MakeInParam("@batchNo", SqlDbType.VarChar, 30, (object) batchNo),
          DataBase.MakeOutParam("@result", SqlDbType.Bit, 1)
        };
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settled_allpass", sqlParameterArray);
                return Convert.ToBoolean(sqlParameterArray[1].Value);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public DataSet GetListWithdrawByApi(string batchNo)
        {
            try
            {
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settled_getlist_WithdrawByApi", new SqlParameter[1]
        {
          DataBase.MakeInParam("@batchNo", SqlDbType.VarChar, 30, (object) batchNo)
        });
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public bool GetListByBatchNo(string batchNo)
        {
            try
            {
                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_settled_allpass", new SqlParameter[1]
        {
          DataBase.MakeInParam("@batchNo", SqlDbType.VarChar, 30, (object) batchNo)
        }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Allfails()
        {
            try
            {
                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_settled_fails", (SqlParameter[])null));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Delete(DateTime etime)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("delete from settled ");
                stringBuilder.Append(" where status = 8 and addtime < @etime");
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
        {
          new SqlParameter("@etime", SqlDbType.DateTime, 8)
        };
                sqlParameterArray[0].Value = (object)etime;
                return DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_Settled";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "addTime desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = settled.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[UserName]\r\n      ,[PayeeName]\r\n      ,[Account]\r\n      ,[id]\r\n      ,[userid]\r\n      ,[amount]\r\n      ,[status]\r\n      ,[addTime]\r\n      ,[tax]\r\n      ,ISNULL([charges],0) as charges,[PayTime],[userid],[PayeeBank],[apptype],[required],[settmode],[settles],[tranapi],Payeeaddress\r\n      ,[amount]-isnull([charges],0)-isnull([tax],0) realpay", tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect ISNULL(sum(amount),0) from V_Settled where " + wheres, paramList.ToArray());
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
                        case "id":
                            stringBuilder.Append(" AND [id] = @id");
                            SqlParameter sqlParameter1 = new SqlParameter("@id", SqlDbType.Int);
                            sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter1);
                            break;
                        case "status":
                            stringBuilder.Append(" AND [status] = @status");
                            SqlParameter sqlParameter2 = new SqlParameter("@status", SqlDbType.Int);
                            sqlParameter2.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter2);
                            break;
                        case "tranapi":
                            stringBuilder.Append(" AND [tranapi] = @tranapi");
                            SqlParameter sqlParameter3 = new SqlParameter("@tranapi", SqlDbType.Int);
                            sqlParameter3.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "userid":
                            stringBuilder.Append(" AND [userid] = @userid");
                            SqlParameter sqlParameter4 = new SqlParameter("@userid", SqlDbType.Int);
                            sqlParameter4.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter4);
                            break;
                        case "settmode":
                            stringBuilder.Append(" AND [settmode] = @settmode");
                            SqlParameter sqlParameter5 = new SqlParameter("@settmode", SqlDbType.TinyInt);
                            sqlParameter5.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter5);
                            break;
                        case "username":
                            stringBuilder.Append(" AND [UserName] like @UserName");
                            SqlParameter sqlParameter6 = new SqlParameter("@UserName", SqlDbType.VarChar);
                            sqlParameter6.Value = (object)("%" + searchParam.ParamValue + "%");
                            paramList.Add(sqlParameter6);
                            break;
                        case "account":
                            stringBuilder.Append(" AND [account] like @account");
                            SqlParameter sqlParameter7 = new SqlParameter("@account", SqlDbType.VarChar);
                            sqlParameter7.Value = (object)((string)searchParam.ParamValue + (object)"%");
                            paramList.Add(sqlParameter7);
                            break;
                        case "payeebank":
                            stringBuilder.Append(" AND [PayeeBank] like @PayeeBank");
                            SqlParameter sqlParameter8 = new SqlParameter("@PayeeBank", SqlDbType.VarChar);
                            sqlParameter8.Value = (object)((string)searchParam.ParamValue + (object)"%");
                            paramList.Add(sqlParameter8);
                            break;
                        case "payeename":
                            stringBuilder.Append(" AND [payeeName] like @payeeName");
                            SqlParameter sqlParameter9 = new SqlParameter("@payeeName", SqlDbType.VarChar);
                            sqlParameter9.Value = (object)((string)searchParam.ParamValue + (object)"%");
                            paramList.Add(sqlParameter9);
                            break;
                        case "begindate":
                            stringBuilder.Append(" AND [paytime] >= @beginpaytime");
                            SqlParameter sqlParameter10 = new SqlParameter("@beginpaytime", SqlDbType.DateTime);
                            sqlParameter10.Value = (object)(DateTime)searchParam.ParamValue;
                            paramList.Add(sqlParameter10);
                            break;
                        case "enddate":
                            stringBuilder.Append(" AND [paytime] <= @endpaytime");
                            SqlParameter sqlParameter11 = new SqlParameter("@endpaytime", SqlDbType.DateTime);
                            sqlParameter11.Value = (object)(DateTime)searchParam.ParamValue;
                            paramList.Add(sqlParameter11);
                            break;
                        case "saddtime":
                            stringBuilder.Append(" AND [addTime] >= @saddtime");
                            SqlParameter sqlParameter12 = new SqlParameter("@saddtime", SqlDbType.DateTime);
                            sqlParameter12.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter12);
                            break;
                        case "eaddtime":
                            stringBuilder.Append(" AND [addTime] <= @eaddtime");
                            SqlParameter sqlParameter13 = new SqlParameter("@eaddtime", SqlDbType.DateTime);
                            sqlParameter13.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter13);
                            break;
                        case "all":
                            stringBuilder.Append(" AND ([userid] = @id or [id] = @id)");
                            SqlParameter sqlParameter14 = new SqlParameter("@id", SqlDbType.Int);
                            sqlParameter14.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter14);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public int Pay(SettledInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[7]
        {
          DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object) model.id),
          DataBase.MakeInParam("@status", SqlDbType.Int, 4, (object) model.status),
          DataBase.MakeInParam("@paytime", SqlDbType.DateTime, 8, (object) model.paytime),
          DataBase.MakeInParam("@tax", SqlDbType.Money, 8, (object) model.tax),
          DataBase.MakeInParam("@charges", SqlDbType.Money, 8, (object) model.charges),
          DataBase.MakeInParam("@tranapi", SqlDbType.Int, 4, (object) model.suppid),
          DataBase.MakeOutParam("@result", SqlDbType.TinyInt, 1)
        };
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settled_pay", sqlParameterArray);
                return Convert.ToInt32(sqlParameterArray[6].Value);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public bool AllSettle()
        {
            try
            {
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settled_allsettle", (SqlParameter[])null);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool AutoSettled(Decimal balance)
        {
            try
            {
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_Sys_AutoSettled", new SqlParameter[1]
        {
          DataBase.MakeInParam("@balance", SqlDbType.Decimal, 9, (object) balance)
        });
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public int GetUserDaySettledTimes(int userid, string day)
        {
            try
            {
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_settled_userdaytimes", new SqlParameter[2]
        {
          DataBase.MakeInParam("@userid", SqlDbType.Int, 4, (object) userid),
          DataBase.MakeInParam("@day", SqlDbType.VarChar, 20, (object) day)
        }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public Decimal GetUserDaySettledAmt(int userid, string day)
        {
            try
            {
                return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_settled_userdayAmt", new SqlParameter[2]
        {
          DataBase.MakeInParam("@userid", SqlDbType.Int, 4, (object) userid),
          DataBase.MakeInParam("@day", SqlDbType.VarChar, 20, (object) day)
        }));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new Decimal(0);
            }
        }

        public DataTable Export(string ids)
        {
            try
            {
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settled_Export", new SqlParameter[1]
        {
          DataBase.MakeInParam("@ids", SqlDbType.NVarChar, 1000, (object) ids)
        }).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public Decimal GetPayDayMoney(int uid)
        {
            return Decimal.Parse(DataBase.ExecuteScalarToStr(CommandType.Text, "SELECT ISNULL(SUM([Amount]*[Pay_Price]),0) FROM [User_Pay_Order] where Status = 2 and datediff(day,CompleteTime,getdate())=0 and UserId=" + uid.ToString()));
        }

        public Decimal Getpayingmoney(int uid)
        {
            return Decimal.Parse(DataBase.ExecuteScalarToStr(CommandType.Text, "SELECT ISNULL(SUM([Money]),0) FROM [settled] WHERE Status IN(0,1) AND [Uid]=" + uid.ToString()));
        }

        public static SettledInfo ReaderBind(SqlDataReader dataReader)
        {
            SettledInfo settledInfo = new SettledInfo();
            object obj1 = dataReader["id"];
            if (obj1 != null && obj1 != DBNull.Value)
                settledInfo.id = (int)obj1;
            object obj2 = dataReader["userid"];
            if (obj2 != null && obj2 != DBNull.Value)
                settledInfo.userid = (int)obj2;
            object obj3 = dataReader["amount"];
            if (obj3 != null && obj3 != DBNull.Value)
                settledInfo.amount = (Decimal)obj3;
            object obj4 = dataReader["status"];
            if (obj4 != null && obj4 != DBNull.Value)
                settledInfo.status = (SettledStatus)obj4;
            object obj5 = dataReader["addtime"];
            if (obj5 != null && obj5 != DBNull.Value)
                settledInfo.addtime = (DateTime)obj5;
            object obj6 = dataReader["paytime"];
            if (obj6 != null && obj6 != DBNull.Value)
                settledInfo.paytime = (DateTime)obj6;
            object obj7 = dataReader["tax"];
            if (obj7 != null && obj7 != DBNull.Value)
                settledInfo.tax = new Decimal?((Decimal)obj7);
            object obj8 = dataReader["charges"];
            if (obj8 != null && obj8 != DBNull.Value)
                settledInfo.charges = new Decimal?((Decimal)obj8);
            object obj9 = dataReader["payeeName"];
            if (obj9 != null && obj9 != DBNull.Value)
                settledInfo.payeeName = (string)obj9;
            object obj10 = dataReader["PayeeBank"];
            if (obj10 != null && obj10 != DBNull.Value)
                settledInfo.PayeeBank = (string)obj10;
            object obj11 = dataReader["Payeeaddress"];
            if (obj11 != null && obj11 != DBNull.Value)
                settledInfo.Payeeaddress = (string)obj11;
            object obj12 = dataReader["Account"];
            if (obj12 != null && obj12 != DBNull.Value)
                settledInfo.Account = (string)obj12;
            return settledInfo;
        }

        public SettledInfo DataRowToModel(DataRow row)
        {
            SettledInfo settledInfo = new SettledInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                    settledInfo.id = int.Parse(row["id"].ToString());
                if (row["settmode"] != null && row["settmode"].ToString() != "")
                    settledInfo.settmode = (SettledmodeEnum)int.Parse(row["settmode"].ToString());
                if (row["userid"] != null && row["userid"].ToString() != "")
                    settledInfo.userid = int.Parse(row["userid"].ToString());
                if (row["amount"] != null && row["amount"].ToString() != "")
                    settledInfo.amount = Decimal.Parse(row["amount"].ToString());
                if (row["status"] != null && row["status"].ToString() != "")
                    settledInfo.status = (SettledStatus)int.Parse(row["status"].ToString());
                if (row["tranapi"] != null && row["tranapi"].ToString() != "")
                    settledInfo.suppid = int.Parse(row["tranapi"].ToString());
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                    settledInfo.addtime = DateTime.Parse(row["addtime"].ToString());
                if (row["required"] != null && row["required"].ToString() != "")
                    settledInfo.required = DateTime.Parse(row["required"].ToString());
                if (row["paytime"] != null && row["paytime"].ToString() != "")
                    settledInfo.paytime = DateTime.Parse(row["paytime"].ToString());
                if (row["tax"] != null && row["tax"].ToString() != "")
                    settledInfo.tax = new Decimal?(Decimal.Parse(row["tax"].ToString()));
                if (row["charges"] != null && row["charges"].ToString() != "")
                    settledInfo.charges = new Decimal?(Decimal.Parse(row["charges"].ToString()));
                if (row["apptype"] != null && row["apptype"].ToString() != "")
                    settledInfo.AppType = (AppTypeEnum)int.Parse(row["apptype"].ToString());
                if (row["Paytype"] != null && row["Paytype"].ToString() != "")
                    settledInfo.Paytype = int.Parse(row["Paytype"].ToString());
                if (row["PayeeBank"] != null)
                    settledInfo.PayeeBank = row["PayeeBank"].ToString();
                if (row["payeeName"] != null)
                    settledInfo.payeeName = row["payeeName"].ToString();
                if (row["Payeeaddress"] != null)
                    settledInfo.Payeeaddress = row["Payeeaddress"].ToString();
                if (row["account"] != null)
                    settledInfo.Account = row["account"].ToString();
            }
            return settledInfo;
        }
    }
}
