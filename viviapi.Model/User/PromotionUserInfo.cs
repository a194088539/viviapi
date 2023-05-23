using System;

namespace viviapi.Model.User
{
    public class PromotionUserInfo
    {
        private int _promId = 0;
        private int _regid;
        private int _pid;
        private int _promstatus;
        private Decimal _prices;
        private DateTime _promtime;

        public int PromId
        {
            get
            {
                return this._promId;
            }
            set
            {
                this._promId = value;
            }
        }

        public int PromStatus
        {
            get
            {
                return this._promstatus;
            }
            set
            {
                this._promstatus = value;
            }
        }

        public DateTime PromTime
        {
            get
            {
                return this._promtime;
            }
            set
            {
                this._promtime = value;
            }
        }

        public int PID
        {
            get
            {
                return this._pid;
            }
            set
            {
                this._pid = value;
            }
        }

        public Decimal Prices
        {
            get
            {
                return this._prices;
            }
            set
            {
                this._prices = value;
            }
        }

        public int RegId
        {
            get
            {
                return this._regid;
            }
            set
            {
                this._regid = value;
            }
        }
    }
}
