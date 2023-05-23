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
    public class UserPayBankApp
    {
        internal const string SQL_TABLE = "V_userPayAcctChange";
        internal const string SQL_TABLE_FIELDS = "[id]\r\n      ,[userid]\r\n      ,[pmode]\r\n      ,[account],accoutType\r\n      ,[payeeName]\r\n      ,[payeeBank]\r\n      ,[bankProvince]\r\n      ,[bankCity]\r\n      ,[bankAddress]\r\n      ,[status]\r\n      ,[AddTime]\r\n      ,[SureTime]\r\n      ,[SureUser]\r\n      ,[userName]\r\n      ,[relname]";

        public static bool Exists2(int userId)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userId;
                return (int)DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userspaybank_Exists", sqlParameterArray) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Exists3(int userId)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userId;
                return (int)DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userspaybankapp_Exists", sqlParameterArray) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Exists(int userId)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userId;
                return (int)DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userspaybankapp_Exists", sqlParameterArray) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static int Add(UserPayBankAppInfo model)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("insert into userspaybankapp(");
                stringBuilder.Append("userid,accoutType,pmode,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,AddTime,SureTime,SureUser,BankCode,provinceCode,cityCode)");
                stringBuilder.Append(" values (");
                stringBuilder.Append("@userid,@accoutType,@pmode,@account,@payeeName,@payeeBank,@bankProvince,@bankCity,@bankAddress,@status,@AddTime,@SureTime,@SureUser,@BankCode,@provinceCode,@cityCode)");
                stringBuilder.Append(";select @@IDENTITY");
                SqlParameter[] sqlParameterArray = new SqlParameter[16]
                {
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@accoutType", SqlDbType.TinyInt, 1),
          new SqlParameter("@pmode", SqlDbType.TinyInt, 1),
          new SqlParameter("@account", SqlDbType.VarChar, 50),
          new SqlParameter("@payeeName", SqlDbType.VarChar, 50),
          new SqlParameter("@payeeBank", SqlDbType.VarChar, 50),
          new SqlParameter("@bankProvince", SqlDbType.VarChar, 50),
          new SqlParameter("@bankCity", SqlDbType.VarChar, 50),
          new SqlParameter("@bankAddress", SqlDbType.VarChar, 100),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@AddTime", SqlDbType.DateTime),
          new SqlParameter("@SureTime", SqlDbType.DateTime),
          new SqlParameter("@SureUser", SqlDbType.Int, 4),
          new SqlParameter("@BankCode", SqlDbType.VarChar, 50),
          new SqlParameter("@provinceCode", SqlDbType.VarChar, 50),
          new SqlParameter("@cityCode", SqlDbType.VarChar, 50)
                };
                sqlParameterArray[0].Value = (object)model.userid;
                sqlParameterArray[1].Value = (object)model.accoutType;
                sqlParameterArray[2].Value = (object)model.pmode;
                sqlParameterArray[3].Value = (object)model.account;
                sqlParameterArray[4].Value = (object)model.payeeName;
                sqlParameterArray[5].Value = (object)model.payeeBank;
                sqlParameterArray[6].Value = (object)model.bankProvince;
                sqlParameterArray[7].Value = (object)model.bankCity;
                sqlParameterArray[8].Value = (object)model.bankAddress;
                sqlParameterArray[9].Value = (object)model.status;
                sqlParameterArray[10].Value = (object)model.AddTime;
                sqlParameterArray[11].Value = (object)model.SureTime;
                sqlParameterArray[12].Value = (object)model.SureUser;
                sqlParameterArray[13].Value = (object)model.BankCode;
                sqlParameterArray[14].Value = (object)model.provinceCode;
                sqlParameterArray[15].Value = (object)model.cityCode;
                object single = DbHelperSQL.GetSingle(stringBuilder.ToString(), sqlParameterArray);
                if (single == null)
                    return 0;
                return Convert.ToInt32(single);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Update(UserPayBankAppInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[13]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@pmode", SqlDbType.TinyInt, 1),
          new SqlParameter("@account", SqlDbType.VarChar, 50),
          new SqlParameter("@payeeName", SqlDbType.VarChar, 50),
          new SqlParameter("@payeeBank", SqlDbType.VarChar, 50),
          new SqlParameter("@bankProvince", SqlDbType.VarChar, 50),
          new SqlParameter("@bankCity", SqlDbType.VarChar, 50),
          new SqlParameter("@bankAddress", SqlDbType.VarChar, 100),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@AddTime", SqlDbType.DateTime),
          new SqlParameter("@SureTime", SqlDbType.DateTime),
          new SqlParameter("@SureUser", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.pmode;
                sqlParameterArray[3].Value = (object)model.account;
                sqlParameterArray[4].Value = (object)model.payeeName;
                sqlParameterArray[5].Value = (object)model.payeeBank;
                sqlParameterArray[6].Value = (object)model.bankProvince;
                sqlParameterArray[7].Value = (object)model.bankCity;
                sqlParameterArray[8].Value = (object)model.bankAddress;
                sqlParameterArray[9].Value = (object)model.status;
                sqlParameterArray[10].Value = (object)model.AddTime;
                sqlParameterArray[11].Value = (object)model.SureTime;
                sqlParameterArray[12].Value = (object)model.SureUser;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userspaybankapp_Update", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Check(UserPayBankAppInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@SureTime", SqlDbType.DateTime),
          new SqlParameter("@SureUser", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.status;
                sqlParameterArray[2].Value = (object)model.SureTime;
                sqlParameterArray[3].Value = (object)model.SureUser;
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userspaybankapp_Check", sqlParameterArray) <= 0)
                    return false;
                UserFactory.ClearCache(model.userid);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_userspaybankapp_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static UserPayBankAppInfo GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            UserPayBankAppInfo userPayBankAppInfo = new UserPayBankAppInfo();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userspaybankapp_GetModel", sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return (UserPayBankAppInfo)null;
            if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
                userPayBankAppInfo.id = int.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
            if (dataSet.Tables[0].Rows[0]["userid"].ToString() != "")
                userPayBankAppInfo.userid = int.Parse(dataSet.Tables[0].Rows[0]["userid"].ToString());
            if (dataSet.Tables[0].Rows[0]["pmode"].ToString() != "")
                userPayBankAppInfo.pmode = int.Parse(dataSet.Tables[0].Rows[0]["pmode"].ToString());
            userPayBankAppInfo.account = dataSet.Tables[0].Rows[0]["account"].ToString();
            userPayBankAppInfo.payeeName = dataSet.Tables[0].Rows[0]["payeeName"].ToString();
            userPayBankAppInfo.payeeBank = dataSet.Tables[0].Rows[0]["payeeBank"].ToString();
            userPayBankAppInfo.bankProvince = dataSet.Tables[0].Rows[0]["bankProvince"].ToString();
            userPayBankAppInfo.bankCity = dataSet.Tables[0].Rows[0]["bankCity"].ToString();
            userPayBankAppInfo.bankAddress = dataSet.Tables[0].Rows[0]["bankAddress"].ToString();
            if (dataSet.Tables[0].Rows[0]["status"].ToString() != "")
                userPayBankAppInfo.status = (AcctChangeEnum)int.Parse(dataSet.Tables[0].Rows[0]["status"].ToString());
            if (dataSet.Tables[0].Rows[0]["AddTime"].ToString() != "")
                userPayBankAppInfo.AddTime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["AddTime"].ToString()));
            if (dataSet.Tables[0].Rows[0]["SureTime"].ToString() != "")
                userPayBankAppInfo.SureTime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["SureTime"].ToString()));
            if (dataSet.Tables[0].Rows[0]["SureUser"].ToString() != "")
                userPayBankAppInfo.SureUser = new int?(int.Parse(dataSet.Tables[0].Rows[0]["SureUser"].ToString()));
            return userPayBankAppInfo;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_userPayAcctChange";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = UserPayBankApp.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userid]\r\n      ,[pmode]\r\n      ,[account],accoutType\r\n      ,[payeeName]\r\n      ,[payeeBank]\r\n      ,[bankProvince]\r\n      ,[bankCity]\r\n      ,[bankAddress]\r\n      ,[status]\r\n      ,[AddTime]\r\n      ,[SureTime]\r\n      ,[SureUser]\r\n      ,[userName]\r\n      ,[relname]", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                        case "status":
                            stringBuilder.Append(" AND [status] = @status");
                            SqlParameter sqlParameter2 = new SqlParameter("@status", SqlDbType.TinyInt);
                            sqlParameter2.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter2);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [AddTime] >= @stime");
                            SqlParameter sqlParameter3 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter3.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [AddTime] <= @etime");
                            SqlParameter sqlParameter4 = new SqlParameter("@etime", SqlDbType.DateTime);
                            sqlParameter4.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter4);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
