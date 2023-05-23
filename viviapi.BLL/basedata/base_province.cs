using DBAccess;
using System;
using System.Data;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.basedata
{
    public class base_province
    {
        public static DataSet GetList(string strWhere)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("select ProvinceID,ProvinceName,DateCreated,DateUpdated ");
                stringBuilder.Append(" FROM base_province ");
                if (strWhere.Trim() != "")
                    stringBuilder.Append(" where " + strWhere);
                return DbHelperSQL.Query(stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }
    }
}
