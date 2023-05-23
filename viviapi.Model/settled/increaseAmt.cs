using System;

namespace viviapi.Model.Settled
{
    public class IncreaseAmtInfo
    {
        private int _id;
        private int? _userid;
        private Decimal? _increaseamt;
        private DateTime? _addtime;
        private int? _mangeid;
        private string _mangename;
        private int? _status;
        private string _desc;
        private optypeenum _optype;

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

        public int? userId
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

        public Decimal? increaseAmt
        {
            get
            {
                return this._increaseamt;
            }
            set
            {
                this._increaseamt = value;
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

        public int? mangeId
        {
            get
            {
                return this._mangeid;
            }
            set
            {
                this._mangeid = value;
            }
        }

        public string mangeName
        {
            get
            {
                return this._mangename;
            }
            set
            {
                this._mangename = value;
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

        public string desc
        {
            get
            {
                return this._desc;
            }
            set
            {
                this._desc = value;
            }
        }

        public optypeenum optype
        {
            get
            {
                return this._optype;
            }
            set
            {
                this._optype = value;
            }
        }
    }
}
