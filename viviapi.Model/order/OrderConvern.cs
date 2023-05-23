using System;

namespace viviapi.Model.Payment
{
    [Serializable]
    public class OrderConvern
    {
        private int _id;
        private ulong _okxrorderid;
        private string _origoutorderid;
        private int _origpaytype;
        private Decimal _origpayprice;
        private Decimal _origpromoney;
        private Decimal _origagmoney;
        private Decimal _origprofit;
        private Decimal _amount;
        private DateTime _created;
        private int _convtpaytype;
        private string _convtoutorderid;
        private Decimal _convtpayprice;
        private Decimal _convtagmoney;
        private Decimal _convtpromoney;
        private Decimal _convtprofit;
        private Decimal? _diffprofit;

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

        public Decimal OrigPayPrice
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

        public Decimal OrigPromoney
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

        public Decimal OrigAgmoney
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

        public Decimal OrigProfit
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

        public Decimal Amount
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

        public Decimal ConvtPayPrice
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

        public Decimal ConvtAgmoney
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

        public Decimal ConvtPromoney
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

        public Decimal ConvtProfit
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

        public Decimal? DiffProfit
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
    }
}
