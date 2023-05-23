using System;

namespace viviapi.Model.Settled
{
    [Serializable]
    public class TocashSchemeInfo
    {
        private Decimal _minamtlimitofeach = new Decimal(100);
        private Decimal _maxamtlimitofeach = new Decimal(50000);
        private int _dailymaxtimes = 10;
        private Decimal _dailymaxamt = new Decimal(50000);
        private Decimal _chargerate = new Decimal(1, 0, 0, false, (byte)2);
        private Decimal _chargeleastofeach = new Decimal(10);
        private Decimal _chargemostofeach = new Decimal(50);
        private int _vaiInterface = 0;
        private int _bankdetentiondays = 0;
        private int _carddetentiondays = 0;
        private int _otherdetentiondays = 0;
        private byte _tranRequiredAudit = (byte)1;
        private int _type = 1;
        private int _id;
        private string _schemename;
        private int _isdefault;

        public int type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

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

        public int vaiInterface
        {
            get
            {
                return this._vaiInterface;
            }
            set
            {
                this._vaiInterface = value;
            }
        }

        public int bankdetentiondays
        {
            get
            {
                return this._bankdetentiondays;
            }
            set
            {
                this._bankdetentiondays = value;
            }
        }

        public int carddetentiondays
        {
            get
            {
                return this._carddetentiondays;
            }
            set
            {
                this._carddetentiondays = value;
            }
        }

        public int otherdetentiondays
        {
            get
            {
                return this._otherdetentiondays;
            }
            set
            {
                this._otherdetentiondays = value;
            }
        }

        public byte tranRequiredAudit
        {
            get
            {
                return this._tranRequiredAudit;
            }
            set
            {
                this._tranRequiredAudit = value;
            }
        }
    }
}
