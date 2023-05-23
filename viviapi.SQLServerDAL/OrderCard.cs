using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Caching;
using viviapi.IDAL;
using viviapi.Model.Order;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.SQLServerDAL
{
    public class OrderCard : IOrderCard
    {
        internal const string SQL_TABLE = "v_ordercard";
        internal const string FIELDS = "[id]\r\n      ,[orderid]\r\n      ,[ordertype]\r\n      ,[userid]\r\n      ,[typeId]\r\n      ,[paymodeId]\r\n      ,[userorder]\r\n      ,[refervalue]\r\n      ,[realvalue]\r\n      ,[notifyurl]\r\n      ,[againNotifyUrl]\r\n      ,[notifycount]\r\n      ,[notifystat]\r\n      ,[notifycontext]\r\n      ,[notifytime]\r\n      ,[returnurl]\r\n      ,[attach]\r\n      ,[payerip]\r\n      ,[clientip]\r\n      ,[referUrl]\r\n      ,[addtime]\r\n      ,[supplierID]\r\n      ,[supplierOrder]\r\n      ,[status]\r\n      ,[completetime]\r\n      ,[payRate]\r\n      ,[supplierRate]\r\n      ,[promRate]\r\n      ,[payAmt]\r\n      ,[promAmt]\r\n      ,[supplierAmt]\r\n      ,[profits]\r\n      ,[server]\r\n      ,[modetypename]\r\n      ,[modeName]\r\n      ,[cardNo]\r\n      ,[cardPwd]\r\n      ,[desc]\r\n      ,[manageId]\r\n      ,[msg]\r\n      ,[commission]\r\n      ,[cardnum]\r\n      ,[resultcode]\r\n      ,[ismulticard]\r\n      ,[version],cus_subject,cus_price,cus_quantity,cus_description,cus_field1,cus_field2,cus_field3,cus_field4,cus_field5,errtype,agentid,faceValue";

        public long Insert(OrderCardInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[43]
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
        new SqlParameter("@referUrl", SqlDbType.NVarChar, 2000),
        new SqlParameter("@addtime", SqlDbType.DateTime),
        new SqlParameter("@supplierID", SqlDbType.Int, 4),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@cardNo", SqlDbType.NVarChar, 1000),
        new SqlParameter("@cardPwd", SqlDbType.NVarChar, 1000),
        new SqlParameter("@server", SqlDbType.Int),
        new SqlParameter("@manageId", SqlDbType.Int),
        new SqlParameter("@cardnum", SqlDbType.Int),
        new SqlParameter("@resultcode", SqlDbType.NVarChar, 100),
        new SqlParameter("@ismulticard", SqlDbType.TinyInt, 1),
        new SqlParameter("@ovalue", SqlDbType.NVarChar, 200),
        new SqlParameter("@opstate", SqlDbType.NVarChar, 200),
        new SqlParameter("@msg", SqlDbType.NVarChar, 200),
        new SqlParameter("@cardtype", SqlDbType.Int, 4),
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
        new SqlParameter("@agentid", SqlDbType.Int),
        new SqlParameter("@faceValue", SqlDbType.Decimal, 9),
        new SqlParameter("@userViewMsg", SqlDbType.NVarChar, 100),
        new SqlParameter("@errtype", SqlDbType.NVarChar, 50),
        new SqlParameter("@makeup", SqlDbType.TinyInt, 1)
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
            sqlParameterArray[17].Value = (object)model.cardNo;
            sqlParameterArray[18].Value = (object)model.cardPwd;
            sqlParameterArray[19].Value = (object)model.server;
            sqlParameterArray[20].Value = (object)model.manageId;
            sqlParameterArray[21].Value = (object)model.cardnum;
            sqlParameterArray[22].Value = (object)model.resultcode;
            sqlParameterArray[23].Value = (object)model.ismulticard;
            sqlParameterArray[24].Value = (object)model.ovalue;
            sqlParameterArray[25].Value = (object)model.opstate;
            sqlParameterArray[26].Value = (object)model.msg;
            sqlParameterArray[27].Value = (object)model.cardType;
            sqlParameterArray[28].Value = (object)model.version;
            sqlParameterArray[29].Value = (object)model.cus_subject;
            sqlParameterArray[30].Value = (object)model.cus_price;
            sqlParameterArray[31].Value = (object)model.cus_quantity;
            sqlParameterArray[32].Value = (object)model.cus_description;
            sqlParameterArray[33].Value = (object)model.cus_field1;
            sqlParameterArray[34].Value = (object)model.cus_field2;
            sqlParameterArray[35].Value = (object)model.cus_field3;
            sqlParameterArray[36].Value = (object)model.cus_field4;
            sqlParameterArray[37].Value = (object)model.cus_field5;
            sqlParameterArray[38].Value = (object)model.agentId;
            sqlParameterArray[39].Value = (object)model.faceValue;
            sqlParameterArray[40].Value = (object)model.userViewMsg;
            sqlParameterArray[41].Value = (object)model.errtype;
            sqlParameterArray[42].Value = (object)model.makeup;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_add", sqlParameterArray);
            return (long)sqlParameterArray[0].Value;
        }

        public long InsertItem(CardItemInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[21]
            {
        new SqlParameter("@id", SqlDbType.BigInt, 8),
        new SqlParameter("@userid", SqlDbType.Int, 4),
        new SqlParameter("@serial", SqlDbType.Int, 4),
        new SqlParameter("@porderid", SqlDbType.NVarChar, 30),
        new SqlParameter("@suppid", SqlDbType.Int, 4),
        new SqlParameter("@cardtype", SqlDbType.Int, 4),
        new SqlParameter("@cardno", SqlDbType.NVarChar, 30),
        new SqlParameter("@cardpwd", SqlDbType.NVarChar, 50),
        new SqlParameter("@refervalue", SqlDbType.Decimal, 9),
        new SqlParameter("@payrate", SqlDbType.Decimal, 9),
        new SqlParameter("@addtime", SqlDbType.DateTime),
        new SqlParameter("@supplierOrder", SqlDbType.VarChar, 50),
        new SqlParameter("@realvalue", SqlDbType.Decimal, 9),
        new SqlParameter("@status", SqlDbType.TinyInt, 1),
        new SqlParameter("@opstate", SqlDbType.NVarChar, 20),
        new SqlParameter("@msg", SqlDbType.NVarChar, 50),
        new SqlParameter("@completetime", SqlDbType.DateTime),
        new SqlParameter("@supplierrate", SqlDbType.Decimal, 9),
        new SqlParameter("@promrate", SqlDbType.Decimal, 9),
        new SqlParameter("@commission", SqlDbType.Decimal, 9),
        new SqlParameter("@agentid", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Direction = ParameterDirection.Output;
            sqlParameterArray[1].Value = (object)model.userid;
            sqlParameterArray[2].Value = (object)model.serial;
            sqlParameterArray[3].Value = (object)model.porderid;
            sqlParameterArray[4].Value = (object)model.suppid;
            sqlParameterArray[5].Value = (object)model.cardtype;
            sqlParameterArray[6].Value = (object)model.cardno;
            sqlParameterArray[7].Value = (object)model.cardpwd;
            sqlParameterArray[8].Value = (object)model.refervalue;
            sqlParameterArray[9].Value = (object)model.payrate;
            sqlParameterArray[10].Value = (object)model.addtime;
            sqlParameterArray[11].Value = (object)model.supplierOrder;
            sqlParameterArray[12].Value = (object)model.realvalue;
            sqlParameterArray[13].Value = (object)model.status;
            sqlParameterArray[14].Value = (object)model.opstate;
            sqlParameterArray[15].Value = (object)model.msg;
            sqlParameterArray[16].Value = (object)model.completetime;
            sqlParameterArray[17].Value = (object)model.supplierrate;
            sqlParameterArray[18].Value = (object)model.promrate;
            sqlParameterArray[19].Value = (object)model.commission;
            sqlParameterArray[20].Value = (object)model.agentId;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercarditem_add", sqlParameterArray);
            return (long)sqlParameterArray[0].Value;
        }

        public bool Complete(OrderCardInfo model)
        {
            if (HttpRuntime.Cache["Complete" + model.orderid] != null)
                return true;
            SqlParameter[] sqlParameterArray = new SqlParameter[30]
            {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30),
        new SqlParameter("@method", SqlDbType.TinyInt, 1),
        new SqlParameter("@supplierid", SqlDbType.Int, 4),
        new SqlParameter("@userId", SqlDbType.Int),
        new SqlParameter("@promId", SqlDbType.Int),
        new SqlParameter("@manageId", SqlDbType.Int),
        new SqlParameter("@status", SqlDbType.TinyInt),
        new SqlParameter("@supplierOrder", SqlDbType.VarChar, 50),
        new SqlParameter("@refervalue", SqlDbType.Decimal, 9),
        new SqlParameter("@faceValue", SqlDbType.Decimal, 9),
        new SqlParameter("@realvalue", SqlDbType.Decimal, 9),
        new SqlParameter("@withholdAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@profits", SqlDbType.Decimal, 9),
        new SqlParameter("@payRate", SqlDbType.Decimal, 9),
        new SqlParameter("@payAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@supplierRate", SqlDbType.Decimal, 9),
        new SqlParameter("@supplierAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@promRate", SqlDbType.Decimal, 9),
        new SqlParameter("@promAmt", SqlDbType.Decimal, 9),
        new SqlParameter("@addtime", SqlDbType.DateTime),
        new SqlParameter("@completetime", SqlDbType.DateTime),
        new SqlParameter("@msg", SqlDbType.NVarChar, 200),
        new SqlParameter("@userViewMsg", SqlDbType.NVarChar, 200),
        new SqlParameter("@opstate", SqlDbType.NVarChar, 200),
        new SqlParameter("@errtype", SqlDbType.NVarChar, 50),
        new SqlParameter("@typeid", SqlDbType.Int),
        new SqlParameter("@cardno", SqlDbType.VarChar, 40),
        new SqlParameter("@cardpwd", SqlDbType.VarChar, 40),
        new SqlParameter("@cardversion", SqlDbType.TinyInt, 1),
        new SqlParameter("@withhold_type", SqlDbType.TinyInt, 1)
            };
            sqlParameterArray[0].Value = (object)model.orderid;
            sqlParameterArray[1].Value = (object)model.method;
            sqlParameterArray[2].Value = (object)model.supplierId;
            sqlParameterArray[3].Value = (object)model.userid;
            sqlParameterArray[4].Value = (object)model.agentId;
            sqlParameterArray[5].Value = (object)model.manageId;
            sqlParameterArray[6].Value = (object)model.status;
            sqlParameterArray[7].Value = (object)model.supplierOrder;
            sqlParameterArray[8].Value = (object)model.refervalue;
            sqlParameterArray[9].Value = (object)model.faceValue;
            sqlParameterArray[10].Value = (object)model.realvalue;
            sqlParameterArray[11].Value = (object)model.withholdAmt;
            sqlParameterArray[12].Value = (object)model.profits;
            sqlParameterArray[13].Value = (object)model.payRate;
            sqlParameterArray[14].Value = (object)model.payAmt;
            sqlParameterArray[15].Value = (object)model.supplierRate;
            sqlParameterArray[16].Value = (object)model.supplierAmt;
            sqlParameterArray[17].Value = (object)model.promRate;
            sqlParameterArray[18].Value = (object)model.promAmt;
            sqlParameterArray[19].Value = (object)DateTime.Now;
            sqlParameterArray[20].Value = (object)model.completetime;
            sqlParameterArray[21].Value = (object)model.msg;
            sqlParameterArray[22].Value = (object)model.userViewMsg;
            sqlParameterArray[23].Value = (object)model.opstate;
            sqlParameterArray[24].Value = (object)model.errtype;
            sqlParameterArray[25].Value = (object)model.typeId;
            sqlParameterArray[26].Value = (object)model.cardNo;
            sqlParameterArray[27].Value = (object)model.cardPwd;
            sqlParameterArray[28].Value = (object)model.cardversion;
            sqlParameterArray[29].Value = (object)model.withhold_type;
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_settled", sqlParameterArray);
            HttpRuntime.Cache.Insert("Complete" + model.orderid, (object)model.status, (CacheDependency)null, DateTime.Now.AddSeconds(5.0), TimeSpan.Zero);
            return true;
        }

        public bool ItemComplete(CardItemInfo model, out bool allCompleted, out string opstate, out string ovalue, out Decimal ototalvalue)
        {
            allCompleted = false;
            opstate = string.Empty;
            ovalue = string.Empty;
            ototalvalue = new Decimal(0);
            if (HttpRuntime.Cache["Item_Complete" + model.porderid + model.serial.ToString()] != null)
                return true;
            SqlParameter[] sqlParameterArray = new SqlParameter[11]
            {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30),
        new SqlParameter("@serial", SqlDbType.Int),
        new SqlParameter("@status", SqlDbType.TinyInt),
        new SqlParameter("@supplierOrder", SqlDbType.VarChar, 50),
        new SqlParameter("@realvalue", SqlDbType.Decimal, 9),
        new SqlParameter("@payrate", SqlDbType.Decimal, 9),
        new SqlParameter("@completetime", SqlDbType.DateTime),
        new SqlParameter("@opstate", SqlDbType.NVarChar, 10),
        new SqlParameter("@msg", SqlDbType.NVarChar, 50),
        new SqlParameter("@completed", SqlDbType.TinyInt),
        new SqlParameter("@promRate", SqlDbType.Decimal, 9)
            };
            sqlParameterArray[0].Value = (object)model.porderid;
            sqlParameterArray[1].Value = (object)model.serial;
            sqlParameterArray[2].Value = (object)model.status;
            sqlParameterArray[3].Value = (object)model.supplierOrder;
            sqlParameterArray[4].Value = (object)model.realvalue;
            sqlParameterArray[5].Value = (object)model.payrate;
            sqlParameterArray[6].Value = (object)model.completetime;
            sqlParameterArray[7].Value = (object)model.opstate;
            sqlParameterArray[8].Value = (object)model.msg;
            sqlParameterArray[9].Direction = ParameterDirection.Output;
            sqlParameterArray[10].Value = (object)model.promrate;
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercarditem_settled", sqlParameterArray);
            HttpRuntime.Cache.Insert("Item_Complete" + model.porderid + model.serial.ToString(), (object)model.status, (CacheDependency)null, DateTime.Now.AddSeconds(5.0), TimeSpan.Zero);
            if (sqlParameterArray[9].Value != DBNull.Value)
            {
                if (Convert.ToInt32(sqlParameterArray[9].Value) == 1)
                    allCompleted = true;
                if (allCompleted)
                {
                    DataTable dataTable = dataSet.Tables[0];
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        DataRow dataRow = dataTable.Rows[0];
                        if (dataRow["totalValue"] != DBNull.Value)
                            ototalvalue = Convert.ToDecimal(dataRow["totalValue"]);
                        if (dataRow["resultcode"] != DBNull.Value)
                            opstate = Convert.ToString(dataRow["resultcode"]);
                        if (dataRow["ovalue"] != DBNull.Value)
                            ovalue = Convert.ToString(dataRow["ovalue"]);
                    }
                }
            }
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
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_deduct", sqlParameterArray);
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
            DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_rededuct", sqlParameterArray);
            return (bool)sqlParameterArray[1].Value;
        }

        internal OrderCardInfo GetModelFromDs(DataSet ds)
        {
            OrderCardInfo orderCardInfo = new OrderCardInfo();
            if (ds.Tables[0].Rows.Count <= 0)
                return (OrderCardInfo)null;
            if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                orderCardInfo.id = long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            orderCardInfo.orderid = ds.Tables[0].Rows[0]["orderid"].ToString();
            orderCardInfo.cardNo = ds.Tables[0].Rows[0]["cardNo"].ToString();
            orderCardInfo.cardPwd = ds.Tables[0].Rows[0]["cardPwd"].ToString();
            orderCardInfo.Desc = ds.Tables[0].Rows[0]["desc"].ToString();
            if (ds.Tables[0].Rows[0]["ordertype"].ToString() != "")
                orderCardInfo.ordertype = int.Parse(ds.Tables[0].Rows[0]["ordertype"].ToString());
            if (ds.Tables[0].Rows[0]["userid"].ToString() != "")
                orderCardInfo.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
            if (ds.Tables[0].Rows[0]["manageId"].ToString() != "")
                orderCardInfo.manageId = new int?(int.Parse(ds.Tables[0].Rows[0]["manageId"].ToString()));
            if (ds.Tables[0].Rows[0]["cardtype"].ToString() != "")
                orderCardInfo.cardType = int.Parse(ds.Tables[0].Rows[0]["cardtype"].ToString());
            if (ds.Tables[0].Rows[0]["typeId"].ToString() != "")
                orderCardInfo.typeId = int.Parse(ds.Tables[0].Rows[0]["typeId"].ToString());
            orderCardInfo.paymodeId = ds.Tables[0].Rows[0]["paymodeId"].ToString();
            orderCardInfo.userorder = ds.Tables[0].Rows[0]["userorder"].ToString();
            if (ds.Tables[0].Rows[0]["refervalue"].ToString() != "")
                orderCardInfo.refervalue = Decimal.Parse(ds.Tables[0].Rows[0]["refervalue"].ToString());
            if (ds.Tables[0].Rows[0]["realvalue"].ToString() != "")
                orderCardInfo.realvalue = new Decimal?(Decimal.Parse(ds.Tables[0].Rows[0]["realvalue"].ToString()));
            orderCardInfo.faceValue = new Decimal(0);
            if (ds.Tables[0].Rows[0]["faceValue"].ToString() != "")
                orderCardInfo.faceValue = Decimal.Parse(ds.Tables[0].Rows[0]["faceValue"].ToString());
            orderCardInfo.notifyurl = ds.Tables[0].Rows[0]["notifyurl"].ToString();
            orderCardInfo.againNotifyUrl = ds.Tables[0].Rows[0]["againNotifyUrl"].ToString();
            if (ds.Tables[0].Rows[0]["notifycount"].ToString() != "")
                orderCardInfo.notifycount = int.Parse(ds.Tables[0].Rows[0]["notifycount"].ToString());
            if (ds.Tables[0].Rows[0]["notifystat"].ToString() != "")
                orderCardInfo.notifystat = int.Parse(ds.Tables[0].Rows[0]["notifystat"].ToString());
            orderCardInfo.notifycontext = ds.Tables[0].Rows[0]["notifycontext"].ToString();
            orderCardInfo.returnurl = ds.Tables[0].Rows[0]["returnurl"].ToString();
            orderCardInfo.attach = ds.Tables[0].Rows[0]["attach"].ToString();
            orderCardInfo.payerip = ds.Tables[0].Rows[0]["payerip"].ToString();
            orderCardInfo.clientip = ds.Tables[0].Rows[0]["clientip"].ToString();
            orderCardInfo.referUrl = ds.Tables[0].Rows[0]["referUrl"].ToString();
            orderCardInfo.msg = ds.Tables[0].Rows[0]["msg"].ToString();
            orderCardInfo.userViewMsg = ds.Tables[0].Rows[0]["userViewMsg"].ToString();
            orderCardInfo.opstate = ds.Tables[0].Rows[0]["opstate"].ToString();
            if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                orderCardInfo.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
            if (ds.Tables[0].Rows[0]["supplierID"].ToString() != "")
                orderCardInfo.supplierId = int.Parse(ds.Tables[0].Rows[0]["supplierID"].ToString());
            orderCardInfo.supplierOrder = ds.Tables[0].Rows[0]["supplierOrder"].ToString();
            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                orderCardInfo.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
            if (ds.Tables[0].Rows[0]["completetime"].ToString() != "")
                orderCardInfo.completetime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["completetime"].ToString()));
            if (ds.Tables[0].Rows[0]["payRate"].ToString() != "")
                orderCardInfo.payRate = Decimal.Parse(ds.Tables[0].Rows[0]["payRate"].ToString());
            if (ds.Tables[0].Rows[0]["supplierRate"].ToString() != "")
                orderCardInfo.supplierRate = Decimal.Parse(ds.Tables[0].Rows[0]["supplierRate"].ToString());
            if (ds.Tables[0].Rows[0]["promRate"].ToString() != "")
                orderCardInfo.promRate = Decimal.Parse(ds.Tables[0].Rows[0]["promRate"].ToString());
            if (ds.Tables[0].Rows[0]["payAmt"].ToString() != "")
                orderCardInfo.payAmt = Decimal.Parse(ds.Tables[0].Rows[0]["payAmt"].ToString());
            if (ds.Tables[0].Rows[0]["promAmt"].ToString() != "")
                orderCardInfo.promAmt = Decimal.Parse(ds.Tables[0].Rows[0]["promAmt"].ToString());
            if (ds.Tables[0].Rows[0]["supplierAmt"].ToString() != "")
                orderCardInfo.supplierAmt = Decimal.Parse(ds.Tables[0].Rows[0]["supplierAmt"].ToString());
            if (ds.Tables[0].Rows[0]["profits"].ToString() != "")
                orderCardInfo.profits = Decimal.Parse(ds.Tables[0].Rows[0]["profits"].ToString());
            if (ds.Tables[0].Rows[0]["server"].ToString() != "")
                orderCardInfo.server = new int?(int.Parse(ds.Tables[0].Rows[0]["server"].ToString()));
            if (ds.Tables[0].Rows[0]["cardnum"].ToString() != "")
                orderCardInfo.cardnum = int.Parse(ds.Tables[0].Rows[0]["cardnum"].ToString());
            if (ds.Tables[0].Rows[0]["ismulticard"].ToString() != "")
                orderCardInfo.ismulticard = int.Parse(ds.Tables[0].Rows[0]["ismulticard"].ToString());
            orderCardInfo.resultcode = ds.Tables[0].Rows[0]["resultcode"].ToString();
            orderCardInfo.ovalue = ds.Tables[0].Rows[0]["ovalue"].ToString();
            orderCardInfo.version = ds.Tables[0].Rows[0]["version"].ToString();
            if (ds.Tables[0].Rows[0]["cus_subject"] != null && ds.Tables[0].Rows[0]["cus_subject"].ToString() != "")
                orderCardInfo.cus_subject = ds.Tables[0].Rows[0]["cus_subject"].ToString();
            if (ds.Tables[0].Rows[0]["cus_price"] != null && ds.Tables[0].Rows[0]["cus_price"].ToString() != "")
                orderCardInfo.cus_price = ds.Tables[0].Rows[0]["cus_price"].ToString();
            if (ds.Tables[0].Rows[0]["cus_quantity"] != null && ds.Tables[0].Rows[0]["cus_quantity"].ToString() != "")
                orderCardInfo.cus_quantity = ds.Tables[0].Rows[0]["cus_quantity"].ToString();
            if (ds.Tables[0].Rows[0]["cus_description"] != null && ds.Tables[0].Rows[0]["cus_description"].ToString() != "")
                orderCardInfo.cus_description = ds.Tables[0].Rows[0]["cus_description"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field1"] != null && ds.Tables[0].Rows[0]["cus_field1"].ToString() != "")
                orderCardInfo.cus_field1 = ds.Tables[0].Rows[0]["cus_field1"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field2"] != null && ds.Tables[0].Rows[0]["cus_field2"].ToString() != "")
                orderCardInfo.cus_field2 = ds.Tables[0].Rows[0]["cus_field2"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field3"] != null && ds.Tables[0].Rows[0]["cus_field3"].ToString() != "")
                orderCardInfo.cus_field3 = ds.Tables[0].Rows[0]["cus_field3"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field4"] != null && ds.Tables[0].Rows[0]["cus_field4"].ToString() != "")
                orderCardInfo.cus_field4 = ds.Tables[0].Rows[0]["cus_field4"].ToString();
            if (ds.Tables[0].Rows[0]["cus_field5"] != null && ds.Tables[0].Rows[0]["cus_field5"].ToString() != "")
                orderCardInfo.cus_field5 = ds.Tables[0].Rows[0]["cus_field5"].ToString();
            orderCardInfo.errtype = ds.Tables[0].Rows[0]["errtype"].ToString();
            if (ds.Tables[0].Rows[0]["agentid"] != null && ds.Tables[0].Rows[0]["agentid"].ToString() != "")
                orderCardInfo.agentId = int.Parse(ds.Tables[0].Rows[0]["agentid"].ToString());
            if (ds.Tables[0].Rows[0]["withhold"] != null && ds.Tables[0].Rows[0]["withhold"].ToString() != "")
                orderCardInfo.withhold_type = byte.Parse(ds.Tables[0].Rows[0]["withhold"].ToString());
            if (ds.Tables[0].Rows[0]["makeup"] != null && ds.Tables[0].Rows[0]["makeup"].ToString() != "")
                orderCardInfo.makeup = byte.Parse(ds.Tables[0].Rows[0]["makeup"].ToString());
            return orderCardInfo;
        }

        public OrderCardInfo GetModel(string orderId)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@orderid", SqlDbType.VarChar, 30)
            };
            sqlParameterArray[0].Value = (object)orderId;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_get", sqlParameterArray));
        }

        public OrderCardInfo GetModel(long id, int userid)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@id", SqlDbType.BigInt),
        new SqlParameter("@userid", SqlDbType.Int)
            };
            sqlParameterArray[0].Value = (object)id;
            sqlParameterArray[1].Value = (object)userid;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_GetModel", sqlParameterArray));
        }

        public OrderCardInfo GetModel(long id)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@id", SqlDbType.BigInt),
        new SqlParameter("@userid", SqlDbType.Int)
            };
            sqlParameterArray[0].Value = (object)id;
            sqlParameterArray[1].Value = (object)DBNull.Value;
            return this.GetModelFromDs(DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercard_GetModel", sqlParameterArray));
        }

        public CardItemInfo GetItemModel(string orderId, int serial)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
            {
        new SqlParameter("@orderId", SqlDbType.NVarChar, 30),
        new SqlParameter("@serial", SqlDbType.Int, 4)
            };
            sqlParameterArray[0].Value = (object)orderId;
            sqlParameterArray[1].Value = (object)serial;
            CardItemInfo cardItemInfo = new CardItemInfo();
            DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercarditem_GetModel", sqlParameterArray);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return (CardItemInfo)null;
            if (dataSet.Tables[0].Rows[0]["id"].ToString() != "")
                cardItemInfo.id = long.Parse(dataSet.Tables[0].Rows[0]["id"].ToString());
            if (dataSet.Tables[0].Rows[0]["userid"].ToString() != "")
                cardItemInfo.userid = int.Parse(dataSet.Tables[0].Rows[0]["userid"].ToString());
            if (dataSet.Tables[0].Rows[0]["serial"].ToString() != "")
                cardItemInfo.serial = int.Parse(dataSet.Tables[0].Rows[0]["serial"].ToString());
            cardItemInfo.porderid = dataSet.Tables[0].Rows[0]["porderid"].ToString();
            if (dataSet.Tables[0].Rows[0]["suppid"].ToString() != "")
                cardItemInfo.suppid = int.Parse(dataSet.Tables[0].Rows[0]["suppid"].ToString());
            if (dataSet.Tables[0].Rows[0]["cardtype"].ToString() != "")
                cardItemInfo.cardtype = int.Parse(dataSet.Tables[0].Rows[0]["cardtype"].ToString());
            cardItemInfo.cardno = dataSet.Tables[0].Rows[0]["cardno"].ToString();
            cardItemInfo.cardpwd = dataSet.Tables[0].Rows[0]["cardpwd"].ToString();
            if (dataSet.Tables[0].Rows[0]["refervalue"].ToString() != "")
                cardItemInfo.refervalue = new Decimal?(Decimal.Parse(dataSet.Tables[0].Rows[0]["refervalue"].ToString()));
            if (dataSet.Tables[0].Rows[0]["payrate"].ToString() != "")
                cardItemInfo.payrate = new Decimal?(Decimal.Parse(dataSet.Tables[0].Rows[0]["payrate"].ToString()));
            if (dataSet.Tables[0].Rows[0]["addtime"].ToString() != "")
                cardItemInfo.addtime = DateTime.Parse(dataSet.Tables[0].Rows[0]["addtime"].ToString());
            cardItemInfo.supplierOrder = dataSet.Tables[0].Rows[0]["supplierOrder"].ToString();
            if (dataSet.Tables[0].Rows[0]["realvalue"].ToString() != "")
                cardItemInfo.realvalue = Decimal.Parse(dataSet.Tables[0].Rows[0]["realvalue"].ToString());
            if (dataSet.Tables[0].Rows[0]["status"].ToString() != "")
                cardItemInfo.status = int.Parse(dataSet.Tables[0].Rows[0]["status"].ToString());
            cardItemInfo.opstate = dataSet.Tables[0].Rows[0]["opstate"].ToString();
            cardItemInfo.msg = dataSet.Tables[0].Rows[0]["msg"].ToString();
            if (dataSet.Tables[0].Rows[0]["completetime"].ToString() != "")
                cardItemInfo.completetime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["completetime"].ToString()));
            if (dataSet.Tables[0].Rows[0]["supplierrate"].ToString() != "")
                cardItemInfo.supplierrate = Decimal.Parse(dataSet.Tables[0].Rows[0]["supplierrate"].ToString());
            if (dataSet.Tables[0].Rows[0]["promrate"].ToString() != "")
                cardItemInfo.promrate = Decimal.Parse(dataSet.Tables[0].Rows[0]["promrate"].ToString());
            if (dataSet.Tables[0].Rows[0]["commission"].ToString() != "")
                cardItemInfo.commission = Decimal.Parse(dataSet.Tables[0].Rows[0]["commission"].ToString());
            if (dataSet.Tables[0].Rows[0]["agent"].ToString() != "")
                cardItemInfo.agentId = int.Parse(dataSet.Tables[0].Rows[0]["agent"].ToString());
            return cardItemInfo;
        }

        public DataTable DataItemsByOrderId(string orderId)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
            {
        new SqlParameter("@orderId", SqlDbType.NVarChar, 30)
            };
            sqlParameterArray[0].Value = (object)orderId;
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_ordercarditem_Getlistbyorderid", sqlParameterArray).Tables[0];
        }

        public bool Notify(OrderCardInfo model)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[6]
            {
        new SqlParameter("@orderid", SqlDbType.VarChar, 20),
        new SqlParameter("@againNotifyUrl", SqlDbType.NVarChar, 2000),
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
            sqlParameterArray[5].Value = (object)DateTime.Now;
            return DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ordercard_notify", sqlParameterArray) > 0;
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "v_ordercard";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "[id] desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = OrderCard.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[orderid]\r\n      ,[ordertype]\r\n      ,[userid]\r\n      ,[typeId]\r\n      ,[paymodeId]\r\n      ,[userorder]\r\n      ,[refervalue]\r\n      ,[realvalue]\r\n      ,[notifyurl]\r\n      ,[againNotifyUrl]\r\n      ,[notifycount]\r\n      ,[notifystat]\r\n      ,[notifycontext]\r\n      ,[notifytime]\r\n      ,[returnurl]\r\n      ,[attach]\r\n      ,[payerip]\r\n      ,[clientip]\r\n      ,[referUrl]\r\n      ,[addtime]\r\n      ,[supplierID]\r\n      ,[supplierOrder]\r\n      ,[status]\r\n      ,[completetime]\r\n      ,[payRate]\r\n      ,[supplierRate]\r\n      ,[promRate]\r\n      ,[payAmt]\r\n      ,[promAmt]\r\n      ,[supplierAmt]\r\n      ,[profits]\r\n      ,[server]\r\n      ,[modetypename]\r\n      ,[modeName]\r\n      ,[cardNo]\r\n      ,[cardPwd]\r\n      ,[desc]\r\n      ,[manageId]\r\n      ,[msg]\r\n      ,[commission]\r\n      ,[cardnum]\r\n      ,[resultcode]\r\n      ,[ismulticard]\r\n      ,[version],cus_subject,cus_price,cus_quantity,cus_description,cus_field1,cus_field2,cus_field3,cus_field4,cus_field5,errtype,agentid,faceValue", tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect sum(1) ordtotal,sum(case when [status]=2 then 1 else 0 end) succordtotal,sum(refervalue) refervalue,sum(realvalue) realvalue,sum(case when [status]=2 then payAmt else 0 end) payAmt,sum(isnull(promAmt,0)) promAmt,sum(supplierAmt-(case when [status]=2 then payAmt else 0 end)) profits,sum(promAmt) promAmt,sum(commission) commission from V_ordercard where " + wheres, paramList.ToArray());
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
                            case "userorder":
                                stringBuilder.Append(" AND [userorder] like @userorder");
                                SqlParameter sqlParameter5 = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                                sqlParameter5.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter5);
                                continue;
                            case "orderid":
                                stringBuilder.Append(" AND [orderid] like @orderid");
                                SqlParameter sqlParameter6 = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                sqlParameter6.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter6);
                                continue;
                            case "orderid_like":
                                stringBuilder.Append(" AND [orderid] like @orderid");
                                SqlParameter sqlParameter7 = new SqlParameter("@orderid", SqlDbType.VarChar, 30);
                                sqlParameter7.Value = (object)(SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter7);
                                continue;
                            case "cardno":
                                stringBuilder.Append(" AND [cardNo] like @cardno");
                                SqlParameter sqlParameter8 = new SqlParameter("@cardno", SqlDbType.NVarChar, 50);
                                sqlParameter8.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 50) + "%");
                                paramList.Add(sqlParameter8);
                                continue;
                            case "supplierorder":
                                stringBuilder.Append(" AND [supplierOrder] like @supplierOrder");
                                SqlParameter sqlParameter9 = new SqlParameter("@supplierOrder", SqlDbType.VarChar, 30);
                                sqlParameter9.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
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
                                stringBuilder.Append(" AND ([notifystat] = @notifystat AND ordertype <> 8)");
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
            return stringBuilder.ToString();
        }

        public DataSet ItemPageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "v_ordercard";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "[id] desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = OrderCard.ItemBuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[orderid]\r\n      ,[ordertype]\r\n      ,[userid]\r\n      ,[typeId]\r\n      ,[paymodeId]\r\n      ,[userorder]\r\n      ,[refervalue]\r\n      ,[realvalue]\r\n      ,[notifyurl]\r\n      ,[againNotifyUrl]\r\n      ,[notifycount]\r\n      ,[notifystat]\r\n      ,[notifycontext]\r\n      ,[notifytime]\r\n      ,[returnurl]\r\n      ,[attach]\r\n      ,[payerip]\r\n      ,[clientip]\r\n      ,[referUrl]\r\n      ,[addtime]\r\n      ,[supplierID]\r\n      ,[supplierOrder]\r\n      ,[status]\r\n      ,[completetime]\r\n      ,[payRate]\r\n      ,[supplierRate]\r\n      ,[promRate]\r\n      ,[payAmt]\r\n      ,[promAmt]\r\n      ,[supplierAmt]\r\n      ,[profits]\r\n      ,[server]\r\n      ,[modetypename]\r\n      ,[modeName]\r\n      ,[cardNo]\r\n      ,[cardPwd]\r\n      ,[desc]\r\n      ,[manageId]\r\n      ,[msg]\r\n      ,[commission]\r\n      ,[cardnum]\r\n      ,[resultcode]\r\n      ,[ismulticard]\r\n      ,[version],cus_subject,cus_price,cus_quantity,cus_description,cus_field1,cus_field2,cus_field3,cus_field4,cus_field5,errtype,agentid,faceValue", tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect sum(refervalue) refervalue,sum(case when [status]=2 then realvalue else 0 end) realvalue,sum(case when [status]=2 then payAmt else 0 end) payAmt,sum(supplierAmt-(case when [status]=2 then payAmt else 0 end)) profits,sum(promAmt) promAmt from V_ordercard where " + wheres, paramList.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return dataSet;
            }
        }

        private static string ItemBuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
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
                            case "manageid":
                                stringBuilder.Append(" AND [manageId] = @manageId");
                                SqlParameter sqlParameter2 = new SqlParameter("@manageId", SqlDbType.Int);
                                sqlParameter2.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter2);
                                continue;
                            case "typeId":
                                stringBuilder.Append(" AND [typeId] = @typeId");
                                SqlParameter sqlParameter3 = new SqlParameter("@typeId", SqlDbType.Int);
                                sqlParameter3.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter3);
                                continue;
                            case "userorder":
                                stringBuilder.Append(" AND [userorder] like @userorder");
                                SqlParameter sqlParameter4 = new SqlParameter("@userorder", SqlDbType.VarChar, 30);
                                sqlParameter4.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter4);
                                continue;
                            case "cardno":
                                stringBuilder.Append(" AND [cardNo] like @cardno");
                                SqlParameter sqlParameter5 = new SqlParameter("@cardno", SqlDbType.NVarChar, 50);
                                sqlParameter5.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 50) + "%");
                                paramList.Add(sqlParameter5);
                                continue;
                            case "supplierorder":
                                stringBuilder.Append(" AND [supplierOrder] like @supplierOrder");
                                SqlParameter sqlParameter6 = new SqlParameter("@supplierOrder", SqlDbType.VarChar, 30);
                                sqlParameter6.Value = (object)("%" + SqlHelper.CleanString((string)searchParam.ParamValue, 30) + "%");
                                paramList.Add(sqlParameter6);
                                continue;
                            case "status":
                                stringBuilder.Append(" AND [status] = @status");
                                SqlParameter sqlParameter7 = new SqlParameter("@status", SqlDbType.TinyInt);
                                sqlParameter7.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter7);
                                continue;
                            case "statusallfail":
                                stringBuilder.Append(" AND ([status] = 4 or  [status] = 8)");
                                continue;
                            case "notifystat":
                                stringBuilder.Append(" AND [notifystat] = @notifystat");
                                SqlParameter sqlParameter8 = new SqlParameter("@notifystat", SqlDbType.TinyInt);
                                sqlParameter8.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter8);
                                continue;
                            case "promid":
                                stringBuilder.Append(" AND exists(select 0 from PromotionUser where PromotionUser.PID = @promid and PromotionUser.RegId=userid)");
                                SqlParameter sqlParameter9 = new SqlParameter("@promid", SqlDbType.Int);
                                sqlParameter9.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter9);
                                continue;
                            case "stime":
                                stringBuilder.Append(" AND [addtime] >= @stime");
                                SqlParameter sqlParameter10 = new SqlParameter("@stime", SqlDbType.DateTime);
                                sqlParameter10.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter10);
                                continue;
                            case "etime":
                                stringBuilder.Append(" AND [addtime] <= @etime");
                                SqlParameter sqlParameter11 = new SqlParameter("@etime", SqlDbType.DateTime);
                                sqlParameter11.Value = searchParam.ParamValue;
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
