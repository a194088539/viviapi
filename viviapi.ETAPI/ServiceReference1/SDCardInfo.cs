using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace viviapi.ETAPI.ServiceReference1
{
    [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
    [DebuggerStepThrough]
    [DataContract(Name = "SDCardInfo", Namespace = "http://www.sdp.com/")]
    [Serializable]
    public class SDCardInfo : IExtensibleDataObject, INotifyPropertyChanged
    {
        [NonSerialized]
        private ExtensionDataObject extensionDataField;
        [OptionalField]
        private string CardNOField;
        [OptionalField]
        private string CardPasswordField;

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
        public string CardNO
        {
            get
            {
                return this.CardNOField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.CardNOField, (object)value))
                    return;
                this.CardNOField = value;
                this.RaisePropertyChanged("CardNO");
            }
        }

        [DataMember(EmitDefaultValue = false)]
        public string CardPassword
        {
            get
            {
                return this.CardPasswordField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.CardPasswordField, (object)value))
                    return;
                this.CardPasswordField = value;
                this.RaisePropertyChanged("CardPassword");
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
