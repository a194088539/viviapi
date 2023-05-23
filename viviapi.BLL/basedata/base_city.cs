using DBAccess;
using System.Data;
using System.Text;

namespace viviapi.BLL.basedata
{
    public class base_city
    {
        public static DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select CityID,CityName,ZipCode,ProvinceID,DateCreated,DateUpdated ");
            stringBuilder.Append(" FROM base_city ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }
    }
}
