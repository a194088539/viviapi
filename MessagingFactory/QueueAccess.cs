namespace viviapi.MessagingFactory
{
    using System.Reflection;
    using viviapi.IMessaging;
    using viviapi.SysConfig;

    public sealed class QueueAccess
    {
        private static readonly string path = MSMQSetting.OrderMessaging;

        private QueueAccess()
        {
        }

        public static IOrderBank CreateBankOrder()
        {
            string typeName = path + ".OrderBank";
            return (IOrderBank)Assembly.Load(path).CreateInstance(typeName);
        }

        public static IOrderCard CreateCardOrder()
        {
            string typeName = path + ".OrderCard";
            return (IOrderCard)Assembly.Load(path).CreateInstance(typeName);
        }

        public static IOrderSms CreateSmsOrder()
        {
            string typeName = path + ".OrderSms";
            return (IOrderSms)Assembly.Load(path).CreateInstance(typeName);
        }

        public static IOrderBankNotify OrderBankNotify()
        {
            string typeName = path + ".OrderBankNotify";
            return (IOrderBankNotify)Assembly.Load(path).CreateInstance(typeName);
        }

        public static IOrderCardNotify OrderCardNotify()
        {
            string typeName = path + ".OrderCardNotify";
            return (IOrderCardNotify)Assembly.Load(path).CreateInstance(typeName);
        }

        public static IOrderSmsNotify OrderSmsNotify()
        {
            string typeName = path + ".OrderSmsNotify";
            return (IOrderSmsNotify)Assembly.Load(path).CreateInstance(typeName);
        }
    }
}

