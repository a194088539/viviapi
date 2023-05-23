using System;

namespace viviapi.Model.Channel
{
    [Serializable]
    public class ChannelTypeUserInfo
    {
        private DateTime? _addtime = new DateTime?(DateTime.Now);
        private DateTime? _updatetime = new DateTime?(DateTime.Now);
        private int _id;
        private int _typeId;
        private int _userid;
        private int? _suppid;
        private bool? _userisopen;
        private bool? _sysisopen;

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

        public int userId
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

        public bool? userIsOpen
        {
            get
            {
                return this._userisopen;
            }
            set
            {
                this._userisopen = value;
            }
        }

        public bool? sysIsOpen
        {
            get
            {
                return this._sysisopen;
            }
            set
            {
                this._sysisopen = value;
            }
        }

        public DateTime? addTime
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

        public int? suppid
        {
            get
            {
                return this._suppid;
            }
            set
            {
                this._suppid = value;
            }
        }
    }
}
