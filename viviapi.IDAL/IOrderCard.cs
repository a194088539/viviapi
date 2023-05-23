namespace viviapi.IDAL
{
    using System.Collections.Generic;
    using System.Data;
    using viviapi.Model.Order;
    using viviLib.Data;

    public interface IOrderCard
    {
        bool Complete(OrderCardInfo order);
        DataTable DataItemsByOrderId(string orderId);
        bool Deduct(string orderid);
        CardItemInfo GetItemModel(string orderId, int serial);
        OrderCardInfo GetModel(long ID);
        OrderCardInfo GetModel(string orderId);
        OrderCardInfo GetModel(long ID, int userid);
        long Insert(OrderCardInfo order);
        long InsertItem(CardItemInfo order);
        bool ItemComplete(CardItemInfo order, out bool allCompleted, out string opstate, out string ovalue, out decimal ototalvalue);
        bool Notify(OrderCardInfo order);
        DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby);
        bool ReDeduct(string orderid);
    }
}

