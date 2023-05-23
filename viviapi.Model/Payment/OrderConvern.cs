namespace viviapi.Model.pay
{
    using System;

    [Serializable]
    public class OrderConvern
    {
        private decimal _amount;
        private decimal _convtagmoney;
        private string _convtoutorderid;
        private decimal _convtpayprice;
        private int _convtpaytype;
        private decimal _convtprofit;
        private decimal _convtpromoney;
        private DateTime _created;
        private decimal? _diffprofit;
        private int _id;
        private ulong _okxrorderid;
        private decimal _origagmoney;
        private string _origoutorderid;
        private decimal _origpayprice;
        private int _origpaytype;
        private decimal _origprofit;
        private decimal _origpromoney;

        public decimal Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public decimal ConvtAgmoney
        {
            get
            {
                return this._convtagmoney;
            }
            set
            {
                this._convtagmoney = value;
            }
        }

        public string ConvtOutOrderId
        {
            get
            {
                return this._convtoutorderid;
            }
            set
            {
                this._convtoutorderid = value;
            }
        }

        public decimal ConvtPayPrice
        {
            get
            {
                return this._convtpayprice;
            }
            set
            {
                this._convtpayprice = value;
            }
        }

        public int ConvtPayType
        {
            get
            {
                return this._convtpaytype;
            }
            set
            {
                this._convtpaytype = value;
            }
        }

        public decimal ConvtProfit
        {
            get
            {
                return this._convtprofit;
            }
            set
            {
                this._convtprofit = value;
            }
        }

        public decimal ConvtPromoney
        {
            get
            {
                return this._convtpromoney;
            }
            set
            {
                this._convtpromoney = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return this._created;
            }
            set
            {
                this._created = value;
            }
        }

        public decimal? DiffProfit
        {
            get
            {
                return this._diffprofit;
            }
            set
            {
                this._diffprofit = value;
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

        public ulong OkxrOrderId
        {
            get
            {
                return this._okxrorderid;
            }
            set
            {
                this._okxrorderid = value;
            }
        }

        public decimal OrigAgmoney
        {
            get
            {
                return this._origagmoney;
            }
            set
            {
                this._origagmoney = value;
            }
        }

        public string OrigOutOrderId
        {
            get
            {
                return this._origoutorderid;
            }
            set
            {
                this._origoutorderid = value;
            }
        }

        public decimal OrigPayPrice
        {
            get
            {
                return this._origpayprice;
            }
            set
            {
                this._origpayprice = value;
            }
        }

        public int OrigPayType
        {
            get
            {
                return this._origpaytype;
            }
            set
            {
                this._origpaytype = value;
            }
        }

        public decimal OrigProfit
        {
            get
            {
                return this._origprofit;
            }
            set
            {
                this._origprofit = value;
            }
        }

        public decimal OrigPromoney
        {
            get
            {
                return this._origpromoney;
            }
            set
            {
                this._origpromoney = value;
            }
        }
    }
}

