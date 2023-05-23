namespace viviapi.BLL.Payment
{
    using DBAccess;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using viviapi.Model.Payment;

    public class PayPriceConverFactory
    {
        public int Add(PayPriceConver model)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Pri_Type", SqlDbType.Int, 4), new SqlParameter("@Value", SqlDbType.Money, 8), new SqlParameter("@Conv_PriType", SqlDbType.Int, 4), new SqlParameter("@Created", SqlDbType.DateTime), new SqlParameter("@IsOpen", SqlDbType.Bit) };
            prams[0].Direction = ParameterDirection.Output;
            prams[1].Value = model.Pri_Type;
            prams[2].Value = model.Value;
            prams[3].Value = model.Conv_PriType;
            prams[4].Value = model.Created;
            prams[5].Value = model.IsOpen;
            if (DataBase.RunProc("PayPriceConver_ADD", prams) > 0)
            {
                return (int)prams[0].Value;
            }
            return 0;
        }

        public bool Delete(int ID)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            prams[0].Value = ID;
            return (DataBase.RunProc("PayPriceConver_Delete", prams) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from PayPriceConver ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString()) > 0);
        }

        public DataSet GetInitList(string PayType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.Value,b.ID,Pri_Type,Conv_PriType,Created,IsOpen from ");
            builder.Append(" (select 30 Value ");
            builder.Append(" union all select 50 ");
            builder.Append(" union all select 100 ");
            builder.Append(" union all select 300");
            builder.Append(" union all select 500) a left join PayPriceConver b");
            builder.AppendFormat(" on a.Value = b.Value and Pri_Type={0} order by a.Value asc", PayType);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Pri_Type,Value,Conv_PriType,Created,IsOpen ");
            builder.Append(" FROM PayPriceConver ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ID,Pri_Type,Value,Conv_PriType,Created,IsOpen ");
            builder.Append(" FROM PayPriceConver ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public PayPriceConver GetModel(int ID)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            prams[0].Value = ID;
            PayPriceConver conver = new PayPriceConver();
            DataSet ds = null;
            DataBase.RunProc("PayPriceConver_GetModel", prams, out ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    conver.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pri_Type"].ToString() != "")
                {
                    conver.Pri_Type = int.Parse(ds.Tables[0].Rows[0]["Pri_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Value"].ToString() != "")
                {
                    conver.Value = decimal.Parse(ds.Tables[0].Rows[0]["Value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Conv_PriType"].ToString() != "")
                {
                    conver.Conv_PriType = int.Parse(ds.Tables[0].Rows[0]["Conv_PriType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Created"].ToString() != "")
                {
                    conver.Created = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString()));
                }
                if (ds.Tables[0].Rows[0]["IsOpen"].ToString() != "")
                {
                    conver.IsOpen = bool.Parse(ds.Tables[0].Rows[0]["IsOpen"].ToString());
                }
                return conver;
            }
            return null;
        }

        public PayPriceConver GetModel(int pay, decimal value)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@value", SqlDbType.Money, 8), new SqlParameter("@pay", SqlDbType.Int, 4) };
            prams[0].Value = value;
            prams[1].Value = pay;
            PayPriceConver conver = new PayPriceConver();
            DataSet ds = null;
            DataBase.RunProc("PayPriceConver_GetModelByPayType", prams, out ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    conver.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pri_Type"].ToString() != "")
                {
                    conver.Pri_Type = int.Parse(ds.Tables[0].Rows[0]["Pri_Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Value"].ToString() != "")
                {
                    conver.Value = decimal.Parse(ds.Tables[0].Rows[0]["Value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Conv_PriType"].ToString() != "")
                {
                    conver.Conv_PriType = int.Parse(ds.Tables[0].Rows[0]["Conv_PriType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Created"].ToString() != "")
                {
                    conver.Created = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString()));
                }
                if (ds.Tables[0].Rows[0]["IsOpen"].ToString() != "")
                {
                    conver.IsOpen = bool.Parse(ds.Tables[0].Rows[0]["IsOpen"].ToString());
                }
                return conver;
            }
            return null;
        }

        public bool Update(PayPriceConver model)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Pri_Type", SqlDbType.Int, 4), new SqlParameter("@Value", SqlDbType.Money, 8), new SqlParameter("@Conv_PriType", SqlDbType.Int, 4), new SqlParameter("@Created", SqlDbType.DateTime), new SqlParameter("@IsOpen", SqlDbType.Bit) };
            prams[0].Value = model.ID;
            prams[1].Value = model.Pri_Type;
            prams[2].Value = model.Value;
            prams[3].Value = model.Conv_PriType;
            prams[4].Value = model.Created;
            prams[5].Value = model.IsOpen;
            return (DataBase.RunProc("PayPriceConver_Update", prams) > 0);
        }
    }
}

