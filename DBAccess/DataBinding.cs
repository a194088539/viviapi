namespace DBAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public sealed class DataBinding
    {
        public static string BuildInsertCommandText(string tableName, params string[] fields)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if ((fields == null) || (fields.Length < 1))
            {
                throw new ArgumentException("“fields”应至少包含一个字段。");
            }
            string str = string.Empty;
            string str2 = string.Empty;
            foreach (string str3 in fields)
            {
                str = str + str3 + ", ";
                str2 = str2 + "@" + str3 + ", ";
            }
            str = str.Remove(str.Length - 2);
            str2 = str2.Remove(str2.Length - 2);
            return string.Format("INSERT INTO [{0}] ( {1} ) VALUES ( {2} )", tableName, str, str2);
        }

        public static SqlParameter[] BuildParameter(string commandText, object dataObj)
        {
            return BuildParameter(commandText, dataObj, true).ToArray();
        }

        public static List<SqlParameter> BuildParameter(string commandText, object dataObj, bool checkParameter)
        {
            Type type = dataObj.GetType();
            MatchCollection matchs = new Regex(@"@(?<paramName>[\w\d_]+)", RegexOptions.Compiled | RegexOptions.Multiline).Matches(commandText);
            string name = string.Empty;
            Dictionary<string, int> dictionary = new Dictionary<string, int>(matchs.Count);
            List<SqlParameter> list = new List<SqlParameter>(matchs.Count);
            SqlParameter item = null;
            object obj2 = null;
            foreach (Match match in matchs)
            {
                name = match.Groups["paramName"].Value;
                PropertyInfo property = type.GetProperty(name);
                if (property == null)
                {
                    if (checkParameter)
                    {
                        throw new ArgumentException(string.Format("没有在参数dataObj的公共属性里找到与查询语句里相对应参数名称“{0}”的属性值。", name));
                    }
                }
                else
                {
                    obj2 = property.GetValue(dataObj, null);
                    item = new SqlParameter("@" + name, obj2);
                    if (!dictionary.ContainsKey(item.ParameterName))
                    {
                        dictionary.Add(item.ParameterName, 0);
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static string BuildUpdateCommandText(string tableName, string condition, params string[] fields)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if ((fields == null) || (fields.Length < 1))
            {
                throw new ArgumentException("“fields”应至少包含一个字段。");
            }
            string str = string.Empty;
            foreach (string str2 in fields)
            {
                string str3 = str;
                str = str3 + str2 + " = @" + str2 + ", ";
            }
            str = str.Remove(str.Length - 2);
            condition = condition.Trim();
            if (!(string.IsNullOrEmpty(condition) || condition.StartsWith("WHERE", true, null)))
            {
                condition = "WHERE " + condition;
            }
            return string.Format("UPDATE [{0}] SET {1} {2}", tableName, str, condition);
        }

        public static List<TClass> LoadForList<TClass>(DataRowCollection rows)
        {
            if (rows == null)
            {
                throw new ArgumentNullException("rows");
            }
            List<TClass> list = new List<TClass>(rows.Count);
            foreach (DataRow row in rows)
            {
                list.Add(LoadFromDataRow<TClass>(row));
            }
            return list;
        }

        public static List<TObject> LoadForObjectList<TObject>(DataRowCollection rows)
        {
            if ((rows == null) || (rows.Count == 0))
            {
                return new List<TObject>(0);
            }
            List<TObject> list = new List<TObject>(rows.Count);
            foreach (DataRow row in rows)
            {
                list.Add((TObject)row[0]);
            }
            return list;
        }

        public static List<TObject> LoadForObjectList<TObject>(DataRowCollection rows, string columnName)
        {
            if ((rows == null) || (rows.Count == 0))
            {
                return new List<TObject>(0);
            }
            List<TObject> list = new List<TObject>(rows.Count);
            foreach (DataRow row in rows)
            {
                list.Add((TObject)row[columnName]);
            }
            return list;
        }

        public static TClass LoadFromDataRow<TClass>(DataRow row)
        {
            Type type = typeof(TClass);
            PropertyInfo[] properties = type.GetProperties();
            object obj2 = Activator.CreateInstance(type);
            foreach (PropertyInfo info in properties)
            {
                if (!(!row.Table.Columns.Contains(info.Name) || row.IsNull(info.Name)))
                {
                    info.SetValue(obj2, row[info.Name], null);
                }
            }
            return (TClass)obj2;
        }

        public static List<string> LoadPropertysAsFileds(Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            if (properties == null)
            {
                return new List<string>(0);
            }
            List<string> list = new List<string>(properties.Length);
            for (int i = 0; i < properties.Length; i++)
            {
                list.Add(properties[i].Name);
            }
            return list;
        }

        public static DataSet ToDataSet(IList p_List)
        {
            DataSet set = new DataSet();
            DataTable table = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] properties = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList list = new ArrayList();
                    foreach (PropertyInfo info in properties)
                    {
                        object obj2 = info.GetValue(p_List[i], null);
                        list.Add(obj2);
                    }
                    object[] values = list.ToArray();
                    table.LoadDataRow(values, true);
                }
            }
            set.Tables.Add(table);
            return set;
        }
    }
}

