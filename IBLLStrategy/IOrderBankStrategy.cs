using viviapi.Model.Order;

namespace viviapi.IBLLStrategy
{
    public interface IOrderBankStrategy
    {
        void Insert(OrderBankInfo order);

        void Complete(OrderBankInfo order);
    }
}
