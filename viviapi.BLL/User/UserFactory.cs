namespace viviapi.BLL.User
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;
    using viviapi.BLL;
    using viviapi.BLL.Sys;
    using viviapi.Cache;
    using viviapi.Model.User;
    using viviLib.Data;
    using viviLib.ExceptionHandling;
    public class UserFactory
    {
        internal const string FIELD_USER = "[id]\r\n      ,[userName]\r\n      ,[password]\r\n      ,[CPSDrate]\r\n      ,[CVSNrate]\r\n      ,[email]\r\n      ,[qq]\r\n      ,[tel]\r\n      ,[idCard]\r\n      ,[settles]\r\n      ,[status]\r\n      ,[regTime]\r\n      ,[company]\r\n      ,[linkMan]\r\n      ,[agentId]\r\n      ,[siteName]\r\n      ,[siteUrl]\r\n      ,[userType]\r\n      ,[userLevel]\r\n      ,[maxdaytocashTimes]\r\n      ,[apiaccount]\r\n      ,[apikey]\r\n      ,[lastLoginIp]\r\n      ,[lastLoginTime]\r\n      ,[sessionId]\r\n      ,[updatetime]\r\n      ,[DESC]\r\n      ,[userid]\r\n      ,[pmode]\r\n      ,[account]\r\n      ,[payeeName]\r\n      ,[payeeBank]\r\n      ,[bankProvince]\r\n      ,[bankCity]\r\n      ,[bankAddress]\r\n      ,[Integral]\r\n      ,[balance]\r\n      ,[payment]\r\n      ,[unpayment]\r\n      ,[enableAmt]\r\n      ,[manageId]\r\n      ,[isRealNamePass]\r\n      ,[isPhonePass]\r\n      ,[isEmailPass]\r\n      ,[question]\r\n      ,[answer]\r\n      ,[smsNotifyUrl]\r\n      ,[full_name]\r\n      ,[classid]\r\n      ,[Freeze]\r\n      ,[schemename]\r\n      ,[idCardtype]\r\n      ,[msn]\r\n      ,[fax]\r\n      ,[province]\r\n      ,[city]\r\n      ,[zip]\r\n      ,[field1],[levName],[frontPic],[versoPic]";
        internal const string SQL_BASE_TABLE = "userbase";
        internal const string SQL_BASE_TABLE_FIELD = "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug],[frontPic],[versoPic]";
        internal const string SQL_PAYBANK_TABLE = "userspaybank";
        internal const string SQL_PAYBANK_TABLE_FIELD = "[userid],[pmode],[account],[payeeName],[payeeBank],[bankProvince],[bankCity],[bankAddress],[status]";
        internal const string SQL_TABLE = "V_Users";
        internal const string SQL_TABLE_FIELD = "id,userName,password,CPSDrate,CVSNrate,email,qq,tel,idCard,pmode,settles,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,regTime,company,linkMan,agentId,siteName,siteUrl,userType,userLevel,maxdaytocashTimes,apiaccount,apikey,lastLoginIp,lastLoginTime,sessionId,manageId,isRealNamePass,full_name,classid,isdebug,frontPic,versoPic";
        public static string USER_CACHE_KEY = (Constant.Cache_Mark + "USER_{0}");
        internal const string USER_CONTEXT_KEY = "{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}";
        internal const string USER_LOGIN_CLIENT_SESSIONID = "{2A1FA22C-201B-471c-B668-2FCC1C4A121A}";
        internal const string USER_LOGIN_SESSIONID = "{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}";

        public static int Add(UserInfo _userinfo)
        {
            try
            {
                SqlParameter parameter = DataBase.MakeOutParam("@id", SqlDbType.Int, 4);
                SqlParameter[] commandParameters = new SqlParameter[] {
                    parameter, DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, _userinfo.UserName), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, _userinfo.Password), DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, _userinfo.CPSDrate), DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, _userinfo.CVSNrate), DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, _userinfo.Email), DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, _userinfo.QQ), DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, _userinfo.Tel), DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, _userinfo.IdCard), DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, _userinfo.Account), DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, _userinfo.PayeeName), DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, _userinfo.PayeeBank), DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, _userinfo.BankProvince), DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, _userinfo.BankCity), DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, _userinfo.BankAddress), DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, _userinfo.Status),
                    DataBase.MakeInParam("@lastloginip", SqlDbType.VarChar, 50, _userinfo.LastLoginIp), DataBase.MakeInParam("@lastlogintime", SqlDbType.DateTime, 8, _userinfo.LastLoginTime), DataBase.MakeInParam("@regtime", SqlDbType.DateTime, 8, _userinfo.RegTime), DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, _userinfo.AgentId), DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, _userinfo.SiteName), DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, _userinfo.SiteUrl), DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int) _userinfo.UserType), DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int) _userinfo.UserLevel), DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, _userinfo.MaxDayToCashTimes), DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, _userinfo.APIAccount), DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, _userinfo.APIKey), DataBase.MakeInParam("@pmode", SqlDbType.TinyInt, 1, _userinfo.PMode), DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, _userinfo.Settles), DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 0xfa0, _userinfo.Desc), DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, _userinfo.manageId), DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, _userinfo.question),
                    DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, _userinfo.answer), DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 100, _userinfo.full_name), DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, _userinfo.classid), DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, _userinfo.Password2), DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, _userinfo.LinkMan), DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, _userinfo.isdebug), DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, _userinfo.IdCardType), DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, _userinfo.msn), DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, _userinfo.fax), DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, _userinfo.province), DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, _userinfo.city), DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, _userinfo.zip), DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, _userinfo.field1), DataBase.MakeInParam("@isagentDistribution", SqlDbType.TinyInt, 1, _userinfo.isagentDistribution), DataBase.MakeInParam("@cardversion", SqlDbType.TinyInt, 1, _userinfo.cardversion)
                 };
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_add", commandParameters) > 0)
                {
                    _userinfo.ID = (int)parameter.Value;
                    return _userinfo.ID;
                }
                return 0;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        private static string BuilderUpdateLogWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                            if (str2 == "userid")
                            {
                                goto Label_00B9;
                            }
                            if (str2 == "usertype")
                            {
                                goto Label_00F2;
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
                    builder.Append(" AND [userid] = @userid");
                    parameter = new SqlParameter("@userid", SqlDbType.Int);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                    continue;
                Label_00F2:
                    builder.Append(" AND [userType] = @userType");
                    parameter = new SqlParameter("@userType", SqlDbType.Int);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                }
            }
            return builder.ToString();
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
                    switch (param2.ParamKey.Trim().ToLower())
                    {
                        case "id":
                            {
                                builder.Append(" AND [id] = @id");
                                parameter = new SqlParameter("@id", SqlDbType.Int);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "username":
                            {
                                builder.Append(" AND [userName] like @UserName");
                                parameter = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                                parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 20) + "%";
                                paramList.Add(parameter);
                                continue;
                            }
                        case "qq":
                            {
                                builder.Append(" AND [qq] like @qq");
                                parameter = new SqlParameter("@qq", SqlDbType.VarChar, 50);
                                parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 50) + "%";
                                paramList.Add(parameter);
                                continue;
                            }
                        case "tel":
                            {
                                builder.Append(" AND [Tel] like @tel");
                                parameter = new SqlParameter("@tel", SqlDbType.VarChar, 50);
                                parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 50) + "%";
                                paramList.Add(parameter);
                                continue;
                            }
                        case "email":
                            {
                                builder.Append(" AND [email] like @email");
                                parameter = new SqlParameter("@email", SqlDbType.VarChar, 50);
                                parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 50) + "%";
                                paramList.Add(parameter);
                                continue;
                            }
                        case "full_name":
                            {
                                builder.Append(" AND [full_name] like @full_name");
                                parameter = new SqlParameter("@full_name", SqlDbType.VarChar, 50);
                                parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 50) + "%";
                                paramList.Add(parameter);
                                continue;
                            }
                        case "status":
                            {
                                builder.Append(" AND [status] = @status");
                                parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "usertype":
                            {
                                builder.Append(" AND [userType] = @userType");
                                parameter = new SqlParameter("@userType", SqlDbType.TinyInt);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "specialchannel":
                            {
                                string paramValue = (string)param2.ParamValue;
                                if (!(paramValue == "1"))
                                {
                                    break;
                                }
                                builder.Append(" AND exists(select 0 from channeltypeusers where isnull(suppid,0)>0 and userid=v_users.id)");
                                continue;
                            }
                        case "special":
                            {
                                builder.Append(" AND [special] = @special");
                                parameter = new SqlParameter("@special", SqlDbType.TinyInt);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "isrealnamepass":
                            {
                                builder.Append(" AND [isRealNamePass] = @isRealNamePass");
                                parameter = new SqlParameter("@isRealNamePass", SqlDbType.TinyInt);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "userlevel":
                            {
                                builder.Append(" AND [userlevel] = @userlevel");
                                parameter = new SqlParameter("@userlevel", SqlDbType.Int);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "proid":
                            {
                                builder.Append(" AND Exists(select 0 from PromotionUser where PromotionUser.PID = @proid and PromotionUser.RegId=v_users.id)");
                                parameter = new SqlParameter("@proid", SqlDbType.Int);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "manageid":
                            {
                                builder.Append(" AND [manageId] = @manageId");
                                parameter = new SqlParameter("@manageId", SqlDbType.Int);
                                parameter.Value = (int)param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "balance":
                            {
                                builder.AppendFormat(" AND [balance] {0} @balance", param2.CmpOperator);
                                parameter = new SqlParameter("@balance", SqlDbType.Decimal);
                                parameter.Value = param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        case "enableamt":
                            {
                                builder.AppendFormat(" AND [enableAmt] {0} @enableAmt", param2.CmpOperator);
                                parameter = new SqlParameter("@enableAmt", SqlDbType.Decimal);
                                parameter.Value = param2.ParamValue;
                                paramList.Add(parameter);
                                continue;
                            }
                        default:
                            {
                                continue;
                            }
                    }
                    builder.Append(" AND not exists(select 0 from channeltypeusers where isnull(suppid,0)>0 and userid=v_users.id)");
                }
            }
            return builder.ToString();
        }

        public static bool CheckUserOrderId(int userid, string OrderId)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@orderNo", SqlDbType.VarChar, 30, OrderId), DataBase.MakeInParam("@userid", SqlDbType.Int, 4, userid) };
            return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_usersorderid_check", commandParameters));
        }

        public static bool chkAgent(int agentid)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@agentid", SqlDbType.Int, 4) };
                commandParameters[0].Value = agentid;
                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_user_chkagent", commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static void ClearCache(int userId)
        {
            string objId = string.Format(USER_CACHE_KEY, userId);
            WebCache.GetCacheService().RemoveObject(objId);
        }

        public static bool Del(int userId)
        {
            try
            {
                bool flag = false;
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@id", SqlDbType.Int, 4, userId) };
                flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_del", commandParameters) > 0;
                if (flag)
                {
                    ClearCache(userId);
                }
                return flag;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static bool DelUpdateLog(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@id", SqlDbType.Int, 4, id) };
                string commandText = "delete from usersupdate where id=@id";
                return (DataBase.ExecuteNonQuery(CommandType.Text, commandText, commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static int EmailExists(string email)
        {
            try
            {
                int num = 0x3e7;
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@email", SqlDbType.NVarChar, 50, email) };
                object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_EmailExists", commandParameters);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    num = Convert.ToInt32(obj2);
                }
                return num;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0x3e7;
            }
        }

        public static bool Exists(int userId)
        {
            try
            {
                bool flag = false;
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@userId", SqlDbType.Int, 4, userId) };
                object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_ExistsId", commandParameters);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    flag = Convert.ToBoolean(obj2);
                }
                return flag;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static bool Exists(string username)
        {
            try
            {
                bool flag = false;
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@userName", SqlDbType.NVarChar, 50, username) };
                object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_Exists", commandParameters);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    flag = Convert.ToBoolean(obj2);
                }
                return flag;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static DataTable getAgentList()
        {
            try
            {
                string commandText = "select id,userName from userbase with(nolock) where userType = 4";
                return DataBase.ExecuteDataset(CommandType.Text, commandText).Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public static UserInfo GetBaseModel(int uid)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = uid;
            UserInfo info = new UserInfo();
            return GetBaseModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userbase_get", commandParameters));
        }

        public static UserInfo GetBaseModelFromDs(DataSet ds)
        {
            UserInfo info = new UserInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    info.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["classid"].ToString() != "")
                {
                    info.classid = int.Parse(ds.Tables[0].Rows[0]["classid"].ToString());
                }
                info.UserName = ds.Tables[0].Rows[0]["userName"].ToString();
                info.Password = ds.Tables[0].Rows[0]["password"].ToString();
                info.Password2 = ds.Tables[0].Rows[0]["pwd2"].ToString();
                if (ds.Tables[0].Rows[0]["CPSDrate"].ToString() != "")
                {
                    info.CPSDrate = int.Parse(ds.Tables[0].Rows[0]["CPSDrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CVSNrate"].ToString() != "")
                {
                    info.CVSNrate = int.Parse(ds.Tables[0].Rows[0]["CVSNrate"].ToString());
                }
                info.Email = ds.Tables[0].Rows[0]["email"].ToString();
                info.QQ = ds.Tables[0].Rows[0]["qq"].ToString();
                info.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                info.IdCard = ds.Tables[0].Rows[0]["idCard"].ToString();
                info.full_name = ds.Tables[0].Rows[0]["full_name"].ToString();
                info.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    info.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["regTime"].ToString() != "")
                {
                    info.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["regTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["agentId"].ToString() != "")
                {
                    info.AgentId = int.Parse(ds.Tables[0].Rows[0]["agentId"].ToString());
                }
                info.SiteName = ds.Tables[0].Rows[0]["siteName"].ToString();
                info.SiteUrl = ds.Tables[0].Rows[0]["siteUrl"].ToString();
                if (ds.Tables[0].Rows[0]["userType"].ToString() != "")
                {
                    info.UserType = (UserTypeEnum)int.Parse(ds.Tables[0].Rows[0]["userType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                {
                    info.UserLevel = (UserLevelEnum)int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString() != "")
                {
                    info.MaxDayToCashTimes = int.Parse(ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["apiaccount"].ToString() != "")
                {
                    info.APIAccount = long.Parse(ds.Tables[0].Rows[0]["apiaccount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                {
                    info.manageId = new int?(int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString()));
                }
                if (ds.Tables[0].Rows[0]["isRealNamePass"].ToString() != "")
                {
                    info.IsRealNamePass = int.Parse(ds.Tables[0].Rows[0]["isRealNamePass"].ToString());
                }
                else
                {
                    info.IsRealNamePass = 0;
                }
                if (ds.Tables[0].Rows[0]["isEmailPass"].ToString() != "")
                {
                    info.IsEmailPass = int.Parse(ds.Tables[0].Rows[0]["isEmailPass"].ToString());
                }
                else
                {
                    info.IsEmailPass = 0;
                }
                if (ds.Tables[0].Rows[0]["isPhonePass"].ToString() != "")
                {
                    info.IsPhonePass = int.Parse(ds.Tables[0].Rows[0]["isPhonePass"].ToString());
                }
                else
                {
                    info.IsPhonePass = 0;
                }
                if (ds.Tables[0].Rows[0]["settles"].ToString() != "")
                {
                    info.Settles = int.Parse(ds.Tables[0].Rows[0]["settles"].ToString());
                }
                else
                {
                    info.Settles = 1;
                }
                info.question = ds.Tables[0].Rows[0]["question"].ToString();
                info.answer = ds.Tables[0].Rows[0]["answer"].ToString();
                info.APIKey = ds.Tables[0].Rows[0]["APIkey"].ToString();
                info.smsNotifyUrl = ds.Tables[0].Rows[0]["smsNotifyUrl"].ToString();
                if (ds.Tables[0].Rows[0]["isdebug"].ToString() != "")
                {
                    info.isdebug = int.Parse(ds.Tables[0].Rows[0]["isdebug"].ToString());
                }
            }
            return info;
        }

        public static UserInfo GetCacheModel(int uid)
        {
            UserInfo o = new UserInfo();
            string objId = string.Format(USER_CACHE_KEY, uid);
            o = (UserInfo)WebCache.GetCacheService().RetrieveObject(objId);
            if (o == null)
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("id", uid);
                SqlDependency dependency = DataBase.AddSqlDependency(objId, "userbase", "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug],[frontPic],[versoPic]", "[id]=@id", parameters);
                SqlDependency dependency2 = DataBase.AddSqlDependency(objId, "userspaybank", "[userid],[pmode],[account],[payeeName],[payeeBank],[bankProvince],[bankCity],[bankAddress],[status]", "[userid]=@id", parameters);
                o = GetModel(uid);
                WebCache.GetCacheService().AddObject(objId, o);
            }
            return o;
        }

        public static UserInfo GetCacheUserBaseInfo(int uid)
        {
            UserInfo o = new UserInfo();
            string objId = string.Format(USER_CACHE_KEY, uid);
            o = (UserInfo)WebCache.GetCacheService().RetrieveObject(objId);
            if (o == null)
            {
                IDictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("id", uid);
                SqlDependency dependency = DataBase.AddSqlDependency(objId, "userbase", "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug],[frontPic],[versoPic]", "[id]=@id", parameters);
                o = GetModel(uid);
                WebCache.GetCacheService().AddObject(objId, o);
            }
            return o;
        }

        public static string GetClassViewName(int classid)
        {
            string str = string.Empty;
            if (classid == 0)
            {
                return "个人";
            }
            if (classid == 1)
            {
                str = "企业";
            }
            return str;
        }

        public static string GetClassViewName(object obj)
        {
            if ((obj == null) || (obj == DBNull.Value))
            {
                return string.Empty;
            }
            return GetClassViewName(Convert.ToInt32(obj));
        }

        public static int GetCurrent()
        {
            try
            {
                object obj2 = HttpContext.Current.Session["{2A1FA22C-201B-471c-B668-2FCC1C4A121A}"];
                if (obj2 != null)
                {
                    return Convert.ToInt32(obj2);
                }
                object obj3 = HttpContext.Current.Session["{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}"];
                if (obj3 != null)
                {
                    SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, obj3) };
                    object obj4 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdBySession", commandParameters);
                    if (obj4 != DBNull.Value)
                    {
                        return Convert.ToInt32(obj4);
                    }
                }
                return 0;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static UserInfo GetModel(int uid)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = uid;
            UserInfo info = new UserInfo();
            return GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_get", commandParameters));
        }

        public static UserInfo GetModelByName(string userName)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userName", SqlDbType.VarChar, 20) };
            commandParameters[0].Value = userName;
            UserInfo info = new UserInfo();
            return GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_getbyname", commandParameters));
        }

        public static UserInfo GetModelFromDs(DataSet ds)
        {
            UserInfo info = new UserInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    info.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["classid"].ToString() != "")
                {
                    info.classid = int.Parse(ds.Tables[0].Rows[0]["classid"].ToString());
                }
                info.UserName = ds.Tables[0].Rows[0]["userName"].ToString();
                info.Password = ds.Tables[0].Rows[0]["password"].ToString();
                info.Password2 = ds.Tables[0].Rows[0]["pwd2"].ToString();
                if (ds.Tables[0].Rows[0]["CPSDrate"].ToString() != "")
                {
                    info.CPSDrate = int.Parse(ds.Tables[0].Rows[0]["CPSDrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CVSNrate"].ToString() != "")
                {
                    info.CVSNrate = int.Parse(ds.Tables[0].Rows[0]["CVSNrate"].ToString());
                }
                info.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                info.Email = ds.Tables[0].Rows[0]["email"].ToString();
                info.QQ = ds.Tables[0].Rows[0]["qq"].ToString();
                info.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                info.IdCard = ds.Tables[0].Rows[0]["idCard"].ToString();
                info.Account = ds.Tables[0].Rows[0]["account"].ToString();
                info.PayeeName = ds.Tables[0].Rows[0]["payeeName"].ToString();
                info.BankCode = ds.Tables[0].Rows[0]["BankCode"].ToString();
                info.PayeeBank = ds.Tables[0].Rows[0]["payeeBank"].ToString();
                info.BankProvince = ds.Tables[0].Rows[0]["bankProvince"].ToString();
                info.BankCity = ds.Tables[0].Rows[0]["bankCity"].ToString();
                info.BankAddress = ds.Tables[0].Rows[0]["bankAddress"].ToString();
                info.full_name = ds.Tables[0].Rows[0]["full_name"].ToString();
                info.versoPic = ds.Tables[0].Rows[0]["versoPic"].ToString();
                info.frontPic = ds.Tables[0].Rows[0]["frontPic"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    info.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["regTime"].ToString() != "")
                {
                    info.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["regTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["balance"].ToString() != "")
                {
                    info.Balance = decimal.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payment"].ToString() != "")
                {
                    info.Payment = decimal.Parse(ds.Tables[0].Rows[0]["payment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["unpayment"].ToString() != "")
                {
                    info.Unpayment = decimal.Parse(ds.Tables[0].Rows[0]["unpayment"].ToString());
                }
                if (ds.Tables[0].Rows[0]["enableAmt"].ToString() != "")
                {
                    info.enableAmt = decimal.Parse(ds.Tables[0].Rows[0]["enableAmt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["agentId"].ToString() != "")
                {
                    info.AgentId = int.Parse(ds.Tables[0].Rows[0]["agentId"].ToString());
                }
                info.SiteName = ds.Tables[0].Rows[0]["siteName"].ToString();
                info.SiteUrl = ds.Tables[0].Rows[0]["siteUrl"].ToString();
                if (ds.Tables[0].Rows[0]["userType"].ToString() != "")
                {
                    info.UserType = (UserTypeEnum)int.Parse(ds.Tables[0].Rows[0]["userType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                {
                    info.UserLevel = (UserLevelEnum)int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Integral"].ToString() != "")
                {
                    info.Integral = int.Parse(ds.Tables[0].Rows[0]["Integral"].ToString());
                }
                if (ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString() != "")
                {
                    info.MaxDayToCashTimes = int.Parse(ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["apiaccount"].ToString() != "")
                {
                    info.APIAccount = long.Parse(ds.Tables[0].Rows[0]["apiaccount"].ToString());
                }
                info.APIKey = ds.Tables[0].Rows[0]["APIkey"].ToString();
                info.LastLoginIp = ds.Tables[0].Rows[0]["lastLoginIp"].ToString();
                if (ds.Tables[0].Rows[0]["lastLoginTime"].ToString() != "")
                {
                    info.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["lastLoginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pmode"].ToString() != "")
                {
                    info.PMode = int.Parse(ds.Tables[0].Rows[0]["pmode"].ToString());
                }
                info.Desc = ds.Tables[0].Rows[0]["Desc"].ToString();
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                {
                    info.manageId = new int?(int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString()));
                }
                if (ds.Tables[0].Rows[0]["isRealNamePass"].ToString() != "")
                {
                    info.IsRealNamePass = int.Parse(ds.Tables[0].Rows[0]["isRealNamePass"].ToString());
                }
                else
                {
                    info.IsRealNamePass = 0;
                }
                if (ds.Tables[0].Rows[0]["isEmailPass"].ToString() != "")
                {
                    info.IsEmailPass = int.Parse(ds.Tables[0].Rows[0]["isEmailPass"].ToString());
                }
                else
                {
                    info.IsEmailPass = 0;
                }
                if (ds.Tables[0].Rows[0]["isPhonePass"].ToString() != "")
                {
                    info.IsPhonePass = int.Parse(ds.Tables[0].Rows[0]["isPhonePass"].ToString());
                }
                else
                {
                    info.IsPhonePass = 0;
                }
                if (ds.Tables[0].Rows[0]["settles"].ToString() != "")
                {
                    info.Settles = int.Parse(ds.Tables[0].Rows[0]["settles"].ToString());
                }
                else
                {
                    info.Settles = 1;
                }
                info.question = ds.Tables[0].Rows[0]["question"].ToString();
                info.answer = ds.Tables[0].Rows[0]["answer"].ToString();
                info.smsNotifyUrl = ds.Tables[0].Rows[0]["smsNotifyUrl"].ToString();
                if (ds.Tables[0].Rows[0]["isdebug"].ToString() != "")
                {
                    info.isdebug = int.Parse(ds.Tables[0].Rows[0]["isdebug"].ToString());
                }
                if ((ds.Tables[0].Rows[0]["idCardtype"] != null) && (ds.Tables[0].Rows[0]["idCardtype"].ToString() != ""))
                {
                    info.IdCardType = int.Parse(ds.Tables[0].Rows[0]["idCardtype"].ToString());
                }
                if ((ds.Tables[0].Rows[0]["msn"] != null) && (ds.Tables[0].Rows[0]["msn"].ToString() != ""))
                {
                    info.msn = ds.Tables[0].Rows[0]["msn"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["fax"] != null) && (ds.Tables[0].Rows[0]["fax"].ToString() != ""))
                {
                    info.fax = ds.Tables[0].Rows[0]["fax"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["province"] != null) && (ds.Tables[0].Rows[0]["province"].ToString() != ""))
                {
                    info.province = ds.Tables[0].Rows[0]["province"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["city"] != null) && (ds.Tables[0].Rows[0]["city"].ToString() != ""))
                {
                    info.city = ds.Tables[0].Rows[0]["city"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["zip"] != null) && (ds.Tables[0].Rows[0]["zip"].ToString() != ""))
                {
                    info.zip = ds.Tables[0].Rows[0]["zip"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["field1"] != null) && (ds.Tables[0].Rows[0]["field1"].ToString() != ""))
                {
                    info.field1 = ds.Tables[0].Rows[0]["field1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["isagentDistribution"].ToString() != "")
                {
                    info.isagentDistribution = int.Parse(ds.Tables[0].Rows[0]["isagentDistribution"].ToString());
                }
                else
                {
                    info.isagentDistribution = 0;
                }
                if (ds.Tables[0].Rows[0]["agentDistscheme"].ToString() != "")
                {
                    info.agentDistscheme = int.Parse(ds.Tables[0].Rows[0]["agentDistscheme"].ToString());
                }
                else
                {
                    info.agentDistscheme = 0;
                }
                if (ds.Tables[0].Rows[0]["cardversion"].ToString() != "")
                {
                    info.cardversion = byte.Parse(ds.Tables[0].Rows[0]["cardversion"].ToString());
                }
            }
            return info;
        }

        public static int GetPromID(int userid)
        {
            try
            {
                string commandText = "SELECT top 1 pid FROM PromotionUser with(nolock) WHERE regid=@userid ";
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userid", SqlDbType.Int, 4) };
                commandParameters[0].Value = userid;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.Text, commandText, commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static UserInfo GetPromSuperior(int userId)
        {
            string commandText = "SELECT u.* FROM userbase u inner JOIN PromotionUser pu ON u.id = pu.PID\r\nWHERE pu.RegId = @RegId";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RegId", SqlDbType.Int, 4) };
            commandParameters[0].Value = userId;
            UserInfo info = new UserInfo();
            return GetBaseModelFromDs(DataBase.ExecuteDataset(CommandType.Text, commandText, commandParameters));
        }

        public static string GetUserApiKey(int userId)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@user_id", SqlDbType.Int, 4, userId) };
                return DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getApiKey", commandParameters).ToString();
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return string.Empty;
            }
        }

        public static int GetUserIdBySession(string _sessionId)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, _sessionId) };
                object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdBySession", commandParameters);
                if (obj2 != DBNull.Value)
                {
                    return Convert.ToInt32(obj2);
                }
                return 0;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static int GetUserIdByToken(string token)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@token", SqlDbType.VarChar, 100, token) };
                object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdByToken", commandParameters);
                if (obj2 != DBNull.Value)
                {
                    return Convert.ToInt32(obj2);
                }
                return 0;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static List<int> GetUsers(string where)
        {
            List<int> list = new List<int>();
            if (string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            string commandText = "select id from dbo.userbase where " + where;
            SqlDataReader reader = DataBase.ExecuteReader(CommandType.Text, commandText);
            while (reader.Read())
            {
                list.Add(reader.GetInt32(0));
            }
            return list;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "V_Users";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userName]\r\n      ,[password]\r\n      ,[CPSDrate]\r\n      ,[CVSNrate]\r\n      ,[email]\r\n      ,[qq]\r\n      ,[tel]\r\n      ,[idCard]\r\n      ,[settles]\r\n      ,[status]\r\n      ,[regTime]\r\n      ,[company]\r\n      ,[linkMan]\r\n      ,[agentId]\r\n      ,[siteName]\r\n      ,[siteUrl]\r\n      ,[userType]\r\n      ,[userLevel]\r\n      ,[maxdaytocashTimes]\r\n      ,[apiaccount]\r\n      ,[apikey]\r\n      ,[lastLoginIp]\r\n      ,[lastLoginTime]\r\n      ,[sessionId]\r\n      ,[updatetime]\r\n      ,[DESC]\r\n      ,[userid]\r\n      ,[pmode]\r\n      ,[account]\r\n      ,[payeeName]\r\n      ,[payeeBank]\r\n      ,[bankProvince]\r\n      ,[bankCity]\r\n      ,[bankAddress]\r\n      ,[Integral]\r\n      ,[balance]\r\n      ,[payment]\r\n      ,[unpayment]\r\n      ,[enableAmt]\r\n      ,[manageId]\r\n      ,[isRealNamePass]\r\n      ,[isPhonePass]\r\n      ,[isEmailPass]\r\n      ,[question]\r\n      ,[answer]\r\n      ,[smsNotifyUrl]\r\n      ,[full_name]\r\n      ,[classid]\r\n      ,[Freeze]\r\n      ,[schemename]\r\n      ,[idCardtype]\r\n      ,[msn]\r\n      ,[fax]\r\n      ,[province]\r\n      ,[city]\r\n      ,[zip]\r\n      ,[field1],[levName],[frontPic],[versoPic]", tables, wheres, orderby, key, pageSize, page, false);
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public static UserInfo ReaderBind(IDataReader dataReader)
        {
            UserInfo info = new UserInfo();
            object obj2 = dataReader["id"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.ID = (int)obj2;
            }
            info.UserName = dataReader["userName"].ToString();
            info.Password = dataReader["password"].ToString();
            obj2 = dataReader["CPSDrate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.CPSDrate = (int)obj2;
            }
            obj2 = dataReader["CVSNrate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.CVSNrate = (int)obj2;
            }
            info.Email = dataReader["email"].ToString();
            info.QQ = dataReader["qq"].ToString();
            info.Tel = dataReader["tel"].ToString();
            info.IdCard = dataReader["idCard"].ToString();
            info.Account = dataReader["account"].ToString();
            info.PayeeName = dataReader["payeeName"].ToString();
            info.PayeeBank = dataReader["payeeBank"].ToString();
            info.BankProvince = dataReader["bankProvince"].ToString();
            info.BankCity = dataReader["bankCity"].ToString();
            info.BankAddress = dataReader["bankAddress"].ToString();
            info.versoPic = dataReader["versoPic"].ToString();
            info.frontPic = dataReader["frontPic"].ToString();
            obj2 = dataReader["status"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Status = (int)obj2;
            }
            obj2 = dataReader["regTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.RegTime = (DateTime)obj2;
            }
            obj2 = dataReader["balance"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Balance = (decimal)obj2;
            }
            obj2 = dataReader["payment"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Payment = (decimal)obj2;
            }
            obj2 = dataReader["unpayment"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Unpayment = (decimal)obj2;
            }
            obj2 = dataReader["agentId"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.AgentId = (int)obj2;
            }
            info.SiteName = dataReader["siteName"].ToString();
            info.SiteUrl = dataReader["siteUrl"].ToString();
            obj2 = dataReader["userType"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.UserType = (UserTypeEnum)obj2;
            }
            obj2 = dataReader["userLevel"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.UserLevel = (UserLevelEnum)obj2;
            }
            obj2 = dataReader["Integral"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Integral = (int)obj2;
            }
            obj2 = dataReader["maxdaytocashTimes"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.MaxDayToCashTimes = (int)obj2;
            }
            obj2 = dataReader["apiaccount"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.APIAccount = (long)obj2;
            }
            info.APIKey = dataReader["apikey"].ToString();
            info.LastLoginIp = dataReader["lastLoginIp"].ToString();
            obj2 = dataReader["lastLoginTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.LastLoginTime = (DateTime)obj2;
            }
            info.smsNotifyUrl = dataReader["smsNotifyUrl"].ToString();
            info.question = dataReader["question"].ToString();
            info.answer = dataReader["answer"].ToString();
            obj2 = dataReader["manageId"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.manageId = new int?((int)obj2);
            }
            return info;
        }

        public static string SignIn(UserInfo userinfo)
        {
            string userloginMsgForUnCheck = string.Empty;
            try
            {
                if (((userinfo == null) || string.IsNullOrEmpty(userinfo.UserName)) || string.IsNullOrEmpty(userinfo.Password))
                {
                    return "请输入账号密码";
                }
                userloginMsgForUnCheck = "用户名或者密码错误,请重新输入!";
                string str2 = Guid.NewGuid().ToString("b");
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@username", SqlDbType.VarChar, 50, userinfo.UserName), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, userinfo.Password), DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, userinfo.LastLoginIp), DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now), DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, str2), DataBase.MakeInParam("@address", SqlDbType.VarChar, 20, userinfo.LastLoginAddress), DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, userinfo.LastLoginRemark), DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, userinfo.Email), DataBase.MakeInParam("@loginType", SqlDbType.TinyInt, 1, userinfo.loginType) };
                SqlDataReader reader = DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_users_Login", commandParameters);
                if (reader.Read())
                {
                    if (reader["status"] != DBNull.Value)
                    {
                        userinfo.Status = (int)reader["status"];
                        if (userinfo.Status == 1)
                        {
                            userloginMsgForUnCheck = SysConfig.UserloginMsgForUnCheck;
                        }
                        else if (userinfo.Status == 2)
                        {
                            userinfo.ID = (int)reader["userId"];
                            userinfo.UserType = (UserTypeEnum)Convert.ToInt32(reader["userType"]);
                            userinfo.IsEmailPass = Convert.ToInt32(reader["isEmailPass"]);
                            userloginMsgForUnCheck = "登录成功";
                            HttpContext.Current.Session["{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}"] = str2;
                            if (userinfo.LastLoginRemark == "客户端登录")
                            {
                                HttpContext.Current.Session["{2A1FA22C-201B-471c-B668-2FCC1C4A121A}"] = userinfo.ID;
                            }
                        }
                        else if (userinfo.Status == 4)
                        {
                            userloginMsgForUnCheck = SysConfig.UserloginMsgForlock;
                        }
                        else if (userinfo.Status == 8)
                        {
                            userloginMsgForUnCheck = SysConfig.UserloginMsgForCheckfail;
                        }
                    }
                    reader.Dispose();
                }
                return userloginMsgForUnCheck;
            }
            catch (Exception exception)
            {
                userloginMsgForUnCheck = "登录失败";
                ExceptionHandler.HandleException(exception);
                return userloginMsgForUnCheck;
            }
        }

        public static void SignOut()
        {
            HttpContext.Current.Items["{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] = null;
            HttpContext.Current.Session["{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}"] = null;
        }

        public static bool Update(UserInfo _userinfo, List<UsersUpdateLog> changeList)
        {
            bool flag;
            SqlParameter[] parameterArray = new SqlParameter[] {
                DataBase.MakeInParam("@id", SqlDbType.Int, 4, _userinfo.ID), DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, _userinfo.UserName), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, _userinfo.Password), DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, _userinfo.CPSDrate), DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, _userinfo.CVSNrate), DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, _userinfo.Email), DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, _userinfo.QQ), DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, _userinfo.Tel), DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, _userinfo.IdCard), DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, _userinfo.Account), DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, _userinfo.PayeeName), DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, _userinfo.PayeeBank), DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, _userinfo.BankProvince), DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, _userinfo.BankCity), DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, _userinfo.BankAddress), DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, _userinfo.Status),
                DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, _userinfo.AgentId), DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, _userinfo.SiteName), DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, _userinfo.SiteUrl), DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int) _userinfo.UserType), DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int) _userinfo.UserLevel), DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, _userinfo.MaxDayToCashTimes), DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, _userinfo.APIAccount), DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, _userinfo.APIKey), DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 0xfa0, _userinfo.Desc), DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, _userinfo.PMode), DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, DateTime.Now), DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, _userinfo.manageId), DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt, 1, _userinfo.IsRealNamePass), DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt, 1, _userinfo.IsEmailPass), DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt, 1, _userinfo.IsPhonePass), DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar, 0xff, _userinfo.smsNotifyUrl),
                DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 50, _userinfo.full_name), DataBase.MakeInParam("@male", SqlDbType.NVarChar, 4, _userinfo.male), DataBase.MakeInParam("@addtress", SqlDbType.NVarChar, 30, _userinfo.addtress), DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, _userinfo.question), DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, _userinfo.answer), DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, _userinfo.Password2), DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, _userinfo.LinkMan), DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, _userinfo.classid), DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, _userinfo.Settles), DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, _userinfo.isdebug), DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, _userinfo.IdCardType), DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, _userinfo.msn), DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, _userinfo.fax), DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, _userinfo.province), DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, _userinfo.city), DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, _userinfo.zip),
                DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, _userinfo.field1), DataBase.MakeInParam("@accoutType", SqlDbType.TinyInt, 1, _userinfo.accoutType), DataBase.MakeInParam("@BankCode", SqlDbType.VarChar, 50, _userinfo.BankCode), DataBase.MakeInParam("@provinceCode", SqlDbType.VarChar, 50, _userinfo.provinceCode), DataBase.MakeInParam("@cityCode", SqlDbType.VarChar, 50, _userinfo.cityCode), DataBase.MakeInParam("@isagentDistribution", SqlDbType.TinyInt, 1, _userinfo.isagentDistribution), DataBase.MakeInParam("@agentDistscheme", SqlDbType.Int, 4, _userinfo.agentDistscheme), DataBase.MakeInParam("@cardversion", SqlDbType.TinyInt, 1, _userinfo.cardversion), DataBase.MakeInParam("@versoPic", SqlDbType.VarChar, 500, _userinfo.versoPic), DataBase.MakeInParam("@frontPic", SqlDbType.VarChar, 500, _userinfo.frontPic)
             };
            using (SqlConnection connection = new SqlConnection(DataBase.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (changeList != null)
                    {
                        foreach (UsersUpdateLog log in changeList)
                        {
                            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@field", SqlDbType.VarChar, 20), new SqlParameter("@oldValue", SqlDbType.VarChar, 100), new SqlParameter("@newvalue", SqlDbType.VarChar, 100), new SqlParameter("@Addtime", SqlDbType.DateTime), new SqlParameter("@editor", SqlDbType.VarChar, 50), new SqlParameter("@oIp", SqlDbType.VarChar, 50), new SqlParameter("@desc", SqlDbType.VarChar, 0xfa0) };
                            parameterArray2[0].Value = log.userid;
                            parameterArray2[1].Value = log.field;
                            parameterArray2[2].Value = log.oldValue;
                            parameterArray2[3].Value = log.newvalue;
                            parameterArray2[4].Value = log.Addtime;
                            parameterArray2[5].Value = log.Editor;
                            parameterArray2[6].Value = log.OIp;
                            parameterArray2[7].Value = log.Desc;
                            if (DataBase.ExecuteNonQuery(transaction, "proc_usersupdate_add", (object[])parameterArray2) < 0)
                            {
                                transaction.Rollback();
                                connection.Close();
                                return false;
                            }
                        }
                    }
                    if (DataBase.ExecuteNonQuery(transaction, "proc_users_Update", (object[])parameterArray) > 0)
                    {
                        HttpContext.Current.Items["{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] = null;
                        transaction.Commit();
                        connection.Close();
                        ClearCache(_userinfo.ID);
                        return true;
                    }
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    ExceptionHandler.HandleException(exception);
                    return false;
                }
                finally
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                    }
                }
            }
            return flag;
        }

        public static bool Update1(UserInfo _userinfo)
        {
            SqlParameter[] commandParameters = new SqlParameter[] {
                DataBase.MakeInParam("@id", SqlDbType.Int, 4, _userinfo.ID), DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, _userinfo.UserName), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, _userinfo.Password), DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, _userinfo.CPSDrate), DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, _userinfo.CVSNrate), DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, _userinfo.Email), DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, _userinfo.QQ), DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, _userinfo.Tel), DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, _userinfo.IdCard), DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, _userinfo.Account), DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, _userinfo.PayeeName), DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, _userinfo.PayeeBank), DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, _userinfo.BankProvince), DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, _userinfo.BankCity), DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, _userinfo.BankAddress), DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, _userinfo.Status),
                DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, _userinfo.AgentId), DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, _userinfo.SiteName), DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, _userinfo.SiteUrl), DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (int) _userinfo.UserType), DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (int) _userinfo.UserLevel), DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, _userinfo.MaxDayToCashTimes), DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, _userinfo.APIAccount), DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, _userinfo.APIKey), DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 0xfa0, _userinfo.Desc), DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, _userinfo.PMode), DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, DateTime.Now), DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, _userinfo.manageId), DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt, 1, _userinfo.IsRealNamePass), DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt, 1, _userinfo.IsEmailPass), DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt, 1, _userinfo.IsPhonePass), DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar, 0xff, _userinfo.smsNotifyUrl),
                DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 50, _userinfo.full_name), DataBase.MakeInParam("@male", SqlDbType.NVarChar, 4, _userinfo.male), DataBase.MakeInParam("@addtress", SqlDbType.NVarChar, 30, _userinfo.addtress), DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, _userinfo.question), DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, _userinfo.answer), DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, _userinfo.Password2), DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, _userinfo.LinkMan), DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, _userinfo.classid), DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, _userinfo.Settles), DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, _userinfo.isdebug), DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, _userinfo.IdCardType), DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, _userinfo.msn), DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, _userinfo.fax), DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, _userinfo.province), DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, _userinfo.city), DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, _userinfo.zip),
                DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, _userinfo.field1), DataBase.MakeInParam("@accoutType", SqlDbType.TinyInt, 1, _userinfo.accoutType), DataBase.MakeInParam("@BankCode", SqlDbType.VarChar, 50, _userinfo.BankCode), DataBase.MakeInParam("@provinceCode", SqlDbType.VarChar, 50, _userinfo.provinceCode), DataBase.MakeInParam("@cityCode", SqlDbType.VarChar, 50, _userinfo.cityCode), DataBase.MakeInParam("@isagentDistribution", SqlDbType.TinyInt, 1, _userinfo.isagentDistribution), DataBase.MakeInParam("@agentDistscheme", SqlDbType.Int, 4, _userinfo.agentDistscheme), DataBase.MakeInParam("@cardversion", SqlDbType.TinyInt, 1, _userinfo.cardversion), DataBase.MakeInParam("@versoPic", SqlDbType.VarChar, 500, _userinfo.versoPic), DataBase.MakeInParam("@frontPic", SqlDbType.VarChar, 500, _userinfo.frontPic)
             };
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_Update", commandParameters) > 0);
        }

        public static DataSet UpdateLogPageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "usersupdate";
                string key = "[id]";
                string columns = "id,\r\nuserid,\r\nfield,\r\noldValue,\r\nnewvalue,\r\nAddtime,\r\neditor,\r\noIp";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "Addtime desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderUpdateLogWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(columns, tables, wheres, orderby, key, pageSize, page, false);
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public static UserInfo CurrentMember
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] == null)
                    {
                        int current = GetCurrent();
                        if (current <= 0)
                        {
                            return null;
                        }
                        HttpContext.Current.Items["{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] = GetCacheModel(current);
                    }
                    return (HttpContext.Current.Items["{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] as UserInfo);
                }
                return null;
            }
        }

        public decimal TotalBalance
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_gettotalbalance"));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }

        public decimal TotalPayment
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_gettotalpayment"));
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                    return 0M;
                }
            }
        }
    }
}

