using System;

namespace viviapi.Model
{
    [Serializable]
    public class distribution
    {
        private int _batchno = 1;
        private string _supp_trade_no = string.Empty;
        private DateTime _addtime = DateTime.Now;
        private DateTime _processingtime = DateTime.Now;
        private string _supp_message = string.Empty;
        private int _status = (int)byte.MaxValue;
        private string _ext1 = string.Empty;
        private string _ext2 = string.Empty;
        private string _ext3 = string.Empty;
        private string _remark = string.Empty;
        private int _id;
        private int _suppid;
        private int? _mode;
        private int? _settledid;
        private string _trade_no;
        private int _userid;
        private Decimal _balance;
        private string _bankcode;
        private string _bankname;
        private string _bankbranch;
        private string _bankaccountname;
        private string _bankaccount;
        private Decimal _amount;
        private Decimal _charges;
        private Decimal? _balance2;

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

        public int? mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
            }
        }

        public int? settledId
        {
            get
            {
                return this._settledid;
            }
            set
            {
                this._settledid = value;
            }
        }

        public string trade_no
        {
            get
            {
                return this._trade_no;
            }
            set
            {
                this._trade_no = value;
            }
        }

        public int batchNo
        {
            get
            {
                return this._batchno;
            }
            set
            {
                this._batchno = value;
            }
        }

        public string supp_trade_no
        {
            get
            {
                return this._supp_trade_no;
            }
            set
            {
                this._supp_trade_no = value;
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

        public Decimal balance
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

        public string bankCode
        {
            get
            {
                return this._bankcode;
            }
            set
            {
                this._bankcode = value;
            }
        }

        public string bankName
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

        public string bankBranch
        {
            get
            {
                return this._bankbranch;
            }
            set
            {
                this._bankbranch = value;
            }
        }

        public string bankAccountName
        {
            get
            {
                return this._bankaccountname;
            }
            set
            {
                this._bankaccountname = value;
            }
        }

        public string bankAccount
        {
            get
            {
                return this._bankaccount;
            }
            set
            {
                this._bankaccount = value;
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

        public Decimal charges
        {
            get
            {
                return this._charges;
            }
            set
            {
                this._charges = value;
            }
        }

        public Decimal? balance2
        {
            get
            {
                return this._balance2;
            }
            set
            {
                this._balance2 = value;
            }
        }

        public DateTime addTime
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

        public DateTime processingTime
        {
            get
            {
                return this._processingtime;
            }
            set
            {
                this._processingtime = value;
            }
        }

        public string supp_message
        {
            get
            {
                return this._supp_message;
            }
            set
            {
                this._supp_message = value;
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

        public string ext1
        {
            get
            {
                return this._ext1;
            }
            set
            {
                this._ext1 = value;
            }
        }

        public string ext2
        {
            get
            {
                return this._ext2;
            }
            set
            {
                this._ext2 = value;
            }
        }

        public string ext3
        {
            get
            {
                return this._ext3;
            }
            set
            {
                this._ext3 = value;
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
