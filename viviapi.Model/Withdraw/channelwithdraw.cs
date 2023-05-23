using System;

namespace viviapi.Model.Withdraw
{
    [Serializable]
    public class channelwithdraw
    {
        private int _supplier = 0;
        private int _id;
        private string _bankcode;
        private string _bankname;
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

        public string bankCode
        {
            get
            {
                return this._bankcode;
            }
            set
            {
                this._bankcode = value;
            }
        }

        public string bankName
        {
            get
            {
                return this._bankname;
            }
            set
            {
                this._bankname = value;
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
