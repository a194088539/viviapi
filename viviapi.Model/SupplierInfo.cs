namespace viviapi.Model
{
    using System;

    [Serializable]
    public class SupplierInfo
    {
        private int? _code;
        private string _desc;
        private string _distributionUrl = string.Empty;
        private int _id;
        private bool? _isali;
        private bool? _isbank;
        private bool? _iscard;
        private bool _isdistribution = false;
        private bool? _issms;
        private bool? _issx;
        private bool? _issys;
        private bool? _iswap;
        private bool? _iswx;
        private string _JumpUrl;
        private string _logourl;
        private string _name;
        private string _name1 = string.Empty;
        private string _pbakurl;
        private string _pcardbakurl;
        private string _postbankurl;
        private string _postcardurl;
        private string _postsmsurl;
        private string _purl;
        private string _puserid;
        private string _puserid1;
        private string _puserid2;
        private string _puserid3;
        private string _puserid4;
        private string _puserid5;
        private string _puserkey;
        private string _puserkey1;
        private string _puserkey2;
        private string _puserkey3;
        private string _puserkey4;
        private string _puserkey5;
        private string _pusername;
        private string _queryCardUrl = string.Empty;
        private bool? _release;
        private int? _sort;

        public int? code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public string desc
        {
            get
            {
                return this._desc;
            }
            set
            {
                this._desc = value;
            }
        }

        public string distributionUrl
        {
            get
            {
                return this._distributionUrl;
            }
            set
            {
                this._distributionUrl = value;
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

        public bool? isali
        {
            get
            {
                return this._isali;
            }
            set
            {
                this._isali = value;
            }
        }

        public bool? isbank
        {
            get
            {
                return this._isbank;
            }
            set
            {
                this._isbank = value;
            }
        }

        public bool? iscard
        {
            get
            {
                return this._iscard;
            }
            set
            {
                this._iscard = value;
            }
        }

        public bool isdistribution
        {
            get
            {
                return this._isdistribution;
            }
            set
            {
                this._isdistribution = value;
            }
        }

        public bool? issms
        {
            get
            {
                return this._issms;
            }
            set
            {
                this._issms = value;
            }
        }

        public bool? issx
        {
            get
            {
                return this._issx;
            }
            set
            {
                this._issx = value;
            }
        }

        public bool? issys
        {
            get
            {
                return this._issys;
            }
            set
            {
                this._issys = value;
            }
        }

        public bool? iswap
        {
            get
            {
                return this._iswap;
            }
            set
            {
                this._iswap = value;
            }
        }

        public bool? iswx
        {
            get
            {
                return this._iswx;
            }
            set
            {
                this._iswx = value;
            }
        }

        public string jumpUrl
        {
            get
            {
                return this._JumpUrl;
            }
            set
            {
                this._JumpUrl = value;
            }
        }

        public string logourl
        {
            get
            {
                return this._logourl;
            }
            set
            {
                this._logourl = value;
            }
        }

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string name1
        {
            get
            {
                return this._name1;
            }
            set
            {
                this._name1 = value;
            }
        }

        public string pbakurl
        {
            get
            {
                return this._pbakurl;
            }
            set
            {
                this._pbakurl = value;
            }
        }

        public string pcardbakurl
        {
            get
            {
                return this._pcardbakurl;
            }
            set
            {
                this._pcardbakurl = value;
            }
        }

        public string postBankUrl
        {
            get
            {
                return this._postbankurl;
            }
            set
            {
                this._postbankurl = value;
            }
        }

        public string postCardUrl
        {
            get
            {
                return this._postcardurl;
            }
            set
            {
                this._postcardurl = value;
            }
        }

        public string postSMSUrl
        {
            get
            {
                return this._postsmsurl;
            }
            set
            {
                this._postsmsurl = value;
            }
        }

        public string purl
        {
            get
            {
                return this._purl;
            }
            set
            {
                this._purl = value;
            }
        }

        public string puserid
        {
            get
            {
                return this._puserid;
            }
            set
            {
                this._puserid = value;
            }
        }

        public string puserid1
        {
            get
            {
                return this._puserid1;
            }
            set
            {
                this._puserid1 = value;
            }
        }

        public string puserid2
        {
            get
            {
                return this._puserid2;
            }
            set
            {
                this._puserid2 = value;
            }
        }

        public string puserid3
        {
            get
            {
                return this._puserid3;
            }
            set
            {
                this._puserid3 = value;
            }
        }

        public string puserid4
        {
            get
            {
                return this._puserid4;
            }
            set
            {
                this._puserid4 = value;
            }
        }

        public string puserid5
        {
            get
            {
                return this._puserid5;
            }
            set
            {
                this._puserid5 = value;
            }
        }

        public string puserkey
        {
            get
            {
                return this._puserkey;
            }
            set
            {
                this._puserkey = value;
            }
        }

        public string puserkey1
        {
            get
            {
                return this._puserkey1;
            }
            set
            {
                this._puserkey1 = value;
            }
        }

        public string puserkey2
        {
            get
            {
                return this._puserkey2;
            }
            set
            {
                this._puserkey2 = value;
            }
        }

        public string puserkey3
        {
            get
            {
                return this._puserkey3;
            }
            set
            {
                this._puserkey3 = value;
            }
        }

        public string puserkey4
        {
            get
            {
                return this._puserkey4;
            }
            set
            {
                this._puserkey4 = value;
            }
        }

        public string puserkey5
        {
            get
            {
                return this._puserkey5;
            }
            set
            {
                this._puserkey5 = value;
            }
        }

        public string pusername
        {
            get
            {
                return this._pusername;
            }
            set
            {
                this._pusername = value;
            }
        }

        public string queryCardUrl
        {
            get
            {
                return this._queryCardUrl;
            }
            set
            {
                this._queryCardUrl = value;
            }
        }

        public bool? release
        {
            get
            {
                return this._release;
            }
            set
            {
                this._release = value;
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

