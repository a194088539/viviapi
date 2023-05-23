using System;

namespace viviapi.Model.Order
{
    [Serializable]
    public class OrderSmsInfo
    {
        private int _notifycount = 0;
        private int _notifystat = 1;
        private bool _issucc = false;
        public string _errcode = string.Empty;
        private int _id;
        private string _orderid;
        private string _userorder;
        private int _supplierid;
        private int _userid;
        private string _mobile;
        private Decimal _fee;
        private string _message;
        private string _servicenum;
        private string _linkid;
        private string _gwid;
        private Decimal _payrate;
        private Decimal _supplierrate;
        private Decimal _promrate;
        private Decimal _payamt;
        private Decimal _promamt;
        private Decimal _supplieramt;
        private Decimal _profits;
        private int _server;
        private DateTime _addtime;
        private DateTime _completetime;
        private string _notifyurl;
        private string _againnotifyurl;
        private string _notifycontext;

        public int status { get; set; }

        public string opstate { get; set; }

        public string msg { get; set; }

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

        public string userorder
        {
            get
            {
                return this._userorder;
            }
            set
            {
                this._userorder = value;
            }
        }

        public int supplierId
        {
            get
            {
                return this._supplierid;
            }
            set
            {
                this._supplierid = value;
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

        public string mobile
        {
            get
            {
                return this._mobile;
            }
            set
            {
                this._mobile = value;
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

        public string servicenum
        {
            get
            {
                return this._servicenum;
            }
            set
            {
                this._servicenum = value;
            }
        }

        public string linkid
        {
            get
            {
                return this._linkid;
            }
            set
            {
                this._linkid = value;
            }
        }

        public string gwid
        {
            get
            {
                return this._gwid;
            }
            set
            {
                this._gwid = value;
            }
        }

        public Decimal payRate
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

        public Decimal supplierRate
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

        public Decimal promRate
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

        public Decimal payAmt
        {
            get
            {
                return this._payamt;
            }
            set
            {
                this._payamt = value;
            }
        }

        public Decimal promAmt
        {
            get
            {
                return this._promamt;
            }
            set
            {
                this._promamt = value;
            }
        }

        public Decimal supplierAmt
        {
            get
            {
                return this._supplieramt;
            }
            set
            {
                this._supplieramt = value;
            }
        }

        public Decimal profits
        {
            get
            {
                return this._profits;
            }
            set
            {
                this._profits = value;
            }
        }

        public int server
        {
            get
            {
                return this._server;
            }
            set
            {
                this._server = value;
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

        public DateTime completetime
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

        public string notifyurl
        {
            get
            {
                return this._notifyurl;
            }
            set
            {
                this._notifyurl = value;
            }
        }

        public string againNotifyUrl
        {
            get
            {
                return this._againnotifyurl;
            }
            set
            {
                this._againnotifyurl = value;
            }
        }

        public int notifycount
        {
            get
            {
                return this._notifycount;
            }
            set
            {
                this._notifycount = value;
            }
        }

        public int notifystat
        {
            get
            {
                return this._notifystat;
            }
            set
            {
                this._notifystat = value;
            }
        }

        public string notifycontext
        {
            get
            {
                return this._notifycontext;
            }
            set
            {
                this._notifycontext = value;
            }
        }

        public string Cmd { get; set; }

        public string userMsgContenct { get; set; }

        public int? manageId { get; set; }

        public Decimal? commission { get; set; }

        public bool issucc
        {
            get
            {
                return this._issucc;
            }
            set
            {
                this._issucc = value;
            }
        }

        public string errcode
        {
            get
            {
                return this._errcode;
            }
            set
            {
                this._errcode = value;
            }
        }
    }
}
