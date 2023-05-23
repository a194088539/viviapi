using System;

namespace viviapi.Model.APP
{
    [Serializable]
    public class apprecharge
    {
        private int _id = 0;
        private int _paytype = 0;
        private int _rechtype = 0;
        private string _orderid = string.Empty;
        private string _account = string.Empty;
        private int _userid = 0;
        private Decimal _rechargeamt = new Decimal(0);
        private Decimal? _realpayamt = new Decimal?(new Decimal(0));
        private DateTime _addtime = DateTime.Now;
        private int _status = 0;
        private int _processstatus = 0;
        private DateTime? _processtime = new DateTime?(DateTime.Now);
        private bool _smsnotification = false;
        private string _field1 = string.Empty;
        private string _field2 = string.Empty;
        private string _remark = string.Empty;
        private int _suppid = 0;

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

        public int paytype
        {
            get
            {
                return this._paytype;
            }
            set
            {
                this._paytype = value;
            }
        }

        public int rechtype
        {
            get
            {
                return this._rechtype;
            }
            set
            {
                this._rechtype = value;
            }
        }

        public string orderid
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }

        public string account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
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

        public Decimal rechargeAmt
        {
            get
            {
                return this._rechargeamt;
            }
            set
            {
                this._rechargeamt = value;
            }
        }

        public Decimal? realPayAmt
        {
            get
            {
                return this._realpayamt;
            }
            set
            {
                this._realpayamt = value;
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

        public int processstatus
        {
            get
            {
                return this._processstatus;
            }
            set
            {
                this._processstatus = value;
            }
        }

        public DateTime? processtime
        {
            get
            {
                return this._processtime;
            }
            set
            {
                this._processtime = value;
            }
        }

        public bool smsnotification
        {
            get
            {
                return this._smsnotification;
            }
            set
            {
                this._smsnotification = value;
            }
        }

        public string field1
        {
            get
            {
                return this._field1;
            }
            set
            {
                this._field1 = value;
            }
        }

        public string field2
        {
            get
            {
                return this._field2;
            }
            set
            {
                this._field2 = value;
            }
        }

        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
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
    }
}
