using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.IDAL;
using viviapi.Model.Order;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.SQLServerDAL
{
    public class OrderSms : IOrderSms
    {
        internal const string SQL_TABLE = "ordersms";
        internal const string FIELDS = "[id],[status],[orderid],[userorder],[supplierId],[userid],[mobile],[fee],[message],[servicenum],[linkid],[gwid],[payRate],[supplierRate],[promRate],[payAmt],[promAmt],[supplierAmt],[profits],[server],[addtime],[completetime]";

        public bool Insert(OrderSmsInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[24]
            {
        new SqlParameter("@orderid", SqlDbType.NVarChar, 30),
        new SqlParameter("@userorder", SqlDbType.NVarChar, 30),
        new SqlParameter("@supplierId", SqlDbType.Int, 4),
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@mobile", SqlDbType.VarChar, 20),
        new SqlParameter("@fee", SqlDbType.Decimal, 9),
        new SqlParameter("@message", SqlDbType.NVarChar, 50),
        new SqlParameter("@servicenum", SqlDbType.VarChar, 50),
        new SqlParameter("@linkid", SqlDbType.VarChar, 50),
        new SqlParameter("@gwid", SqlDbType.VarChar, 2),
        new SqlParameter("@payRate", SqlDbType.Decimal, 9),
        new SqlParameter("@supplierRate", SqlDbType.Decimal, 9),
        new SqlParameter("@promRate", SqlDbType.Decimal, 9),
        new SqlParameter("@payAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@promAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@supplierAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@profits", SqlDbType.Decimal, 9),
        new SqlParameter("@server", SqlDbType.Int, 4),
        new SqlParameter("@addtime", SqlDbType.DateTime),
        new SqlParameter("@completetime", SqlDbType.DateTime),
        new SqlParameter("@status", SqlDbType.TinyInt),
        new SqlParameter("@manageId", SqlDbType.Int),
        new SqlParameter("@Cmd", SqlDbType.NVarChar, 10),
        new SqlParameter("@userMsgContenct", SqlDbType.NVarChar, 50)
            };
            sqlParameterArray[0].Value = (object)model.orderid;
            sqlParameterArray[1].Value = (object)model.userorder;
            sqlParameterArray[2].Value = (object)model.supplierId;
            sqlParameterArray[3].Value = (object)model.userid;
            sqlParameterArray[4].Value = (object)model.mobile;
            sqlParameterArray[5].Value = (object)model.fee;
            sqlParameterArray[6].Value = (object)model.message;
            sqlParameterArray[7].Value = (object)model.servicenum;
            sqlParameterArray[8].Value = (object)model.linkid;
            sqlParameterArray[9].Value = (object)model.gwid;
            sqlParameterArray[10].Value = (object)model.payRate;
            sqlParameterArray[11].Value = (object)model.supplierRate;
            sqlParameterArray[12].Value = (object)model.promRate;
            sqlParameterArray[13].Value = (object)model.payAmt;
            sqlParameterArray[14].Value = (object)model.promAmt;
            sqlParameterArray[15].Value = (object)model.supplierAmt;
            sqlParameterArray[16].Value = (object)model.profits;
            sqlParameterArray[17].Value = (object)model.server;
            sqlParameterArray[18].Value = (object)model.addtime;
            sqlParameterArray[19].Value = (object)model.completetime;
            sqlParameterArray[20].Value = (object)model.status;
            sqlParameterArray[21].Value = (object)model.manageId;
            sqlParameterArray[22].Value = (object)model.Cmd;
            sqlParameterArray[23].Value = (object)model.userMsgContenct;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordersms_Insert", sqlParameterArray);
            return true;
        }

        internal OrderSmsInfo GetModelFromDs(DataSet ds)
        {
            OrderSmsInfo orderSmsInfo = new OrderSmsInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (OrderSmsInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                orderSmsInfo.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            orderSmsInfo.orderid = ds.Tables[0].Rows[0]["orderid"].ToString();
            orderSmsInfo.userorder = ds.Tables[0].Rows[0]["userorder"].ToString();
            if (ds.Tables[0].Rows[0]["supplierId"].ToString() != "")
                orderSmsInfo.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
            if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                orderSmsInfo.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
            orderSmsInfo.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
            if (ds.Tables[0].Rows[0]["fee"].ToString() != "")
                orderSmsInfo.fee = Decimal.Parse(ds.Tables[0].Rows[0]["fee"].ToString());
            orderSmsInfo.message = ds.Tables[0].Rows[0]["message"].ToString();
            orderSmsInfo.servicenum = ds.Tables[0].Rows[0]["servicenum"].ToString();
            orderSmsInfo.linkid = ds.Tables[0].Rows[0]["linkid"].ToString();
            orderSmsInfo.gwid = ds.Tables[0].Rows[0]["gwid"].ToString();
            if (ds.Tables[0].Rows[0]["payRate"].ToString() != "")
                orderSmsInfo.payRate = Decimal.Parse(ds.Tables[0].Rows[0]["payRate"].ToString());
            if (ds.Tables[0].Rows[0]["supplierRate"].ToString() != "")
                orderSmsInfo.supplierRate = Decimal.Parse(ds.Tables[0].Rows[0]["supplierRate"].ToString());
            if (ds.Tables[0].Rows[0]["promRate"].ToString() != "")
                orderSmsInfo.promRate = Decimal.Parse(ds.Tables[0].Rows[0]["promRate"].ToString());
            if (ds.Tables[0].Rows[0]["payAmt"].ToString() != "")
                orderSmsInfo.payAmt = Decimal.Parse(ds.Tables[0].Rows[0]["payAmt"].ToString());
            if (ds.Tables[0].Rows[0]["promAmt"].ToString() != "")
                orderSmsInfo.promAmt = Decimal.Parse(ds.Tables[0].Rows[0]["promAmt"].ToString());
            if (ds.Tables[0].Rows[0]["supplierAmt"].ToString() != "")
                orderSmsInfo.supplierAmt = Decimal.Parse(ds.Tables[0].Rows[0]["supplierAmt"].ToString());
            if (ds.Tables[0].Rows[0]["profits"].ToString() != "")
                orderSmsInfo.profits = Decimal.Parse(ds.Tables[0].Rows[0]["profits"].ToString());
            if (ds.Tables[0].Rows[0]["server"].ToString() != "")
                orderSmsInfo.server = int.Parse(ds.Tables[0].Rows[0]["server"].ToString());
            if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                orderSmsInfo.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
            if (ds.Tables[0].Rows[0]["completetime"].ToString() != "")
                orderSmsInfo.completetime = DateTime.Parse(ds.Tables[0].Rows[0]["completetime"].ToString());
            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                orderSmsInfo.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            orderSmsInfo.notifyurl = ds.Tables[0].Rows[0]["notifyurl"].ToString();
            orderSmsInfo.againNotifyUrl = ds.Tables[0].Rows[0]["againNotifyUrl"].ToString();
            if (ds.Tables[0].Rows[0]["notifycount"].ToString() != "")
                orderSmsInfo.notifycount = int.Parse(ds.Tables[0].Rows[0]["notifycount"].ToString());
            if (ds.Tables[0].Rows[0]["notifystat"].ToString() != "")
                orderSmsInfo.notifystat = int.Parse(ds.Tables[0].Rows[0]["notifystat"].ToString());
            orderSmsInfo.notifycontext = ds.Tables[0].Rows[0]["notifycontext"].ToString();
            return orderSmsInfo;
        }

        public OrderSmsInfo GetModel(int id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@id", SqlDbType.Int),
        new SqlParameter("@userid", SqlDbType.Int)
            };
            sqlParameterArray[0].Value = (object)id;
            sqlParameterArray[1].Value = (object)DBNull.Value;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_GetById", sqlParameterArray));
        }

        public OrderSmsInfo GetModel(int id, int userid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@id", SqlDbType.Int),
        new SqlParameter("@userid", SqlDbType.Int)
            };
            sqlParameterArray[0].Value = (object)id;
            sqlParameterArray[1].Value = (object)userid;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_GetById", sqlParameterArray));
        }

        public OrderSmsInfo GetModel(string orderId)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@orderId", SqlDbType.VarChar, 30)
            };
            sqlParameterArray[0].Value = (object)orderId;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_Get", sqlParameterArray));
        }

        public OrderSmsInfo GetModel(int suppId, string linkId)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@supplierId", SqlDbType.Int),
        new SqlParameter("@linkid", SqlDbType.VarChar, 50)
            };
            sqlParameterArray[0].Value = (object)suppId;
            sqlParameterArray[1].Value = (object)linkId;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordersms_GetModel", sqlParameterArray));
        }

        public bool Deduct(string orderid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30),
        new SqlParameter("@result", SqlDbType.Bit)
            };
            sqlParameterArray[0].Value = (object)orderid;
            sqlParameterArray[1].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordersms_deduct", sqlParameterArray);
            return (bool)sqlParameterArray[1].Value;
        }

        public bool ReDeduct(string orderid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30),
        new SqlParameter("@result", SqlDbType.Bit)
            };
            sqlParameterArray[0].Value = (object)orderid;
            sqlParameterArray[1].Direction = ParameterDirection.Output;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordersms_rededuct", sqlParameterArray);
            return (bool)sqlParameterArray[1].Value;
        }

        public bool Notify(OrderSmsInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[10]
            {
        new SqlParameter("@linkId", SqlDbType.VarChar, 50),
        new SqlParameter("@againNotifyUrl", SqlDbType.VarChar, 300),
        new SqlParameter("@notifycount", SqlDbType.Int, 4),
        new SqlParameter("@notifystat", SqlDbType.TinyInt, 1),
        new SqlParameter("@notifycontext", SqlDbType.VarChar, 200),
        new SqlParameter("@notifytime", SqlDbType.DateTime),
        new SqlParameter("@userOrder", SqlDbType.VarChar, 30),
        new SqlParameter("@suppId", SqlDbType.VarChar, 50),
        new SqlParameter("@issucc", SqlDbType.Bit, 1),
        new SqlParameter("@errcode", SqlDbType.VarChar, 50)
            };
            sqlParameterArray[0].Value = (object)model.linkid;
            sqlParameterArray[1].Value = (object)model.againNotifyUrl;
            sqlParameterArray[2].Value = (object)model.notifycount;
            sqlParameterArray[3].Value = (object)model.notifystat;
            sqlParameterArray[4].Value = (object)model.notifycontext;
            sqlParameterArray[5].Value = (object)DateTime.Now;
            if (model.issucc)
                sqlParameterArray[6].Value = (object)model.notifycontext;
            else
                sqlParameterArray[6].Value = (object)string.Empty;
            sqlParameterArray[7].Value = (object)model.supplierId;
            sqlParameterArray[8].Value = (object)(model.issucc ? 1 : 0);
            sqlParameterArray[9].Value = (object)model.errcode;
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordersms_notify", sqlParameterArray) > 0;
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_ordersms";
                string columns = "[id],[orderid],[userorder],[supplierId],[userid],[mobile],[fee],[message],[servicenum],[linkid],[gwid],[payRate],[supplierRate],[promRate],[payAmt],[promAmt],[supplierAmt],[profits],[server],[addtime],[completetime],\r\n[againNotifyUrl],[notifycount],[notifystat],[notifycontext],[notifytime],[status],[commission]";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = OrderSms.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL(columns, tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect sum(fee) realvalue,sum(case when [status]=2 then payAmt else 0 end) payAmt,sum(supplierAmt-(case when [status]=2 then payAmt else 0 end)) profits,sum(promAmt) promAmt,sum(commission) commission from V_ordersms where " + wheres, paramList.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return dataSet;
            }
        }

        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            StringBuilder stringBuilder = new StringBuilder(" 1 = 1");
            if (param != null && param.Count > 0)
            {
                for (int index = 0; index < param.Count; ++index)
                {
                    SearchParam searchParam = param[index];
                    if (searchParam.CmpOperator == "=")
                    {
                        switch (searchParam.ParamKey.Trim().ToLower())
                        {
                            case "userid":
                                stringBuilder.Append(" AND [userid] = @userid");
                                SqlParameter sqlParameter1 = new SqlParameter("@userid", SqlDbType.Int);
                                sqlParameter1.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter1);
                                continue;
                            case "typeid":
                                stringBuilder.Append(" AND [typeId] = @typeId");
                                SqlParameter sqlParameter2 = new SqlParameter("@typeId", SqlDbType.Int);
                                sqlParameter2.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter2);
                                continue;
                            case "userorder":
                                stringBuilder.Append(" AND [userorder] like @userorder");
                                SqlParameter sqlParameter3 = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                                sqlParameter3.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter3);
                                continue;
                            case "supplierorder":
                                stringBuilder.Append(" AND [linkid] like @supplierOrder");
                                SqlParameter sqlParameter4 = new SqlParameter("@supplierOrder", SqlDbType.VarChar, 30);
                                sqlParameter4.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter4);
                                continue;
                            case "orderid_like":
                                stringBuilder.Append(" AND [orderid] like @orderid");
                                SqlParameter sqlParameter5 = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                sqlParameter5.Value = (object)(SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter5);
                                continue;
                            case "status":
                                stringBuilder.Append(" AND [status] = @status");
                                SqlParameter sqlParameter6 = new SqlParameter("@status", SqlDbType.TinyInt);
                                sqlParameter6.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter6);
                                continue;
                            case "notifystat":
                                stringBuilder.Append(" AND [notifystat] = @notifystat");
                                SqlParameter sqlParameter7 = new SqlParameter("@notifystat", SqlDbType.TinyInt);
                                sqlParameter7.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter7);
                                continue;
                            case "promid":
                                stringBuilder.Append(" AND exists(select 0 from PromotionUser where PromotionUser.PID = @promid and PromotionUser.RegId=userid)");
                                SqlParameter sqlParameter8 = new SqlParameter("@promid", SqlDbType.Int);
                                sqlParameter8.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter8);
                                continue;
                            case "stime":
                                stringBuilder.Append(" AND [addtime] >= @stime");
                                SqlParameter sqlParameter9 = new SqlParameter("@stime", SqlDbType.DateTime);
                                sqlParameter9.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter9);
                                continue;
                            case "etime":
                                stringBuilder.Append(" AND [addtime] <= @etime");
                                SqlParameter sqlParameter10 = new SqlParameter("@etime", SqlDbType.DateTime);
                                sqlParameter10.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter10);
                                continue;
                            case "mobile":
                                stringBuilder.Append(" AND [mobile] like @mobile");
                                SqlParameter sqlParameter11 = new SqlParameter("@mobile", SqlDbType.VarChar, 20);
                                sqlParameter11.Value = (object)("%" + (string)searchParam.ParamValue + "%");
                                paramList.Add(sqlParameter11);
                                continue;
                            default:
                                continue;
                        }
                    }
                    else
                    {
                        switch (searchParam.ParamKey.Trim().ToLower())
                        {
                            case "status":
                                stringBuilder.AppendFormat(" AND [status] {0} @status1", (object)searchParam.CmpOperator);
                                SqlParameter sqlParameter12 = new SqlParameter("@status1", SqlDbType.TinyInt);
                                sqlParameter12.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter12);
                                break;
                        }
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
