namespace viviapi.BLL.Payment
{
    using DBAccess;
    using viviapi.Model.Payment;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrderConvernFactory
    {
        public static int Add(OrderConvern model)
        {
            try
            {
                SqlParameter[] prams = new SqlParameter[] { 
                    new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@OkxrOrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrigOutOrderId", SqlDbType.VarChar, 500), new SqlParameter("@OrigPayType", SqlDbType.Int, 4), new SqlParameter("@OrigPayPrice", SqlDbType.Money, 8), new SqlParameter("@OrigPromoney", SqlDbType.Money, 8), new SqlParameter("@OrigAgmoney", SqlDbType.Money, 8), new SqlParameter("@OrigProfit", SqlDbType.Money, 8), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@Created", SqlDbType.DateTime), new SqlParameter("@ConvtPayType", SqlDbType.Int, 4), new SqlParameter("@ConvtOutOrderId", SqlDbType.VarChar, 500), new SqlParameter("@ConvtPayPrice", SqlDbType.Money, 4), new SqlParameter("@ConvtAgmoney", SqlDbType.Money, 8), new SqlParameter("@ConvtPromoney", SqlDbType.Money, 8), new SqlParameter("@ConvtProfit", SqlDbType.Money, 8), 
                    new SqlParameter("@DiffProfit", SqlDbType.Money, 8)
                 };
                prams[0].Direction = ParameterDirection.Output;
                prams[1].Value = model.OkxrOrderId;
                prams[2].Value = model.OrigOutOrderId;
                prams[3].Value = model.OrigPayType;
                prams[4].Value = model.OrigPayPrice;
                prams[5].Value = model.OrigPromoney;
                prams[6].Value = model.OrigAgmoney;
                prams[7].Value = model.OrigProfit;
                prams[8].Value = model.Amount;
                prams[9].Value = model.Created;
                prams[10].Value = model.ConvtPayType;
                prams[11].Value = model.ConvtOutOrderId;
                prams[12].Value = model.ConvtPayPrice;
                prams[13].Value = model.ConvtAgmoney;
                prams[14].Value = model.ConvtPromoney;
                prams[15].Value = model.ConvtProfit;
                prams[0x10].Value = model.DiffProfit;
                DataBase.RunProc("OrderConvern_ADD", prams);
                return Convert.ToInt32(prams[0].Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool Delete(int ID)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            prams[0].Value = ID;
            return (DataBase.RunProc("OrderConvern_Delete", prams) > 0);
        }

        public static bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OrderConvern ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DataBase.ExecuteNonQuery(CommandType.Text, builder.ToString()) > 0);
        }

        public static bool Exists(int OkxrOrderId)
        {
            object obj2;
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@OkxrOrderId ", SqlDbType.BigInt, 8) };
            prams[0].Value = OkxrOrderId;
            DataBase.RunProc("OrderConvern_Exists", prams, out obj2);
            return (Convert.ToInt32(obj2.ToString()) == 1);
        }

        public static DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,OkxrOrderId,OrigOutOrderId,OrigPayType,OrigPayPrice,OrigPromoney,OrigAgmoney,OrigProfit,Amount,Created,ConvtPayType,ConvtOutOrderId,ConvtPayPrice,ConvtAgmoney,ConvtPromoney,ConvtProfit,DiffProfit ");
            builder.Append(" FROM OrderConvern ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ID,OkxrOrderId,OrigOutOrderId,OrigPayType,OrigPayPrice,OrigPromoney,OrigAgmoney,OrigProfit,Amount,Created,ConvtPayType,ConvtOutOrderId,ConvtPayPrice,ConvtAgmoney,ConvtPromoney,ConvtProfit,DiffProfit ");
            builder.Append(" FROM OrderConvern ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DataBase.ExecuteDataset(CommandType.Text, builder.ToString());
        }

        public static OrderConvern GetModel(ulong OkxrOrderId)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@OkxrOrderId", SqlDbType.BigInt, 8) };
            prams[0].Value = OkxrOrderId;
            OrderConvern convern = new OrderConvern();
            DataSet ds = null;
            DataBase.RunProc("OrderConvern_GetModel", prams, out ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    convern.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OkxrOrderId"].ToString() != "")
                {
                    convern.OkxrOrderId = ulong.Parse(ds.Tables[0].Rows[0]["OkxrOrderId"].ToString());
                }
                convern.OrigOutOrderId = ds.Tables[0].Rows[0]["OrigOutOrderId"].ToString();
                if (ds.Tables[0].Rows[0]["OrigPayType"].ToString() != "")
                {
                    convern.OrigPayType = int.Parse(ds.Tables[0].Rows[0]["OrigPayType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrigPayPrice"].ToString() != "")
                {
                    convern.OrigPayPrice = decimal.Parse(ds.Tables[0].Rows[0]["OrigPayPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrigPromoney"].ToString() != "")
                {
                    convern.OrigPromoney = decimal.Parse(ds.Tables[0].Rows[0]["OrigPromoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrigAgmoney"].ToString() != "")
                {
                    convern.OrigAgmoney = decimal.Parse(ds.Tables[0].Rows[0]["OrigAgmoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrigProfit"].ToString() != "")
                {
                    convern.OrigProfit = decimal.Parse(ds.Tables[0].Rows[0]["OrigProfit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    convern.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Created"].ToString() != "")
                {
                    convern.Created = DateTime.Parse(ds.Tables[0].Rows[0]["Created"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ConvtPayType"].ToString() != "")
                {
                    convern.ConvtPayType = int.Parse(ds.Tables[0].Rows[0]["ConvtPayType"].ToString());
                }
                convern.ConvtOutOrderId = ds.Tables[0].Rows[0]["ConvtOutOrderId"].ToString();
                if (ds.Tables[0].Rows[0]["ConvtPayPrice"].ToString() != "")
                {
                    convern.ConvtPayPrice = decimal.Parse(ds.Tables[0].Rows[0]["ConvtPayPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ConvtAgmoney"].ToString() != "")
                {
                    convern.ConvtAgmoney = decimal.Parse(ds.Tables[0].Rows[0]["ConvtAgmoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ConvtPromoney"].ToString() != "")
                {
                    convern.ConvtPromoney = decimal.Parse(ds.Tables[0].Rows[0]["ConvtPromoney"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ConvtProfit"].ToString() != "")
                {
                    convern.ConvtProfit = decimal.Parse(ds.Tables[0].Rows[0]["ConvtProfit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DiffProfit"].ToString() != "")
                {
                    convern.DiffProfit = new decimal?(decimal.Parse(ds.Tables[0].Rows[0]["DiffProfit"].ToString()));
                }
                return convern;
            }
            return null;
        }

        public static bool Update(OrderConvern model)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@OkxrOrderId", SqlDbType.BigInt, 8), new SqlParameter("@OrigOutOrderId", SqlDbType.VarChar, 500), new SqlParameter("@OrigPayType", SqlDbType.Int, 4), new SqlParameter("@OrigPayPrice", SqlDbType.Money, 8), new SqlParameter("@OrigPromoney", SqlDbType.Money, 8), new SqlParameter("@OrigAgmoney", SqlDbType.Money, 8), new SqlParameter("@OrigProfit", SqlDbType.Money, 8), new SqlParameter("@Amount", SqlDbType.Money, 8), new SqlParameter("@Created", SqlDbType.DateTime), new SqlParameter("@ConvtPayType", SqlDbType.Int, 4), new SqlParameter("@ConvtOutOrderId", SqlDbType.VarChar, 500), new SqlParameter("@ConvtPayPrice", SqlDbType.Money, 4), new SqlParameter("@ConvtAgmoney", SqlDbType.Money, 8), new SqlParameter("@ConvtPromoney", SqlDbType.Money, 8), new SqlParameter("@ConvtProfit", SqlDbType.Money, 8), 
                new SqlParameter("@DiffProfit", SqlDbType.Money, 8)
             };
            prams[0].Value = model.ID;
            prams[1].Value = model.OkxrOrderId;
            prams[2].Value = model.OrigOutOrderId;
            prams[3].Value = model.OrigPayType;
            prams[4].Value = model.OrigPayPrice;
            prams[5].Value = model.OrigPromoney;
            prams[6].Value = model.OrigAgmoney;
            prams[7].Value = model.OrigProfit;
            prams[8].Value = model.Amount;
            prams[9].Value = model.Created;
            prams[10].Value = model.ConvtPayType;
            prams[11].Value = model.ConvtOutOrderId;
            prams[12].Value = model.ConvtPayPrice;
            prams[13].Value = model.ConvtAgmoney;
            prams[14].Value = model.ConvtPromoney;
            prams[15].Value = model.ConvtProfit;
            prams[0x10].Value = model.DiffProfit;
            return (DataBase.RunProc("OrderConvern_Update", prams) > 0);
        }
    }
}

