using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    public interface IOrderCard
    {
        object Receive();

        object Receive(int timeout);

        void Send(OrderCardInfo orderMessage);

        void SendItem(CardItemInfo orderMessage);

        void Complete(OrderCardInfo orderMessage);

        void ItemComplete(CardItemInfo orderMessage);
    }
}
