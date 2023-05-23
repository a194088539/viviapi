using System;

namespace viviapi.Model.APP
{
    [Serializable]
    public class EntrustInfo
    {
        private int _id;
        private int _userid;
        private int _status;
        private string _bankcardnum;
        private string _bankname;
        private string _payee;
        private Decimal _amount;
        private Decimal _rate;
        private Decimal _remittancefee;
        private Decimal _totalamt;
        private DateTime _addtime;
        private DateTime? _cdate;
        private int? _cadmin;
        private string _remark;

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

        public string bankcardnum
        {
            get
            {
                return this._bankcardnum;
            }
            set
            {
                this._bankcardnum = value;
            }
        }

        public string bankname
        {
            get
            {
                return this._bankname;
            }
            set
            {
                this._bankname = value;
            }
        }

        public string payee
        {
            get
            {
                return this._payee;
            }
            set
            {
                this._payee = value;
            }
        }

        public Decimal amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public Decimal rate
        {
            get
            {
                return this._rate;
            }
            set
            {
                this._rate = value;
            }
        }

        public Decimal remittancefee
        {
            get
            {
                return this._remittancefee;
            }
            set
            {
                this._remittancefee = value;
            }
        }

        public Decimal totalAmt
        {
            get
            {
                return this._totalamt;
            }
            set
            {
                this._totalamt = value;
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

        public DateTime? cdate
        {
            get
            {
                return this._cdate;
            }
            set
            {
                this._cdate = value;
            }
        }

        public int? cadmin
        {
            get
            {
                return this._cadmin;
            }
            set
            {
                this._cadmin = value;
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
    }
}
