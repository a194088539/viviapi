using System;
using System.Collections.Generic;

namespace viviapi.Model.Financial
{
    [Serializable]
    public class tenpay_batch_trans_head
    {
        private int _id = 0;
        private string _package_id = string.Empty;
        private int _total_num = 0;
        private Decimal _total_amt = new Decimal(0);
        private int _status = 0;
        private string _version = string.Empty;
        private string _client_ip = string.Empty;
        private string _op_user = string.Empty;
        private DateTime _op_time = DateTime.Now;
        private DateTime? _completetime = new DateTime?(DateTime.Now);
        private string _retcode = string.Empty;
        private string _message = string.Empty;
        private string _retcontext = string.Empty;
        private int? _success = new int?(0);
        private int? _fail = new int?(0);
        private int? _uncertain = new int?(0);
        private List<tenpay_batch_trans_detail> _items = new List<tenpay_batch_trans_detail>();

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

        public int total_num
        {
            get
            {
                return this._total_num;
            }
            set
            {
                this._total_num = value;
            }
        }

        public Decimal total_amt
        {
            get
            {
                return this._total_amt;
            }
            set
            {
                this._total_amt = value;
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

        public string version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }

        public string client_ip
        {
            get
            {
                return this._client_ip;
            }
            set
            {
                this._client_ip = value;
            }
        }

        public string op_user
        {
            get
            {
                return this._op_user;
            }
            set
            {
                this._op_user = value;
            }
        }

        public DateTime op_time
        {
            get
            {
                return this._op_time;
            }
            set
            {
                this._op_time = value;
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

        public string retcode
        {
            get
            {
                return this._retcode;
            }
            set
            {
                this._retcode = value;
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

        public string retcontext
        {
            get
            {
                return this._retcontext;
            }
            set
            {
                this._retcontext = value;
            }
        }

        public int? success
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

        public int? fail
        {
            get
            {
                return this._fail;
            }
            set
            {
                this._fail = value;
            }
        }

        public int? uncertain
        {
            get
            {
                return this._uncertain;
            }
            set
            {
                this._uncertain = value;
            }
        }

        public List<tenpay_batch_trans_detail> items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }
    }
}
