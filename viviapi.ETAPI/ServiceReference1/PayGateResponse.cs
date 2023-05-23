using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace viviapi.ETAPI.ServiceReference1
{
    [DataContract(Name = "PayGateResponse", Namespace = "http://www.sdp.com/")]
    [DebuggerStepThrough]
    [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
    [Serializable]
    public class PayGateResponse : IExtensibleDataObject, INotifyPropertyChanged
    {
        [NonSerialized]
        private ExtensionDataObject extensionDataField;
        private int CodeField;
        [OptionalField]
        private string MessageField;
        [OptionalField]
        private string BillNoField;
        [OptionalField]
        private string OrderNoField;
        private Decimal AmountField;
        [OptionalField]
        private SDCardResult[] CardsField;

        [Browsable(false)]
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [DataMember(IsRequired = true)]
        public int Code
        {
            get
            {
                return this.CodeField;
            }
            set
            {
                if (this.CodeField.Equals(value))
                    return;
                this.CodeField = value;
                this.RaisePropertyChanged("Code");
            }
        }

        [DataMember(EmitDefaultValue = false)]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.MessageField, (object)value))
                    return;
                this.MessageField = value;
                this.RaisePropertyChanged("Message");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string BillNo
        {
            get
            {
                return this.BillNoField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.BillNoField, (object)value))
                    return;
                this.BillNoField = value;
                this.RaisePropertyChanged("BillNo");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 3)]
        public string OrderNo
        {
            get
            {
                return this.OrderNoField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.OrderNoField, (object)value))
                    return;
                this.OrderNoField = value;
                this.RaisePropertyChanged("OrderNo");
            }
        }

        [DataMember(IsRequired = true, Order = 4)]
        public Decimal Amount
        {
            get
            {
                return this.AmountField;
            }
            set
            {
                if (this.AmountField.Equals(value))
                    return;
                this.AmountField = value;
                this.RaisePropertyChanged("Amount");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 5)]
        public SDCardResult[] Cards
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
            if (changedEventHandler == null)
                return;
            changedEventHandler((object)this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
