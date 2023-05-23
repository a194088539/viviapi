using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.BLL.Sys;
using viviapi.Cache;
using viviapi.Model.Channel;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Channel
{
    public class ChannelTypeUsers
    {
        public static string ChannelTypeUsers_CACHEKEY = Constant.Cache_Mark + "CHANNEL_TYPE_USER_{0}";
        internal static string SQL_TABLE = "channeltypeusers";
        internal static string SQL_TABLE_FIELD = "[id],[typeId],[userId],[userIsOpen],[suppid],[sysIsOpen],[updateTime]";

        public static int Add(ChannelTypeUserInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[6]
                {
          new SqlParameter("@typeId", SqlDbType.Int, 4),
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@userIsOpen", SqlDbType.Bit, 1),
          new SqlParameter("@sysIsOpen", SqlDbType.Bit, 1),
          new SqlParameter("@addTime", SqlDbType.DateTime),
          new SqlParameter("@updateTime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)model.typeId;
                sqlParameterArray[1].Value = (object)model.userId;
                sqlParameterArray[2].Value = (object)model.userIsOpen;
                sqlParameterArray[3].Value = (object)model.sysIsOpen;
                sqlParameterArray[4].Value = (object)model.addTime;
                sqlParameterArray[5].Value = (object)model.updateTime;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channeltypeusers_add", sqlParameterArray);
                if (obj == null)
                    return 0;
                ChannelTypeUsers.ClearCache(model.userId);
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static int AddSupp(ChannelTypeUserInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[5]
                {
          new SqlParameter("@typeId", SqlDbType.Int, 4),
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@suppid", SqlDbType.Int, 4),
          new SqlParameter("@addTime", SqlDbType.DateTime),
          new SqlParameter("@updateTime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)model.typeId;
                sqlParameterArray[1].Value = (object)model.userId;
                sqlParameterArray[2].Value = (object)model.suppid;
                sqlParameterArray[3].Value = (object)model.addTime;
                sqlParameterArray[4].Value = (object)model.updateTime;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channeltypeusers_addsuppid", sqlParameterArray);
                if (obj == null)
                    return 0;
                ChannelTypeUsers.ClearCache(model.userId);
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static int Exists(int userid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_channeltypeusers_exists", sqlParameterArray);
                if (obj == null || obj == DBNull.Value)
                    return 0;
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Setting(int userId, int isOpen)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@isOpen", SqlDbType.TinyInt),
          new SqlParameter("@addtime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Value = (object)userId;
                sqlParameterArray[1].Value = (object)isOpen;
                sqlParameterArray[2].Value = (object)DateTime.Now;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channeltypeusers_Setting", sqlParameterArray) > 0;
                if (flag)
                    ChannelTypeUsers.ClearCache(userId);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static ChannelTypeUserInfo GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltypeusers_GetModel", sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count > 0)
                return ChannelTypeUsers.GetModelFromRow(dataSet.Tables[0].Rows[0]);
            return (ChannelTypeUserInfo)null;
        }

        public static ChannelTypeUserInfo GetModel(int userId, int typeId)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@userId", SqlDbType.Int, 4),
        new SqlParameter("@typeId", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)userId;
            sqlParameterArray[1].Value = (object)typeId;
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltypeusers_GetbyKey", sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count > 0)
                return ChannelTypeUsers.GetModelFromRow(dataSet.Tables[0].Rows[0]);
            return (ChannelTypeUserInfo)null;
        }

        public static ChannelTypeUserInfo GetCacheModel(int userId, int typeId)
        {
            DataTable list = ChannelTypeUsers.GetList(userId, true);
            if (list == null || list.Rows.Count <= 0)
                return (ChannelTypeUserInfo)null;
            DataRow[] dataRowArray = list.Select("typeId=" + typeId.ToString());
            if (dataRowArray != null && dataRowArray.Length > 0)
                return ChannelTypeUsers.GetModelFromRow(dataRowArray[0]);
            return (ChannelTypeUserInfo)null;
        }

        public static ChannelTypeUserInfo GetModelFromRow(DataRow dr)
        {
            ChannelTypeUserInfo channelTypeUserInfo = new ChannelTypeUserInfo();
            if (dr["id"].ToString() != "")
                channelTypeUserInfo.id = int.Parse(dr["id"].ToString());
            if (dr["typeId"].ToString() != "")
                channelTypeUserInfo.typeId = int.Parse(dr["typeId"].ToString());
            if (dr["userId"].ToString() != "")
                channelTypeUserInfo.userId = int.Parse(dr["userId"].ToString());
            if (dr["userIsOpen"].ToString() != "")
            {
                int num = dr["userIsOpen"].ToString() == "1" ? 0 : (!(dr["userIsOpen"].ToString().ToLower() == "true") ? 1 : 0);
                channelTypeUserInfo.userIsOpen = num != 0 ? new bool?(false) : new bool?(true);
            }
            else
                channelTypeUserInfo.userIsOpen = new bool?();
            if (dr["sysIsOpen"].ToString() != "")
            {
                int num = dr["sysIsOpen"].ToString() == "1" ? 0 : (!(dr["sysIsOpen"].ToString().ToLower() == "true") ? 1 : 0);
                channelTypeUserInfo.sysIsOpen = num != 0 ? new bool?(false) : new bool?(true);
            }
            else
                channelTypeUserInfo.sysIsOpen = new bool?();
            if (dr.Table.Columns.Contains("addTime") && dr["addTime"].ToString() != "")
                channelTypeUserInfo.addTime = new DateTime?(DateTime.Parse(dr["addTime"].ToString()));
            if (dr.Table.Columns.Contains("updateTime") && dr["updateTime"].ToString() != "")
                channelTypeUserInfo.updateTime = new DateTime?(DateTime.Parse(dr["updateTime"].ToString()));
            channelTypeUserInfo.suppid = new int?();
            if (dr["suppid"].ToString() != "")
                channelTypeUserInfo.suppid = new int?(int.Parse(dr["suppid"].ToString()));
            return channelTypeUserInfo;
        }

        public static DataTable GetList(int userId, bool iscache)
        {
            try
            {
                string str = string.Format(ChannelTypeUsers.ChannelTypeUsers_CACHEKEY, (object)userId);
                DataSet dataSet = new DataSet();
                if (iscache)
                    dataSet = (DataSet)WebCache.GetCacheService().RetrieveObject(str);
                if (dataSet == null || !iscache)
                {
                    IDictionary<string, object> parameters = (IDictionary<string, object>)new Dictionary<string, object>();
                    parameters.Add("userId", (object)userId);
                    DataBase.AddSqlDependency(str, ChannelTypeUsers.SQL_TABLE, ChannelTypeUsers.SQL_TABLE_FIELD, "[userId]=@userId", parameters);
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("select [id],[suppid],[typeId],[userId],[userIsOpen],[sysIsOpen],addTime,updateTime ");
                    stringBuilder.Append(" FROM [ChannelTypeUsers] ");
                    dataSet = DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString() + " where userId=" + userId.ToString());
                    if (iscache)
                        WebCache.GetCacheService().AddObject(str, (object)dataSet);
                }
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        private static void ClearCache(int userId)
        {
            WebCache.GetCacheService().RemoveObject(string.Format(ChannelTypeUsers.ChannelTypeUsers_CACHEKEY, (object)userId));
        }
    }
}
