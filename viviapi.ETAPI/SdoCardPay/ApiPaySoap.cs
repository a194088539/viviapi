using System.CodeDom.Compiler;
using System.ServiceModel;

namespace viviapi.Gateway.ETAPISdoCardPay
{
  [ServiceContract(ConfigurationName = "ETAPISdoCardPay.ApiPaySoap", Namespace = "http://www.sdp.com/")]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  public interface ApiPaySoap
  {
    [OperationContract(Action = "http://www.sdp.com/Pay", ReplyAction = "*")]
    PayResponse Pay(PayRequest request);
  }
}
