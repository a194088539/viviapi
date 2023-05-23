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
    public class recharge
    {
        internal const string SQL_TABLE = "recharge";
        internal const string SQL_FIELDS = "id,userid,orderno,rechargeAmt,balance,addtime,status,paytime,transNo,remark ";

        public int Add(RechargeInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[10]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@orderno", SqlDbType.NVarChar, 30),
          new SqlParameter("@rechargeAmt", SqlDbType.Decimal, 9),
          new SqlParameter("@balance", SqlDbType.Decimal, 9),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@paytime", SqlDbType.DateTime),
          new SqlParameter("@transNo", SqlDbType.NVarChar, 50),
          new SqlParameter("@remark", SqlDbType.NVarChar, 200)
                };
                sqlParameterArray[0].Direction = ParameterDirection.Output;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.orderno;
                sqlParameterArray[3].Value = (object)model.rechargeAmt;
                sqlParameterArray[4].Value = (object)model.balance;
                sqlParameterArray[5].Value = (object)model.addtime;
                sqlParameterArray[6].Value = (object)model.status;
                sqlParameterArray[7].Value = (object)model.paytime;
                sqlParameterArray[8].Value = (object)model.transNo;
                sqlParameterArray[9].Value = (object)model.remark;
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_recharge_add", sqlParameterArray);
                return (int)sqlParameterArray[0].Value;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(RechargeInfo model)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[10]
                {
          new SqlParameter("@id", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@orderno", SqlDbType.NVarChar, 30),
          new SqlParameter("@rechargeAmt", SqlDbType.Decimal, 9),
          new SqlParameter("@balance", SqlDbType.Decimal, 9),
          new SqlParameter("@addtime", SqlDbType.DateTime),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@paytime", SqlDbType.DateTime),
          new SqlParameter("@transNo", SqlDbType.NVarChar, 50),
          new SqlParameter("@remark", SqlDbType.NVarChar, 200)
                };
                sqlParameterArray[0].Value = (object)model.id;
                sqlParameterArray[1].Value = (object)model.userid;
                sqlParameterArray[2].Value = (object)model.orderno;
                sqlParameterArray[3].Value = (object)model.rechargeAmt;
                sqlParameterArray[4].Value = (object)model.balance;
                sqlParameterArray[5].Value = (object)model.addtime;
                sqlParameterArray[6].Value = (object)model.status;
                sqlParameterArray[7].Value = (object)model.paytime;
                sqlParameterArray[8].Value = (object)model.transNo;
                sqlParameterArray[9].Value = (object)model.remark;
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_recharge_Update", sqlParameterArray) > 0;
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
                return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_recharge_Delete", sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public RechargeInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
                {
          new SqlParameter("@id", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)id;
                return recharge.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_recharge_GetModel", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (RechargeInfo)null;
            }
        }

        public static RechargeInfo GetModelFromDs(DataSet ds)
        {
            RechargeInfo rechargeInfo = new RechargeInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (RechargeInfo)null;
            if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                rechargeInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            if (ds.Tables[0].Rows[0]["userid"] != null && ds.Tables[0].Rows[0]["userid"].ToString() != "")
                rechargeInfo.userid = new int?(int.Parse(ds.Tables[0].Rows[0]["userid"].ToString()));
            if (ds.Tables[0].Rows[0]["orderno"] != null && ds.Tables[0].Rows[0]["orderno"].ToString() != "")
                rechargeInfo.orderno = ds.Tables[0].Rows[0]["orderno"].ToString();
            if (ds.Tables[0].Rows[0]["rechargeAmt"] != null && ds.Tables[0].Rows[0]["rechargeAmt"].ToString() != "")
                rechargeInfo.rechargeAmt = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["rechargeAmt"].ToString()));
            if (ds.Tables[0].Rows[0]["balance"] != null && ds.Tables[0].Rows[0]["balance"].ToString() != "")
                rechargeInfo.balance = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["balance"].ToString()));
            if (ds.Tables[0].Rows[0]["addtime"] != null && ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                rechargeInfo.addtime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString()));
            if (ds.Tables[0].Rows[0]["status"] != null && ds.Tables[0].Rows[0]["status"].ToString() != "")
                rechargeInfo.status = new int?(int.Parse(ds.Tables[0].Rows[0]["status"].ToString()));
            if (ds.Tables[0].Rows[0]["paytime"] != null && ds.Tables[0].Rows[0]["paytime"].ToString() != "")
                rechargeInfo.paytime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["paytime"].ToString()));
            if (ds.Tables[0].Rows[0]["transNo"] != null && ds.Tables[0].Rows[0]["transNo"].ToString() != "")
                rechargeInfo.transNo = ds.Tables[0].Rows[0]["transNo"].ToString();
            if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                rechargeInfo.remark = ds.Tables[0].Rows[0]["remark"].ToString();
            return rechargeInfo;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select id,userid,orderno,rechargeAmt,balance,addtime,status,paytime,transNo,remark ");
            stringBuilder.Append(" FROM recharge ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "recharge";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = recharge.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("id,userid,orderno,rechargeAmt,balance,addtime,status,paytime,transNo,remark ", tables, wheres, orderby, key, pageSize, page, false), paramList.ToArray());
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
