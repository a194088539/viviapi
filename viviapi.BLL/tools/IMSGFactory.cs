namespace viviapi.BLL
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.Model;
    using viviLib.Data;
    using viviLib.ExceptionHandling;

    public class IMSGFactory
    {
        internal const string SQL_TABLE = "msg";
        internal const string SQL_TABLE_FIELD = "[ID]\r\n      ,[msg_from]\r\n      ,[msg_to]\r\n      ,[msg_content]\r\n      ,[msg_addtime]\r\n      ,[msg_title]\r\n      ,[isRead],[msg_fromname]";

        public static int Add(IMSG model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Msg(");
            builder.Append("msg_from,msg_to,msg_content,msg_addtime,msg_title,msg_fromname)");
            builder.Append(" values (");
            builder.Append("@msg_from,@msg_to,@msg_content,@msg_addtime,@msg_title,@msg_fromname)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@msg_from", SqlDbType.Int, 4), new SqlParameter("@msg_to", SqlDbType.Int, 4), new SqlParameter("@msg_content", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@msg_addtime", SqlDbType.DateTime), new SqlParameter("@msg_title", SqlDbType.NVarChar, 50), new SqlParameter("@msg_fromname", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = model.msg_from;
            commandParameters[1].Value = model.msg_to;
            commandParameters[2].Value = model.msg_content;
            commandParameters[3].Value = model.msg_addtime;
            commandParameters[4].Value = model.msg_title;
            commandParameters[5].Value = model.msg_fromname;
            object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
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
                        if (!(str2 == "msg_to"))
                        {
                            if (str2 == "msg_from")
                            {
                                goto Label_00D0;
                            }
                            if (str2 == "stime")
                            {
                                goto Label_0109;
                            }
                            if (str2 == "etime")
                            {
                                goto Label_013E;
                            }
                        }
                        else
                        {
                            builder.Append(" AND [msg_to] = @msg_to");
                            parameter = new SqlParameter("@msg_to", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                        }
                    }
                    goto Label_0173;
                Label_00D0:
                    builder.Append(" AND [msg_from] = @msg_from");
                    parameter = new SqlParameter("@msg_from", SqlDbType.Int);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_0173;
                Label_0109:
                    builder.Append(" AND [msg_addtime] > @stime");
                    parameter = new SqlParameter("@stime", SqlDbType.DateTime2);
                    parameter.Value = (string)param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_0173;
                Label_013E:
                    builder.Append(" AND [msg_addtime] < @etime");
                    parameter = new SqlParameter("@etime", SqlDbType.DateTime2);
                    parameter.Value = (string)param2.ParamValue;
                    paramList.Add(parameter);
                Label_0173:;
                }
            }
            return builder.ToString();
        }

        public static bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Msg ");
            builder.Append(" where ID=@ID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            commandParameters[0].Value = ID;
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public static bool DeleteList(string IDlist)
        {
            string str = string.Empty;
            foreach (string str2 in IDlist.Split(new char[] { ',' }))
            {
                int result = 0;
                if (int.TryParse(str2, out result))
                {
                    str = str + str2 + ",";
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.Length - 1);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Msg ");
            builder.Append(" where ID in (" + str + ")  ");
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString()) > 0);
        }

        public static bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Msg");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            commandParameters[0].Value = ID;
            object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
            if ((obj2 == null) || (obj2 == DBNull.Value))
            {
                return false;
            }
            if (int.Parse(obj2.ToString()) <= 0)
            {
                return false;
            }
            return true;
        }

        public static IMSG GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,msg_from,msg_to,msg_content,msg_addtime,msg_title from Msg ");
            builder.Append(" where ID=@ID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            commandParameters[0].Value = ID;
            IMSG imsg = new IMSG();
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    imsg.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
                }
                if (set.Tables[0].Rows[0]["msg_from"].ToString() != "")
                {
                    imsg.msg_from = new int?(int.Parse(set.Tables[0].Rows[0]["msg_from"].ToString()));
                }
                if (set.Tables[0].Rows[0]["msg_to"].ToString() != "")
                {
                    imsg.msg_to = new int?(int.Parse(set.Tables[0].Rows[0]["msg_to"].ToString()));
                }
                imsg.msg_content = set.Tables[0].Rows[0]["msg_content"].ToString();
                if (set.Tables[0].Rows[0]["msg_addtime"].ToString() != "")
                {
                    imsg.msg_addtime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["msg_addtime"].ToString()));
                }
                imsg.msg_title = set.Tables[0].Rows[0]["msg_title"].ToString();
                return imsg;
            }
            return null;
        }

        public static IMSG GetModel(int ID, int msg_to)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,msg_from,msg_to,msg_content,msg_addtime,msg_title from Msg ");
            builder.Append(" where ID=@ID and msg_to  = @msg_to");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@msg_to", SqlDbType.Int, 4) };
            commandParameters[0].Value = ID;
            commandParameters[1].Value = msg_to;
            IMSG imsg = new IMSG();
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    imsg.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
                }
                if (set.Tables[0].Rows[0]["msg_from"].ToString() != "")
                {
                    imsg.msg_from = new int?(int.Parse(set.Tables[0].Rows[0]["msg_from"].ToString()));
                }
                if (set.Tables[0].Rows[0]["msg_to"].ToString() != "")
                {
                    imsg.msg_to = new int?(int.Parse(set.Tables[0].Rows[0]["msg_to"].ToString()));
                }
                imsg.msg_content = set.Tables[0].Rows[0]["msg_content"].ToString();
                if (set.Tables[0].Rows[0]["msg_addtime"].ToString() != "")
                {
                    imsg.msg_addtime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["msg_addtime"].ToString()));
                }
                imsg.msg_title = set.Tables[0].Rows[0]["msg_title"].ToString();
                return imsg;
            }
            return null;
        }

        public static IMSG GetModelByTo(int msg_to)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,msg_from,msg_to,msg_content,msg_addtime,msg_title from Msg ");
            builder.Append(" where msg_to=@msg_to");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@msg_to", SqlDbType.Int, 4) };
            commandParameters[0].Value = msg_to;
            IMSG imsg = new IMSG();
            DataSet set = DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    imsg.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
                }
                if (set.Tables[0].Rows[0]["msg_from"].ToString() != "")
                {
                    imsg.msg_from = new int?(int.Parse(set.Tables[0].Rows[0]["msg_from"].ToString()));
                }
                if (set.Tables[0].Rows[0]["msg_to"].ToString() != "")
                {
                    imsg.msg_to = new int?(int.Parse(set.Tables[0].Rows[0]["msg_to"].ToString()));
                }
                imsg.msg_content = set.Tables[0].Rows[0]["msg_content"].ToString();
                if (set.Tables[0].Rows[0]["msg_addtime"].ToString() != "")
                {
                    imsg.msg_addtime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["msg_addtime"].ToString()));
                }
                imsg.msg_title = set.Tables[0].Rows[0]["msg_title"].ToString();
                return imsg;
            }
            return null;
        }

        public static int GetUserMsgCount(int userId)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userid", SqlDbType.Int, 4) };
                commandParameters[0].Value = userId;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_msg_getusercount", commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static bool IsRead(int uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Msg");
            builder.Append(" where msg_to=@ID and (isRead is null or isRead = 0) ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            commandParameters[0].Value = uid;
            object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
            if ((obj2 == null) || (obj2 == DBNull.Value))
            {
                return false;
            }
            if (int.Parse(obj2.ToString()) <= 0)
            {
                return false;
            }
            return true;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "msg";
                string key = "[ID]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "ID desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[ID]\r\n      ,[msg_from]\r\n      ,[msg_to]\r\n      ,[msg_content]\r\n      ,[msg_addtime]\r\n      ,[msg_title]\r\n      ,[isRead],[msg_fromname]", tables, wheres, orderby, key, pageSize, page, false);
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public static bool Update(IMSG model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Msg set ");
            builder.Append("msg_from=@msg_from,");
            builder.Append("msg_to=@msg_to,");
            builder.Append("msg_content=@msg_content,");
            builder.Append("msg_addtime=@msg_addtime,");
            builder.Append("msg_title=@msg_title,");
            builder.Append("isRead=@isRead");
            builder.Append(" where ID=@ID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@msg_from", SqlDbType.Int, 4), new SqlParameter("@msg_to", SqlDbType.Int, 4), new SqlParameter("@msg_content", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@msg_addtime", SqlDbType.DateTime), new SqlParameter("@msg_title", SqlDbType.NVarChar, 50), new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@isRead", SqlDbType.Bit, 1) };
            commandParameters[0].Value = model.msg_from;
            commandParameters[1].Value = model.msg_to;
            commandParameters[2].Value = model.msg_content;
            commandParameters[3].Value = model.msg_addtime;
            commandParameters[4].Value = model.msg_title;
            commandParameters[5].Value = model.ID;
            commandParameters[6].Value = model.isRead;
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

