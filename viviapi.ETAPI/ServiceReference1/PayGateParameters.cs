using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace viviapi.ETAPI.ServiceReference1
{
    [DataContract(Name = "PayGateParameters", Namespace = "http://www.sdp.com/")]
    [DebuggerStepThrough]
    [KnownType(typeof(PayGateRequest))]
    [GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
    [Serializable]
    public class PayGateParameters : IExtensibleDataObject, INotifyPropertyChanged
    {
        [NonSerialized]
        private ExtensionDataObject extensionDataField;
        [OptionalField]
        private string MacField;
        [OptionalField]
        private string VersionField;
        [OptionalField]
        private string AmountField;
        [OptionalField]
        private string OrderNoField;
        [OptionalField]
        private string MerchantNoField;
        [OptionalField]
        private string MerchantUserIdField;
        [OptionalField]
        private string NotifyUrlField;
        [OptionalField]
        private string OrderTimeField;
        [OptionalField]
        private string CurrencyTypeField;
        [OptionalField]
        private string NotifyUrlTypeField;
        [OptionalField]
        private string SignTypeField;
        [OptionalField]
        private string ProductNoField;
        [OptionalField]
        private string ProductDescField;
        [OptionalField]
        private string Remark1Field;
        [OptionalField]
        private string Remark2Field;
        [OptionalField]
        private string ProductUrlField;
        [OptionalField]
        private string CharSetField;
        [OptionalField]
        private string ProductNameField;
        [OptionalField]
        private string RoyaltyTypeField;
        [OptionalField]
        private string RoyaltyParametersField;
        [OptionalField]
        private string OverTimeField;
        [OptionalField]
        private string ProxyMerchantNoField;
        [OptionalField]
        private string MerDisplayNameField;

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
        public string Mac
        {
            get
            {
                return this.MacField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.MacField, (object)value))
                    return;
                this.MacField = value;
                this.RaisePropertyChanged("Mac");
            }
        }

        [DataMember(EmitDefaultValue = false)]
        public string Version
        {
            get
            {
                return this.VersionField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.VersionField, (object)value))
                    return;
                this.VersionField = value;
                this.RaisePropertyChanged("Version");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Amount
        {
            get
            {
                return this.AmountField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.AmountField, (object)value))
                    return;
                this.AmountField = value;
                this.RaisePropertyChanged("Amount");
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

        [DataMember(EmitDefaultValue = false, Order = 4)]
        public string MerchantNo
        {
            get
            {
                return this.MerchantNoField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.MerchantNoField, (object)value))
                    return;
                this.MerchantNoField = value;
                this.RaisePropertyChanged("MerchantNo");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 5)]
        public string MerchantUserId
        {
            get
            {
                return this.MerchantUserIdField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.MerchantUserIdField, (object)value))
                    return;
                this.MerchantUserIdField = value;
                this.RaisePropertyChanged("MerchantUserId");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 6)]
        public string NotifyUrl
        {
            get
            {
                return this.NotifyUrlField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.NotifyUrlField, (object)value))
                    return;
                this.NotifyUrlField = value;
                this.RaisePropertyChanged("NotifyUrl");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 7)]
        public string OrderTime
        {
            get
            {
                return this.OrderTimeField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.OrderTimeField, (object)value))
                    return;
                this.OrderTimeField = value;
                this.RaisePropertyChanged("OrderTime");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 8)]
        public string CurrencyType
        {
            get
            {
                return this.CurrencyTypeField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.CurrencyTypeField, (object)value))
                    return;
                this.CurrencyTypeField = value;
                this.RaisePropertyChanged("CurrencyType");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 9)]
        public string NotifyUrlType
        {
            get
            {
                return this.NotifyUrlTypeField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.NotifyUrlTypeField, (object)value))
                    return;
                this.NotifyUrlTypeField = value;
                this.RaisePropertyChanged("NotifyUrlType");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 10)]
        public string SignType
        {
            get
            {
                return this.SignTypeField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.SignTypeField, (object)value))
                    return;
                this.SignTypeField = value;
                this.RaisePropertyChanged("SignType");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 11)]
        public string ProductNo
        {
            get
            {
                return this.ProductNoField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.ProductNoField, (object)value))
                    return;
                this.ProductNoField = value;
                this.RaisePropertyChanged("ProductNo");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 12)]
        public string ProductDesc
        {
            get
            {
                return this.ProductDescField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.ProductDescField, (object)value))
                    return;
                this.ProductDescField = value;
                this.RaisePropertyChanged("ProductDesc");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 13)]
        public string Remark1
        {
            get
            {
                return this.Remark1Field;
            }
            set
            {
                if (object.ReferenceEquals((object)this.Remark1Field, (object)value))
                    return;
                this.Remark1Field = value;
                this.RaisePropertyChanged("Remark1");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 14)]
        public string Remark2
        {
            get
            {
                return this.Remark2Field;
            }
            set
            {
                if (object.ReferenceEquals((object)this.Remark2Field, (object)value))
                    return;
                this.Remark2Field = value;
                this.RaisePropertyChanged("Remark2");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 15)]
        public string ProductUrl
        {
            get
            {
                return this.ProductUrlField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.ProductUrlField, (object)value))
                    return;
                this.ProductUrlField = value;
                this.RaisePropertyChanged("ProductUrl");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 16)]
        public string CharSet
        {
            get
            {
                return this.CharSetField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.CharSetField, (object)value))
                    return;
                this.CharSetField = value;
                this.RaisePropertyChanged("CharSet");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 17)]
        public string ProductName
        {
            get
            {
                return this.ProductNameField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.ProductNameField, (object)value))
                    return;
                this.ProductNameField = value;
                this.RaisePropertyChanged("ProductName");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 18)]
        public string RoyaltyType
        {
            get
            {
                return this.RoyaltyTypeField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.RoyaltyTypeField, (object)value))
                    return;
                this.RoyaltyTypeField = value;
                this.RaisePropertyChanged("RoyaltyType");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 19)]
        public string RoyaltyParameters
        {
            get
            {
                return this.RoyaltyParametersField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.RoyaltyParametersField, (object)value))
                    return;
                this.RoyaltyParametersField = value;
                this.RaisePropertyChanged("RoyaltyParameters");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 20)]
        public string OverTime
        {
            get
            {
                return this.OverTimeField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.OverTimeField, (object)value))
                    return;
                this.OverTimeField = value;
                this.RaisePropertyChanged("OverTime");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 21)]
        public string ProxyMerchantNo
        {
            get
            {
                return this.ProxyMerchantNoField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.ProxyMerchantNoField, (object)value))
                    return;
                this.ProxyMerchantNoField = value;
                this.RaisePropertyChanged("ProxyMerchantNo");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 22)]
        public string MerDisplayName
        {
            get
            {
                return this.MerDisplayNameField;
            }
            set
            {
                if (object.ReferenceEquals((object)this.MerDisplayNameField, (object)value))
                    return;
                this.MerDisplayNameField = value;
                this.RaisePropertyChanged("MerDisplayName");
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
