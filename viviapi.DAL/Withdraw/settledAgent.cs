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

    public class settledAgent
    {
        internal string FIELDS = "[id]\r\n      ,[mode]\r\n      ,[trade_no]\r\n      ,[out_trade_no]\r\n      ,[service]\r\n      ,[userid]\r\n      ,[sign_type]\r\n      ,[return_url]\r\n      ,[bankCode]\r\n      ,[bankName]\r\n      ,[bankBranch]\r\n      ,[bankAccountName]\r\n      ,[bankAccount]\r\n      ,[amount]\r\n      ,[charge]\r\n      ,[addTime]\r\n      ,[processingTime]\r\n      ,[audit_status]\r\n      ,[payment_status]\r\n      ,[is_cancel]\r\n      ,[ext1]\r\n      ,[ext2]\r\n      ,[ext3]\r\n      ,[remark]\r\n      ,[tranApi]\r\n      ,[notifyTimes]\r\n      ,[notifystatus]\r\n      ,[callbackText]\r\n      ,[username],[input_charset],[suppstatus],lotno,serial,totalamt,[issure]";
        internal string SQL_TABLE = "v_settledAgent";

        public int Add(viviapi.Model.Withdraw.settledAgent model)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@mode", SqlDbType.TinyInt, 1), new SqlParameter("@trade_no", SqlDbType.VarChar, 40), new SqlParameter("@out_trade_no", SqlDbType.VarChar, 0x40), new SqlParameter("@service", SqlDbType.VarChar, 40), new SqlParameter("@input_charset", SqlDbType.VarChar, 20), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@sign_type", SqlDbType.VarChar, 8), new SqlParameter("@return_url", SqlDbType.VarChar, 0x100), new SqlParameter("@bankCode", SqlDbType.VarChar, 10), new SqlParameter("@bankName", SqlDbType.NVarChar, 20), new SqlParameter("@bankBranch", SqlDbType.NVarChar, 0xff), new SqlParameter("@bankAccountName", SqlDbType.NVarChar, 20), new SqlParameter("@bankAccount", SqlDbType.VarChar, 50), new SqlParameter("@amount", SqlDbType.Decimal, 9), new SqlParameter("@charge", SqlDbType.Decimal, 9),
                new SqlParameter("@addTime", SqlDbType.DateTime), new SqlParameter("@processingTime", SqlDbType.DateTime), new SqlParameter("@audit_status", SqlDbType.TinyInt, 1), new SqlParameter("@auditTime", SqlDbType.DateTime), new SqlParameter("@auditUser", SqlDbType.Int, 4), new SqlParameter("@auditUserName", SqlDbType.VarChar, 50), new SqlParameter("@payment_status", SqlDbType.TinyInt, 1), new SqlParameter("@is_cancel", SqlDbType.Bit, 1), new SqlParameter("@ext1", SqlDbType.VarChar, 50), new SqlParameter("@ext2", SqlDbType.VarChar, 50), new SqlParameter("@ext3", SqlDbType.VarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 500), new SqlParameter("@tranApi", SqlDbType.Int, 4), new SqlParameter("@notifyTimes", SqlDbType.Int, 4), new SqlParameter("@notifystatus", SqlDbType.TinyInt, 1), new SqlParameter("@callbackText", SqlDbType.NVarChar, 50),
                new SqlParameter("@issure", SqlDbType.TinyInt, 1), new SqlParameter("@sureclientip", SqlDbType.NVarChar, 50)
             };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.mode;
            parameters[2].Value = model.trade_no;
            parameters[3].Value = model.out_trade_no;
            parameters[4].Value = model.service;
            parameters[5].Value = model.input_charset;
            parameters[6].Value = model.userid;
            parameters[7].Value = model.sign_type;
            parameters[8].Value = model.return_url;
            parameters[9].Value = model.bankCode;
            parameters[10].Value = model.bankName;
            parameters[11].Value = model.bankBranch;
            parameters[12].Value = model.bankAccountName;
            parameters[13].Value = model.bankAccount;
            parameters[14].Value = model.amount;
            parameters[15].Value = model.charge;
            parameters[0x10].Value = model.addTime;
            parameters[0x11].Value = model.processingTime;
            parameters[0x12].Value = model.audit_status;
            parameters[0x13].Value = model.auditTime;
            parameters[20].Value = model.auditUser;
            parameters[0x15].Value = model.auditUserName;
            parameters[0x16].Value = model.payment_status;
            parameters[0x17].Value = model.is_cancel;
            parameters[0x18].Value = model.ext1;
            parameters[0x19].Value = model.ext2;
            parameters[0x1a].Value = model.ext3;
            parameters[0x1b].Value = model.remark;
            parameters[0x1c].Value = model.suppid;
            parameters[0x1d].Value = model.notifyTimes;
            parameters[30].Value = model.notifystatus;
            parameters[0x1f].Value = model.callbackText;
            parameters[0x20].Value = model.issure;
            parameters[0x21].Value = model.sureclientip;
            DbHelperSQL.RunProcedure("proc_settledAgent_ADD", parameters, out num);
            return (int)parameters[0].Value;
        }

        public int Affirm(string trade_no, byte sure, string clientip)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@trade_no", SqlDbType.VarChar, 40), new SqlParameter("@sure", SqlDbType.TinyInt, 1), new SqlParameter("@sureTime", SqlDbType.DateTime, 8), new SqlParameter("@clientip", SqlDbType.VarChar, 50), new SqlParameter("@result", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = trade_no;
            commandParameters[1].Value = sure;
            commandParameters[3].Value = DateTime.Now;
            commandParameters[4].Value = clientip;
            commandParameters[5].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_settledAgent_affirm", commandParameters);
            return Convert.ToInt32(commandParameters[5].Value);
        }

        public int Audit(string trade_no, int auditstatus, int auditUser, string auditUserName)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@trade_no", SqlDbType.VarChar, 40), new SqlParameter("@auditstatus", SqlDbType.TinyInt, 1), new SqlParameter("@auditUser", SqlDbType.Int), new SqlParameter("@auditTime", SqlDbType.DateTime), new SqlParameter("@auditUserName", SqlDbType.VarChar, 50), new SqlParameter("@result", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = trade_no;
            commandParameters[1].Value = auditstatus;
            commandParameters[2].Value = auditUser;
            commandParameters[3].Value = DateTime.Now;
            commandParameters[4].Value = auditUserName;
            commandParameters[5].Direction = ParameterDirection.Output;
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settledAgent_audit", commandParameters);
            return Convert.ToInt32(commandParameters[5].Value);
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
                        case "userid":
                            builder.Append(" AND [userid] = @userid");
                            parameter = new SqlParameter("@userid", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "trade_no":
                            builder.Append(" AND [trade_no] like @trade_no");
                            parameter = new SqlParameter("@trade_no", SqlDbType.VarChar);
                            parameter.Value = param2.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "out_trade_no":
                            builder.Append(" AND [out_trade_no] like @out_trade_no");
                            parameter = new SqlParameter("@out_trade_no", SqlDbType.VarChar);
                            parameter.Value = param2.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "lotno":
                            builder.Append(" AND [lotno] like @lotno");
                            parameter = new SqlParameter("@lotno", SqlDbType.VarChar);
                            parameter.Value = param2.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "bankcode":
                            builder.Append(" AND [bankCode] like @bankCode");
                            parameter = new SqlParameter("@bankCode", SqlDbType.VarChar);
                            parameter.Value = param2.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "bankname":
                            builder.Append(" AND [bankName] like @bankName");
                            parameter = new SqlParameter("@bankName", SqlDbType.VarChar);
                            parameter.Value = param2.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "bankaccount":
                            builder.Append(" AND [bankAccount] like @bankAccount");
                            parameter = new SqlParameter("@bankAccount", SqlDbType.VarChar);
                            parameter.Value = param2.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "bankaccountname":
                            builder.Append(" AND [bankAccountName] like @bankAccountName");
                            parameter = new SqlParameter("@bankAccountName", SqlDbType.VarChar);
                            parameter.Value = param2.ParamValue + "%";
                            paramList.Add(parameter);
                            break;

                        case "tranapi":
                            builder.Append(" AND [tranapi] = @tranapi");
                            parameter = new SqlParameter("@tranapi", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "audit_status":
                            builder.Append(" AND [audit_status] = @audit_status");
                            parameter = new SqlParameter("@audit_status", SqlDbType.TinyInt);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "payment_status":
                            builder.Append(" AND [payment_status] = @payment_status");
                            parameter = new SqlParameter("@payment_status", SqlDbType.TinyInt);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "notifystatus":
                            builder.Append(" AND [notifystatus] = @notifystatus");
                            parameter = new SqlParameter("@notifystatus", SqlDbType.TinyInt);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "is_cancel":
                            builder.Append(" AND [is_cancel] = @is_cancel");
                            parameter = new SqlParameter("@is_cancel", SqlDbType.Bit);
                            parameter.Value = (bool)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "id":
                            builder.Append(" AND [id] = @id");
                            parameter = new SqlParameter("@id", SqlDbType.Int);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "mode":
                            builder.Append(" AND [mode] = @mode");
                            parameter = new SqlParameter("@mode", SqlDbType.TinyInt);
                            parameter.Value = (int)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "amount_from":
                            builder.Append(" AND [amount] <= @amount_from");
                            parameter = new SqlParameter("@amount_from", SqlDbType.Decimal);
                            parameter.Value = (decimal)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "amount_to":
                            builder.Append(" AND [amount] <= @amount_to");
                            parameter = new SqlParameter("@amount_to", SqlDbType.Decimal);
                            parameter.Value = (decimal)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "begindate":
                            builder.Append(" AND [processingTime] >= @beginpaytime");
                            parameter = new SqlParameter("@beginpaytime", SqlDbType.DateTime);
                            parameter.Value = (DateTime)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "enddate":
                            builder.Append(" AND [processingTime] <= @endpaytime");
                            parameter = new SqlParameter("@endpaytime", SqlDbType.DateTime);
                            parameter.Value = (DateTime)param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "saddtime":
                            builder.Append(" AND [addTime] >= @saddtime");
                            parameter = new SqlParameter("@saddtime", SqlDbType.DateTime);
                            parameter.Value = param2.ParamValue;
                            paramList.Add(parameter);
                            break;

                        case "eaddtime":
                            builder.Append(" AND [addTime] <= @eaddtime");
                            parameter = new SqlParameter("@eaddtime", SqlDbType.DateTime);
                            parameter.Value = param2.ParamValue;
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }

        public int Cancel(string trade_no)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@trade_no", SqlDbType.VarChar, 40), new SqlParameter("@result", SqlDbType.TinyInt, 0) };
            commandParameters[0].Value = trade_no;
            commandParameters[1].Direction = ParameterDirection.Output;
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settledAgent_cancel", commandParameters);
            return Convert.ToInt32(commandParameters[1].Value);
        }

        public int ChkParms(int userid, string backcode, decimal amount, out DataRow row)
        {
            row = null;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@i_userid", SqlDbType.Int, 4), new SqlParameter("@i_amount", SqlDbType.Decimal, 0x12), new SqlParameter("@i_bankCode", SqlDbType.VarChar, 10), new SqlParameter("@checkTime", SqlDbType.DateTime, 8), new SqlParameter("@result", SqlDbType.TinyInt) };
            commandParameters[0].Value = userid;
            commandParameters[1].Value = amount;
            commandParameters[2].Value = backcode;
            commandParameters[3].Value = DateTime.Now;
            commandParameters[4].Direction = ParameterDirection.Output;
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settledAgent_chkParms", commandParameters);
            int num = Convert.ToInt32(commandParameters[3].Value);
            if (((num == 0) && (set != null)) && (set.Tables.Count > 0))
            {
                row = set.Tables[0].Rows[0];
            }
            return num;
        }

        public int Complete(string trade_no, int pstatus)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@trade_no", SqlDbType.VarChar, 40), new SqlParameter("@pstatus", SqlDbType.TinyInt, 1), new SqlParameter("@processingTime", SqlDbType.DateTime), new SqlParameter("@result", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = trade_no;
            commandParameters[1].Value = pstatus;
            commandParameters[2].Value = DateTime.Now;
            commandParameters[3].Direction = ParameterDirection.Output;
            DataSet set = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_settledAgent_complete", commandParameters);
            return Convert.ToInt32(commandParameters[3].Value);
        }

        public viviapi.Model.Withdraw.settledAgent DataRowToModel(DataRow row)
        {
            viviapi.Model.Withdraw.settledAgent agent = new viviapi.Model.Withdraw.settledAgent();
            if (row != null)
            {
                if ((row["id"] != null) && (row["id"].ToString() != ""))
                {
                    agent.id = int.Parse(row["id"].ToString());
                }
                agent.lotno = row["lotno"].ToString();
                if ((row["serial"] != null) && (row["serial"].ToString() != ""))
                {
                    agent.serial = int.Parse(row["serial"].ToString());
                }
                if ((row["mode"] != null) && (row["mode"].ToString() != ""))
                {
                    agent.mode = int.Parse(row["mode"].ToString());
                }
                if (row["trade_no"] != null)
                {
                    agent.trade_no = row["trade_no"].ToString();
                }
                if (row["out_trade_no"] != null)
                {
                    agent.out_trade_no = row["out_trade_no"].ToString();
                }
                if (row["service"] != null)
                {
                    agent.service = row["service"].ToString();
                }
                if (row["input_charset"] != null)
                {
                    agent.input_charset = row["input_charset"].ToString();
                }
                if ((row["userid"] != null) && (row["userid"].ToString() != ""))
                {
                    agent.userid = int.Parse(row["userid"].ToString());
                }
                if (row["sign_type"] != null)
                {
                    agent.sign_type = row["sign_type"].ToString();
                }
                if (row["return_url"] != null)
                {
                    agent.return_url = row["return_url"].ToString();
                }
                if (row["bankCode"] != null)
                {
                    agent.bankCode = row["bankCode"].ToString();
                }
                if (row["bankName"] != null)
                {
                    agent.bankName = row["bankName"].ToString();
                }
                if (row["bankBranch"] != null)
                {
                    agent.bankBranch = row["bankBranch"].ToString();
                }
                if (row["bankAccountName"] != null)
                {
                    agent.bankAccountName = row["bankAccountName"].ToString();
                }
                if (row["bankAccount"] != null)
                {
                    agent.bankAccount = row["bankAccount"].ToString();
                }
                if ((row["amount"] != null) && (row["amount"].ToString() != ""))
                {
                    agent.amount = decimal.Parse(row["amount"].ToString());
                }
                if ((row["charge"] != null) && (row["charge"].ToString() != ""))
                {
                    agent.charge = decimal.Parse(row["charge"].ToString());
                }
                if ((row["addTime"] != null) && (row["addTime"].ToString() != ""))
                {
                    agent.addTime = DateTime.Parse(row["addTime"].ToString());
                }
                if ((row["processingTime"] != null) && (row["processingTime"].ToString() != ""))
                {
                    agent.processingTime = DateTime.Parse(row["processingTime"].ToString());
                }
                if ((row["audit_status"] != null) && (row["audit_status"].ToString() != ""))
                {
                    agent.audit_status = int.Parse(row["audit_status"].ToString());
                }
                if ((row["auditTime"] != null) && (row["auditTime"].ToString() != ""))
                {
                    agent.auditTime = new DateTime?(DateTime.Parse(row["auditTime"].ToString()));
                }
                if ((row["auditUser"] != null) && (row["auditUser"].ToString() != ""))
                {
                    agent.auditUser = new int?(int.Parse(row["auditUser"].ToString()));
                }
                if (row["auditUserName"] != null)
                {
                    agent.auditUserName = row["auditUserName"].ToString();
                }
                if ((row["payment_status"] != null) && (row["payment_status"].ToString() != ""))
                {
                    agent.payment_status = int.Parse(row["payment_status"].ToString());
                }
                if ((row["is_cancel"] != null) && (row["is_cancel"].ToString() != ""))
                {
                    if ((row["is_cancel"].ToString() == "1") || (row["is_cancel"].ToString().ToLower() == "true"))
                    {
                        agent.is_cancel = true;
                    }
                    else
                    {
                        agent.is_cancel = false;
                    }
                }
                if (row["ext1"] != null)
                {
                    agent.ext1 = row["ext1"].ToString();
                }
                if (row["ext2"] != null)
                {
                    agent.ext2 = row["ext2"].ToString();
                }
                if (row["ext3"] != null)
                {
                    agent.ext3 = row["ext3"].ToString();
                }
                if (row["remark"] != null)
                {
                    agent.remark = row["remark"].ToString();
                }
                if ((row["tranApi"] != null) && (row["tranApi"].ToString() != ""))
                {
                    agent.suppid = int.Parse(row["tranApi"].ToString());
                }
                if ((row["notifyTimes"] != null) && (row["notifyTimes"].ToString() != ""))
                {
                    agent.notifyTimes = int.Parse(row["notifyTimes"].ToString());
                }
                if ((row["notifystatus"] != null) && (row["notifystatus"].ToString() != ""))
                {
                    agent.notifystatus = int.Parse(row["notifystatus"].ToString());
                }
                if (row["callbackText"] != null)
                {
                    agent.callbackText = row["callbackText"].ToString();
                }
            }
            return agent;
        }

        public bool Delete(int id)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            DbHelperSQL.RunProcedure("proc_settledAgent_Delete", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }

        public bool Delete(string trade_no)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from settledAgent ");
            builder.Append(" where trade_no=@trade_no ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@trade_no", SqlDbType.VarChar, 40) };
            cmdParms[0].Value = trade_no;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from settledAgent ");
            builder.Append(" where id in (" + idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string trade_no)
        {
            int num;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@trade_no", SqlDbType.VarChar, 40) };
            parameters[0].Value = trade_no;
            return (DbHelperSQL.RunProcedure("proc_settledAgent_Exists", parameters, out num) == 1);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,mode,lotno,serial,trade_no,out_trade_no,[service],input_charset,userid,sign_type,return_url,bankCode,bankName,bankBranch,bankAccountName,bankAccount,amount,charge,totalamt,addTime,processingTime,audit_status,auditTime,auditUser,auditUserName,payment_status,is_cancel,ext1,ext2,ext3,remark,tranApi,suppstatus,notifyTimes,notifystatus,callbackText,issure,suretime,sureclientip,sureuser  ");
            builder.Append(" FROM settledAgent ");
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
            builder.Append(" id,mode,trade_no,out_trade_no,service,userid,sign_type,return_url,bankCode,bankName,bankBranch,bankAccountName,bankAccount,amount,charge,addTime,processingTime,audit_status,payment_status,is_cancel,ext1,ext2,ext3,remark,tranApi,notifyTimes,notifystatus,callbackText ");
            builder.Append(" FROM settledAgent ");
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
            builder.Append(")AS Row, T.*  from settledAgent T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public viviapi.Model.Withdraw.settledAgent GetModel(int id)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameters[0].Value = id;
            viviapi.Model.Withdraw.settledAgent agent = new viviapi.Model.Withdraw.settledAgent();
            DataSet set = DbHelperSQL.RunProcedure("proc_settledAgent_GetModel", parameters, "ds");
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public viviapi.Model.Withdraw.settledAgent GetModel(string trade_no)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@trade_no", SqlDbType.VarChar, 40) };
            parameters[0].Value = trade_no;
            viviapi.Model.Withdraw.settledAgent agent = new viviapi.Model.Withdraw.settledAgent();
            DataSet set = DbHelperSQL.RunProcedure("proc_settledAgent_GetModel2", parameters, "ds");
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM settledAgent ");
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

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, byte isstat)
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
                if (isstat == 1)
                {
                    commandText = commandText + "\r\nselect sum(amount) as amount,sum(charge) as charge,sum(amount+charge) as totalpay from v_settledAgent where " + wheres;
                }
                else if (isstat == 2)
                {
                    commandText = commandText + "\r\nselect count(0) as qty\r\n,sum(case when is_cancel=1 then 1 else 0 end) as cancel_qty\r\n,sum(case when audit_status=3 then 1 else 0 end) as qty1\r\n,sum(case when audit_status=3 then amount else 0 end) as amt1\r\n,sum(case when is_cancel=0 then 1 else 0 end) as qty2\r\n,sum(case when is_cancel=0 then amount else 0 end) as amt2\r\n,sum(case when audit_status=2 and payment_status=2 then 1 else 0 end) as qty3\r\n,sum(case when audit_status=2 and payment_status=2 then amount else 0 end) as amt3\r\n,sum(case when audit_status=2 and payment_status=3 then 1 else 0 end) as qty4\r\n,sum(case when audit_status=2 and payment_status=3 then amount else 0 end) as amt4 from v_settledAgent where " + wheres;
                }
                return DataBase.ExecuteDataset(CommandType.Text, commandText, paramList.ToArray());
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return set;
            }
        }

        public bool Update(viviapi.Model.Withdraw.settledAgent model)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@mode", SqlDbType.TinyInt, 1), new SqlParameter("@trade_no", SqlDbType.VarChar, 40), new SqlParameter("@out_trade_no", SqlDbType.VarChar, 0x40), new SqlParameter("@service", SqlDbType.VarChar, 40), new SqlParameter("@input_charset", SqlDbType.VarChar, 20), new SqlParameter("@userid", SqlDbType.Int, 4), new SqlParameter("@sign_type", SqlDbType.VarChar, 8), new SqlParameter("@return_url", SqlDbType.VarChar, 0x100), new SqlParameter("@bankCode", SqlDbType.VarChar, 10), new SqlParameter("@bankName", SqlDbType.NVarChar, 20), new SqlParameter("@bankBranch", SqlDbType.NVarChar, 0xff), new SqlParameter("@bankAccountName", SqlDbType.NVarChar, 20), new SqlParameter("@bankAccount", SqlDbType.VarChar, 50), new SqlParameter("@amount", SqlDbType.Decimal, 9), new SqlParameter("@charge", SqlDbType.Decimal, 9),
                new SqlParameter("@addTime", SqlDbType.DateTime), new SqlParameter("@processingTime", SqlDbType.DateTime), new SqlParameter("@audit_status", SqlDbType.TinyInt, 1), new SqlParameter("@auditTime", SqlDbType.DateTime), new SqlParameter("@auditUser", SqlDbType.Int, 4), new SqlParameter("@auditUserName", SqlDbType.VarChar, 50), new SqlParameter("@payment_status", SqlDbType.TinyInt, 1), new SqlParameter("@is_cancel", SqlDbType.Bit, 1), new SqlParameter("@ext1", SqlDbType.VarChar, 50), new SqlParameter("@ext2", SqlDbType.VarChar, 50), new SqlParameter("@ext3", SqlDbType.VarChar, 50), new SqlParameter("@remark", SqlDbType.NVarChar, 500), new SqlParameter("@tranApi", SqlDbType.Int, 4), new SqlParameter("@notifyTimes", SqlDbType.Int, 4), new SqlParameter("@notifystatus", SqlDbType.TinyInt, 1), new SqlParameter("@callbackText", SqlDbType.NVarChar, 50)
             };
            parameters[0].Value = model.id;
            parameters[1].Value = model.mode;
            parameters[2].Value = model.trade_no;
            parameters[3].Value = model.out_trade_no;
            parameters[4].Value = model.service;
            parameters[5].Value = model.input_charset;
            parameters[6].Value = model.userid;
            parameters[7].Value = model.sign_type;
            parameters[8].Value = model.return_url;
            parameters[9].Value = model.bankCode;
            parameters[10].Value = model.bankName;
            parameters[11].Value = model.bankBranch;
            parameters[12].Value = model.bankAccountName;
            parameters[13].Value = model.bankAccount;
            parameters[14].Value = model.amount;
            parameters[15].Value = model.charge;
            parameters[0x10].Value = model.addTime;
            parameters[0x11].Value = model.processingTime;
            parameters[0x12].Value = model.audit_status;
            parameters[0x13].Value = model.auditTime;
            parameters[20].Value = model.auditUser;
            parameters[0x15].Value = model.auditUserName;
            parameters[0x16].Value = model.payment_status;
            parameters[0x17].Value = model.is_cancel;
            parameters[0x18].Value = model.ext1;
            parameters[0x19].Value = model.ext2;
            parameters[0x1a].Value = model.ext3;
            parameters[0x1b].Value = model.remark;
            parameters[0x1c].Value = model.suppid;
            parameters[0x1d].Value = model.notifyTimes;
            parameters[30].Value = model.notifystatus;
            parameters[0x1f].Value = model.callbackText;
            DbHelperSQL.RunProcedure("proc_settledAgent_Update", parameters, out rowsAffected);
            return (rowsAffected > 0);
        }
    }
}

