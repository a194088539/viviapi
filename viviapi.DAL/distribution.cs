using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL
{
    public class distribution
    {
        internal string SQL_TABLE = "distribution";
        internal string SQL_TABLE_FIELD = "[id]\r\n      ,[suppid]\r\n      ,[mode]\r\n      ,[settledId]\r\n      ,[trade_no]\r\n      ,[batchNo]\r\n      ,[supp_trade_no]\r\n      ,[userid]\r\n      ,[balance]\r\n      ,[bankCode]\r\n      ,[bankName]\r\n      ,[bankBranch]\r\n      ,[bankAccountName]\r\n      ,[bankAccount]\r\n      ,[amount]\r\n      ,[charges]\r\n      ,[balance2]\r\n      ,[addTime]\r\n      ,[processingTime]\r\n      ,[supp_message]\r\n      ,[status]\r\n      ,[ext1]\r\n      ,[ext2]\r\n      ,[ext3]\r\n      ,[remark]\r\n      ,isnull(amount,0)+isnull(charges,0) as realpay";

        public bool Exists(string trade_no)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@trade_no", SqlDbType.VarChar, 30)
      };
            sqlParameterArray[0].Value = (object)trade_no;
            int rowsAffected;
            return DbHelperSQL.RunProcedure("proc_distribution_Exists", (IDataParameter[])sqlParameterArray, out rowsAffected) == 1;
        }

        public int Add(viviapi.Model.distribution model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[25]
      {
        new SqlParameter("@id", SqlDbType.Int, 4),
        new SqlParameter("@suppid", SqlDbType.Int, 4),
        new SqlParameter("@mode", SqlDbType.TinyInt, 1),
        new SqlParameter("@settledId", SqlDbType.Int, 4),
        new SqlParameter("@trade_no", SqlDbType.VarChar, 30),
        new SqlParameter("@batchNo", SqlDbType.Int, 4),
        new SqlParameter("@supp_trade_no", SqlDbType.VarChar, 50),
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@balance", SqlDbType.Decimal, 9),
        new SqlParameter("@bankCode", SqlDbType.VarChar, 50),
        new SqlParameter("@bankName", SqlDbType.NVarChar, 20),
        new SqlParameter("@bankBranch", SqlDbType.NVarChar, (int) byte.MaxValue),
        new SqlParameter("@bankAccountName", SqlDbType.NVarChar, 20),
        new SqlParameter("@bankAccount", SqlDbType.VarChar, 50),
        new SqlParameter("@amount", SqlDbType.Decimal, 9),
        new SqlParameter("@charges", SqlDbType.Decimal, 9),
        new SqlParameter("@balance2", SqlDbType.Decimal, 9),
        new SqlParameter("@addTime", SqlDbType.DateTime),
        new SqlParameter("@processingTime", SqlDbType.DateTime),
        new SqlParameter("@supp_message", SqlDbType.NVarChar, 200),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@ext1", SqlDbType.VarChar, 50),
        new SqlParameter("@ext2", SqlDbType.VarChar, 50),
        new SqlParameter("@ext3", SqlDbType.VarChar, 50),
        new SqlParameter("@remark", SqlDbType.NVarChar, 500)
      };
            sqlParameterArray[0].Direction = ParameterDirection.Output;
            sqlParameterArray[1].Value = (object)model.suppid;
            sqlParameterArray[2].Value = (object)model.mode;
            sqlParameterArray[3].Value = (object)model.settledId;
            sqlParameterArray[4].Value = (object)model.trade_no;
            sqlParameterArray[5].Value = (object)model.batchNo;
            sqlParameterArray[6].Value = (object)model.supp_trade_no;
            sqlParameterArray[7].Value = (object)model.userid;
            sqlParameterArray[8].Value = (object)model.balance;
            sqlParameterArray[9].Value = (object)model.bankCode;
            sqlParameterArray[10].Value = (object)model.bankName;
            sqlParameterArray[11].Value = (object)model.bankBranch;
            sqlParameterArray[12].Value = (object)model.bankAccountName;
            sqlParameterArray[13].Value = (object)model.bankAccount;
            sqlParameterArray[14].Value = (object)model.amount;
            sqlParameterArray[15].Value = (object)model.charges;
            sqlParameterArray[16].Value = (object)model.balance2;
            sqlParameterArray[17].Value = (object)model.addTime;
            sqlParameterArray[18].Value = (object)model.processingTime;
            sqlParameterArray[19].Value = (object)model.supp_message;
            sqlParameterArray[20].Value = (object)model.status;
            sqlParameterArray[21].Value = (object)model.ext1;
            sqlParameterArray[22].Value = (object)model.ext2;
            sqlParameterArray[23].Value = (object)model.ext3;
            sqlParameterArray[24].Value = (object)model.remark;
            int rowsAffected;
            DbHelperSQL.RunProcedure("proc_distribution_ADD", (IDataParameter[])sqlParameterArray, out rowsAffected);
            return (int)sqlParameterArray[0].Value;
        }

        public bool Update(viviapi.Model.distribution model)
        {
            int rowsAffected = 0;
            SqlParameter[] sqlParameterArray = new SqlParameter[25]
      {
        new SqlParameter("@id", SqlDbType.Int, 4),
        new SqlParameter("@suppid", SqlDbType.Int, 4),
        new SqlParameter("@mode", SqlDbType.TinyInt, 1),
        new SqlParameter("@settledId", SqlDbType.Int, 4),
        new SqlParameter("@trade_no", SqlDbType.VarChar, 30),
        new SqlParameter("@batchNo", SqlDbType.Int, 4),
        new SqlParameter("@supp_trade_no", SqlDbType.VarChar, 50),
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@balance", SqlDbType.Decimal, 9),
        new SqlParameter("@bankCode", SqlDbType.VarChar, 50),
        new SqlParameter("@bankName", SqlDbType.NVarChar, 20),
        new SqlParameter("@bankBranch", SqlDbType.NVarChar, (int) byte.MaxValue),
        new SqlParameter("@bankAccountName", SqlDbType.NVarChar, 20),
        new SqlParameter("@bankAccount", SqlDbType.VarChar, 50),
        new SqlParameter("@amount", SqlDbType.Decimal, 9),
        new SqlParameter("@charges", SqlDbType.Decimal, 9),
        new SqlParameter("@balance2", SqlDbType.Decimal, 9),
        new SqlParameter("@addTime", SqlDbType.DateTime),
        new SqlParameter("@processingTime", SqlDbType.DateTime),
        new SqlParameter("@supp_message", SqlDbType.NVarChar, 200),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@ext1", SqlDbType.VarChar, 50),
        new SqlParameter("@ext2", SqlDbType.VarChar, 50),
        new SqlParameter("@ext3", SqlDbType.VarChar, 50),
        new SqlParameter("@remark", SqlDbType.NVarChar, 500)
      };
            sqlParameterArray[0].Value = (object)model.id;
            sqlParameterArray[1].Value = (object)model.suppid;
            sqlParameterArray[2].Value = (object)model.mode;
            sqlParameterArray[3].Value = (object)model.settledId;
            sqlParameterArray[4].Value = (object)model.trade_no;
            sqlParameterArray[5].Value = (object)model.batchNo;
            sqlParameterArray[6].Value = (object)model.supp_trade_no;
            sqlParameterArray[7].Value = (object)model.userid;
            sqlParameterArray[8].Value = (object)model.balance;
            sqlParameterArray[9].Value = (object)model.bankCode;
            sqlParameterArray[10].Value = (object)model.bankName;
            sqlParameterArray[11].Value = (object)model.bankBranch;
            sqlParameterArray[12].Value = (object)model.bankAccountName;
            sqlParameterArray[13].Value = (object)model.bankAccount;
            sqlParameterArray[14].Value = (object)model.amount;
            sqlParameterArray[15].Value = (object)model.charges;
            sqlParameterArray[16].Value = (object)model.balance2;
            sqlParameterArray[17].Value = (object)model.addTime;
            sqlParameterArray[18].Value = (object)model.processingTime;
            sqlParameterArray[19].Value = (object)model.supp_message;
            sqlParameterArray[20].Value = (object)model.status;
            sqlParameterArray[21].Value = (object)model.ext1;
            sqlParameterArray[22].Value = (object)model.ext2;
            sqlParameterArray[23].Value = (object)model.ext3;
            sqlParameterArray[24].Value = (object)model.remark;
            DbHelperSQL.RunProcedure("proc_distribution_Update", (IDataParameter[])sqlParameterArray, out rowsAffected);
            return rowsAffected > 0;
        }

        public int Process(int suppId, string trade_no, bool is_cancel, int status, string amount, string supp_trade_no, string message, out string bill_trade_no)
        {
            bill_trade_no = string.Empty;
            SqlParameter[] sqlParameterArray = new SqlParameter[8]
      {
        new SqlParameter("@suppid", SqlDbType.Int, 4),
        new SqlParameter("@trade_no", SqlDbType.VarChar, 30),
        new SqlParameter("@supp_trade_no", SqlDbType.VarChar, 50),
        new SqlParameter("@is_cancel", SqlDbType.Bit, 1),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@amount", SqlDbType.Decimal, 9),
        new SqlParameter("@processingTime", SqlDbType.DateTime, 8),
        new SqlParameter("@supp_message", SqlDbType.NVarChar, 200)
      };
            sqlParameterArray[0].Value = (object)suppId;
            sqlParameterArray[1].Value = (object)trade_no;
            sqlParameterArray[2].Value = (object)supp_trade_no;
            sqlParameterArray[3].Value = (object)(is_cancel ? 1 : 0);
            sqlParameterArray[4].Value = (object)status;
            sqlParameterArray[5].Value = (object)Decimal.Parse(amount);
            sqlParameterArray[6].Value = (object)DateTime.Now;
            sqlParameterArray[7].Value = (object)message;
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_distribution_process", sqlParameterArray).Tables[0];
            if (dataTable == null || dataTable.Rows.Count <= 0)
                return -1;
            DataRow dataRow = dataTable.Rows[0];
            bill_trade_no = Convert.ToString(dataRow["bill_trade_no"]);
            return Convert.ToInt32(dataRow["result"]);
        }

        public bool Delete(int id)
        {
            int rowsAffected = 0;
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@id", SqlDbType.Int, 4)
      };
            sqlParameterArray[0].Value = (object)id;
            DbHelperSQL.RunProcedure("proc_distribution_Delete", (IDataParameter[])sqlParameterArray, out rowsAffected);
            return rowsAffected > 0;
        }

        public bool Delete(string trade_no)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from distribution ");
            stringBuilder.Append(" where trade_no=@trade_no ");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@trade_no", SqlDbType.VarChar, 30)
      };
            sqlParameterArray[0].Value = (object)trade_no;
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString(), sqlParameterArray) > 0;
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from distribution ");
            stringBuilder.Append(" where id in (" + idlist + ")  ");
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString()) > 0;
        }

        public viviapi.Model.distribution GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@id", SqlDbType.Int, 4)
      };
            sqlParameterArray[0].Value = (object)id;
            viviapi.Model.distribution distribution = new viviapi.Model.distribution();
            DataSet dataSet = DbHelperSQL.RunProcedure("proc_distribution_GetModel", (IDataParameter[])sqlParameterArray, "ds");
            if (dataSet.Tables[0].Rows.Count > 0)
                return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
            return (viviapi.Model.distribution)null;
        }

        public viviapi.Model.distribution DataRowToModel(DataRow row)
        {
            viviapi.Model.distribution distribution = new viviapi.Model.distribution();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                    distribution.id = int.Parse(row["id"].ToString());
                if (row["suppid"] != null && row["suppid"].ToString() != "")
                    distribution.suppid = int.Parse(row["suppid"].ToString());
                if (row["mode"] != null && row["mode"].ToString() != "")
                    distribution.mode = new int?(int.Parse(row["mode"].ToString()));
                if (row["settledId"] != null && row["settledId"].ToString() != "")
                    distribution.settledId = new int?(int.Parse(row["settledId"].ToString()));
                if (row["trade_no"] != null)
                    distribution.trade_no = row["trade_no"].ToString();
                if (row["batchNo"] != null && row["batchNo"].ToString() != "")
                    distribution.batchNo = int.Parse(row["batchNo"].ToString());
                if (row["supp_trade_no"] != null)
                    distribution.supp_trade_no = row["supp_trade_no"].ToString();
                if (row["userid"] != null && row["userid"].ToString() != "")
                    distribution.userid = int.Parse(row["userid"].ToString());
                if (row["balance"] != null && row["balance"].ToString() != "")
                    distribution.balance = Decimal.Parse(row["balance"].ToString());
                if (row["bankCode"] != null)
                    distribution.bankCode = row["bankCode"].ToString();
                if (row["bankName"] != null)
                    distribution.bankName = row["bankName"].ToString();
                if (row["bankBranch"] != null)
                    distribution.bankBranch = row["bankBranch"].ToString();
                if (row["bankAccountName"] != null)
                    distribution.bankAccountName = row["bankAccountName"].ToString();
                if (row["bankAccount"] != null)
                    distribution.bankAccount = row["bankAccount"].ToString();
                if (row["amount"] != null && row["amount"].ToString() != "")
                    distribution.amount = Decimal.Parse(row["amount"].ToString());
                if (row["charges"] != null && row["charges"].ToString() != "")
                    distribution.charges = Decimal.Parse(row["charges"].ToString());
                if (row["balance2"] != null && row["balance2"].ToString() != "")
                    distribution.balance2 = new Decimal?(Decimal.Parse(row["balance2"].ToString()));
                if (row["addTime"] != null && row["addTime"].ToString() != "")
                    distribution.addTime = DateTime.Parse(row["addTime"].ToString());
                if (row["processingTime"] != null && row["processingTime"].ToString() != "")
                    distribution.processingTime = DateTime.Parse(row["processingTime"].ToString());
                if (row["supp_message"] != null)
                    distribution.supp_message = row["supp_message"].ToString();
                if (row["status"] != null && row["status"].ToString() != "")
                    distribution.status = int.Parse(row["status"].ToString());
                if (row["ext1"] != null)
                    distribution.ext1 = row["ext1"].ToString();
                if (row["ext2"] != null)
                    distribution.ext2 = row["ext2"].ToString();
                if (row["ext3"] != null)
                    distribution.ext3 = row["ext3"].ToString();
                if (row["remark"] != null)
                    distribution.remark = row["remark"].ToString();
            }
            return distribution;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select id,suppid,mode,settledId,trade_no,batchNo,supp_trade_no,userid,balance,bankCode,bankName,bankBranch,bankAccountName,bankAccount,amount,charges,balance2,addTime,processingTime,supp_message,status,ext1,ext2,ext3,remark ");
            stringBuilder.Append(" FROM distribution ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ");
            if (Top > 0)
                stringBuilder.Append(" top " + Top.ToString());
            stringBuilder.Append(" id,suppid,mode,settledId,trade_no,batchNo,supp_trade_no,userid,balance,bankCode,bankName,bankBranch,bankAccountName,bankAccount,amount,charges,balance2,addTime,processingTime,supp_message,status,ext1,ext2,ext3,remark ");
            stringBuilder.Append(" FROM distribution ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            stringBuilder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) FROM distribution ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            object single = DbHelperSQL.GetSingle(stringBuilder.ToString());
            if (single == null)
                return 0;
            return Convert.ToInt32(single);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT * FROM ( ");
            stringBuilder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
                stringBuilder.Append("order by T." + orderby);
            else
                stringBuilder.Append("order by T.id desc");
            stringBuilder.Append(")AS Row, T.*  from distribution T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
                stringBuilder.Append(" WHERE " + strWhere);
            stringBuilder.Append(" ) TT");
            stringBuilder.AppendFormat(" WHERE TT.Row between {0} and {1}", (object)startIndex, (object)endIndex);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = this.SQL_TABLE;
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = this.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(this.SQL_TABLE_FIELD, tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return dataSet;
            }
        }

        private string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder stringBuilder = new StringBuilder(" 1 = 1");
            if (param != null && param.Count > 0)
            {
                for (int index = 0; index < param.Count; ++index)
                {
                    SearchParam searchParam = param[index];
                    switch (searchParam.ParamKey.Trim().ToLower())
                    {
                        case "userid":
                            stringBuilder.Append(" AND [userid] = @userid");
                            SqlParameter sqlParameter1 = new SqlParameter("@userid", SqlDbType.Int);
                            sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter1);
                            break;
                        case "trade_no":
                            stringBuilder.Append(" AND [trade_no] like @trade_no");
                            SqlParameter sqlParameter2 = new SqlParameter("@trade_no", SqlDbType.VarChar, 30);
                            sqlParameter2.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "bankaccount":
                            stringBuilder.Append(" AND [bankAccount] like @bankAccount");
                            SqlParameter sqlParameter3 = new SqlParameter("@bankAccount", SqlDbType.VarChar, 30);
                            sqlParameter3.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                            paramList.Add(sqlParameter3);
                            break;
                        case "bankcode":
                            stringBuilder.Append(" AND [bankCode] = @bankCode");
                            SqlParameter sqlParameter4 = new SqlParameter("@bankCode", SqlDbType.VarChar, 20);
                            sqlParameter4.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 20) + "%");
                            paramList.Add(sqlParameter4);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [processingTime] >= @stime");
                            SqlParameter sqlParameter5 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter5.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter5);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [processingTime] <= @etime");
                            SqlParameter sqlParameter6 = new SqlParameter("@etime", SqlDbType.DateTime);
                            sqlParameter6.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter6);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
