namespace viviapi.Gateway.ETAPISdoCardPay
{
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using viviapi.ETAPI.ServiceReference1;

    [GeneratedCode("System.ServiceModel", "3.0.0.0"), DebuggerStepThrough]
    public class ApiPaySoapClient : ClientBase<viviapi.Gateway.ETAPISdoCardPay.ApiPaySoap>, viviapi.Gateway.ETAPISdoCardPay.ApiPaySoap
    {
        public ApiPaySoapClient()
        {
        }

        public ApiPaySoapClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public ApiPaySoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public ApiPaySoapClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public ApiPaySoapClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public PayGateResponse Pay(PayGateRequest request)
        {
            PayRequest inValue = new PayRequest();
            inValue.Body = new PayRequestBody();
            inValue.Body.request = request;
            return ((viviapi.Gateway.ETAPISdoCardPay.ApiPaySoap)this).Pay(inValue).Body.PayResult;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        viviapi.Gateway.ETAPISdoCardPay.PayResponse viviapi.Gateway.ETAPISdoCardPay.ApiPaySoap.Pay(PayRequest request)
        {
            return base.Channel.Pay(request);
        }
    }
}

