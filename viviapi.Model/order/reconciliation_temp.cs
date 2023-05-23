using System;

namespace viviapi.Model.order
{
    [Serializable]
    public class reconciliation_temp
    {
        private string _serverid = string.Empty;
        private string _orderid = string.Empty;
        private int? _count = new int?(0);
        private int _id;

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

        public string serverid
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

        public string orderid
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }

        public int? count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }
    }
}
