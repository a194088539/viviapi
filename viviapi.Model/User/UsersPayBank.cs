using System;

namespace viviapi.Model.User
{
    [Serializable]
    public class userspaybank
    {
        private int _accouttype = 0;
        private int _pmode = 1;
        private int _id;
        private int _userid;
        private string _account;
        private string _payeename;
        private string _bankcode;
        private string _payeebank;
        private string _provincecode;
        private string _bankprovince;
        private string _citycode;
        private string _bankcity;
        private string _bankaddress;
        private int? _status;
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

        public int accoutType
        {
            get
            {
                return this._accouttype;
            }
            set
            {
                this._accouttype = value;
            }
        }

        public int pmode
        {
            get
            {
                return this._pmode;
            }
            set
            {
                this._pmode = value;
            }
        }

        public string pmodeName
        {
            get
            {
                string str = string.Empty;
                switch (this.pmode)
                {
                    case 1:
                        str = "银行帐户";
                        break;
                    case 2:
                        str = "支付宝";
                        break;
                    case 3:
                        str = "财付通";
                        break;
                }
                return str;
            }
        }

        public string account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
            }
        }

        public string payeeName
        {
            get
            {
                return this._payeename;
            }
            set
            {
                this._payeename = value;
            }
        }

        public string BankCode
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

        public string payeeBank
        {
            get
            {
                return this._payeebank;
            }
            set
            {
                this._payeebank = value;
            }
        }

        public string provinceCode
        {
            get
            {
                return this._provincecode;
            }
            set
            {
                this._provincecode = value;
            }
        }

        public string bankProvince
        {
            get
            {
                return this._bankprovince;
            }
            set
            {
                this._bankprovince = value;
            }
        }

        public string cityCode
        {
            get
            {
                return this._citycode;
            }
            set
            {
                this._citycode = value;
            }
        }

        public string bankCity
        {
            get
            {
                return this._bankcity;
            }
            set
            {
                this._bankcity = value;
            }
        }

        public string bankAddress
        {
            get
            {
                return this._bankaddress;
            }
            set
            {
                this._bankaddress = value;
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

        public DateTime AddTime
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

        public DateTime? updateTime
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
