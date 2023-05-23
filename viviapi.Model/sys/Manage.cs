using System;

namespace viviapi.Model
{
    public class Manage
    {
        private int _id;

        private string _username;

        private string _password;

        private string _secondpwd;

        private ManageRole _role;

        private int? _status;

        private string _relname;

        private string _lastloginip;

        private DateTime? _lastlogintime;

        private string _lastloginaddress = string.Empty;

        private string _lastloginremark = string.Empty;

        private string _sessionid;

        private int _isSuperAdmin = 0;

        private int _isAgent = 0;

        private string _qq = string.Empty;

        private string _tel = string.Empty;

        public decimal? balance
        {
            get;
            set;
        }

        public decimal? cardcommission
        {
            get;
            set;
        }

        public decimal? commission
        {
            get;
            set;
        }

        public int? commissiontype
        {
            get;
            set;
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

        public int isAgent
        {
            get
            {
                return this._isAgent;
            }
            set
            {
                this._isAgent = value;
            }
        }

        public int isSuperAdmin
        {
            get
            {
                return this._isSuperAdmin;
            }
            set
            {
                this._isSuperAdmin = value;
            }
        }

        public string LastLoginAddress
        {
            get
            {
                return this._lastloginaddress;
            }
            set
            {
                this._lastloginaddress = value;
            }
        }

        public string lastLoginIp
        {
            get
            {
                return this._lastloginip;
            }
            set
            {
                this._lastloginip = value;
            }
        }

        public string LastLoginRemark
        {
            get
            {
                return this._lastloginremark;
            }
            set
            {
                this._lastloginremark = value;
            }
        }

        public DateTime? lastLoginTime
        {
            get
            {
                return this._lastlogintime;
            }
            set
            {
                this._lastlogintime = value;
            }
        }

        public string password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string qq
        {
            get
            {
                return this._qq;
            }
            set
            {
                this._qq = value;
            }
        }

        public string relname
        {
            get
            {
                return this._relname;
            }
            set
            {
                this._relname = value;
            }
        }

        public ManageRole role
        {
            get
            {
                return this._role;
            }
            set
            {
                this._role = value;
            }
        }

        public string secondpwd
        {
            get
            {
                return this._secondpwd;
            }
            set
            {
                this._secondpwd = value;
            }
        }

        public string sessionid
        {
            get
            {
                return this._sessionid;
            }
            set
            {
                this._sessionid = value;
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

        public string tel
        {
            get
            {
                return this._tel;
            }
            set
            {
                this._tel = value;
            }
        }

        public string username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public Manage()
        {
        }
    }
}