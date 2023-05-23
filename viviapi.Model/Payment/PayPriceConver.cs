namespace viviapi.Model.Payment
{
    using System;

    [Serializable]
    public class PayPriceConver
    {
        private int _conv_pritype = 0;
        private DateTime? _created;
        private int _id;
        private bool _IsOpen;
        private int _pri_type;
        private decimal _value;

        public int Conv_PriType
        {
            get
            {
                return this._conv_pritype;
            }
            set
            {
                this._conv_pritype = value;
            }
        }

        public DateTime? Created
        {
            get
            {
                return this._created;
            }
            set
            {
                this._created = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return this._IsOpen;
            }
            set
            {
                this._IsOpen = value;
            }
        }

        public int Pri_Type
        {
            get
            {
                return this._pri_type;
            }
            set
            {
                this._pri_type = value;
            }
        }

        public decimal Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }
}

