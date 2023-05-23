using System.Threading;

namespace viviapi.Model.Order
{
    public class OrderSmsNotify
    {
        public Timer tmr;

        public OrderSmsInfo orderInfo { get; set; }
    }
}
