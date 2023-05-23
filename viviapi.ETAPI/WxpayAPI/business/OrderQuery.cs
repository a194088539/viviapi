namespace viviapi.ETAPI.WxPayAPI
{
    public class OrderQuery
    {
        public static string Run(string out_trade_no)
        {
            Log.Info("OrderQuery", "OrderQuery is processing...");
            WxPayData inputObj = new WxPayData();
            inputObj.SetValue("out_trade_no", (object)out_trade_no);
            WxPayData wxPayData = WxPayApi.OrderQuery(inputObj, 6);
            Log.Info("OrderQuery", "OrderQuery process complete, result : " + wxPayData.ToXml());
            return wxPayData.ToPrintStr();
        }
    }
}
