using System;

namespace viviapi.Model.Sys
{
    [Serializable]
    public class debuginfo
    {
        private int _id;
        private debugtypeenum _bugtype;
        private int? _userid;
        private string _url;
        private string _errorcode;
        private string _errorinfo;
        private string _detail;
        private DateTime? _addtime;
        private string _userorder;

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

        public debugtypeenum bugtype
        {
            get
            {
                return this._bugtype;
            }
            set
            {
                this._bugtype = value;
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

        public string url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        public string errorcode
        {
            get
            {
                return this._errorcode;
            }
            set
            {
                this._errorcode = value;
            }
        }

        public string errorinfo
        {
            get
            {
                return this._errorinfo;
            }
            set
            {
                this._errorinfo = value;
            }
        }

        public string detail
        {
            get
            {
                return this._detail;
            }
            set
            {
                this._detail = value;
            }
        }

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
    }
}
