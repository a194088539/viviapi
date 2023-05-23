using System;
using viviapi.IBLLStrategy;
using viviapi.IMessaging;
using viviapi.MessagingFactory;
using viviapi.Model.Order;

namespace viviapi.BLL
{
    public class OrderCardAsynchronous : IOrderCardStrategy
    {
        private static readonly IOrderCard asynchOrder = QueueAccess.CreateCardOrder();

        public void Insert(OrderCardInfo order)
        {
            OrderCardAsynchronous.asynchOrder.Send(order);
        }

        public void InsertItem(CardItemInfo order)
        {
            OrderCardAsynchronous.asynchOrder.SendItem(order);
        }

        public void Complete(OrderCardInfo order)
        {
            OrderCardAsynchronous.asynchOrder.Complete(order);
        }

        public bool ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out Decimal ototalvalue)
        {
            allCompleted = false;
            opstate = string.Empty;
            ovalue = string.Empty;
            ototalvalue = new Decimal(0);
            OrderCardAsynchronous.asynchOrder.ItemComplete(order);
            return true;
        }
    }
}
