using System.CodeDom.Compiler;
using System.ServiceModel;

namespace viviapi.ETAPI.ServiceReference1
{
    [GeneratedCode("System.ServiceModel", "3.0.0.0")]
    [ServiceContract(ConfigurationName = "ServiceReference1.ApiPaySoap", Namespace = "http://www.sdp.com/")]
    public interface ApiPaySoap
    {
        [OperationContract(Action = "http://www.sdp.com/Pay", ReplyAction = "*")]
        PayResponse Pay(Pay request);
    }
}
