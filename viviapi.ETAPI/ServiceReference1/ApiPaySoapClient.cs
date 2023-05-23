using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace viviapi.ETAPI.ServiceReference1
{
    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "3.0.0.0")]
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
        PayResponse ApiPaySoap.Pay(Pay request)
        {
            return this.Channel.Pay(request);
        }

        public PayGateResponse Pay(PayGateRequest request)
        {
            Pay request1 = new Pay();
            request1.Body = new PayBody();
            request1.Body.request = request;
            return ((ApiPaySoap)this).Pay(request1).Body.PayResult;
        }
    }
}
