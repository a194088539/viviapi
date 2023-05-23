using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace viviapi.ETAPI.ServiceReference1
{
    [DataContract(Namespace = "http://www.sdp.com/")]
    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "3.0.0.0")]
    public class PayResponseBody
    {
        [DataMember(EmitDefaultValue = false, Order = 0)]
        public PayGateResponse PayResult;

        public PayResponseBody()
        {
        }

        public PayResponseBody(PayGateResponse PayResult)
        {
            this.PayResult = PayResult;
        }
    }
}
