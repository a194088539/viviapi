namespace viviapi.BLL.Channel
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using viviapi.BLL.Sys;
    using viviapi.Cache;
    using viviapi.Model.Channel;
    using viviLib.ExceptionHandling;
    public class ChannelType
    {
        public static string CHANNELTYPE_CACHEKEY = (Constant.Cache_Mark + "CHANNEL_TYPE");
        internal static string SQL_TABLE = "channeltype";
        internal static string SQL_TABLE_FIELD = "[id]\r\n      ,[typeId]\r\n      ,[code]\r\n      ,[classid]\r\n      ,[modetypename]\r\n      ,[isOpen]\r\n      ,[supplier]\r\n      ,[supprate]\r\n      ,[addtime]\r\n      ,[sort]\r\n      ,[release]\r\n      ,[runmode]\r\n      ,[runset]";

        public static int Add(ChannelTypeInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@classid", SqlDbType.TinyInt, 1), new SqlParameter("@typeId", SqlDbType.Int, 4), new SqlParameter("@modetypename", SqlDbType.VarChar, 50), new SqlParameter("@isOpen", SqlDbType.TinyInt, 1), new SqlParameter("@supplier", SqlDbType.Int, 4), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@sort", SqlDbType.Int, 4), new SqlParameter("@release", SqlDbType.Bit, 1), new SqlParameter("@runmode", SqlDbType.TinyInt, 4), new SqlParameter("@runset", SqlDbType.VarChar, 0x3e8) };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.Class;
                commandParameters[2].Value = model.typeId;
                commandParameters[3].Value = model.modetypename;
                commandParameters[4].Value = (int)model.isOpen;
                commandParameters[5].Value = model.supplier;
                commandParameters[6].Value = model.addtime;
                commandParameters[7].Value = model.sort;
                commandParameters[8].Value = model.release;
                commandParameters[9].Value = model.runmode;
                commandParameters[10].Value = model.runset;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channeltype_add", commandParameters);
                int num = (int)commandParameters[0].Value;
                if (num > 0)
                {
                    ClearCache();
                }
                return num;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static bool CheckCardFormat(int typeId, string cardno, string cardpwd, int facevalue)
        {
            bool flag = false;
            string str = @"^\d{17}$";
            string str2 = @"^\d{18}$";
            if (typeId == 0x67)
            {
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                if (!flag)
                {
                    str = @"^\d{10}$";
                    str2 = @"^\d{8}$";
                    if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    str = @"^\d{16}$";
                    str2 = @"^\d{17}$";
                    if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    str = @"^\d{16}$";
                    str2 = @"^\d{21}$";
                    if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            if (typeId == 0x68)
            {
                str = "^[0-9a-zA-Z]{15}$";
                str2 = @"^\d{8,9}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x69)
            {
                str = @"^\d{16}$";
                str2 = @"^\d{8}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x6a)
            {
                str = @"^\d{16}$";
                str2 = @"^\d{16}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x6b)
            {
                str = @"^\d{9}$";
                str2 = @"^\d{12}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x6c)
            {
                str = @"^\d{15}$";
                str2 = @"^\d{19}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 110)
            {
                str = @"^\d{13}$";
                str2 = @"^\d{9}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x6f)
            {
                str = @"^\d{10}$";
                str2 = @"^\d{15}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x70)
            {
                str = @"^\d{20}$";
                str2 = @"^\d{12}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x71)
            {
                str = @"^\d{19}$";
                str2 = @"^\d{18}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x75)
            {
                str = @"^\d{15}$";
                str2 = @"^\d{15}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x76)
            {
                str = @"^\d{15}$";
                str2 = @"^\d{8}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                return flag;
            }
            if (typeId == 0x77)
            {
                str = @"^[a-zA-Z]{2}\d{10}$";
                str2 = @"^\d{15}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
                if (flag)
                {
                    return flag;
                }
                str = @"^[a-zA-Z]{2}\d{8}$";
                str2 = @"^\d{15}$";
                if (QuickValidate(str, cardno) && QuickValidate(str2, cardpwd))
                {
                    flag = true;
                }
            }
            return flag;
        }

        private static void ClearCache()
        {
            string objId = CHANNELTYPE_CACHEKEY;
            WebCache.GetCacheService().RemoveObject(objId);
            objId = viviapi.BLL.Channel.Channel.CHANEL_CACHEKEY;
            WebCache.GetCacheService().RemoveObject(objId);
        }

        public static DataTable GetCacheList()
        {
            try
            {
                string objId = CHANNELTYPE_CACHEKEY;
                DataSet o = new DataSet();
                o = (DataSet)WebCache.GetCacheService().RetrieveObject(objId);
                if (o == null)
                {
                    SqlDependency dependency = DataBase.AddSqlDependency(objId, SQL_TABLE, SQL_TABLE_FIELD, "", null);
                    o = GetList(true);
                    WebCache.GetCacheService().AddObject(objId, o);
                }
                return o.Tables[0];
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static ChannelTypeInfo GetCacheModel(int typeId)
        {
            try
            {
                DataTable cacheList = GetCacheList();
                if ((cacheList == null) || (cacheList.Rows.Count <= 0))
                {
                    return null;
                }
                DataRow[] rowArray = cacheList.Select("typeId=" + typeId.ToString());
                if ((rowArray == null) || (rowArray.Length <= 0))
                {
                    return null;
                }
                return GetInfoFromRow(rowArray[0]);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static int GetChannelTypeStatus(int typeId, int userId)
        {
            int num = 0;
            ChannelTypeInfo cacheModel = GetCacheModel(typeId);
            if (cacheModel != null)
            {
                int suppid = 0;
                switch (cacheModel.isOpen)
                {
                    case OpenEnum.AllClose:
                        num = 0;
                        break;

                    case OpenEnum.AllOpen:
                        num = 1;
                        break;

                    case OpenEnum.Close:
                        num = viviapi.BLL.Channel.Channel.GetChanelSysStatus(4, userId, string.Empty, typeId, ref suppid);
                        break;

                    case OpenEnum.Open:
                        num = viviapi.BLL.Channel.Channel.GetChanelSysStatus(8, userId, string.Empty, typeId, ref suppid);
                        break;
                }
                if (num == 1)
                {
                    num = viviapi.BLL.Channel.Channel.GetUserOpenStatus(userId, string.Empty, typeId, 1);
                }
            }
            return num;
        }

        internal static ChannelTypeInfo GetInfoFromRow(DataRow dr)
        {
            ChannelTypeInfo info = new ChannelTypeInfo();
            if (dr["id"].ToString() != "")
            {
                info.id = int.Parse(dr["id"].ToString());
            }
            if (dr["classid"].ToString() != "")
            {
                info.Class = (ChannelClassEnum)int.Parse(dr["classid"].ToString());
            }
            if (dr["typeId"].ToString() != "")
            {
                info.typeId = int.Parse(dr["typeId"].ToString());
            }
            info.modetypename = dr["modetypename"].ToString();
            info.code = dr["code"].ToString();
            if (dr["isOpen"].ToString() != "")
            {
                info.isOpen = (OpenEnum)int.Parse(dr["isOpen"].ToString());
            }
            if (dr["supplier"].ToString() != "")
            {
                info.supplier = int.Parse(dr["supplier"].ToString());
            }
            if (dr["suppRate"].ToString() != "")
            {
                info.supprate = Convert.ToDecimal(dr["suppRate"]);
            }
            if (dr["sort"].ToString() != "")
            {
                info.sort = new int?(int.Parse(dr["sort"].ToString()));
            }
            if (dr["release"].ToString() != "")
            {
                if ((dr["release"].ToString() == "1") || (dr["release"].ToString().ToLower() == "true"))
                {
                    info.release = true;
                }
                else
                {
                    info.release = false;
                }
            }
            if (dr["runmode"].ToString() != "")
            {
                info.runmode = int.Parse(dr["runmode"].ToString());
            }
            info.runset = dr["runset"].ToString();
            return info;
        }

        public static DataSet GetList(bool? release)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@release", SqlDbType.Bit, 1) };
            if (release.HasValue)
            {
                commandParameters[0].Value = release.Value;
            }
            else
            {
                commandParameters[0].Value = DBNull.Value;
            }
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetList", commandParameters);
        }

        public static DataTable GetListByUser(int userid)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userId", SqlDbType.Bit, 1) };
                commandParameters[0].Value = userid;
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetListByUser", commandParameters).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ChannelTypeInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetModel", commandParameters);
                if (set.Tables[0].Rows.Count > 0)
                {
                    return GetInfoFromRow(set.Tables[0].Rows[0]);
                }
                return null;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static ChannelTypeInfo GetModel(int typeId, int userId, out bool enable)
        {
            enable = false;
            int num = 0;
            ChannelTypeInfo cacheModel = GetCacheModel(typeId);
            if (cacheModel == null)
            {
                return null;
            }
            switch (cacheModel.isOpen)
            {
                case OpenEnum.AllClose:
                    num = 0;
                    break;

                case OpenEnum.AllOpen:
                    num = 1;
                    break;

                case OpenEnum.Close:
                    num = GetSysOpenStatus(userId, typeId, 0);
                    break;

                case OpenEnum.Open:
                    num = GetSysOpenStatus(userId, typeId, 1);
                    break;
            }
            if (num == 1)
            {
                num = GetUserOpenStatus(userId, typeId, 1);
            }
            enable = num == 1;
            return cacheModel;
        }

        public static ChannelTypeInfo GetModelByTypeId(int typeId)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeId", SqlDbType.Int, 4) };
                commandParameters[0].Value = typeId;
                DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetByTypeId", commandParameters);
                if (set.Tables[0].Rows.Count > 0)
                {
                    return GetInfoFromRow(set.Tables[0].Rows[0]);
                }
                return null;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static bool GetSysOpenStatus(int userId, int typeId, bool defaultvalue)
        {
            bool flag = defaultvalue;
            ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if ((cacheModel != null) && cacheModel.sysIsOpen.HasValue)
            {
                flag = cacheModel.sysIsOpen.Value;
            }
            return flag;
        }

        public static int GetSysOpenStatus(int userId, int typeId, int defaultvalue)
        {
            int num = defaultvalue;
            ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if ((cacheModel != null) && cacheModel.sysIsOpen.HasValue)
            {
                num = cacheModel.sysIsOpen.Value ? 1 : 0;
            }
            return num;
        }

        public static int GetSysTypeId(int interfaceTypeId)
        {
            int num = interfaceTypeId;
            switch (interfaceTypeId)
            {
                case 1:
                    return 0x6b;

                case 2:
                    return 0x68;

                case 3:
                    return 0x6a;

                case 4:
                    return 0x75;

                case 5:
                    return 0x6f;

                case 6:
                    return 0x70;

                case 7:
                    return 0x69;

                case 8:
                    return 0x6d;

                case 9:
                    return 110;

                case 10:
                    return 0x76;

                case 11:
                    return 0x77;

                case 12:
                    return 0x71;

                case 13:
                    return 0x67;

                case 14:
                    return 0x6c;

                case 15:
                    return 0x74;

                case 0x10:
                    return 0x73;

                case 0x11:
                    return 0x67;

                case 0x12:
                    return 0x67;

                case 0x13:
                    return 0x67;

                case 20:
                    return 0x67;

                case 0x15:
                    return 0x76;

                case 0x16:
                    return 0x77;

                case 0x17:
                    return 0x75;

                case 0x18:
                case 0x19:
                    return num;

                case 0x1a:
                    return 0xd0;

                case 0x1b:
                    return 0xd1;

                case 0x1c:
                    return 210;
            }
            return num;
        }

        public static int GetSysTypeId(int interfaceTypeId, string cardno)
        {
            int num = interfaceTypeId;
            switch (interfaceTypeId)
            {
                case 1:
                    return 0x6b;

                case 2:
                    num = 0x68;
                    if (cardno.StartsWith("80"))
                    {
                        num = 210;
                    }
                    return num;

                case 3:
                    return 0x6a;

                case 4:
                    return 0x75;

                case 5:
                    return 0x6f;

                case 6:
                    return 0x70;

                case 7:
                    return 0x69;

                case 8:
                    return 0x6d;

                case 9:
                    return 110;

                case 10:
                    return 0x76;

                case 11:
                    return 0x77;

                case 12:
                    return 0x71;

                case 13:
                    return 0x67;

                case 14:
                    return 0x6c;

                case 15:
                    return 0x74;

                case 0x10:
                    return 0x73;

                case 0x11:
                    return 0x67;

                case 0x12:
                    return 0x67;

                case 0x13:
                    return 0x67;

                case 20:
                    return 0x67;

                case 0x15:
                    return 0x76;

                case 0x16:
                    return 0x77;

                case 0x17:
                    return 0x75;

                case 0x18:
                case 0x19:
                    return num;

                case 0x1a:
                    return 0xd0;

                case 0x1b:
                    return 0xd1;

                case 0x1c:
                    return 210;
            }
            return num;
        }

        public static bool GetUserOpenStatus(int userId, int typeId, bool defaultvalue)
        {
            bool flag = defaultvalue;
            ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if ((cacheModel != null) && cacheModel.userIsOpen.HasValue)
            {
                flag = cacheModel.userIsOpen.Value;
            }
            return flag;
        }

        public static int GetUserOpenStatus(int userId, int typeId, int defaultvalue)
        {
            int num = defaultvalue;
            ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if ((cacheModel != null) && cacheModel.userIsOpen.HasValue)
            {
                num = cacheModel.userIsOpen.Value ? 1 : 0;
            }
            return num;
        }

        public static bool IsOpen(int typeId, int userId)
        {
            bool flag = false;
            ChannelTypeInfo cacheModel = GetCacheModel(typeId);
            if (cacheModel != null)
            {
                switch (cacheModel.isOpen)
                {
                    case OpenEnum.AllClose:
                        flag = false;
                        break;

                    case OpenEnum.AllOpen:
                        flag = true;
                        break;

                    case OpenEnum.Close:
                        flag = GetSysOpenStatus(userId, typeId, false);
                        break;

                    case OpenEnum.Open:
                        flag = GetSysOpenStatus(userId, typeId, true);
                        break;
                }
                if (flag)
                {
                    flag = GetUserOpenStatus(userId, typeId, true);
                }
            }
            return flag;
        }

        public static bool IsShengFuTong(string cardno)
        {
            if (string.IsNullOrEmpty(cardno))
            {
                return false;
            }
            string str = "^(8013|YA|YB|YC|YD)";
            return QuickValidate(str, cardno);
        }

        public static bool QuickValidate(string _express, string _value)
        {
            Regex regex = new Regex(_express, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if ((_value == null) || (_value.Length == 0))
            {
                return false;
            }
            return regex.IsMatch(_value);
        }

        public static bool Update(ChannelTypeInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@classid", SqlDbType.TinyInt, 1), new SqlParameter("@typeId", SqlDbType.Int, 4), new SqlParameter("@modetypename", SqlDbType.VarChar, 50), new SqlParameter("@isOpen", SqlDbType.TinyInt, 1), new SqlParameter("@supplier", SqlDbType.Int, 4), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@sort", SqlDbType.Int, 4), new SqlParameter("@release", SqlDbType.Bit, 1), new SqlParameter("@runmode", SqlDbType.TinyInt, 4), new SqlParameter("@runset", SqlDbType.VarChar, 0x3e8) };
                commandParameters[0].Value = model.id;
                commandParameters[1].Value = model.Class;
                commandParameters[2].Value = model.typeId;
                commandParameters[3].Value = model.modetypename;
                commandParameters[4].Value = (int)model.isOpen;
                commandParameters[5].Value = model.supplier;
                commandParameters[6].Value = DateTime.Now;
                commandParameters[7].Value = model.sort;
                commandParameters[8].Value = model.release;
                commandParameters[9].Value = model.runmode;
                commandParameters[10].Value = model.runset;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channeltype_Update", commandParameters) > 0;
                if (flag)
                {
                    ClearCache();
                }
                return flag;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
    }
}

