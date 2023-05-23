using System;

namespace viviapi.Model.Settled
{
    [Serializable]
    public class transferscheme
    {
        private int _id = 0;
        private string _schemename;
        private Decimal _minamtlimitofeach;
        private Decimal _maxamtlimitofeach;
        private int _dailymaxtimes;
        private Decimal _dailymaxamt;
        private int _monthmaxtimes;
        private Decimal _monthmaxamt;
        private Decimal _chargerate;
        private Decimal _chargeleastofeach;
        private Decimal _chargemostofeach;
        private int _isdefault;

        public int id
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

        public string schemename
        {
            get
            {
                return this._schemename;
            }
            set
            {
                this._schemename = value;
            }
        }

        public Decimal minamtlimitofeach
        {
            get
            {
                return this._minamtlimitofeach;
            }
            set
            {
                this._minamtlimitofeach = value;
            }
        }

        public Decimal maxamtlimitofeach
        {
            get
            {
                return this._maxamtlimitofeach;
            }
            set
            {
                this._maxamtlimitofeach = value;
            }
        }

        public int dailymaxtimes
        {
            get
            {
                return this._dailymaxtimes;
            }
            set
            {
                this._dailymaxtimes = value;
            }
        }

        public Decimal dailymaxamt
        {
            get
            {
                return this._dailymaxamt;
            }
            set
            {
                this._dailymaxamt = value;
            }
        }

        public int monthmaxtimes
        {
            get
            {
                return this._monthmaxtimes;
            }
            set
            {
                this._monthmaxtimes = value;
            }
        }

        public Decimal monthmaxamt
        {
            get
            {
                return this._monthmaxamt;
            }
            set
            {
                this._monthmaxamt = value;
            }
        }

        public Decimal chargerate
        {
            get
            {
                return this._chargerate;
            }
            set
            {
                this._chargerate = value;
            }
        }

        public Decimal chargeleastofeach
        {
            get
            {
                return this._chargeleastofeach;
            }
            set
            {
                this._chargeleastofeach = value;
            }
        }

        public Decimal chargemostofeach
        {
            get
            {
                return this._chargemostofeach;
            }
            set
            {
                this._chargemostofeach = value;
            }
        }

        public int isdefault
        {
            get
            {
                return this._isdefault;
            }
            set
            {
                this._isdefault = value;
            }
        }
    }
}
