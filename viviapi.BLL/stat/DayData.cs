using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace viviapi.BLL
{
    public class DayData
    {
        public static DataTable GetDayData(DateTime beginTime, DateTime endTime, int uid)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (uid > 0)
                list.Add(DataBase.MakeInParam("@uid", SqlDbType.Int, 4, (object)uid));
            list.Add(DataBase.MakeInParam("@beginTime", SqlDbType.DateTime, 8, (object)beginTime));
            list.Add(DataBase.MakeInParam("@endTime", SqlDbType.DateTime, 8, (object)endTime));
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "Proc_SelectDayData", list.ToArray()).Tables[0];
        }

        public static DataTable GetDayHHReginfo(DateTime begintime, DateTime endtime, int adid)
        {
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "Proc_SelectDayDataHH", DataBase.MakeInParam("@AddTime", SqlDbType.DateTime, 8, (object)begintime), DataBase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, (object)endtime), DataBase.MakeInParam("@UserId", SqlDbType.Int, 4, (object)adid)).Tables[0];
        }

        public static DataTable GetGMDayData(DateTime beginTime, DateTime endTime, int uid)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (uid > 0)
                list.Add(DataBase.MakeInParam("@uid", SqlDbType.Int, 4, (object)uid));
            list.Add(DataBase.MakeInParam("@beginTime", SqlDbType.DateTime, 8, (object)beginTime));
            list.Add(DataBase.MakeInParam("@endTime", SqlDbType.DateTime, 8, (object)endTime));
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "GM_SelectDayData", list.ToArray()).Tables[0];
        }
    }
}
