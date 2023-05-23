namespace viviapi.BLL
{
    using System;
    using System.Transactions;
    using viviapi.DALFactory;
    using viviapi.IBLLStrategy;
    using viviapi.IDAL;
    using viviapi.Model.Order;
    public class OrderBankSynchronous : IOrderBankStrategy
    {
        private static readonly IOrderBank dal = DataAccess.CreateOrderBank();

        public void Complete(OrderBankInfo order)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                dal.Complete(order);
                scope.Complete();
            }
        }

        public void Insert(OrderBankInfo order)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (dal.Insert(order) <= 0L)
                {
                    new ApplicationException("Add orders fails");
                }
                scope.Complete();
            }
        }
    }
}

