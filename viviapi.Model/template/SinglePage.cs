using System;

namespace viviapi.Model
{
    public class SinglePage
    {
        private DateTime _addtime;
        private string _content;
        private string _interface1;
        private string _interface2;
        private int _sid;
        private string _title;

        public DateTime Addtime
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

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public string Interface1
        {
            get
            {
                return this._interface1;
            }
            set
            {
                this._interface1 = value;
            }
        }

        public string Interface2
        {
            get
            {
                return this._interface2;
            }
            set
            {
                this._interface2 = value;
            }
        }

        public int Sid
        {
            get
            {
                return this._sid;
            }
            set
            {
                this._sid = value;
            }
        }

        public string Title
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

        public SinglePage()
        {
        }

        public SinglePage(int _sid, string _title, string _content, DateTime _addtime, string _interface1, string _interface2)
        {
            this._sid = _sid;
            this._title = _title;
            this._content = _content;
            this._addtime = _addtime;
            this._interface1 = _interface1;
            this._interface2 = _interface2;
        }
    }
}
