namespace viviapi.BLL.Channel
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Security.Cryptography;
    using System.Text;
    using viviapi.BLL.Sys;
    using viviapi.Cache;
    using viviapi.Model.Channel;
    using viviLib.Data;
    using viviLib.ExceptionHandling;
    using viviLib.Utils;
    public class Channel
    {
        public static string CHANEL_CACHEKEY = (Constant.Cache_Mark + "CHANNELS");
        internal static string SQL_TABLE = "channel";
        internal static string SQL_TABLE_FIELD = "[id]\r\n      ,[code]\r\n      ,[typeId]\r\n      ,[supplier]\r\n      ,[supprate]\r\n      ,[modeName]\r\n      ,[modeEnName]\r\n      ,[faceValue]\r\n      ,[isOpen]\r\n      ,[addtime]\r\n      ,[sort]";

        public static int Add(ChannelInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@code", SqlDbType.VarChar, 10), new SqlParameter("@typeId", SqlDbType.Int, 4), new SqlParameter("@supplier", SqlDbType.Int, 4), new SqlParameter("@modeName", SqlDbType.VarChar, 50), new SqlParameter("@modeEnName", SqlDbType.VarChar, 50), new SqlParameter("@faceValue", SqlDbType.Int, 4), new SqlParameter("@isOpen", SqlDbType.TinyInt, 1), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@sort", SqlDbType.Int, 4) };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.code;
                commandParameters[2].Value = model.typeId;
                commandParameters[3].Value = model.supplier;
                commandParameters[4].Value = model.modeName;
                commandParameters[5].Value = model.modeEnName;
                commandParameters[6].Value = model.faceValue;
                commandParameters[7].Value = model.isOpen;
                commandParameters[8].Value = model.addtime;
                commandParameters[9].Value = model.sort;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channel_ADD", commandParameters);
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

        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder builder = new StringBuilder(" 1 = 1");
            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam param2 = param[i];
                    string str2 = param2.ParamKey.Trim().ToLower();
                    if (str2 != null)
                    {
                        if (!(str2 == "id"))
                        {
                            if (str2 == "typeid")
                            {
                                goto Label_00B9;
                            }
                            if (str2 == "cardtype")
                            {
                                goto Label_00F3;
                            }
                        }
                        else
                        {
                            builder.Append(" AND [id] = @id");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                        }
                    }
                    continue;
                Label_00B9:
                    builder.Append(" AND [typeId] = @typeId");
                    parameter = new SqlParameter("@typeId", SqlDbType.Int, 4);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                    continue;
                Label_00F3:
                    builder.Append(" AND [typeId] > 110 ");
                }
            }
            return builder.ToString();
        }

        private static void ClearCache()
        {
            string objId = CHANEL_CACHEKEY;
            WebCache.GetCacheService().RemoveObject(objId);
        }

        public static bool Delete(int id)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = id;
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channel_Delete", commandParameters) > 0);
        }

        public static DataSet GetBankChanels(int pageindex, int pagesize, int userid, int typeid, int facevalue, int chanelstatus)
        {
            string commandText = "select count(*) as total\r\nfrom (\r\nselect\r\n\tdbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus\r\nfrom\r\n\tchanneltype a left join channel b on a.typeId = b.typeId\r\n\t\t\t\t  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId\r\nwhere\r\n\ta.release = 1 and a.typeid <= 102\r\n\tand (a.typeid = @typeid or @typeid is null)\r\n\tand (b.faceValue = @faceValue or @faceValue is null)\r\n\t) dd\r\nwhere (dd.chanelstatus = @chanelstatus or @chanelstatus is null)\r\n\r\nselect typeid,code,faceValue,modetypename,chanelstatus,modeName\r\nfrom (\r\nselect\r\n\ta.typeid,\r\n\tb.code,\r\n\tb.faceValue,\r\n\ta.modetypename,b.modeName,\r\n\tdbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus\r\n\t,ROW_NUMBER() OVER(ORDER BY a.typeid,b.facevalue) AS P_ROW \r\nfrom\r\n\tchanneltype a left join channel b on a.typeId = b.typeId\r\n\t\t\t\t  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId\r\nwhere\r\n\ta.release = 1 and a.typeid <= 102\r\n\tand (a.typeid = @typeid or @typeid is null)\r\n\tand (b.faceValue = @faceValue or @faceValue is null)\r\n\t) dd\r\nwhere (dd.chanelstatus = @chanelstatus or @chanelstatus is null) \r\nand dd.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@page", pageindex - 1));
            list.Add(new SqlParameter("@pagesize", pagesize));
            list.Add(new SqlParameter("@userId", userid));
            if (typeid > 0)
            {
                list.Add(new SqlParameter("@typeid", typeid));
            }
            else
            {
                list.Add(new SqlParameter("@typeid", DBNull.Value));
            }
            if (facevalue > 0)
            {
                list.Add(new SqlParameter("@facevalue", facevalue));
            }
            else
            {
                list.Add(new SqlParameter("@facevalue", DBNull.Value));
            }
            if (chanelstatus > -1)
            {
                list.Add(new SqlParameter("@chanelstatus", chanelstatus));
            }
            else
            {
                list.Add(new SqlParameter("@chanelstatus", DBNull.Value));
            }
            return DataBase.ExecuteDataset(CommandType.Text, commandText, list.ToArray());
        }

        public static DataTable GetCacheList()
        {
            try
            {
                string objId = CHANEL_CACHEKEY;
                DataSet o = new DataSet();
                o = (DataSet)WebCache.GetCacheService().RetrieveObject(objId);
                if (o == null)
                {
                    SqlDependency dependency = DataBase.AddSqlDependency(objId, SQL_TABLE, SQL_TABLE_FIELD, "", null);
                    o = GetList(0);
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

        public static ChannelInfo GetCacheModel(string code)
        {
            try
            {
                DataTable cacheList = GetCacheList();
                if (cacheList == null)
                {
                    return null;
                }
                DataRow[] rowArray = cacheList.Select("code='" + code + "'");
                if ((rowArray == null) || (rowArray.Length <= 0))
                {
                    return null;
                }
                return GetModelFromRow(rowArray[0]);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static DataTable GetCardChanels(int userid, int typeid, int facevalue, int chanelstatus)
        {
            string commandText = "\r\nselect typeid,code,faceValue,modetypename,chanelstatus\r\nfrom (\r\nselect\r\n\ta.typeid,\r\n\tb.code,\r\n\tb.faceValue,\r\n\ta.modetypename,\r\n\tdbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus\t\r\nfrom\r\n\tchanneltype a left join channel b on a.typeId = b.typeId\r\n\t\t\t\t  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId\r\nwhere\r\n\ta.release = 1 and a.typeid > 102\r\n\tand (a.typeid = @typeid or @typeid is null)\r\n\tand (b.faceValue = @faceValue or @faceValue is null)\r\n\t) dd\r\nwhere (dd.chanelstatus = @chanelstatus or @chanelstatus is null) \r\n";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userId", userid));
            if (typeid > 0)
            {
                list.Add(new SqlParameter("@typeid", typeid));
            }
            else
            {
                list.Add(new SqlParameter("@typeid", DBNull.Value));
            }
            if (facevalue > 0)
            {
                list.Add(new SqlParameter("@facevalue", facevalue));
            }
            else
            {
                list.Add(new SqlParameter("@facevalue", DBNull.Value));
            }
            if (chanelstatus > -1)
            {
                list.Add(new SqlParameter("@chanelstatus", chanelstatus));
            }
            else
            {
                list.Add(new SqlParameter("@chanelstatus", DBNull.Value));
            }
            return DataBase.ExecuteDataset(CommandType.Text, commandText, list.ToArray()).Tables[0];
        }

        public static DataSet GetCardChanels(int pageindex, int pagesize, int userid, int typeid, int facevalue, int chanelstatus)
        {
            string commandText = "select count(*) as total\r\nfrom (\r\nselect\r\n\tdbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus\r\nfrom\r\n\tchanneltype a left join channel b on a.typeId = b.typeId\r\n\t\t\t\t  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId\r\nwhere\r\n\ta.release = 1 and a.typeid > 102\r\n\tand (a.typeid = @typeid or @typeid is null)\r\n\tand (b.faceValue = @faceValue or @faceValue is null)\r\n\t) dd\r\nwhere (dd.chanelstatus = @chanelstatus or @chanelstatus is null)\r\n\r\nselect typeid,code,faceValue,modetypename,chanelstatus\r\nfrom (\r\nselect\r\n\ta.typeid,\r\n\tb.code,\r\n\tb.faceValue,\r\n\ta.modetypename,\r\n\tdbo.f_getuserChanelStatus(a.isOpen,b.isOpen,c.sysIsOpen,c.userIsOpen) as chanelstatus\r\n\t,ROW_NUMBER() OVER(ORDER BY a.typeid,b.facevalue) AS P_ROW \r\nfrom\r\n\tchanneltype a left join channel b on a.typeId = b.typeId\r\n\t\t\t\t  left join channeltypeusers c on a.typeId = c.typeId and c.userId = @userId\r\nwhere\r\n\ta.release = 1 and a.typeid > 102\r\n\tand (a.typeid = @typeid or @typeid is null)\r\n\tand (b.faceValue = @faceValue or @faceValue is null)\r\n\t) dd\r\nwhere (dd.chanelstatus = @chanelstatus or @chanelstatus is null) \r\nand dd.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@page", pageindex - 1));
            list.Add(new SqlParameter("@pagesize", pagesize));
            list.Add(new SqlParameter("@userId", userid));
            if (typeid > 0)
            {
                list.Add(new SqlParameter("@typeid", typeid));
            }
            else
            {
                list.Add(new SqlParameter("@typeid", DBNull.Value));
            }
            if (facevalue > 0)
            {
                list.Add(new SqlParameter("@facevalue", facevalue));
            }
            else
            {
                list.Add(new SqlParameter("@facevalue", DBNull.Value));
            }
            if (chanelstatus > -1)
            {
                list.Add(new SqlParameter("@chanelstatus", chanelstatus));
            }
            else
            {
                list.Add(new SqlParameter("@chanelstatus", DBNull.Value));
            }
            return DataBase.ExecuteDataset(CommandType.Text, commandText, list.ToArray());
        }

        public static int GetChanelSysStatus(int typeStatus, int userId, string chanelNo, int typeId, ref int suppid)
        {
            suppid = -1;
            int num = 0;
            int num2 = -1;
            int num3 = -1;
            ChannelInfo info = null;
            if (!string.IsNullOrEmpty(chanelNo))
            {
                info = GetCacheModel(chanelNo);
            }
            ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if ((info != null) && info.isOpen.HasValue)
            {
                num2 = info.isOpen.Value;
            }
            if ((cacheModel != null) && cacheModel.sysIsOpen.HasValue)
            {
                num3 = cacheModel.sysIsOpen.Value ? 1 : 0;
                if (cacheModel.suppid.HasValue && (cacheModel.suppid.Value > 0))
                {
                    suppid = cacheModel.suppid.Value;
                }
            }
            if (typeStatus == 4)
            {
                if (num2 == -1)
                {
                    num2 = 0;
                }
                if (num3 == -1)
                {
                    num3 = 0;
                }
            }
            else if (typeStatus == 8)
            {
                if (num2 == -1)
                {
                    num2 = 1;
                }
                if (num3 == -1)
                {
                    num3 = 1;
                }
            }
            if ((num2 == 1) && (num3 == 1))
            {
                num = 1;
            }
            return num;
        }

        public static DataSet GetList(int typeId)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeId", SqlDbType.Int) };
                commandParameters[0].Value = typeId;
                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channel_GetList", commandParameters);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static ChannelInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channel_GetModel", commandParameters);
                if (set.Tables[0].Rows.Count > 0)
                {
                    return GetModelFromRow(set.Tables[0].Rows[0]);
                }
                return null;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static ChannelInfo GetModel(string chanelNo, int userId, bool isupdatecurr_channel)
        {
            int num = 0;
            ChannelInfo cacheModel = GetCacheModel(chanelNo);
            if (cacheModel != null)
            {
                int? nullable;
                ChannelTypeInfo info2 = ChannelType.GetCacheModel(cacheModel.typeId);
                if (info2 == null)
                {
                    return null;
                }
                int suppid = -1;
                if (!(cacheModel.supplier.HasValue && !(((nullable = cacheModel.supplier).GetValueOrDefault() == 0) && nullable.HasValue)))
                {
                    cacheModel.supplier = new int?(info2.supplier);
                    cacheModel.supprate = info2.supprate;
                }
                if (isupdatecurr_channel && (info2.runmode == 1))
                {
                    string runset = info2.runset;
                    List<int> list = new List<int>();
                    List<ushort> list2 = new List<ushort>();
                    foreach (string str2 in runset.Split(new char[] { '|' }))
                    {
                        string[] strArray = str2.Split(new char[] { ':' });
                        list.Add(Convert.ToInt32(strArray[0]));
                        list2.Add(Convert.ToUInt16(strArray[1]));
                    }
                    RandomController controller = new RandomController(1);
                    controller.datas = list;
                    controller.weights = list2;
                    Random rand = new Random(GetRandomSeed());
                    int[] numArray = controller.ControllerRandomExtract(rand);
                    cacheModel.supplier = new int?(numArray[0]);
                }
                switch (info2.isOpen)
                {
                    case OpenEnum.AllClose:
                        suppid = GetUserSupp(userId, cacheModel.typeId);
                        num = 0;
                        break;

                    case OpenEnum.AllOpen:
                        suppid = GetUserSupp(userId, cacheModel.typeId);
                        num = 1;
                        break;

                    case OpenEnum.Close:
                        num = GetChanelSysStatus(4, userId, chanelNo, cacheModel.typeId, ref suppid);
                        break;

                    case OpenEnum.Open:
                        num = GetChanelSysStatus(8, userId, chanelNo, cacheModel.typeId, ref suppid);
                        break;
                }
                if (suppid > -1)
                {
                    cacheModel.supplier = new int?(suppid);
                }
                if (num == 1)
                {
                    num = GetUserOpenStatus(userId, chanelNo, cacheModel.typeId, 1);
                }
                cacheModel.isOpen = new int?(num);
            }
            return cacheModel;
        }

        public static ChannelInfo GetModel(int typeId, int value, int userId, bool isupdatecurr_channel)
        {
            try
            {
                DataTable cacheList = GetCacheList();
                if (cacheList == null)
                {
                    return null;
                }
                DataRow[] rowArray = cacheList.Select("typeId=" + typeId.ToString() + " and faceValue=" + value.ToString());
                if ((rowArray == null) || (rowArray.Length <= 0))
                {
                    return null;
                }
                return GetModel(rowArray[0]["code"].ToString(), userId, isupdatecurr_channel);
            }
            catch
            {
                return null;
            }
        }

        public static ChannelInfo GetModelByCode(string code)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@code", SqlDbType.VarChar, 10) };
                commandParameters[0].Value = code;
                DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channel_GetBycode", commandParameters);
                if (set.Tables[0].Rows.Count > 0)
                {
                    return GetModelFromRow(set.Tables[0].Rows[0]);
                }
                return null;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static ChannelInfo GetModelFromRow(DataRow dr)
        {
            ChannelInfo info = new ChannelInfo();
            if (dr["id"].ToString() != "")
            {
                info.id = int.Parse(dr["id"].ToString());
            }
            info.code = dr["code"].ToString();
            if (dr["typeId"].ToString() != "")
            {
                info.typeId = int.Parse(dr["typeId"].ToString());
            }
            if (dr["supplier"].ToString() != "")
            {
                info.supplier = new int?(int.Parse(dr["supplier"].ToString()));
            }
            if (dr["suppRate"].ToString() != "")
            {
                info.supprate = Convert.ToDecimal(dr["suppRate"]);
            }
            info.modeName = dr["modeName"].ToString();
            info.modeEnName = dr["modeEnName"].ToString();
            if (dr["faceValue"].ToString() != "")
            {
                info.faceValue = int.Parse(dr["faceValue"].ToString());
            }
            if (dr["isOpen"].ToString() != "")
            {
                info.isOpen = new int?(int.Parse(dr["isOpen"].ToString()));
            }
            if (dr["sort"].ToString() != "")
            {
                info.sort = new int?(int.Parse(dr["sort"].ToString()));
            }
            return info;
        }

        public static int GetRandomSeed()
        {
            byte[] data = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        public static int GetUserOpenStatus(int userId, string chanelNo, int typeId, int defaultvalue)
        {
            int num = defaultvalue;
            ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if ((cacheModel != null) && cacheModel.userIsOpen.HasValue)
            {
                num = cacheModel.userIsOpen.Value ? 1 : 0;
            }
            return num;
        }

        private static int GetUserSupp(int userId, int typeId)
        {
            int num = -1;
            ChannelTypeUserInfo cacheModel = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if (((cacheModel != null) && cacheModel.suppid.HasValue) && (cacheModel.suppid.Value > 0))
            {
                num = cacheModel.suppid.Value;
            }
            return num;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELD, tables, wheres, orderby, key, pageSize, page, false);
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public static bool Update(ChannelInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@code", SqlDbType.VarChar, 10), new SqlParameter("@typeId", SqlDbType.Int, 4), new SqlParameter("@supplier", SqlDbType.Int, 4), new SqlParameter("@modeName", SqlDbType.VarChar, 50), new SqlParameter("@modeEnName", SqlDbType.VarChar, 50), new SqlParameter("@faceValue", SqlDbType.Int, 4), new SqlParameter("@isOpen", SqlDbType.TinyInt, 1), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@sort", SqlDbType.Int, 4) };
                commandParameters[0].Value = model.id;
                commandParameters[1].Value = model.code;
                commandParameters[2].Value = model.typeId;
                commandParameters[3].Value = model.supplier;
                commandParameters[4].Value = model.modeName;
                commandParameters[5].Value = model.modeEnName;
                commandParameters[6].Value = model.faceValue;
                commandParameters[7].Value = model.isOpen;
                commandParameters[8].Value = DateTime.Now;
                commandParameters[9].Value = model.sort;
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channel_Update", commandParameters) > 0;
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

