using System;

namespace viviapi.Model.Channel
{
    [Serializable]
    public class ChannelSupplier
    {
        private int _typeid;
        private int _suppid;
        private int _userid;
        private bool _isopen;
        private bool _isdefault;
        private Decimal _payrate;

        public int typeid
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

        public int suppid
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

        public bool isopen
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

        public bool isdefault
        {
            get
            {
                return this._isdefault;
            }
            set
            {
                this._isdefault = value;
            }
        }

        public Decimal payrate
        {
            get
            {
                return this._payrate;
            }
            set
            {
                this._payrate = value;
            }
        }
    }
}
