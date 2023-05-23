using viviapi.BLL;
using viviapi.Model;
using viviapi.SysConfig;

namespace viviapi.ETAPI.Lefupay
{
    public class lefupayConfig
    {
        private int suppId = 1012;
        private SupplierInfo _suppInfo = (SupplierInfo)null;
        private string partner = "";
        private string key = "";
        private string redirectURL = "";
        private string notifyURL = "";
        private string inputCharset = "";
        private string signType = "";
        private string apiCode = "";
        private string versionCode = "";
        private string buyer = "";
        private string paymentType = "";

        internal string returnurl
        {
            get
            {
                return RuntimeSetting.SiteDomain + "/return/LefupayReturn.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return RuntimeSetting.SiteDomain + "/notify/Lefupaynotify.aspx";
            }
        }

        private SupplierInfo suppInfo
        {
            get
            {
                if (this._suppInfo == null)
                    this._suppInfo = SupplierFactory.GetCacheModel(this.suppId);
                return this._suppInfo;
            }
        }

        public string Partner
        {
            get
            {
                return this.partner;
            }
            set
            {
                this.partner = value;
            }
        }

        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }

        public string RedirectURL
        {
            get
            {
                return this.redirectURL;
            }
            set
            {
                this.redirectURL = value;
            }
        }

        public string NotifyURL
        {
            get
            {
                return this.notifyURL;
            }
            set
            {
                this.notifyURL = value;
            }
        }

        public string InputCharset
        {
            get
            {
                return this.inputCharset;
            }
            set
            {
                this.inputCharset = value;
            }
        }

        public string SignType
        {
            get
            {
                return this.signType;
            }
            set
            {
                this.signType = value;
            }
        }

        public string ApiCode
        {
            get
            {
                return this.apiCode;
            }
            set
            {
                this.apiCode = value;
            }
        }

        public string VersionCode
        {
            get
            {
                return this.versionCode;
            }
            set
            {
                this.versionCode = value;
            }
        }

        public string Buyer
        {
            get
            {
                return this.buyer;
            }
            set
            {
                this.buyer = value;
            }
        }

        public string PaymentType
        {
            get
            {
                return this.paymentType;
            }
            set
            {
                this.paymentType = value;
            }
        }

        public lefupayConfig()
        {
            this.partner = this.suppInfo.puserid;
            this.key = this.suppInfo.puserkey;
            this.notifyURL = this.notifyUrl;
            this.redirectURL = this.returnurl;
            this.inputCharset = "UTF-8";
            this.signType = "MD5";
            this.apiCode = "directPay";
            this.versionCode = "1.0";
            this.buyer = "000001";
            this.paymentType = "ALL";
        }
    }
}
