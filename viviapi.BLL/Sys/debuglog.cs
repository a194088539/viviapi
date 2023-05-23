using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.Sys;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Sys
{
    public sealed class debuglog
    {
        internal const string SQL_TABLE = "v_debuginfo";
        internal const string SQL_TABLE_FIELDS = "id,bugtype,userid,userName,url,errorcode,errorinfo,detail,addtime,userorder";

        public static int Insert(debuginfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[9]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@bugtype", SqlDbType.TinyInt, 1),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@url", SqlDbType.VarChar, 2000),
          new SqlParameter("@errorcode", SqlDbType.VarChar, 50),
          new SqlParameter("@errorinfo", SqlDbType.VarChar, 200),
          new SqlParameter("@detail", SqlDbType.VarChar, 500),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@userorder", SqlDbType.VarChar, 30)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.bugtype;
                sqlParameterArray[2].Value = (object)model.userid;
                sqlParameterArray[3].Value = (object)model.url;
                sqlParameterArray[4].Value = (object)model.errorcode;
                sqlParameterArray[5].Value = (object)model.errorinfo;
                sqlParameterArray[6].Value = (object)model.detail;
                sqlParameterArray[7].Value = (object)model.addtime;
                sqlParameterArray[8].Value = (object)model.userorder;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_debuginfo_Insert", sqlParameterArray);
                if (obj != null)
                    return Convert.ToInt32(obj);
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static debuginfo GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            debuginfo debuginfo = new debuginfo();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_debuginfo_GetModel", sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return (debuginfo)null;
            if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
                debuginfo.id = int.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
            if (dataSet.Tables[0].Rows[0]["bugtype"].ToString() != "")
                debuginfo.bugtype = (debugtypeenum)int.Parse(dataSet.Tables[0].Rows[0]["bugtype"].ToString());
            if (dataSet.Tables[0].Rows[0]["userid"].ToString() != "")
                debuginfo.userid = new int?(int.Parse(dataSet.Tables[0].Rows[0]["userid"].ToString()));
            debuginfo.url = dataSet.Tables[0].Rows[0]["url"].ToString();
            debuginfo.errorcode = dataSet.Tables[0].Rows[0]["errorcode"].ToString();
            debuginfo.errorinfo = dataSet.Tables[0].Rows[0]["errorinfo"].ToString();
            debuginfo.detail = dataSet.Tables[0].Rows[0]["detail"].ToString();
            debuginfo.userorder = dataSet.Tables[0].Rows[0]["userorder"].ToString();
            if (dataSet.Tables[0].Rows[0]["addtime"].ToString() != "")
                debuginfo.addtime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["addtime"].ToString()));
            return debuginfo;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "v_debuginfo";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = debuglog.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("id,bugtype,userid,userName,url,errorcode,errorinfo,detail,addtime,userorder", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                        case "userorder":
                            stringBuilder.Append(" AND [userorder] like @userorder");
                            SqlParameter sqlParameter2 = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                            sqlParameter2.Value = (object)("%" + searchParam.ParamValue.ToString() + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [addtime] >= @stime");
                            SqlParameter sqlParameter3 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter3.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [addtime] <= @etime");
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
