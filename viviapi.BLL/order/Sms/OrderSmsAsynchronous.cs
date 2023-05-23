using viviapi.IBLLStrategy;
using viviapi.IMessaging;
using viviapi.MessagingFactory;
using viviapi.Model.Order;

namespace viviapi.BLL
{
    public class OrderSmsAsynchronous : IOrderSmsStrategy
    {
        private static readonly IOrderSms asynchOrder = QueueAccess.CreateSmsOrder();

        public void Insert(OrderSmsInfo order)
        {
            OrderSmsAsynchronous.asynchOrder.Send(order);
        }

        public void Complete(OrderSmsInfo order)
        {
            OrderSmsAsynchronous.asynchOrder.Complete(order);
        }
    }
}
