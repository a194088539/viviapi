using viviapi.Model.Order;

namespace viviapi.IMessaging
{
    public interface IOrderBank
    {
        OrderBankInfo Receive();

        OrderBankInfo Receive(int timeout);

        void Send(OrderBankInfo orderMessage);

        void Complete(OrderBankInfo orderMessage);
    }
}
