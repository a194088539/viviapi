using System;

namespace viviapi.Model
{
    [Serializable]
    public class JuBaoInfo
    {
        private int _id;
        private string _name;
        private string _email;
        private string _tel;
        private string _url;
        private JuBaoEnum _type;
        private string _remark;
        private DateTime? _addtime;
        private JuBaoStatusEnum _status;
        private DateTime? _checktime;
        private int? _check;
        private string _checkremark;
        private string _pwd;
        private string _field1;
        private string _field2;
        private string _field3;

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

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public string tel
        {
            get
            {
                return this._tel;
            }
            set
            {
                this._tel = value;
            }
        }

        public string url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        public JuBaoEnum type
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

        public DateTime? addtime
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

        public JuBaoStatusEnum status
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

        public DateTime? checktime
        {
            get
            {
                return this._checktime;
            }
            set
            {
                this._checktime = value;
            }
        }

        public int? check
        {
            get
            {
                return this._check;
            }
            set
            {
                this._check = value;
            }
        }

        public string checkremark
        {
            get
            {
                return this._checkremark;
            }
            set
            {
                this._checkremark = value;
            }
        }

        public string pwd
        {
            get
            {
                return this._pwd;
            }
            set
            {
                this._pwd = value;
            }
        }

        public string field1
        {
            get
            {
                return this._field1;
            }
            set
            {
                this._field1 = value;
            }
        }

        public string field2
        {
            get
            {
                return this._field2;
            }
            set
            {
                this._field2 = value;
            }
        }

        public string field3
        {
            get
            {
                return this._field3;
            }
            set
            {
                this._field3 = value;
            }
        }
    }
}
