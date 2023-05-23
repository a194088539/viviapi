using System;

namespace viviapi.Model.APP
{
    [Serializable]
    public class TransferInfo
    {
        private int _id;
        private int? _userid;
        private int? _status;
        private string _billingname;
        private int? _bankname;
        private string _province;
        private string _city;
        private string _branch;
        private string _cardnum;
        private string _payee;
        private Decimal? _tranamt;
        private Decimal? _charges;
        private int? _paybank;
        private string _email;
        private string _mobile;
        private int? _iswarn;
        private int? _warnday;
        private int? _processstatus;
        private DateTime? _processtime;
        private bool _smsnotification;
        private string _field1;
        private string _field2;
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

        public string billingName
        {
            get
            {
                return this._billingname;
            }
            set
            {
                this._billingname = value;
            }
        }

        public int? bankname
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

        public string province
        {
            get
            {
                return this._province;
            }
            set
            {
                this._province = value;
            }
        }

        public string city
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
            }
        }

        public string branch
        {
            get
            {
                return this._branch;
            }
            set
            {
                this._branch = value;
            }
        }

        public string cardnum
        {
            get
            {
                return this._cardnum;
            }
            set
            {
                this._cardnum = value;
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

        public Decimal? tranAmt
        {
            get
            {
                return this._tranamt;
            }
            set
            {
                this._tranamt = value;
            }
        }

        public Decimal? charges
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

        public int? paybank
        {
            get
            {
                return this._paybank;
            }
            set
            {
                this._paybank = value;
            }
        }

        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
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

        public int? iswarn
        {
            get
            {
                return this._iswarn;
            }
            set
            {
                this._iswarn = value;
            }
        }

        public int? warnday
        {
            get
            {
                return this._warnday;
            }
            set
            {
                this._warnday = value;
            }
        }

        public int? processstatus
        {
            get
            {
                return this._processstatus;
            }
            set
            {
                this._processstatus = value;
            }
        }

        public DateTime? processtime
        {
            get
            {
                return this._processtime;
            }
            set
            {
                this._processtime = value;
            }
        }

        public bool smsnotification
        {
            get
            {
                return this._smsnotification;
            }
            set
            {
                this._smsnotification = value;
            }
        }

        public string field1
        {
            get
            {
                return this._field1;
            }
            set
            {
                this._field1 = value;
            }
        }

        public string field2
        {
            get
            {
                return this._field2;
            }
            set
            {
                this._field2 = value;
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
