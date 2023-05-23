using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using viviapi.BLL.User;
using viviapi.Model;
using viviapi.Model.User;

namespace viviapi.BLL
{
    public class CPA
    {
        public static int Add(RegUserInfo reguserinfo)
        {
            SqlParameter sqlParameter = DataBase.MakeOutParam("@ID", SqlDbType.Int, 4);
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "RegUser_ADD", sqlParameter, DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)reguserinfo.Uid), DataBase.MakeInParam("@Cid", SqlDbType.Int, 4, (object)reguserinfo.Cid), DataBase.MakeInParam("@UserId", SqlDbType.Int, 4, (object)reguserinfo.UserId), DataBase.MakeInParam("@AdsType", SqlDbType.TinyInt, 1, (object)reguserinfo.AdsType), DataBase.MakeInParam("@Prices", SqlDbType.Decimal, 8, (object)reguserinfo.Prices), DataBase.MakeInParam("@Status", SqlDbType.TinyInt, 1, (object)reguserinfo.Status), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)reguserinfo.AddTime)) == 1)
                return (int)sqlParameter.Value;
            return 0;
        }

        public static DataTable CountByUid(int uid, int cid, int status, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (cid > 0)
                list.Add(DataBase.MakeInParam("@cid", SqlDbType.Int, 4, (object)cid));
            if (status != 999)
                list.Add(DataBase.MakeInParam("@Status", SqlDbType.Int, 4, (object)status));
            list.Add(DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)uid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "CPA_CountByUid", list.ToArray()).Tables[0];
        }

        public static int GetCountByChannel(int cid)
        {
            string commandText = string.Concat(new object[4]
            {
        (object) "SELECT ISNULL(COUNT(*),0) FROM RegUser WHERE Status<>",
        (object) 1,
        (object) " AND Cid=",
        (object) cid
            });
            int num = 0;
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, commandText);
            if (sqlDataReader.Read())
                num = (int)sqlDataReader[0];
            sqlDataReader.Close();
            return num;
        }

        public static DataTable GetCountByHour(int uid, DateTime date)
        {
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "CPA_CountByHour", new SqlParameter[2]
            {
        DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object) uid),
        DataBase.MakeInParam("@date", SqlDbType.DateTime, 8, (object) date)
            }).Tables[0];
        }

        public static DataTable GetDateList(int uid)
        {
            return DataBase.ExecuteDataset(CommandType.Text, "SELECT Convert(varchar(10),[AddTime],120) AS AddTime FROM [RegUser] WHERE AdsType=" + (object)0 + " AND [Status]=" + (string)(object)0 + " AND Uid=" + uid.ToString() + " GROUP BY Convert(varchar(10),[AddTime],120) ORDER BY Convert(varchar(10),[AddTime],120) DESC").Tables[0];
        }

        public static DataTable GetList(int uid)
        {
            return DataBase.ExecuteDataset(CommandType.Text, "SELECT * FROM [RegUser] WHERE AdsType=" + (object)0 + " AND [Status]=" + (string)(object)0 + " AND Uid=" + (string)(object)uid + " ORDER BY ID DESC").Tables[0];
        }

        public static DataTable GetList(int uid, DateTime date)
        {
            return DataBase.ExecuteDataset(CommandType.Text, "SELECT * FROM [RegUser] WHERE AdsType=" + (object)0 + " AND [Status]=" + (string)(object)0 + " AND Uid=" + (string)(object)uid + " AND convert(char(10),[AddTime],120)=convert(char(10),'" + date.ToString("yyyy-MM-dd") + "',120) ORDER BY ID DESC").Tables[0];
        }

        public static DataTable GetList(int uid, int status, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (uid != 0)
                list.Add(DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)uid));
            if (status != 999)
                list.Add(DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "CPA_GetList", list.ToArray()).Tables[0];
        }

        public static RegUserInfo GetRegUserInfo(int userid)
        {
            RegUserInfo regUserInfo = new RegUserInfo();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT TOP 1 * FROM [RegUser] WHERE [UserId]=" + (object)userid);
            if (sqlDataReader.Read())
            {
                regUserInfo.AddTime = DateTime.Parse(sqlDataReader["AddTime"].ToString());
                regUserInfo.AdsType = (AdsTypeEnum)int.Parse(sqlDataReader["AdsType"].ToString());
                regUserInfo.Cid = (int)sqlDataReader["Cid"];
                regUserInfo.ID = (int)sqlDataReader["ID"];
                regUserInfo.Prices = Decimal.Parse(sqlDataReader["Prices"].ToString());
                regUserInfo.Status = (CPAInfo.RegUserStatusEnum)int.Parse(sqlDataReader["Status"].ToString());
                regUserInfo.Uid = (int)sqlDataReader["Uid"];
                regUserInfo.UserId = (int)sqlDataReader["UserId"];
            }
            sqlDataReader.Close();
            return regUserInfo;
        }

        public static UserInfo GetUserInfoByUserId(int userid)
        {
            UserInfo userInfo = new UserInfo();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT TOP 1 Uid FROM RegUser WHERE UserId=" + (object)userid);
            if (sqlDataReader.Read())
                userInfo = UserFactory.GetModel((int)sqlDataReader["Uid"]);
            sqlDataReader.Close();
            return userInfo;
        }

        public static DataTable GetUserList(DateTime stime, DateTime etime)
        {
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRegUserList", new SqlParameter[2]
            {
        DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object) stime),
        DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object) etime)
            }).Tables[0];
        }

        public static DataTable GetUserList(AdsTypeEnum adstype, DateTime stime, DateTime etime)
        {
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRegUserList", DataBase.MakeInParam("@adstype", SqlDbType.TinyInt, 1, (object)adstype), DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime), DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime)).Tables[0];
        }

        public static WebSiteInfo GetWebSiteInfoByUserId(int userid)
        {
            WebSiteInfo webSiteInfo = new WebSiteInfo();
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "SELECT TOP 1 Cid FROM RegUser WHERE UserId=" + (object)userid);
            if (sqlDataReader.Read())
                webSiteInfo = WebSiteFactory.GetWebSiteInfoById((int)sqlDataReader["Cid"]);
            sqlDataReader.Close();
            return webSiteInfo;
        }
    }
}
