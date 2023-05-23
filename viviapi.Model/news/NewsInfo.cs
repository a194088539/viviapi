using System;

namespace viviapi.Model
{
    public class NewsInfo
    {
        private int _isbold = 0;
        private int _newsid;
        private int _newstype;
        private string _newstitle;
        private DateTime _addtime;
        private string _newscontent;
        private int _isred;
        private int _istop;
        private int _ispop;
        private string _color;

        public int newsid
        {
            get
            {
                return this._newsid;
            }
            set
            {
                this._newsid = value;
            }
        }

        public int newstype
        {
            get
            {
                return this._newstype;
            }
            set
            {
                this._newstype = value;
            }
        }

        public string newstitle
        {
            get
            {
                return this._newstitle;
            }
            set
            {
                this._newstitle = value;
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

        public string newscontent
        {
            get
            {
                return this._newscontent;
            }
            set
            {
                this._newscontent = value;
            }
        }

        public int IsRed
        {
            get
            {
                return this._isred;
            }
            set
            {
                this._isred = value;
            }
        }

        public int IsTop
        {
            get
            {
                return this._istop;
            }
            set
            {
                this._istop = value;
            }
        }

        public int IsPop
        {
            get
            {
                return this._ispop;
            }
            set
            {
                this._ispop = value;
            }
        }

        public int Isbold
        {
            get
            {
                return this._isbold;
            }
            set
            {
                this._isbold = value;
            }
        }

        public string Color
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
            }
        }

        public bool release { get; set; }
    }
}
