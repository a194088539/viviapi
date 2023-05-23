using viviapi.BLL;
using viviapi.Model;
using viviapi.SysConfig;

namespace TestEasy
{
    public class EasypayConfig
    {
        private int suppId = 1000;
        private SupplierInfo _suppInfo = (SupplierInfo)null;
        private string partner = "";
        private string key = "";
        private string seller_email = "";
        private string return_url = "";
        private string notify_url = "";
        private string input_charset = "";
        private string sign_type = "";
        private string transport = "";
        private string payment_type = "";
        private string service = "";

        internal string returnurl
        {
            get
            {
                return RuntimeSetting.SiteDomain + "/return/EasyPay_Return.aspx";
            }
        }

        internal string notifyUrl
        {
            get
            {
                return RuntimeSetting.SiteDomain + "/notify/EasyPay_notify.aspx";
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

        public string Seller_email
        {
            get
            {
                return this.seller_email;
            }
            set
            {
                this.seller_email = value;
            }
        }

        public string Return_url
        {
            get
            {
                return this.return_url;
            }
            set
            {
                this.return_url = value;
            }
        }

        public string Notify_url
        {
            get
            {
                return this.notify_url;
            }
            set
            {
                this.notify_url = value;
            }
        }

        public string Input_charset
        {
            get
            {
                return this.input_charset;
            }
            set
            {
                this.input_charset = value;
            }
        }

        public string Sign_type
        {
            get
            {
                return this.sign_type;
            }
            set
            {
                this.sign_type = value;
            }
        }

        public string Transport
        {
            get
            {
                return this.transport;
            }
            set
            {
                this.transport = value;
            }
        }

        public string Payment_type
        {
            get
            {
                return this.payment_type;
            }
            set
            {
                this.payment_type = value;
            }
        }

        public string Service
        {
            get
            {
                return this.service;
            }
            set
            {
                this.service = value;
            }
        }

        public EasypayConfig()
        {
            this.partner = this.suppInfo.puserid;
            this.key = this.suppInfo.puserkey;
            this.seller_email = this.suppInfo.pusername;
            this.notify_url = this.notifyUrl;
            this.return_url = this.returnurl;
            this.input_charset = "gbk";
            this.sign_type = "MD5";
            this.transport = "http";
            this.service = "create_direct_pay_by_user";
            this.payment_type = "1";
        }
    }
}
