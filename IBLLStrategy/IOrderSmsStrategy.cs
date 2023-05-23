using viviapi.Model.Order;

namespace viviapi.IBLLStrategy
{
    public interface IOrderSmsStrategy
    {
        void Insert(OrderSmsInfo order);
    }
}
