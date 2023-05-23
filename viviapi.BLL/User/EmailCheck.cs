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
    public class EmailCheck
    {
        internal const string SQL_TABLE = "V_usersIdImage";
        internal const string SQL_FIELDS = "[id]\r\n      ,[userId]\r\n      ,[ptype]\r\n      ,[filesize]\r\n      ,[ptype1]\r\n      ,[filesize1]\r\n      ,[status]\r\n      ,[why]\r\n      ,[admin]\r\n      ,[checktime]\r\n      ,[addtime]\r\n      ,[userName],[payeeName],[account],[IdCard]";

        public bool Exists(int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_useremailcheck_Exists", sqlParameterArray)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public int Add(EmailCheckInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[8]
                {
          new SqlParameter("@typeid", SqlDbType.TinyInt, 1),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@email", SqlDbType.NVarChar, 50),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@checktime", SqlDbType.DateTime),
          new SqlParameter("@Expired", SqlDbType.DateTime),
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)model.typeid;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.email;
                sqlParameterArray[3].Value = (object)model.addtime;
                sqlParameterArray[4].Value = (object)model.status;
                sqlParameterArray[5].Value = (object)model.checktime;
                sqlParameterArray[6].Value = (object)model.Expired;
                sqlParameterArray[7].Direction = ParameterDirection.Output;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_useremailcheck_add", sqlParameterArray);
                return (int)sqlParameterArray[7].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(EmailCheckInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[6]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@typeid", SqlDbType.TinyInt, 1),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@email", SqlDbType.NVarChar, 50),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@checktime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.typeid;
                sqlParameterArray[2].Value = (object)model.userid;
                sqlParameterArray[3].Value = (object)model.email;
                sqlParameterArray[4].Value = (object)model.status;
                sqlParameterArray[5].Value = (object)model.checktime;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_useremailcheck_update", sqlParameterArray) > 0;
                if (flag && model.status == EmailCheckStatus.已审核)
                    UserFactory.ClearCache(model.userid);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_useremailcheck_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public EmailCheckInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return EmailCheck.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_useremailcheck_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (EmailCheckInfo)null;
            }
        }

        public EmailCheckInfo GetModelByUser(int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userid", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                return EmailCheck.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_useremailcheck_GetByUser", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (EmailCheckInfo)null;
            }
        }

        public static EmailCheckInfo GetModelFromDs(DataSet ds)
        {
            EmailCheckInfo emailCheckInfo = new EmailCheckInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (EmailCheckInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                emailCheckInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["typeid"].ToString() != "")
                emailCheckInfo.typeid = (EmailCheckType)int.Parse(ds.Tables[0].Rows[0]["typeid"].ToString());
            if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                emailCheckInfo.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
            emailCheckInfo.email = ds.Tables[0].Rows[0]["email"].ToString();
            if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                emailCheckInfo.addtime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString()));
            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                emailCheckInfo.status = (EmailCheckStatus)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            if (ds.Tables[0].Rows[0]["checktime"].ToString() != "")
                emailCheckInfo.checktime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["checktime"].ToString()));
            if (ds.Tables[0].Rows[0]["Expired"].ToString() != "")
                emailCheckInfo.Expired = DateTime.Parse(ds.Tables[0].Rows[0]["Expired"].ToString());
            return emailCheckInfo;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_usersIdImage";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = EmailCheck.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userId]\r\n      ,[ptype]\r\n      ,[filesize]\r\n      ,[ptype1]\r\n      ,[filesize1]\r\n      ,[status]\r\n      ,[why]\r\n      ,[admin]\r\n      ,[checktime]\r\n      ,[addtime]\r\n      ,[userName],[payeeName],[account],[IdCard]", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                            stringBuilder.Append(" AND [userName] like @UserName");
                            SqlParameter sqlParameter2 = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                            sqlParameter2.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 20) + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "status":
                            stringBuilder.Append(" AND [status] = @status");
                            SqlParameter sqlParameter3 = new SqlParameter("@status", SqlDbType.TinyInt);
                            sqlParameter3.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [addtime] >= @stime");
                            SqlParameter sqlParameter4 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter4.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter4);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [addtime] <= @etime");
                            SqlParameter sqlParameter5 = new SqlParameter("@etime", SqlDbType.DateTime);
                            sqlParameter5.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter5);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
