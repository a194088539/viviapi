using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using viviapi.ETAPI.ServiceReference1;

namespace viviapi.Gateway.ETAPISdoCardPay
{
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [DebuggerStepThrough]
  public class ApiPaySoapClient : ClientBase<ApiPaySoap>, ApiPaySoap
  {
    public ApiPaySoapClient()
    {
    }

    public ApiPaySoapClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public ApiPaySoapClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ApiPaySoapClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ApiPaySoapClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    PayResponse ApiPaySoap.Pay(PayRequest request)
    {
      return this.Channel.Pay(request);
    }

    public PayGateResponse Pay(PayGateRequest request)
    {
      PayRequest request1 = new PayRequest();
      request1.Body = new PayRequestBody();
      request1.Body.request = request;
      return ((ApiPaySoap) this).Pay(request1).Body.PayResult;
    }
  }
}
