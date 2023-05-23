using System;

namespace viviapi.Model.Settled
{
    [Serializable]
    public class transfer
    {
        private int _id;
        private int? _year;
        private int? _month;
        private int _userid;
        private int _touserid;
        private Decimal _amt;
        private Decimal _charge;
        private string _remark;
        private int _status;
        private DateTime _addtime;
        private DateTime? _updatetime;

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

        public int? year
        {
            get
            {
                return this._year;
            }
            set
            {
                this._year = value;
            }
        }

        public int? month
        {
            get
            {
                return this._month;
            }
            set
            {
                this._month = value;
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

        public int touserid
        {
            get
            {
                return this._touserid;
            }
            set
            {
                this._touserid = value;
            }
        }

        public Decimal amt
        {
            get
            {
                return this._amt;
            }
            set
            {
                this._amt = value;
            }
        }

        public Decimal charge
        {
            get
            {
                return this._charge;
            }
            set
            {
                this._charge = value;
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

        public DateTime? updatetime
        {
            get
            {
                return this._updatetime;
            }
            set
            {
                this._updatetime = value;
            }
        }
    }
}
