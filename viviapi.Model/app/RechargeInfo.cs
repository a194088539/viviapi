using System;

namespace viviapi.Model.APP
{
    [Serializable]
    public class RechargeInfo
    {
        private int _id;
        private int? _userid;
        private string _orderno;
        private Decimal? _rechargeamt;
        private Decimal? _balance;
        private DateTime? _addtime;
        private int? _status;
        private DateTime? _paytime;
        private string _transno;
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

        public int? userid
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

        public string orderno
        {
            get
            {
                return this._orderno;
            }
            set
            {
                this._orderno = value;
            }
        }

        public Decimal? rechargeAmt
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

        public Decimal? balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this._balance = value;
            }
        }

        public DateTime? addtime
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

        public int? status
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

        public DateTime? paytime
        {
            get
            {
                return this._paytime;
            }
            set
            {
                this._paytime = value;
            }
        }

        public string transNo
        {
            get
            {
                return this._transno;
            }
            set
            {
                this._transno = value;
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
