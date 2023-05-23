using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model;

namespace viviapi.BLL
{
    public class SinglePageFactory
    {
        public static int ADD(SinglePage _singlepage)
        {
            SqlParameter sqlParameter = DataBase.MakeOutParam("@Sid", SqlDbType.Int, 4);
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_SinglePage_ADD", sqlParameter, DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, (object)_singlepage.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 8000, (object)_singlepage.Content), DataBase.MakeInParam("@Addtime", SqlDbType.DateTime, 8, (object)_singlepage.Addtime), DataBase.MakeInParam("@interface1", SqlDbType.VarChar, 200, (object)_singlepage.Interface1), DataBase.MakeInParam("@Interface2", SqlDbType.VarChar, 200, (object)_singlepage.Interface2)) != 1)
                return 0;
            _singlepage.Sid = (int)sqlParameter.Value;
            return _singlepage.Sid;
        }

        public static SinglePage Get(int id)
        {
            SinglePage singlePage = new SinglePage();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM [SinglePage] WHERE [SID]=" + (object)id);
            if (sqlDataReader.Read())
            {
                singlePage.Sid = (int)sqlDataReader["Sid"];
                singlePage.Title = sqlDataReader["Title"].ToString();
                singlePage.Content = sqlDataReader["Content"].ToString();
                singlePage.Addtime = (DateTime)sqlDataReader["addtime"];
                singlePage.Interface1 = sqlDataReader["interface1"].ToString();
                singlePage.Interface2 = sqlDataReader["interface2"].ToString();
            }
            sqlDataReader.Close();
            return singlePage;
        }

        public static bool Update(SinglePage _singlepage)
        {
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_SinglePage_Update", DataBase.MakeInParam("@Sid", SqlDbType.Int, 4, (object)_singlepage.Sid), DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, (object)_singlepage.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 8000, (object)_singlepage.Content), DataBase.MakeInParam("@Addtime", SqlDbType.DateTime, 8, (object)_singlepage.Addtime), DataBase.MakeInParam("@interface1", SqlDbType.VarChar, 200, (object)_singlepage.Interface1), DataBase.MakeInParam("@Interface2", SqlDbType.VarChar, 200, (object)_singlepage.Interface2)) == 1;
        }
    }
}
