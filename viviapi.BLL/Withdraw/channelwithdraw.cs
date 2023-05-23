using System;
using System.Collections.Generic;
using System.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Withdraw
{
    public class channelwithdraw
    {
        private readonly viviapi.DAL.Withdraw.channelwithdraw dal = new viviapi.DAL.Withdraw.channelwithdraw();

        public int Add(viviapi.Model.Withdraw.channelwithdraw model)
        {
            return this.dal.Add(model);
        }

        public bool Update(viviapi.Model.Withdraw.channelwithdraw model)
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

        public viviapi.Model.Withdraw.channelwithdraw GetModel(int id)
        {
            return this.dal.GetModel(id);
        }

        public viviapi.Model.Withdraw.channelwithdraw GetModelByBankName(string bankName)
        {
            try
            {
                return this.dal.GetModelByBankName(bankName);
            }
            catch (Exception ex)
            {
                return (viviapi.Model.Withdraw.channelwithdraw)null;
            }
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public List<viviapi.Model.Withdraw.channelwithdraw> GetModelList(string strWhere)
        {
            return this.DataTableToList(this.dal.GetList(strWhere).Tables[0]);
        }

        public List<viviapi.Model.Withdraw.channelwithdraw> DataTableToList(DataTable dt)
        {
            List<viviapi.Model.Withdraw.channelwithdraw> list = new List<viviapi.Model.Withdraw.channelwithdraw>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int index = 0; index < count; ++index)
                {
                    viviapi.Model.Withdraw.channelwithdraw channelwithdraw = this.dal.DataRowToModel(dt.Rows[index]);
                    if (channelwithdraw != null)
                        list.Add(channelwithdraw);
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

        public int GetSupplier(string bankCode)
        {
            try
            {
                return this.dal.GetSupplier(bankCode);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
    }
}
