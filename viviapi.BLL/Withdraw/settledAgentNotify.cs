using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using viviapi.BLL.User;
using viviLib.Data;
using viviLib.ExceptionHandling;
using viviLib.Text;
using viviLib.Web;

namespace viviapi.BLL.Withdraw
{
    public class settledAgentNotify
    {
        private readonly viviapi.DAL.Withdraw.settledAgentNotify dal = new viviapi.DAL.Withdraw.settledAgentNotify();

        public int Add(viviapi.Model.Withdraw.settledAgentNotify model)
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

        public bool Update(viviapi.Model.Withdraw.settledAgentNotify model)
        {
            return this.dal.Update(model);
        }

        public bool Delete(int id)
        {
            return this.dal.Delete(id);
        }

        public bool DeleteList(string idlist)
        {
            return this.dal.DeleteList(idlist);
        }

        public viviapi.Model.Withdraw.settledAgentNotify GetModel(int id)
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

        public List<viviapi.Model.Withdraw.settledAgentNotify> GetModelList(string strWhere)
        {
            return this.DataTableToList(this.dal.GetList(strWhere).Tables[0]);
        }

        public List<viviapi.Model.Withdraw.settledAgentNotify> DataTableToList(DataTable dt)
        {
            List<viviapi.Model.Withdraw.settledAgentNotify> list = new List<viviapi.Model.Withdraw.settledAgentNotify>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int index = 0; index < count; ++index)
                {
                    viviapi.Model.Withdraw.settledAgentNotify settledAgentNotify = this.dal.DataRowToModel(dt.Rows[index]);
                    if (settledAgentNotify != null)
                        list.Add(settledAgentNotify);
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

        public void DoNotify(viviapi.Model.Withdraw.settledAgent model)
        {
            if (model == null || !PageValidate.IsUrl(model.return_url) || model.is_cancel)
                return;
            viviapi.Model.Withdraw.settledAgentNotify model1 = new viviapi.Model.Withdraw.settledAgentNotify();
            model1.userid = model.userid;
            model1.trade_no = model.trade_no;
            string notifyId = model1.notify_id;
            string service = model.service;
            string inputCharset = model.input_charset;
            string str1 = model.userid.ToString();
            string signType = model.sign_type;
            string str2 = model1.addTime.ToString("yyyyMMddHHmmss");
            string tradeNo = model.trade_no;
            string str3 = "0.00";
            int num1 = 1;
            if (model.is_cancel)
            {
                num1 = 1;
            }
            else
            {
                if (model.audit_status == 1)
                    num1 = 1;
                else if (model.audit_status == 2)
                    num1 = 0;
                else if (model.audit_status == 3)
                    num1 = 2;
                if (model.audit_status == 2)
                {
                    if (model.payment_status == 2)
                    {
                        num1 = 3;
                        str3 = model.amount.ToString("f2");
                    }
                    if (model.payment_status == 3)
                        num1 = 4;
                }
            }
            string str4 = num1.ToString();
            string str5 = string.Empty;
            string str6 = CommonHelper.BuildParamString(CommonHelper.BubbleSort(new string[10]
            {
        "service=" + service,
        "input_charset=" + inputCharset,
        "partner=" + str1,
        "sign_type=" + signType,
        "notify_id=" + notifyId,
        "notify_time=" + str2,
        "out_trade_no=" + tradeNo,
        "trade_status=" + str4,
        "error_message=" + str5,
        "amount_str=" + str3
            }));
            string userApiKey = UserFactory.GetUserApiKey(model.userid);
            string str7 = CommonHelper.md5(inputCharset, str6 + userApiKey).ToLower();
            string url = model.return_url + "?" + string.Format("service={0}", (object)service) + string.Format("&input_charset={0}", (object)inputCharset) + string.Format("&partner={0}", (object)str1) + string.Format("&sign_type={0}", (object)signType) + string.Format("&notify_id={0}", (object)notifyId) + string.Format("&notify_time={0}", (object)str2) + string.Format("&out_trade_no={0}", (object)tradeNo) + string.Format("&trade_status={0}", (object)str4) + string.Format("&error_message={0}", (object)str5) + string.Format("&amount_str={0}", (object)str3) + string.Format("&sign={0}", (object)str7);
            try
            {
                string @string = WebClientHelper.GetString(url, (NameValueCollection)null, "GET", Encoding.GetEncoding(inputCharset));
                model1.resTime = new DateTime?(DateTime.Now);
                int num2 = !(@string == notifyId) ? 0 : 2;
                model1.notifyurl = url;
                model1.resText = @string;
                model1.notifystatus = num2;
            }
            catch (Exception ex)
            {
                model1.notifyurl = url;
                model1.resText = "";
                model1.notifystatus = 0;
                model1.remark = ex.Message;
            }
            new settledAgentNotify().Add(model1);
        }

        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return this.dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return (DataSet)null;
            }
        }

        public string GetNotifyStatusText(object notifystatus)
        {
            string str = string.Empty;
            if (notifystatus == DBNull.Value)
                return str;
            switch (Convert.ToInt32(notifystatus))
            {
                case 1:
                    str = "处理中";
                    break;
                case 0:
                    str = "失败";
                    break;
                case 2:
                    str = "成功";
                    break;
            }
            return str;
        }
    }
}
