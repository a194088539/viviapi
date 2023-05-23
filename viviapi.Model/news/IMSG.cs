using System;

namespace viviapi.Model
{
    [Serializable]
    public class IMSG
    {
        private int _id;
        private int? _msg_from;
        private int? _msg_to;
        private string _msg_content;
        private DateTime? _msg_addtime;
        private string _msg_title;
        private bool _isRead;

        public bool isRead
        {
            get
            {
                return this._isRead;
            }
            set
            {
                this._isRead = value;
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

        public int? msg_from
        {
            get
            {
                return this._msg_from;
            }
            set
            {
                this._msg_from = value;
            }
        }

        public int? msg_to
        {
            get
            {
                return this._msg_to;
            }
            set
            {
                this._msg_to = value;
            }
        }

        public string msg_content
        {
            get
            {
                return this._msg_content;
            }
            set
            {
                this._msg_content = value;
            }
        }

        public DateTime? msg_addtime
        {
            get
            {
                return this._msg_addtime;
            }
            set
            {
                this._msg_addtime = value;
            }
        }

        public string msg_title
        {
            get
            {
                return this._msg_title;
            }
            set
            {
                this._msg_title = value;
            }
        }

        public string msg_fromname { get; set; }
    }
}
