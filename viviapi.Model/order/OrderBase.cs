using System;
using viviLib.TimeControl;

namespace viviapi.Model.Order
{
    [Serializable]
    public class OrderBase
    {
        private int _notifycount = 0;
        private int _notifystat = 1;
        private string _referurl = string.Empty;
        private int _status = 1;
        private Decimal _payrate = new Decimal(0);
        private Decimal _supplierrate = new Decimal(0);
        private Decimal _promrate = new Decimal(0);
        private Decimal _payamt = new Decimal(0);
        private Decimal _promamt = new Decimal(0);
        private Decimal _supplieramt = new Decimal(0);
        private Decimal _profits = new Decimal(0);
        private int? _server = new int?(1);
        private string _version = string.Empty;
        private string _cus_subject = string.Empty;
        private string _cus_price = string.Empty;
        private string _cus_quantity = string.Empty;
        private string _cus_description = string.Empty;
        private string _cus_field1 = string.Empty;
        private string _cus_field2 = string.Empty;
        private string _cus_field3 = string.Empty;
        private string _cus_field4 = string.Empty;
        private string _cus_field5 = string.Empty;
        private string _errtype = string.Empty;
        private int _agent = 0;
        private DateTime _notifytime = FormatConvertor.SqlDateTimeMinValue;
        private string _msg = string.Empty;
        private long _id;
        private string _orderid;
        private int _ordertype;
        private int _userid;
        private int _typeid;
        private string _paymodeid;
        private string _userorder;
        private Decimal _refervalue;
        private Decimal? _realvalue;
        private string _notifyurl;
        private string _againnotifyurl;
        private string _notifycontext;
        private string _returnurl;
        private string _attach;
        private string _payerip;
        private string _clientip;
        private DateTime _addtime;
        private int _supplierid;
        private string _supplierorder;
        private DateTime? _processingtime;
        private DateTime? _completetime;

        public string opstate { get; set; }

        public long id
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

        public int ordertype
        {
            get
            {
                return this._ordertype;
            }
            set
            {
                this._ordertype = value;
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

        public int typeId
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string paymodeId
        {
            get
            {
                return this._paymodeid;
            }
            set
            {
                this._paymodeid = value;
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

        public Decimal refervalue
        {
            get
            {
                return this._refervalue;
            }
            set
            {
                this._refervalue = value;
            }
        }

        public Decimal? realvalue
        {
            get
            {
                return this._realvalue;
            }
            set
            {
                this._realvalue = value;
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

        public string returnurl
        {
            get
            {
                return this._returnurl;
            }
            set
            {
                this._returnurl = value;
            }
        }

        public string attach
        {
            get
            {
                return this._attach;
            }
            set
            {
                this._attach = value;
            }
        }

        public string payerip
        {
            get
            {
                return this._payerip;
            }
            set
            {
                this._payerip = value;
            }
        }

        public string clientip
        {
            get
            {
                return this._clientip;
            }
            set
            {
                this._clientip = value;
            }
        }

        public string referUrl
        {
            get
            {
                return this._referurl;
            }
            set
            {
                this._referurl = value;
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

        public string supplierOrder
        {
            get
            {
                return this._supplierorder;
            }
            set
            {
                this._supplierorder = value;
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

        public DateTime? processingtime
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

        public int? server
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

        public int? manageId { get; set; }

        public Decimal? commission { get; set; }

        public DateTime notifytime
        {
            get
            {
                return this._notifytime;
            }
            set
            {
                this._notifytime = value;
            }
        }

        public string msg
        {
            get
            {
                return this._msg;
            }
            set
            {
                this._msg = value;
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

        public string cus_subject
        {
            get
            {
                return this._cus_subject;
            }
            set
            {
                this._cus_subject = value;
            }
        }

        public string cus_price
        {
            get
            {
                return this._cus_price;
            }
            set
            {
                this._cus_price = value;
            }
        }

        public string cus_quantity
        {
            get
            {
                return this._cus_quantity;
            }
            set
            {
                this._cus_quantity = value;
            }
        }

        public string cus_description
        {
            get
            {
                return this._cus_description;
            }
            set
            {
                this._cus_description = value;
            }
        }

        public string cus_field1
        {
            get
            {
                return this._cus_field1;
            }
            set
            {
                this._cus_field1 = value;
            }
        }

        public string cus_field2
        {
            get
            {
                return this._cus_field2;
            }
            set
            {
                this._cus_field2 = value;
            }
        }

        public string cus_field3
        {
            get
            {
                return this._cus_field3;
            }
            set
            {
                this._cus_field3 = value;
            }
        }

        public string cus_field4
        {
            get
            {
                return this._cus_field4;
            }
            set
            {
                this._cus_field4 = value;
            }
        }

        public string cus_field5
        {
            get
            {
                return this._cus_field5;
            }
            set
            {
                this._cus_field5 = value;
            }
        }

        public string errtype
        {
            get
            {
                return this._errtype;
            }
            set
            {
                this._errtype = value;
            }
        }

        public int agentId
        {
            get
            {
                return this._agent;
            }
            set
            {
                this._agent = value;
            }
        }
    }
}
