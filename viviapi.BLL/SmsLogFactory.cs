using DBAccess;
using OKXR.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace viviapi.BLL
{
    public class SmsLogFactory
    {
        public static void Add(Smslog model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into Smslog(");
            stringBuilder.Append("status,price,message,mobile,servicenum,linkid,siteid,spid,type,addtime)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@status,@price,@message,@mobile,@servicenum,@linkid,@siteid,@spid,@type,@addtime)");
            stringBuilder.Append(";select @@IDENTITY");
            SqlParameter[] sqlParameterArray = new SqlParameter[10]
            {
        new SqlParameter("@status", SqlDbType.VarChar, 5),
        new SqlParameter("@price", SqlDbType.Decimal, 9),
        new SqlParameter("@message", SqlDbType.VarChar, 100),
        new SqlParameter("@mobile", SqlDbType.VarChar, 12),
        new SqlParameter("@servicenum", SqlDbType.VarChar, 50),
        new SqlParameter("@linkid", SqlDbType.VarChar, 50),
        new SqlParameter("@siteid", SqlDbType.VarChar, 50),
        new SqlParameter("@spid", SqlDbType.VarChar, 50),
        new SqlParameter("@type", SqlDbType.VarChar, 2),
        new SqlParameter("@addtime", SqlDbType.DateTime)
            };
            sqlParameterArray[0].Value = (object)model.status;
            sqlParameterArray[1].Value = (object)model.price;
            sqlParameterArray[2].Value = (object)model.message;
            sqlParameterArray[3].Value = (object)model.mobile;
            sqlParameterArray[4].Value = (object)model.servicenum;
            sqlParameterArray[5].Value = (object)model.linkid;
            sqlParameterArray[6].Value = (object)model.siteid;
            sqlParameterArray[7].Value = (object)model.spid;
            sqlParameterArray[8].Value = (object)model.type;
            sqlParameterArray[9].Value = (object)model.addtime;
            DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
        }

        public static void Delete(string Id)
        {
            Comm.Delete("Smslog", "Sid=@Id", new SqlParameter("@Id", (object)Id));
        }

        public static DataTable GetList(int userid, int aid, int _sid, int pageindex, DateTime stime, DateTime etime, int status, out int total, out double money)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (userid > 0)
                list.Add(DataBase.MakeInParam("@uid", SqlDbType.Int, 4, (object)userid));
            if (_sid > 0)
                list.Add(DataBase.MakeInParam("@sid", SqlDbType.Int, 4, (object)_sid));
            if (aid > 0)
                list.Add(DataBase.MakeInParam("@aid", SqlDbType.Int, 4, (object)aid));
            list.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, (object)stime));
            list.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, (object)etime));
            list.Add(DataBase.MakeInParam("@Status", SqlDbType.TinyInt, 1, (object)status));
            list.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, (object)pageindex));
            list.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, (object)40));
            SqlParameter sqlParameter1 = DataBase.MakeOutParam("@totalmoney", SqlDbType.Decimal, 8);
            list.Add(sqlParameter1);
            SqlParameter sqlParameter2 = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            list.Add(sqlParameter2);
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "User_PaySMS_Getlist", list.ToArray());
            DataTable dataTable = (DataTable)null;
            money = 0.0;
            total = 0;
            if (dataSet.Tables.Count != 0)
            {
                dataTable = dataSet.Tables[0];
                money = double.Parse(sqlParameter1.Value.ToString());
                total = int.Parse(sqlParameter2.Value.ToString());
            }
            return dataTable;
        }

        public static Smslog GetModel(string linkid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  top 1 sid,status,price,message,mobile,servicenum,linkid,siteid,spid,type,addtime from Smslog ");
            stringBuilder.Append(" where linkid=@linkid ");
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@linkid", SqlDbType.VarChar, 50)
            };
            sqlParameterArray[0].Value = (object)linkid;
            Smslog smslog = new Smslog();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return smslog;
            if (dataSet.Tables[0].Rows[0]["sid"].ToString() != "")
                smslog.sid = int.Parse(dataSet.Tables[0].Rows[0]["sid"].ToString());
            smslog.status = dataSet.Tables[0].Rows[0]["status"].ToString();
            if (dataSet.Tables[0].Rows[0]["price"].ToString() != "")
                smslog.price = Decimal.Parse(dataSet.Tables[0].Rows[0]["price"].ToString());
            smslog.message = dataSet.Tables[0].Rows[0]["message"].ToString();
            smslog.mobile = dataSet.Tables[0].Rows[0]["mobile"].ToString();
            smslog.servicenum = dataSet.Tables[0].Rows[0]["servicenum"].ToString();
            smslog.linkid = dataSet.Tables[0].Rows[0]["linkid"].ToString();
            smslog.siteid = dataSet.Tables[0].Rows[0]["siteid"].ToString();
            smslog.spid = dataSet.Tables[0].Rows[0]["spid"].ToString();
            smslog.type = dataSet.Tables[0].Rows[0]["type"].ToString();
            if (dataSet.Tables[0].Rows[0]["addtime"].ToString() != "")
                smslog.addtime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["addtime"].ToString()));
            return smslog;
        }

        public static Smslog GetSMSId(string Id)
        {
            return Comm.SelectOne<Smslog>("Smslog", "Sid=@Id", new SqlParameter("@Id", (object)Id));
        }

        public static List<Smslog> List(string table, string filed, string condition, string fldname, int asc, int pageindex, int pageMax)
        {
            return Comm.Select<Smslog>(table, filed, condition, fldname, asc, pageindex, pageMax);
        }

        public static int SmsOrderComplete(string ServerId, Decimal Amount, string LinkId)
        {
            int num = -2;
            SqlParameter sqlParameter = DataBase.MakeOutParam("@result", SqlDbType.Int, 4);
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "User_Pay_SMS_Complete", DataBase.MakeInParam("@serverId", SqlDbType.Int, 4, (object)ServerId), DataBase.MakeInParam("@Amount", SqlDbType.Money, 8, (object)Amount), DataBase.MakeInParam("@LinkId", SqlDbType.VarChar, 20, (object)LinkId), sqlParameter);
            if (sqlParameter != null)
                num = (int)sqlParameter.Value;
            return num == 1 ? 1 : -1;
        }

        public static void Update(Smslog model)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update Smslog set ");
            stringBuilder.Append("status=@status,");
            stringBuilder.Append("price=@price,");
            stringBuilder.Append("message=@message,");
            stringBuilder.Append("mobile=@mobile,");
            stringBuilder.Append("servicenum=@servicenum,");
            stringBuilder.Append("linkid=@linkid,");
            stringBuilder.Append("siteid=@siteid,");
            stringBuilder.Append("spid=@spid,");
            stringBuilder.Append("type=@type,");
            stringBuilder.Append("addtime=@addtime");
            stringBuilder.Append(" where sid=@sid ");
            SqlParameter[] sqlParameterArray = new SqlParameter[11]
            {
        new SqlParameter("@sid", SqlDbType.Int, 4),
        new SqlParameter("@status", SqlDbType.VarChar, 5),
        new SqlParameter("@price", SqlDbType.Decimal, 9),
        new SqlParameter("@message", SqlDbType.VarChar, 100),
        new SqlParameter("@mobile", SqlDbType.VarChar, 12),
        new SqlParameter("@servicenum", SqlDbType.VarChar, 50),
        new SqlParameter("@linkid", SqlDbType.VarChar, 50),
        new SqlParameter("@siteid", SqlDbType.VarChar, 50),
        new SqlParameter("@spid", SqlDbType.VarChar, 50),
        new SqlParameter("@type", SqlDbType.VarChar, 2),
        new SqlParameter("@addtime", SqlDbType.DateTime)
            };
            sqlParameterArray[0].Value = (object)model.sid;
            sqlParameterArray[1].Value = (object)model.status;
            sqlParameterArray[2].Value = (object)model.price;
            sqlParameterArray[3].Value = (object)model.message;
            sqlParameterArray[4].Value = (object)model.mobile;
            sqlParameterArray[5].Value = (object)model.servicenum;
            sqlParameterArray[6].Value = (object)model.linkid;
            sqlParameterArray[7].Value = (object)model.siteid;
            sqlParameterArray[8].Value = (object)model.spid;
            sqlParameterArray[9].Value = (object)model.type;
            sqlParameterArray[10].Value = (object)model.addtime;
            DataBase.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), sqlParameterArray);
        }
    }
}
