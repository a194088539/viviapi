using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.channel
{
    public class currchannel
    {
        public static int Get(int userid, int typeid, int len, string serverid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[5]
                {
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@typeid", SqlDbType.Int, 4),
          new SqlParameter("@len", SqlDbType.Int, 4),
          new SqlParameter("@serverid", SqlDbType.VarChar, 50),
          new SqlParameter("@datetime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)userid;
                sqlParameterArray[1].Value = (object)typeid;
                sqlParameterArray[2].Value = (object)len;
                sqlParameterArray[3].Value = (object)serverid;
                sqlParameterArray[4].Value = (object)DateTime.Now;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_currchannel_get", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 1;
            }
        }
    }
}
