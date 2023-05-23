using viviapi.IBLLStrategy;
using viviapi.IMessaging;
using viviapi.MessagingFactory;
using viviapi.Model.Order;

namespace viviapi.BLL
{
    public class OrderBankAsynchronous : IOrderBankStrategy
    {
        private static readonly IOrderBank asynchOrder = QueueAccess.CreateBankOrder();

        public void Insert(OrderBankInfo order)
        {
            OrderBankAsynchronous.asynchOrder.Send(order);
        }

        public void Complete(OrderBankInfo order)
        {
            OrderBankAsynchronous.asynchOrder.Complete(order);
        }
    }
}
