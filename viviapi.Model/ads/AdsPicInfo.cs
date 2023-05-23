namespace viviapi.Model
{
    public class AdsPicInfo
    {
        private int _adsid;
        private string _adspicpath;
        private int _picid;
        private int _sizex;
        private int _sizey;

        public int AdsId
        {
            get
            {
                return this._adsid;
            }
            set
            {
                this._adsid = value;
            }
        }

        public string AdsPicPath
        {
            get
            {
                return this._adspicpath;
            }
            set
            {
                this._adspicpath = value;
            }
        }

        public int PicId
        {
            get
            {
                return this._picid;
            }
            set
            {
                this._picid = value;
            }
        }

        public int SizeX
        {
            get
            {
                return this._sizex;
            }
            set
            {
                this._sizex = value;
            }
        }

        public int SizeY
        {
            get
            {
                return this._sizey;
            }
            set
            {
                this._sizey = value;
            }
        }
    }
}
