using System;

namespace viviapi.Model.Settled
{
    [Serializable]
    public class UsersAmtFreezeInfo
    {
        private AmtFreezeInfoStatus _status = AmtFreezeInfoStatus.否;
        private AmtunFreezeMode _unfreezemode = AmtunFreezeMode.未处理;
        private int _id;
        private int _userid;
        private Decimal _freezeamt;
        private DateTime? _addtime;
        private int? _manageid;
        private DateTime? _checktime;
        private string _why;

        public string why
        {
            get
            {
                return this._why;
            }
            set
            {
                this._why = value;
            }
        }

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

        public Decimal freezeAmt
        {
            get
            {
                return this._freezeamt;
            }
            set
            {
                this._freezeamt = value;
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

        public int? manageId
        {
            get
            {
                return this._manageid;
            }
            set
            {
                this._manageid = value;
            }
        }

        public AmtunFreezeMode unfreezemode
        {
            get
            {
                return this._unfreezemode;
            }
            set
            {
                this._unfreezemode = value;
            }
        }

        public AmtFreezeInfoStatus status
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

        public DateTime? checktime
        {
            get
            {
                return this._checktime;
            }
            set
            {
                this._checktime = value;
            }
        }
    }
}
