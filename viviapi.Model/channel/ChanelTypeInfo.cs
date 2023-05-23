using System;

namespace viviapi.Model.Channel
{
    [Serializable]
    public class ChannelTypeInfo
    {
        private string _code = string.Empty;
        private Decimal _supprate = new Decimal(0);
        private int _runmode = 0;
        private string _runset = string.Empty;
        private int _id;
        private ChannelClassEnum _class;
        private string _modetypename;
        private int _typeId;
        private OpenEnum _isopen;
        private DateTime _addtime;
        private int? _sort;
        private bool _release;
        private int _supplier;

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

        public int typeId
        {
            get
            {
                return this._typeId;
            }
            set
            {
                this._typeId = value;
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

        public int supplier
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

        public ChannelClassEnum Class
        {
            get
            {
                return this._class;
            }
            set
            {
                this._class = value;
            }
        }

        public string modetypename
        {
            get
            {
                return this._modetypename;
            }
            set
            {
                this._modetypename = value;
            }
        }

        public OpenEnum isOpen
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

        public bool release
        {
            get
            {
                return this._release;
            }
            set
            {
                this._release = value;
            }
        }

        public int runmode
        {
            get
            {
                return this._runmode;
            }
            set
            {
                this._runmode = value;
            }
        }

        public string runset
        {
            get
            {
                return this._runset;
            }
            set
            {
                this._runset = value;
            }
        }
    }
}
