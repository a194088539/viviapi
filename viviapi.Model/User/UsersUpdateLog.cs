using System;

namespace viviapi.Model.User
{
    public class UsersUpdateLog
    {
        private string _editor = string.Empty;
        private string _oIp = string.Empty;
        private string _desc = string.Empty;
        private int _id;
        private int _userid;
        private string _field;
        private string _oldvalue;
        private string _newvalue;
        private DateTime _addtime;

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

        public string field
        {
            get
            {
                return this._field;
            }
            set
            {
                this._field = value;
            }
        }

        public string Desc
        {
            get
            {
                return this._desc;
            }
            set
            {
                this._desc = value;
            }
        }

        public string Editor
        {
            get
            {
                return this._editor;
            }
            set
            {
                this._editor = value;
            }
        }

        public string OIp
        {
            get
            {
                return this._oIp;
            }
            set
            {
                this._oIp = value;
            }
        }

        public string oldValue
        {
            get
            {
                return this._oldvalue;
            }
            set
            {
                this._oldvalue = value;
            }
        }

        public string newvalue
        {
            get
            {
                return this._newvalue;
            }
            set
            {
                this._newvalue = value;
            }
        }

        public DateTime Addtime
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
    }
}
