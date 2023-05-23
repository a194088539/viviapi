using System;

namespace viviapi.Model.Withdraw
{
    [Serializable]
    public class settledAgentSummary
    {
        private int _succqty = 0;
        private Decimal _succamt = new Decimal(0);
        private Decimal _fee = new Decimal(0);
        private Decimal _realfee = new Decimal(0);
        private int _success = 1;
        private int _status = 1;
        private DateTime _addtime = DateTime.Now;
        private DateTime _updatetime = DateTime.Now;
        private int? _audit_status = new int?(1);
        private DateTime? _audittime = new DateTime?(DateTime.Now);
        private int? _audituser = new int?(0);
        private string _auditusername = "";
        private int _id;
        private int _userid;
        private string _lotno;
        private int _qty;
        private Decimal _amt;
        private Decimal? _totalamt;
        private Decimal? _totalsuccamt;
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

        public string lotno
        {
            get
            {
                return this._lotno;
            }
            set
            {
                this._lotno = value;
            }
        }

        public int qty
        {
            get
            {
                return this._qty;
            }
            set
            {
                this._qty = value;
            }
        }

        public int succqty
        {
            get
            {
                return this._succqty;
            }
            set
            {
                this._succqty = value;
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

        public Decimal succamt
        {
            get
            {
                return this._succamt;
            }
            set
            {
                this._succamt = value;
            }
        }

        public Decimal fee
        {
            get
            {
                return this._fee;
            }
            set
            {
                this._fee = value;
            }
        }

        public Decimal realfee
        {
            get
            {
                return this._realfee;
            }
            set
            {
                this._realfee = value;
            }
        }

        public Decimal? totalamt
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

        public Decimal? totalsuccamt
        {
            get
            {
                return this._totalsuccamt;
            }
            set
            {
                this._totalsuccamt = value;
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

        public int success
        {
            get
            {
                return this._success;
            }
            set
            {
                this._success = value;
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

        public DateTime updatetime
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

        public int? audit_status
        {
            get
            {
                return this._audit_status;
            }
            set
            {
                this._audit_status = value;
            }
        }

        public DateTime? auditTime
        {
            get
            {
                return this._audittime;
            }
            set
            {
                this._audittime = value;
            }
        }

        public int? auditUser
        {
            get
            {
                return this._audituser;
            }
            set
            {
                this._audituser = value;
            }
        }

        public string auditUserName
        {
            get
            {
                return this._auditusername;
            }
            set
            {
                this._auditusername = value;
            }
        }
    }
}
