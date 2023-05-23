using System;
using viviapi.Model.Order;

namespace viviapi.IBLLStrategy
{
    public interface IOrderCardStrategy
    {
        void Insert(OrderCardInfo order);

        void InsertItem(CardItemInfo order);

        void Complete(OrderCardInfo order);

        bool ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out Decimal ototalvalue);
    }
}
