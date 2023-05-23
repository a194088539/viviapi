namespace DBAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Comm
    {
        private static string str = "0123456789abcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ";

        public static int Delete(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if (string.IsNullOrEmpty(condition))
            {
                condition = string.Empty;
            }
            condition = condition.Trim();
            if (!(string.IsNullOrEmpty(condition) || condition.StartsWith("WHERE", true, null)))
            {
                condition = "WHERE " + condition;
            }
            string commandText = "DELETE FROM [" + tableName + "] " + condition;
            return DataBase.ExecuteNonQuery(CommandType.Text, commandText, sqlParams);
        }

        public static List<T> ExecuteDataset<T>(string cmdText, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(cmdText))
            {
                throw new ArgumentNullException("cmdText");
            }
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, cmdText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        public static int ExtcuteCommand(string cmdText, params SqlParameter[] sqlParams)
        {
            return DataBase.ExecuteNonQuery(CommandType.Text, cmdText, sqlParams);
        }

        public static string GetRandomChar(Random rnd)
        {
            int num = rnd.Next(0, 0x7b);
            if (num < 10)
            {
                return num.ToString();
            }
            char c = (char)num;
            return (char.IsLower(c) ? c.ToString() : GetRandomChar(rnd));
        }

        public static int GetRecordCount(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            condition = condition.Trim();
            if (!(string.IsNullOrEmpty(condition) || condition.StartsWith("WHERE", true, null)))
            {
                condition = "WHERE " + condition;
            }
            string commandText = "SELECT COUNT(*) FROM [" + tableName + "] " + condition;
            return (int)DataBase.ExecuteScalar(CommandType.Text, commandText, sqlParams);
        }

        public static void Insert(string cmdText, object dataObj)
        {
            DataBase.ExecuteNonQuery(CommandType.Text, cmdText, DataBinding.BuildParameter(cmdText, dataObj));
        }

        public static string RandomNum(int n)
        {
            string[] strArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string str2 = "";
            int num = -1;
            Random random = new Random();
            for (int i = 1; i < (n + 1); i++)
            {
                if (num != -1)
                {
                    random = new Random((i * num) * ((int)DateTime.Now.Ticks));
                }
                int index = random.Next(10);
                if ((num != -1) && (num == index))
                {
                    return RandomNum(n);
                }
                num = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }

        public static List<T> Select<T>(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            return Select<T>(tableName, "*", condition, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string[] fieldsArray, string condition, params SqlParameter[] sqlParams)
        {
            string fields = string.Empty;
            if ((fieldsArray == null) || (fieldsArray.Length == 0))
            {
                fields = "*";
            }
            else
            {
                for (int i = 0; i < (fieldsArray.Length - 1); i++)
                {
                    fields = fields + fieldsArray[i] + ", ";
                }
                fields = fields + fieldsArray[fieldsArray.Length - 1];
            }
            return Select<T>(tableName, fields, -1, condition, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string fields, string condition, params SqlParameter[] sqlParams)
        {
            return Select<T>(tableName, fields, -1, condition, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string fields, int topCount, string condition, params SqlParameter[] sqlParams)
        {
            return Select<T>(tableName, fields, topCount, condition, string.Empty, true, sqlParams);
        }

        public static List<T> Select<T>(string tableName, string fields, string condition, string fldName, int pageIndex, int pageSize)
        {
            return Select<T>(tableName, fields, condition, fldName, pageIndex, pageSize);
        }

        public static List<T> Select<T>(string tableName, string fields, int topCount, string condition, string orderField, bool asc, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if (condition == null)
            {
                condition = string.Empty;
            }
            condition = condition.Trim();
            if (!(string.IsNullOrEmpty(condition) || condition.StartsWith("WHERE", true, null)))
            {
                condition = "WHERE " + condition;
            }
            if (string.IsNullOrEmpty(fields))
            {
                fields = "*";
            }
            string commandText = "SELECT";
            if (topCount > 0)
            {
                commandText = commandText + " TOP " + topCount.ToString();
            }
            string str2 = commandText;
            commandText = str2 + " " + fields + " FROM [" + tableName + "] " + condition;
            orderField = orderField.Trim();
            if (!string.IsNullOrEmpty(orderField))
            {
                orderField = " ORDER BY [" + orderField + "] " + (asc ? "ASC" : "DESC");
                commandText = commandText + orderField;
            }
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        public static List<T> Select<T>(string tableName, string fields, string condition, string fldName, int asc, int pageIndex, int pageSize)
        {
            List<SqlParameter> list = new List<SqlParameter>(7);
            list.Add(new SqlParameter("@tblName", tableName));
            list.Add(new SqlParameter("@fldName", fldName));
            list.Add(new SqlParameter("@PageSize", pageSize));
            list.Add(new SqlParameter("@PageIndex", pageIndex));
            list.Add(new SqlParameter("@IsCount", SqlDbType.BigInt));
            list.Add(new SqlParameter("@OrderType", asc));
            list.Add(new SqlParameter("@strWhere", condition));
            using (DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRecordFromPage", list.ToArray()))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        public static List<T> Select<T>(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, int pageSize)
        {
            List<SqlParameter> list = new List<SqlParameter>(8);
            list.Add(new SqlParameter("@tbname", tbname));
            list.Add(new SqlParameter("@FieldKey", FieldKey));
            list.Add(new SqlParameter("@PageCurrent", PageCurrent));
            list.Add(new SqlParameter("@PageSize", PageSize));
            list.Add(new SqlParameter("@FieldShow", FieldShow));
            list.Add(new SqlParameter("@FieldOrder", FieldOrder));
            list.Add(new SqlParameter("@Where", Where));
            list.Add(new SqlParameter("@PageCount", 10));
            using (DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "sp_pageView", list.ToArray()))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForList<T>(set.Tables[0].Rows);
            }
        }

        public static List<T> SelectObjectList<T>(string commandText, params SqlParameter[] sqlParams)
        {
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return new List<T>(0);
                }
                return DataBinding.LoadForObjectList<T>(set.Tables[0].Rows);
            }
        }

        public static T SelectOne<T>(string tableName, string condition, params SqlParameter[] sqlParams)
        {
            return SelectOne<T>(tableName, "*", condition, sqlParams);
        }

        public static T SelectOne<T>(string tableName, string fields, string condition, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            condition = condition.Trim();
            if (!(string.IsNullOrEmpty(condition) || condition.StartsWith("WHERE", true, null)))
            {
                condition = "WHERE " + condition;
            }
            fields = fields.Trim();
            if (string.IsNullOrEmpty(fields))
            {
                fields = "*";
            }
            string commandText = "SELECT " + fields + " FROM [" + tableName + "] " + condition;
            using (DataSet set = DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParams))
            {
                if ((set.Tables.Count < 1) || (set.Tables[0].Rows.Count < 1))
                {
                    return default(T);
                }
                return DataBinding.LoadFromDataRow<T>(set.Tables[0].Rows[0]);
            }
        }

        public static T SelectOne<T>(string tableName, string[] fieldsArray, string condition, params SqlParameter[] sqlParams)
        {
            string fields = string.Empty;
            if ((fieldsArray == null) || (fieldsArray.Length == 0))
            {
                fields = "*";
            }
            else
            {
                for (int i = 0; i < (fieldsArray.Length - 1); i++)
                {
                    fields = fields + fieldsArray[i] + ", ";
                }
                fields = fields + fieldsArray[fieldsArray.Length - 1];
            }
            return SelectOne<T>(tableName, fields, condition, sqlParams);
        }

        public static int Update(string cmdText, object dataObj)
        {
            return DataBase.ExecuteNonQuery(CommandType.Text, cmdText, DataBinding.BuildParameter(cmdText, dataObj));
        }
    }
}

