using System;

namespace viviapi.Model.User
{
    public class FindPwd
    {
        private int _id;
        private int? _uid;
        private string _username;
        private string _oldpwd;
        private string _newpwd;
        private int? _status;
        private DateTime? _addtimer;

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

        public int? uid
        {
            get
            {
                return this._uid;
            }
            set
            {
                this._uid = value;
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

        public string oldpwd
        {
            get
            {
                return this._oldpwd;
            }
            set
            {
                this._oldpwd = value;
            }
        }

        public string newpwd
        {
            get
            {
                return this._newpwd;
            }
            set
            {
                this._newpwd = value;
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

        public DateTime? addtimer
        {
            get
            {
                return this._addtimer;
            }
            set
            {
                this._addtimer = value;
            }
        }
    }
}
