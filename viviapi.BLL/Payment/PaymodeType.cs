namespace viviapi.BLL.Payment
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.BLL.Sys;
    using viviapi.Cache;
    using viviLib.ExceptionHandling;

    public class PaymodeType
    {
        internal static string PAYMODETYPE_CACHEKEY = (Constant.Cache_Mark + "{{E493762A-6010-4c74-818D-657F8EE0BD13}}");
        internal static string SQL_TABLE = "paymodetype";
        internal static string SQL_TABLE_FIELD = "id,type,modetypename,payrateid,isOpen,addtime,sort,release";

        public static DataTable GetList(bool iscache)
        {
            try
            {
                string objId = PAYMODETYPE_CACHEKEY;
                DataSet o = new DataSet();
                if (iscache)
                {
                    o = (DataSet)WebCache.GetCacheService().RetrieveObject(objId);
                }
                if ((o == null) || !iscache)
                {
                    SqlDependency dependency = DataBase.AddSqlDependency(objId, SQL_TABLE, SQL_TABLE_FIELD, "", null);
                    StringBuilder builder = new StringBuilder();
                    builder.Append("select id,type,modetypename,payrateid,isOpen,addtime,sort,release ");
                    builder.Append(" FROM paymodetype Where release = 1 order by sort");
                    o = DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
                    if (iscache)
                    {
                        WebCache.GetCacheService().AddObject(objId, o);
                    }
                }
                return o.Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
    }
}

