using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Financial
{
    public class tenpay_batch_trans_detail
    {
        internal string SQL_TABLE = "tenpay_batch_trans_detail";
        internal string SQL_TABLE_FIELD = "[id]\r\n      ,[package_id]\r\n      ,[serial]\r\n      ,[settleid]\r\n      ,[hid]\r\n      ,[userid]\r\n      ,[balance]\r\n      ,[status]\r\n      ,[rec_acc]\r\n      ,[rec_name]\r\n      ,[cur_type]\r\n      ,[pay_amt]\r\n      ,[succ_amt]\r\n      ,[remark]\r\n      ,[trans_id]\r\n      ,[message]\r\n      ,[completetime]";

        public string GetStatusText(object _status)
        {
            if (_status == null || _status == DBNull.Value)
                return string.Empty;
            int num = Convert.ToInt32(_status);
            switch (num)
            {
                case 1:
                    return "处理中";
                case 2:
                    return "已成功";
                case 4:
                    return "失败";
                case 8:
                    return "未确定状态";
                default:
                    return num.ToString();
            }
        }

        public int Complete(string package_id, int serial, int status, Decimal succ_amt, string message, string trans_id)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[7]
                {
          new SqlParameter("@package_id", SqlDbType.VarChar, 20),
          new SqlParameter("@serial", SqlDbType.Int, 4),
          new SqlParameter("@status", SqlDbType.TinyInt, 1),
          new SqlParameter("@succ_amt", SqlDbType.Decimal, 9),
          new SqlParameter("@trans_id", SqlDbType.VarChar, 50),
          new SqlParameter("@message", SqlDbType.VarChar, 50),
          new SqlParameter("@completetime", SqlDbType.DateTime, 8)
                };
                sqlParameterArray[0].Value = (object)package_id;
                sqlParameterArray[1].Value = (object)serial;
                sqlParameterArray[2].Value = (object)status;
                sqlParameterArray[3].Value = (object)succ_amt;
                sqlParameterArray[4].Value = (object)trans_id;
                sqlParameterArray[5].Value = (object)message;
                sqlParameterArray[6].Value = (object)DateTime.Now;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_tenpay_batch_trans_detail_complete", sqlParameterArray);
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
                        case "rec_acc":
                            stringBuilder.Append(" AND [rec_acc] like @rec_acc");
                            SqlParameter sqlParameter2 = new SqlParameter("@rec_acc", SqlDbType.VarChar, 20);
                            sqlParameter2.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 20) + "%");
                            paramList.Add(sqlParameter2);
                            break;
                        case "stime":
                            stringBuilder.Append(" AND [completetime] >= @stime");
                            SqlParameter sqlParameter3 = new SqlParameter("@stime", SqlDbType.DateTime);
                            sqlParameter3.Value = searchParam.ParamValue;
                            paramList.Add(sqlParameter3);
                            break;
                        case "etime":
                            stringBuilder.Append(" AND [completetime] <= @etime");
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
