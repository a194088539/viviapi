using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using viviapi.BLL.Sys;
using viviapi.Cache;
using viviapi.Model.User;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.User
{
    public class usersOrderIncome
    {
        public static string USER_CACHE_KEY = Constant.Cache_Mark + "USER_{0}";
        internal const string USER_CONTEXT_KEY = "{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}";
        internal const string USER_LOGIN_SESSIONID = "{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}";
        internal const string SQL_BASE_TABLE = "userbase";
        internal const string SQL_BASE_TABLE_FIELD = "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug]";
        internal const string SQL_PAYBANK_TABLE = "userspaybank";
        internal const string SQL_PAYBANK_TABLE_FIELD = "[userid],[pmode],[account],[payeeName],[payeeBank],[bankProvince],[bankCity],[bankAddress],[status]";
        internal const string SQL_TABLE = "V_Users";
        internal const string SQL_TABLE_FIELD = "id,userName,password,CPSDrate,CVSNrate,email,qq,tel,idCard,pmode,settles,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,regTime,company,linkMan,agentId,siteName,siteUrl,userType,userLevel,maxdaytocashTimes,apiaccount,apikey,lastLoginIp,lastLoginTime,sessionId,manageId,isRealNamePass,full_name,classid,isdebug";
        internal const string FIELD_USER = "[id]\r\n      ,[userName]\r\n      ,[password]\r\n      ,[CPSDrate]\r\n      ,[CVSNrate]\r\n      ,[email]\r\n      ,[qq]\r\n      ,[tel]\r\n      ,[idCard]\r\n      ,[settles]\r\n      ,[status]\r\n      ,[regTime]\r\n      ,[company]\r\n      ,[linkMan]\r\n      ,[agentId]\r\n      ,[siteName]\r\n      ,[siteUrl]\r\n      ,[userType]\r\n      ,[userLevel]\r\n      ,[maxdaytocashTimes]\r\n      ,[apiaccount]\r\n      ,[apikey]\r\n      ,[lastLoginIp]\r\n      ,[lastLoginTime]\r\n      ,[sessionId]\r\n      ,[updatetime]\r\n      ,[DESC]\r\n      ,[userid]\r\n      ,[pmode]\r\n      ,[account]\r\n      ,[payeeName]\r\n      ,[payeeBank]\r\n      ,[bankProvince]\r\n      ,[bankCity]\r\n      ,[bankAddress]\r\n      ,[Integral]\r\n      ,[balance]\r\n      ,[payment]\r\n      ,[unpayment]\r\n      ,[enableAmt]\r\n      ,[manageId]\r\n      ,[isRealNamePass]\r\n      ,[isPhonePass]\r\n      ,[isEmailPass]\r\n      ,[question]\r\n      ,[answer]\r\n      ,[smsNotifyUrl]\r\n      ,[full_name]\r\n      ,[classid]\r\n      ,[Freeze]\r\n      ,[schemename]\r\n      ,[idCardtype]\r\n      ,[msn]\r\n      ,[fax]\r\n      ,[province]\r\n      ,[city]\r\n      ,[zip]\r\n      ,[field1],[levName]";

        public static UserInfo CurrentMember
        {
            get
            {
                if (HttpContext.Current == null)
                    return (UserInfo)null;
                if (HttpContext.Current.Items[(object)"{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] == null)
                {
                    int current = usersOrderIncome.GetCurrent();
                    if (current <= 0)
                        return (UserInfo)null;
                    HttpContext.Current.Items[(object)"{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] = (object)usersOrderIncome.GetCacheModel(current);
                }
                return HttpContext.Current.Items[(object)"{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] as UserInfo;
            }
        }

        public Decimal TotalBalance
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_gettotalbalance"));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return new Decimal(0);
                }
            }
        }

        public Decimal TotalPayment
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_gettotalpayment"));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                    return new Decimal(0);
                }
            }
        }

        public static bool chkAgent(int agentid)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@agentid", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)agentid;
                return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_user_chkagent", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static int Add(UserInfo _userinfo)
        {
            try
            {
                SqlParameter sqlParameter = DataBase.MakeOutParam("@id", SqlDbType.Int, 4);
                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_add", sqlParameter, DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, (object)_userinfo.UserName), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, (object)_userinfo.Password), DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, (object)_userinfo.CPSDrate), DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, (object)_userinfo.CVSNrate), DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, (object)_userinfo.Email), DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, (object)_userinfo.QQ), DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, (object)_userinfo.Tel), DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, (object)_userinfo.IdCard), DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, (object)_userinfo.Account), DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, (object)_userinfo.PayeeName), DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, (object)_userinfo.PayeeBank), DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, (object)_userinfo.BankProvince), DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, (object)_userinfo.BankCity), DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, (object)_userinfo.BankAddress), DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object)_userinfo.Status), DataBase.MakeInParam("@lastloginip", SqlDbType.VarChar, 50, (object)_userinfo.LastLoginIp), DataBase.MakeInParam("@lastlogintime", SqlDbType.DateTime, 8, (object)_userinfo.LastLoginTime), DataBase.MakeInParam("@regtime", SqlDbType.DateTime, 8, (object)_userinfo.RegTime), DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, (object)_userinfo.AgentId), DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, (object)_userinfo.SiteName), DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, (object)_userinfo.SiteUrl), DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (object)_userinfo.UserType), DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (object)_userinfo.UserLevel), DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, (object)_userinfo.MaxDayToCashTimes), DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, (object)_userinfo.APIAccount), DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, (object)_userinfo.APIKey), DataBase.MakeInParam("@pmode", SqlDbType.TinyInt, 1, (object)_userinfo.PMode), DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, (object)_userinfo.Settles), DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, (object)_userinfo.Desc), DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, (object)_userinfo.manageId), DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, (object)_userinfo.question), DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, (object)_userinfo.answer), DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 100, (object)_userinfo.full_name), DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, (object)_userinfo.classid), DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, (object)_userinfo.Password2), DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, (object)_userinfo.LinkMan), DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, (object)_userinfo.isdebug), DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, (object)_userinfo.IdCardType), DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, (object)_userinfo.msn), DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, (object)_userinfo.fax), DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, (object)_userinfo.province), DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, (object)_userinfo.city), DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, (object)_userinfo.zip), DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, (object)_userinfo.field1)) <= 0)
                    return 0;
                _userinfo.ID = (int)sqlParameter.Value;
                return _userinfo.ID;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Update1(UserInfo _userinfo)
        {
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_Update", DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object)_userinfo.ID), DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, (object)_userinfo.UserName), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, (object)_userinfo.Password), DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, (object)_userinfo.CPSDrate), DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, (object)_userinfo.CVSNrate), DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, (object)_userinfo.Email), DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, (object)_userinfo.QQ), DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, (object)_userinfo.Tel), DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, (object)_userinfo.IdCard), DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, (object)_userinfo.Account), DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, (object)_userinfo.PayeeName), DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, (object)_userinfo.PayeeBank), DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, (object)_userinfo.BankProvince), DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, (object)_userinfo.BankCity), DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, (object)_userinfo.BankAddress), DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object)_userinfo.Status), DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, (object)_userinfo.AgentId), DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, (object)_userinfo.SiteName), DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, (object)_userinfo.SiteUrl), DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (object)_userinfo.UserType), DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (object)_userinfo.UserLevel), DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, (object)_userinfo.MaxDayToCashTimes), DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, (object)_userinfo.APIAccount), DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, (object)_userinfo.APIKey), DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, (object)_userinfo.Desc), DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, (object)_userinfo.PMode), DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, (object)DateTime.Now), DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, (object)_userinfo.manageId), DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt, 1, (object)_userinfo.IsRealNamePass), DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt, 1, (object)_userinfo.IsEmailPass), DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt, 1, (object)_userinfo.IsPhonePass), DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar, (int)byte.MaxValue, (object)_userinfo.smsNotifyUrl), DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 50, (object)_userinfo.full_name), DataBase.MakeInParam("@male", SqlDbType.NVarChar, 4, (object)_userinfo.male), DataBase.MakeInParam("@addtress", SqlDbType.NVarChar, 30, (object)_userinfo.addtress), DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, (object)_userinfo.question), DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, (object)_userinfo.answer), DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, (object)_userinfo.Password2), DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, (object)_userinfo.LinkMan), DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, (object)_userinfo.classid), DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, (object)_userinfo.Settles), DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, (object)_userinfo.isdebug), DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, (object)_userinfo.IdCardType), DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, (object)_userinfo.msn), DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, (object)_userinfo.fax), DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, (object)_userinfo.province), DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, (object)_userinfo.city), DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, (object)_userinfo.zip), DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, (object)_userinfo.field1)) > 0;
        }

        public static bool Update(UserInfo _userinfo, List<UsersUpdateLog> changeList)
        {
            SqlParameter[] sqlParameterArray1 = new SqlParameter[49]
            {
        DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object) _userinfo.ID),
        DataBase.MakeInParam("@userName", SqlDbType.VarChar, 50, (object) _userinfo.UserName),
        DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, (object) _userinfo.Password),
        DataBase.MakeInParam("@cpsdrate", SqlDbType.Int, 4, (object) _userinfo.CPSDrate),
        DataBase.MakeInParam("@cvsnrate", SqlDbType.Int, 4, (object) _userinfo.CVSNrate),
        DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, (object) _userinfo.Email),
        DataBase.MakeInParam("@qq", SqlDbType.VarChar, 50, (object) _userinfo.QQ),
        DataBase.MakeInParam("@tel", SqlDbType.VarChar, 50, (object) _userinfo.Tel),
        DataBase.MakeInParam("@idCard", SqlDbType.VarChar, 50, (object) _userinfo.IdCard),
        DataBase.MakeInParam("@account", SqlDbType.VarChar, 50, (object) _userinfo.Account),
        DataBase.MakeInParam("@payeeName", SqlDbType.VarChar, 50, (object) _userinfo.PayeeName),
        DataBase.MakeInParam("@payeeBank", SqlDbType.VarChar, 50, (object) _userinfo.PayeeBank),
        DataBase.MakeInParam("@bankProvince", SqlDbType.VarChar, 50, (object) _userinfo.BankProvince),
        DataBase.MakeInParam("@bankCity", SqlDbType.VarChar, 50, (object) _userinfo.BankCity),
        DataBase.MakeInParam("@bankAddress", SqlDbType.VarChar, 50, (object) _userinfo.BankAddress),
        DataBase.MakeInParam("@status", SqlDbType.TinyInt, 1, (object) _userinfo.Status),
        DataBase.MakeInParam("@agentId", SqlDbType.Int, 4, (object) _userinfo.AgentId),
        DataBase.MakeInParam("@siteName", SqlDbType.VarChar, 50, (object) _userinfo.SiteName),
        DataBase.MakeInParam("@siteUrl", SqlDbType.VarChar, 100, (object) _userinfo.SiteUrl),
        DataBase.MakeInParam("@userType", SqlDbType.Int, 4, (object) _userinfo.UserType),
        DataBase.MakeInParam("@userLevel", SqlDbType.Int, 4, (object) _userinfo.UserLevel),
        DataBase.MakeInParam("@maxdaytocashTimes", SqlDbType.Int, 4, (object) _userinfo.MaxDayToCashTimes),
        DataBase.MakeInParam("@apiaccount", SqlDbType.BigInt, 8, (object) _userinfo.APIAccount),
        DataBase.MakeInParam("@apikey", SqlDbType.VarChar, 50, (object) _userinfo.APIKey),
        DataBase.MakeInParam("@DESC", SqlDbType.VarChar, 4000, (object) _userinfo.Desc),
        DataBase.MakeInParam("@pmode", SqlDbType.Int, 4, (object) _userinfo.PMode),
        DataBase.MakeInParam("@updatetime", SqlDbType.DateTime, 8, (object) DateTime.Now),
        DataBase.MakeInParam("@manageId", SqlDbType.Int, 4, (object) _userinfo.manageId),
        DataBase.MakeInParam("@isRealNamePass", SqlDbType.TinyInt, 1, (object) _userinfo.IsRealNamePass),
        DataBase.MakeInParam("@isEmailPass", SqlDbType.TinyInt, 1, (object) _userinfo.IsEmailPass),
        DataBase.MakeInParam("@isPhonePass", SqlDbType.TinyInt, 1, (object) _userinfo.IsPhonePass),
        DataBase.MakeInParam("@smsNotifyUrl", SqlDbType.NVarChar, (int) byte.MaxValue, (object) _userinfo.smsNotifyUrl),
        DataBase.MakeInParam("@full_name", SqlDbType.NVarChar, 50, (object) _userinfo.full_name),
        DataBase.MakeInParam("@male", SqlDbType.NVarChar, 4, (object) _userinfo.male),
        DataBase.MakeInParam("@addtress", SqlDbType.NVarChar, 30, (object) _userinfo.addtress),
        DataBase.MakeInParam("@question", SqlDbType.NVarChar, 150, (object) _userinfo.question),
        DataBase.MakeInParam("@answer", SqlDbType.NVarChar, 100, (object) _userinfo.answer),
        DataBase.MakeInParam("@pwd2", SqlDbType.NVarChar, 50, (object) _userinfo.Password2),
        DataBase.MakeInParam("@linkman", SqlDbType.NVarChar, 50, (object) _userinfo.LinkMan),
        DataBase.MakeInParam("@classid", SqlDbType.TinyInt, 1, (object) _userinfo.classid),
        DataBase.MakeInParam("@settles", SqlDbType.TinyInt, 1, (object) _userinfo.Settles),
        DataBase.MakeInParam("@isdebug", SqlDbType.TinyInt, 1, (object) _userinfo.isdebug),
        DataBase.MakeInParam("@idCardtype", SqlDbType.TinyInt, 1, (object) _userinfo.IdCardType),
        DataBase.MakeInParam("@msn", SqlDbType.VarChar, 30, (object) _userinfo.msn),
        DataBase.MakeInParam("@fax", SqlDbType.VarChar, 20, (object) _userinfo.fax),
        DataBase.MakeInParam("@province", SqlDbType.VarChar, 20, (object) _userinfo.province),
        DataBase.MakeInParam("@city", SqlDbType.VarChar, 20, (object) _userinfo.city),
        DataBase.MakeInParam("@zip", SqlDbType.VarChar, 8, (object) _userinfo.zip),
        DataBase.MakeInParam("@field1", SqlDbType.NVarChar, 50, (object) _userinfo.field1)
            };
            using (SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                {
                    try
                    {
                        if (changeList != null)
                        {
                            foreach (UsersUpdateLog usersUpdateLog in changeList)
                            {
                                SqlParameter[] sqlParameterArray2 = new SqlParameter[8]
                                {
                  new SqlParameter("@userid", SqlDbType.Int, 4),
                  new SqlParameter("@field", SqlDbType.VarChar, 20),
                  new SqlParameter("@oldValue", SqlDbType.VarChar, 100),
                  new SqlParameter("@newvalue", SqlDbType.VarChar, 100),
                  new SqlParameter("@Addtime", SqlDbType.DateTime),
                  new SqlParameter("@editor", SqlDbType.VarChar, 50),
                  new SqlParameter("@oIp", SqlDbType.VarChar, 50),
                  new SqlParameter("@desc", SqlDbType.VarChar, 4000)
                                };
                                sqlParameterArray2[0].Value = (object)usersUpdateLog.userid;
                                sqlParameterArray2[1].Value = (object)usersUpdateLog.field;
                                sqlParameterArray2[2].Value = (object)usersUpdateLog.oldValue;
                                sqlParameterArray2[3].Value = (object)usersUpdateLog.newvalue;
                                sqlParameterArray2[4].Value = (object)usersUpdateLog.Addtime;
                                sqlParameterArray2[5].Value = (object)usersUpdateLog.Editor;
                                sqlParameterArray2[6].Value = (object)usersUpdateLog.OIp;
                                sqlParameterArray2[7].Value = (object)usersUpdateLog.Desc;
                                if (DataBase.ExecuteNonQuery(transaction, "proc_usersupdate_add", (object[])sqlParameterArray2) < 0)
                                {
                                    transaction.Rollback();
                                    sqlConnection.Close();
                                    return false;
                                }
                            }
                        }
                        if (DataBase.ExecuteNonQuery(transaction, "proc_users_Update", (object[])sqlParameterArray1) > 0)
                        {
                            HttpContext.Current.Items[(object)"{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] = (object)null;
                            transaction.Commit();
                            sqlConnection.Close();
                            usersOrderIncome.ClearCache(_userinfo.ID);
                            return true;
                        }
                        transaction.Rollback();
                        sqlConnection.Close();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionHandler.HandleException(ex);
                        return false;
                    }
                }
            }
        }

        public static bool Exists(string username)
        {
            try
            {
                bool flag = false;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_Exists", new SqlParameter[1]
                {
          DataBase.MakeInParam("@userName", SqlDbType.NVarChar, 50, (object) username)
                });
                if (obj != null && obj != DBNull.Value)
                    flag = Convert.ToBoolean(obj);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static int EmailExists(string email)
        {
            try
            {
                int num = 999;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_EmailExists", new SqlParameter[1]
                {
          DataBase.MakeInParam("@email", SqlDbType.NVarChar, 50, (object) email)
                });
                if (obj != null && obj != DBNull.Value)
                    num = Convert.ToInt32(obj);
                return num;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 999;
            }
        }

        public static bool Exists(int userId)
        {
            try
            {
                bool flag = false;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_ExistsId", new SqlParameter[1]
                {
          DataBase.MakeInParam("@userId", SqlDbType.Int, 4, (object) userId)
                });
                if (obj != null && obj != DBNull.Value)
                    flag = Convert.ToBoolean(obj);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Del(int userId)
        {
            try
            {
                bool flag = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_users_del", new SqlParameter[1]
                {
          DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object) userId)
                }) > 0;
                if (flag)
                    usersOrderIncome.ClearCache(userId);
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static UserInfo GetCacheUserBaseInfo(int uid)
        {
            UserInfo userInfo1 = new UserInfo();
            string str = string.Format(usersOrderIncome.USER_CACHE_KEY, (object)uid);
            UserInfo userInfo2 = (UserInfo)WebCache.GetCacheService().RetrieveObject(str);
            if (userInfo2 == null)
            {
                IDictionary<string, object> parameters = (IDictionary<string, object>)new Dictionary<string, object>();
                parameters.Add("id", (object)uid);
                DataBase.AddSqlDependency(str, "userbase", "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug]", "[id]=@id", parameters);
                userInfo2 = usersOrderIncome.GetModel(uid);
                WebCache.GetCacheService().AddObject(str, (object)userInfo2);
            }
            return userInfo2;
        }

        public static UserInfo GetBaseModel(int uid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)uid;
            UserInfo userInfo = new UserInfo();
            return usersOrderIncome.GetBaseModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_userbase_get", sqlParameterArray));
        }

        public static UserInfo GetBaseModelFromDs(DataSet ds)
        {
            UserInfo userInfo = new UserInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    userInfo.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                if (ds.Tables[0].Rows[0]["classid"].ToString() != "")
                    userInfo.classid = int.Parse(ds.Tables[0].Rows[0]["classid"].ToString());
                userInfo.UserName = ds.Tables[0].Rows[0]["userName"].ToString();
                userInfo.Password = ds.Tables[0].Rows[0]["password"].ToString();
                userInfo.Password2 = ds.Tables[0].Rows[0]["pwd2"].ToString();
                if (ds.Tables[0].Rows[0]["CPSDrate"].ToString() != "")
                    userInfo.CPSDrate = int.Parse(ds.Tables[0].Rows[0]["CPSDrate"].ToString());
                if (ds.Tables[0].Rows[0]["CVSNrate"].ToString() != "")
                    userInfo.CVSNrate = int.Parse(ds.Tables[0].Rows[0]["CVSNrate"].ToString());
                userInfo.Email = ds.Tables[0].Rows[0]["email"].ToString();
                userInfo.QQ = ds.Tables[0].Rows[0]["qq"].ToString();
                userInfo.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                userInfo.IdCard = ds.Tables[0].Rows[0]["idCard"].ToString();
                userInfo.full_name = ds.Tables[0].Rows[0]["full_name"].ToString();
                userInfo.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                    userInfo.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                if (ds.Tables[0].Rows[0]["regTime"].ToString() != "")
                    userInfo.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["regTime"].ToString());
                if (ds.Tables[0].Rows[0]["agentId"].ToString() != "")
                    userInfo.AgentId = int.Parse(ds.Tables[0].Rows[0]["agentId"].ToString());
                userInfo.SiteName = ds.Tables[0].Rows[0]["siteName"].ToString();
                userInfo.SiteUrl = ds.Tables[0].Rows[0]["siteUrl"].ToString();
                if (ds.Tables[0].Rows[0]["userType"].ToString() != "")
                    userInfo.UserType = (UserTypeEnum)int.Parse(ds.Tables[0].Rows[0]["userType"].ToString());
                if (ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                    userInfo.UserLevel = (UserLevelEnum)int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                if (ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString() != "")
                    userInfo.MaxDayToCashTimes = int.Parse(ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString());
                if (ds.Tables[0].Rows[0]["apiaccount"].ToString() != "")
                    userInfo.APIAccount = long.Parse(ds.Tables[0].Rows[0]["apiaccount"].ToString());
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                    userInfo.manageId = new int?(int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString()));
                userInfo.IsRealNamePass = !(ds.Tables[0].Rows[0]["isRealNamePass"].ToString() != "") ? 0 : int.Parse(ds.Tables[0].Rows[0]["isRealNamePass"].ToString());
                userInfo.IsEmailPass = !(ds.Tables[0].Rows[0]["isEmailPass"].ToString() != "") ? 0 : int.Parse(ds.Tables[0].Rows[0]["isEmailPass"].ToString());
                userInfo.IsPhonePass = !(ds.Tables[0].Rows[0]["isPhonePass"].ToString() != "") ? 0 : int.Parse(ds.Tables[0].Rows[0]["isPhonePass"].ToString());
                userInfo.Settles = !(ds.Tables[0].Rows[0]["settles"].ToString() != "") ? 1 : int.Parse(ds.Tables[0].Rows[0]["settles"].ToString());
                userInfo.question = ds.Tables[0].Rows[0]["question"].ToString();
                userInfo.answer = ds.Tables[0].Rows[0]["answer"].ToString();
                userInfo.APIKey = ds.Tables[0].Rows[0]["APIkey"].ToString();
                userInfo.smsNotifyUrl = ds.Tables[0].Rows[0]["smsNotifyUrl"].ToString();
                if (ds.Tables[0].Rows[0]["isdebug"].ToString() != "")
                    userInfo.isdebug = int.Parse(ds.Tables[0].Rows[0]["isdebug"].ToString());
            }
            return userInfo;
        }

        public static UserInfo GetCacheModel(int uid)
        {
            UserInfo userInfo1 = new UserInfo();
            string str = string.Format(usersOrderIncome.USER_CACHE_KEY, (object)uid);
            UserInfo userInfo2 = (UserInfo)WebCache.GetCacheService().RetrieveObject(str);
            if (userInfo2 == null)
            {
                IDictionary<string, object> parameters = (IDictionary<string, object>)new Dictionary<string, object>();
                parameters.Add("id", (object)uid);
                DataBase.AddSqlDependency(str, "userbase", "[id],[pwd2],[full_name],[userName],[password],[CPSDrate],[CVSNrate],[email],[qq],[tel],[idCard],[settles],[status],[regTime],[company],[linkMan],[agentId],[siteName],[siteUrl],[userType],[userLevel],[maxdaytocashTimes],[apiaccount],[apikey],[updatetime],[DESC],isRealNamePass,isEmailPass,isPhonePass,[classid],[isdebug]", "[id]=@id", parameters);
                DataBase.AddSqlDependency(str, "userspaybank", "[userid],[pmode],[account],[payeeName],[payeeBank],[bankProvince],[bankCity],[bankAddress],[status]", "[userid]=@id", parameters);
                userInfo2 = usersOrderIncome.GetModel(uid);
                WebCache.GetCacheService().AddObject(str, (object)userInfo2);
            }
            return userInfo2;
        }

        public static UserInfo GetModel(int uid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)uid;
            UserInfo userInfo = new UserInfo();
            return usersOrderIncome.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_get", sqlParameterArray));
        }

        public static UserInfo GetModelByName(string userName)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@userName", SqlDbType.VarChar, 20)
            };
            sqlParameterArray[0].Value = (object)userName;
            UserInfo userInfo = new UserInfo();
            return usersOrderIncome.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_getbyname", sqlParameterArray));
        }

        public static UserInfo GetPromSuperior(int userId)
        {
            string commandText = "SELECT u.* FROM userbase u inner JOIN PromotionUser pu ON u.id = pu.PID\r\nWHERE pu.RegId = @RegId";
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@RegId", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)userId;
            UserInfo userInfo = new UserInfo();
            return usersOrderIncome.GetBaseModelFromDs(DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray));
        }

        public static int GetPromID(int userid)
        {
            try
            {
                string commandText = "SELECT top 1 pid FROM PromotionUser with(nolock) WHERE regid=@userid ";
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@userid", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)userid;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.Text, commandText, sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static UserInfo GetModelFromDs(DataSet ds)
        {
            UserInfo userInfo = new UserInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    userInfo.ID = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                if (ds.Tables[0].Rows[0]["classid"].ToString() != "")
                    userInfo.classid = int.Parse(ds.Tables[0].Rows[0]["classid"].ToString());
                userInfo.UserName = ds.Tables[0].Rows[0]["userName"].ToString();
                userInfo.Password = ds.Tables[0].Rows[0]["password"].ToString();
                userInfo.Password2 = ds.Tables[0].Rows[0]["pwd2"].ToString();
                if (ds.Tables[0].Rows[0]["CPSDrate"].ToString() != "")
                    userInfo.CPSDrate = int.Parse(ds.Tables[0].Rows[0]["CPSDrate"].ToString());
                if (ds.Tables[0].Rows[0]["CVSNrate"].ToString() != "")
                    userInfo.CVSNrate = int.Parse(ds.Tables[0].Rows[0]["CVSNrate"].ToString());
                userInfo.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                userInfo.Email = ds.Tables[0].Rows[0]["email"].ToString();
                userInfo.QQ = ds.Tables[0].Rows[0]["qq"].ToString();
                userInfo.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                userInfo.IdCard = ds.Tables[0].Rows[0]["idCard"].ToString();
                userInfo.Account = ds.Tables[0].Rows[0]["account"].ToString();
                userInfo.PayeeName = ds.Tables[0].Rows[0]["payeeName"].ToString();
                userInfo.PayeeBank = ds.Tables[0].Rows[0]["payeeBank"].ToString();
                userInfo.BankProvince = ds.Tables[0].Rows[0]["bankProvince"].ToString();
                userInfo.BankCity = ds.Tables[0].Rows[0]["bankCity"].ToString();
                userInfo.BankAddress = ds.Tables[0].Rows[0]["bankAddress"].ToString();
                userInfo.full_name = ds.Tables[0].Rows[0]["full_name"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                    userInfo.Status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                if (ds.Tables[0].Rows[0]["regTime"].ToString() != "")
                    userInfo.RegTime = DateTime.Parse(ds.Tables[0].Rows[0]["regTime"].ToString());
                if (ds.Tables[0].Rows[0]["balance"].ToString() != "")
                    userInfo.Balance = Decimal.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
                if (ds.Tables[0].Rows[0]["payment"].ToString() != "")
                    userInfo.Payment = Decimal.Parse(ds.Tables[0].Rows[0]["payment"].ToString());
                if (ds.Tables[0].Rows[0]["unpayment"].ToString() != "")
                    userInfo.Unpayment = Decimal.Parse(ds.Tables[0].Rows[0]["unpayment"].ToString());
                if (ds.Tables[0].Rows[0]["enableAmt"].ToString() != "")
                    userInfo.enableAmt = Decimal.Parse(ds.Tables[0].Rows[0]["enableAmt"].ToString());
                if (ds.Tables[0].Rows[0]["agentId"].ToString() != "")
                    userInfo.AgentId = int.Parse(ds.Tables[0].Rows[0]["agentId"].ToString());
                userInfo.SiteName = ds.Tables[0].Rows[0]["siteName"].ToString();
                userInfo.SiteUrl = ds.Tables[0].Rows[0]["siteUrl"].ToString();
                if (ds.Tables[0].Rows[0]["userType"].ToString() != "")
                    userInfo.UserType = (UserTypeEnum)int.Parse(ds.Tables[0].Rows[0]["userType"].ToString());
                if (ds.Tables[0].Rows[0]["userLevel"].ToString() != "")
                    userInfo.UserLevel = (UserLevelEnum)int.Parse(ds.Tables[0].Rows[0]["userLevel"].ToString());
                if (ds.Tables[0].Rows[0]["Integral"].ToString() != "")
                    userInfo.Integral = int.Parse(ds.Tables[0].Rows[0]["Integral"].ToString());
                if (ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString() != "")
                    userInfo.MaxDayToCashTimes = int.Parse(ds.Tables[0].Rows[0]["maxdaytocashTimes"].ToString());
                if (ds.Tables[0].Rows[0]["apiaccount"].ToString() != "")
                    userInfo.APIAccount = long.Parse(ds.Tables[0].Rows[0]["apiaccount"].ToString());
                userInfo.APIKey = ds.Tables[0].Rows[0]["APIkey"].ToString();
                userInfo.LastLoginIp = ds.Tables[0].Rows[0]["lastLoginIp"].ToString();
                if (ds.Tables[0].Rows[0]["lastLoginTime"].ToString() != "")
                    userInfo.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["lastLoginTime"].ToString());
                if (ds.Tables[0].Rows[0]["pmode"].ToString() != "")
                    userInfo.PMode = int.Parse(ds.Tables[0].Rows[0]["pmode"].ToString());
                userInfo.Desc = ds.Tables[0].Rows[0]["Desc"].ToString();
                if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                    userInfo.manageId = new int?(int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString()));
                userInfo.IsRealNamePass = !(ds.Tables[0].Rows[0]["isRealNamePass"].ToString() != "") ? 0 : int.Parse(ds.Tables[0].Rows[0]["isRealNamePass"].ToString());
                userInfo.IsEmailPass = !(ds.Tables[0].Rows[0]["isEmailPass"].ToString() != "") ? 0 : int.Parse(ds.Tables[0].Rows[0]["isEmailPass"].ToString());
                userInfo.IsPhonePass = !(ds.Tables[0].Rows[0]["isPhonePass"].ToString() != "") ? 0 : int.Parse(ds.Tables[0].Rows[0]["isPhonePass"].ToString());
                userInfo.Settles = !(ds.Tables[0].Rows[0]["settles"].ToString() != "") ? 1 : int.Parse(ds.Tables[0].Rows[0]["settles"].ToString());
                userInfo.question = ds.Tables[0].Rows[0]["question"].ToString();
                userInfo.answer = ds.Tables[0].Rows[0]["answer"].ToString();
                userInfo.smsNotifyUrl = ds.Tables[0].Rows[0]["smsNotifyUrl"].ToString();
                if (ds.Tables[0].Rows[0]["isdebug"].ToString() != "")
                    userInfo.isdebug = int.Parse(ds.Tables[0].Rows[0]["isdebug"].ToString());
                if (ds.Tables[0].Rows[0]["idCardtype"] != null && ds.Tables[0].Rows[0]["idCardtype"].ToString() != "")
                    userInfo.IdCardType = int.Parse(ds.Tables[0].Rows[0]["idCardtype"].ToString());
                if (ds.Tables[0].Rows[0]["msn"] != null && ds.Tables[0].Rows[0]["msn"].ToString() != "")
                    userInfo.msn = ds.Tables[0].Rows[0]["msn"].ToString();
                if (ds.Tables[0].Rows[0]["fax"] != null && ds.Tables[0].Rows[0]["fax"].ToString() != "")
                    userInfo.fax = ds.Tables[0].Rows[0]["fax"].ToString();
                if (ds.Tables[0].Rows[0]["province"] != null && ds.Tables[0].Rows[0]["province"].ToString() != "")
                    userInfo.province = ds.Tables[0].Rows[0]["province"].ToString();
                if (ds.Tables[0].Rows[0]["city"] != null && ds.Tables[0].Rows[0]["city"].ToString() != "")
                    userInfo.city = ds.Tables[0].Rows[0]["city"].ToString();
                if (ds.Tables[0].Rows[0]["zip"] != null && ds.Tables[0].Rows[0]["zip"].ToString() != "")
                    userInfo.zip = ds.Tables[0].Rows[0]["zip"].ToString();
                if (ds.Tables[0].Rows[0]["field1"] != null && ds.Tables[0].Rows[0]["field1"].ToString() != "")
                    userInfo.field1 = ds.Tables[0].Rows[0]["field1"].ToString();
            }
            return userInfo;
        }

        public static string GetClassViewName(int classid)
        {
            string str = string.Empty;
            if (classid == 0)
                str = "个人";
            else if (classid == 1)
                str = "企业";
            return str;
        }

        public static string GetClassViewName(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;
            return usersOrderIncome.GetClassViewName(Convert.ToInt32(obj));
        }

        public static int GetCurrent()
        {
            try
            {
                object obj1 = HttpContext.Current.Session["{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}"];
                if (obj1 == null)
                    return 0;
                object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdBySession", new SqlParameter[1]
                {
          DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, obj1)
                });
                if (obj2 != DBNull.Value)
                    return Convert.ToInt32(obj2);
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static int GetUserIdBySession(string _sessionId)
        {
            try
            {
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdBySession", new SqlParameter[1]
                {
          DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, (object) _sessionId)
                });
                if (obj != DBNull.Value)
                    return Convert.ToInt32(obj);
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static string SignIn(UserInfo userinfo)
        {
            string str1 = string.Empty;
            try
            {
                if (userinfo == null || string.IsNullOrEmpty(userinfo.UserName) || string.IsNullOrEmpty(userinfo.Password))
                    return "请输入账号密码";
                string str2 = "用户名或者密码错误,请重新输入!";
                string str3 = Guid.NewGuid().ToString("b");
                SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_users_Login", DataBase.MakeInParam("@username", SqlDbType.VarChar, 50, (object)userinfo.UserName), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, (object)userinfo.Password), DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, (object)userinfo.LastLoginIp), DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, (object)DateTime.Now), DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, (object)str3), DataBase.MakeInParam("@address", SqlDbType.VarChar, 20, (object)userinfo.LastLoginAddress), DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, (object)userinfo.LastLoginRemark), DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, (object)userinfo.Email));
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader["status"] != DBNull.Value)
                    {
                        userinfo.Status = (int)sqlDataReader["status"];
                        if (userinfo.Status == 1)
                            str2 = SysConfig.UserloginMsgForUnCheck;
                        else if (userinfo.Status == 2)
                        {
                            userinfo.ID = (int)sqlDataReader["userId"];
                            userinfo.UserType = (UserTypeEnum)Convert.ToInt32(sqlDataReader["userType"]);
                            userinfo.IsEmailPass = Convert.ToInt32(sqlDataReader["isEmailPass"]);
                            str2 = "登录成功";
                            HttpContext.Current.Session["{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}"] = (object)str3;
                        }
                        else if (userinfo.Status == 4)
                            str2 = SysConfig.UserloginMsgForlock;
                        else if (userinfo.Status == 8)
                            str2 = SysConfig.UserloginMsgForCheckfail;
                    }
                    sqlDataReader.Dispose();
                }
                return str2;
            }
            catch (Exception ex)
            {
                string str2 = "登录失败";
                ExceptionHandler.HandleException(ex);
                return str2;
            }
        }

        public static void SignOut()
        {
            HttpContext.Current.Items[(object)"{FD7BE212-8537-427f-9EF6-1D1AABCA8EA3}"] = (object)null;
            HttpContext.Current.Session["{10E6C4EE-54C1-4895-8CDE-202A5B3DD9E9}"] = (object)null;
        }

        public static UserInfo ReaderBind(IDataReader dataReader)
        {
            UserInfo userInfo = new UserInfo();
            object obj1 = dataReader["id"];
            if (obj1 != null && obj1 != DBNull.Value)
                userInfo.ID = (int)obj1;
            userInfo.UserName = dataReader["userName"].ToString();
            userInfo.Password = dataReader["password"].ToString();
            object obj2 = dataReader["CPSDrate"];
            if (obj2 != null && obj2 != DBNull.Value)
                userInfo.CPSDrate = (int)obj2;
            object obj3 = dataReader["CVSNrate"];
            if (obj3 != null && obj3 != DBNull.Value)
                userInfo.CVSNrate = (int)obj3;
            userInfo.Email = dataReader["email"].ToString();
            userInfo.QQ = dataReader["qq"].ToString();
            userInfo.Tel = dataReader["tel"].ToString();
            userInfo.IdCard = dataReader["idCard"].ToString();
            userInfo.Account = dataReader["account"].ToString();
            userInfo.PayeeName = dataReader["payeeName"].ToString();
            userInfo.PayeeBank = dataReader["payeeBank"].ToString();
            userInfo.BankProvince = dataReader["bankProvince"].ToString();
            userInfo.BankCity = dataReader["bankCity"].ToString();
            userInfo.BankAddress = dataReader["bankAddress"].ToString();
            object obj4 = dataReader["status"];
            if (obj4 != null && obj4 != DBNull.Value)
                userInfo.Status = (int)obj4;
            object obj5 = dataReader["regTime"];
            if (obj5 != null && obj5 != DBNull.Value)
                userInfo.RegTime = (DateTime)obj5;
            object obj6 = dataReader["balance"];
            if (obj6 != null && obj6 != DBNull.Value)
                userInfo.Balance = (Decimal)obj6;
            object obj7 = dataReader["payment"];
            if (obj7 != null && obj7 != DBNull.Value)
                userInfo.Payment = (Decimal)obj7;
            object obj8 = dataReader["unpayment"];
            if (obj8 != null && obj8 != DBNull.Value)
                userInfo.Unpayment = (Decimal)obj8;
            object obj9 = dataReader["agentId"];
            if (obj9 != null && obj9 != DBNull.Value)
                userInfo.AgentId = (int)obj9;
            userInfo.SiteName = dataReader["siteName"].ToString();
            userInfo.SiteUrl = dataReader["siteUrl"].ToString();
            object obj10 = dataReader["userType"];
            if (obj10 != null && obj10 != DBNull.Value)
                userInfo.UserType = (UserTypeEnum)obj10;
            object obj11 = dataReader["userLevel"];
            if (obj11 != null && obj11 != DBNull.Value)
                userInfo.UserLevel = (UserLevelEnum)obj11;
            object obj12 = dataReader["Integral"];
            if (obj12 != null && obj12 != DBNull.Value)
                userInfo.Integral = (int)obj12;
            object obj13 = dataReader["maxdaytocashTimes"];
            if (obj13 != null && obj13 != DBNull.Value)
                userInfo.MaxDayToCashTimes = (int)obj13;
            object obj14 = dataReader["apiaccount"];
            if (obj14 != null && obj14 != DBNull.Value)
                userInfo.APIAccount = (long)obj14;
            userInfo.APIKey = dataReader["apikey"].ToString();
            userInfo.LastLoginIp = dataReader["lastLoginIp"].ToString();
            object obj15 = dataReader["lastLoginTime"];
            if (obj15 != null && obj15 != DBNull.Value)
                userInfo.LastLoginTime = (DateTime)obj15;
            userInfo.smsNotifyUrl = dataReader["smsNotifyUrl"].ToString();
            userInfo.question = dataReader["question"].ToString();
            userInfo.answer = dataReader["answer"].ToString();
            object obj16 = dataReader["manageId"];
            if (obj16 != null && obj16 != DBNull.Value)
                userInfo.manageId = new int?((int)obj16);
            return userInfo;
        }

        public static List<int> GetUsers(string where)
        {
            List<int> list = new List<int>();
            if (string.IsNullOrEmpty(where))
                where = "1=1";
            SqlDataReader sqlDataReader = DataBase.ExecuteReader(CommandType.Text, "select id from dbo.userbase where " + where);
            while (sqlDataReader.Read())
                list.Add(sqlDataReader.GetInt32(0));
            return list;
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_Users";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = usersOrderIncome.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[userName]\r\n      ,[password]\r\n      ,[CPSDrate]\r\n      ,[CVSNrate]\r\n      ,[email]\r\n      ,[qq]\r\n      ,[tel]\r\n      ,[idCard]\r\n      ,[settles]\r\n      ,[status]\r\n      ,[regTime]\r\n      ,[company]\r\n      ,[linkMan]\r\n      ,[agentId]\r\n      ,[siteName]\r\n      ,[siteUrl]\r\n      ,[userType]\r\n      ,[userLevel]\r\n      ,[maxdaytocashTimes]\r\n      ,[apiaccount]\r\n      ,[apikey]\r\n      ,[lastLoginIp]\r\n      ,[lastLoginTime]\r\n      ,[sessionId]\r\n      ,[updatetime]\r\n      ,[DESC]\r\n      ,[userid]\r\n      ,[pmode]\r\n      ,[account]\r\n      ,[payeeName]\r\n      ,[payeeBank]\r\n      ,[bankProvince]\r\n      ,[bankCity]\r\n      ,[bankAddress]\r\n      ,[Integral]\r\n      ,[balance]\r\n      ,[payment]\r\n      ,[unpayment]\r\n      ,[enableAmt]\r\n      ,[manageId]\r\n      ,[isRealNamePass]\r\n      ,[isPhonePass]\r\n      ,[isEmailPass]\r\n      ,[question]\r\n      ,[answer]\r\n      ,[smsNotifyUrl]\r\n      ,[full_name]\r\n      ,[classid]\r\n      ,[Freeze]\r\n      ,[schemename]\r\n      ,[idCardtype]\r\n      ,[msn]\r\n      ,[fax]\r\n      ,[province]\r\n      ,[city]\r\n      ,[zip]\r\n      ,[field1],[levName]", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return dataSet;
            }
        }

        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder stringBuilder = new StringBuilder(" 1 = 1");
            if (param != null && param.Count > 0)
            {
                for (int index = 0; index < param.Count; ++index)
                {
                    SearchParam searchParam = param[index];
                    switch (searchParam.ParamKey.Trim().ToLower())
                    {
                        case "id":
                            stringBuilder.Append(" AND [id] = @id");
                            SqlParameter sqlParameter1 = new SqlParameter("@id", SqlDbType.Int);
                            sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter1);
                            break;
                        case "username":
                            stringBuilder.Append(" AND [userName] like @UserName");
                            SqlParameter sqlParameter2 = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                            sqlParameter2.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 20) + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "qq":
                            stringBuilder.Append(" AND [qq] like @qq");
                            SqlParameter sqlParameter3 = new SqlParameter("@qq", SqlDbType.VarChar, 50);
                            sqlParameter3.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 50) + "%");
                            paramList.Add(sqlParameter3);
                            break;
                        case "tel":
                            stringBuilder.Append(" AND [Tel] like @tel");
                            SqlParameter sqlParameter4 = new SqlParameter("@tel", SqlDbType.VarChar, 50);
                            sqlParameter4.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 50) + "%");
                            paramList.Add(sqlParameter4);
                            break;
                        case "email":
                            stringBuilder.Append(" AND [email] like @email");
                            SqlParameter sqlParameter5 = new SqlParameter("@email", SqlDbType.VarChar, 50);
                            sqlParameter5.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 50) + "%");
                            paramList.Add(sqlParameter5);
                            break;
                        case "full_name":
                            stringBuilder.Append(" AND [full_name] like @full_name");
                            SqlParameter sqlParameter6 = new SqlParameter("@full_name", SqlDbType.VarChar, 50);
                            sqlParameter6.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 50) + "%");
                            paramList.Add(sqlParameter6);
                            break;
                        case "status":
                            stringBuilder.Append(" AND [status] = @status");
                            SqlParameter sqlParameter7 = new SqlParameter("@status", SqlDbType.TinyInt);
                            sqlParameter7.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter7);
                            break;
                        case "usertype":
                            stringBuilder.Append(" AND [userType] = @userType");
                            SqlParameter sqlParameter8 = new SqlParameter("@userType", SqlDbType.TinyInt);
                            sqlParameter8.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter8);
                            break;
                        case "proid":
                            stringBuilder.Append(" AND Exists(select 0 from PromotionUser where PromotionUser.PID = @proid and PromotionUser.RegId=v_users.id)");
                            SqlParameter sqlParameter9 = new SqlParameter("@proid", SqlDbType.Int);
                            sqlParameter9.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter9);
                            break;
                        case "manageid":
                            stringBuilder.Append(" AND [manageId] = @manageId");
                            SqlParameter sqlParameter10 = new SqlParameter("@manageId", SqlDbType.Int);
                            sqlParameter10.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter10);
                            break;
                        case "balance":
                            stringBuilder.AppendFormat(" AND [balance] {0} @balance", (object)searchParam.CmpOperator);
                            SqlParameter sqlParameter11 = new SqlParameter("@balance", SqlDbType.Decimal);
                            sqlParameter11.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter11);
                            break;
                        case "enableamt":
                            stringBuilder.AppendFormat(" AND [enableAmt] {0} @enableAmt", (object)searchParam.CmpOperator);
                            SqlParameter sqlParameter12 = new SqlParameter("@enableAmt", SqlDbType.Decimal);
                            sqlParameter12.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter12);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public static bool DelUpdateLog(int id)
        {
            try
            {
                return DataBase.ExecuteNonQuery(CommandType.Text, "delete from usersupdate where id=@id", new SqlParameter[1]
                {
          DataBase.MakeInParam("@id", SqlDbType.Int, 4, (object) id)
                }) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static DataSet UpdateLogPageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "usersupdate";
                string key = "[id]";
                string columns = "id,\r\nuserid,\r\nfield,\r\noldValue,\r\nnewvalue,\r\nAddtime,\r\neditor,\r\noIp";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "Addtime desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = usersOrderIncome.BuilderUpdateLogWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(columns, tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return dataSet;
            }
        }

        private static string BuilderUpdateLogWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder stringBuilder = new StringBuilder(" 1 = 1");
            if (param != null && param.Count > 0)
            {
                for (int index = 0; index < param.Count; ++index)
                {
                    SearchParam searchParam = param[index];
                    switch (searchParam.ParamKey.Trim().ToLower())
                    {
                        case "id":
                            stringBuilder.Append(" AND [id] = @id");
                            SqlParameter sqlParameter1 = new SqlParameter("@id", SqlDbType.Int);
                            sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter1);
                            break;
                        case "userid":
                            stringBuilder.Append(" AND [userid] = @userid");
                            SqlParameter sqlParameter2 = new SqlParameter("@userid", SqlDbType.Int);
                            sqlParameter2.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter2);
                            break;
                        case "usertype":
                            stringBuilder.Append(" AND [userType] = @userType");
                            SqlParameter sqlParameter3 = new SqlParameter("@userType", SqlDbType.Int);
                            sqlParameter3.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public static DataTable getAgentList()
        {
            try
            {
                return DataBase.ExecuteDataset(CommandType.Text, "select id,userName from userbase with(nolock) where userType = 4").Tables[0];
            }
            catch
            {
                return (DataTable)null;
            }
        }

        public static bool CheckUserOrderId(int userid, string OrderId)
        {
            return Convert.ToBoolean(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_usersorderid_check", new SqlParameter[2]
            {
        DataBase.MakeInParam("@orderNo", SqlDbType.VarChar, 30, (object) OrderId),
        DataBase.MakeInParam("@userid", SqlDbType.Int, 4, (object) userid)
            }));
        }

        internal static void ClearCache(int userId)
        {
            WebCache.GetCacheService().RemoveObject(string.Format(usersOrderIncome.USER_CACHE_KEY, (object)userId));
        }
    }
}
