namespace viviapi.Gateway.ETAPISdoCardPay
{
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using System.ServiceModel;

    [MessageContract(IsWrapped = false), GeneratedCode("System.ServiceModel", "3.0.0.0"), DebuggerStepThrough]
    public class PayRequest
    {
        [MessageBodyMember(Name = "Pay", Namespace = "http://www.sdp.com/", Order = 0)]
        public PayRequestBody Body;

        public PayRequest()
        {
        }

        public PayRequest(PayRequestBody Body)
        {
            this.Body = Body;
        }
    }
}

