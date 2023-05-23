using viviapi.BLL;
using viviapi.Model;
using viviapi.SysConfig;

namespace viviapi.ETAPI
{
    public class ETAPIBase
    {
        public SupplierInfo _suppInfo = (SupplierInfo)null;

        public string suppAccount
        {
            get
            {
                return this._suppInfo.puserid;
            }
        }

        public string suppKey
        {
            get
            {
                return this._suppInfo.puserkey;
            }
        }

        public string suppUserName
        {
            get
            {
                return this._suppInfo.pusername;
            }
        }

        public string postCardUrl
        {
            get
            {
                return this._suppInfo.postCardUrl;
            }
        }

        public string postBankUrl
        {
            get
            {
                return this._suppInfo.postBankUrl;
            }
        }

        public string SiteDomain
        {
            get
            {
                return RuntimeSetting.SiteDomain;
            }
        }

        public ETAPIBase(int suppcode)
        {
            this._suppInfo = SupplierFactory.GetCacheModel(suppcode);
        }
    }
}
