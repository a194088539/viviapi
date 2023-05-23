using System;

namespace viviapi.Model.Channel
{
    [Serializable]
    public class ChannelInfo
    {
        private Decimal _supprate = new Decimal(0);
        private int _id;
        private string _code;
        private int _typeid;
        private int? _supplier;
        private string _modename;
        private string _modeenname;
        private int _facevalue;
        private int? _isopen;
        private DateTime _addtime;
        private int? _sort;

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

        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
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

        public int? supplier
        {
            get
            {
                return this._supplier;
            }
            set
            {
                this._supplier = value;
            }
        }

        public string modeName
        {
            get
            {
                return this._modename;
            }
            set
            {
                this._modename = value;
            }
        }

        public string modeEnName
        {
            get
            {
                return this._modeenname;
            }
            set
            {
                this._modeenname = value;
            }
        }

        public int faceValue
        {
            get
            {
                return this._facevalue;
            }
            set
            {
                this._facevalue = value;
            }
        }

        public Decimal supprate
        {
            get
            {
                return this._supprate;
            }
            set
            {
                this._supprate = value;
            }
        }

        public int? isOpen
        {
            get
            {
                return this._isopen;
            }
            set
            {
                this._isopen = value;
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

        public int? sort
        {
            get
            {
                return this._sort;
            }
            set
            {
                this._sort = value;
            }
        }
    }
}
