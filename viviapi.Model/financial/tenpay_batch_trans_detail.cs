using System;

namespace viviapi.Model.Financial
{
    [Serializable]
    public class tenpay_batch_trans_detail
    {
        private int _id = 0;
        private int _hid = 0;
        private int _serial = 0;
        private int _settleid = 0;
        private string _package_id = string.Empty;
        private int _userid = 0;
        private Decimal _balance = new Decimal(0);
        private int _status = 1;
        private string _rec_acc = string.Empty;
        private string _rec_name = string.Empty;
        private string _cur_type = string.Empty;
        private Decimal _pay_amt = new Decimal(0);
        private Decimal? _succ_amt = new Decimal?(new Decimal(0));
        private string _remark = string.Empty;
        private string _trans_id = string.Empty;
        private string _message = string.Empty;

        public int settleid
        {
            get
            {
                return this._settleid;
            }
            set
            {
                this._settleid = value;
            }
        }

        public string package_id
        {
            get
            {
                return this._package_id;
            }
            set
            {
                this._package_id = value;
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

        public int hid
        {
            get
            {
                return this._hid;
            }
            set
            {
                this._hid = value;
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

        public string rec_acc
        {
            get
            {
                return this._rec_acc;
            }
            set
            {
                this._rec_acc = value;
            }
        }

        public string rec_name
        {
            get
            {
                return this._rec_name;
            }
            set
            {
                this._rec_name = value;
            }
        }

        public string cur_type
        {
            get
            {
                return this._cur_type;
            }
            set
            {
                this._cur_type = value;
            }
        }

        public Decimal pay_amt
        {
            get
            {
                return this._pay_amt;
            }
            set
            {
                this._pay_amt = value;
            }
        }

        public Decimal? succ_amt
        {
            get
            {
                return this._succ_amt;
            }
            set
            {
                this._succ_amt = value;
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

        public string trans_id
        {
            get
            {
                return this._trans_id;
            }
            set
            {
                this._trans_id = value;
            }
        }

        public string message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }
    }
}
