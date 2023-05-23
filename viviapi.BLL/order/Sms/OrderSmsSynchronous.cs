using System.Transactions;
using viviapi.DALFactory;
using viviapi.IBLLStrategy;
using viviapi.IDAL;
using viviapi.Model.Order;

namespace viviapi.BLL
{
    public class OrderSmsSynchronous : IOrderSmsStrategy
    {
        private static readonly IOrderSms dal = DataAccess.CreateOrderSms();

        public void Insert(OrderSmsInfo order)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                OrderSmsSynchronous.dal.Insert(order);
                transactionScope.Complete();
            }
        }
    }
}
