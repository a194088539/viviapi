using System;
using viviapi.Model.Model;

namespace viviapi.Model
{
    public class FillMoneyInfo
    {
        private DateTime _addtime;
        private int _id;
        private Decimal _money;
        private CPS.FillMoneyStatusEnum _status;
        private int _userid;

        public DateTime AddTime
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

        public Decimal Money
        {
            get
            {
                return this._money;
            }
            set
            {
                this._money = value;
            }
        }

        public CPS.FillMoneyStatusEnum Status
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

        public int UserId
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
    }
}
