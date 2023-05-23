using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    public interface IOrderSmsNotify
    {
        OrderSmsInfo Receive();

        OrderSmsInfo Receive(int timeout);

        void Send(OrderSmsInfo orderMessage);
    }
}
