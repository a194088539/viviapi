using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using viviapi.Model;

namespace viviapi.BLL
{
    public class AdsFactory
    {
        public static int Add(AdsInfo _adsinfo)
        {
            SqlParameter sqlParameter = DataBase.MakeOutParam("@ID", SqlDbType.Int, 4);
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "Ads_ADD", sqlParameter, DataBase.MakeInParam("@AdsName", SqlDbType.VarChar, 100, (object)_adsinfo.AdsName), DataBase.MakeInParam("@Description", SqlDbType.VarChar, 200, (object)_adsinfo.Description), DataBase.MakeInParam("@Href", SqlDbType.VarChar, 200, (object)_adsinfo.Href), DataBase.MakeInParam("@AdsType", SqlDbType.TinyInt, 1, (object)_adsinfo.AdsType), DataBase.MakeInParam("@Prices", SqlDbType.Money, 8, (object)_adsinfo.Prices), DataBase.MakeInParam("@ShowStyle", SqlDbType.TinyInt, 1, (object)_adsinfo.ShowStyle), DataBase.MakeInParam("@TargetType", SqlDbType.VarChar, 50, (object)_adsinfo.TargetType), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)_adsinfo.AddTime), DataBase.MakeInParam("@AdvertisersId", SqlDbType.Int, 4, (object)_adsinfo.AdvertisersId), DataBase.MakeInParam("@AdsStatus", SqlDbType.TinyInt, 1, (object)_adsinfo.AdsStatus)) != 1)
                return 0;
            _adsinfo.ID = (int)sqlParameter.Value;
            HttpContext.Current.Cache.Insert("adsinfo" + _adsinfo.ID.ToString(), (object)_adsinfo);
            return _adsinfo.ID;
        }

        public static bool DelAds(int _adsid)
        {
            if (!AdsPicFactory.DeleteByAdsId(_adsid))
                return false;
            DataBase.ExecuteNonQuery(CommandType.Text, "DELETE FROM [Ads] WHERE ID=@ID", new SqlParameter[1]
            {
        DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object) _adsid)
            });
            return true;
        }

        public static AdsInfo GetInfo(int id)
        {
            AdsInfo adsInfo = new AdsInfo();
            if (HttpContext.Current.Cache["adsinfo" + id.ToString()] != null)
                return (AdsInfo)HttpContext.Current.Cache.Get("adsinfo" + id.ToString());
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM [Ads] WHERE [ID]=@ID", new SqlParameter[1]
            {
        DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object) id)
            });
            if (sqlDataReader.Read())
            {
                adsInfo.ID = (int)sqlDataReader["ID"];
                adsInfo.AdsName = sqlDataReader["AdsName"].ToString();
                adsInfo.AdsType = (AdsTypeEnum)int.Parse(sqlDataReader["AdsType"].ToString());
                adsInfo.AdsStatus = (AdsStatusEnum)int.Parse(sqlDataReader["AdsStatus"].ToString());
                adsInfo.AddTime = DateTime.Parse(sqlDataReader["AddTime"].ToString());
                adsInfo.AdvertisersId = (int)sqlDataReader["AdvertisersId"];
                adsInfo.Description = sqlDataReader["Description"].ToString();
                adsInfo.Href = sqlDataReader["Href"].ToString();
                adsInfo.Prices = (Decimal)sqlDataReader["Prices"];
                adsInfo.ShowStyle = (ShowStyleEnum)int.Parse(sqlDataReader["ShowStyle"].ToString());
                adsInfo.TargetType = sqlDataReader["TargetType"].ToString();
            }
            sqlDataReader.Close();
            HttpContext.Current.Cache.Insert("adsinfo" + id.ToString(), (object)adsInfo);
            return adsInfo;
        }

        public static bool Update(AdsInfo _adsinfo)
        {
            HttpContext.Current.Cache.Remove("adsinfo" + _adsinfo.ID.ToString());
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "Ads_Update", DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object)_adsinfo.ID), DataBase.MakeInParam("@AdsName", SqlDbType.VarChar, 100, (object)_adsinfo.AdsName), DataBase.MakeInParam("@Description", SqlDbType.VarChar, 200, (object)_adsinfo.Description), DataBase.MakeInParam("@Href", SqlDbType.VarChar, 200, (object)_adsinfo.Href), DataBase.MakeInParam("@AdsType", SqlDbType.TinyInt, 1, (object)_adsinfo.AdsType), DataBase.MakeInParam("@Prices", SqlDbType.Money, 8, (object)_adsinfo.Prices), DataBase.MakeInParam("@ShowStyle", SqlDbType.TinyInt, 1, (object)_adsinfo.ShowStyle), DataBase.MakeInParam("@TargetType", SqlDbType.VarChar, 50, (object)_adsinfo.TargetType), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)_adsinfo.AddTime), DataBase.MakeInParam("@AdvertisersId", SqlDbType.Int, 4, (object)_adsinfo.AdvertisersId), DataBase.MakeInParam("@AdsStatus", SqlDbType.TinyInt, 1, (object)_adsinfo.AdsStatus)) > 0;
        }
    }
}
