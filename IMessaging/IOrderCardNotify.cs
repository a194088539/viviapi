using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    public interface IOrderCardNotify
    {
        OrderCardInfo Receive();

        OrderCardInfo Receive(int timeout);

        void Send(OrderCardInfo orderMessage);
    }
}
