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
    public class usersIdImage
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
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_usersIdImage_Exists", sqlParameterArray)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public int Add(usersIdImageInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[10]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@image_on", SqlDbType.Image),
          new SqlParameter("@image_down", SqlDbType.Image),
          new SqlParameter("@ptype", SqlDbType.NVarChar, 20),
          new SqlParameter("@filesize", SqlDbType.Int, 4),
          new SqlParameter("@ptype1", SqlDbType.NVarChar, 20),
          new SqlParameter("@filesize1", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@addtime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.userId;
                sqlParameterArray[2].Value = (object)model.image_on;
                sqlParameterArray[3].Value = (object)model.image_down;
                sqlParameterArray[4].Value = (object)model.ptype;
                sqlParameterArray[5].Value = (object)model.filesize;
                sqlParameterArray[6].Value = (object)model.ptype1;
                sqlParameterArray[7].Value = (object)model.filesize1;
                sqlParameterArray[8].Value = (object)model.status;
                sqlParameterArray[9].Value = (object)model.addtime;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersIdImage_add", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Check(usersIdImageInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[5]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@why", SqlDbType.NVarChar, 150),
          new SqlParameter("@admin", SqlDbType.Int, 4),
          new SqlParameter("@checktime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.status;
                sqlParameterArray[2].Value = (object)model.why;
                sqlParameterArray[3].Value = (object)model.admin;
                sqlParameterArray[4].Value = (object)model.checktime;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersIdImage_update", sqlParameterArray) > 0;
                if (flag && model.status == IdImageStatus.审核成功)
                    UserFactory.ClearCache(model.userId.Value);
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
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersIdImage_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public usersIdImageInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return usersIdImage.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersIdImage_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (usersIdImageInfo)null;
            }
        }

        public usersIdImageInfo GetModelByUser(int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userid", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                return usersIdImage.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersIdImage_GetByUser", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (usersIdImageInfo)null;
            }
        }

        public usersIdImageInfo Get(int id)
        {
            usersIdImageInfo usersIdImageInfo = new usersIdImageInfo();
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            IDataReader dataReader = (IDataReader)DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_usersIdImage_GetModel", sqlParameterArray);
            if (dataReader.Read())
            {
                object obj1 = dataReader["image_on"];
                if (obj1 != null && obj1 != DBNull.Value)
                    usersIdImageInfo.image_on = (byte[])obj1;
                object obj2 = dataReader["image_down"];
                if (obj2 != null && obj2 != DBNull.Value)
                    usersIdImageInfo.image_down = (byte[])obj2;
                usersIdImageInfo.ptype = dataReader["ptype"].ToString();
                object obj3 = dataReader["filesize"];
                if (obj3 != null && obj3 != DBNull.Value)
                    usersIdImageInfo.filesize = new int?((int)obj3);
                usersIdImageInfo.ptype1 = dataReader["ptype1"].ToString();
                object obj4 = dataReader["filesize1"];
                if (obj4 != null && obj4 != DBNull.Value)
                    usersIdImageInfo.filesize1 = new int?((int)obj4);
            }
            return usersIdImageInfo;
        }

        public static usersIdImageInfo GetModelFromDs(DataSet ds)
        {
            usersIdImageInfo usersIdImageInfo = new usersIdImageInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (usersIdImageInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                usersIdImageInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["userId"].ToString() != "")
                usersIdImageInfo.userId = new int?(int.Parse(ds.Tables[0].Rows[0]["userId"].ToString()));
            usersIdImageInfo.ptype = ds.Tables[0].Rows[0]["ptype"].ToString();
            if (ds.Tables[0].Rows[0]["filesize"].ToString() != "")
                usersIdImageInfo.filesize = new int?(int.Parse(ds.Tables[0].Rows[0]["filesize"].ToString()));
            usersIdImageInfo.ptype1 = ds.Tables[0].Rows[0]["ptype1"].ToString();
            if (ds.Tables[0].Rows[0]["filesize1"].ToString() != "")
                usersIdImageInfo.filesize1 = new int?(int.Parse(ds.Tables[0].Rows[0]["filesize1"].ToString()));
            usersIdImageInfo.status = !(ds.Tables[0].Rows[0]["status"].ToString() != "") ? IdImageStatus.未知 : (IdImageStatus)int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            usersIdImageInfo.why = ds.Tables[0].Rows[0]["why"].ToString();
            if (ds.Tables[0].Rows[0]["admin"].ToString() != "")
                usersIdImageInfo.admin = new int?(int.Parse(ds.Tables[0].Rows[0]["admin"].ToString()));
            if (ds.Tables[0].Rows[0]["checktime"].ToString() != "")
                usersIdImageInfo.checktime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["checktime"].ToString()));
            if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                usersIdImageInfo.addtime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString()));
            return usersIdImageInfo;
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
                string wheres = usersIdImage.BuilderWhere(searchParams, paramList);
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
