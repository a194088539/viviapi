namespace viviapi.Gateway.ETAPISdoCardPay
{
    using System.CodeDom.Compiler;
    using System.ServiceModel;

    [ServiceContract(Namespace = "http://www.sdp.com/", ConfigurationName = "ETAPISdoCardPay.ApiPaySoap"), GeneratedCode("System.ServiceModel", "3.0.0.0")]
    public interface ApiPaySoap
    {
        [OperationContract(Action = "http://www.sdp.com/Pay", ReplyAction = "*")]
        PayResponse Pay(PayRequest request);
    }
}

