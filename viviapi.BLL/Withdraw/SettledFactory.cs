using System;
using System.Collections.Generic;
using System.Data;
using viviapi.DAL;
using viviapi.Model;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL
{
    public class SettledFactory
    {
        private static settled dal = new settled();

        public static int Add(SettledInfo model)
        {
            try
            {
                return SettledFactory.dal.Add(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static int Apply(SettledInfo model)
        {
            try
            {
                return SettledFactory.dal.Apply(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static bool Cancel(int id)
        {
            try
            {
                return SettledFactory.dal.Cancel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Update(SettledInfo model)
        {
            try
            {
                return SettledFactory.dal.Update(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static SettledInfo GetModel(int id)
        {
            try
            {
                return SettledFactory.dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (SettledInfo)null;
            }
        }

        public static bool Audit(int id, int status)
        {
            try
            {
                return SettledFactory.dal.Audit(id, status);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool BatchPass(string ids, string batchNo, out DataTable withdrawListByApi)
        {
            withdrawListByApi = (DataTable)null;
            try
            {
                return SettledFactory.dal.BatchPass(ids, batchNo, out withdrawListByApi);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool BatchSettle(string ids)
        {
            try
            {
                return SettledFactory.dal.BatchSettle(ids);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool AllPass(string batchNo)
        {
            try
            {
                return SettledFactory.dal.AllPass(batchNo);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static DataTable GetListWithdrawByApi(string batchNo)
        {
            try
            {
                return SettledFactory.dal.GetListWithdrawByApi(batchNo).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public static bool Allfails()
        {
            try
            {
                return SettledFactory.dal.Allfails();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool Delete(DateTime etime)
        {
            try
            {
                return SettledFactory.dal.Delete(etime);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return SettledFactory.dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public static int Pay(SettledInfo model)
        {
            try
            {
                return SettledFactory.dal.Pay(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public static bool AllSettle()
        {
            try
            {
                return SettledFactory.dal.AllSettle();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static bool AutoSettled(Decimal balance)
        {
            try
            {
                return SettledFactory.dal.AutoSettled(balance);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public static List<SettledInfo> DataTableToList(DataTable dt)
        {
            List<SettledInfo> list = new List<SettledInfo>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int index = 0; index < count; ++index)
                {
                    SettledInfo settledInfo = SettledFactory.dal.DataRowToModel(dt.Rows[index]);
                    if (settledInfo != null)
                        list.Add(settledInfo);
                }
            }
            return list;
        }

        public static int GetUserDaySettledTimes(int userid, string day)
        {
            try
            {
                return SettledFactory.dal.GetUserDaySettledTimes(userid, day);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public static Decimal GetUserDaySettledAmt(int userid, string day)
        {
            try
            {
                return SettledFactory.dal.GetUserDaySettledAmt(userid, day);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new Decimal(0);
            }
        }

        public static string GetSettleBankName(string code)
        {
            string str = code;
            switch (code)
            {
                case "0002":
                    str = "支付宝";
                    break;
                case "0003":
                    str = "财付通";
                    break;
                case "1002":
                    str = "工商银行";
                    break;
                case "1005":
                    str = "农业银行";
                    break;
                case "1003":
                    str = "建设银行";
                    break;
                case "1026":
                    str = "中国银行";
                    break;
                case "1001":
                    str = "招商银行";
                    break;
                case "1006":
                    str = "民生银行";
                    break;
                case "1020":
                    str = "交通银行";
                    break;
                case "1025":
                    str = "华夏银行";
                    break;
                case "1009":
                    str = "兴业银行";
                    break;
                case "1027":
                    str = "广发银行";
                    break;
                case "1004":
                    str = "浦发银行";
                    break;
                case "1022":
                    str = "光大银行";
                    break;
                case "1021":
                    str = "中信银行";
                    break;
                case "1010":
                    str = "平安银行";
                    break;
                case "1066":
                    str = "邮政储蓄银行";
                    break;
            }
            return str;
        }

        public static DataTable Export(string ids)
        {
            try
            {
                return SettledFactory.dal.Export(ids);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataTable)null;
            }
        }

        public static Decimal GetPayDayMoney(int uid)
        {
            try
            {
                return SettledFactory.dal.GetPayDayMoney(uid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new Decimal(0);
            }
        }

        public static Decimal Getpayingmoney(int uid)
        {
            try
            {
                return SettledFactory.dal.Getpayingmoney(uid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return new Decimal(0);
            }
        }
    }
}
