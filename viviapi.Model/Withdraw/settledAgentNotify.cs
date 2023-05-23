using System;

namespace viviapi.Model.Withdraw
{
    [Serializable]
    public class settledAgentNotify
    {
        private string _restext = string.Empty;
        private DateTime _addtime = DateTime.Now;
        private string _ext1 = string.Empty;
        private string _ext2 = string.Empty;
        private string _ext3 = string.Empty;
        private string _remark = string.Empty;
        private int _id;
        private string _notify_id;
        private int _userid;
        private string _trade_no;
        private string _out_trade_no;
        private int _notifystatus;
        private string _notifyurl;
        private DateTime? _restime;

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

        public string notify_id
        {
            get
            {
                return this._notify_id;
            }
            set
            {
                this._notify_id = value;
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

        public string trade_no
        {
            get
            {
                return this._trade_no;
            }
            set
            {
                this._trade_no = value;
            }
        }

        public string out_trade_no
        {
            get
            {
                return this._out_trade_no;
            }
            set
            {
                this._out_trade_no = value;
            }
        }

        public int notifystatus
        {
            get
            {
                return this._notifystatus;
            }
            set
            {
                this._notifystatus = value;
            }
        }

        public string notifyurl
        {
            get
            {
                return this._notifyurl;
            }
            set
            {
                this._notifyurl = value;
            }
        }

        public string resText
        {
            get
            {
                return this._restext;
            }
            set
            {
                this._restext = value;
            }
        }

        public DateTime addTime
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

        public DateTime? resTime
        {
            get
            {
                return this._restime;
            }
            set
            {
                this._restime = value;
            }
        }

        public string ext1
        {
            get
            {
                return this._ext1;
            }
            set
            {
                this._ext1 = value;
            }
        }

        public string ext2
        {
            get
            {
                return this._ext2;
            }
            set
            {
                this._ext2 = value;
            }
        }

        public string ext3
        {
            get
            {
                return this._ext3;
            }
            set
            {
                this._ext3 = value;
            }
        }

        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public settledAgentNotify()
        {
            this.notify_id = DateTime.Now.Ticks.ToString();
        }
    }
}
