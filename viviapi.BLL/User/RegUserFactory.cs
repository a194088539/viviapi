using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model;
using viviapi.Model.User;

namespace viviapi.BLL
{
    public class RegUserFactory : BaseFactory
    {
        public DataTable GetUserList(RegUserInfo reguserinfo, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (reguserinfo.Cid > 0)
                list.Add(DataBase.MakeInParam("@Cid", SqlDbType.Int, 4, (object)reguserinfo.Cid));
            if (reguserinfo.Uid > 0)
                list.Add(DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)reguserinfo.Uid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)this.Page));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)this.PageSize));
            SqlParameter sqlParameter = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            list.Add(sqlParameter);
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRegUserList", list.ToArray()).Tables[0];
            this.Total = (int)sqlParameter.Value;
            return dataTable;
        }

        public DataTable GetUserList(RegUserInfo reguserinfo, AdsTypeEnum adstype, DateTime stime, DateTime etime)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (reguserinfo.Cid > 0)
                list.Add(DataBase.MakeInParam("@Cid", SqlDbType.Int, 4, (object)reguserinfo.Cid));
            if (reguserinfo.Uid > 0)
                list.Add(DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, (object)reguserinfo.Uid));
            list.Add(DataBase.MakeInParam("@adstype", SqlDbType.TinyInt, 1, (object)adstype));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)this.Page));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)this.PageSize));
            SqlParameter sqlParameter = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            list.Add(sqlParameter);
            DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRegUserList", list.ToArray()).Tables[0];
            this.Total = (int)sqlParameter.Value;
            return dataTable;
        }
    }
}
