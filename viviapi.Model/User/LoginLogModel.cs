using System;

namespace viviapi.Model.User
{
    public class UserLoginLog
    {
        private string _address = "";
        private string _endDate = DateTime.Now.ToString("yyyy-MM-dd");
        private int _id = 0;
        private string _lastip = "";
        private DateTime _lasttime = DateTime.Now;
        private int _pageSize = 1000;
        private string _remark = "";
        private string _startDate = "2000-01-01";
        private int _type = 0;
        private int _userid = 0;

        public string address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

        public string endDate
        {
            get
            {
                return this._endDate.Replace("'", "").Replace("=", "").Replace("--", "").Replace("xp_cmdshell", "").Replace("master", "").Replace("exec", "");
            }
            set
            {
                this._endDate = value.Replace("'", "").Replace("=", "").Replace("--", "").Replace("xp_cmdshell", "").Replace("master", "").Replace("exec", "");
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

        public string lastIP
        {
            get
            {
                return this._lastip;
            }
            set
            {
                this._lastip = value;
            }
        }

        public DateTime lastTime
        {
            get
            {
                return this._lasttime;
            }
            set
            {
                this._lasttime = value;
            }
        }

        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                this._pageSize = value;
            }
        }

        public string remark
        {
            get
            {
                return this._remark.Replace("'", "").Replace("=", "").Replace("--", "").Replace("xp_cmdshell", "").Replace("master", "").Replace("exec", "");
            }
            set
            {
                this._remark = value.Replace("'", "").Replace("=", "").Replace("--", "").Replace("xp_cmdshell", "").Replace("master", "").Replace("exec", "");
            }
        }

        public string startDate
        {
            get
            {
                return this._startDate.Replace("'", "").Replace("=", "").Replace("--", "").Replace("xp_cmdshell", "").Replace("master", "").Replace("exec", "");
            }
            set
            {
                this._startDate = value.Replace("'", "").Replace("=", "").Replace("--", "").Replace("xp_cmdshell", "").Replace("master", "").Replace("exec", "");
            }
        }

        public int type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public int userID
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
    }
}
