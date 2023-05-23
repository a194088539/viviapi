using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Tools
{
    public class db
    {
        public static bool Backup(string path)
        {
            try
            {
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_database_backup", new SqlParameter[1]
                {
          DataBase.MakeInParam("@datapath", SqlDbType.VarChar, 200, (object) path)
                });
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
    }
}
