using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL
{
    public class JuBao
    {
        internal const string SQL_TABLE = "JuBao";
        internal const string SQL_FIELDS = "id,[name],email,tel,url,[type],remark,addtime,status,checktime,[check],checkremark,pwd,field1,field2,field3";

        public bool Exists(int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_JuBao_Exists", sqlParameterArray)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public int Add(JuBaoInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[16]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@name", SqlDbType.NVarChar, 50),
          new SqlParameter("@email", SqlDbType.NVarChar, 30),
          new SqlParameter("@tel", SqlDbType.VarChar, 20),
          new SqlParameter("@url", SqlDbType.NVarChar, 200),
          new SqlParameter("@type", SqlDbType.TinyInt, 1),
          new SqlParameter("@remark", SqlDbType.NVarChar, 500),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@checktime", SqlDbType.DateTime),
          new SqlParameter("@check", SqlDbType.Int, 4),
          new SqlParameter("@checkremark", SqlDbType.NVarChar, 500),
          new SqlParameter("@pwd", SqlDbType.NVarChar, 20),
          new SqlParameter("@field1", SqlDbType.NVarChar, 50),
          new SqlParameter("@field2", SqlDbType.NVarChar, 50),
          new SqlParameter("@field3", SqlDbType.NVarChar, 200)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.name;
                sqlParameterArray[2].Value = (object)model.email;
                sqlParameterArray[3].Value = (object)model.tel;
                sqlParameterArray[4].Value = (object)model.url;
                sqlParameterArray[5].Value = (object)model.type;
                sqlParameterArray[6].Value = (object)model.remark;
                sqlParameterArray[7].Value = (object)model.addtime;
                sqlParameterArray[8].Value = (object)model.status;
                sqlParameterArray[9].Value = (object)model.checktime;
                sqlParameterArray[10].Value = (object)model.check;
                sqlParameterArray[11].Value = (object)model.checkremark;
                sqlParameterArray[12].Value = (object)model.pwd;
                sqlParameterArray[13].Value = (object)model.field1;
                sqlParameterArray[14].Value = (object)model.field2;
                sqlParameterArray[15].Value = (object)model.field3;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_JuBao_add", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(JuBaoInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[16]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@name", SqlDbType.NVarChar, 50),
          new SqlParameter("@email", SqlDbType.NVarChar, 30),
          new SqlParameter("@tel", SqlDbType.VarChar, 20),
          new SqlParameter("@url", SqlDbType.NVarChar, 200),
          new SqlParameter("@type", SqlDbType.TinyInt, 1),
          new SqlParameter("@remark", SqlDbType.NVarChar, 500),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@checktime", SqlDbType.DateTime),
          new SqlParameter("@check", SqlDbType.Int, 4),
          new SqlParameter("@checkremark", SqlDbType.NVarChar, 500),
          new SqlParameter("@pwd", SqlDbType.NVarChar, 20),
          new SqlParameter("@field1", SqlDbType.NVarChar, 50),
          new SqlParameter("@field2", SqlDbType.NVarChar, 50),
          new SqlParameter("@field3", SqlDbType.NVarChar, 200)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.name;
                sqlParameterArray[2].Value = (object)model.email;
                sqlParameterArray[3].Value = (object)model.tel;
                sqlParameterArray[4].Value = (object)model.url;
                sqlParameterArray[5].Value = (object)model.type;
                sqlParameterArray[6].Value = (object)model.remark;
                sqlParameterArray[7].Value = (object)model.addtime;
                sqlParameterArray[8].Value = (object)model.status;
                sqlParameterArray[9].Value = (object)model.checktime;
                sqlParameterArray[10].Value = (object)model.check;
                sqlParameterArray[11].Value = (object)model.checkremark;
                sqlParameterArray[12].Value = (object)model.pwd;
                sqlParameterArray[13].Value = (object)model.field1;
                sqlParameterArray[14].Value = (object)model.field2;
                sqlParameterArray[15].Value = (object)model.field3;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_JuBao_update", sqlParameterArray) > 0;
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
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_JuBao_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public JuBaoInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_JuBao_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (JuBaoInfo)null;
            }
        }

        public JuBaoInfo GetModelByPwd(string pwd)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@pwd", SqlDbType.NVarChar, 20)
                };
                sqlParameterArray[0].Value = (object)pwd;
                return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_JuBao_GetModelBypwd", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (JuBaoInfo)null;
            }
        }

        public JuBaoInfo GetModelFromDs(DataSet ds)
        {
            JuBaoInfo juBaoInfo = new JuBaoInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (JuBaoInfo)null;
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                juBaoInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                juBaoInfo.name = ds.Tables[0].Rows[0]["name"].ToString();
            if (ds.Tables[0].Rows[0]["email"] != null && ds.Tables[0].Rows[0]["email"].ToString() != "")
                juBaoInfo.email = ds.Tables[0].Rows[0]["email"].ToString();
            if (ds.Tables[0].Rows[0]["tel"] != null && ds.Tables[0].Rows[0]["tel"].ToString() != "")
                juBaoInfo.tel = ds.Tables[0].Rows[0]["tel"].ToString();
            if (ds.Tables[0].Rows[0]["url"] != null && ds.Tables[0].Rows[0]["url"].ToString() != "")
                juBaoInfo.url = ds.Tables[0].Rows[0]["url"].ToString();
            if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                juBaoInfo.type = (JuBaoEnum)int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
            if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                juBaoInfo.remark = ds.Tables[0].Rows[0]["remark"].ToString();
            if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                juBaoInfo.addtime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString()));
            if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                juBaoInfo.status = (JuBaoStatusEnum)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            if (ds.Tables[0].Rows[0]["checktime"] != null && ds.Tables[0].Rows[0]["checktime"].ToString() != "")
                juBaoInfo.checktime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["checktime"].ToString()));
            if (ds.Tables[0].Rows[0]["check"] != null && ds.Tables[0].Rows[0]["check"].ToString() != "")
                juBaoInfo.check = new int?(int.Parse(ds.Tables[0].Rows[0]["check"].ToString()));
            if (ds.Tables[0].Rows[0]["checkremark"] != null && ds.Tables[0].Rows[0]["checkremark"].ToString() != "")
                juBaoInfo.checkremark = ds.Tables[0].Rows[0]["checkremark"].ToString();
            if (ds.Tables[0].Rows[0]["pwd"] != null && ds.Tables[0].Rows[0]["pwd"].ToString() != "")
                juBaoInfo.pwd = ds.Tables[0].Rows[0]["pwd"].ToString();
            if (ds.Tables[0].Rows[0]["field1"] != null && ds.Tables[0].Rows[0]["field1"].ToString() != "")
                juBaoInfo.field1 = ds.Tables[0].Rows[0]["field1"].ToString();
            if (ds.Tables[0].Rows[0]["field2"] != null && ds.Tables[0].Rows[0]["field2"].ToString() != "")
                juBaoInfo.field2 = ds.Tables[0].Rows[0]["field2"].ToString();
            if (ds.Tables[0].Rows[0]["field3"] != null && ds.Tables[0].Rows[0]["field3"].ToString() != "")
                juBaoInfo.field3 = ds.Tables[0].Rows[0]["field3"].ToString();
            return juBaoInfo;
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
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_JuBao_GetList", sqlParameterArray).Tables[0];
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
                string tables = "JuBao";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "addtime desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = this.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("id,[name],email,tel,url,[type],remark,addtime,status,checktime,[check],checkremark,pwd,field1,field2,field3", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
    }
}
