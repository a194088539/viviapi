using System;
using System.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Order
{
    public class Helper
    {
        private readonly viviapi.DAL.Order.Helper dal = new viviapi.DAL.Order.Helper();

        public int search_check(int o_userid, string userorderid, out DataRow row)
        {
            row = (DataRow)null;
            try
            {
                return this.dal.search_check(o_userid, userorderid, out row);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 99;
            }
        }
    }
}
