using System;

namespace viviapi.Model.User
{
    public class usersIdImageInfo
    {
        private int _id;
        private int? _userid;
        private byte[] _image_on;
        private byte[] _image_down;
        private string _ptype;
        private int? _filesize;
        private string _ptype1;
        private int? _filesize1;
        private IdImageStatus _status;
        private string _why;
        private int? _admin;
        private DateTime? _checktime;
        private DateTime? _addtime;

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

        public int? userId
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

        public byte[] image_on
        {
            get
            {
                return this._image_on;
            }
            set
            {
                this._image_on = value;
            }
        }

        public byte[] image_down
        {
            get
            {
                return this._image_down;
            }
            set
            {
                this._image_down = value;
            }
        }

        public string ptype
        {
            get
            {
                return this._ptype;
            }
            set
            {
                this._ptype = value;
            }
        }

        public int? filesize
        {
            get
            {
                return this._filesize;
            }
            set
            {
                this._filesize = value;
            }
        }

        public string ptype1
        {
            get
            {
                return this._ptype1;
            }
            set
            {
                this._ptype1 = value;
            }
        }

        public int? filesize1
        {
            get
            {
                return this._filesize1;
            }
            set
            {
                this._filesize1 = value;
            }
        }

        public IdImageStatus status
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

        public string why
        {
            get
            {
                return this._why;
            }
            set
            {
                this._why = value;
            }
        }

        public int? admin
        {
            get
            {
                return this._admin;
            }
            set
            {
                this._admin = value;
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
    }
}
