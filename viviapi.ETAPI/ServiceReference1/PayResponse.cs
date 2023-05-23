using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace viviapi.ETAPI.ServiceReference1
{
    [MessageContract(IsWrapped = false)]
    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "3.0.0.0")]
    public class PayResponse
    {
        [MessageBodyMember(Name = "PayResponse", Namespace = "http://www.sdp.com/", Order = 0)]
        public PayResponseBody Body;

        public PayResponse()
        {
        }

        public PayResponse(PayResponseBody Body)
        {
            this.Body = Body;
        }
    }
}
