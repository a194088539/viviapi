using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.Channel;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Channel
{
    public class CodeMappingFactory
    {
        public static int Add(CodeMappingInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@pmodeCode", SqlDbType.VarChar, 20),
          new SqlParameter("@suppId", SqlDbType.Int, 4),
          new SqlParameter("@suppCode", SqlDbType.VarChar, 20)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.pmodeCode;
                sqlParameterArray[2].Value = (object)model.suppId;
                sqlParameterArray[3].Value = (object)model.suppCode;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_codemapping_ADD", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Update(CodeMappingInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@pmodeCode", SqlDbType.VarChar, 20),
          new SqlParameter("@suppId", SqlDbType.Int, 4),
          new SqlParameter("@suppCode", SqlDbType.VarChar, 20)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.pmodeCode;
                sqlParameterArray[2].Value = (object)model.suppId;
                sqlParameterArray[3].Value = (object)model.suppCode;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_codemapping_Update", sqlParameterArray) > 0;
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
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_codemapping_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static CodeMappingInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                CodeMappingInfo codeMappingInfo = new CodeMappingInfo();
                DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_codemapping_GetModel", sqlParameterArray);
                if (dataSet.Tables[0].Rows.Count <= 0)
                    return (CodeMappingInfo)null;
                if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
                    codeMappingInfo.id = int.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
                codeMappingInfo.pmodeCode = dataSet.Tables[0].Rows[0]["pmodeCode"].ToString();
                if (dataSet.Tables[0].Rows[0]["suppId"].ToString() != "")
                    codeMappingInfo.suppId = int.Parse(dataSet.Tables[0].Rows[0]["suppId"].ToString());
                codeMappingInfo.suppCode = dataSet.Tables[0].Rows[0]["suppCode"].ToString();
                return codeMappingInfo;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (CodeMappingInfo)null;
            }
        }

        public static DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select [id]\r\n      ,[pmodeCode]\r\n      ,[suppId]\r\n      ,[suppCode]\r\n      ,[SuppName]\r\n      ,[modeName] ");
            stringBuilder.Append(" FROM V_Codemapping ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
        }
    }
}
