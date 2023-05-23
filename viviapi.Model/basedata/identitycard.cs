using System;

namespace viviapi.Model.basedata
{
    [Serializable]
    public class identitycard
    {
        private int _id;
        private string _bm;
        private string _dq;

        public int ID
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

        public string BM
        {
            get
            {
                return this._bm;
            }
            set
            {
                this._bm = value;
            }
        }

        public string DQ
        {
            get
            {
                return this._dq;
            }
            set
            {
                this._dq = value;
            }
        }
    }
}
