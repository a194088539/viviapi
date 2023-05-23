using DBAccess;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model;

namespace viviapi.BLL
{
    public class TempLabelFactory
    {
        public static int ADD(TempLabel _templabel)
        {
            SqlParameter sqlParameter = DataBase.MakeOutParam("@Id", SqlDbType.Int, 4);
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_TempLabel_ADD", sqlParameter, DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, (object)_templabel.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 500, (object)_templabel.Content), DataBase.MakeInParam("@info", SqlDbType.VarChar, 200, (object)_templabel.Info), DataBase.MakeInParam("@TemplateId", SqlDbType.VarChar, 20, (object)_templabel.TemplateId), DataBase.MakeInParam("@sort", SqlDbType.Int, 4, (object)_templabel.Sort), DataBase.MakeInParam("@source", SqlDbType.VarChar, 200, (object)_templabel.Source)) != 1)
                return 0;
            _templabel.ID = (int)sqlParameter.Value;
            return _templabel.ID;
        }

        public static TempLabel Get(int id)
        {
            TempLabel tempLabel = new TempLabel();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM [TempLabel] WHERE [ID]=" + (object)id);
            if (sqlDataReader.Read())
            {
                tempLabel.ID = (int)sqlDataReader["id"];
                tempLabel.Title = sqlDataReader["Title"].ToString();
                tempLabel.Content = sqlDataReader["Content"].ToString();
                tempLabel.Info = sqlDataReader["Info"].ToString();
                tempLabel.TemplateId = sqlDataReader["TemplateId"].ToString();
                tempLabel.Sort = int.Parse(sqlDataReader["Sort"].ToString());
                tempLabel.Source = sqlDataReader["Source"].ToString();
            }
            sqlDataReader.Close();
            return tempLabel;
        }

        public static bool Update(TempLabel _templabel)
        {
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_TempLabel_Update", DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object)_templabel.ID), DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, (object)_templabel.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 500, (object)_templabel.Content), DataBase.MakeInParam("@info", SqlDbType.VarChar, 8, (object)_templabel.Info), DataBase.MakeInParam("@TemplateId", SqlDbType.VarChar, 200, (object)_templabel.TemplateId), DataBase.MakeInParam("@sort", SqlDbType.Int, 4, (object)_templabel.Sort), DataBase.MakeInParam("@source", SqlDbType.VarChar, 200, (object)_templabel.Source)) == 1;
        }
    }
}
