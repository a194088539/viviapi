using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model;

namespace viviapi.BLL
{
    public class PayFactory
    {
        public static int Add(PayListInfo model)
        {
            SqlParameter sqlParameter = DataBase.MakeOutParam("@ID", SqlDbType.Int, 4);
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_PayList_ADD", sqlParameter, DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)model.Uid), DataBase.MakeInParam("@Money", SqlDbType.Money, 8, (object)model.Money), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, (object)model.Status), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)model.AddTime), DataBase.MakeInParam("@PayTime", SqlDbType.DateTime, 8, (object)model.PayTime), DataBase.MakeInParam("@Tax", SqlDbType.Money, 8, (object)model.Tax), DataBase.MakeInParam("@Charges", SqlDbType.Money, 8, (object)model.Charges)) == 1)
                return (int)sqlParameter.Value;
            return 0;
        }

        public static List<PayListInfo> GetListArray(string strWhere)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ID,Uid,Money,Status,AddTime,PayTime,Tax,Charges ");
            stringBuilder.Append(" FROM PayList ");
            if (strWhere.Trim() != "")
                stringBuilder.Append(" where " + strWhere);
            List<PayListInfo> list = new List<PayListInfo>();
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.Text, stringBuilder.ToString()))
            {
                while (dataReader.Read())
                    list.Add(PayFactory.ReaderBind(dataReader));
            }
            return list;
        }

        public static PayListInfo GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object) id)
            };
            PayListInfo payListInfo = (PayListInfo)null;
            using (SqlDataReader dataReader = DataBase.ExecuteReader(CommandType.StoredProcedure, "UP_PayList_GetModel", sqlParameterArray))
            {
                if (dataReader.Read())
                    payListInfo = PayFactory.ReaderBind(dataReader);
            }
            return payListInfo;
        }

        public static Decimal GetPayDayMoney(int uid)
        {
            return Decimal.Parse(DataBase.ExecuteScalarToStr(CommandType.Text, "SELECT ISNULL(SUM([Amount]*[Pay_Price]),0) FROM [User_Pay_Order] where Status = 2 and datediff(day,CompleteTime,getdate())=0 and UserId=" + uid.ToString()));
        }

        public static Decimal Getpayingmoney(int uid)
        {
            return Decimal.Parse(DataBase.ExecuteScalarToStr(CommandType.Text, "SELECT ISNULL(SUM([Money]),0) FROM [PayList] WHERE Status IN(0,1) AND [Uid]=" + uid.ToString()));
        }

        public static PayListInfo ReaderBind(SqlDataReader dataReader)
        {
            PayListInfo payListInfo = new PayListInfo();
            object obj1 = dataReader["ID"];
            if (obj1 != null && obj1 != DBNull.Value)
                payListInfo.ID = (int)obj1;
            object obj2 = dataReader["Uid"];
            if (obj2 != null && obj2 != DBNull.Value)
                payListInfo.Uid = (int)obj2;
            object obj3 = dataReader["Money"];
            if (obj3 != null && obj3 != DBNull.Value)
                payListInfo.Money = (Decimal)obj3;
            object obj4 = dataReader["Status"];
            if (obj4 != null && obj4 != DBNull.Value)
                payListInfo.Status = (int)obj4;
            object obj5 = dataReader["AddTime"];
            if (obj5 != null && obj5 != DBNull.Value)
                payListInfo.AddTime = (DateTime)obj5;
            object obj6 = dataReader["PayTime"];
            if (obj6 != null && obj6 != DBNull.Value)
                payListInfo.PayTime = (DateTime)obj6;
            object obj7 = dataReader["Tax"];
            if (obj7 != null && obj7 != DBNull.Value)
                payListInfo.Tax = (Decimal)obj7;
            object obj8 = dataReader["Charges"];
            if (obj8 != null && obj8 != DBNull.Value)
                payListInfo.Charges = (Decimal)obj8;
            return payListInfo;
        }

        public static bool Update(PayListInfo model)
        {
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_PayList_Update", DataBase.MakeInParam("@ID", SqlDbType.Int, 4, (object)model.ID), DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)model.Uid), DataBase.MakeInParam("@Money", SqlDbType.Money, 8, (object)model.Money), DataBase.MakeInParam("@Status", SqlDbType.Int, 4, (object)model.Status), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)model.AddTime), DataBase.MakeInParam("@PayTime", SqlDbType.DateTime, 8, (object)model.PayTime), DataBase.MakeInParam("@Tax", SqlDbType.Money, 8, (object)model.Tax), DataBase.MakeInParam("@Charges", SqlDbType.Money, 8, (object)model.Charges)) > 0;
        }
    }
}
