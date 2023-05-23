namespace viviapi.BLL.APP
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.Model.APP;
    using viviLib.Data;
    using viviLib.ExceptionHandling;

    public class transfer
    {
        internal const string SQL_FIELDS = "id,userid,status,billingName,bankname,province,city,branch,cardnum,payee,tranAmt,charges,paybank,email,mobile,iswarn,warnday,processstatus,processtime,smsnotification,field1,field2,remark ";
        internal const string SQL_TABLE = "transfer";

        public int Add(TransferInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@billingName", SqlDbType.NVarChar, 100), new SqlParameter("@bankname", SqlDbType.TinyInt, 1), new SqlParameter("@province", SqlDbType.NVarChar, 15), new SqlParameter("@city", SqlDbType.NVarChar, 15), new SqlParameter("@branch", SqlDbType.NVarChar, 100), new SqlParameter("@cardnum", SqlDbType.VarChar, 20), new SqlParameter("@payee", SqlDbType.NVarChar, 10), new SqlParameter("@tranAmt", SqlDbType.Decimal, 9), new SqlParameter("@charges", SqlDbType.Decimal, 9), new SqlParameter("@paybank", SqlDbType.TinyInt, 1), new SqlParameter("@email", SqlDbType.VarChar, 0x16), new SqlParameter("@mobile", SqlDbType.VarChar, 20), new SqlParameter("@iswarn", SqlDbType.TinyInt, 1),
                    new SqlParameter("@warnday", SqlDbType.Int, 4), new SqlParameter("@processstatus", SqlDbType.TinyInt, 1), new SqlParameter("@processtime", SqlDbType.DateTime), new SqlParameter("@smsnotification", SqlDbType.Bit, 1), new SqlParameter("@field1", SqlDbType.NVarChar, 50), new SqlParameter("@field2", SqlDbType.NVarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 200)
                 };
                commandParameters[0].Direction = ParameterDirection.Output;
                commandParameters[1].Value = model.userid;
                commandParameters[2].Value = model.status;
                commandParameters[3].Value = model.billingName;
                commandParameters[4].Value = model.bankname;
                commandParameters[5].Value = model.province;
                commandParameters[6].Value = model.city;
                commandParameters[7].Value = model.branch;
                commandParameters[8].Value = model.cardnum;
                commandParameters[9].Value = model.payee;
                commandParameters[10].Value = model.tranAmt;
                commandParameters[11].Value = model.charges;
                commandParameters[12].Value = model.paybank;
                commandParameters[13].Value = model.email;
                commandParameters[14].Value = model.mobile;
                commandParameters[15].Value = model.iswarn;
                commandParameters[0x10].Value = model.warnday;
                commandParameters[0x11].Value = model.processstatus;
                commandParameters[0x12].Value = model.processtime;
                commandParameters[0x13].Value = model.smsnotification;
                commandParameters[20].Value = model.field1;
                commandParameters[0x15].Value = model.field2;
                commandParameters[0x16].Value = model.remark;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_transfer_ADD", commandParameters);
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
                        if (!(str2 == "userid"))
                        {
                            if (str2 == "username")
                            {
                                goto Label_00E1;
                            }
                            if (str2 == "status")
                            {
                                goto Label_0131;
                            }
                            if (str2 == "stime")
                            {
                                goto Label_016B;
                            }
                            if (str2 == "etime")
                            {
                                goto Label_019A;
                            }
                        }
                        else
                        {
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                        }
                    }
                    goto Label_01C9;
                Label_00E1:
                    builder.Append(" AND [userName] like @UserName");
                    parameter = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                    parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 20) + "%";
                    paramList.Add(parameter);
                    goto Label_01C9;
                Label_0131:
                    builder.Append(" AND [status] = @status");
                    parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_01C9;
                Label_016B:
                    builder.Append(" AND [addtime] >= @stime");
                    parameter = new SqlParameter("@stime", SqlDbType.DateTime);
                    parameter.Value = param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_01C9;
                Label_019A:
                    builder.Append(" AND [addtime] <= @etime");
                    parameter = new SqlParameter("@etime", SqlDbType.DateTime);
                    parameter.Value = param2.ParamValue;
                    paramList.Add(parameter);
                Label_01C9:;
                }
            }
            return builder.ToString();
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_transfer_Delete", commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,userid,status,billingName,bankname,province,city,branch,cardnum,payee,tranAmt,charges,paybank,email,mobile,iswarn,warnday,processstatus,processtime,smsnotification,field1,field2,remark ");
            builder.Append(" FROM transfer ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), null);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" id,userid,status,billingName,bankname,province,city,branch,cardnum,payee,tranAmt,charges,paybank,email,mobile,iswarn,warnday,processstatus,processtime,smsnotification,field1,field2,remark ");
            builder.Append(" FROM transfer ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), null);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.id desc");
            }
            builder.Append(")AS Row, T.*  from transfer T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString(), null);
        }

        public TransferInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
                commandParameters[0].Value = id;
                return GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_transfer_GetModel", commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static TransferInfo GetModelFromDs(DataSet ds)
        {
            TransferInfo info = new TransferInfo();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if ((ds.Tables[0].Rows[0]["id"] != null) && (ds.Tables[0].Rows[0]["id"].ToString() != ""))
                {
                    info.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if ((ds.Tables[0].Rows[0]["userid"] != null) && (ds.Tables[0].Rows[0]["userid"].ToString() != ""))
                {
                    info.userid = new int?(int.Parse(ds.Tables[0].Rows[0]["userid"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["status"] != null) && (ds.Tables[0].Rows[0]["status"].ToString() != ""))
                {
                    info.status = new int?(int.Parse(ds.Tables[0].Rows[0]["status"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["billingName"] != null) && (ds.Tables[0].Rows[0]["billingName"].ToString() != ""))
                {
                    info.billingName = ds.Tables[0].Rows[0]["billingName"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["bankname"] != null) && (ds.Tables[0].Rows[0]["bankname"].ToString() != ""))
                {
                    info.bankname = new int?(int.Parse(ds.Tables[0].Rows[0]["bankname"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["province"] != null) && (ds.Tables[0].Rows[0]["province"].ToString() != ""))
                {
                    info.province = ds.Tables[0].Rows[0]["province"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["city"] != null) && (ds.Tables[0].Rows[0]["city"].ToString() != ""))
                {
                    info.city = ds.Tables[0].Rows[0]["city"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["branch"] != null) && (ds.Tables[0].Rows[0]["branch"].ToString() != ""))
                {
                    info.branch = ds.Tables[0].Rows[0]["branch"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["cardnum"] != null) && (ds.Tables[0].Rows[0]["cardnum"].ToString() != ""))
                {
                    info.cardnum = ds.Tables[0].Rows[0]["cardnum"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["payee"] != null) && (ds.Tables[0].Rows[0]["payee"].ToString() != ""))
                {
                    info.payee = ds.Tables[0].Rows[0]["payee"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["tranAmt"] != null) && (ds.Tables[0].Rows[0]["tranAmt"].ToString() != ""))
                {
                    info.tranAmt = new decimal?(decimal.Parse(ds.Tables[0].Rows[0]["tranAmt"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["charges"] != null) && (ds.Tables[0].Rows[0]["charges"].ToString() != ""))
                {
                    info.charges = new decimal?(decimal.Parse(ds.Tables[0].Rows[0]["charges"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["paybank"] != null) && (ds.Tables[0].Rows[0]["paybank"].ToString() != ""))
                {
                    info.paybank = new int?(int.Parse(ds.Tables[0].Rows[0]["paybank"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["email"] != null) && (ds.Tables[0].Rows[0]["email"].ToString() != ""))
                {
                    info.email = ds.Tables[0].Rows[0]["email"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["mobile"] != null) && (ds.Tables[0].Rows[0]["mobile"].ToString() != ""))
                {
                    info.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["iswarn"] != null) && (ds.Tables[0].Rows[0]["iswarn"].ToString() != ""))
                {
                    info.iswarn = new int?(int.Parse(ds.Tables[0].Rows[0]["iswarn"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["warnday"] != null) && (ds.Tables[0].Rows[0]["warnday"].ToString() != ""))
                {
                    info.warnday = new int?(int.Parse(ds.Tables[0].Rows[0]["warnday"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["processstatus"] != null) && (ds.Tables[0].Rows[0]["processstatus"].ToString() != ""))
                {
                    info.processstatus = new int?(int.Parse(ds.Tables[0].Rows[0]["processstatus"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["processtime"] != null) && (ds.Tables[0].Rows[0]["processtime"].ToString() != ""))
                {
                    info.processtime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["processtime"].ToString()));
                }
                if ((ds.Tables[0].Rows[0]["smsnotification"] != null) && (ds.Tables[0].Rows[0]["smsnotification"].ToString() != ""))
                {
                    if ((ds.Tables[0].Rows[0]["smsnotification"].ToString() == "1") || (ds.Tables[0].Rows[0]["smsnotification"].ToString().ToLower() == "true"))
                    {
                        info.smsnotification = true;
                    }
                    else
                    {
                        info.smsnotification = false;
                    }
                }
                if ((ds.Tables[0].Rows[0]["field1"] != null) && (ds.Tables[0].Rows[0]["field1"].ToString() != ""))
                {
                    info.field1 = ds.Tables[0].Rows[0]["field1"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["field2"] != null) && (ds.Tables[0].Rows[0]["field2"].ToString() != ""))
                {
                    info.field2 = ds.Tables[0].Rows[0]["field2"].ToString();
                }
                if ((ds.Tables[0].Rows[0]["remark"] != null) && (ds.Tables[0].Rows[0]["remark"].ToString() != ""))
                {
                    info.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                return info;
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM transfer ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object obj2 = DataBase.ExecuteScalar(CommandType.Text, builder.ToString(), null);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "transfer";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("id,userid,status,billingName,bankname,province,city,branch,cardnum,payee,tranAmt,charges,paybank,email,mobile,iswarn,warnday,processstatus,processtime,smsnotification,field1,field2,remark ", tables, wheres, orderby, key, pageSize, page, false);
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public bool Update(TransferInfo model)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] {
                    new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@billingName", SqlDbType.NVarChar, 100), new SqlParameter("@bankname", SqlDbType.TinyInt, 1), new SqlParameter("@province", SqlDbType.NVarChar, 15), new SqlParameter("@city", SqlDbType.NVarChar, 15), new SqlParameter("@branch", SqlDbType.NVarChar, 100), new SqlParameter("@cardnum", SqlDbType.VarChar, 20), new SqlParameter("@payee", SqlDbType.NVarChar, 10), new SqlParameter("@tranAmt", SqlDbType.Decimal, 9), new SqlParameter("@charges", SqlDbType.Decimal, 9), new SqlParameter("@paybank", SqlDbType.TinyInt, 1), new SqlParameter("@email", SqlDbType.VarChar, 0x16), new SqlParameter("@mobile", SqlDbType.VarChar, 20), new SqlParameter("@iswarn", SqlDbType.TinyInt, 1),
                    new SqlParameter("@warnday", SqlDbType.Int, 4), new SqlParameter("@processstatus", SqlDbType.TinyInt, 1), new SqlParameter("@processtime", SqlDbType.DateTime), new SqlParameter("@smsnotification", SqlDbType.Bit, 1), new SqlParameter("@field1", SqlDbType.NVarChar, 50), new SqlParameter("@field2", SqlDbType.NVarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 200)
                 };
                commandParameters[0].Value = model.id;
                commandParameters[1].Value = model.userid;
                commandParameters[2].Value = model.status;
                commandParameters[3].Value = model.billingName;
                commandParameters[4].Value = model.bankname;
                commandParameters[5].Value = model.province;
                commandParameters[6].Value = model.city;
                commandParameters[7].Value = model.branch;
                commandParameters[8].Value = model.cardnum;
                commandParameters[9].Value = model.payee;
                commandParameters[10].Value = model.tranAmt;
                commandParameters[11].Value = model.charges;
                commandParameters[12].Value = model.paybank;
                commandParameters[13].Value = model.email;
                commandParameters[14].Value = model.mobile;
                commandParameters[15].Value = model.iswarn;
                commandParameters[0x10].Value = model.warnday;
                commandParameters[0x11].Value = model.processstatus;
                commandParameters[0x12].Value = model.processtime;
                commandParameters[0x13].Value = model.smsnotification;
                commandParameters[20].Value = model.field1;
                commandParameters[0x15].Value = model.field2;
                commandParameters[0x16].Value = model.remark;
                return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_transfer_Update", commandParameters) > 0);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
    }
}

