using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    public interface IOrderSms
    {
        OrderSmsInfo Receive();

        OrderSmsInfo Receive(int timeout);

        void Send(OrderSmsInfo orderMessage);

        void Complete(OrderSmsInfo orderMessage);
    }
}
