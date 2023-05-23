using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace viviapi.ETAPI.ServiceReference1
{
    [DebuggerStepThrough]
    [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
    [DataContract(Name = "SDCardResult", Namespace = "http://www.sdp.com/")]
    [Serializable]
    public class SDCardResult : IExtensibleDataObject, INotifyPropertyChanged
    {
        [NonSerialized]
        private ExtensionDataObject extensionDataField;
        [OptionalField]
        private string CardNoField;
        [OptionalField]
        private string ErrorCodeField;
        [OptionalField]
        private string MessageField;

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

        [DataMember(EmitDefaultValue = false)]
        public string CardNo
        {
            get
            {
                return this.CardNoField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.CardNoField, (object)value))
                    return;
                this.CardNoField = value;
                this.RaisePropertyChanged("CardNo");
            }
        }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorCode
        {
            get
            {
                return this.ErrorCodeField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.ErrorCodeField, (object)value))
                    return;
                this.ErrorCodeField = value;
                this.RaisePropertyChanged("ErrorCode");
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
