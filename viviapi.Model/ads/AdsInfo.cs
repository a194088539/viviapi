using System;

namespace viviapi.Model
{
    public class AdsInfo
    {
        private int _id = 0;
        private DateTime _addtime;
        private string _adsname;
        private AdsStatusEnum _adsstatus;
        private AdsTypeEnum _adstype;
        private int _advertisersid;
        private string _description;
        private string _href;
        private Decimal _prices;
        private ShowStyleEnum _showstyle;
        private string _targettype;

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

        public string AdsName
        {
            get
            {
                return this._adsname;
            }
            set
            {
                this._adsname = value;
            }
        }

        public AdsStatusEnum AdsStatus
        {
            get
            {
                return this._adsstatus;
            }
            set
            {
                this._adsstatus = value;
            }
        }

        public AdsTypeEnum AdsType
        {
            get
            {
                return this._adstype;
            }
            set
            {
                this._adstype = value;
            }
        }

        public int AdvertisersId
        {
            get
            {
                return this._advertisersid;
            }
            set
            {
                this._advertisersid = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string Href
        {
            get
            {
                return this._href;
            }
            set
            {
                this._href = value;
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

        public ShowStyleEnum ShowStyle
        {
            get
            {
                return this._showstyle;
            }
            set
            {
                this._showstyle = value;
            }
        }

        public string TargetType
        {
            get
            {
                return this._targettype;
            }
            set
            {
                this._targettype = value;
            }
        }
    }
}
