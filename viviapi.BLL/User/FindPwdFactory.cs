using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model.User;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
    public sealed class FindPwdFactory
    {
        public static bool Exists(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_findpwd_Exists", sqlParameterArray)) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static int Add(FindPwd model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[7]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@uid", SqlDbType.Int, 4),
          new SqlParameter("@username", SqlDbType.VarChar, 50),
          new SqlParameter("@oldpwd", SqlDbType.VarChar, 100),
          new SqlParameter("@newpwd", SqlDbType.VarChar, 100),
          new SqlParameter("@status", SqlDbType.Int, 4),
          new SqlParameter("@addtimer", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.uid;
                sqlParameterArray[2].Value = (object)model.username;
                sqlParameterArray[3].Value = (object)model.oldpwd;
                sqlParameterArray[4].Value = (object)model.newpwd;
                sqlParameterArray[5].Value = (object)model.status;
                sqlParameterArray[6].Value = (object)model.addtimer;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_findpwd_add", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(FindPwd model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[7]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@uid", SqlDbType.Int, 4),
          new SqlParameter("@username", SqlDbType.VarChar, 50),
          new SqlParameter("@oldpwd", SqlDbType.VarChar, 100),
          new SqlParameter("@newpwd", SqlDbType.VarChar, 100),
          new SqlParameter("@status", SqlDbType.Int, 4),
          new SqlParameter("@addtimer", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.uid;
                sqlParameterArray[2].Value = (object)model.username;
                sqlParameterArray[3].Value = (object)model.oldpwd;
                sqlParameterArray[4].Value = (object)model.newpwd;
                sqlParameterArray[5].Value = (object)model.status;
                sqlParameterArray[6].Value = (object)model.addtimer;
                return DataBase.ExecuteNonQuery("proc_findpwd_Update", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static FindPwd GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                FindPwd findPwd = new FindPwd();
                DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_findpwd_GetModel", sqlParameterArray);
                if (dataSet.Tables[0].Rows.Count <= 0)
                    return (FindPwd)null;
                if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
                    findPwd.id = int.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
                if (dataSet.Tables[0].Rows[0]["uid"].ToString() != "")
                    findPwd.uid = new int?(int.Parse(dataSet.Tables[0].Rows[0]["uid"].ToString()));
                findPwd.username = dataSet.Tables[0].Rows[0]["username"].ToString();
                findPwd.oldpwd = dataSet.Tables[0].Rows[0]["oldpwd"].ToString();
                findPwd.newpwd = dataSet.Tables[0].Rows[0]["newpwd"].ToString();
                if (dataSet.Tables[0].Rows[0]["status"].ToString() != "")
                    findPwd.status = new int?(int.Parse(dataSet.Tables[0].Rows[0]["status"].ToString()));
                if (dataSet.Tables[0].Rows[0]["addtimer"].ToString() != "")
                    findPwd.addtimer = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["addtimer"].ToString()));
                return findPwd;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (FindPwd)null;
            }
        }

        public static bool FindSucess(FindPwd model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@uid", SqlDbType.Int, 4),
          new SqlParameter("@newpwd", SqlDbType.VarChar, 100),
          new SqlParameter("@status", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.uid;
                sqlParameterArray[2].Value = (object)model.newpwd;
                sqlParameterArray[3].Value = (object)model.status;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_findpwd_success", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
    }
}
