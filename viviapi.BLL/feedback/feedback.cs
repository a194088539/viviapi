using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.BLL.Sys;
using viviapi.Cache;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL
{
    public class feedback
    {
        public static string CACHE_KEY = Constant.Cache_Mark + "USERHOST_{0}";
        internal const string SQL_TABLE = "V_feedback";
        internal const string SQL_FIELDS = "[id]\r\n      ,[userid]\r\n      ,[typeid]\r\n      ,[title]\r\n      ,[cont]\r\n      ,[status]\r\n      ,[addtime]\r\n      ,[reply]\r\n      ,[replyer]\r\n      ,[replytime]\r\n      ,[userName]\r\n      ,[replyname]\r\n      ,[relname],clientip";

        public bool Exists(int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_feedback_Exists", sqlParameterArray)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public int Add(feedbackInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[11]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@typeid", SqlDbType.TinyInt, 1),
          new SqlParameter("@title", SqlDbType.NVarChar, 50),
          new SqlParameter("@cont", SqlDbType.NVarChar, 200),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@reply", SqlDbType.NVarChar, 50),
          new SqlParameter("@replyer", SqlDbType.Int, 4),
          new SqlParameter("@replytime", SqlDbType.DateTime),
          new SqlParameter("@clientip", SqlDbType.VarChar, 20)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.typeid;
                sqlParameterArray[3].Value = (object)model.title;
                sqlParameterArray[4].Value = (object)model.cont;
                sqlParameterArray[5].Value = (object)model.status;
                sqlParameterArray[6].Value = (object)model.addtime;
                sqlParameterArray[7].Value = (object)model.reply;
                sqlParameterArray[8].Value = (object)model.replyer;
                sqlParameterArray[9].Value = (object)model.replytime;
                sqlParameterArray[10].Value = (object)model.clientip;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_add", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(feedbackInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[10]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@typeid", SqlDbType.TinyInt, 1),
          new SqlParameter("@title", SqlDbType.NVarChar, 50),
          new SqlParameter("@cont", SqlDbType.NVarChar, 200),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@reply", SqlDbType.NVarChar, 50),
          new SqlParameter("@replyer", SqlDbType.Int, 4),
          new SqlParameter("@replytime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.typeid;
                sqlParameterArray[3].Value = (object)model.title;
                sqlParameterArray[4].Value = (object)model.cont;
                sqlParameterArray[5].Value = (object)model.status;
                sqlParameterArray[6].Value = (object)model.addtime;
                sqlParameterArray[7].Value = (object)model.reply;
                sqlParameterArray[8].Value = (object)model.replyer;
                sqlParameterArray[9].Value = (object)model.replytime;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_update", sqlParameterArray) > 0;
                if (flag)
                    this.ClearCache(model.id);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool ChangeStatus(int id, int status)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt)
                };
                sqlParameterArray[0].Value = (object)id;
                sqlParameterArray[1].Value = (object)status;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_ChangeStatus", sqlParameterArray) > 0;
                if (flag)
                    this.ClearCache(id);
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
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_feedback_Delete", sqlParameterArray) > 0;
                if (flag)
                    this.ClearCache(id);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public feedbackInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return feedback.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_feedback_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (feedbackInfo)null;
            }
        }

        public feedbackInfo GetModel(int id, int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                sqlParameterArray[1].Value = (object)userid;
                return feedback.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_feedback_GetModelByuser", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (feedbackInfo)null;
            }
        }

        public feedbackInfo GetCacheModel(int id)
        {
            feedbackInfo feedbackInfo1 = new feedbackInfo();
            string str = string.Format(feedback.CACHE_KEY, (object)id);
            feedbackInfo feedbackInfo2 = (feedbackInfo)WebCache.GetCacheService().RetrieveObject(str);
            if (feedbackInfo2 == null)
            {
                IDictionary<string, object> parameters = (IDictionary<string, object>)new Dictionary<string, object>();
                parameters.Add("id", (object)id);
                DataBase.AddSqlDependency(str, "V_feedback", "[id]\r\n      ,[userid]\r\n      ,[typeid]\r\n      ,[title]\r\n      ,[cont]\r\n      ,[status]\r\n      ,[addtime]\r\n      ,[reply]\r\n      ,[replyer]\r\n      ,[replytime]\r\n      ,[userName]\r\n      ,[replyname]\r\n      ,[relname],clientip", "[id]=@id", parameters);
                feedbackInfo2 = this.GetModel(id);
                WebCache.GetCacheService().AddObject(str, (object)feedbackInfo2);
            }
            return feedbackInfo2;
        }

        public static feedbackInfo GetModelFromDs(DataSet ds)
        {
            feedbackInfo feedbackInfo = new feedbackInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (feedbackInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                feedbackInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                feedbackInfo.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
            if (ds.Tables[0].Rows[0]["typeid"].ToString() != "")
                feedbackInfo.typeid = (feedbacktype)int.Parse(ds.Tables[0].Rows[0]["typeid"].ToString());
            feedbackInfo.title = ds.Tables[0].Rows[0]["title"].ToString();
            feedbackInfo.cont = ds.Tables[0].Rows[0]["cont"].ToString();
            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                feedbackInfo.status = (feedbackstatus)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                feedbackInfo.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
            feedbackInfo.reply = ds.Tables[0].Rows[0]["reply"].ToString();
            if (ds.Tables[0].Rows[0]["replyer"].ToString() != "")
                feedbackInfo.replyer = new int?(int.Parse(ds.Tables[0].Rows[0]["replyer"].ToString()));
            if (ds.Tables[0].Rows[0]["replytime"].ToString() != "")
                feedbackInfo.replytime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["replytime"].ToString()));
            return feedbackInfo;
        }

        public DataTable GetList(int userId)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int)
                };
                sqlParameterArray[0].Value = (object)userId;
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_feedback_GetList", sqlParameterArray).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_feedback";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc,userid desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = this.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userid]\r\n      ,[typeid]\r\n      ,[title]\r\n      ,[cont]\r\n      ,[status]\r\n      ,[addtime]\r\n      ,[reply]\r\n      ,[replyer]\r\n      ,[replytime]\r\n      ,[userName]\r\n      ,[replyname]\r\n      ,[relname],clientip", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return dataSet;
            }
        }

        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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

        internal void ClearCache(int id)
        {
            WebCache.GetCacheService().RemoveObject(string.Format(feedback.CACHE_KEY, (object)id));
        }
    }
}
