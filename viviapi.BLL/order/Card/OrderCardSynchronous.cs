using System;
using System.Transactions;
using viviapi.DALFactory;
using viviapi.IBLLStrategy;
using viviapi.IDAL;
using viviapi.Model.Order;

namespace viviapi.BLL
{
    public class OrderCardSynchronous : IOrderCardStrategy
    {
        private static readonly IOrderCard dal = DataAccess.CreateOrderCard();

        public void Insert(OrderCardInfo order)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                OrderCardSynchronous.dal.Insert(order);
                transactionScope.Complete();
            }
        }

        public void InsertItem(CardItemInfo order)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                OrderCardSynchronous.dal.InsertItem(order);
                transactionScope.Complete();
            }
        }

        public void Complete(OrderCardInfo order)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                OrderCardSynchronous.dal.Complete(order);
                transactionScope.Complete();
            }
        }

        public bool ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out Decimal ototalvalue)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                allCompleted = false;
                opstate = string.Empty;
                ovalue = string.Empty;
                ototalvalue = new Decimal(0);
                OrderCardSynchronous.dal.ItemComplete(order, out allCompleted, out opstate, out ovalue, out ototalvalue);
                transactionScope.Complete();
                return true;
            }
        }
    }
}
