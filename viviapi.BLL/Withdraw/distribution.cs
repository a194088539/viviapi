namespace viviapi.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using viviapi.Cache;
    using viviLib.Data;
    using viviLib.ExceptionHandling;

    public class distribution
    {
        private static viviapi.DAL.distribution dal = new viviapi.DAL.distribution();

        public static int Add(viviapi.Model.distribution model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public List<viviapi.Model.distribution> DataTableToList(DataTable dt)
        {
            try
            {
                List<viviapi.Model.distribution> list = new List<viviapi.Model.distribution>();
                int count = dt.Rows.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        viviapi.Model.distribution item = dal.DataRowToModel(dt.Rows[i]);
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return dal.Delete(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public bool Delete(string trade_no)
        {
            try
            {
                return dal.Delete(trade_no);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public bool DeleteList(string idlist)
        {
            try
            {
                return dal.DeleteList(idlist);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static bool Exists(string trade_no)
        {
            try
            {
                return dal.Exists(trade_no);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static string GenerateTradeNo(int mode)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string objId = mode + new Random().Next(100, 0x3e7).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
            {
                return GenerateTradeNo(mode);
            }
            WebCache.GetCacheService().AddObject(objId, objId, 10);
            return objId;
        }

        public DataSet GetAllList()
        {
            try
            {
                return this.GetList("");
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public DataSet GetList(string strWhere)
        {
            try
            {
                return dal.GetList(strWhere);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                return dal.GetList(Top, strWhere, filedOrder);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            try
            {
                return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static viviapi.Model.distribution GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public List<viviapi.Model.distribution> GetModelList(string strWhere)
        {
            try
            {
                DataSet list = dal.GetList(strWhere);
                return this.DataTableToList(list.Tables[0]);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public int GetRecordCount(string strWhere)
        {
            try
            {
                return dal.GetRecordCount(strWhere);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        public static string GetStatusText(object _status)
        {
            if ((_status == null) || (_status == DBNull.Value))
            {
                return string.Empty;
            }
            int num = Convert.ToInt32(_status);
            switch (num)
            {
                case 0:
                    return "已受理";

                case 1:
                    return "未受理";

                case 2:
                    return "审核拒绝";

                case 3:
                    return "代发成功";

                case 4:
                    return "代发失败";

                case 0xff:
                    return "初始状态";
            }
            return num.ToString();
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static int Process(int suppId, string trade_no, bool is_cancel, int status, string amount, string supp_trade_no, string message, out string bill_trade_no)
        {
            bill_trade_no = string.Empty;
            try
            {
                return dal.Process(suppId, trade_no, is_cancel, status, amount, supp_trade_no, message, out bill_trade_no);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0x63;
            }
        }

        public static bool Update(viviapi.Model.distribution model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
    }
}

