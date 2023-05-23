namespace DBAccess
{
    using System.Web;

    public class SqlHelper
    {
        public static string CleanString(string inputString, int length)
        {
            return CleanString(inputString, length, false);
        }

        public static string CleanString(string inputString, int length, bool cleanSpecial)
        {
            string str;
            if ((length > 0) && (length < HttpUtility.HtmlEncode(inputString).Length))
            {
                str = HttpUtility.HtmlEncode(inputString).Substring(0, length);
            }
            else
            {
                str = HttpUtility.HtmlEncode(inputString);
            }
            if (cleanSpecial)
            {
                str = str.Replace("'", "''");
            }
            return str;
        }

        public static string GetCountSQL(string tables, string wheres, string distinctField)
        {
            return string.Format("\r\nSELECT \r\n\tCOUNT({0}) \r\nFROM \r\n\t{1} with(nolock)\r\nWHERE\r\n\t{2}", (distinctField.Length > 0) ? ("DISTINCT " + distinctField) : "0", tables, wheres);
        }

        public static string GetPageSelectSQL(string columns, string tables, string wheres, string orders, string key, int pageSize, int pageNum, bool isDistinct)
        {
            int count = pageSize;
            int startIndex = (pageNum - 1) * pageSize;
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            return GetSelectSQL(columns, tables, wheres, orders, key, startIndex, count, isDistinct);
        }

        public static string GetSelectSQL(string columns, string tables, string wheres, string orders, string key, bool isDistinct)
        {
            return GetSelectSQL(columns, tables, wheres, orders, key, 0, 0, isDistinct);
        }

        public static string GetSelectSQL(string columns, string tables, string wheres, string orders, string key, int startIndex, bool isDistinct)
        {
            return GetSelectSQL(columns, tables, wheres, orders, key, startIndex, 0, isDistinct);
        }

        public static string GetSelectSQL(string columns, string tables, string wheres, string orders, string key, int startIndex, int count, bool isDistinct)
        {
            if (startIndex > 1)
            {
                return string.Format("\r\nSELECT {5} {7:d} \r\n\t{0}\r\nFROM \r\n\t{1} with(nolock) \r\nWHERE \r\n\t{2} \r\nAND \r\n\t{4} NOT IN (\r\n\t\tSELECT {5} TOP {6:d}\r\n\t\t\t{4}\r\n\t\tFROM\r\n\t\t\t{1} with(nolock)\r\n\t\tWHERE \r\n\t\t\t{2} \r\n\t\tORDER BY {3}\r\n\t)\r\nORDER BY {3}", new object[] { columns, tables, wheres, orders, key, isDistinct ? "DISTINCT" : string.Empty, startIndex, (count > 0) ? string.Format("TOP {0:d}", count) : string.Empty });
            }
            return string.Format("\r\nSELECT {4} {5}\r\n\t{0} \r\nFROM \r\n\t{1} with(nolock) \r\nWHERE \r\n\t{2} \r\nORDER BY {3}", new object[] { columns, tables, wheres, orders, isDistinct ? "DISTINCT" : "", (count > 0) ? string.Format("TOP {0:d}", count) : string.Empty });
        }
    }
}

