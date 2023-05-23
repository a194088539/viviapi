namespace viviapi.DAL.Order
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class Helper
    {
        public int search_check(int o_userid, string userorderid, out DataRow row)
        {
            row = null;
            int num = 0x63;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@o_userid", SqlDbType.Int, 4), new SqlParameter("@userorderid_str", SqlDbType.VarChar, 30), new SqlParameter("@result", SqlDbType.TinyInt) };
            commandParameters[0].Value = o_userid;
            commandParameters[1].Value = userorderid;
            commandParameters[2].Direction = ParameterDirection.Output;
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_order_search_chk", commandParameters);
            num = Convert.ToInt32(commandParameters[2].Value);
            if (num == 0)
            {
                row = set.Tables[0].Rows[0];
            }
            return num;
        }
    }
}

