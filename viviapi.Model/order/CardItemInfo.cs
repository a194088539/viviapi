using System;

namespace viviapi.Model.Order
{
    [Serializable]
    public class CardItemInfo
    {
        private Decimal _supplierrate = new Decimal(0);
        private Decimal _promrate = new Decimal(0);
        private Decimal _commission = new Decimal(0);
        private int _agent = 0;
        private long _id;
        private int _userid;
        private int _serial;
        private string _porderid;
        private int _suppid;
        private int _cardtype;
        private string _cardno;
        private string _cardpwd;
        private Decimal? _refervalue;
        private Decimal? _payrate;
        private DateTime _addtime;
        private string _supplierorder;
        private Decimal _realvalue;
        private int _status;
        private string _opstate;
        private string _msg;
        private DateTime? _completetime;

        public long id
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

        public int userid
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public int serial
        {
            get
            {
                return this._serial;
            }
            set
            {
                this._serial = value;
            }
        }

        public string porderid
        {
            get
            {
                return this._porderid;
            }
            set
            {
                this._porderid = value;
            }
        }

        public int suppid
        {
            get
            {
                return this._suppid;
            }
            set
            {
                this._suppid = value;
            }
        }

        public int cardtype
        {
            get
            {
                return this._cardtype;
            }
            set
            {
                this._cardtype = value;
            }
        }

        public string cardno
        {
            get
            {
                return this._cardno;
            }
            set
            {
                this._cardno = value;
            }
        }

        public string cardpwd
        {
            get
            {
                return this._cardpwd;
            }
            set
            {
                this._cardpwd = value;
            }
        }

        public Decimal? refervalue
        {
            get
            {
                return this._refervalue;
            }
            set
            {
                this._refervalue = value;
            }
        }

        public Decimal? payrate
        {
            get
            {
                return this._payrate;
            }
            set
            {
                this._payrate = value;
            }
        }

        public DateTime addtime
        {
            get
            {
                return this._addtime;
            }
            set
            {
                this._addtime = value;
            }
        }

        public string supplierOrder
        {
            get
            {
                return this._supplierorder;
            }
            set
            {
                this._supplierorder = value;
            }
        }

        public Decimal realvalue
        {
            get
            {
                return this._realvalue;
            }
            set
            {
                this._realvalue = value;
            }
        }

        public int status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public string opstate
        {
            get
            {
                return this._opstate;
            }
            set
            {
                this._opstate = value;
            }
        }

        public string msg
        {
            get
            {
                return this._msg;
            }
            set
            {
                this._msg = value;
            }
        }

        public DateTime? completetime
        {
            get
            {
                return this._completetime;
            }
            set
            {
                this._completetime = value;
            }
        }

        public Decimal supplierrate
        {
            get
            {
                return this._supplierrate;
            }
            set
            {
                this._supplierrate = value;
            }
        }

        public Decimal promrate
        {
            get
            {
                return this._promrate;
            }
            set
            {
                this._promrate = value;
            }
        }

        public Decimal commission
        {
            get
            {
                return this._commission;
            }
            set
            {
                this._commission = value;
            }
        }

        public int agentId
        {
            get
            {
                return this._agent;
            }
            set
            {
                this._agent = value;
            }
        }
    }
}
