using System;

namespace OKXR.Entity
{
    [Serializable]
    public class Smslog
    {
        private DateTime? _addtime;
        private string _linkid;
        private string _message;
        private string _mobile;
        private Decimal _price;
        private string _serverid;
        private string _servicenum;
        private int _sid;
        private string _siteid;
        private string _spid;
        private string _status;
        private string _type;

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

        public Decimal price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public string ServerId
        {
            get
            {
                return this._serverid;
            }
            set
            {
                this._serverid = value;
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

        public int sid
        {
            get
            {
                return this._sid;
            }
            set
            {
                this._sid = value;
            }
        }

        public string siteid
        {
            get
            {
                return this._siteid;
            }
            set
            {
                this._siteid = value;
            }
        }

        public string spid
        {
            get
            {
                return this._spid;
            }
            set
            {
                this._spid = value;
            }
        }

        public string status
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

        public string type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}
