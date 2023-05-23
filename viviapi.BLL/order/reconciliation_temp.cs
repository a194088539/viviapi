using DBAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.order
{
    public class reconciliation_temp
    {
        public int Add(viviapi.Model.order.reconciliation_temp model)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("insert into reconciliation_temp(");
                stringBuilder.Append("serverid,orderid,count)");
                stringBuilder.Append(" values (");
                stringBuilder.Append("@serverid,@orderid,@count)");
                stringBuilder.Append(";select @@IDENTITY");
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
                {
          new SqlParameter("@serverid", SqlDbType.VarChar, 50),
          new SqlParameter("@orderid", SqlDbType.VarChar, 30),
          new SqlParameter("@count", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)model.serverid;
                sqlParameterArray[1].Value = (object)model.orderid;
                sqlParameterArray[2].Value = (object)model.count;
                object obj = DataBase.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
                if (obj == null)
                    return 0;
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(string orderid, string callback)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("update reconciliation_temp set ");
                stringBuilder.Append("count=count+1,callback=@callback");
                stringBuilder.Append(" where orderid=@orderid");
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
          new SqlParameter("@orderid", SqlDbType.VarChar, 30),
          new SqlParameter("@callback", SqlDbType.VarChar, 2000)
                };
                sqlParameterArray[0].Value = (object)orderid;
                sqlParameterArray[1].Value = (object)callback;
                return DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Delete(string orderid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from reconciliation_temp ");
            stringBuilder.Append(" where orderid=@orderid ");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30)
            };
            sqlParameterArray[0].Value = (object)orderid;
            return DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), sqlParameterArray) > 0;
        }

        public viviapi.Model.order.reconciliation_temp GetModel(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  top 1 id,serverid,orderid,count from reconciliation_temp ");
            stringBuilder.Append(" where id=@id");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@id", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)id;
            viviapi.Model.order.reconciliation_temp reconciliationTemp = new viviapi.Model.order.reconciliation_temp();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count > 0)
                return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
            return (viviapi.Model.order.reconciliation_temp)null;
        }

        public viviapi.Model.order.reconciliation_temp DataRowToModel(DataRow row)
        {
            viviapi.Model.order.reconciliation_temp reconciliationTemp = new viviapi.Model.order.reconciliation_temp();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                    reconciliationTemp.id = int.Parse(row["id"].ToString());
                if (row["serverid"] != null)
                    reconciliationTemp.serverid = row["serverid"].ToString();
                if (row["orderid"] != null)
                    reconciliationTemp.orderid = row["orderid"].ToString();
                if (row["count"] != null && row["count"].ToString() != "")
                    reconciliationTemp.count = new int?(int.Parse(row["count"].ToString()));
            }
            return reconciliationTemp;
        }

        public DataSet GetList(string strWhere)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("select id,serverid,orderid,count ");
                stringBuilder.Append(" FROM reconciliation_temp ");
                if (strWhere.Trim() != "")
                    stringBuilder.Append(" where " + strWhere);
                return DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), (SqlParameter[])null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }
    }
}
