namespace viviapi.DAL.Withdraw
{
    using DBAccess;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviLib.Data;
    using viviLib.ExceptionHandling;

    public class settledAgentSummary
    {
        internal string FIELDS = "[id]\r\n      ,[userid]\r\n      ,[lotno]\r\n      ,[qty]\r\n      ,[succqty]\r\n      ,[amt]\r\n      ,[succamt]\r\n      ,[fee]\r\n      ,[realfee]\r\n      ,[totalamt]\r\n      ,[totalsuccamt]\r\n      ,[status]\r\n      ,[success]\r\n      ,[audit_status]\r\n      ,[auditTime]\r\n      ,[auditUser]\r\n      ,[auditUserName]\r\n      ,[addtime]\r\n      ,[updatetime]\r\n      ,[remark]\r\n      ,[username]";
        internal string SQL_TABLE = "v_settledAgentSummary";

        public int Add(viviapi.Model.Withdraw.settledAgentSummary model)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@lotno", SqlDbType.VarChar, 30), new SqlParameter("@qty", SqlDbType.Int, 4), new SqlParameter("@succqty", SqlDbType.Int, 4), new SqlParameter("@amt", SqlDbType.Decimal, 9), new SqlParameter("@succamt", SqlDbType.Decimal, 9), new SqlParameter("@fee", SqlDbType.Decimal, 9), new SqlParameter("@realfee", SqlDbType.Decimal, 9), new SqlParameter("@totalamt", SqlDbType.Decimal, 9), new SqlParameter("@totalsuccamt", SqlDbType.Decimal, 13), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@updatetime", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.NVarChar, 100) };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.lotno;
            parameters[3].Value = model.qty;
            parameters[4].Value = model.succqty;
            parameters[5].Value = model.amt;
            parameters[6].Value = model.succamt;
            parameters[7].Value = model.fee;
            parameters[8].Value = model.realfee;
            parameters[9].Value = model.totalamt;
            parameters[10].Value = model.totalsuccamt;
            parameters[11].Value = model.status;
            parameters[12].Value = model.addtime;
            parameters[13].Value = model.updatetime;
            parameters[14].Value = model.remark;
            DbHelperSQL.RunProcedure("proc_settledAgentSummary_ADD", parameters, out num);
            return (int)parameters[0].Value;
        }

        public int Affirm(string lot_no, int auditstatus, int auditUser, string auditUserName, string clientip)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@lot_no", SqlDbType.VarChar, 40), new SqlParameter("@auditstatus", SqlDbType.TinyInt, 1), new SqlParameter("@auditUser", SqlDbType.Int), new SqlParameter("@auditTime", SqlDbType.DateTime), new SqlParameter("@auditUserName", SqlDbType.VarChar, 50), new SqlParameter("@clientip", SqlDbType.VarChar, 50), new SqlParameter("@result", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = lot_no;
            commandParameters[1].Value = auditstatus;
            commandParameters[2].Value = auditUser;
            commandParameters[3].Value = DateTime.Now;
            commandParameters[4].Value = auditUserName;
            commandParameters[5].Value = clientip;
            commandParameters[6].Direction = ParameterDirection.Output;
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settledAgentSummary_audit", commandParameters);
            return Convert.ToInt32(commandParameters[6].Value);
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
                            if (str2 == "lotno")
                            {
                                goto Label_00F5;
                            }
                            if (str2 == "status")
                            {
                                goto Label_0132;
                            }
                            if (str2 == "is_cancel")
                            {
                                goto Label_016F;
                            }
                            if (str2 == "saddtime")
                            {
                                goto Label_01A8;
                            }
                            if (str2 == "eaddtime")
                            {
                                goto Label_01D7;
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
                    goto Label_0206;
                Label_00F5:
                    builder.Append(" AND [lotno] like @lotno");
                    parameter = new SqlParameter("@lotno", SqlDbType.VarChar);
                    parameter.Value = param2.ParamValue + "%";
                    paramList.Add(parameter);
                    goto Label_0206;
                Label_0132:
                    builder.Append(" AND [status] = @status");
                    parameter = new SqlParameter("@status", SqlDbType.TinyInt);
                    parameter.Value = (int)param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_0206;
                Label_016F:
                    builder.Append(" AND [is_cancel] = @is_cancel");
                    parameter = new SqlParameter("@is_cancel", SqlDbType.Bit);
                    parameter.Value = (bool)param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_0206;
                Label_01A8:
                    builder.Append(" AND [addTime] >= @saddtime");
                    parameter = new SqlParameter("@saddtime", SqlDbType.DateTime);
                    parameter.Value = param2.ParamValue;
                    paramList.Add(parameter);
                    goto Label_0206;
                Label_01D7:
                    builder.Append(" AND [addTime] <= @eaddtime");
                    parameter = new SqlParameter("@eaddtime", SqlDbType.DateTime);
                    parameter.Value = param2.ParamValue;
                    paramList.Add(parameter);
                Label_0206:;
                }
            }
            return builder.ToString();
        }

        public int ChkParms(int userid, decimal tamount)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@i_amount", SqlDbType.Decimal, 0x12), new SqlParameter("@checkTime", SqlDbType.DateTime, 8), new SqlParameter("@result", SqlDbType.TinyInt) };
            commandParameters[0].Value = userid;
            commandParameters[1].Value = tamount;
            commandParameters[2].Value = DateTime.Now;
            commandParameters[3].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settledAgentSummary_chkParms", commandParameters);
            return Convert.ToInt32(commandParameters[3].Value);
        }

        public viviapi.Model.Withdraw.settledAgentSummary DataRowToModel(DataRow row)
        {
            viviapi.Model.Withdraw.settledAgentSummary summary = new viviapi.Model.Withdraw.settledAgentSummary();
            if (row != null)
            {
                if ((row["id"] != null) && (row["id"].ToString() != ""))
                {
                    summary.id = int.Parse(row["id"].ToString());
                }
                if ((row["userid"] != null) && (row["userid"].ToString() != ""))
                {
                    summary.userid = int.Parse(row["userid"].ToString());
                }
                if (row["lotno"] != null)
                {
                    summary.lotno = row["lotno"].ToString();
                }
                if ((row["qty"] != null) && (row["qty"].ToString() != ""))
                {
                    summary.qty = int.Parse(row["qty"].ToString());
                }
                if ((row["succqty"] != null) && (row["succqty"].ToString() != ""))
                {
                    summary.succqty = int.Parse(row["succqty"].ToString());
                }
                if ((row["amt"] != null) && (row["amt"].ToString() != ""))
                {
                    summary.amt = decimal.Parse(row["amt"].ToString());
                }
                if ((row["succamt"] != null) && (row["succamt"].ToString() != ""))
                {
                    summary.succamt = decimal.Parse(row["succamt"].ToString());
                }
                if ((row["fee"] != null) && (row["fee"].ToString() != ""))
                {
                    summary.fee = decimal.Parse(row["fee"].ToString());
                }
                if ((row["realfee"] != null) && (row["realfee"].ToString() != ""))
                {
                    summary.realfee = decimal.Parse(row["realfee"].ToString());
                }
                if ((row["totalamt"] != null) && (row["totalamt"].ToString() != ""))
                {
                    summary.totalamt = new decimal?(decimal.Parse(row["totalamt"].ToString()));
                }
                if ((row["totalsuccamt"] != null) && (row["totalsuccamt"].ToString() != ""))
                {
                    summary.totalsuccamt = new decimal?(decimal.Parse(row["totalsuccamt"].ToString()));
                }
                if ((row["status"] != null) && (row["status"].ToString() != ""))
                {
                    summary.status = int.Parse(row["status"].ToString());
                }
                if ((row["addtime"] != null) && (row["addtime"].ToString() != ""))
                {
                    summary.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if ((row["updatetime"] != null) && (row["updatetime"].ToString() != ""))
                {
                    summary.updatetime = DateTime.Parse(row["updatetime"].ToString());
                }
                if (row["remark"] != null)
                {
                    summary.remark = row["remark"].ToString();
                }
            }
            return summary;
        }

        public bool Delete(int id)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            DbHelperSQL.RunProcedure("proc_settledAgentSummary_Delete", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Delete(string lotno)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from settledAgentSummary ");
            builder.Append(" where lotno=@lotno ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@lotno", SqlDbType.VarChar, 30) };
            cmdParms[0].Value = lotno;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from settledAgentSummary ");
            builder.Append(" where id in (" + idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string lotno)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@lotno", SqlDbType.VarChar, 30) };
            parameters[0].Value = lotno;
            return (DbHelperSQL.RunProcedure("proc_settledAgentSummary_Exists", parameters, out num) == 1);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,userid,lotno,qty,succqty,amt,succamt,fee,realfee,totalamt,totalsuccamt,status,addtime,updatetime,remark ");
            builder.Append(" FROM settledAgentSummary ");
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
            builder.Append(" id,userid,lotno,qty,succqty,amt,succamt,fee,realfee,totalamt,totalsuccamt,status,addtime,updatetime,remark ");
            builder.Append(" FROM settledAgentSummary ");
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
            builder.Append(")AS Row, T.*  from settledAgentSummary T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public viviapi.Model.Withdraw.settledAgentSummary GetModel(int id)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            viviapi.Model.Withdraw.settledAgentSummary summary = new viviapi.Model.Withdraw.settledAgentSummary();
            DataSet set = DbHelperSQL.RunProcedure("proc_settledAgentSummary_GetModel", parameters, "ds");
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM settledAgentSummary ");
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

        public int Insert(viviapi.Model.Withdraw.settledAgentSummary summarymodel, List<viviapi.Model.Withdraw.settledAgent> itemlist)
        {
            int num3;
            using (SqlConnection connection = new SqlConnection(DataBase.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlParameter[] parameterArray = new SqlParameter[] {
                        new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@lotno", SqlDbType.VarChar, 30), new SqlParameter("@qty", SqlDbType.Int, 4), new SqlParameter("@succqty", SqlDbType.Int, 4), new SqlParameter("@amt", SqlDbType.Decimal, 9), new SqlParameter("@succamt", SqlDbType.Decimal, 9), new SqlParameter("@fee", SqlDbType.Decimal, 9), new SqlParameter("@realfee", SqlDbType.Decimal, 9), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@success", SqlDbType.TinyInt, 1), new SqlParameter("@audit_status", SqlDbType.TinyInt, 1), new SqlParameter("@auditTime", SqlDbType.DateTime), new SqlParameter("@auditUser", SqlDbType.Int, 4), new SqlParameter("@auditUserName", SqlDbType.VarChar, 50), new SqlParameter("@addtime", SqlDbType.DateTime),
                        new SqlParameter("@updatetime", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.NVarChar, 100)
                     };
                    parameterArray[0].Direction = ParameterDirection.Output;
                    parameterArray[1].Value = summarymodel.userid;
                    parameterArray[2].Value = summarymodel.lotno;
                    parameterArray[3].Value = summarymodel.qty;
                    parameterArray[4].Value = summarymodel.succqty;
                    parameterArray[5].Value = summarymodel.amt;
                    parameterArray[6].Value = summarymodel.succamt;
                    parameterArray[7].Value = summarymodel.fee;
                    parameterArray[8].Value = summarymodel.realfee;
                    parameterArray[9].Value = summarymodel.status;
                    parameterArray[10].Value = summarymodel.success;
                    parameterArray[11].Value = summarymodel.audit_status;
                    parameterArray[12].Value = summarymodel.auditTime;
                    parameterArray[13].Value = summarymodel.auditUser;
                    parameterArray[14].Value = summarymodel.auditUserName;
                    parameterArray[15].Value = summarymodel.addtime;
                    parameterArray[0x10].Value = summarymodel.updatetime;
                    parameterArray[0x11].Value = summarymodel.remark;
                    int num = Convert.ToInt32(DataBase.ExecuteScalar(transaction, "proc_settledAgentSummary_ADD", (object[])parameterArray));
                    if (num > 0)
                    {
                        foreach (viviapi.Model.Withdraw.settledAgent agent in itemlist)
                        {
                            int num2;
                            SqlParameter[] parameters = new SqlParameter[] {
                                new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@mode", SqlDbType.TinyInt, 1), new SqlParameter("@lotno", SqlDbType.VarChar, 30), new SqlParameter("@serial", SqlDbType.Int, 4), new SqlParameter("@trade_no", SqlDbType.VarChar, 40), new SqlParameter("@out_trade_no", SqlDbType.VarChar, 0x40), new SqlParameter("@service", SqlDbType.VarChar, 40), new SqlParameter("@input_charset", SqlDbType.VarChar, 20), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@sign_type", SqlDbType.VarChar, 8), new SqlParameter("@return_url", SqlDbType.VarChar, 0x100), new SqlParameter("@bankCode", SqlDbType.VarChar, 10), new SqlParameter("@bankName", SqlDbType.NVarChar, 20), new SqlParameter("@bankBranch", SqlDbType.NVarChar, 0xff), new SqlParameter("@bankAccountName", SqlDbType.NVarChar, 20), new SqlParameter("@bankAccount", SqlDbType.VarChar, 50),
                                new SqlParameter("@amount", SqlDbType.Decimal, 9), new SqlParameter("@charge", SqlDbType.Decimal, 9), new SqlParameter("@addTime", SqlDbType.DateTime), new SqlParameter("@processingTime", SqlDbType.DateTime), new SqlParameter("@audit_status", SqlDbType.TinyInt, 1), new SqlParameter("@auditTime", SqlDbType.DateTime), new SqlParameter("@auditUser", SqlDbType.Int, 4), new SqlParameter("@auditUserName", SqlDbType.VarChar, 50), new SqlParameter("@payment_status", SqlDbType.TinyInt, 1), new SqlParameter("@is_cancel", SqlDbType.Bit, 1), new SqlParameter("@ext1", SqlDbType.VarChar, 50), new SqlParameter("@ext2", SqlDbType.VarChar, 50), new SqlParameter("@ext3", SqlDbType.VarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 500), new SqlParameter("@tranApi", SqlDbType.Int, 4), new SqlParameter("@suppstatus", SqlDbType.TinyInt, 1),
                                new SqlParameter("@notifyTimes", SqlDbType.Int, 4), new SqlParameter("@notifystatus", SqlDbType.TinyInt, 1), new SqlParameter("@callbackText", SqlDbType.NVarChar, 50)
                             };
                            parameters[0].Direction = ParameterDirection.Output;
                            parameters[1].Value = agent.mode;
                            parameters[2].Value = agent.lotno;
                            parameters[3].Value = agent.serial;
                            parameters[4].Value = agent.trade_no;
                            parameters[5].Value = agent.out_trade_no;
                            parameters[6].Value = agent.service;
                            parameters[7].Value = agent.input_charset;
                            parameters[8].Value = agent.userid;
                            parameters[9].Value = agent.sign_type;
                            parameters[10].Value = agent.return_url;
                            parameters[11].Value = agent.bankCode;
                            parameters[12].Value = agent.bankName;
                            parameters[13].Value = agent.bankBranch;
                            parameters[14].Value = agent.bankAccountName;
                            parameters[15].Value = agent.bankAccount;
                            parameters[0x10].Value = agent.amount;
                            parameters[0x11].Value = agent.charge;
                            parameters[0x12].Value = agent.addTime;
                            parameters[0x13].Value = agent.processingTime;
                            parameters[20].Value = agent.audit_status;
                            parameters[0x15].Value = agent.auditTime;
                            parameters[0x16].Value = agent.auditUser;
                            parameters[0x17].Value = agent.auditUserName;
                            parameters[0x18].Value = agent.payment_status;
                            parameters[0x19].Value = agent.is_cancel;
                            parameters[0x1a].Value = agent.ext1;
                            parameters[0x1b].Value = agent.ext2;
                            parameters[0x1c].Value = agent.ext3;
                            parameters[0x1d].Value = agent.remark;
                            parameters[30].Value = agent.suppid;
                            parameters[0x1f].Value = agent.suppstatus;
                            parameters[0x20].Value = agent.notifyTimes;
                            parameters[0x21].Value = agent.notifystatus;
                            parameters[0x22].Value = agent.callbackText;
                            DbHelperSQL.RunProcedure("proc_settledAgent_ADD2", parameters, out num2);
                        }
                        transaction.Commit();
                        connection.Close();
                        return num;
                    }
                    transaction.Rollback();
                    connection.Close();
                    return 0;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    ExceptionHandler.HandleException(exception);
                    return 0;
                }
                finally
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                    }
                }
            }
            return num3;
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            DataSet set = new DataSet();
            try
            {
                string tables = this.SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "addTime desc";
                }
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = BuilderWhere(searchParams, paramList);
                string commandText = SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(this.FIELDS, tables, wheres, orderby, key, pageSize, page, false);
                if (isstat)
                {
                    commandText = commandText + "\r\nselect sum(amount) as amount,sum(charge) as charge,sum(amount+charge) as totalpay from v_settledAgent where " + wheres;
                }
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public bool Update(viviapi.Model.Withdraw.settledAgentSummary model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@lotno", SqlDbType.VarChar, 30), new SqlParameter("@qty", SqlDbType.Int, 4), new SqlParameter("@succqty", SqlDbType.Int, 4), new SqlParameter("@amt", SqlDbType.Decimal, 9), new SqlParameter("@succamt", SqlDbType.Decimal, 9), new SqlParameter("@fee", SqlDbType.Decimal, 9), new SqlParameter("@realfee", SqlDbType.Decimal, 9), new SqlParameter("@totalamt", SqlDbType.Decimal, 9), new SqlParameter("@totalsuccamt", SqlDbType.Decimal, 13), new SqlParameter("@status", SqlDbType.TinyInt, 1), new SqlParameter("@addtime", SqlDbType.DateTime), new SqlParameter("@updatetime", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.NVarChar, 100) };
            parameters[0].Value = model.id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.lotno;
            parameters[3].Value = model.qty;
            parameters[4].Value = model.succqty;
            parameters[5].Value = model.amt;
            parameters[6].Value = model.succamt;
            parameters[7].Value = model.fee;
            parameters[8].Value = model.realfee;
            parameters[9].Value = model.totalamt;
            parameters[10].Value = model.totalsuccamt;
            parameters[11].Value = model.status;
            parameters[12].Value = model.addtime;
            parameters[13].Value = model.updatetime;
            parameters[14].Value = model.remark;
            DbHelperSQL.RunProcedure("proc_settledAgentSummary_Update", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }
    }
}

