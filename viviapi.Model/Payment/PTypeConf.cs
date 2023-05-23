using System;

namespace viviapi.Model
{
    [Serializable]
    public class PTypeConf
    {
        private bool _payalipay = true;
        private bool _payTanPay = true;
        private bool _payBank = true;
        private bool _pay103 = true;
        private bool _pay104 = true;
        private bool _pay105 = true;
        private bool _pay106 = true;
        private bool _pay107 = true;
        private bool _pay108 = true;
        private bool _pay109 = true;
        private bool _pay110 = true;
        private bool _pay111 = true;
        private bool _pay112 = true;
        private bool _pay113 = true;
        private int _id;
        private int _goodtype;
        private int _gm_id;
        private int _paytype;
        private int _isuse;

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

        public int GoodType
        {
            get
            {
                return this._goodtype;
            }
            set
            {
                this._goodtype = value;
            }
        }

        public int GM_ID
        {
            get
            {
                return this._gm_id;
            }
            set
            {
                this._gm_id = value;
            }
        }

        public int PayType
        {
            get
            {
                return this._paytype;
            }
            set
            {
                this._paytype = value;
            }
        }

        public int IsUse
        {
            get
            {
                return this._isuse;
            }
            set
            {
                this._isuse = value;
            }
        }

        public bool PayAlipay
        {
            get
            {
                return this._payalipay;
            }
            set
            {
                this._payalipay = value;
            }
        }

        public bool PayTanPay
        {
            get
            {
                return this._payTanPay;
            }
            set
            {
                this._payTanPay = value;
            }
        }

        public bool PayBank
        {
            get
            {
                return this._payBank;
            }
            set
            {
                this._payBank = value;
            }
        }

        public bool Pay103
        {
            get
            {
                return this._pay103;
            }
            set
            {
                this._pay103 = value;
            }
        }

        public bool Pay104
        {
            get
            {
                return this._pay104;
            }
            set
            {
                this._pay104 = value;
            }
        }

        public bool Pay105
        {
            get
            {
                return this._pay105;
            }
            set
            {
                this._pay105 = value;
            }
        }

        public bool Pay106
        {
            get
            {
                return this._pay106;
            }
            set
            {
                this._pay106 = value;
            }
        }

        public bool Pay107
        {
            get
            {
                return this._pay107;
            }
            set
            {
                this._pay107 = value;
            }
        }

        public bool Pay108
        {
            get
            {
                return this._pay108;
            }
            set
            {
                this._pay108 = value;
            }
        }

        public bool Pay109
        {
            get
            {
                return this._pay109;
            }
            set
            {
                this._pay109 = value;
            }
        }

        public bool Pay110
        {
            get
            {
                return this._pay110;
            }
            set
            {
                this._pay110 = value;
            }
        }

        public bool Pay111
        {
            get
            {
                return this._pay111;
            }
            set
            {
                this._pay111 = value;
            }
        }

        public bool Pay112
        {
            get
            {
                return this._pay112;
            }
            set
            {
                this._pay112 = value;
            }
        }

        public bool Pay113
        {
            get
            {
                return this._pay113;
            }
            set
            {
                this._pay113 = value;
            }
        }
    }
}
