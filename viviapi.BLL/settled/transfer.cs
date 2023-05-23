using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Settled
{
    public class transfer
    {
        internal string SQL_TABLE = "v_transfer";
        internal string SQL_TABLE_FIELD = "[id]\r\n      ,[year]\r\n      ,[month]\r\n      ,[userid]\r\n      ,[touserid]\r\n      ,[amt]\r\n      ,[charge]\r\n      ,[remark]\r\n      ,[status]\r\n      ,[addtime]\r\n      ,[updatetime]\r\n      ,[username]\r\n      ,[full_name]\r\n      ,[username1]\r\n      ,[full_name1]";

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "transfer");
        }

        public bool Exists(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) from transfer");
            stringBuilder.Append(" where id=@id ");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            return DbHelperSQL.Exists(stringBuilder.ToString(), sqlParameterArray);
        }

        public int Add(viviapi.Model.Settled.transfer model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[11]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@year", SqlDbType.Int, 4),
          new SqlParameter("@month", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@touserid", SqlDbType.Int, 4),
          new SqlParameter("@amt", SqlDbType.Decimal, 9),
          new SqlParameter("@charge", SqlDbType.Decimal, 9),
          new SqlParameter("@remark", SqlDbType.VarChar, 200),
          new SqlParameter("@status", SqlDbType.Int, 4),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@updatetime", SqlDbType.DateTime)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.year;
                sqlParameterArray[2].Value = (object)model.month;
                sqlParameterArray[3].Value = (object)model.userid;
                sqlParameterArray[4].Value = (object)model.touserid;
                sqlParameterArray[5].Value = (object)model.amt;
                sqlParameterArray[6].Value = (object)model.charge;
                sqlParameterArray[7].Value = (object)model.remark;
                sqlParameterArray[8].Value = (object)model.status;
                sqlParameterArray[9].Value = (object)model.addtime;
                sqlParameterArray[10].Value = (object)model.updatetime;
                int rowsAffected;
                DbHelperSQL.RunProcedure("proc_transfer_add", (IDataParameter[])sqlParameterArray, out rowsAffected);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(viviapi.Model.Settled.transfer model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update transfer set ");
            stringBuilder.Append("year=@year,");
            stringBuilder.Append("month=@month,");
            stringBuilder.Append("userid=@userid,");
            stringBuilder.Append("touserid=@touserid,");
            stringBuilder.Append("amt=@amt,");
            stringBuilder.Append("charge=@charge,");
            stringBuilder.Append("remark=@remark,");
            stringBuilder.Append("status=@status,");
            stringBuilder.Append("addtime=@addtime,");
            stringBuilder.Append("updatetime=@updatetime");
            stringBuilder.Append(" where id=@id ");
            SqlParameter[] sqlParameterArray = new SqlParameter[11]
            {
        new SqlParameter("@year", SqlDbType.Int, 4),
        new SqlParameter("@month", SqlDbType.Int, 4),
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@touserid", SqlDbType.Int, 4),
        new SqlParameter("@amt", SqlDbType.Decimal, 9),
        new SqlParameter("@charge", SqlDbType.Decimal, 9),
        new SqlParameter("@remark", SqlDbType.VarChar, 200),
        new SqlParameter("@status", SqlDbType.Int, 4),
        new SqlParameter("@addtime", SqlDbType.DateTime),
        new SqlParameter("@updatetime", SqlDbType.DateTime),
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)model.year;
            sqlParameterArray[1].Value = (object)model.month;
            sqlParameterArray[2].Value = (object)model.userid;
            sqlParameterArray[3].Value = (object)model.touserid;
            sqlParameterArray[4].Value = (object)model.amt;
            sqlParameterArray[5].Value = (object)model.charge;
            sqlParameterArray[6].Value = (object)model.remark;
            sqlParameterArray[7].Value = (object)model.status;
            sqlParameterArray[8].Value = (object)model.addtime;
            sqlParameterArray[9].Value = (object)model.updatetime;
            sqlParameterArray[10].Value = (object)model.id;
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString(), sqlParameterArray) > 0;
        }

        public bool Delete(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from transfer ");
            stringBuilder.Append(" where id=@id ");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString(), sqlParameterArray) > 0;
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from transfer ");
            stringBuilder.Append(" where id in (" + idlist + ")  ");
            return DbHelperSQL.ExecuteSql(stringBuilder.ToString()) > 0;
        }

        public viviapi.Model.Settled.transfer GetModel(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  top 1 id,year,month,userid,touserid,amt,charge,remark,status,addtime,updatetime from transfer ");
            stringBuilder.Append(" where id=@id ");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            viviapi.Model.Settled.transfer transfer = new viviapi.Model.Settled.transfer();
            DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count > 0)
                return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
            return (viviapi.Model.Settled.transfer)null;
        }

        public viviapi.Model.Settled.transfer DataRowToModel(DataRow row)
        {
            viviapi.Model.Settled.transfer transfer = new viviapi.Model.Settled.transfer();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                    transfer.id = int.Parse(row["id"].ToString());
                if (row["year"] != null && row["year"].ToString() != "")
                    transfer.year = new int?(int.Parse(row["year"].ToString()));
                if (row["month"] != null && row["month"].ToString() != "")
                    transfer.month = new int?(int.Parse(row["month"].ToString()));
                if (row["userid"] != null && row["userid"].ToString() != "")
                    transfer.userid = int.Parse(row["userid"].ToString());
                if (row["touserid"] != null && row["touserid"].ToString() != "")
                    transfer.touserid = int.Parse(row["touserid"].ToString());
                if (row["amt"] != null && row["amt"].ToString() != "")
                    transfer.amt = Decimal.Parse(row["amt"].ToString());
                if (row["charge"] != null && row["charge"].ToString() != "")
                    transfer.charge = Decimal.Parse(row["charge"].ToString());
                if (row["remark"] != null)
                    transfer.remark = row["remark"].ToString();
                if (row["status"] != null && row["status"].ToString() != "")
                    transfer.status = int.Parse(row["status"].ToString());
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                    transfer.addtime = DateTime.Parse(row["addtime"].ToString());
                if (row["updatetime"] != null && row["updatetime"].ToString() != "")
                    transfer.updatetime = new DateTime?(DateTime.Parse(row["updatetime"].ToString()));
            }
            return transfer;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select id,year,month,userid,touserid,amt,charge,remark,status,addtime,updatetime ");
            stringBuilder.Append(" FROM transfer ");
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
            stringBuilder.Append(" id,year,month,userid,touserid,amt,charge,remark,status,addtime,updatetime ");
            stringBuilder.Append(" FROM transfer ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            stringBuilder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(stringBuilder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) FROM transfer ");
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
            stringBuilder.Append(")AS Row, T.*  from transfer T ");
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
                        case "touserid":
                            stringBuilder.Append(" AND [touserid] = @touserid");
                            SqlParameter sqlParameter2 = new SqlParameter("@touserid", SqlDbType.Int);
                            sqlParameter2.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter2);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [addtime] >= @stime");
                            SqlParameter sqlParameter3 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter3.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [addtime] <= @etime");
                            SqlParameter sqlParameter4 = new SqlParameter("@etime", SqlDbType.DateTime);
                            sqlParameter4.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter4);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
