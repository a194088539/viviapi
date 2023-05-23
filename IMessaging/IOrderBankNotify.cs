using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    public interface IOrderBankNotify
    {
        OrderBankInfo Receive();

        OrderBankInfo Receive(int timeout);

        void Send(OrderBankInfo orderMessage);
    }
}
