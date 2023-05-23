using System;
using System.Collections.Generic;
using System.Data;
using viviapi.Cache;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Withdraw
{
    public class settledAgentSummary
    {
        private readonly viviapi.DAL.Withdraw.settledAgentSummary dal = new viviapi.DAL.Withdraw.settledAgentSummary();

        public bool Exists(string lotno)
        {
            return this.dal.Exists(lotno);
        }

        public int Add(viviapi.Model.Withdraw.settledAgentSummary model)
        {
            return this.dal.Add(model);
        }

        public bool Update(viviapi.Model.Withdraw.settledAgentSummary model)
        {
            return this.dal.Update(model);
        }

        public bool Delete(int id)
        {
            return this.dal.Delete(id);
        }

        public bool Delete(string lotno)
        {
            return this.dal.Delete(lotno);
        }

        public bool DeleteList(string idlist)
        {
            return this.dal.DeleteList(idlist);
        }

        public viviapi.Model.Withdraw.settledAgentSummary GetModel(int id)
        {
            return this.dal.GetModel(id);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public List<viviapi.Model.Withdraw.settledAgentSummary> GetModelList(string strWhere)
        {
            return this.DataTableToList(this.dal.GetList(strWhere).Tables[0]);
        }

        public List<viviapi.Model.Withdraw.settledAgentSummary> DataTableToList(DataTable dt)
        {
            List<viviapi.Model.Withdraw.settledAgentSummary> list = new List<viviapi.Model.Withdraw.settledAgentSummary>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int index = 0; index < count; ++index)
                {
                    viviapi.Model.Withdraw.settledAgentSummary settledAgentSummary = this.dal.DataRowToModel(dt.Rows[index]);
                    if (settledAgentSummary != null)
                        list.Add(settledAgentSummary);
                }
            }
            return list;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public string Generatelotno()
        {
            string objId = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random(Guid.NewGuid().GetHashCode()).Next(10000).ToString("0000");
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                return this.Generatelotno();
            WebCache.GetCacheService().AddObject(objId, (object)objId, 10);
            return objId;
        }

        public int ChkParms(int userid, Decimal tamount)
        {
            try
            {
                return this.dal.ChkParms(userid, tamount);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public int Insert(viviapi.Model.Withdraw.settledAgentSummary summarymodel, List<viviapi.Model.Withdraw.settledAgent> itemlist)
        {
            try
            {
                return this.dal.Insert(summarymodel, itemlist);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public int Affirm(string lot_no, int auditstatus, int auditUser, string auditUserName, string clientip)
        {
            try
            {
                return this.dal.Affirm(lot_no, auditstatus, auditUser, auditUserName, clientip);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, bool isstat)
        {
            try
            {
                return this.dal.PageSearch(searchParams, pageSize, page, orderby, isstat);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public string GetAuditStatusText(object audit_status)
        {
            string str = string.Empty;
            if (audit_status == DBNull.Value)
                return str;
            switch (Convert.ToInt32(audit_status))
            {
                case 1:
                    str = "等待处理";
                    break;
                case 2:
                    str = "已确认";
                    break;
                case 3:
                    str = "已取消";
                    break;
            }
            return str;
        }

        public string GetStatus(object status)
        {
            string str = string.Empty;
            if (status == DBNull.Value)
                return str;
            switch (Convert.ToInt32(status))
            {
                case 1:
                    str = "等待处理";
                    break;
                case 2:
                    str = "部分完成";
                    break;
                case 3:
                    str = "全部完成";
                    break;
            }
            return str;
        }

        public string GetCancelText(object is_cancel)
        {
            string str = string.Empty;
            if (is_cancel == DBNull.Value)
                return str;
            switch (Convert.ToInt32(is_cancel))
            {
                case 0:
                    str = "未取消";
                    break;
                case 1:
                    str = "已取消";
                    break;
            }
            return str;
        }

        public string GetNotifyStatusText(object notifystatus)
        {
            string str = string.Empty;
            if (notifystatus == DBNull.Value)
                return str;
            switch (Convert.ToInt32(notifystatus))
            {
                case 0:
                    str = "发送失败";
                    break;
                case 1:
                    str = "处理中";
                    break;
                case 2:
                    str = "已成功";
                    break;
            }
            return str;
        }

        public string GetModeText(object mode)
        {
            string str = string.Empty;
            if (mode == DBNull.Value)
                return str;
            switch (Convert.ToInt32(mode))
            {
                case 1:
                    str = "API接口提交";
                    break;
                case 2:
                    str = "手动增加";
                    break;
            }
            return str;
        }
    }
}
