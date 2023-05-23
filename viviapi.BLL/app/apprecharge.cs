namespace viviapi.BLL.APP
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviLib.Data;
    using viviLib.ExceptionHandling;

    public class apprecharge
    {
        internal const string FIELD_NEWS = "[id]\r\n      ,[paytype]\r\n      ,[rechtype]\r\n      ,[orderid]\r\n      ,[account]\r\n      ,[userid]\r\n      ,[rechargeAmt]\r\n      ,[realPayAmt]\r\n      ,[addtime]\r\n      ,[status]\r\n      ,[processstatus]\r\n      ,[processtime]\r\n      ,[smsnotification]\r\n      ,[field1]\r\n      ,[field2]\r\n      ,[remark],Balance";
        internal const string SQL_TABLE = "apprecharge";

        public int Add(viviapi.Model.APP.apprecharge model)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@paytype", SqlDbType.Int, 4), new SqlParameter("@rechtype", SqlDbType.TinyInt, 1), new SqlParameter("@orderid", SqlDbType.VarChar, 30), new SqlParameter("@account", SqlDbType.NVarChar, 50), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@rechargeAmt", SqlDbType.Decimal, 9), new SqlParameter("@realPayAmt", SqlDbType.Decimal, 9), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@processstatus", SqlDbType.TinyInt, 1), new SqlParameter("@processtime", SqlDbType.DateTime), new SqlParameter("@smsnotification", SqlDbType.Bit, 1), new SqlParameter("@field1", SqlDbType.NVarChar, 50), new SqlParameter("@field2", SqlDbType.NVarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 200),
                new SqlParameter("@suppid", SqlDbType.Int, 4)
             };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.paytype;
            parameters[2].Value = model.rechtype;
            parameters[3].Value = model.orderid;
            parameters[4].Value = model.account;
            parameters[5].Value = model.userid;
            parameters[6].Value = model.rechargeAmt;
            parameters[7].Value = model.realPayAmt;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.status;
            parameters[10].Value = model.processstatus;
            parameters[11].Value = model.processtime;
            parameters[12].Value = model.smsnotification;
            parameters[13].Value = model.field1;
            parameters[14].Value = model.field2;
            parameters[15].Value = model.remark;
            parameters[0x10].Value = model.suppid;
            DbHelperSQL.RunProcedure("proc_apprecharge_ADD", parameters, out num);
            return (int)parameters[0].Value;
        }

        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                            if (str2 == "status")
                            {
                                goto Label_00E1;
                            }
                            if (str2 == "username")
                            {
                                goto Label_011D;
                            }
                            if (str2 == "starttime")
                            {
                                goto Label_016A;
                            }
                            if (str2 == "endtime")
                            {
                                goto Label_01A3;
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
                    goto Label_01DC;
                Label_00E1:
                    builder.Append(" AND [status] = @status");
                    parameter = new SqlParameter("@status", SqlDbType.Int);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_01DC;
                Label_011D:
                    builder.Append(" AND [account] like @userName");
                    parameter = new SqlParameter("@userName", SqlDbType.VarChar, 20);
                    parameter.Value = "%" + SqlHelper.CleanString((string)param2.ParamValue, 100) + "%";
                    paramList.Add(parameter);
                    goto Label_01DC;
                Label_016A:
                    builder.Append(" AND [addtime] > @starttime");
                    parameter = new SqlParameter("@starttime", SqlDbType.DateTime);
                    parameter.Value = Convert.ToDateTime(param2.ParamValue);
                    paramList.Add(parameter);
                    goto Label_01DC;
                Label_01A3:
                    builder.Append(" AND [addtime] < @endtime");
                    parameter = new SqlParameter("@endtime", SqlDbType.DateTime);
                    parameter.Value = Convert.ToDateTime(param2.ParamValue);
                    paramList.Add(parameter);
                Label_01DC:;
                }
            }
            return builder.ToString();
        }

        public int Complete(string transactionNo, string suppTranNo, decimal payMoney, int status)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@orderid", SqlDbType.VarChar, 30), new SqlParameter("@realPayAmt", SqlDbType.Decimal, 9), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@processtime", SqlDbType.DateTime), new SqlParameter("@supptranno", SqlDbType.VarChar, 30) };
                commandParameters[0].Value = transactionNo;
                commandParameters[1].Value = payMoney;
                commandParameters[2].Value = status;
                commandParameters[3].Value = DateTime.Now;
                commandParameters[4].Value = suppTranNo;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_apprecharge_complete", commandParameters));
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public viviapi.Model.APP.apprecharge DataRowToModel(DataRow row)
        {
            viviapi.Model.APP.apprecharge apprecharge = new viviapi.Model.APP.apprecharge();
            if (row != null)
            {
                if ((row["id"] != null) && (row["id"].ToString() != ""))
                {
                    apprecharge.id = int.Parse(row["id"].ToString());
                }
                if ((row["paytype"] != null) && (row["paytype"].ToString() != ""))
                {
                    apprecharge.paytype = int.Parse(row["paytype"].ToString());
                }
                if ((row["suppid"] != null) && (row["suppid"].ToString() != ""))
                {
                    apprecharge.suppid = int.Parse(row["suppid"].ToString());
                }
                if ((row["rechtype"] != null) && (row["rechtype"].ToString() != ""))
                {
                    apprecharge.rechtype = int.Parse(row["rechtype"].ToString());
                }
                if (row["orderid"] != null)
                {
                    apprecharge.orderid = row["orderid"].ToString();
                }
                if (row["account"] != null)
                {
                    apprecharge.account = row["account"].ToString();
                }
                if ((row["userid"] != null) && (row["userid"].ToString() != ""))
                {
                    apprecharge.userid = int.Parse(row["userid"].ToString());
                }
                if ((row["rechargeAmt"] != null) && (row["rechargeAmt"].ToString() != ""))
                {
                    apprecharge.rechargeAmt = decimal.Parse(row["rechargeAmt"].ToString());
                }
                if ((row["realPayAmt"] != null) && (row["realPayAmt"].ToString() != ""))
                {
                    apprecharge.realPayAmt = new decimal?(decimal.Parse(row["realPayAmt"].ToString()));
                }
                if ((row["addtime"] != null) && (row["addtime"].ToString() != ""))
                {
                    apprecharge.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if ((row["status"] != null) && (row["status"].ToString() != ""))
                {
                    apprecharge.status = int.Parse(row["status"].ToString());
                }
                if ((row["processstatus"] != null) && (row["processstatus"].ToString() != ""))
                {
                    apprecharge.processstatus = int.Parse(row["processstatus"].ToString());
                }
                if ((row["processtime"] != null) && (row["processtime"].ToString() != ""))
                {
                    apprecharge.processtime = new DateTime?(DateTime.Parse(row["processtime"].ToString()));
                }
                if ((row["smsnotification"] != null) && (row["smsnotification"].ToString() != ""))
                {
                    if ((row["smsnotification"].ToString() == "1") || (row["smsnotification"].ToString().ToLower() == "true"))
                    {
                        apprecharge.smsnotification = true;
                    }
                    else
                    {
                        apprecharge.smsnotification = false;
                    }
                }
                if (row["field1"] != null)
                {
                    apprecharge.field1 = row["field1"].ToString();
                }
                if (row["field2"] != null)
                {
                    apprecharge.field2 = row["field2"].ToString();
                }
                if (row["remark"] != null)
                {
                    apprecharge.remark = row["remark"].ToString();
                }
            }
            return apprecharge;
        }

        public bool Delete(int id)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            DbHelperSQL.RunProcedure("proc_apprecharge_Delete", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from apprecharge ");
            builder.Append(" where id in (" + idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int id)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            return (DbHelperSQL.RunProcedure("proc_apprecharge_Exists", parameters, out num) == 1);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,paytype,rechtype,orderid,account,userid,rechargeAmt,realPayAmt,addtime,status,processstatus,processtime,smsnotification,field1,field2,remark ");
            builder.Append(" FROM apprecharge ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" id,paytype,rechtype,orderid,account,userid,rechargeAmt,realPayAmt,addtime,status,processstatus,processtime,smsnotification,field1,field2,remark ");
            builder.Append(" FROM apprecharge ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
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
            builder.Append(")AS Row, T.*  from apprecharge T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "apprecharge");
        }

        public viviapi.Model.APP.apprecharge GetModel(string orderid)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@orderid", SqlDbType.VarChar, 30) };
            parameters[0].Value = orderid;
            viviapi.Model.APP.apprecharge apprecharge = new viviapi.Model.APP.apprecharge();
            DataSet set = DbHelperSQL.RunProcedure("proc_apprecharge_GetModel", parameters, "ds");
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM apprecharge ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public string GetStatusName(int status)
        {
            string str = "未付款";
            if (status == 2)
            {
                return "付款成功";
            }
            if (status == 4)
            {
                str = "付款失败";
            }
            return str;
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = "apprecharge";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "id desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = this.BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[paytype]\r\n      ,[rechtype]\r\n      ,[orderid]\r\n      ,[account]\r\n      ,[userid]\r\n      ,[rechargeAmt]\r\n      ,[realPayAmt]\r\n      ,[addtime]\r\n      ,[status]\r\n      ,[processstatus]\r\n      ,[processtime]\r\n      ,[smsnotification]\r\n      ,[field1]\r\n      ,[field2]\r\n      ,[remark],Balance", tables, wheres, orderby, key, pageSize, page, false);
                if (isstat)
                {
                    commandText = commandText + "\r\n select sum(realPayAmt) as realPayAmt from apprecharge where " + wheres;
                }
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public bool Update(viviapi.Model.APP.apprecharge model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@paytype", SqlDbType.Int, 4), new SqlParameter("@rechtype", SqlDbType.TinyInt, 1), new SqlParameter("@orderid", SqlDbType.VarChar, 30), new SqlParameter("@account", SqlDbType.NVarChar, 50), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@rechargeAmt", SqlDbType.Decimal, 9), new SqlParameter("@realPayAmt", SqlDbType.Decimal, 9), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@processstatus", SqlDbType.TinyInt, 1), new SqlParameter("@processtime", SqlDbType.DateTime), new SqlParameter("@smsnotification", SqlDbType.Bit, 1), new SqlParameter("@field1", SqlDbType.NVarChar, 50), new SqlParameter("@field2", SqlDbType.NVarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 200) };
            parameters[0].Value = model.id;
            parameters[1].Value = model.paytype;
            parameters[2].Value = model.rechtype;
            parameters[3].Value = model.orderid;
            parameters[4].Value = model.account;
            parameters[5].Value = model.userid;
            parameters[6].Value = model.rechargeAmt;
            parameters[7].Value = model.realPayAmt;
            parameters[8].Value = model.addtime;
            parameters[9].Value = model.status;
            parameters[10].Value = model.processstatus;
            parameters[11].Value = model.processtime;
            parameters[12].Value = model.smsnotification;
            parameters[13].Value = model.field1;
            parameters[14].Value = model.field2;
            parameters[15].Value = model.remark;
            DbHelperSQL.RunProcedure("proc_apprecharge_Update", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }
    }
}

