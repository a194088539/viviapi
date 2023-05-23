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
    public class OrderBank : IOrderBank
    {
        internal const string SQL_TABLE = "v_orderbank";
        internal const string FIELDS = "[id]\r\n      ,[orderid]\r\n      ,[ordertype]\r\n      ,[userid]\r\n      ,[typeId]\r\n      ,[paymodeId]\r\n      ,[userorder]\r\n      ,[refervalue]\r\n      ,[realvalue]\r\n      ,[notifyurl]\r\n      ,[againNotifyUrl]\r\n      ,[notifycount]\r\n      ,[notifystat]\r\n      ,[notifycontext]\r\n      ,[returnurl]\r\n      ,[attach]\r\n      ,[payerip]\r\n      ,[clientip]\r\n      ,[referUrl]\r\n      ,[addtime]\r\n      ,[supplierID]\r\n      ,[supplierOrder]\r\n      ,[status]\r\n      ,[completetime]\r\n      ,[payRate]\r\n      ,[supplierRate]\r\n      ,[promRate]\r\n      ,[payAmt]\r\n      ,[promAmt]\r\n      ,[supplierAmt]\r\n      ,[profits]\r\n      ,[server]\r\n      ,[modetypename]\r\n      ,[modeName],[commission],[notifytime],[version]\r\n      ,cus_subject,cus_price,cus_quantity,cus_description,cus_field1,cus_field2,cus_field3,cus_field4,cus_field5,agentid";

        public long Insert(OrderBankInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[30]
      {
        new SqlParameter("@id", SqlDbType.BigInt, 8),
        new SqlParameter("@orderid", SqlDbType.VarChar, 20),
        new SqlParameter("@ordertype", SqlDbType.TinyInt, 1),
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@typeId", SqlDbType.Int),
        new SqlParameter("@paymodeId", SqlDbType.VarChar, 10),
        new SqlParameter("@userorder", SqlDbType.VarChar, 30),
        new SqlParameter("@refervalue", SqlDbType.Decimal, 9),
        new SqlParameter("@notifyurl", SqlDbType.VarChar, 200),
        new SqlParameter("@returnurl", SqlDbType.VarChar, 200),
        new SqlParameter("@attach", SqlDbType.VarChar, (int) byte.MaxValue),
        new SqlParameter("@payerip", SqlDbType.VarChar, 20),
        new SqlParameter("@clientip", SqlDbType.VarChar, 20),
        new SqlParameter("@referUrl", SqlDbType.VarChar, 200),
        new SqlParameter("@addtime", SqlDbType.DateTime),
        new SqlParameter("@supplierId", SqlDbType.Int, 4),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@server", SqlDbType.Int),
        new SqlParameter("@manageId", SqlDbType.Int),
        new SqlParameter("@version", SqlDbType.VarChar, 10),
        new SqlParameter("@cus_subject", SqlDbType.NVarChar, 100),
        new SqlParameter("@cus_price", SqlDbType.NVarChar, 50),
        new SqlParameter("@cus_quantity", SqlDbType.NVarChar, 50),
        new SqlParameter("@cus_description", SqlDbType.NVarChar, 1000),
        new SqlParameter("@cus_field1", SqlDbType.NVarChar, 100),
        new SqlParameter("@cus_field2", SqlDbType.NVarChar, 100),
        new SqlParameter("@cus_field3", SqlDbType.NVarChar, 100),
        new SqlParameter("@cus_field4", SqlDbType.NVarChar, 100),
        new SqlParameter("@cus_field5", SqlDbType.NVarChar, 100),
        new SqlParameter("@agentId", SqlDbType.Int, 4)
      };
            sqlParameterArray[0].Direction = ParameterDirection.Output;
            sqlParameterArray[1].Value = (object)model.orderid;
            sqlParameterArray[2].Value = (object)model.ordertype;
            sqlParameterArray[3].Value = (object)model.userid;
            sqlParameterArray[4].Value = (object)model.typeId;
            sqlParameterArray[5].Value = (object)model.paymodeId;
            sqlParameterArray[6].Value = (object)model.userorder;
            sqlParameterArray[7].Value = (object)model.refervalue;
            sqlParameterArray[8].Value = (object)model.notifyurl;
            sqlParameterArray[9].Value = (object)model.returnurl;
            sqlParameterArray[10].Value = (object)model.attach;
            sqlParameterArray[11].Value = (object)model.payerip;
            sqlParameterArray[12].Value = (object)model.clientip;
            sqlParameterArray[13].Value = (object)model.referUrl;
            sqlParameterArray[14].Value = (object)model.addtime;
            sqlParameterArray[15].Value = (object)model.supplierId;
            sqlParameterArray[16].Value = (object)model.status;
            sqlParameterArray[17].Value = (object)model.server;
            sqlParameterArray[18].Value = (object)model.manageId;
            sqlParameterArray[19].Value = (object)model.version;
            sqlParameterArray[20].Value = (object)model.cus_subject;
            sqlParameterArray[21].Value = (object)model.cus_price;
            sqlParameterArray[22].Value = (object)model.cus_quantity;
            sqlParameterArray[23].Value = (object)model.cus_description;
            sqlParameterArray[24].Value = (object)model.cus_field1;
            sqlParameterArray[25].Value = (object)model.cus_field2;
            sqlParameterArray[26].Value = (object)model.cus_field3;
            sqlParameterArray[27].Value = (object)model.cus_field4;
            sqlParameterArray[28].Value = (object)model.cus_field5;
            sqlParameterArray[29].Value = (object)model.agentId;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_add", sqlParameterArray);
            return (long)sqlParameterArray[0].Value;
        }

        public bool Complete(OrderBankInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[17]
      {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30),
        new SqlParameter("@userId", SqlDbType.Int),
        new SqlParameter("@status", SqlDbType.TinyInt),
        new SqlParameter("@supplierOrder", SqlDbType.VarChar, 50),
        new SqlParameter("@realvalue", SqlDbType.Decimal, 9),
        new SqlParameter("@payRate", SqlDbType.Decimal, 9),
        new SqlParameter("@supplierRate", SqlDbType.Decimal, 9),
        new SqlParameter("@payAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@supplierAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@profits", SqlDbType.Decimal, 9),
        new SqlParameter("@addtime", SqlDbType.DateTime),
        new SqlParameter("@completetime", SqlDbType.DateTime),
        new SqlParameter("@manageId", SqlDbType.Int),
        new SqlParameter("@promRate", SqlDbType.Decimal, 9),
        new SqlParameter("@promAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@promId", SqlDbType.Int),
        new SqlParameter("@commission", SqlDbType.Decimal, 9)
      };
            sqlParameterArray[0].Value = (object)model.orderid;
            sqlParameterArray[1].Value = (object)model.userid;
            sqlParameterArray[2].Value = (object)model.status;
            sqlParameterArray[3].Value = (object)model.supplierOrder;
            sqlParameterArray[4].Value = (object)model.realvalue.Value;
            sqlParameterArray[5].Value = (object)model.payRate;
            sqlParameterArray[6].Value = (object)model.supplierRate;
            sqlParameterArray[7].Value = (object)model.payAmt;
            sqlParameterArray[8].Value = (object)model.supplierAmt;
            sqlParameterArray[9].Value = (object)model.profits;
            sqlParameterArray[10].Value = (object)DateTime.Now;
            sqlParameterArray[11].Value = (object)model.completetime;
            sqlParameterArray[12].Value = (object)model.manageId;
            sqlParameterArray[13].Value = (object)model.promRate;
            sqlParameterArray[14].Value = (object)model.promAmt;
            sqlParameterArray[15].Value = (object)model.agentId;
            sqlParameterArray[16].Value = (object)model.commission;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_settled", sqlParameterArray);
            return true;
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
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_deduct", sqlParameterArray);
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
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_rededuct", sqlParameterArray);
            return (bool)sqlParameterArray[1].Value;
        }

        internal OrderBankInfo GetModelFromDs(DataSet ds)
        {
            OrderBankInfo orderBankInfo = new OrderBankInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (OrderBankInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                orderBankInfo.id = long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            orderBankInfo.orderid = ds.Tables[0].Rows[0]["orderid"].ToString();
            if (ds.Tables[0].Rows[0]["ordertype"].ToString() != "")
                orderBankInfo.ordertype = int.Parse(ds.Tables[0].Rows[0]["ordertype"].ToString());
            if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                orderBankInfo.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
            if (ds.Tables[0].Rows[0]["typeId"].ToString() != "")
                orderBankInfo.typeId = int.Parse(ds.Tables[0].Rows[0]["typeId"].ToString());
            orderBankInfo.paymodeId = ds.Tables[0].Rows[0]["paymodeId"].ToString();
            orderBankInfo.userorder = ds.Tables[0].Rows[0]["userorder"].ToString();
            if (ds.Tables[0].Rows[0]["refervalue"].ToString() != "")
                orderBankInfo.refervalue = Decimal.Parse(ds.Tables[0].Rows[0]["refervalue"].ToString());
            if (ds.Tables[0].Rows[0]["realvalue"].ToString() != "")
                orderBankInfo.realvalue = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["realvalue"].ToString()));
            orderBankInfo.notifyurl = ds.Tables[0].Rows[0]["notifyurl"].ToString();
            orderBankInfo.againNotifyUrl = ds.Tables[0].Rows[0]["againNotifyUrl"].ToString();
            if (ds.Tables[0].Rows[0]["notifycount"].ToString() != "")
                orderBankInfo.notifycount = int.Parse(ds.Tables[0].Rows[0]["notifycount"].ToString());
            if (ds.Tables[0].Rows[0]["notifystat"].ToString() != "")
                orderBankInfo.notifystat = int.Parse(ds.Tables[0].Rows[0]["notifystat"].ToString());
            orderBankInfo.notifycontext = ds.Tables[0].Rows[0]["notifycontext"].ToString();
            orderBankInfo.returnurl = ds.Tables[0].Rows[0]["returnurl"].ToString();
            orderBankInfo.attach = ds.Tables[0].Rows[0]["attach"].ToString();
            orderBankInfo.payerip = ds.Tables[0].Rows[0]["payerip"].ToString();
            orderBankInfo.clientip = ds.Tables[0].Rows[0]["clientip"].ToString();
            orderBankInfo.referUrl = ds.Tables[0].Rows[0]["referUrl"].ToString();
            if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                orderBankInfo.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
            if (ds.Tables[0].Rows[0]["supplierID"].ToString() != "")
                orderBankInfo.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierID"].ToString());
            orderBankInfo.supplierOrder = ds.Tables[0].Rows[0]["supplierOrder"].ToString();
            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                orderBankInfo.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            if (ds.Tables[0].Rows[0]["completetime"].ToString() != "")
                orderBankInfo.completetime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["completetime"].ToString()));
            if (ds.Tables[0].Rows[0]["payRate"].ToString() != "")
                orderBankInfo.payRate = Decimal.Parse(ds.Tables[0].Rows[0]["payRate"].ToString());
            if (ds.Tables[0].Rows[0]["supplierRate"].ToString() != "")
                orderBankInfo.supplierRate = Decimal.Parse(ds.Tables[0].Rows[0]["supplierRate"].ToString());
            if (ds.Tables[0].Rows[0]["promRate"].ToString() != "")
                orderBankInfo.promRate = Decimal.Parse(ds.Tables[0].Rows[0]["promRate"].ToString());
            if (ds.Tables[0].Rows[0]["payAmt"].ToString() != "")
                orderBankInfo.payAmt = Decimal.Parse(ds.Tables[0].Rows[0]["payAmt"].ToString());
            if (ds.Tables[0].Rows[0]["promAmt"].ToString() != "")
                orderBankInfo.promAmt = Decimal.Parse(ds.Tables[0].Rows[0]["promAmt"].ToString());
            if (ds.Tables[0].Rows[0]["supplierAmt"].ToString() != "")
                orderBankInfo.supplierAmt = Decimal.Parse(ds.Tables[0].Rows[0]["supplierAmt"].ToString());
            if (ds.Tables[0].Rows[0]["profits"].ToString() != "")
                orderBankInfo.profits = Decimal.Parse(ds.Tables[0].Rows[0]["profits"].ToString());
            if (ds.Tables[0].Rows[0]["server"].ToString() != "")
                orderBankInfo.server = new int?(int.Parse(ds.Tables[0].Rows[0]["server"].ToString()));
            if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                orderBankInfo.manageId = new int?(int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString()));
            if (ds.Tables[0].Rows[0]["commission"].ToString() != "")
                orderBankInfo.commission = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["commission"].ToString()));
            orderBankInfo.version = ds.Tables[0].Rows[0]["version"].ToString();
            if (ds.Tables[0].Rows[0]["cus_subject"] != null && ds.Tables[0].Rows[0]["cus_subject"].ToString() != "")
                orderBankInfo.cus_subject = ds.Tables[0].Rows[0]["cus_subject"].ToString();
            if (ds.Tables[0].Rows[0]["cus_price"] != null && ds.Tables[0].Rows[0]["cus_price"].ToString() != "")
                orderBankInfo.cus_price = ds.Tables[0].Rows[0]["cus_price"].ToString();
            if (ds.Tables[0].Rows[0]["cus_quantity"] != null && ds.Tables[0].Rows[0]["cus_quantity"].ToString() != "")
                orderBankInfo.cus_quantity = ds.Tables[0].Rows[0]["cus_quantity"].ToString();
            if (ds.Tables[0].Rows[0]["cus_description"] != null && ds.Tables[0].Rows[0]["cus_description"].ToString() != "")
                orderBankInfo.cus_description = ds.Tables[0].Rows[0]["cus_description"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field1"] != null && ds.Tables[0].Rows[0]["cus_field1"].ToString() != "")
                orderBankInfo.cus_field1 = ds.Tables[0].Rows[0]["cus_field1"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field2"] != null && ds.Tables[0].Rows[0]["cus_field2"].ToString() != "")
                orderBankInfo.cus_field2 = ds.Tables[0].Rows[0]["cus_field2"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field3"] != null && ds.Tables[0].Rows[0]["cus_field3"].ToString() != "")
                orderBankInfo.cus_field3 = ds.Tables[0].Rows[0]["cus_field3"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field4"] != null && ds.Tables[0].Rows[0]["cus_field4"].ToString() != "")
                orderBankInfo.cus_field4 = ds.Tables[0].Rows[0]["cus_field4"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field5"] != null && ds.Tables[0].Rows[0]["cus_field5"].ToString() != "")
                orderBankInfo.cus_field5 = ds.Tables[0].Rows[0]["cus_field5"].ToString();
            if (ds.Tables[0].Rows[0]["agentid"] != null && ds.Tables[0].Rows[0]["agentid"].ToString() != "")
                orderBankInfo.agentId = Convert.ToInt32(ds.Tables[0].Rows[0]["agentid"].ToString());
            return orderBankInfo;
        }

        public OrderBankInfo GetModel(string orderId)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
      {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30)
      };
            sqlParameterArray[0].Value = (object)orderId;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_getbankdirectinfo", sqlParameterArray));
        }

        public OrderBankInfo GetModel(long id, int userid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@id", SqlDbType.BigInt),
        new SqlParameter("@userid", SqlDbType.Int)
      };
            sqlParameterArray[0].Value = (object)id;
            sqlParameterArray[1].Value = (object)userid;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_GetModel", sqlParameterArray));
        }

        public OrderBankInfo GetModel(long id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@id", SqlDbType.BigInt),
        new SqlParameter("@userid", SqlDbType.Int)
      };
            sqlParameterArray[0].Value = (object)id;
            sqlParameterArray[1].Value = (object)DBNull.Value;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_GetModel", sqlParameterArray));
        }

        public bool Notify(OrderBankInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[6]
      {
        new SqlParameter("@orderid", SqlDbType.VarChar, 20),
        new SqlParameter("@againNotifyUrl", SqlDbType.VarChar, 2000),
        new SqlParameter("@notifycount", SqlDbType.Int, 4),
        new SqlParameter("@notifystat", SqlDbType.TinyInt, 1),
        new SqlParameter("@notifycontext", SqlDbType.VarChar, 200),
        new SqlParameter("@notifytime", SqlDbType.DateTime)
      };
            sqlParameterArray[0].Value = (object)model.orderid;
            sqlParameterArray[1].Value = (object)model.againNotifyUrl;
            sqlParameterArray[2].Value = (object)model.notifycount;
            sqlParameterArray[3].Value = (object)model.notifystat;
            sqlParameterArray[4].Value = (object)model.notifycontext;
            sqlParameterArray[5].Value = (object)model.notifytime;
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_orderbank_notify", sqlParameterArray) > 0;
        }



        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "v_orderbank";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = OrderBank.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[orderid]\r\n      ,[ordertype]\r\n      ,[userid]\r\n      ,[typeId]\r\n      ,[paymodeId]\r\n      ,[userorder]\r\n      ,[refervalue]\r\n      ,[realvalue]\r\n      ,[notifyurl]\r\n      ,[againNotifyUrl]\r\n      ,[notifycount]\r\n      ,[notifystat]\r\n      ,[notifycontext]\r\n      ,[returnurl]\r\n      ,[attach]\r\n      ,[payerip]\r\n      ,[clientip]\r\n      ,[referUrl]\r\n      ,[addtime]\r\n      ,[supplierID]\r\n      ,[supplierOrder]\r\n      ,[status]\r\n      ,[completetime]\r\n      ,[payRate]\r\n      ,[supplierRate]\r\n      ,[promRate]\r\n      ,[payAmt]\r\n      ,[promAmt]\r\n      ,[supplierAmt]\r\n      ,[profits]\r\n      ,[server]\r\n      ,[modetypename]\r\n      ,[modeName],[commission],[notifytime],[version]\r\n      ,cus_subject,cus_price,cus_quantity,cus_description,cus_field1,cus_field2,cus_field3,cus_field4,cus_field5,agentid,(realvalue-payAmt) as shouxufei", tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect sum(1) ordtotal,sum(case when [status]=2 then 1 else 0 end) succordtotal,sum(refervalue) refervalue,sum(case when [status]=2 then realvalue else 0 end) realvalue,sum(isnull(promAmt,0)) promAmt,sum(case when [status]=2 then payAmt else 0 end) payAmt,sum(case when [status]=2 then supplierAmt-payAmt-promAmt else 0 end) profits,sum(promAmt) promAmt,sum(commission) commission,(sum(case when [status]=2 then realvalue else 0 end)-sum(case when [status]=2 then payAmt else 0 end)) as sumsxf from v_orderbank where " + wheres, paramList.ToArray());

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
                            case "agentid":
                                stringBuilder.Append(" AND [agentid] = @agentid");
                                SqlParameter sqlParameter2 = new SqlParameter("@agentid", SqlDbType.Int);
                                sqlParameter2.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter2);
                                continue;
                            case "manageid":
                                stringBuilder.Append(" AND [manageId] = @manageId");
                                SqlParameter sqlParameter3 = new SqlParameter("@manageId", SqlDbType.Int);
                                sqlParameter3.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter3);
                                continue;
                            case "typeid":
                                stringBuilder.Append(" AND [typeId] = @typeId");
                                SqlParameter sqlParameter4 = new SqlParameter("@typeId", SqlDbType.Int);
                                sqlParameter4.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter4);
                                continue;
                            case "supplierid":
                                stringBuilder.Append(" AND [supplierId] = @supplierId");
                                SqlParameter sqlParameter5 = new SqlParameter("@supplierId", SqlDbType.Int);
                                sqlParameter5.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter5);
                                continue;
                            case "userorder":
                                stringBuilder.Append(" AND [userorder] like @userorder");
                                SqlParameter sqlParameter6 = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                                sqlParameter6.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter6);
                                continue;
                            case "orderid":
                                stringBuilder.Append(" AND [orderid] like @orderid");
                                SqlParameter sqlParameter7 = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                sqlParameter7.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter7);
                                continue;
                            case "supplierorder":
                                stringBuilder.Append(" AND [supplierOrder] like @supplierOrder");
                                SqlParameter sqlParameter8 = new SqlParameter("@supplierOrder", SqlDbType.VarChar, 30);
                                sqlParameter8.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter8);
                                continue;
                            case "orderid_like":
                                stringBuilder.Append(" AND [orderid] like @orderid");
                                SqlParameter sqlParameter9 = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                sqlParameter9.Value = (object)(SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter9);
                                continue;
                            case "status":
                                stringBuilder.Append(" AND [status] = @status");
                                SqlParameter sqlParameter10 = new SqlParameter("@status", SqlDbType.TinyInt);
                                sqlParameter10.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter10);
                                continue;
                            case "statusallfail":
                                stringBuilder.Append(" AND ([status] = 4 or  [status] = 8)");
                                continue;
                            case "notifystat":
                                stringBuilder.Append(" AND [notifystat] = @notifystat");
                                SqlParameter sqlParameter11 = new SqlParameter("@notifystat", SqlDbType.TinyInt);
                                sqlParameter11.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter11);
                                continue;
                            case "promid":
                                stringBuilder.Append(" AND exists(select 0 from PromotionUser where PromotionUser.PID = @promid and PromotionUser.RegId=userid)");
                                SqlParameter sqlParameter12 = new SqlParameter("@promid", SqlDbType.Int);
                                sqlParameter12.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter12);
                                continue;
                            case "stime":
                                stringBuilder.Append(" AND [processingtime] >= @stime");
                                SqlParameter sqlParameter13 = new SqlParameter("@stime", SqlDbType.DateTime);
                                sqlParameter13.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter13);
                                continue;
                            case "etime":
                                stringBuilder.Append(" AND [processingtime] <= @etime");
                                SqlParameter sqlParameter14 = new SqlParameter("@etime", SqlDbType.DateTime);
                                sqlParameter14.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter14);
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
                                SqlParameter sqlParameter15 = new SqlParameter("@status1", SqlDbType.TinyInt);
                                sqlParameter15.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter15);
                                break;
                        }
                    }
                }
            }
            return ((object)stringBuilder).ToString();
        }
    }
}
