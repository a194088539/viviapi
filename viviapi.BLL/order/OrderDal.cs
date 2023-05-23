using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using viviapi.Model.Order;
using viviapi.Model.User;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order
{
    public class Dal
    {
        internal const string SQL_TABLE = "V_usersOrderIncome";
        internal const string FIELDS = "[id]\r\n      ,[mydate]\r\n      ,[typeId]\r\n      ,[modetypename]\r\n      ,[faceValue]\r\n      ,[payrate]\r\n      ,[s_num]\r\n      ,[userId]\r\n      ,[Username]\r\n      ,[full_name]\r\n      ,[sumpay]";

        public static bool Update(string orderid, string bankcode, int suppid)
        {
            try
            {
                string commandText = "update orderbank set supplierId = @supplierId,paymodeId=@paymodeId where orderid = @orderid";
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          new SqlParameter("@orderid", SqlDbType.VarChar, 20),
          new SqlParameter("@paymodeId", SqlDbType.VarChar, 10),
          new SqlParameter("@supplierId", SqlDbType.Int, 4)
        };
                sqlParameterArray[0].Value = (object)orderid;
                sqlParameterArray[1].Value = (object)bankcode;
                sqlParameterArray[2].Value = (object)suppid;
                return DataBase.ExecuteNonQuery(CommandType.Text, commandText, sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool UpdateCardOrderStatus(string orderid)
        {
            try
            {
                string commandText = "update ordercardamt set [status] = 1 where orderid=@orderid and [status] = 4";
                SqlParameter[] sqlParameterArray = new SqlParameter[1]
        {
          new SqlParameter("@orderid", SqlDbType.VarChar, 30)
        };
                sqlParameterArray[0].Value = (object)orderid;
                return DataBase.ExecuteNonQuery(CommandType.Text, commandText, sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool UpdateCardOrderStatus(string orderid, int suppid, Decimal refervalue)
        {
            try
            {
                string commandText = "declare @status tinyint\r\nset @status = 1\r\nselect @status = [status] from ordercardamt with(nolock) where orderid=@orderid\r\nset @status = isnull(@status,1)\r\n\r\nif (@status = 1 or @status = 4)\r\nbegin\r\n\tupdate ordercard set supplierID = @supplierID,refervalue=@refervalue where orderid=@orderid\r\n\tupdate ordercardamt set [status] = 1 where orderid=@orderid\r\nend";
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          new SqlParameter("@orderid", SqlDbType.VarChar, 30),
          new SqlParameter("@supplierID", SqlDbType.Int, 4),
          new SqlParameter("@refervalue", SqlDbType.Decimal, 9)
        };
                sqlParameterArray[0].Value = (object)orderid;
                sqlParameterArray[1].Value = (object)suppid;
                sqlParameterArray[2].Value = (object)refervalue;
                return DataBase.ExecuteNonQuery(CommandType.Text, commandText, sqlParameterArray) > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static int BankOrder_DataCheck(int intput_userid, bool ischeckuserorder, string intput_userorder, out UserInfo userInfo)
        {
            userInfo = (UserInfo)null;
            int num;
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
        {
          new SqlParameter("@intput_userid", SqlDbType.Int, 4),
          new SqlParameter("@ischeckuserorder", SqlDbType.Bit),
          new SqlParameter("@intput_userorder", SqlDbType.VarChar, 30),
          new SqlParameter("@result", SqlDbType.TinyInt)
        };
                sqlParameterArray[0].Value = (object)intput_userid;
                sqlParameterArray[1].Value = (object)(bool)(ischeckuserorder);
                sqlParameterArray[2].Value = (object)intput_userorder;
                sqlParameterArray[3].Direction = ParameterDirection.Output;
                DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_orderbank_checkdata", sqlParameterArray);
                num = Convert.ToInt32(sqlParameterArray[3].Value);
                if (num == 0 && dataSet != null && dataSet.Tables.Count > 0)
                {
                    userInfo = new UserInfo();
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    userInfo.ID = intput_userid;
                    userInfo.APIKey = dataRow["apikey"].ToString();
                    if (dataRow["isdebug"] != DBNull.Value)
                        userInfo.isdebug = Convert.ToInt32(dataRow["isdebug"]);
                    if (dataRow["manageId"] != DBNull.Value)
                        userInfo.manageId = new int?(Convert.ToInt32(dataRow["manageId"]));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                num = 99;
            }
            return num;
        }

        public static DataTable OrderSearch(int userId, string userorder)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
        {
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@userorder", SqlDbType.VarChar, 30)
        };
                sqlParameterArray[0].Value = (object)userId;
                sqlParameterArray[1].Value = (object)userorder;
                DataSet dataSet = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_order_search", sqlParameterArray);
                if (dataSet != null && dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
                return (DataTable)null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public static DataTable GetUserOrderCount(int userId, string begin, string edate)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@userId", SqlDbType.Int, 4),
        new SqlParameter("@begin", SqlDbType.VarChar, 10),
        new SqlParameter("@end", SqlDbType.VarChar, 10)
      };
            sqlParameterArray[0].Value = (object)userId;
            sqlParameterArray[1].Value = (object)begin;
            sqlParameterArray[2].Value = (object)edate;
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_stat_user_ordercount_bychannel", sqlParameterArray).Tables[0];
        }

        public static DataTable GetUserDayAmt(int userId, string begin, string edate)
        {
            SqlParameter[] sqlParameterArray = new SqlParameter[3]
      {
        new SqlParameter("@userId", SqlDbType.Int, 4),
        new SqlParameter("@begin", SqlDbType.VarChar, 10),
        new SqlParameter("@end", SqlDbType.VarChar, 10)
      };
            sqlParameterArray[0].Value = (object)userId;
            sqlParameterArray[1].Value = (object)begin;
            sqlParameterArray[2].Value = (object)edate;
            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_stat_user_order_getdaymoney", sqlParameterArray).Tables[0];
        }

        public static CheckCardResult CheckCardRepeat(int userid, string userorderid, int cardtype, string cardno, string cardpwd)
        {
            CheckCardResult checkCardResult = new CheckCardResult();
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[5]
        {
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@cardtype", SqlDbType.Int, 4),
          new SqlParameter("@cardNo", SqlDbType.NVarChar, 100),
          new SqlParameter("@cardPwd", SqlDbType.NVarChar, 100),
          new SqlParameter("@userorder", SqlDbType.NVarChar, 30)
        };
                sqlParameterArray[0].Value = (object)userid;
                sqlParameterArray[1].Value = (object)cardtype;
                sqlParameterArray[2].Value = (object)cardno;
                sqlParameterArray[3].Value = (object)cardpwd;
                sqlParameterArray[4].Value = (object)userorderid;
                DataTable dataTable = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_checkcard_repeat", sqlParameterArray).Tables[0];
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];
                    checkCardResult.isRepeat = Convert.ToByte(dataRow["repeat"]);
                    checkCardResult.makeup = Convert.ToByte(dataRow["makeup"]);
                    checkCardResult.withhold = Convert.ToDecimal(dataRow["withhold"]);
                    checkCardResult.cardpwd = Convert.ToString(dataRow["cardpwd"]);
                    checkCardResult.supprate = Convert.ToDecimal(dataRow["supprate"]);
                    checkCardResult.supplierid = Convert.ToInt32(dataRow["supplierid"]);
                }
                else
                    checkCardResult = (CheckCardResult)null;
                return checkCardResult;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (CheckCardResult)null;
            }
        }

        public static int CheckCardRepeat2(int userid, string userorderid, int cardtype, string cardno, string cardpwd)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
        {
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@cardtype", SqlDbType.Int, 4),
          new SqlParameter("@cardNo", SqlDbType.NVarChar, 100),
          new SqlParameter("@userorder", SqlDbType.NVarChar, 30)
        };
                sqlParameterArray[0].Value = (object)userid;
                sqlParameterArray[1].Value = (object)cardtype;
                sqlParameterArray[2].Value = (object)cardno;
                sqlParameterArray[3].Value = (object)userorderid;
                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_checkcard_repeat2", sqlParameterArray));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string tables = "V_usersOrderIncome";
                string key = "[id]";
                if (string.IsNullOrEmpty(orderby))
                    orderby = "id desc";
                List<SqlParameter> paramList = new List<SqlParameter>();
                string wheres = Dal.BuilderWhere(searchParams, paramList);
                return DataBase.ExecuteDataset(CommandType.Text, SqlHelper.GetCountSQL(tables, wheres, string.Empty) + "\r\n" + SqlHelper.GetPageSelectSQL("[id]\r\n      ,[mydate]\r\n      ,[typeId]\r\n      ,[modetypename]\r\n      ,[faceValue]\r\n      ,[payrate]\r\n      ,[s_num]\r\n      ,[userId]\r\n      ,[Username]\r\n      ,[full_name]\r\n      ,[sumpay]", tables, wheres, orderby, key, pageSize, page, false) + "\r\nselect sum(sumpay) sumpay,sum(s_num) s_num from V_usersOrderIncome where " + wheres, paramList.ToArray());
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
                            case "stime":
                                stringBuilder.Append(" AND [mydate] >= @beginmydate");
                                SqlParameter sqlParameter2 = new SqlParameter("@beginmydate", SqlDbType.VarChar, 10);
                                sqlParameter2.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter2);
                                continue;
                            case "etime":
                                stringBuilder.Append(" AND [mydate] <= @endmydate");
                                SqlParameter sqlParameter3 = new SqlParameter("@endmydate", SqlDbType.VarChar, 10);
                                sqlParameter3.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter3);
                                continue;
                            case "fvaluefrom":
                                stringBuilder.Append(" AND [faceValue] >= @fvaluefrom");
                                SqlParameter sqlParameter4 = new SqlParameter("@fvaluefrom", SqlDbType.Decimal, 9);
                                sqlParameter4.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter4);
                                continue;
                            case "fvalueto":
                                stringBuilder.Append(" AND [faceValue] <= @fvalueto");
                                SqlParameter sqlParameter5 = new SqlParameter("@fvalueto", SqlDbType.Decimal, 9);
                                sqlParameter5.Value = searchParam.ParamValue;
                                paramList.Add(sqlParameter5);
                                continue;
                            case "typeid":
                                stringBuilder.Append(" AND [typeId] = @typeId");
                                SqlParameter sqlParameter6 = new SqlParameter("@typeId", SqlDbType.Int);
                                sqlParameter6.Value = (object)(int)searchParam.ParamValue;
                                paramList.Add(sqlParameter6);
                                continue;
                            default:
                                continue;
                        }
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public static Decimal GetAgentTotalAmt(int agentid, DateTime sdt, DateTime edt)
        {
            Decimal num = new Decimal(0);
            try
            {
                string commandText = "declare @amt decimal(18,4)\r\nselect @amt = sum(realvalue) from v_orderbank \r\nwhere agentid=@agentid and status = 2 and processingtime > @begintime and processingtime < @endtime\r\n\r\nselect @amt = isnull(@amt,0)+ isnull(sum(realvalue),0) from v_order where agentid=@agentid and status = 2\r\n and processingtime > @begintime and processingtime < @endtime\r\n\r\nselect isnull(@amt,0)";
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          new SqlParameter("@agentid", SqlDbType.Int, 4),
          new SqlParameter("@begintime", SqlDbType.DateTime, 8),
          new SqlParameter("@endtime", SqlDbType.DateTime, 8)
        };
                sqlParameterArray[0].Value = (object)agentid;
                sqlParameterArray[1].Value = (object)sdt;
                sqlParameterArray[2].Value = (object)edt;
                object obj = DataBase.ExecuteScalar(CommandType.Text, commandText, sqlParameterArray);
                if (obj == DBNull.Value)
                    return new Decimal(0);
                num = Convert.ToDecimal(obj);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return num;
        }

        public static Decimal GetAgentIncome(int agentid, DateTime sdt, DateTime edt)
        {
            Decimal num = new Decimal(0);
            try
            {
                string commandText = "select sum(amt) from trade where billType = 2 and userid = @userid and tradeTime >= @begintime and tradeTime < @endtime";
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          new SqlParameter("@userid", SqlDbType.Int, 4),
          new SqlParameter("@begintime", SqlDbType.DateTime, 8),
          new SqlParameter("@endtime", SqlDbType.DateTime, 8)
        };
                sqlParameterArray[0].Value = (object)agentid;
                sqlParameterArray[1].Value = (object)sdt;
                sqlParameterArray[2].Value = (object)edt;
                object obj = DataBase.ExecuteScalar(CommandType.Text, commandText, sqlParameterArray);
                if (obj == DBNull.Value)
                    return new Decimal(0);
                num = Convert.ToDecimal(obj);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            return num;
        }

        public static DataTable GetFailOrders()
        {
            return DataBase.ExecuteDataset(CommandType.Text, "select orderid from ordercard with(nolock) where typeid=106 and supplierID = 85\r\nand not exists(select 0 from ordercardamt with(nolock) where ordercard.orderid = ordercardamt.orderid)", (SqlParameter[])null).Tables[0];
        }

        public static DataTable GetFailOrders2(DateTime sdt, DateTime edt)
        {
            string commandText = "select userid,ordercard.typeId,channeltype.modetypename,supplierID,orderid,cardNo,refervalue,processingtime from ordercard with(nolock), channeltype \r\nwhere \r\n\tordercard.typeId=channeltype.typeId\r\nand\r\n\tprocessingtime >= @sdt and processingtime <= @edt\r\nand\r\n    makeup=0\r\nand not exists(select 0 from ordercardamt with(nolock) where ordercardamt.orderid = ordercard.orderid)";
            SqlParameter[] sqlParameterArray = new SqlParameter[2]
      {
        new SqlParameter("@sdt", SqlDbType.DateTime, 8),
        new SqlParameter("@edt", SqlDbType.DateTime, 8)
      };
            sqlParameterArray[0].Value = (object)sdt;
            sqlParameterArray[1].Value = (object)edt;
            return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray).Tables[0];
        }

        public static DataTable Stat(int suppid, DateTime sdt, DateTime edt)
        {
            try
            {
                string commandText = "select b.typeId,c.modetypename,sum(realvalue) realvalue,sum(supplierAmt) supplierAmt,sum(payAmt) payAmt,sum(profits) profits \r\nfrom ordercardamt a with(nolock) \r\n\t\tleft join ordercard b with(nolock) on a.orderid = b.orderid\r\n\t\tleft join channeltype c with(nolock) on b.typeId = c.typeId\r\n\t\tleft join supplier d with(nolock) on b.supplierID = d.id\r\nwhere (a.[status] = 2 or a.[status] = 8)\r\nand (b.supplierID = @suppid or @suppid is null)\r\nand a.completetime >= @begindt\r\nand a.completetime < @enddt\r\ngroup by b.typeId,c.modetypename";
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          new SqlParameter("@suppid", SqlDbType.Int, 4),
          new SqlParameter("@begindt", SqlDbType.DateTime, 8),
          new SqlParameter("@enddt", SqlDbType.DateTime, 8)
        };
                sqlParameterArray[0].Value = (object)suppid;
                if (suppid == 0)
                    sqlParameterArray[0].Value = (object)DBNull.Value;
                sqlParameterArray[1].Value = (object)sdt;
                sqlParameterArray[2].Value = (object)edt;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public static DataTable StatForBusiness(int manageId, DateTime sdt, DateTime edt)
        {
            try
            {
                string commandText = "select typeId,modetypename,sum(realvalue) as realvalue,sum(supplierAmt) supplierAmt,sum(commission) commission,sum(payAmt) payAmt\r\nfrom v_order\r\nwhere ([status] = 2 or [status] = 8)\r\nand (manageId = @manageId)\r\nand processingtime >= @begindt\r\nand processingtime < @enddt\r\ngroup by typeId,modetypename";
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          new SqlParameter("@manageId", SqlDbType.Int, 4),
          new SqlParameter("@begindt", SqlDbType.DateTime, 8),
          new SqlParameter("@enddt", SqlDbType.DateTime, 8)
        };
                sqlParameterArray[0].Value = (object)manageId;
                sqlParameterArray[1].Value = (object)sdt;
                sqlParameterArray[2].Value = (object)edt;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public static DataTable StatForAgent(int agentid, DateTime sdt, DateTime edt)
        {
            try
            {
                string commandText = "select typeId,modetypename,sum(realvalue) as realvalue,sum(supplierAmt) supplierAmt,sum(promAmt) promAmt,sum(payAmt) payAmt\r\nfrom v_order\r\nwhere ([status] = 2 or [status] = 8)\r\nand (agentid = @agentid)\r\nand processingtime >= @begindt\r\nand processingtime < @enddt\r\ngroup by typeId,modetypename";
                SqlParameter[] sqlParameterArray = new SqlParameter[3]
        {
          new SqlParameter("@agentid", SqlDbType.Int, 4),
          new SqlParameter("@begindt", SqlDbType.DateTime, 8),
          new SqlParameter("@enddt", SqlDbType.DateTime, 8)
        };
                sqlParameterArray[0].Value = (object)agentid;
                sqlParameterArray[1].Value = (object)sdt;
                sqlParameterArray[2].Value = (object)edt;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public static DataSet AgentStat2(DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            try
            {
                string commandText = "\r\nselect count(0) as C\r\n\tfrom(\r\n\tselect agentid\r\n\tfrom v_order with(nolock)\r\n\twhere agentid > 0 and promAmt > 0 and processingtime >= @sdt and processingtime < @edt \r\n\tgroup by agentid) A\r\n\r\n\r\nselect D1.agentid,payAmt,promAmt,supplierAmt,realvalue,B.username,B.full_name\r\nfrom(\r\n\tselect agentid,payAmt,promAmt,supplierAmt,realvalue,ROW_NUMBER() OVER(ORDER BY D.agentid) AS P_ROW \r\n\tfrom(\r\n\tselect agentid,sum(payAmt) payAmt,sum(promAmt) as promAmt,sum(supplierAmt) as supplierAmt,sum(realvalue) as realvalue\r\n\tfrom v_order with(nolock)\r\n\twhere agentid > 0 and promAmt > 0 and processingtime >= @sdt and processingtime < @edt  \r\n\tgroup by agentid) D \r\n)D1  left join userbase B with(nolock)  on D1.agentid = B.id\r\nWHERE D1.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize\r\norder by " + orderby;
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
        {
          new SqlParameter("@sdt", SqlDbType.DateTime, 8),
          new SqlParameter("@edt", SqlDbType.DateTime, 8),
          new SqlParameter("@page", SqlDbType.Int, 4),
          new SqlParameter("@pagesize", SqlDbType.Int, 4)
        };
                sqlParameterArray[0].Value = (object)sdt;
                sqlParameterArray[1].Value = (object)edt;
                sqlParameterArray[2].Value = (object)page;
                sqlParameterArray[3].Value = (object)pagesize;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public static DataSet AgentStat3(int agentid, DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            try
            {
                string commandText = "\r\nselect count(0) as C\r\n\t\tfrom orderbankamt a with(nolock)\r\n\t\t\t\tleft join orderbank b with(nolock) \r\n\t\t\t\t\t\t\ton a.orderid = b.orderid\t\t\r\n\t\twhere a.[status] = 2 and b.agentid > 0 and processingtime >= @begintime and processingtime <= @endtime and (b.agentid \r\n\t\tgroup by b.agentid\r\n\r\n\r\nselect \r\n\ta.agentid\r\n\t,userName\r\n\t,tradeAmt\r\n\t,promAmt\r\n\t,profits\r\n\t,lowercount = (select count(0) from PromotionUser where PID = a.agentid)\r\n\t,ROW_NUMBER() OVER(ORDER BY a.agentid) AS P_ROW\r\nfrom \r\n\t(\r\n\t\tselect b.agentid,sum(realvalue) as tradeAmt,sum(promAmt) as promAmt,sum(profits) as profits\r\n\t\tfrom orderbankamt a with(nolock)\r\n\t\t\t\tleft join orderbank b with(nolock) \r\n\t\t\t\t\t\t\ton a.orderid = b.orderid\t\t\r\n\t\twhere a.[status] = 2 and b.agentid > 0 and processingtime >= @begintime and processingtime <= @endtime and (b.agentid = @userid or @userid = 0)\r\n\t\tgroup by b.agentid\r\n\t) a \r\n\tleft join userbase b with(nolock) \r\n\t\t\t\t\ton a.agentid = b.id\r\nwhere a.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize";
                SqlParameter[] sqlParameterArray = new SqlParameter[5]
        {
          new SqlParameter("@begintime", SqlDbType.DateTime, 8),
          new SqlParameter("@endtime", SqlDbType.DateTime, 8),
          new SqlParameter("@page", SqlDbType.Int, 4),
          new SqlParameter("@pagesize", SqlDbType.Int, 4),
          new SqlParameter("@userid", SqlDbType.Int, 4)
        };
                sqlParameterArray[0].Value = (object)sdt;
                sqlParameterArray[1].Value = (object)edt;
                sqlParameterArray[2].Value = (object)page;
                sqlParameterArray[3].Value = (object)pagesize;
                sqlParameterArray[4].Value = (object)agentid;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public static DataSet AgentStat4(int userid, int typeid, string sdt, string edt, int pagesize, int page, string orderby)
        {
            try
            {
                string commandText = "\r\nselect count(*) C from\r\n(select mydate,typeId,userId\r\nfrom usersOrderIncome with(nolock)\r\nwhere 1=1\r\nand (mydate >= @sdate or @sdate = '')\r\nand (mydate >= @edate or @edate = '')\r\nand (userId = @userId or @userId = 0)\r\nand (typeId = @typeId or @typeId = 0)\r\ngroup by mydate,typeId,userId) a\r\n\r\nselect mydate,userId,username,full_name,d.typeId,sumpay,pecent,P_ROW,f.modetypename\r\nfrom(\r\nselect a.mydate,a.userId,c.username,c.full_name,a.typeId,a.sumpay,a.sumpay/b.total pecent,ROW_NUMBER() OVER(ORDER BY a.mydate) AS P_ROW\r\nfrom \r\n(select mydate, userId, typeId,sum(sumpay) as sumpay\r\nfrom usersOrderIncome with(nolock)\r\nwhere 1=1\r\nand (mydate >= @sdate or @sdate = '')\r\nand (mydate >= @edate or @edate = '')\r\nand (userId = @userId or @userId = 0)\r\nand (typeId = @typeId or @typeId = 0)\r\ngroup by mydate,typeId,userId ) a\r\nleft join (select mydate, userId,sum(sumpay) as total\r\nfrom usersOrderIncome with(nolock)\r\nwhere 1=1\r\nand (mydate >= @sdate or @sdate = '')\r\nand (mydate >= @edate or @edate = '')\r\nand (userId = @userId or @userId = 0)\r\nand (typeId = @typeId or @typeId = 0)\r\ngroup by mydate,userId ) b on a.mydate=b.mydate\r\nand a.userId = b.userId\r\nleft join userbase c on a.userId = c.id) d\r\nleft join channeltype f ON d.typeId = f.typeId\r\nwhere P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize";
                SqlParameter[] sqlParameterArray = new SqlParameter[6]
        {
          new SqlParameter("@sdate", SqlDbType.VarChar, 10),
          new SqlParameter("@edate", SqlDbType.VarChar, 10),
          new SqlParameter("@userId", SqlDbType.Int, 4),
          new SqlParameter("@typeId", SqlDbType.Int, 4),
          new SqlParameter("@page", SqlDbType.Int, 4),
          new SqlParameter("@pagesize", SqlDbType.Int, 4)
        };
                sqlParameterArray[0].Value = (object)sdt;
                sqlParameterArray[1].Value = (object)edt;
                sqlParameterArray[2].Value = (object)userid;
                sqlParameterArray[3].Value = (object)typeid;
                sqlParameterArray[4].Value = (object)page;
                sqlParameterArray[5].Value = (object)pagesize;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public static DataSet BusinessStat4(DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            try
            {
                string commandText = "\r\nselect count(0) as C\r\n\tfrom(\r\n\tselect manageid\r\n\tfrom v_order with(nolock)\r\n\twhere manageid > 0  and processingtime >= @sdt and processingtime <= @edt \r\n\tgroup by manageid) A\r\n\r\n\r\nselect D1.manageid,payAmt,promAmt,supplierAmt,realvalue,B.username,B.relname\r\nfrom(\r\n\tselect manageid,payAmt,promAmt,supplierAmt,realvalue,ROW_NUMBER() OVER(ORDER BY D.manageid) AS P_ROW \r\n\tfrom(\r\n\tselect manageid,sum(payAmt) payAmt,sum(commission) as promAmt,sum(supplierAmt) as supplierAmt,sum(realvalue) as realvalue\r\n\tfrom v_order with(nolock)\r\n\twhere manageid > 0 and processingtime >= @sdt and processingtime <= @edt  \r\n\tgroup by manageid) D \r\n)D1  left join manage B with(nolock)  on D1.manageid = B.id\r\nWHERE D1.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize\r\norder by " + orderby;
                SqlParameter[] sqlParameterArray = new SqlParameter[4]
        {
          new SqlParameter("@sdt", SqlDbType.DateTime, 8),
          new SqlParameter("@edt", SqlDbType.DateTime, 8),
          new SqlParameter("@page", SqlDbType.Int, 4),
          new SqlParameter("@pagesize", SqlDbType.Int, 4)
        };
                sqlParameterArray[0].Value = (object)sdt;
                sqlParameterArray[1].Value = (object)edt;
                sqlParameterArray[2].Value = (object)page;
                sqlParameterArray[3].Value = (object)pagesize;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public static DataSet BusinessStat7(DateTime sdt, DateTime edt)
        {
            try
            {
                string commandText = "\r\nselect '网银利润' as class,sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt\r\nfrom v_orderbank with(nolock) \r\nwhere status=2 and typeid=102 and  processingtime>=@sdt and processingtime <= @edt\r\n\r\nunion all\r\n\r\nselect '支付宝利润',sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt\r\nfrom v_orderbank with(nolock) \r\nwhere status=2 and typeid=101 and  processingtime>=@sdt and processingtime <= @edt\r\n\r\nunion all\r\n\r\nselect '财付通利润',sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt\r\nfrom v_orderbank with(nolock) \r\nwhere status=2 and typeid=100 and  processingtime>=@sdt and processingtime <= @edt\r\n\r\nunion all\r\n\r\nselect '点卡利润',sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt\r\nfrom v_ordercard with(nolock) \r\nwhere status=2 and  processingtime>=@sdt and processingtime <= @edt";
                SqlParameter[] sqlParameterArray = new SqlParameter[2]
        {
          new SqlParameter("@sdt", SqlDbType.DateTime, 8),
          new SqlParameter("@edt", SqlDbType.DateTime, 8)
        };
                sqlParameterArray[0].Value = (object)sdt;
                sqlParameterArray[1].Value = (object)edt;
                return DataBase.ExecuteDataset(CommandType.Text, commandText, sqlParameterArray);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }
    }
}
