using DBAccess;
using OKXR.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace viviapi.BLL
{
    public class ResultVFactory
    {
        public static void Add(ResultV resultv)
        {
            if (resultv == null)
                throw new ArgumentNullException("resultv");
            Comm.Insert(DataBinding.BuildInsertCommandText("ResultV", "ServerID", "Username", "MoneyV", "AddTimer"), (object)resultv);
        }

        public static void DeleteResultV(string Id)
        {
            Comm.Delete("ResultV", "ID=@Id", new SqlParameter("@Id", (object)Id));
        }

        public static List<ResultV> List(string table, string filed, string condition, string fldname, int asc, int pageindex, int pageMax)
        {
            return Comm.Select<ResultV>(table, filed, condition, fldname, asc, pageindex, pageMax);
        }

        public static ResultV SelectResultV(string Id)
        {
            return Comm.SelectOne<ResultV>("ResultV", "ID=@Id", new SqlParameter("@Id", (object)Id));
        }

        public static void UpdateResultV(ResultV resultv)
        {
            if (resultv == null)
                throw new ArgumentNullException("resultv");
            Comm.Update(DataBinding.BuildUpdateCommandText("ResultV", "ID=@ID", "ServerID", "Username", "MoneyV", "AddTimer"), (object)resultv);
        }
    }
}
