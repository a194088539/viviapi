using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.APP;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.APP
{
    public class entrust
    {
        internal const string SQL_TABLE = "entrust";
        internal const string SQL_FIELDS = "id,userid,status,bankcardnum,bankname,payee,amount,rate,remittancefee,totalAmt,addtime,cdate,cadmin,remark ";

        public int Add(EntrustInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[14]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@bankcardnum", SqlDbType.VarChar, 30),
          new SqlParameter("@bankname", SqlDbType.NVarChar, 150),
          new SqlParameter("@payee", SqlDbType.NVarChar, 10),
          new SqlParameter("@amount", SqlDbType.Decimal, 9),
          new SqlParameter("@rate", SqlDbType.Decimal, 9),
          new SqlParameter("@remittancefee", SqlDbType.Decimal, 9),
          new SqlParameter("@totalAmt", SqlDbType.Decimal, 9),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@cdate", SqlDbType.DateTime),
          new SqlParameter("@cadmin", SqlDbType.Int, 4),
          new SqlParameter("@remark", SqlDbType.NVarChar, 200)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.status;
                sqlParameterArray[3].Value = (object)model.bankcardnum;
                sqlParameterArray[4].Value = (object)model.bankname;
                sqlParameterArray[5].Value = (object)model.payee;
                sqlParameterArray[6].Value = (object)model.amount;
                sqlParameterArray[7].Value = (object)model.rate;
                sqlParameterArray[8].Value = (object)model.remittancefee;
                sqlParameterArray[9].Value = (object)model.totalAmt;
                sqlParameterArray[10].Value = (object)model.addtime;
                sqlParameterArray[11].Value = (object)model.cdate;
                sqlParameterArray[12].Value = (object)model.cadmin;
                sqlParameterArray[13].Value = (object)model.remark;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_entrust_ADD", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(EntrustInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[14]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@bankcardnum", SqlDbType.VarChar, 30),
          new SqlParameter("@bankname", SqlDbType.NVarChar, 150),
          new SqlParameter("@payee", SqlDbType.NVarChar, 10),
          new SqlParameter("@amount", SqlDbType.Decimal, 9),
          new SqlParameter("@rate", SqlDbType.Decimal, 9),
          new SqlParameter("@remittancefee", SqlDbType.Decimal, 9),
          new SqlParameter("@totalAmt", SqlDbType.Decimal, 9),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@cdate", SqlDbType.DateTime),
          new SqlParameter("@cadmin", SqlDbType.Int, 4),
          new SqlParameter("@remark", SqlDbType.NVarChar, 200)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.status;
                sqlParameterArray[3].Value = (object)model.bankcardnum;
                sqlParameterArray[4].Value = (object)model.bankname;
                sqlParameterArray[5].Value = (object)model.payee;
                sqlParameterArray[6].Value = (object)model.amount;
                sqlParameterArray[7].Value = (object)model.rate;
                sqlParameterArray[8].Value = (object)model.remittancefee;
                sqlParameterArray[9].Value = (object)model.totalAmt;
                sqlParameterArray[10].Value = (object)model.addtime;
                sqlParameterArray[11].Value = (object)model.cdate;
                sqlParameterArray[12].Value = (object)model.cadmin;
                sqlParameterArray[13].Value = (object)model.remark;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_entrust_Update", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_entrust_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public EntrustInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return entrust.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_entrust_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (EntrustInfo)null;
            }
        }

        public static EntrustInfo GetModelFromDs(DataSet ds)
        {
            EntrustInfo entrustInfo = new EntrustInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (EntrustInfo)null;
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                entrustInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["userid"] != null && ds.Tables[0].Rows[0]["userid"].ToString() != "")
                entrustInfo.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
            if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                entrustInfo.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            if (ds.Tables[0].Rows[0]["bankcardnum"] != null && ds.Tables[0].Rows[0]["bankcardnum"].ToString() != "")
                entrustInfo.bankcardnum = ds.Tables[0].Rows[0]["bankcardnum"].ToString();
            if (ds.Tables[0].Rows[0]["bankname"] != null && ds.Tables[0].Rows[0]["bankname"].ToString() != "")
                entrustInfo.bankname = ds.Tables[0].Rows[0]["bankname"].ToString();
            if (ds.Tables[0].Rows[0]["payee"] != null && ds.Tables[0].Rows[0]["payee"].ToString() != "")
                entrustInfo.payee = ds.Tables[0].Rows[0]["payee"].ToString();
            if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                entrustInfo.amount = Decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
            if (ds.Tables[0].Rows[0]["rate"] != null && ds.Tables[0].Rows[0]["rate"].ToString() != "")
                entrustInfo.rate = Decimal.Parse(ds.Tables[0].Rows[0]["rate"].ToString());
            if (ds.Tables[0].Rows[0]["remittancefee"] != null && ds.Tables[0].Rows[0]["remittancefee"].ToString() != "")
                entrustInfo.remittancefee = Decimal.Parse(ds.Tables[0].Rows[0]["remittancefee"].ToString());
            if (ds.Tables[0].Rows[0]["totalAmt"] != null && ds.Tables[0].Rows[0]["totalAmt"].ToString() != "")
                entrustInfo.totalAmt = Decimal.Parse(ds.Tables[0].Rows[0]["totalAmt"].ToString());
            if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                entrustInfo.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
            if (ds.Tables[0].Rows[0]["cdate"] != null && ds.Tables[0].Rows[0]["cdate"].ToString() != "")
                entrustInfo.cdate = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["cdate"].ToString()));
            if (ds.Tables[0].Rows[0]["cadmin"] != null && ds.Tables[0].Rows[0]["cadmin"].ToString() != "")
                entrustInfo.cadmin = new int?(int.Parse(ds.Tables[0].Rows[0]["cadmin"].ToString()));
            if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                entrustInfo.remark = ds.Tables[0].Rows[0]["remark"].ToString();
            return entrustInfo;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select id,userid,status,bankcardnum,bankname,payee,amount,rate,remittancefee,totalAmt,addtime,cdate,cadmin,remark ");
            stringBuilder.Append(" FROM entrust ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ");
            if (Top > 0)
                stringBuilder.Append(" top " + Top.ToString());
            stringBuilder.Append(" id,userid,status,bankcardnum,bankname,payee,amount,rate,remittancefee,totalAmt,addtime,cdate,cadmin,remark ");
            stringBuilder.Append(" FROM entrust ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            stringBuilder.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(1) FROM entrust ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            object obj = DataBase.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
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
            stringBuilder.Append(")AS Row, T.*  from entrust T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
                stringBuilder.Append(" WHERE " + strWhere);
            stringBuilder.Append(" ) TT");
            stringBuilder.AppendFormat(" WHERE TT.Row between {0} and {1}", (object)startIndex, (object)endIndex);
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "entrust";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = entrust.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("id,userid,status,bankcardnum,bankname,payee,amount,rate,remittancefee,totalAmt,addtime,cdate,cadmin,remark ", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
                        case "userid":
                            stringBuilder.Append(" AND [userid] = @userid");
                            SqlParameter sqlParameter1 = new SqlParameter("@userid", SqlDbType.Int);
                            sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter1);
                            break;
                        case "username":
                            stringBuilder.Append(" AND [userName] like @UserName");
                            SqlParameter sqlParameter2 = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                            sqlParameter2.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 20) + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "status":
                            stringBuilder.Append(" AND [status] = @status");
                            SqlParameter sqlParameter3 = new SqlParameter("@status", SqlDbType.TinyInt);
                            sqlParameter3.Value = (object)(int)searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [addtime] >= @stime");
                            SqlParameter sqlParameter4 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter4.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter4);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [addtime] <= @etime");
                            SqlParameter sqlParameter5 = new SqlParameter("@etime", SqlDbType.DateTime);
                            sqlParameter5.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter5);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
