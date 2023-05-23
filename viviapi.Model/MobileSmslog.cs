using System;

namespace viviapi.Model.SMS
{
    public class MobileSmslog
    {
        private int _id;
        private string _mobile;
        private DateTime _sendtime;
        private int _count;
        private string _clientip;
        private string _code;

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

        public string Mobile
        {
            get
            {
                return this._mobile;
            }
            set
            {
                this._mobile = value;
            }
        }

        public DateTime SendTime
        {
            get
            {
                return this._sendtime;
            }
            set
            {
                this._sendtime = value;
            }
        }

        public int Count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }

        public string ClientIP
        {
            get
            {
                return this._clientip;
            }
            set
            {
                this._clientip = value;
            }
        }

        public string Code
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
    }
}
