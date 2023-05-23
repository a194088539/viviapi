using DBAccess;
using System;
using System.Data;

namespace viviapi.BLL
{
    public class PayLogFactory
    {
        public static int Add(int uid, Decimal prices, Decimal money)
        {
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "PayLog_ADD", DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)uid), DataBase.MakeInParam("@AdsType", SqlDbType.TinyInt, 1, (object)2), DataBase.MakeInParam("@AdsPrices", SqlDbType.Decimal, 8, (object)prices), DataBase.MakeInParam("@PayMoney", SqlDbType.Decimal, 8, (object)money), DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)DateTime.Now)) != 1 ? 0 : 1;
        }
    }
}
