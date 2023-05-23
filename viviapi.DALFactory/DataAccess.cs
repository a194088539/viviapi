namespace viviapi.DALFactory
{
    using System.Reflection;
    using viviapi.IDAL;
    using viviapi.SysConfig;

    public class DataAccess
    {
        private static readonly string orderPath = RuntimeSetting.OrdersDAL;
        private static readonly string path = RuntimeSetting.WebDAL;

        public static IOrderBank CreateOrderBank()
        {
            string typeName = path + ".OrderBank";
            return (IOrderBank)Assembly.Load(orderPath).CreateInstance(typeName);
        }

        public static IOrderCard CreateOrderCard()
        {
            string typeName = path + ".OrderCard";
            return (IOrderCard)Assembly.Load(orderPath).CreateInstance(typeName);
        }

        public static IOrderSms CreateOrderSms()
        {
            string typeName = path + ".OrderSms";
            return (IOrderSms)Assembly.Load(orderPath).CreateInstance(typeName);
        }
    }
}

