using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model.User;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
    public class usersetting
    {
        public bool Insert(usersettingInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[6]
                {
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@defaultpay", SqlDbType.Int, 4),
          new SqlParameter("@payrate", SqlDbType.VarChar, 1000),
          new SqlParameter("@special", SqlDbType.Int, 4),
          new SqlParameter("@istransfer", SqlDbType.TinyInt, 1),
          new SqlParameter("@isRequireAgentDistAudit", SqlDbType.TinyInt, 1)
                };
                sqlParameterArray[0].Value = (object)model.userid;
                sqlParameterArray[1].Value = (object)model.defaultpay;
                sqlParameterArray[2].Value = (object)model.payrate;
                sqlParameterArray[3].Value = (object)model.special;
                sqlParameterArray[4].Value = (object)model.istransfer;
                sqlParameterArray[5].Value = (object)model.isRequireAgentDistAudit;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersetting_Insert", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public usersettingInfo GetModel(int userid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@userid", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)userid;
            usersettingInfo usersettingInfo = new usersettingInfo();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersetting_GetModel", sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["userid"] != null && dataSet.Tables[0].Rows[0]["userid"].ToString() != "")
                    usersettingInfo.userid = int.Parse(dataSet.Tables[0].Rows[0]["userid"].ToString());
                if (dataSet.Tables[0].Rows[0]["defaultpay"] != null && dataSet.Tables[0].Rows[0]["defaultpay"].ToString() != "")
                    usersettingInfo.defaultpay = int.Parse(dataSet.Tables[0].Rows[0]["defaultpay"].ToString());
                if (dataSet.Tables[0].Rows[0]["special"] != null && dataSet.Tables[0].Rows[0]["special"].ToString() != "")
                    usersettingInfo.special = int.Parse(dataSet.Tables[0].Rows[0]["special"].ToString());
                if (dataSet.Tables[0].Rows[0]["istransfer"] != null && dataSet.Tables[0].Rows[0]["istransfer"].ToString() != "")
                    usersettingInfo.istransfer = int.Parse(dataSet.Tables[0].Rows[0]["istransfer"].ToString());
                if (dataSet.Tables[0].Rows[0]["isRequireAgentDistAudit"] != null && dataSet.Tables[0].Rows[0]["isRequireAgentDistAudit"].ToString() != "")
                    usersettingInfo.isRequireAgentDistAudit = byte.Parse(dataSet.Tables[0].Rows[0]["isRequireAgentDistAudit"].ToString());
                usersettingInfo.payrate = dataSet.Tables[0].Rows[0]["payrate"].ToString();
                return usersettingInfo;
            }
            usersettingInfo.userid = userid;
            return usersettingInfo;
        }
    }
}
