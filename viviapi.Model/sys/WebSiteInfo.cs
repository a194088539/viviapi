using System;

namespace viviapi.Model
{
    [Serializable]
    public class WebSiteInfo
    {
        private string _description = string.Empty;
        private string _domain = string.Empty;
        private string _sitename = string.Empty;
        private DateTime _addtime;
        private int _id;
        private int _sitetype;
        private int _status;
        private int _uid;

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

        public string Domain
        {
            get
            {
                return this._domain;
            }
            set
            {
                this._domain = value;
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

        public string SiteName
        {
            get
            {
                return this._sitename;
            }
            set
            {
                this._sitename = value;
            }
        }

        public int SiteType
        {
            get
            {
                return this._sitetype;
            }
            set
            {
                this._sitetype = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public int Uid
        {
            get
            {
                return this._uid;
            }
            set
            {
                this._uid = value;
            }
        }
    }
}
