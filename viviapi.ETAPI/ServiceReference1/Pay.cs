using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;

namespace viviapi.ETAPI.ServiceReference1
{
    [GeneratedCode("System.ServiceModel", "3.0.0.0")]
    [DebuggerStepThrough]
    [MessageContract(IsWrapped = false)]
    public class Pay
    {
        [MessageBodyMember(Name = "Pay", Namespace = "http://www.sdp.com/", Order = 0)]
        public PayBody Body;

        public Pay()
        {
        }

        public Pay(PayBody Body)
        {
            this.Body = Body;
        }
    }
}
