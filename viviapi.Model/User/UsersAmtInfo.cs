using System;

namespace viviapi.Model.User
{
    [Serializable]
    public class UsersAmtInfo
    {
        private int? _integral = new int?(0);
        private Decimal? _balance = new Decimal?(new Decimal(0));
        private Decimal? _payment = new Decimal?(new Decimal(0));
        private Decimal? _unpayment = new Decimal?(new Decimal(0));
        private Decimal? _Freeze = new Decimal?(new Decimal(0));
        private Decimal _enableAmt = new Decimal(0);
        private int _id;
        private int _userid;

        public Decimal enableAmt
        {
            get
            {
                return this._enableAmt;
            }
            set
            {
                this._enableAmt = value;
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

        public int? Integral
        {
            get
            {
                return this._integral;
            }
            set
            {
                this._integral = value;
            }
        }

        public Decimal? balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this._balance = value;
            }
        }

        public Decimal? payment
        {
            get
            {
                return this._payment;
            }
            set
            {
                this._payment = value;
            }
        }

        public Decimal? unpayment
        {
            get
            {
                return this._unpayment;
            }
            set
            {
                this._unpayment = value;
            }
        }

        public Decimal? Freeze
        {
            get
            {
                return this._Freeze;
            }
            set
            {
                this._Freeze = value;
            }
        }
    }
}
