namespace viviapi.Gateway.ETAPISdoCardPay
{
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using viviapi.ETAPI.ServiceReference1;

    [DataContract(Namespace = "http://www.sdp.com/"), GeneratedCode("System.ServiceModel", "3.0.0.0"), DebuggerStepThrough]
    public class PayRequestBody
    {
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public PayGateRequest request;

        public PayRequestBody()
        {
        }

        public PayRequestBody(PayGateRequest request)
        {
            this.request = request;
        }
    }
}

