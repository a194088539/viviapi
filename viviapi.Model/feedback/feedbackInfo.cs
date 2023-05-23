using System;

namespace viviapi.Model
{
    public class feedbackInfo
    {
        private feedbacktype _typeid = feedbacktype.BUG反馈;
        private feedbackstatus _status = feedbackstatus.等待回复;
        private int _id;
        private int _userid;
        private string _title;
        private string _cont;
        private DateTime _addtime;
        private string _reply;
        private int? _replyer;
        private DateTime? _replytime;
        private string _clientip;

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

        public int userid
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

        public feedbacktype typeid
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public string cont
        {
            get
            {
                return this._cont;
            }
            set
            {
                this._cont = value;
            }
        }

        public feedbackstatus status
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

        public DateTime addtime
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

        public string reply
        {
            get
            {
                return this._reply;
            }
            set
            {
                this._reply = value;
            }
        }

        public int? replyer
        {
            get
            {
                return this._replyer;
            }
            set
            {
                this._replyer = value;
            }
        }

        public DateTime? replytime
        {
            get
            {
                return this._replytime;
            }
            set
            {
                this._replytime = value;
            }
        }

        public string clientip
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
    }
}
