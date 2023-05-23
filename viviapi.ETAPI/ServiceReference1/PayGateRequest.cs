using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace viviapi.ETAPI.ServiceReference1
{
    [DebuggerStepThrough]
    [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
    [DataContract(Name = "PayGateRequest", Namespace = "http://www.sdp.com/")]
    [Serializable]
    public class PayGateRequest : PayGateParameters
    {
        [OptionalField]
        private string PostBackUrlField;
        [OptionalField]
        private SDCardInfo[] CardsField;

        [DataMember(EmitDefaultValue = false)]
        public string PostBackUrl
        {
            get
            {
                return this.PostBackUrlField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.PostBackUrlField, (object)value))
                    return;
                this.PostBackUrlField = value;
                this.RaisePropertyChanged("PostBackUrl");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 1)]
        public SDCardInfo[] Cards
        {
            get
            {
                return this.CardsField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.CardsField, (object)value))
                    return;
                this.CardsField = value;
                this.RaisePropertyChanged("Cards");
            }
        }
    }
}
