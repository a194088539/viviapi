using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using viviapi.Model;
using viviLib;

namespace viviapi.BLL
{
    public class AdsPicFactory
    {
        public static int Add(AdsPicInfo adspicinfo)
        {
            SqlParameter sqlParameter = DataBase.MakeOutParam("@PicId", SqlDbType.Int, 4);
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "AdsPic_ADD", sqlParameter, DataBase.MakeInParam("@AdsId", SqlDbType.Int, 4, (object)adspicinfo.AdsId), DataBase.MakeInParam("@AdsPicPath", SqlDbType.VarChar, 100, (object)adspicinfo.AdsPicPath), DataBase.MakeInParam("@SizeX", SqlDbType.Int, 4, (object)adspicinfo.SizeX), DataBase.MakeInParam("@SizeY", SqlDbType.Int, 4, (object)adspicinfo.SizeY)) != 1)
                return 0;
            adspicinfo.PicId = (int)sqlParameter.Value;
            return adspicinfo.PicId;
        }

        public static void Delete(int PicId)
        {
            DataBase.ExecuteNonQuery(CommandType.Text, "DELETE FROM [AdsPic] WHERE PicId=@PicId", new SqlParameter[1]
            {
        DataBase.MakeInParam("@PicId", SqlDbType.Int, 4, (object) PicId)
            });
        }

        public static bool DeleteByAdsId(int adsid)
        {
            try
            {
                foreach (AdsPicInfo adspicinfo in AdsPicFactory.GetAdsPicInfosByAdsId(adsid))
                    AdsPicFactory.DeletePic(adspicinfo);
                DataBase.ExecuteNonQuery(CommandType.Text, "DELETE FROM [AdsPic] WHERE AdsId=@AdsId", new SqlParameter[1]
                {
          DataBase.MakeInParam("@AdsId", SqlDbType.Int, 4, (object) adsid)
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void DeletePic(AdsPicInfo adspicinfo)
        {
            string path = HttpContext.Current.Server.MapPath(adspicinfo.AdsPicPath);
            if (!File.Exists(path))
                return;
            File.Delete(path);
        }

        public static List<AdsPicInfo> GetAdsPicInfosByAdsId(int adsid)
        {
            List<AdsPicInfo> list = new List<AdsPicInfo>();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT [PicId] ,[AdsId] ,[AdsPicPath] ,[SizeX] ,[SizeY] FROM [AdsPic] WHERE [AdsId]=@AdsId", new SqlParameter[1]
            {
        DataBase.MakeInParam("@AdsId", SqlDbType.Int, 4, (object) adsid)
            });
            while (sqlDataReader.Read())
                list.Add(new AdsPicInfo()
                {
                    AdsId = (int)sqlDataReader["AdsId"],
                    AdsPicPath = sqlDataReader["AdsPicPath"].ToString(),
                    PicId = (int)sqlDataReader["PicId"],
                    SizeX = (int)sqlDataReader["SizeX"],
                    SizeY = (int)sqlDataReader["SizeY"]
                });
            sqlDataReader.Close();
            return list;
        }

        public static AdsPicInfo GetInfo(int picid)
        {
            AdsPicInfo adsPicInfo = new AdsPicInfo();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM [AdsPic] WHERE PicId=@PicId", new SqlParameter[1]
            {
        DataBase.MakeInParam("@PicId", SqlDbType.Int, 4, (object) picid)
            });
            if (sqlDataReader.Read())
            {
                adsPicInfo.AdsId = (int)sqlDataReader["AdsId"];
                adsPicInfo.AdsPicPath = sqlDataReader["AdsPicPath"].ToString();
                adsPicInfo.PicId = (int)sqlDataReader["PicId"];
                adsPicInfo.SizeX = (int)sqlDataReader["SizeX"];
                adsPicInfo.SizeY = (int)sqlDataReader["SizeY"];
            }
            sqlDataReader.Close();
            return adsPicInfo;
        }

        public static AdsPicInfo GetInfoADID(int picid)
        {
            AdsPicInfo adsPicInfo = new AdsPicInfo();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT * FROM [AdsPic] WHERE AdsId=@AdsId", new SqlParameter[1]
            {
        DataBase.MakeInParam("@AdsId", SqlDbType.Int, 4, (object) picid)
            });
            if (sqlDataReader.Read())
            {
                adsPicInfo.AdsId = (int)sqlDataReader["AdsId"];
                adsPicInfo.AdsPicPath = sqlDataReader["AdsPicPath"].ToString();
                adsPicInfo.PicId = (int)sqlDataReader["PicId"];
                adsPicInfo.SizeX = (int)sqlDataReader["SizeX"];
                adsPicInfo.SizeY = (int)sqlDataReader["SizeY"];
            }
            sqlDataReader.Close();
            return adsPicInfo;
        }

        public static void SaveAdsPic(int adsid, string filekey)
        {
            foreach (AdsPicInfo adspicinfo in AdsPicFactory.SaveRequestFiles(filekey))
            {
                adspicinfo.AdsId = adsid;
                AdsPicFactory.Add(adspicinfo);
            }
        }

        public static List<AdsPicInfo> SaveRequestFiles(string filekey)
        {
            List<AdsPicInfo> list = new List<AdsPicInfo>();
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int index = 0; index < HttpContext.Current.Request.Files.Count; ++index)
            {
                if (!HttpContext.Current.Request.Files[index].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[index].Equals(filekey))
                {
                    string str1 = Path.GetExtension(Path.GetFileName(HttpContext.Current.Request.Files[index].FileName)).ToLower();
                    HttpContext.Current.Request.Files[index].ContentType.ToLower();
                    string mapPath = Utility.GetMapPath("/");
                    StringBuilder stringBuilder1 = new StringBuilder("");
                    stringBuilder1.Append("upload");
                    stringBuilder1.Append(Path.DirectorySeparatorChar);
                    StringBuilder stringBuilder2 = stringBuilder1;
                    DateTime now = DateTime.Now;
                    string str2 = now.ToString("yyyy");
                    stringBuilder2.Append(str2);
                    stringBuilder1.Append("-");
                    StringBuilder stringBuilder3 = stringBuilder1;
                    now = DateTime.Now;
                    string str3 = now.ToString("MM");
                    stringBuilder3.Append(str3);
                    stringBuilder1.Append("-");
                    StringBuilder stringBuilder4 = stringBuilder1;
                    now = DateTime.Now;
                    string str4 = now.ToString("dd");
                    stringBuilder4.Append(str4);
                    stringBuilder1.Append(Path.DirectorySeparatorChar);
                    if (!Directory.Exists(mapPath + stringBuilder1.ToString()))
                        Utility.CreateDir(mapPath + stringBuilder1.ToString());
                    string str5 = stringBuilder1.ToString() + (Environment.TickCount & int.MaxValue).ToString() + index.ToString() + random.Next(1000, 9999).ToString() + str1;
                    try
                    {
                        HttpContext.Current.Request.Files[index].SaveAs(mapPath + str5);
                    }
                    catch
                    {
                        if (!Utility.FileExists(mapPath + str5))
                            HttpContext.Current.Request.Files[index].SaveAs(mapPath + str5);
                    }
                    list.Add(new AdsPicInfo()
                    {
                        SizeX = int.Parse(HttpContext.Current.Request.Form["SizeXBox"].Split(',')[index]),
                        SizeY = int.Parse(HttpContext.Current.Request.Form["SizeYBox"].Split(',')[index]),
                        AdsPicPath = "\\" + str5
                    });
                }
            }
            return list;
        }

        public static bool Update(AdsPicInfo adspicinfo)
        {
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "Ads_Update", DataBase.MakeInParam("@PicId", SqlDbType.Int, 4, (object)adspicinfo.PicId), DataBase.MakeInParam("@AdsId", SqlDbType.Int, 4, (object)adspicinfo.AdsId), DataBase.MakeInParam("@AdsPicPath", SqlDbType.VarChar, 100, (object)adspicinfo.AdsPicPath), DataBase.MakeInParam("@SizeX", SqlDbType.Int, 4, (object)adspicinfo.SizeX), DataBase.MakeInParam("@SizeY", SqlDbType.Int, 4, (object)adspicinfo.SizeY)) > 0;
        }
    }
}
