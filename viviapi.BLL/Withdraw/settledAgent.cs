using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using viviapi.Cache;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Withdraw
{
    public class settledAgent
    {
        private readonly viviapi.DAL.Withdraw.settledAgent dal = new viviapi.DAL.Withdraw.settledAgent();

        public bool Exists(string trade_no)
        {
            try
            {
                return this.dal.Exists(trade_no);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public int Add(viviapi.Model.Withdraw.settledAgent model)
        {
            try
            {
                return this.dal.Add(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }

        public bool Update(viviapi.Model.Withdraw.settledAgent model)
        {
            try
            {
                return this.dal.Update(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return this.dal.Delete(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool Delete(string trade_no)
        {
            try
            {
                return this.dal.Delete(trade_no);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public bool DeleteList(string idlist)
        {
            try
            {
                return this.dal.DeleteList(idlist);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        public viviapi.Model.Withdraw.settledAgent GetModel(int id)
        {
            try
            {
                return this.dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (viviapi.Model.Withdraw.settledAgent)null;
            }
        }

        public viviapi.Model.Withdraw.settledAgent GetModel(string trade_no)
        {
            try
            {
                return this.dal.GetModel(trade_no);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (viviapi.Model.Withdraw.settledAgent)null;
            }
        }

        public DataSet GetList(string strWhere)
        {
            try
            {
                return this.dal.GetList(strWhere);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                return this.dal.GetList(Top, strWhere, filedOrder);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public List<viviapi.Model.Withdraw.settledAgent> GetModelList(string strWhere)
        {
            try
            {
                return this.DataTableToList(this.dal.GetList(strWhere).Tables[0]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (List<viviapi.Model.Withdraw.settledAgent>)null;
            }
        }

        public List<viviapi.Model.Withdraw.settledAgent> DataTableToList(DataTable dt)
        {
            List<viviapi.Model.Withdraw.settledAgent> list = new List<viviapi.Model.Withdraw.settledAgent>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int index = 0; index < count; ++index)
                {
                    viviapi.Model.Withdraw.settledAgent settledAgent = this.dal.DataRowToModel(dt.Rows[index]);
                    if (settledAgent != null)
                        list.Add(settledAgent);
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

        public string GenerateTradeNo(int userid, int serial)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string objId = string.Concat(new object[4]
            {
        (object) DateTime.Now.ToString("yyyyMMddHHmmssff"),
        (object) userid,
        (object) serial.ToString(),
        (object) random.Next(1000).ToString("0000")
            });
            if (WebCache.GetCacheService().RetrieveObject(objId) != null)
                return this.GenerateTradeNo(userid, serial);
            WebCache.GetCacheService().AddObject(objId, (object)objId, 10);
            return objId;
        }

        public int Cancel(string trade_no)
        {
            try
            {
                int num = this.dal.Cancel(trade_no);
                if (num == 0)
                    this.DoNotify(trade_no);
                return num;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public string doCancel(string trade_no)
        {
            string str1 = string.Empty;
            string str2;
            switch (this.Cancel(trade_no))
            {
                case 0:
                    str2 = "取消成功";
                    break;
                case 1:
                    str2 = "不存在此单";
                    break;
                case 2:
                    str2 = "此单已取消，不可重复操作";
                    break;
                case 3:
                    str2 = "已审核，不可取消";
                    break;
                case 4:
                    str2 = "系统故障，请查看日志";
                    break;
                case 5:
                    str2 = "用户未确认";
                    break;
                default:
                    str2 = "未知错误";
                    break;
            }
            return str2;
        }

        public int Audit(string trade_no, int auditstatus, int auditUser, string auditUserName)
        {
            try
            {
                int num = this.dal.Audit(trade_no, auditstatus, auditUser, auditUserName);
                if (num == 0)
                    this.DoNotify(trade_no);
                return num;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public string doAudit(string trade_no, int auditUser, string auditUserName)
        {
            string str1 = string.Empty;
            string str2;
            switch (this.Audit(trade_no, 2, auditUser, auditUserName))
            {
                case 0:
                    str2 = "审核成功";
                    break;
                case 1:
                    str2 = "不存在此单";
                    break;
                case 2:
                    str2 = "此单已取消";
                    break;
                case 3:
                    str2 = "已审核过，不可重复操作";
                    break;
                case 4:
                    str2 = "输入状态不正确";
                    break;
                case 5:
                    str2 = "系统故障，请查看日志";
                    break;
                case 6:
                    str2 = "用户未确认,不可操作";
                    break;
                default:
                    str2 = "未知错误";
                    break;
            }
            return str2;
        }

        public string doRefuse(string trade_no, int auditUser, string auditUserName)
        {
            string str1 = string.Empty;
            string str2;
            switch (this.Audit(trade_no, 3, auditUser, auditUserName))
            {
                case 0:
                    str2 = "操作成功";
                    break;
                case 1:
                    str2 = "不存在此单";
                    break;
                case 2:
                    str2 = "此单已取消";
                    break;
                case 3:
                    str2 = "已审核过，不可重复操作";
                    break;
                case 4:
                    str2 = "输入状态不正确";
                    break;
                case 5:
                    str2 = "系统故障，请查看日志";
                    break;
                default:
                    str2 = "未知错误";
                    break;
            }
            return str2;
        }

        public int Complete(string trade_no, int pstatus)
        {
            try
            {
                int num = this.dal.Complete(trade_no, pstatus);
                if (num == 0)
                    this.DoNotify(trade_no);
                return num;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public string PaySuccess(string trade_no)
        {
            string str1 = string.Empty;
            string str2;
            switch (this.Complete(trade_no, 2))
            {
                case 0:
                    str2 = "付款成功";
                    break;
                case 1:
                    str2 = "不存在此单";
                    break;
                case 2:
                    str2 = "此单已取消";
                    break;
                case 3:
                    str2 = "此单未审核，不可完成此操作";
                    break;
                case 4:
                    str2 = "此单已结案";
                    break;
                case 5:
                    str2 = "系统故障，请查看日志";
                    break;
                default:
                    str2 = "未知错误";
                    break;
            }
            return str2;
        }

        public string PayFail(string trade_no)
        {
            string str1 = string.Empty;
            string str2;
            switch (this.Complete(trade_no, 3))
            {
                case 0:
                    str2 = "付款成功";
                    break;
                case 1:
                    str2 = "不存在此单";
                    break;
                case 2:
                    str2 = "此单已取消";
                    break;
                case 3:
                    str2 = "此单未审核，不可完成此操作";
                    break;
                case 4:
                    str2 = "此单已结案";
                    break;
                case 5:
                    str2 = "系统故障，请查看日志";
                    break;
                default:
                    str2 = "未知错误";
                    break;
            }
            return str2;
        }

        public int ChkParms(int userid, string bankCode, Decimal amount, out DataRow row)
        {
            row = (DataRow)null;
            try
            {
                return this.dal.ChkParms(userid, bankCode, amount, out row);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby, byte stat)
        {
            try
            {
                return this.dal.PageSearch(searchParams, pageSize, page, orderby, stat);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public void DoNotify(string trade_no)
        {
            viviapi.Model.Withdraw.settledAgent model = this.GetModel(trade_no);
            if (model == null)
                return;
            new Thread(new ThreadStart(new notifyHelper()
            {
                model = model
            }.DoNotify)).Start();
        }

        public int Affirm(string trade_no, byte sure, string clientip)
        {
            try
            {
                return this.dal.Affirm(trade_no, sure, clientip);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }

        public string GetIsSureText(object issure)
        {
            string str = string.Empty;
            if (issure == DBNull.Value)
                return str;
            switch (Convert.ToInt32(issure))
            {
                case 1:
                    str = "等待处理";
                    break;
                case 2:
                    str = "已确认";
                    break;
                case 3:
                    str = "未确认";
                    break;
            }
            return str;
        }

        public string GetAuditStatusText(object audit_status)
        {
            string str = string.Empty;
            if (audit_status == DBNull.Value)
                return str;
            switch (Convert.ToInt32(audit_status))
            {
                case 1:
                    str = "等待审核";
                    break;
                case 2:
                    str = "审核通过";
                    break;
                case 3:
                    str = "审核拒绝";
                    break;
            }
            return str;
        }

        public string GetPaymentStatusText(object payment_status)
        {
            string str = string.Empty;
            if (payment_status == DBNull.Value)
                return str;
            switch (Convert.ToInt32(payment_status))
            {
                case 1:
                    str = "未知";
                    break;
                case 2:
                    str = "成功";
                    break;
                case 3:
                    str = "失败";
                    break;
                case 4:
                    str = "付款中";
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

        public string GetSureText(object is_sure)
        {
            string str = string.Empty;
            if (is_sure == DBNull.Value)
                return str;
            switch (Convert.ToByte(is_sure))
            {
                case (byte)1:
                    str = "等待处理";
                    break;
                case (byte)2:
                    str = "已确认";
                    break;
                case (byte)3:
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
                    str = "API提交";
                    break;
                case 2:
                    str = "上传文件";
                    break;
            }
            return str;
        }

        public string GetStatusText(object is_cancel, object audit_status, object payment_status)
        {
            string str = "未知";
            if (is_cancel == DBNull.Value)
                return str;
            if ((int)Convert.ToByte(is_cancel) == 1)
                return "已取消";
            switch (Convert.ToByte(audit_status))
            {
                case (byte)1:
                    str = "等待审核";
                    break;
                case (byte)2:
                    str = "已审核(处理中)";
                    break;
                case (byte)3:
                    str = "审核拒绝";
                    break;
            }
            switch (Convert.ToByte(payment_status))
            {
                case (byte)2:
                    str = "支付成功";
                    break;
                case (byte)3:
                    str = "付款失败";
                    break;
                case (byte)4:
                    str = "已审核(处理中)";
                    break;
            }
            return str;
        }
    }
}
