namespace viviapi.BLL
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.Model;
    using viviLib.Data;
    using viviLib.ExceptionHandling;

    public class PhoneValidFactory
    {
        internal const string SQL_TABLE = "phoneValid";
        internal const string SQL_TABLE_FIELD = "[ID],[phone],[count],[isValid],[enable]";

        public static int Add(PhoneValidLog model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@phone", SqlDbType.NVarChar, 20), new SqlParameter("@sendTime", SqlDbType.DateTime), new SqlParameter("@clientIP", SqlDbType.NVarChar, 50), new SqlParameter("@code", SqlDbType.NVarChar, 50), new SqlParameter("@enable", SqlDbType.Bit) };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.phone;
                commandParameters[2].Value = model.sendTime;
                commandParameters[3].Value = model.clientIP;
                commandParameters[4].Value = model.code;
                commandParameters[5].Value = model.Enable;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_phoneValid_Add", commandParameters);
                if (commandParameters[0].Value != DBNull.Value)
                {
                    return (int)commandParameters[0].Value;
                }
                return 0;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder builder = new StringBuilder(" 1 = 1");
            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SearchParam param2 = param[i];
                    string str2 = param2.ParamKey.Trim().ToLower();
                    if ((str2 != null) && (str2 == "mobile"))
                    {
                        builder.Append(" AND [phone] like @phone");
                        SqlParameter item = new SqlParameter("@phone", SqlDbType.VarChar, 40);
                        item.Value = "%" + param2.ParamValue + "%";
                        paramList.Add(item);
                    }
                }
            }
            return builder.ToString();
        }

        public static bool isLimited(string phone)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(" select [enable] from phoneValid");
                builder.Append(" where phone=@phone ");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@phone", SqlDbType.NVarChar, 50) };
                commandParameters[0].Value = phone;
                object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
                if ((obj2 == null) || (obj2 == DBNull.Value))
                {
                    return false;
                }
                return Convert.ToBoolean(obj2);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "phoneValid";
                string key = "[id]";
                string columns = "[ID],[phone],[count],[isValid],[enable]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(columns, tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect * from dbo.phoneValidLog where " + wheres;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public static bool PhoneSetting(int id, bool state)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@enable", SqlDbType.Bit) };
                commandParameters[0].Value = id;
                commandParameters[1].Value = state;
                return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_phoneValid_setting", commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static int SendCount(string phone)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("select [count] from phoneValid");
                builder.Append(" where phone=@phone ");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@phone", SqlDbType.NVarChar, 50) };
                commandParameters[0].Value = phone;
                object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
                if ((obj2 == null) || (obj2 == DBNull.Value))
                {
                    return 0;
                }
                return Convert.ToInt32(obj2);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }
    }
}

