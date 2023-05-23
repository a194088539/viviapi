using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using viviLib.ExceptionHandling;
using viviLib.Web;

namespace viviapi.BLL.Financial
{
    public class tenpay_batch_trans_head
    {
        public viviapi.Model.Financial.tenpay_batch_trans_head InitInfo(int settleid, Decimal tranamt, int userid, string account, string rec_name, string op_user)
        {
            string str = new Random().Next(100, 999).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
            return new viviapi.Model.Financial.tenpay_batch_trans_head()
            {
                client_ip = ServerVariables.TrueIP,
                completetime = new DateTime?(DateTime.Now),
                fail = new int?(0),
                success = new int?(0),
                uncertain = new int?(0),
                id = 0,
                message = string.Empty,
                op_time = DateTime.Now,
                op_user = op_user,
                package_id = str,
                status = 1,
                total_amt = tranamt,
                total_num = 1,
                version = "2",
                items = new List<viviapi.Model.Financial.tenpay_batch_trans_detail>()
        {
          new viviapi.Model.Financial.tenpay_batch_trans_detail()
          {
            settleid = settleid,
            package_id = str,
            hid = 0,
            id = 0,
            message = string.Empty,
            pay_amt = tranamt,
            rec_acc = account,
            rec_name = rec_name,
            remark = string.Empty,
            serial = 1,
            status = 1,
            succ_amt = new Decimal?(new Decimal(0)),
            trans_id = string.Empty,
            userid = userid,
            balance = new Decimal(0)
          }
        }
            };
        }

        public int Insert(viviapi.Model.Financial.tenpay_batch_trans_head model)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString))
            {
                sqlConnection.Open();
                using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                {
                    try
                    {
                        SqlParameter[] sqlParameterArray1 = new SqlParameter[16]
                        {
              new SqlParameter("@id", SqlDbType.Int, 4),
              new SqlParameter("@package_id", SqlDbType.VarChar, 20),
              new SqlParameter("@total_num", SqlDbType.Int, 4),
              new SqlParameter("@total_amt", SqlDbType.Decimal, 9),
              new SqlParameter("@status", SqlDbType.Int, 4),
              new SqlParameter("@version", SqlDbType.VarChar, 10),
              new SqlParameter("@client_ip", SqlDbType.VarChar, 20),
              new SqlParameter("@op_user", SqlDbType.VarChar, 30),
              new SqlParameter("@op_time", SqlDbType.DateTime),
              new SqlParameter("@completetime", SqlDbType.DateTime),
              new SqlParameter("@retcode", SqlDbType.VarChar, 50),
              new SqlParameter("@message", SqlDbType.VarChar, 200),
              new SqlParameter("@retcontext", SqlDbType.VarChar, 8000),
              new SqlParameter("@success", SqlDbType.Int, 4),
              new SqlParameter("@fail", SqlDbType.Int, 4),
              new SqlParameter("@uncertain", SqlDbType.Int, 4)
                        };
                        sqlParameterArray1[0].Direction = ParameterDirection.Output;
                        sqlParameterArray1[1].Value = (object)model.package_id;
                        sqlParameterArray1[2].Value = (object)model.total_num;
                        sqlParameterArray1[3].Value = (object)model.total_amt;
                        sqlParameterArray1[4].Value = (object)model.status;
                        sqlParameterArray1[5].Value = (object)model.version;
                        sqlParameterArray1[6].Value = (object)model.client_ip;
                        sqlParameterArray1[7].Value = (object)model.op_user;
                        sqlParameterArray1[8].Value = (object)model.op_time;
                        sqlParameterArray1[9].Value = (object)model.completetime;
                        sqlParameterArray1[10].Value = (object)model.retcode;
                        sqlParameterArray1[11].Value = (object)model.message;
                        sqlParameterArray1[12].Value = (object)model.retcontext;
                        sqlParameterArray1[13].Value = (object)model.success;
                        sqlParameterArray1[14].Value = (object)model.fail;
                        sqlParameterArray1[15].Value = (object)model.uncertain;
                        int num = Convert.ToInt32(DataBase.ExecuteScalar(transaction, "proc_tenpay_batch_trans_head_add", (object[])sqlParameterArray1));
                        if (num > 0)
                        {
                            string str = string.Empty;
                            foreach (viviapi.Model.Financial.tenpay_batch_trans_detail batchTransDetail in model.items)
                            {
                                SqlParameter[] sqlParameterArray2 = new SqlParameter[17]
                                {
                  new SqlParameter("@id", SqlDbType.Int, 4),
                  new SqlParameter("@settleid", SqlDbType.Int, 4),
                  new SqlParameter("@hid", SqlDbType.Int, 4),
                  new SqlParameter("@package_id", SqlDbType.VarChar, 20),
                  new SqlParameter("@serial", SqlDbType.Int, 4),
                  new SqlParameter("@userid", SqlDbType.Int, 4),
                  new SqlParameter("@balance", SqlDbType.Decimal, 9),
                  new SqlParameter("@status", SqlDbType.TinyInt, 1),
                  new SqlParameter("@rec_acc", SqlDbType.VarChar, 20),
                  new SqlParameter("@rec_name", SqlDbType.VarChar, 20),
                  new SqlParameter("@cur_type", SqlDbType.VarChar, 20),
                  new SqlParameter("@pay_amt", SqlDbType.Decimal, 9),
                  new SqlParameter("@succ_amt", SqlDbType.Decimal, 9),
                  new SqlParameter("@remark", SqlDbType.VarChar, 200),
                  new SqlParameter("@trans_id", SqlDbType.VarChar, 50),
                  new SqlParameter("@message", SqlDbType.VarChar, 50),
                  new SqlParameter("@completetime", SqlDbType.DateTime)
                                };
                                sqlParameterArray2[0].Direction = ParameterDirection.Output;
                                sqlParameterArray2[1].Value = (object)batchTransDetail.settleid;
                                sqlParameterArray2[2].Value = (object)num;
                                sqlParameterArray2[3].Value = (object)batchTransDetail.package_id;
                                sqlParameterArray2[4].Value = (object)batchTransDetail.serial;
                                sqlParameterArray2[5].Value = (object)batchTransDetail.userid;
                                sqlParameterArray2[6].Value = (object)batchTransDetail.balance;
                                sqlParameterArray2[7].Value = (object)batchTransDetail.status;
                                sqlParameterArray2[8].Value = (object)batchTransDetail.rec_acc;
                                sqlParameterArray2[9].Value = (object)batchTransDetail.rec_name;
                                sqlParameterArray2[10].Value = (object)batchTransDetail.cur_type;
                                sqlParameterArray2[11].Value = (object)batchTransDetail.pay_amt;
                                sqlParameterArray2[12].Value = (object)batchTransDetail.succ_amt;
                                sqlParameterArray2[13].Value = (object)batchTransDetail.remark;
                                sqlParameterArray2[14].Value = (object)batchTransDetail.trans_id;
                                sqlParameterArray2[15].Value = (object)batchTransDetail.message;
                                sqlParameterArray2[16].Value = (object)DateTime.Now;
                                DataBase.ExecuteScalar(transaction, "proc_tenpay_batch_trans_detail_ADD", (object[])sqlParameterArray2);
                            }
                            transaction.Commit();
                            sqlConnection.Close();
                            return num;
                        }
                        transaction.Rollback();
                        sqlConnection.Close();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionHandler.HandleException(ex);
                        return 0;
                    }
                }
            }
        }

        public int Complete(string package_id, int status, string retcode, string message, string retcontext, int success, int fail, int uncertain)
        {
            try
            {
                SqlParameter[] sqlParameterArray = new SqlParameter[9]
                {
          new SqlParameter("@package_id", SqlDbType.VarChar, 20),
          new SqlParameter("@status", SqlDbType.Int, 4),
          new SqlParameter("@completetime", SqlDbType.DateTime),
          new SqlParameter("@retcode", SqlDbType.VarChar, 50),
          new SqlParameter("@message", SqlDbType.VarChar, 200),
          new SqlParameter("@retcontext", SqlDbType.VarChar, 8000),
          new SqlParameter("@success", SqlDbType.Int, 4),
          new SqlParameter("@fail", SqlDbType.Int, 4),
          new SqlParameter("@uncertain", SqlDbType.Int, 4)
                };
                sqlParameterArray[0].Value = (object)package_id;
                sqlParameterArray[1].Value = (object)status;
                sqlParameterArray[2].Value = (object)DateTime.Now;
                sqlParameterArray[3].Value = (object)retcode;
                sqlParameterArray[4].Value = (object)message;
                sqlParameterArray[5].Value = (object)retcontext;
                sqlParameterArray[6].Value = (object)success;
                sqlParameterArray[7].Value = (object)fail;
                sqlParameterArray[8].Value = (object)uncertain;
                object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_tenpay_batch_trans_head_complete", sqlParameterArray);
                if (obj != DBNull.Value)
                    return Convert.ToInt32(obj);
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
    }
}
